using SuperRate.API.Infrastructure.Extensions;
using SuperRate.API.Infrastructure.Middlewares;

namespace SuperRate.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddServices();

        builder.Services.AddDbContextAndIdentity(builder.Configuration);

        builder.Services.AddTokenAuthorization(builder.Configuration);

        builder.Services.AddCustomValidators();

        builder.Services.UseConfiguredCors();

        builder.Services.AddControllers();

        builder.Services.UseSwaggerConfiguration();

        var app = builder.Build();

        app.UseMiddleware<CultureMiddleware>();
        //app.UseMiddleware<CustomExceptionHandlerMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseCors("AllowAllOrigins");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}