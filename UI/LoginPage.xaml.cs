namespace Lab6_Starter;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e)
	{
		// Implement your authentication logic here
		string username = usernameEntry.Text;
		string password = passwordEntry.Text;

		// Insert database / authentication logic here
		
		Navigation.PushAsync(new MainTabbedPage());
	}
}