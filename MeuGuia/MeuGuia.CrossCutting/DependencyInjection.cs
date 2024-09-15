using MediatR;
using MeuGuia.Application.Commands.Revenue.Create;
using MeuGuia.Application.Commands.Revenue.Delete;
using MeuGuia.Application.Commands.Revenue.Update;
using MeuGuia.Application.Notification;
using MeuGuia.Domain.Interface;
using MeuGuia.Infra.Repository;

using Microsoft.Extensions.DependencyInjection;

namespace MeuGuia.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        ResolveDependenciesIdentity(services);
        ResolveRepositoryDependencies(services);
        ResolveMediatRDependencies(services);
        ResolveDependenciesHelper(services);
        ResolveDependenciesNotification(services);

        return services;
    }

    private static void ResolveRepositoryDependencies(IServiceCollection services)
    {
        services.AddScoped<IRepositoryRevenue, RepositoryRevenue>();
    }

    private static void ResolveMediatRDependencies(this IServiceCollection services)
    {
            services.AddMediatR(typeof(CreateRevenueCommandRequest));
            services.AddMediatR(typeof(UpdateRevenueCommandRequest));
            services.AddMediatR(typeof(DeleteRevenueCommandRequest));

    }

    private static void ResolveDependenciesHelper(IServiceCollection services)
    {
    }

    private static void ResolveDependenciesNotification(IServiceCollection services)
    {
        services.AddScoped<INotificationError, NotificationError>();
    }

    private static void ResolveDependenciesIdentity(IServiceCollection services)
    {

    }
}
