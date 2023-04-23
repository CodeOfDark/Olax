using System.Net;
using Olax.Models;
using Olax.Structs;

namespace Olax.Impl;

public class LogoRoute : IRoute
{
    public string Route => ".well-known/logo.png";
    public string OperationId => "get_logo";
    public string Summary => "logo of the plugin";
    public HttpMethod HttpMethod => HttpMethod.Get;
    public List<Option> Parameters => new();
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        return new Response()
        {
            HttpStatusCode = HttpStatusCode.OK,
            Content = File.ReadAllBytes(olaxContext.Config.Logo),
            ContentType = "image/png"
        };
    }
}