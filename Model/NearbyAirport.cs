using System.ComponentModel;

namespace Lab6_Starter.Model;
public class NearbyAirport : INotifyPropertyChanged
{
    String id;
    String city;
    Double distance;
    String visited;

    public String Id
    {
        get { return id; }
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }
    public String City
    {
        get { return city; }
        set
        {
            if (city != value)
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
    }
    public Double Distance
    {
        get { return distance; }
        set
        {
            if (distance != value)
            {
                distance = Math.Round(value, 4);
                OnPropertyChanged(nameof(Distance));
            }
        }
    }
    public String Visited
    {
        get { return visited; }
        set 
        {
            if (visited != value)
            {
                visited = value;
                OnPropertyChanged(nameof(Visited));
            }
        }
    }

    public NearbyAirport(String id, String city, Double distance, String visited) 
    { 
        Id = id;
        City = city;
        Distance = distance;
        Visited = visited;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
