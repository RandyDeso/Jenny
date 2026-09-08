using Jenny.Core.Abstractions;
using Jenny.Core.Services;
using Jenny.Core.Services.Interfaces;
using Jenny.Data.Infrastructure;
using Jenny.Data.Repositories;
using Jenny.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace Jenny.Data.Extensions;

/// <summary>
/// Registers Jenny data and service dependencies.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Jenny backend services and seeded repositories.
    /// </summary>
    public static IServiceCollection AddJennyBackend(this IServiceCollection services)
    {
        services.AddSingleton(_ => MockTravelDataStore.CreateSeeded());
        services.AddSingleton<KeyedStateLock>();
        services.AddSingleton<IUserStateLock, UserStateLock>();
        services.AddSingleton<IConversationStateLock, ConversationStateLock>();

        services.AddSingleton<ILocationRepository, LocationRepository>();
        services.AddSingleton<IActivityRepository, ActivityRepository>();
        services.AddSingleton<IRouteRepository, RouteRepository>();
        services.AddSingleton<IRestaurantRepository, RestaurantRepository>();
        services.AddSingleton<ITravelTipRepository, TravelTipRepository>();
        services.AddSingleton<IConversationRepository, ConversationRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();

        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<ITransportationService, TransportationService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChatService, ChatService>();

        return services;
    }
}
