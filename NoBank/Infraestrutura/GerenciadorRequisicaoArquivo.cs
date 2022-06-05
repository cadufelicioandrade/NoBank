using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NoBank.Portal.Infraestrutura
{
    public class GerenciadorRequisicaoArquivo
    {
        public void Gerenciar(HttpListenerResponse response, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var nomeResource = Utils.PathToNameAssembly(path);

            var resourceStrem = assembly.GetManifestResourceStream(nomeResource);

            if (resourceStrem is null)
            {
                response.StatusCode = 404;
                response.OutputStream.Close();
            }
            else
            {
                using (resourceStrem)
                {
                    var bytesResource = new byte[resourceStrem.Length];
                    resourceStrem.Read(bytesResource, 0, (int)resourceStrem.Length);
                    response.ContentType = Utils.GetContentType(path);
                    response.StatusCode = 200;
                    response.ContentLength64 = resourceStrem.Length;
                    response.OutputStream.Write(bytesResource, 0, bytesResource.Length);
                    response.OutputStream.Close();
                }
            }
        }
    }
}
