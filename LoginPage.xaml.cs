using Grievance.Views;

namespace Grievance;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private void OnButtonClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new HomePage()); 
	}






}