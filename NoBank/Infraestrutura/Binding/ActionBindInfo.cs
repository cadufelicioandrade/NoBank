using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Infraestrutura.Binding
{
    public class ActionBindInfo
    {

        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<ArgumentoNomeValor> TuplasArgumentoNomeValor { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, 
        IEnumerable<ArgumentoNomeValor> tuplasArgumentoNomeValor)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));

            if (tuplasArgumentoNomeValor is null)
                throw new ArgumentNullException(nameof(tuplasArgumentoNomeValor));

            TuplasArgumentoNomeValor = new ReadOnlyCollection<ArgumentoNomeValor>(tuplasArgumentoNomeValor.ToList());
        }


        public object Invoke(object controller)
        {
            var countParam = TuplasArgumentoNomeValor.Count;
            var hasArguments = countParam > 0;

            if (!hasArguments)
                return MethodInfo.Invoke(controller, new object[0]);

            var paramMethoInfo = MethodInfo.GetParameters();
            var paramsInvoke = new object[countParam];

            for (var i = 0; i < countParam; i++)
            {
                var param = paramMethoInfo[i];
                var paramNome = param.Name;
                var arg = TuplasArgumentoNomeValor.Single(t => t.Nome == paramNome);
                paramsInvoke[i] = Convert.ChangeType(arg.Valor,param.ParameterType);
            }

            return MethodInfo.Invoke(controller, paramsInvoke);
        }

    }
}
