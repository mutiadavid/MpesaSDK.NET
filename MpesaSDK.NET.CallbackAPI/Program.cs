using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = AppDomain.CurrentDomain.FriendlyName, Version = "v1" });
});


WebApplication app = builder.Build();

//swagger
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppDomain.CurrentDomain.FriendlyName} - v1"));

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();