using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab6_Starter.Model;
public interface IBusinessLogic
{
    AirportAdditionError AddAirport(String id, String city, DateTime dateVisited, int rating);
    AirportDeletionError DeleteAirport(String id);
    AirportEditError EditAirport(String id, String city, DateTime dateVisited, int rating);
    Airport FindAirport(String id);
    String CalculateStatistics();
    Double ToRadians(Double x);
    Double CalculateDistance(Tuple<Double, Double> latLong1, Tuple<Double, Double> latLong2);
    
    ObservableCollection<NearbyAirport> CalculateAllAirportDist(String startAirportId, int distance);
    ObservableCollection<Airport> GetAirports();
    ObservableCollection<Resource> GetResources();
}
