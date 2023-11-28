using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Net;

namespace Lab6_Starter;

public partial class RewardsPage : ContentPage
{

    public RewardsPage()
    {
        var entry = new Entry();

        var validStyle = new Style(typeof(Entry));
        validStyle.Setters.Add(new Setter
        {
            Property = Entry.TextColorProperty,
            Value = Colors.Green
        });

        var invalidStyle = new Style(typeof(Entry));
        invalidStyle.Setters.Add(new Setter
        {
            Property = Entry.TextColorProperty,
            Value = Colors.Red
        });

        var emailValidationBehavior = new EmailValidationBehavior
        {
            InvalidStyle = invalidStyle,
            ValidStyle = validStyle,
            Flags = ValidationFlags.ValidateOnValueChanged
        };

        entry.Behaviors.Add(emailValidationBehavior);

        Content = entry;
        InitializeComponent();

    }

    public async Task SubmitApplication()
    {
        String name = nameENT.Text;
        String email = emailENT.Text;
        String address = addressENT.Text;
        String city = cityENT.Text;
        String state = stateENT.Text;
        String zip = zipENT.Text;

        //Should only return the status and nothing else
        String status = MauiProgram.BusinessLogic.CalculateStatistics();
        //validate incoming input
        string result = ValidateApplicationInput(name, email, address, city, state, zip, status);
        if (result != "")
        {
            try
            {
                var message = new EmailMessage
                {
                    //Enter a email here
                    To = new List<string> { "mprogers@mac.com" },
                    Subject = name + " has reached " + status + " status.",
                    Body = name + " has reached " + status + " status and is requesting their reward. " +
                           "\nEmail: " + email +
                           "\n" + address +
                           ",\n" + city + ", " + state + zip
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException e)
            {
                Console.WriteLine($"Feature not supported: {e.Message}");
            }
            catch (Exception e)
            {
                    Console.WriteLine($"Error: {e.Message}");
            }
        }
        else
        {
            await DisplayAlert("Alert", result, "Okay");
        }

        
    }

    public async void OnSubmitButtonClicked(object sender, EventArgs e)
    {
        await SubmitApplication();
        // This button should work only if they have enough airports visited for a reward status to be met
    }/// <summary>
    /// Validates all inputs from rewards application form
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="address"></param>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="zip"></param>
    /// <param name="status"></param>
    /// <returns>string result</returns>
    private string ValidateApplicationInput(string name, string email, string address, string city, string state, string zip, string status)
    {
        string result = "";

        //Invalidate missing names
        if (string.IsNullOrEmpty(name))
        {
            result = "Please provide your name";
        }

        //Invalidate missing or incorrect emails
        if (string.IsNullOrEmpty(email))
        {
            result = "Please provide a valid email";
        }
        //Invalidate missing or incorrect addresses
        if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zip))
        {
            result = "Please provide a valid address";
        }
        //Invalidate missing or incorrect status
        if (status != "BRONZE" && status != "SILVER" && status != "GOLD")
        {
            result = "You are not eligible for an award yet";
        }


        return result;
    }
}

