using CommunityToolkit.Maui.Views;
using Lab6_Starter;
using Lab6_Starter.Model;

namespace Lab6_Solution;

/// <summary>
/// Authors: Evan Olson, Olivia Ozbaki, Alex Ceithamer
/// </summary>
public partial class AddNewAirportPopup : Popup
{
	/// <summary>
	/// Initializes the popup component.
	/// </summary>
	public AddNewAirportPopup()
	{
		InitializeComponent();
	}

	/// <summary>
	/// Adds an airport with the specified information to the database.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="args">Arguments</param>
	public void AddAirportButtonClicked(System.Object sender, System.EventArgs args)
	{
		// Group 1's contributions
		DateTime dateVisited;
        int rating;
		string id = IdENT.Text;
		string city = CityENT.Text;
        bool validDate = DateTime.TryParse(DateVisitedENT.Text, out dateVisited);
		bool validRating = int.TryParse(RatingENT.Text, out rating);
        AirportAdditionError result = AirportAdditionError.NoError;

		if (!validDate || !validRating)
		{
			// Parsing operations failed
			result = AirportAdditionError.InvalidDate;
		} else if (rating > 5 || rating < 1)
		{
			result = AirportAdditionError.InvalidRating;
		} else if (city.Length > 200)
		{
			result = AirportAdditionError.InvalidCityLength;
		} else if (id.Length > 200)
		{
			result = AirportAdditionError.InvalidIdLength;
		}
         else {
            //no issues in input
            result = MauiProgram.BusinessLogic.AddAirport(id, city, dateVisited, rating);
        }

        Close(result);
     }
	

	/// <summary>
	/// Closes the "Add New Airport" popup.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="args">Arguments</param>
	public void ClosePopupButtonClicked(System.Object sender, System.EventArgs args)
	{

		Close();
	}
}
