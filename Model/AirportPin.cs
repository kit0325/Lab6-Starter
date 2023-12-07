using Npgsql.Internal.TypeHandlers.LTreeHandlers;
using System;
using System.ComponentModel;

namespace Lab6_Starter.Model;

[Serializable()]
public class AirportPin : INotifyPropertyChanged
{
    String id;
    String name;
    int lat;
    int longi;

    public String Id
    {
        get { return id; }
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public String Name
    {
        get { return name; }
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public int Lat
    {
        get { return lat; }
        set
        {
            lat = value;
            OnPropertyChanged(nameof(Lat));
        }
    }

    public int Longi
    {
        get { return longi; }
        set
        {
            longi = value;
            OnPropertyChanged(nameof(Longi));
        }
    }

    public AirportPin(String id, String name, int lat, int longi)
    {
        Id = id;
        Name = name;
        Lat = lat;
        Longi = longi;
    }

    public AirportPin() { }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public override bool Equals(object obj)
    {
        var otherAirportPin = obj as AirportPin;
        return Id == otherAirportPin.Id;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}


