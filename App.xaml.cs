namespace Grievance;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        // MainPage = new ReportIssue();
        var NavPage = new NavigationPage(new ReportIssue());
        MainPage = NavPage;
    }
}
