namespace ProxyPeeker.Storage;

public interface IRequestRepository
{
    Task SaveRequestAsync(
        string? requestUrl,
        string? requestMethod,
        string? requestBody,
        string? responseStatusCode,
        string? responseBody
    );
}

public class RequestRepository
    : IRequestRepository
{
    private readonly AppDbContext _dbContext;

    public RequestRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveRequestAsync(
        string? requestUrl,
        string? requestMethod,
        string? requestBody,
        string? responseStatusCode,
        string? responseBody
    )
    {
        RequestEntity entity = new()
        {
            SendAt = DateTime.UtcNow,
            RequestUrl = requestUrl,
            RequestMethod = requestMethod,
            RequestBody = requestBody,
            ResponseStatusCode = responseStatusCode,
            ResponseBody = responseBody
        };
        await _dbContext.Requests.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}
