using Confluent.Kafka;
using ExpenseTracker.Persistence.Kafka;
using ExpenseTracker.Persistence.LiteDb;
using Serilog;
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddHealthChecks();

            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

            var app = builder.Build();

            app.UseExceptionHandler(_ => { });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
