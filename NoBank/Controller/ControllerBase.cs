using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Controller
{
    public class ControllerBase
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var diretorio = GetType().Name.Replace("Controller", "");
            var nomeResource = $"NoBank.Portal.View.{diretorio}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeResource);
            var conteudoPagina = String.Empty;

            using (var sr = new StreamReader(streamRecurso))
                conteudoPagina = sr.ReadToEnd();

            return conteudoPagina;
        }
    }
}
