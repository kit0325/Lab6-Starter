using Npgsql.Internal.TypeHandlers.LTreeHandlers;
using System;
using System.ComponentModel;

namespace Lab6_Starter.Model;

[Serializable()]
public class AirportPin : INotifyPropertyChanged
{
    String id;
    String name;
    Location location;

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

    public Location Loc
    {
        get { return location; }
        set
        {
            location = value;
            OnPropertyChanged(nameof(Loc));
        }
    }

    public AirportPin(String id, String name, Location location)
    {
        Id = id;
        Name = name;
        Loc = location;
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


