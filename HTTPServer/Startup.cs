using HTTPServer.ServerConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTPServer
{
    /// <summary>
    /// Класс конфигурации запросов
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Конфигурация запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <returns>Ответ клиенту</returns>
        public string Configure(HttpListenerRequest request)
        {
            // Пользовательские Middlewares

            // Возвращаем ответ от роутера
            return new Router(
                    new Uri("http://domain/{controller}/{action}"),
                    request.Url).InvokeAction();
        }


    }
}
