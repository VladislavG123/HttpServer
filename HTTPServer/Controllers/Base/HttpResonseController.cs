using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPServer.Controllers.Base
{
    /// <summary>
    /// Контроллер выова константных ошибок
    /// </summary>
    public class HttpResonseController
    {
        /// <summary>
        /// Ошибка 404
        /// </summary>
        /// <returns></returns>
        public string NotFoundAction()
        {
            return "{ error: \"404\" }";
        }
    }
}
