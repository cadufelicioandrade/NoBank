using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Infraestrutura
{
    public static class Utils
    {

        public static string PathToNameAssembly(string path)
        {
            var prefixoAssembly = "NoBank";
            var newPath = path.Replace("/", ".");

            return $"{prefixoAssembly}{newPath}";
        }

        public static string GetContentType(string path)
        {

            if (path.EndsWith(".css"))
                return "text/css; charset=utf-8";

            if (path.EndsWith(".js"))
                return "application/js; charset=utf-8";

            if (path.EndsWith(".html"))
                return "text/html; charset=utf-8";

            throw new NotImplementedException("Tipo de arquivo não esperado");
        }
    }
}
