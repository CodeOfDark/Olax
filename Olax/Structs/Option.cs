using Olax.Enums;

namespace Olax.Structs;

public struct Option
{
    public string Name { get; init; }
    public dynamic Value { get; init; }
    public Types Type { get; init; }
}