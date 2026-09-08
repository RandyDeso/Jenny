using Jenny.Core.Models;

namespace Jenny.Data.Seed;

/// <summary>
/// Holds seeded mock travel data for the MVP.
/// </summary>
public sealed class MockTravelDataStore
{
    /// <summary>
    /// Gets the locations.
    /// </summary>
    public IList<Location> Locations { get; } = new List<Location>();

    /// <summary>
    /// Gets the activities.
    /// </summary>
    public IList<Activity> Activities { get; } = new List<Activity>();

    /// <summary>
    /// Gets the routes.
    /// </summary>
    public IList<Route> Routes { get; } = new List<Route>();

    /// <summary>
    /// Gets the restaurants.
    /// </summary>
    public IList<Restaurant> Restaurants { get; } = new List<Restaurant>();

    /// <summary>
    /// Gets the travel tips.
    /// </summary>
    public IDictionary<Guid, IReadOnlyCollection<string>> TravelTips { get; } = new Dictionary<Guid, IReadOnlyCollection<string>>();

    /// <summary>
    /// Gets the conversation store.
    /// </summary>
    public IDictionary<Guid, Conversation> Conversations { get; } = new Dictionary<Guid, Conversation>();

    /// <summary>
    /// Gets the user store.
    /// </summary>
    public IDictionary<Guid, User> Users { get; } = new Dictionary<Guid, User>();

    /// <summary>
    /// Creates a seeded store.
    /// </summary>
    public static MockTravelDataStore CreateSeeded()
    {
        var store = new MockTravelDataStore();

        var hongKong = NewLocation("Hong Kong", "China", "Pearl River Delta", "Major train hub with fast rail and Victoria Harbour ferry links.", 22.3193, 114.1694);
        var macau = NewLocation("Macau", "China", "Pearl River Delta", "Historic ferry-connected destination known for heritage sites and food.", 22.1987, 113.5439);
        var bangkok = NewLocation("Bangkok", "Thailand", "Central Thailand", "Regional rail hub with markets, temples, and river access.", 13.7563, 100.5018);
        var singapore = NewLocation("Singapore", "Singapore", "Singapore", "Efficient transit city with ferry links and culinary highlights.", 1.3521, 103.8198);
        var ayutthaya = NewLocation("Ayutthaya", "Thailand", "Central Thailand", "Historic rail day-trip city north of Bangkok.", 14.3532, 100.5689);
        var phuket = NewLocation("Phuket", "Thailand", "Southern Thailand", "Island gateway with ferry terminals and beaches.", 7.8804, 98.3923);

        store.Locations.Add(hongKong);
        store.Locations.Add(macau);
        store.Locations.Add(bangkok);
        store.Locations.Add(singapore);
        store.Locations.Add(ayutthaya);
        store.Locations.Add(phuket);

        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Victoria Peak Tram", Description = "Ride to panoramic harbour views.", Category = ActivityCategory.Sightseeing, Location = hongKong, Duration = TimeSpan.FromHours(2), Cost = 14, Rating = 4.7 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Star Ferry Promenade", Description = "Classic harbour crossing and waterfront walk.", Category = ActivityCategory.Culture, Location = hongKong, Duration = TimeSpan.FromHours(1.5), Cost = 5, Rating = 4.8 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Senado Square Walk", Description = "Explore UNESCO heritage streets and pastel plazas.", Category = ActivityCategory.Sightseeing, Location = macau, Duration = TimeSpan.FromHours(2), Cost = 0, Rating = 4.5 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Ruins of St. Paul's", Description = "Visit Macau's iconic landmark.", Category = ActivityCategory.Culture, Location = macau, Duration = TimeSpan.FromHours(1), Cost = 0, Rating = 4.4 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Chatuchak Market", Description = "Browse one of Asia's largest weekend markets.", Category = ActivityCategory.Food, Location = bangkok, Duration = TimeSpan.FromHours(3), Cost = 20, Rating = 4.6 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Chao Phraya River Cruise", Description = "See Bangkok from the water.", Category = ActivityCategory.Sightseeing, Location = bangkok, Duration = TimeSpan.FromHours(2), Cost = 18, Rating = 4.5 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Gardens by the Bay", Description = "Experience the Supertree Grove and conservatories.", Category = ActivityCategory.Nature, Location = singapore, Duration = TimeSpan.FromHours(3), Cost = 24, Rating = 4.8 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Maxwell Food Centre", Description = "Sample signature hawker dishes.", Category = ActivityCategory.Food, Location = singapore, Duration = TimeSpan.FromHours(1.5), Cost = 12, Rating = 4.7 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Ayutthaya Historical Park", Description = "Cycle among temple ruins.", Category = ActivityCategory.Culture, Location = ayutthaya, Duration = TimeSpan.FromHours(4), Cost = 10, Rating = 4.7 });
        store.Activities.Add(new Activity { Id = Guid.NewGuid(), Name = "Phuket Old Town Walk", Description = "Discover Sino-Portuguese streets and cafes.", Category = ActivityCategory.Culture, Location = phuket, Duration = TimeSpan.FromHours(2), Cost = 8, Rating = 4.4 });

        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = hongKong, To = macau, TransportType = TransportType.Ferry, Duration = TimeSpan.FromHours(1), Cost = 24, Stops = new[] { "Hong Kong Macau Ferry Terminal" }, Difficulty = RouteDifficulty.Easy });
        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = bangkok, To = ayutthaya, TransportType = TransportType.Train, Duration = TimeSpan.FromHours(1.5), Cost = 8, Stops = new[] { "Krung Thep Aphiwat Central Terminal" }, Difficulty = RouteDifficulty.Easy });
        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = ayutthaya, To = bangkok, TransportType = TransportType.Train, Duration = TimeSpan.FromHours(1.5), Cost = 8, Stops = new[] { "Ayutthaya Station" }, Difficulty = RouteDifficulty.Easy });
        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = bangkok, To = singapore, TransportType = TransportType.Train, Duration = TimeSpan.FromHours(18), Cost = 85, Stops = new[] { "Padang Besar", "Johor Bahru" }, Difficulty = RouteDifficulty.Complex });
        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = singapore, To = bangkok, TransportType = TransportType.Train, Duration = TimeSpan.FromHours(18), Cost = 85, Stops = new[] { "Johor Bahru", "Padang Besar" }, Difficulty = RouteDifficulty.Complex });
        store.Routes.Add(new Route { Id = Guid.NewGuid(), From = phuket, To = singapore, TransportType = TransportType.Ferry, Duration = TimeSpan.FromHours(12), Cost = 65, Stops = new[] { "Langkawi" }, Difficulty = RouteDifficulty.Complex });

        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Tim Ho Wan Central", Cuisine = "Dim Sum", Location = hongKong, Rating = 4.5, PriceRange = "$", Description = "Affordable dim sum near the harbour." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Yat Lok", Cuisine = "Cantonese Roast", Location = hongKong, Rating = 4.4, PriceRange = "$$", Description = "Famous roast goose stop." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Lord Stow's Bakery", Cuisine = "Bakery", Location = macau, Rating = 4.6, PriceRange = "$", Description = "Classic egg tarts in Coloane." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Raan Jay Fai", Cuisine = "Thai Street Food", Location = bangkok, Rating = 4.5, PriceRange = "$$$", Description = "Known for wok-fried crab omelette." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Thip Samai", Cuisine = "Pad Thai", Location = bangkok, Rating = 4.4, PriceRange = "$$", Description = "Reliable late-night noodle stop." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Tian Tian Hainanese Chicken Rice", Cuisine = "Hawker", Location = singapore, Rating = 4.6, PriceRange = "$", Description = "Popular Maxwell Food Centre stall." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Burnt Ends", Cuisine = "Modern Australian", Location = singapore, Rating = 4.7, PriceRange = "$$$$", Description = "Reserve ahead for open-flame tasting menus." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "Baan Hollanda Cafe", Cuisine = "Thai Cafe", Location = ayutthaya, Rating = 4.2, PriceRange = "$", Description = "Casual stop near the river." });
        store.Restaurants.Add(new Restaurant { Id = Guid.NewGuid(), Name = "One Chun", Cuisine = "Southern Thai", Location = phuket, Rating = 4.5, PriceRange = "$$", Description = "Beloved Phuket Town institution." });

        store.TravelTips[hongKong.Id] = new[] { "Use an Octopus card for quick transfers between MTR and ferries.", "Visit Victoria Peak early to avoid the longest queues." };
        store.TravelTips[macau.Id] = new[] { "Weekday ferries are usually calmer and easier to book.", "Keep small cash handy for Portuguese snacks in the old town." };
        store.TravelTips[bangkok.Id] = new[] { "Pair river boats with BTS trains to skip traffic.", "Carry water and light clothing for temple visits." };
        store.TravelTips[singapore.Id] = new[] { "EZ-Link cards also work well for MRT and buses.", "Reserve popular hawker meals outside peak lunch hours." };
        store.TravelTips[ayutthaya.Id] = new[] { "Morning trains from Bangkok make the best day trips.", "Bring sun protection when cycling between ruins." };
        store.TravelTips[phuket.Id] = new[] { "Confirm ferry check-in times the night before departure.", "Old Town is best explored in the cooler evening hours." };

        return store;
    }

    private static Location NewLocation(string name, string country, string region, string description, double latitude, double longitude) => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        Country = country,
        Region = region,
        Description = description,
        Coordinates = new Coordinates { Latitude = latitude, Longitude = longitude }
    };
}
