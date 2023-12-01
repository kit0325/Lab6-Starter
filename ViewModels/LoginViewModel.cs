namespace Lab6_Starter.ViewModels;

using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Lab6_Starter.Services;

// This class is the ViewModel for the LoginPage.xaml
public class LoginViewModel : INotifyPropertyChanged
{
    private readonly UserAuthenticationService _userAuthenticationService;
    public event Action RequestNavigation; // an Action is a delegate that returns void
                                           // it will be used to trigger the event (navigation) in the LoginPage.xaml.cs
    private string _userId;
    private string _password;
    private string _errorMessage;

/// <summary>
/// These properties will be bound to the LoginPage.xaml. They have to be properties, not fields.
/// </summary>
    public string UserId
    {
        get { return _userId; }
        set
        {
            _userId = value;
            OnPropertyChanged(nameof(UserId));
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public string ErrorMessage  // the View will bind to this property
    {
        get { return _errorMessage; }
        set
        {
            _errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    // An ICommand is an interface that allows you to invoke a method from the View
    // This is the command that will be invoked when the user clicks the Login button
    // We will bind the LoginPage.xaml to this command. It is sooooo simple :-)

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        _userAuthenticationService = new UserAuthenticationService(); // better: use dependency injection
        LoginCommand = new Command(OnLogin);
        this.UserId = "bsmith";         // prepopulates the database
        this.Password = "FWAPPA_CS341_2023";
    }

/// <summary>
/// This method will be invoked when the user clicks the Login button (in LoginPage.xaml)
/// </summary>
    private void OnLogin()
    {
        // For some reason, this does not print the values of UserId and Password
        Console.WriteLine($"LoginViewModel.OnLogin() called with {UserId} and {Password}");

        bool validUser = _userAuthenticationService.ValidateUser(UserId, Password);

        if (validUser)
        {
            ErrorMessage = String.Empty; // in data binding, this will clear the error message (in case we had an error message showing previously)
            MauiProgram.BusinessLogic.UserId = UserId;
            RequestNavigation?.Invoke(); // this will trigger the event in the LoginPage.xaml.cs, handling navigation to the MainTabbedPage
        }
        else
        {
            ErrorMessage = "Login Failed: Invalid UserId or password";
        }
    }

    // This is the standard way to implement the INotifyPropertyChanged interface
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

