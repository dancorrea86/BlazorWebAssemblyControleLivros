var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient(); // Isso permite que o _httpClientFactory funcione

// 1. Defina a política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7012") // A URL do seu Blazor
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseCors("PermitirBlazor");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
