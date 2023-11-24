using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Runtime.Intrinsics.Arm;
using System.ComponentModel;
using System.Linq;
using Lab6_Starter;

namespace Lab6_Starter.Model;


public class BusinessLogic : IBusinessLogic, INotifyPropertyChanged
{
    const int BRONZE_LEVEL = 42;
    const int SILVER_LEVEL = 84;
    const int GOLD_LEVEL = 128;


    IDatabase db;
    private readonly int MAX_RATING = 5;
    public ObservableCollection<NearbyAirport> airportsNearby;
    public ObservableCollection<Airport> wiAirports;

    public event PropertyChangedEventHandler PropertyChanged;

    public ObservableCollection<Airport> Airports
    {
        get { return GetAirports(); }

    }

    public ObservableCollection<Resource> Resources
    {
        get { return GetResources(); }

    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ObservableCollection<NearbyAirport> AirportsNearby
    {
        get { return airportsNearby; }
        set
        {
            if (airportsNearby != value)
            {
                airportsNearby = value;
                OnPropertyChanged(nameof(AirportsNearby)); // Notify that AirportsNearby has changed
            }
        }
    }


    public BusinessLogic(IDatabase? db)
    {
        this.db = db;
        wiAirports = db.SelectAllWiAirports();
    }


    public Airport FindAirport(String id)
    {
        return db.SelectAirport(id);
    }

    private AirportAdditionError CheckAirportFields(String id, String city, DateTime dateVisited, int rating)
    {
        if (id.Length < 3 || id.Length > 4)
        {
            return AirportAdditionError.InvalidIdLength;
        }
        if (city.Length < 3)
        {
            return AirportAdditionError.InvalidCityLength;
        }
        if (rating < 1 || rating > MAX_RATING)
        {
            return AirportAdditionError.InvalidRating;
        }

        return AirportAdditionError.NoError;
    }


    public AirportAdditionError AddAirport(String id, String city, DateTime dateVisited, int rating)
    {

        var result = CheckAirportFields(id, city, dateVisited, rating);
        if (result != AirportAdditionError.NoError)
        {
            return result;
        }

        if (db.SelectAirport(id) != null)
        {
            return AirportAdditionError.DuplicateAirportId;
        }
        Airport airport = new Airport(id, city, dateVisited, rating);
        db.InsertAirport(airport);

        return AirportAdditionError.NoError;
    }

    public AirportDeletionError DeleteAirport(String id)
    {

        var entry = db.SelectAirport(id);

        if (entry != null)
        {
            AirportDeletionError success = db.DeleteAirport(entry);
            if (success == AirportDeletionError.NoError)
            {
                return AirportDeletionError.NoError;

            }
            else
            {
                return AirportDeletionError.DBDeletionError;
            }
        }
        else
        {
            return AirportDeletionError.AirportNotFound;
        }
    }


    public AirportEditError EditAirport(String id, String city, DateTime dateVisited, int rating)
    {

        var fieldCheck = CheckAirportFields(id, city, dateVisited, rating);
        if (fieldCheck != AirportAdditionError.NoError)
        {
            return AirportEditError.InvalidFieldError;
        }

        var airport = db.SelectAirport(id);
        airport.Id = id;
        airport.City = city;
        airport.DateVisited = dateVisited;
        airport.Rating = rating;

        AirportEditError success = db.UpdateAirport(airport);
        if (success != AirportEditError.NoError)
        {
            return AirportEditError.DBEditError;
        }

        return AirportEditError.NoError;
    }


    public String CalculateStatistics()
    {
        FlyWisconsinLevel nextLevel;
        int numAirportsUntilNextLevel;

        int numAirportsVisited = db.SelectAllAirports().Count;
        if (numAirportsVisited < BRONZE_LEVEL)
        {
            nextLevel = FlyWisconsinLevel.Bronze;
            numAirportsUntilNextLevel = BRONZE_LEVEL - numAirportsVisited;
        }
        else if (numAirportsVisited < SILVER_LEVEL)
        {
            nextLevel = FlyWisconsinLevel.Silver;
            numAirportsUntilNextLevel = SILVER_LEVEL - numAirportsVisited;
        }
        else if (numAirportsVisited < GOLD_LEVEL)
        {
            nextLevel = FlyWisconsinLevel.Gold;
            numAirportsUntilNextLevel = GOLD_LEVEL - numAirportsVisited;
        }
        else
        {
            nextLevel = FlyWisconsinLevel.None;
            numAirportsUntilNextLevel = 0;
        }

        return String.Format("{0} airport{1} visited; {2} airports remaining until achieving {3}",
              numAirportsVisited, numAirportsVisited != 1 ? "s" : "", numAirportsUntilNextLevel, nextLevel);
    }

    // Converts degrees to radians
    public Double ToRadians(Double x)
    {
        return x * (Math.PI / 180);
    }

    // Calculate the distance (in NM) between two airports given their latitudes and longitudes
    public Double CalculateDistance(Tuple<Double, Double> latLong1, Tuple<Double, Double> latLong2)
    {
        const double RADIUS = 6371; // in km

        // If there are no lat / long values in the table for a given id, return a max value
        if (latLong1 == null || latLong2 == null)
        {
            return Double.MaxValue;
        }

        // Separate values from tuples to make the math easier to read
        double lat1 = latLong1.Item1;
        double lat2 = latLong2.Item1;
        double long1 = latLong1.Item2;
        double long2 = latLong2.Item2;

        // Calculate distance in km (algorithm found on stack overflow)
        double dlat = ToRadians(lat1 - lat2);
        double dlong = ToRadians(long1 - long2);

        double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * (Math.Sin(dlong / 2) * Math.Sin(dlong / 2));
        double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double kmDistance = angle * RADIUS;

        // Convert to nautical miles (NM)
        return kmDistance / 1.852;
    }

    // Calculates the distances less than given distance from given starting airport id to all other airports
    public ObservableCollection<NearbyAirport> CalculateAllAirportDist(String startAirportId, int distance)
    {
        ObservableCollection<NearbyAirport> DistancesFromAirport = new ObservableCollection<NearbyAirport>();

        // check if startAirport is in wiAirports, return null if it isn't
        Airport startAirport = null;
        foreach (Airport airport in wiAirports)
        {
            String airportId = airport.Id.Trim();
            if (airportId.Equals(startAirportId))
            {
                startAirport = airport;
            }
        }
        if (startAirport == null)
        {
            return null;
        }

        // adds airports that are close enough to the starting airport to an ObservableCollection
        foreach (Airport airport in wiAirports)
        {
            if (airport.Id != null)
            {
                String id = airport.Id;
                String city = airport.City;
                Double distanceToAdd = CalculateDistance(new Tuple<Double, Double>(startAirport.Latitude, startAirport.Longitude), new Tuple<Double, Double>(airport.Latitude, airport.Longitude));
                String visited = "X";
                // assumes the airports are unvisited
                if (distanceToAdd <= distance)
                {
                    DistancesFromAirport.Add(new NearbyAirport(id, city, distanceToAdd, visited));
                }
            }
        }

        // checks for visited airports and updates them in the ObservableCollection
        ObservableCollection<NearbyAirport> airportsToReplace = new();
        foreach (Airport airport in Airports)
        {
            foreach (NearbyAirport nearbyAirport in DistancesFromAirport)
            {
                if (airport.Id.Equals(nearbyAirport.Id.Trim()))
                {
                    airportsToReplace.Add(nearbyAirport);
                }
            }
        }
        foreach (NearbyAirport visitedAirport in airportsToReplace)
        {
            DistancesFromAirport.Remove(visitedAirport);
            DistancesFromAirport.Add(new NearbyAirport(visitedAirport.Id, visitedAirport.City, visitedAirport.Distance, "✓"));
        }

        var sortedAirports = DistancesFromAirport.OrderBy(a => a.Distance).ToList();

        AirportsNearby = new ObservableCollection<NearbyAirport>(sortedAirports);
        return AirportsNearby;
    }

    public ObservableCollection<Airport> GetAirports()
    {
        return db.SelectAllAirports();
    }

    public ObservableCollection<Resource> GetResources()
    {
        return db.SelectAllResources();
    }
}

