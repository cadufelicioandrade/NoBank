using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Infraestrutura.Binding
{
    public class ActionBinder
    {
        public ActionBindInfo GetActionBindInfo(object? controller, string path)
        {
            // /Cambio/Calculo?moedaOrigem=BRL&moedaDestino=USD&valor=10
            // /Cambio/Calculo?moedaDestino=USD&moedaOrigem=BRL&valor=10
            // /Cambio/Calculo?valor=10&moedaOrigem=BRL&moedaDestino=USD
            // /Cambio/Calculo?moedaDestino=USD&valor=10

            var idxInterrogacao = path.IndexOf('?');
            var existQueryString = idxInterrogacao > -1;
            string nomeAction = String.Empty;
            MethodInfo methodInfo = null;

            if (!existQueryString)
            {
                nomeAction = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
                methodInfo = controller.GetType().GetMethod(nomeAction);

                return new ActionBindInfo(methodInfo, Enumerable.Empty<ArgumentoNomeValor>());
            }

            var nomeControllerAction = path.Substring(0, idxInterrogacao);
            nomeAction = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
            var queryString = path.Substring(idxInterrogacao + 1);

            var tuplasNomeValor = GetArgumentNomeValor(queryString);
            var nomeArgs = tuplasNomeValor.Select(t => t.Nome).ToArray();

            methodInfo = GetMethodOfNameArguments(nomeAction, nomeArgs, controller);

            return new ActionBindInfo(methodInfo, tuplasNomeValor);
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

        private MethodInfo GetMethodOfNameArguments(string nomeAction, string[] argumentos, object controller)
        {
            var argumentosCount = argumentos.Length;

            var bindingFlags = BindingFlags.Instance |
                                BindingFlags.Static |
                                BindingFlags.Public |
                                BindingFlags.DeclaredOnly;

            var metodos = controller.GetType().GetMethods(bindingFlags);
            var sobreCargas = metodos.Where(m => m.Name == nomeAction);

            foreach (var sobreCarga in sobreCargas)
            {
                var paramentros = sobreCarga.GetParameters();

                if (argumentosCount != paramentros.Length)
                    continue;

                var match = paramentros.All(p => argumentos.Contains(p.Name));

                if (match)
                    return sobreCarga;

            }

            throw new ArgumentException($"Método {nomeAction} não encontrado!");
        }
    }
}
