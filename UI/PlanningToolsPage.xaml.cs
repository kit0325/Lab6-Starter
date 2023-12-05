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

    /// <summary>
    /// This is the event handler for the Logout button. All it does is bring us back to the Login page
    /// </summary>
    /// <param name="sender">button that triggered this</param>
    /// <param name="e">no events worth worrying about</param>
    void Logout_Clicked(System.Object sender, System.EventArgs e)
    {
        //MauiProgram.BusinessLogic.Logout();
        Application.Current.MainPage = new LoginPage();
    }
}
