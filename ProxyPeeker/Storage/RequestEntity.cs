namespace ProxyPeeker.Storage;

public class RequestEntity
{
    public long? Id { get; set; }

    public DateTime? SendAt { get; set; }

    public string? RequestUrl { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestBody { get; set; }

    public string? ResponseStatusCode { get; set; }

    public string? ResponseBody { get; set; }
}
