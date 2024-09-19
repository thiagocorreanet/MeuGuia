using MediatR;

using MeuGuia.Application.Commands.Login.Create;
using MeuGuia.Application.Commands.Logout.Create;
using MeuGuia.Application.Commands.Revenue.Create;
using MeuGuia.Application.Commands.Revenue.Delete;
using MeuGuia.Application.Commands.Revenue.Update;
using MeuGuia.Application.Commands.User.Create;
using MeuGuia.Application.Helper;
using MeuGuia.Application.Notification;
using MeuGuia.Domain.Entitie;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Context;
using MeuGuia.Infra.Repository;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MeuGuia.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        ResolveDependenciesIdentity(services);
        ResolveRepositoryDependencies(services);
        ResolveMediatRDependencies(services);
        ResolveDependenciesNotification(services);
        ResolveDependenciesHelper(services);

        return services;
    }

    private static void ResolveRepositoryDependencies(IServiceCollection services)
    {
        services.AddScoped<IRepositoryRevenue, RepositoryRevenue>();
        services.AddScoped<IRepositoryManagementAccount, RepositoryManagementAccount>();
    }

    private static void ResolveMediatRDependencies(this IServiceCollection services)
    {
        #region Revenue

        services.AddMediatR(typeof(CreateRevenueCommandRequest));
        services.AddMediatR(typeof(UpdateRevenueCommandRequest));
        services.AddMediatR(typeof(DeleteRevenueCommandRequest));

        #endregion

        #region Account

        services.AddMediatR(typeof(CreateLoginCommandRequest));
        services.AddMediatR(typeof(CreateLogoutCommandRequest));
        services.AddMediatR(typeof(CreateLogoutCommandRequest));
        services.AddMediatR(typeof(CreateUserCommandRequest));

        #endregion

    }

    private static void ResolveDependenciesHelper(IServiceCollection services)
    {
        services.AddScoped<HelperIdentity>();
    }

    private static void ResolveDependenciesNotification(IServiceCollection services)
    {
        services.AddScoped<INotificationError, NotificationError>();
    }

    private static void ResolveDependenciesIdentity(IServiceCollection services)
    {
        services.AddIdentity<IdentityUserCustom, IdentityRole>()
            .AddEntityFrameworkStores<MeuGuiaContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<UserManager<IdentityUserCustom>>();
        services.AddScoped<SignInManager<IdentityUserCustom>>();
    }
}
