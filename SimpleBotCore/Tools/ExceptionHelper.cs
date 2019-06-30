using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBotCore.Tools
{
    public class ExceptionHelper
    {
        public static string RecuperarDescricaoErro(Exception ex)
        {
            StringBuilder str = new StringBuilder();

            if (ex != null)
            {
                if (ex.InnerException != null)
                {
                    str.AppendLine($"InnerException : {ex.InnerException.Message}");
                }

                if (ex.Message != null)
                {
                    str.AppendLine($"Message : {ex.Message}");
                }

                if (ex.StackTrace != null)
                {
                    str.AppendLine($"StackTrace : {ex.StackTrace}");
                }

            }

            return str.ToString();
        }
    }
}
