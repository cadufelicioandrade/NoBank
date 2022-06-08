using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Infraestrutura.Binding
{
    public class ActionBinder
    {
        public object GetMethoInfo(object? controller, string path)
        {
            // /Cambio/Calculo?moedaOrigem=BRL&moedaDestino=USD&valor=10
            // /Cambio/Calculo?moedaDestino=USD&moedaOrigem=BRL&valor=10
            // /Cambio/Calculo?valor=10&moedaOrigem=BRL&moedaDestino=USD
            // /Cambio/Calculo?moedaDestino=USD&valor=10

            var idxInterrogacao = path.IndexOf('?');
            var existQueryString = idxInterrogacao > -1;
            string nomeAction = String.Empty;

            if (!existQueryString)
            {
                nomeAction = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
                var methoInfo = controller.GetType().GetMethod(nomeAction);
                return methoInfo;
            }
            
            var nomeControllerAction = path.Substring(0, idxInterrogacao);
            nomeAction = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
            var queryString = path.Substring(idxInterrogacao + 1);

            var tuplasNomeValor = GetArgumentNomeValor(queryString);

        }

        private IEnumerable<ArgumentoNomeValor> GetArgumentNomeValor(string queryString)
        {
            var tuplasNomeValor = queryString.Split('&', StringSplitOptions.RemoveEmptyEntries);

            foreach (var tupla in tuplasNomeValor)
            {
                var partesTupla = tupla.Split('=', StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentoNomeValor(partesTupla[0], partesTupla[1]);
            }
        }
    }
}
