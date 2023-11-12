using Presentation.Modules.Authentication;
using Presentation.Modules.Cors;
using Presentation.Modules.Injection;
using Presentation.Modules.Swagger;
using Presentation.Modules.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Authentication
builder.Services.AddCustomCors();
builder.Services.AddCustomAuthentication(builder);
builder.Services.AddCustomInjection(builder);
builder.Services.AddCustomValidators();
builder.Services.AddCustomSwagger();

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIXEL Technology Services API Market v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("pixelCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
