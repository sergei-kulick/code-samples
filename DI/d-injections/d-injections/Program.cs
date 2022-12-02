namespace d_injections
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            // Add services to the container.
            var services = builder.Services;
            services.AddSingleton(config);
            services.AddControllers();
            services.AddHttpClient("named", (provider, client) =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var url = config.GetValue<string>("url");
                client.BaseAddress = new Uri(url);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}