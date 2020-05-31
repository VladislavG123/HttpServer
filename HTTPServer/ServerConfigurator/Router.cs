using System;
using System.Linq;
using System.Reflection;

namespace HTTPServer.ServerConfigurator
{
    /// <summary>
    /// Ищет нужный контроллер и action по URL
    /// </summary>
    public class Router
    {
        /// <summary>
        /// Паттерн URL
        /// </summary>
        private readonly Uri pattern;

        /// <summary>
        /// URL текущего запроса от клиента
        /// </summary>
        private readonly Uri requestUrl;

        /// <summary>
        /// Значение контроллера из url по шаблону
        /// </summary>
        private string controller = "HttpResonse";

        /// <summary>
        /// Значение action из url по шаблону
        /// </summary>
        private string action = "NotFound";

        /// <summary>
        /// </summary>
        /// <param name="pattern">Паттерн Url. Пример: http://domain/{controller}/{action}</param>
        /// <param name="requestUrl">Url запроса</param>
        public Router(Uri pattern, Uri requestUrl)
        {
            this.pattern = pattern;
            this.requestUrl = requestUrl;
        }

        /// <summary>
        /// Парсинг контроллера и action-a из Url запроса
        /// </summary>
        private void ParseUrl()
        {
            if (pattern.Segments.Length != requestUrl.Segments.Length ||
                !pattern.Segments.Any(x => x.Contains("controller")) ||
                !pattern.Segments.Any(x => x.Contains("action")))
                return;

            for (int i = 0; i < pattern.Segments.Length; i++)
            {
                if (pattern.Segments[i].Contains("controller"))
                {
                    // Убераем лишние слеши по концам 
                    var controller = requestUrl.Segments[i].Trim('/').Trim('\\');
                    if (controller.Length == 0) return;

                    this.controller = controller;
                    
                }
                else if (pattern.Segments[i].Contains("action"))
                {
                    var action = requestUrl.Segments[i].Trim('/').Trim('\\');
                    if(action.Length == 0) return;

                    this.action = action;
                }
            }

        }

        /// <summary>
        /// Вызов Action
        /// </summary>
        /// <returns>Ответ клиенту</returns>
        public string InvokeAction()
        {
            // Валидируем данные клиента
            ParseUrl();

            return InvokeAction(controller, action);
        }

        /// <summary>
        /// Возврат ошибки 404
        /// </summary>
        /// <returns></returns>
        private string NotFound()
        {
            return InvokeAction("HttpResonse", "NotFound");
        }

        /// <summary>
        /// Вызов Action
        /// </summary>
        /// <param name="controller">Тип контроллера</param>
        /// <param name="action">Название метода</param>
        /// <returns></returns>
        private string InvokeAction(string controller, string action)
        {
            // Ищем тип контроллера по строке
            var controllerType = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .FirstOrDefault(t => t.IsClass &&
                              t.Name.Contains($"{controller}Controller"));

            if (controllerType is null)
            {
                return NotFound();
            }

            // Создаём объект контроллера через его конструктор
            var constructor = controllerType.GetConstructor(Type.EmptyTypes);
            object ontrollerObject = constructor.Invoke(new object[] { });

            // Ищим нужный метод
            MethodInfo method = controllerType.GetMethod(action + "Action");

            if (method is null)
            {
                return NotFound();
            }

            // Вызываем метод и возвращаем его значение
            return method.Invoke(ontrollerObject, null) as string;
        }

    }
}
