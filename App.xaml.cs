namespace Grievance;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        // MainPage = new LoginPage();
        var NavPage = new NavigationPage(new LoginPage());
        MainPage = NavPage;
    }
}
