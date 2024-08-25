
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Yaml;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using Substrate.NET.Indexer.Data;
using Substrate.Integration.Helper;
using System.Text.Json.Serialization;

namespace Substrate.NET.Indexer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo
                .Console()
                .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);

            // Set up configuration with YAML file
            builder.Configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("config.yml", optional: false, reloadOnChange: true);


            builder.Services.AddDbContext<IndexerContext>
                (opt => opt.UseInMemoryDatabase("IndexerServiceDb"));

            // Add services to the container
            builder.Services.AddHostedService<IndexerUpdateService>();

            // Add json converters
            builder.Services
                .AddControllers().
                AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new BigIntegerConverter());
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
