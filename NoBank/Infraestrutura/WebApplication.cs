using System.Net;
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
            var listener = new HttpListener();
            
            listener.Start();

            foreach (var prefiso in _prefixos)
                listener.Prefixes.Add(prefiso);

            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            var conteudo = "<h1>Hello World</h1>";
            var conteudoBytes = Encoding.UTF8.GetBytes(conteudo);

            response.ContentType = "text/html; charset=utf-8";
            response.StatusCode = 200;
            response.ContentLength64 = conteudoBytes.Length;

            response.OutputStream.Write(conteudoBytes, 0, conteudoBytes.Length);
            response.OutputStream.Close();

            listener.Stop();
        }
    }
}
