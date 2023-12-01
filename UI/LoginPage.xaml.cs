namespace Lab6_Starter;

using Lab6_Starter.Services;
using Lab6_Starter.ViewModels;

/// <summary>
/// This is the code-behind for the LoginPage.xaml
/// </summary>
public partial class LoginPage : ContentPage
{

	public LoginPage()
	{
		InitializeComponent();
		
		LoginViewModel viewModel = new LoginViewModel();
		
		// Here we are adding a listener to the LoginViewModel's RequestNavigation event
		// so when we invoke RequestNavigation there (after a successful login), it will trigger this code here :-) 
		viewModel.RequestNavigation += () => Navigation.PushAsync(new MainTabbedPage());
		BindingContext = viewModel; // now the LoginPage can bind to the LoginViewModel's properties
	}

	// Don't need this anymore because we are using the LoginViewModel

	// private void OnLoginClicked(object sender, EventArgs e)
	// {
	// 	// Implement your authentication logic here
	// 	string username = UsernameENT.Text;
	// 	string password = PasswordENT.Text;

	// 	// Insert database / authentication logic here

	// 	Navigation.PushAsync(new MainTabbedPage());
	// }
}