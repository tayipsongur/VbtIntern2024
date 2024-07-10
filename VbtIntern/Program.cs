
using Cache.RedisCache;
using Microsoft.EntityFrameworkCore;
using VbtIntern.Context;
using VbtIntern.Services;

namespace VbtIntern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.x

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database Inject
            builder.Services.AddDbContext<VbtContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //RedisCache Inject
            builder.Services.AddSingleton<RedisConfigurationService>();
            builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

            //ServiceInject
            builder.Services.AddTransient<IUserService, UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
