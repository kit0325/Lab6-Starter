using CommunityToolkit.Maui.Core.Platform;
using Lab6_Starter.Model;
using System.Reflection;

namespace Lab6_Starter;

public partial class Weather : ContentPage
{
    public Weather()
    {
        InitializeComponent();
        try
        {
            BindingContext = MauiProgram.BusinessLogic;
        }
        catch(TargetInvocationException tie)
        {
            throw; 
        }
    }

    public void OnSearch_Clicked(object sender, EventArgs e)
    {
        _ = entry.HideKeyboardAsync(CancellationToken.None); //close the keyboard after searching
        //Check length of Wisconsin ICAO airport is correct length of 4 characters.
        if (entry.Text.Length == 4)
        {
            //Check if it is a valid Wisconsin ICAO airport (First character must be a 'K')
            if (entry.Text.ToUpper().IndexOf("K") == 0)
            {
                try
                {
                    String metar = Meteorologist.GetMetar(entry.Text);
                    if (metar == "Unexpected JSON structure")
                    {
                        MetarLabel.Text = "";
                        DisplayAlert("Error: Invalid METAR", "Error while reading METAR data", "OK");
                    }
                    else
                    {
                        MetarLabel.Text = metar;
                    }
                } catch (Exception ex)
                {
                    MetarLabel.Text = "";
                    DisplayAlert("Error: Invalid METAR", ex.Message, "OK");
                }
                try
                {
                    String taf = Meteorologist.GetTaf(entry.Text);
                    if (taf == "Unexpected JSON structure")
                    {
                        TafLabel.Text = "";
                        DisplayAlert("Error: Invalid TAF", "Error while reading TAF data", "OK");
                    }
                    else if( taf == "0")
                    {
                        TafLabel.Text = "No TAF available for this airport";
                    }
                    else
                    {
                        TafLabel.Text = taf;
                    }
                } catch (Exception ex)
                {
                    TafLabel.Text = "";
                    DisplayAlert("Error: Invalid TAF", ex.Message, "OK"); 
                }
            } 
            else
            {
                DisplayAlert("Error: Invalid Wisconsin ICAO airport", "Not an airport starting with K character", "OK"); 
            }
        } 
        else
        {
            DisplayAlert("Error: Invalid ICAO airport", "Not a length of 4 characters", "OK"); 
        }
    }

}