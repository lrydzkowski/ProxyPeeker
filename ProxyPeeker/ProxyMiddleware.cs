using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using ProxyPeeker.Options;
using ProxyPeeker.Storage;

namespace ProxyPeeker;

public class ProxyMiddleware
{
    private readonly ILogger<ProxyMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly ProxyOptions _options;

    public ProxyMiddleware(
        RequestDelegate next,
        IOptions<ProxyOptions> options,
        ILogger<ProxyMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, IRequestRepository requestRepository)
    {
        if (context.Request.Path.Value == null)
        {
            await _next(context);

            return;
        }

        ProxyMappingOptions? mapping = _options.Mapping.FirstOrDefault(
            x => x.Path.Equals(context.Request.Path.Value, StringComparison.InvariantCultureIgnoreCase)
        );
        if (mapping == null)
        {
            await _next(context);

            return;
        }

        IHttpClientFactory clientFactory = context.RequestServices.GetRequiredService<IHttpClientFactory>();
        HttpClient client = clientFactory.CreateClient();

        HttpRequestMessage request = new(new HttpMethod(context.Request.Method), mapping.Url);
        string? requestBody = null;
        if (context.Request.Method == HttpMethods.Post)
        {
            requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            request.Content = new StringContent(requestBody, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        foreach (KeyValuePair<string, StringValues> header in context.Request.Headers)
        {
            if (!header.Key.StartsWith("x-", StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }

        HttpResponseMessage response = await client.SendAsync(request);

        string requestUrl = mapping.Url;
        string requestMethod = context.Request.Method;
        HttpStatusCode responseStatusCode = response.StatusCode;
        string responseBody = await response.Content.ReadAsStringAsync();
        LogRequest(requestUrl, requestMethod, responseStatusCode);
        await SaveRequestAsync(
            requestRepository,
            requestUrl,
            requestMethod,
            requestBody,
            responseStatusCode,
            responseBody
        );

        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(responseBody);
    }

    private void LogRequest(
        string requestUrl,
        string requestMethod,
        HttpStatusCode responseStatusCode
    )
    {
        _logger.LogInformation(
            "Request URL: {RequestUrl}, Request method: {RequestMethod}, Response status code: {ResponseStatusCode}",
            requestUrl,
            requestMethod,
            responseStatusCode
        );
    }

    private async Task SaveRequestAsync(
        IRequestRepository requestRepository,
        string requestUrl,
        string requestMethod,
        string? requestBody,
        HttpStatusCode responseStatusCode,
        string responseBody
    )
    {
        await requestRepository.SaveRequestAsync(
            requestUrl,
            requestMethod,
            requestBody,
            responseStatusCode.ToString(),
            responseBody
        );
    }
}
