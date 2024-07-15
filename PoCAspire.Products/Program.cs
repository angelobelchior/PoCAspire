var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseOutputCache();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new[]
{
    "Xbox Series X", "PlayStation 5", "Nintendo Switch", "Oculus Quest 2", "Apple Watch Series 6", "Samsung Galaxy S21", "Google Pixel 5", "Amazon Echo Dot", "Apple AirPods Pro", "DJI Mavic Air 2"
};

app.MapGet("/products", () =>
    {
        Console.WriteLine("Não passei no cache");
        return products;
    })
    .CacheOutput()
    .WithName("GetProducts")
    .WithOpenApi();

app.Run();