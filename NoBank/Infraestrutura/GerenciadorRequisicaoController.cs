using NoBank.Portal.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Infraestrutura
{
    public class GerenciadorRequisicaoController
    {
        public void Gerenciar(HttpListenerResponse response, string path)
        {
            var arrPath = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = arrPath[0];
            var actionNome = arrPath[1];

            var fullcontroller = $"NoBank.Portal.Controller.{controllerNome}Controller";

            var controllerWrapper = Activator.CreateInstance("NoBank.Portal", fullcontroller, new object[0]);
            var controller = controllerWrapper.Unwrap();

            var methoInfo = controller.GetType().GetMethod(actionNome);
            var resultAction = (string)methoInfo.Invoke(controller, new object[0]);

            var bufferFile = Encoding.UTF8.GetBytes(resultAction);
            response.StatusCode = 200;
            response.ContentType = "text/html; charset=utf-8";
            response.ContentLength64 = bufferFile.Length;
            response.OutputStream.Write(bufferFile, 0, bufferFile.Length);
            response.OutputStream.Close();
        }
    }
}
