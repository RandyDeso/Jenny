using Jenny.Models;

namespace Jenny.Data;

public sealed class TravelData
{
    public IReadOnlyList<TravelLocation> Locations { get; } =
    [
        new(
            "London",
            "United Kingdom",
            "A strong base for rail trips, river walks, museums, and easy ferry connections onward to Ireland.",
            "Mild weather with changeable skies—pack a light waterproof layer.",
            ["Ride the Thames Clippers to Greenwich", "See the British Museum", "Walk South Bank to Borough Market"],
            ["Padella for fresh pasta", "Dishoom Covent Garden for relaxed all-day dining", "Mallow Borough Market for plant-based comfort food"],
            ["The Clermont Charing Cross", "The Hoxton Southwark", "Point A King's Cross"],
            ["Book Eurostar and intercity rail a few weeks ahead for the best fares.", "For Ireland-bound journeys, combine rail to Holyhead with the ferry crossing."]),
        new(
            "Paris",
            "France",
            "Ideal for station-to-station city breaks, riverside strolls, and easy onward high-speed rail.",
            "Generally mild, with warm afternoons and cooler evenings in shoulder season.",
            ["Take a Seine river cruise at sunset", "Browse Le Marais and Place des Vosges", "Visit the Musée d'Orsay"],
            ["Breizh Café for crêpes", "Bouillon République for classic Paris dining", "Septime La Cave for small plates and wine"],
            ["OKKO Hotels Paris Gare de l'Est", "citizenM Paris Gare de Lyon", "Hôtel Fabric"],
            ["Stay near Gare du Nord or Gare de Lyon if you expect early train departures.", "Many museums open late one evening each week—great for flexible itineraries."]),
        new(
            "Amsterdam",
            "Netherlands",
            "Compact canals, easy tram connections, and fast rail links make this a great no-flight city break.",
            "Expect breezes and occasional showers; layers and a small umbrella help.",
            ["Cruise the canals", "Visit the Rijksmuseum", "Explore Jordaan by foot"],
            ["Foodhallen for variety", "Breda for modern Dutch dining", "Cafe de Klepel for cheese and wine"],
            ["Motel One Amsterdam-Waterlooplein", "Volkshotel", "Hotel Jakarta Amsterdam"],
            ["Reserve timed museum tickets early, especially for peak weekends.", "Use the ferry behind Centraal Station for free short crossings across the IJ."]),
        new(
            "Dublin",
            "Ireland",
            "A ferry-friendly capital with walkable neighborhoods, live music, and good onward rail options.",
            "Often cool and breezy with scattered showers—waterproof shoes are useful.",
            ["Tour Trinity College", "Walk St Stephen's Green", "Catch live music in Temple Bar or Smithfield"],
            ["The Winding Stair", "Brother Hubbard North", "Fish Shop Benburb"],
            ["The Alex", "Wren Urban Nest", "Maldron Hotel Pearse Street"],
            ["If arriving by ferry, leave extra buffer time between port transfers and city-center plans.", "DART trains are handy for coastal side trips like Howth."]),
        new(
            "Edinburgh",
            "United Kingdom",
            "A scenic rail destination with historic streets, hilltop views, and easy onward connections through the UK.",
            "Cooler temperatures are common; a windproof layer helps year-round.",
            ["Walk the Royal Mile", "Climb Arthur's Seat", "Visit the Scottish National Gallery"],
            ["The Little Chartroom", "Dishoom Edinburgh", "Makars Mash Bar"],
            ["Market Street Hotel", "Motel One Edinburgh-Princes", "The Resident Edinburgh"],
            ["Choose accommodations near Waverley for easier luggage handling.", "Leave time for uphill walking—Edinburgh rewards comfortable shoes."])
    ];

    public IReadOnlyList<TravelRoute> Routes { get; } =
    [
        new(
            "London",
            "Paris",
            "Fastest option: direct train via Eurostar.",
            "2h 20m",
            "from £120",
            ["Board Eurostar from St Pancras International", "Arrive directly into Paris Gare du Nord"]),
        new(
            "Paris",
            "Amsterdam",
            "High-speed rail with a simple city-center to city-center journey.",
            "3h 25m",
            "from €75",
            ["Take the direct high-speed train from Paris Gare du Nord", "Arrive at Amsterdam Centraal"]),
        new(
            "London",
            "Amsterdam",
            "Direct rail is the easiest no-flight option.",
            "4h 10m",
            "from £95",
            ["Board the direct Eurostar from St Pancras", "Arrive at Amsterdam Centraal"]),
        new(
            "London",
            "Dublin",
            "Train-and-ferry option that keeps the whole journey plane-free.",
            "7h 15m",
            "from £68",
            ["Take the train from London Euston to Holyhead", "Transfer to the ferry from Holyhead to Dublin Port", "Continue into central Dublin by bus or taxi"]),
        new(
            "Edinburgh",
            "London",
            "Frequent intercity rail makes this a simple backbone journey.",
            "4h 30m",
            "from £55",
            ["Take the direct train from Edinburgh Waverley", "Arrive at London King's Cross"]),
        new(
            "Amsterdam",
            "London",
            "Direct rail back to the UK with no airport transfers needed.",
            "4h 10m",
            "from €95",
            ["Board the direct Eurostar from Amsterdam Centraal", "Arrive at London St Pancras"]),
        new(
            "Dublin",
            "London",
            "Rail-and-ferry return route via Holyhead.",
            "7h 15m",
            "from £68",
            ["Travel from central Dublin to Dublin Port", "Sail from Dublin Port to Holyhead", "Take the train from Holyhead to London Euston"])
    ];
}
