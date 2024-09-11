using MeuGuia.Application.Notification;
using MeuGuia.Domain.Interface;

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
    }

    private static void ResolveMediatRDependencies(this IServiceCollection services)
    {
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
