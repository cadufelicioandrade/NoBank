using NoBank.Portal.Controller;
using System.Net;
using System.Reflection;
using System.Text;

namespace NoBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;

        public WebApplication(string[] prefixo)
        {
            _prefixos = prefixo ?? throw new ArgumentNullException(nameof(prefixo));
        }

        public void StartWebApplication()
        {
            while (true)
                ManipularRequisicao();
        }

        private void ManipularRequisicao()
        {
            var listener = new HttpListener();

            listener.Start();

            foreach (var prefiso in _prefixos)
                listener.Prefixes.Add(prefiso);

            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            var path = request.Url.AbsolutePath;

            if (Utils.IsFile(path))
            {
                var gerenciadorArquivo = new GerenciadorRequisicaoArquivo();
                gerenciadorArquivo.Gerenciar(response, path);
            }
            else
            {
                var gerenciadorController = new GerenciadorRequisicaoController();
                gerenciadorController.Gerenciar(response, path);
            }

            listener.Stop();
        }
    }
}
