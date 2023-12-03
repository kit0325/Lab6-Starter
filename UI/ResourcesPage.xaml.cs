using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
namespace Lab6_Starter;
public partial class ResourcesPage : ContentPage
{
    public ResourcesPage()
    {


        InitializeComponent();

        // Create a list of URLs
        List<string> urlList = new List<string>
            {
                "https://wisconsindot.gov/Pages/travel/air/pilot-info/flywi.aspx",
                "http://wiama.org/",
                "https://appletonflight.com/",
                "https://www.brennandairport.com/facilities/"

            };

        collectionView.ItemsSource = urlList;

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
