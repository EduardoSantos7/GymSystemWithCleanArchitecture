using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        return services;
    }
}