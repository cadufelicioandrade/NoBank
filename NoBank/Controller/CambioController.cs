using NoBank.Service;
using NoBank.Service.Cambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Controller
{
    public class CambioController
    {
        private readonly ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string BRL()
        {
            var valorFinal = _cambioService.Calcular("BRL", "USD", 1);

            var nomeCompletoResource = "NoBank.Portal.View.Cambio.BRL.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var sr = new StreamReader(streamRecurso);

            var textPage = sr.ReadToEnd();

            var textResult = textPage.Replace("VALOR_REAIS", valorFinal.ToString());

            return textResult;
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);

            var nomeCompletoResource = "NoBank.Portal.View.Cambio.USD.html";
            var assembly = Assembly.GetExecutingAssembly();

            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var sr = new StreamReader(streamRecurso);

            var textPage = sr.ReadToEnd();

            var textResult = textPage.Replace("VALOR_DOLAR", valorFinal.ToString());

            return textResult;
            return textResult;
        }
    }
}
