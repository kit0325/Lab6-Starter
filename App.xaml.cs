﻿namespace Lab6_Starter;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage(); // new NavigationPage(new LoginPage ());
	}
}

