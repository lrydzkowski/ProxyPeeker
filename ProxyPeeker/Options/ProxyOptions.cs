namespace ProxyPeeker.Options;

public class ProxyOptions
{
    public const string Position = "Proxy";

    public List<ProxyMappingOptions> Mapping { get; init; } = new();
}

public class ProxyMappingOptions
{
    public string Path { get; init; } = "";

    public string Url { get; init; } = "";
}
