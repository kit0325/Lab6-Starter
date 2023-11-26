using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Lab6_Starter;

public partial class PlanningToolsPage : ContentPage
{
    public PlanningToolsPage()
    {
        InitializeComponent();
    }

    public void OnRoutingButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RoutingStrategies());
    }

    public void OnNearbyAirportsButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NearbyAirports());
    }

    public void OnWeatherButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Weather());
    }

    public void OnRewardsButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RewardsPage());
    }
}
