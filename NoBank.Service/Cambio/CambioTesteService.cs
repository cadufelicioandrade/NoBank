using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Service.Cambio
{
    public class CambioTesteService : ICambioService
    {
        private readonly Random _random = new Random();

        public decimal Calcular(string moedaOrigem, string moedaDestino, decimal valor)
        {

            if (moedaOrigem == "BRL" && moedaDestino == "USD")
                return (valor * (decimal)_random.NextDouble()) + 4;
            else if (moedaOrigem == "USD" && moedaDestino == "BRL")
                return valor * (decimal)_random.NextDouble(); 

            return 0;
        }
    }
}
