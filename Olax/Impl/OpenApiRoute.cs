using System.Net;
using System.Text;
using Olax.Enums;
using Olax.Models;
using Olax.Structs;

namespace Olax.Impl;

public class OpenApiRoute : IRoute
{
    public string Route => ".well-known/openapi.yaml";
    public string OperationId => "get_openai";
    public string Summary => "OpenApi yaml info";
    public HttpMethod HttpMethod => HttpMethod.Get;
    public List<Option> Parameters => new();

    private List<Type> _blackList = new()
    {
        typeof(OpenApiRoute),
        typeof(AiPluginRoute),
        typeof(LogoRoute)
    };
    
    public Response Execute(OlaxContext olaxContext, HttpListenerRequest httpListenerRequest, List<Option> parameters)
    {
        List<IRoute> operationSchemas = new();
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("openapi: 3.0.1");
        stringBuilder.AppendLine("info:");
        stringBuilder.AppendLine($" title: {olaxContext.Config.PluginName}");
        stringBuilder.AppendLine($" description: {olaxContext.Config.Description}");
        stringBuilder.AppendLine($" version: '{olaxContext.Config.Version}'");
        
        stringBuilder.AppendLine("servers:");
        stringBuilder.AppendLine($" - url: http{(olaxContext.Https ? "s" : "")}://{httpListenerRequest.Headers.Get("Host")}");

        stringBuilder.AppendLine("paths:");

        foreach (var route in olaxContext.Routes.Where(route => !_blackList.Contains(route.GetType())))
        {
            stringBuilder.AppendLine($" /{route.Route}:");

            stringBuilder.AppendLine($"  {route.HttpMethod.Method.ToLower()}:");
            stringBuilder.AppendLine($"   operationId: {route.OperationId}");
            stringBuilder.AppendLine($"   summary: {route.Summary}");

            if (route.Parameters.Count <= 0) 
                continue;
            
            stringBuilder.AppendLine($"   requestBody:");
            stringBuilder.AppendLine($"    content:");
            stringBuilder.AppendLine($"     application/json:");
            stringBuilder.AppendLine($"      schema:");
            stringBuilder.AppendLine($"       $ref: \"#/components/schemas/{route.OperationId}\"");
            stringBuilder.AppendLine($"       required: true");
                    
            operationSchemas.Add(route);
        }

        if (operationSchemas.Count > 0)
        {
            stringBuilder.AppendLine($"components:");
            stringBuilder.AppendLine($" schemas:");
        }
        
        foreach (var route in operationSchemas)
        {
            stringBuilder.AppendLine($"  {route.OperationId}:");
            stringBuilder.AppendLine($"   title: {route.OperationId}");
            stringBuilder.AppendLine($"   type: object");
            
            stringBuilder.AppendLine($"   required:");
            foreach (var parameter in route.Parameters)
                stringBuilder.AppendLine($"    - {parameter.Name}");
                
            stringBuilder.AppendLine($"   properties:");
            foreach (var parameter in route.Parameters)
            {
                stringBuilder.AppendLine($"    {parameter.Name}:");
                stringBuilder.AppendLine($"     title: {parameter.Name}");
                stringBuilder.AppendLine($"     type: {GetTypeAsString(parameter.Type)}");
            }
        }
        
        return new Response()
        {
            HttpStatusCode = HttpStatusCode.OK,
            Content = stringBuilder.ToString(),
            ContentType = "text/yaml"
        };
    }

    private string GetTypeAsString(Types type)
    {
        return type switch
        {
            Types.String => "string",
            Types.Integer => "integer",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}