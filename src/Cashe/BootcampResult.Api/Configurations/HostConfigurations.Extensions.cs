using BootcampResult.Application.Common.Identity.Services;
using BootcampResult.Infrastructure.Common.Identity.Services;
using BootcampResult.Infrastructure.Common.Settings;
using BootcampResult.Persistence.Cashing.Brokers;
using BootcampResult.Persistence.DataContext;
using BootcampResult.Persistence.Interceptors;
using BootcampResult.Persistence.Repositories;
using BootcampResult.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BootcampResult.Api.Configurations;

public static partial class HostConfigurations
{
    private static IList<Assembly> Assemblies;

    static HostConfigurations()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAutoMapper(Assemblies);

        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserCredentialsService, UserCredentialsService>();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<IdentityDbContext>((provider, option) =>
            {
                option.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDbContextConnection"));

                option.AddInterceptors(provider.GetRequiredService<AuditableInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<SoftDeletedInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<CreationAuditableInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<ModificationAuditableEntity>())
                    .AddInterceptors(provider.GetRequiredService<DeletionAuditableEntity>());
            });
        builder.Services
            .AddDbContext<NotificationDbContext>((provider, option) =>
            {
                option.UseNpgsql(builder.Configuration.GetConnectionString("NotificationDbContextConnection"));

                option.AddInterceptors(provider.GetRequiredService<AuditableInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<SoftDeletedInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<CreationAuditableInterceptor>())
                    .AddInterceptors(provider.GetRequiredService<ModificationAuditableEntity>())
                    .AddInterceptors(provider.GetRequiredService<DeletionAuditableEntity>());
            });

        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

        builder.Services
            .AddScoped<AuditableInterceptor>()
            .AddScoped<SoftDeletedInterceptor>()
            .AddScoped<CreationAuditableInterceptor>()
            .AddScoped<ModificationAuditableEntity>()
            .AddScoped<DeletionAuditableEntity>();

        return builder;
    }

    private static WebApplicationBuilder AddCashing(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CasheSettings>(builder.Configuration.GetSection(nameof(CasheSettings)));

        builder.Services.AddLazyCache();
        builder.Services.AddSingleton<ICasheBroker, LazyMemoryCasheBroker>();

        // builder.Services
        //     .AddStackExchangeRedisCache(option =>
        //     {
        //         option.Configuration = builder.Configuration.GetConnectionString("RedisConnectionString");
        //         option.InstanceName = "BootcampRes";
        //     });

        builder.Services.AddSingleton<ICasheBroker, LazyMemoryCasheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(route => route.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}