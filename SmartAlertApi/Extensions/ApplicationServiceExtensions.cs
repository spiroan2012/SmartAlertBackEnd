using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartAlertApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SmartAlertContext>(x =>
                x.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IHallRepository, HallRepository>();
            //services.AddScoped<IShowRepository, ShowRepository>();
            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IBookingRepository, BookingRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            return services;
        }
    }
}
