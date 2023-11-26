using CommunityToolkit.Maui.Views;
using Lab6_Starter;

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

         if (DateTime.TryParse(DateVisitedENT.Text, out dateVisited) && int.TryParse(RatingENT.Text, out rating))
         {
             // Both parsing operations were successful.
             MauiProgram.BusinessLogic.AddAirport(IdENT.Text, CityENT.Text, dateVisited, rating);
         }

 		Close();
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
