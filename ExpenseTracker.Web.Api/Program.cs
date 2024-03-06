using Asp.Versioning;
using Confluent.Kafka;
using ExpenseTracker.Persistence.EfCore;
using ExpenseTracker.Persistence.Kafka;
using ExpenseTracker.Persistence.LiteDb;
using ExpenseTracker.Web.Api.Swagger;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace ExpenseTracker.Web.Api
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AddPersistenceProvider(builder);
            
            builder.Services.AddAutoMapper(typeof(ApiProfile));
          
            builder.Services.AddCors(option => option.AddPolicy("AllowPolicy", build =>
            {
                build.WithOrigins(builder.Configuration.GetValue<string>("Settings:WebAppBlazorBaseUrl")!).AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsConfig>();
            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("X-Api-Version"));
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddHealthChecks();

            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            var app = builder.Build();

            app.UseExceptionHandler(_ => { });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var descriptions = app.DescribeApiVersions();

                    foreach (var description in descriptions)
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });
            }

            app.UseCors("AllowPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/health");

            app.Run();
        }

        private static void AddPersistenceProvider(WebApplicationBuilder builder)
        {
            switch(builder.Configuration.GetValue<string>("Settings:PersistenceProvider")!)
            {
                case "EfCore":
                {
                    builder.Services.Configure<EfCoreOptions>(builder.Configuration.GetSection("Settings:EfCoreOptions"));
                    builder.Services.AddEfCore();
                    builder.Services.AddAutoMapper(typeof(EfCoreProfile));
                    break;
                }
                case "LiteDb":
                {
                    builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("Settings:LiteDbOptions"));
                    builder.Services.AddLiteDb();
                    builder.Services.AddAutoMapper(typeof(LiteDbProfile));
                    break;
                }
                case "Kafka":
                {
                    builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Settings:KafkaOptions"));
                    builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection("Settings:KafkaOptions"));
                    builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("Settings:KafkaOptions"));
                    builder.Services.AddKafka();
                    builder.Services.AddAutoMapper(typeof(KafkaProfile));
                    break;
                }
            }
        }
    }
}
