using System.Net;
using Olax.Models;
using Olax.Structs;

namespace Olax.Impl;

public class AiPluginRoute : IRoute
{
    public string Route => ".well-known/ai-plugin.json";
    public string OperationId => "get_plugin";
    public string Summary => "ai plugin info";
    public HttpMethod HttpMethod => HttpMethod.Get;
    public List<Option> Parameters => new();
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        return new Response()
        {
            HttpStatusCode = HttpStatusCode.OK,
            Content = 
                $@"{{
    ""schema_version"": ""{olaxContext.Config.Version}"",
    ""name_for_human"": ""{olaxContext.Config.PluginName}"",
    ""name_for_model"": ""{olaxContext.Config.ModelName}"",
    ""description_for_human"": ""{olaxContext.Config.Description}"",
    ""description_for_model"": ""{olaxContext.Config.Description}"",
    ""auth"": {{
        ""type"": ""none""
    }},
    ""api"": {{
        ""type"": ""openapi"",
        ""url"": ""http{(olaxContext.Https ? "s" : "")}://{httpListenerRequest.Headers.Get("Host")}/.well-known/openapi.yaml"",
        ""is_user_authenticated"": false
    }},
    ""logo_url"": ""http{(olaxContext.Https ? "s" : "")}://{httpListenerRequest.Headers.Get("Host")}/.well-known/logo.png"",
    ""contact_email"": ""{olaxContext.Config.ContactEmail}"",
    ""legal_info_url"": ""{olaxContext.Config.LegalUrlInfo}""
}}",
            ContentType = "text/json"
        };
    }
}