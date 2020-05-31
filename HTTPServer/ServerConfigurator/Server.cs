using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HTTPServer
{
    public class Server
    {
        private HttpListener _listener;
        private HttpListenerContext _context;

        /// <summary>
        /// Конструктор сервера
        /// </summary>
        /// <param name="domain">Доменное имя или IP</param>
        /// <param name="port">Порт. По умолчанию - 80</param>
        /// <param name="enableSSL">Включен ли SSL. По умолчанию - false</param>
        public Server(string domain, int port = 80, bool enableSSL = false)
        {
            // Создаём слушатель на указанный URL адрес
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http{(enableSSL ? "s" : "")}://{domain}:{port}/");
        }

        /// <summary>
        /// Ожидание запроса от клиента
        /// </summary>
        /// <returns>Запрос</returns>
        public HttpListenerRequest GetRequest()
        {
            _listener.Start();
            _context = _listener.GetContext();
            return _context.Request;
        }

        /// <summary>
        /// Ответ за запрос пользователя
        /// </summary>
        /// <param name="response">Ответ</param>
        public void RespondToRequest(string response)
        {
            // Получаем объект ответа
            HttpListenerResponse httpResponse = _context.Response;

            // Переводим ответ в байты
            byte[] buffer = Encoding.UTF8.GetBytes(response);

            // Получаем поток ответа и пишем в него ответ
            httpResponse.ContentLength64 = buffer.Length;
            Stream output = httpResponse.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // Закрываем поток
            output.Close();
        }
    }
}
