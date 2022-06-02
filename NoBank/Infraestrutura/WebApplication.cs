using System.Net;
using System.Reflection;
using System.Text;

namespace NoBank.Infraestrutura
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

            var assembly = Assembly.GetExecutingAssembly();
            var nomeResource = Utils.PathToNameAssembly(path);

            var resourceStrem = assembly.GetManifestResourceStream(nomeResource);
            var bytesResource = new byte[resourceStrem.Length];

            resourceStrem.Read(bytesResource, 0, (int)resourceStrem.Length);

            response.ContentType = Utils.GetContentType(path);
            response.StatusCode = 200;
            response.ContentLength64 = resourceStrem.Length;

            response.OutputStream.Write(bytesResource, 0, bytesResource.Length);
            response.OutputStream.Close();

            listener.Stop();
        }
    }
}
