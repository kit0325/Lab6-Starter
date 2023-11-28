namespace Lab6_Starter.Services;

/// <summary>
///  This class will be used to authenticate the user
/// </summary>
public class UserAuthenticationService
{

//TODO: Implement the methods below
/// <summary>
/// 
/// This method will validate the user's credentials against the database
/// </summary>
/// <param name="username">user name</param>
/// <param name="password">password</param>
/// <returns></returns>
    public bool ValidateUser(string username, string password)
    {
        // Implement the logic to check if the user exists in the database
        // Return true if the user is valid, false otherwise

        // Stub method ... replace with your own implementation
        return username == "bsmith" && password == "FWAPPA_CS341_2023"; // stored in users
    }
}
