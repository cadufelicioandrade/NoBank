using NoBank.Portal.Infraestrutura;


string[] prefixos = new string[]
{
    "http://localhost:5354/"
};

var webApp = new WebApplication(prefixos);
webApp.StartWebApplication();
