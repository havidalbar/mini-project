using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Authentication;
using Persistence.DatabaseContext;
using Persistence.Redis;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        const string dbConnection = "server=127.0.0.1;port=8889;pooling=true;user=root;password=root;database=learn;sslMode=Preferred";

        services.AddDbContext<TableContext>(opt => opt.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddScoped<ITodoDetailRepository, TodoDetailRepository>();

        return services;
    }

    public static IServiceCollection AddRedisServices(this IServiceCollection services)
    {
        services.AddSingleton<RedisServer>();
        services.AddSingleton<ICacheService, RedisCacheService>();
        return services;
    }

    public static IServiceCollection AddPasswordServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHashingService, PasswordHashingService>();
        return services;
    }
}