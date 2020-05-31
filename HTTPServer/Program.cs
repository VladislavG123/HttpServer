using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Экземпляр сервера, в парамметрах указываются домен, порт и присутствует ли SSL
            var server = new Server("localhost", 8888);
            // Экземляр класса конфигурации запросов
            var startup = new Startup();
            Console.WriteLine("Server is running");
            while (true)
            {
                // Получаем запрос
                var request = server.GetRequest();

                Console.WriteLine("Request has gotten");

                // Возвращаем ответ, возвращаемый конфигурационным методом
                server.RespondToRequest(startup.Configure(request));
            }            
        }
    }
}
