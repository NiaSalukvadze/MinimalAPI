using System.Data.SqlClient;
using System.Data;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMvc();

        var programAssembly = typeof(Program).Assembly;

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(programAssembly));

        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        builder.Services.AddScoped<IDbConnection>(provider =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        app.Run();
    }
}
