using Api.Extensions;
using Application;
using Infrastructure;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //agregando capa de infrastructure y application
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // OJO!!! este es un método de extensión que ejecuta las migraciones
        // se explica en el video número 53 del curso de udemy
        // (migración con ef)
        app.ApplyMigrations();

        // OJO!!! este es un método de extensión que alimenta con data de prueba las 
        // tablas de la db, esto se explica en el video 55 del curso de udemy
        // (data de prueba con dapper)
        // app.SeedDatabaseData();


        app.MapControllers();

        app.Run(); 
    }
}
