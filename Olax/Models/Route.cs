using System.Net;
using Olax.Enums;

namespace Olax.Models;

public abstract class Route : IRoute
{
    public abstract string Path { get; set; }
    public abstract Dictionary<string, Types> Parameters { get; set; }
    public abstract Dictionary<string, Types> Body { get; set; }
    public abstract void Execute(Olax olex, Dictionary<string, Types> parameters, Dictionary<string, Types> body);

    public OlaxResponse Response(HttpStatusCode responseStatusCode, string responseBody)
    {
        throw new NotImplementedException();
    }
}