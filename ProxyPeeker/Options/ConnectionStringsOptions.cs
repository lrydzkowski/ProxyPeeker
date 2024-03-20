namespace ProxyPeeker.Options;

public class ConnectionStringsOptions
{
    public const string Position = "ConnectionStrings";

    public string? SqlServerDb { get; init; }
}
