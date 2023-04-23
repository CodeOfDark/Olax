using System.Net;
using System.Text;
using Newtonsoft.Json;
using Olax.Enums;
using Olax.Impl;
using Olax.Models;
using Olax.Structs;

namespace Olax;

public class OlaxContext
{
    public Config Config { get; }
    
    public List<IRoute> Routes { get; }

    public bool Https { get; }

    private readonly HttpListener _listener;
    
    public OlaxContext(Config config, bool useHttps=true)
    {
        Config = config;
        Https = useHttps;

        Routes = new List<IRoute>()
        {
            new AiPluginRoute(),
            new LogoRoute(),
            new OpenApiRoute()
        };
        
        _listener = new HttpListener()
        {
            Prefixes = { $"http{(useHttps ? "s" : "")}://{config.Host}:{config.Port}/" }
        };
    }

    public void Start()
    {
        _listener.Start();
        _listener.IgnoreWriteExceptions = true;
        
        new Thread(() =>
        {
            _listener.BeginGetContext(OnReceive, null);
        }).Start();
    }

    private void OnReceive(IAsyncResult ar)
    {
        var context = _listener.EndGetContext(ar);
     
        var path = (context.Request.Url?.AbsolutePath)?[1..];
        if (path == null)
            return;
        
        var route = Routes.FirstOrDefault(x => x.Route == path!);
        if (route == null)
        {
            Response(context, new Response
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Content = "Error 404 Not Found!"
            });
        }
        else
        {
            if (context.Request.HttpMethod == HttpMethod.Options.Method)
            {
                Response(context, new Response
                {
                    HttpStatusCode = HttpStatusCode.OK,
                }, route);
            }
            else
            {
                var parameters = new List<Option>();
                var body = new StreamReader(context.Request.InputStream).ReadToEnd();
                var values = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(body);
                if (values != null)
                {
                    foreach (var value in values)
                    {
                        switch (value.Value)
                        {
                            case string:
                                parameters.Add(new Option()
                                {
                                    Name = value.Key,
                                    Type = Types.String,
                                    Value = value.Value
                                });
                                break;
                            case long:
                                parameters.Add(new Option()
                                {
                                    Name = value.Key,
                                    Type = Types.Integer,
                                    Value = value.Value
                                });
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                }
                
                var response = route.Execute(this, context.Request, parameters);
                Response(context, response);
            }
            
        }

        _listener.BeginGetContext(OnReceive, null);
    }
    
    private void Response(HttpListenerContext context, Response response, IRoute? route = null)
    {
        byte[] buffer = response.Content switch
        {
            string => Encoding.UTF8.GetBytes(response.Content),
            byte[] => response.Content,
            _ => Array.Empty<byte>(),
        };

        if (response.ContentType != null)
            context.Response.ContentType = response.ContentType;
        
        context.Response.ContentLength64 = buffer.Length;
        context.Response.StatusCode = (int)response.HttpStatusCode;
        
        if (context.Request.HttpMethod == HttpMethod.Options.Method)
        {
            context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", $"OPTIONS, {route!.HttpMethod.Method}");
        }
        
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

        var output = context.Response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
    }
    
}