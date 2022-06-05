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
    public class CambioController : ControllerBase
    {
        private readonly ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string BRL()
        {
            var valorFinal = _cambioService.Calcular("BRL", "USD", 1);
            var resutlView = View();

            return resutlView.Replace("VALOR_MOEDA", valorFinal.ToString()); 
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);
            var resutlView = View();

            return resutlView.Replace("VALOR_MOEDA", valorFinal.ToString());
        }
    }
}
