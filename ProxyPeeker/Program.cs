using ProxyPeeker;
using ProxyPeeker.Storage;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ProxyMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

AppDbInitializer.Init(app.Services);

app.Run();
