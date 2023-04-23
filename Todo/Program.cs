using System.Net;
using Olax;
using Olax.Enums;
using Olax.Models;
using Olax.Structs;

namespace Todo;

public static class Program
{
    internal static void Main(string[] args)
    {
        var config = new Config()
        {
            Logo = @"your plugin logo",
            Version = "v1",
            PluginName = "TODO plugin",
            ModelName = "todo",
            Description = "Example about todo plugin",
            Host = "127.0.0.1",
            Port = 8800,
            ContactEmail = "test@test.com",
            LegalUrlInfo = "https://www.nozhdar.com"
        };
        
        var olax = new OlaxContext(config, false);
        olax.Routes.Add(new GetTodosRoute());
        olax.Routes.Add(new AddTodoRoute());
        olax.Routes.Add(new DeleteTodoRoute());
        olax.Start();

        Console.Read();
    }

}