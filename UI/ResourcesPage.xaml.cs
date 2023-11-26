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
}
