using NoBank.Infraestrutura;

Console.WriteLine("Hello, World!");

string[] prefixos = new string[]
{
    "http://localhost:5354/"
};


var webApp = new WebApplication(prefixos);

webApp.StartWebApplication();

Console.ReadLine();
