using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Lab6_Starter.Model;

namespace Lab6_Starter.Model;
[Serializable()]
public class Route : INotifyPropertyChanged
{
    public ObservableCollection<Airport> Airports { get; set; }
    public string RouteNumber { get; set; }
    public int TotalAirports { get; set; }
    public int TotalDistance { 
        get
        {
            return CalculateTotalDistance();
        }
        set { }
    }

    // Constructor to initialize from a collection of Airports
    public Route(ObservableCollection<Airport> airports, int routeNumber)
    {
        Airports = airports;
        RouteNumber = $"Route {routeNumber}";
        TotalAirports = airports.Count;
        TotalDistance = CalculateTotalDistance();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private int CalculateTotalDistance()
    {
        double totalDistance = 0;

        for (int i = 0; i < Airports.Count - 1; i++)
        {
            Airport currentAirport = Airports[i];
            Airport nextAirport = Airports[i + 1];

            double distanceBetweenAirports = currentAirport.GetDistance(nextAirport.Id);
            totalDistance += distanceBetweenAirports;
        }

        return (int)Math.Ceiling(totalDistance);
    }
}