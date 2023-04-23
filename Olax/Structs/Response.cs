using System.Net;

namespace Olax.Structs;

public struct Response
{
    public HttpStatusCode HttpStatusCode { get; init; }
    public string? ContentType { get; init; }
    public dynamic? Content { get; init; }
}