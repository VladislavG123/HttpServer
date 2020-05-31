using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Controllers
{
    /// <summary>
    /// Тестовый контроллер
    /// При вызове контроллера в клиенте, опускать приставку Controller
    /// </summary>
    class TestController
    {
        /// <summary>
        /// Приемер Action-a
        /// При вызове с клиента, опускать приставку Action
        /// В нашем случае - http://localhost:8888/Test/Index
        /// </summary>
        /// <returns></returns>
        public string IndexAction()
        {
            return "HelloWorld";
        }

        public string SomeAction()
        {
            return "SomeAction";
        }
    }
}
