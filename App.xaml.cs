namespace Grievance;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new DashboardPage();
	}
}
