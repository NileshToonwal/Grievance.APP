using Entities.Models;
using Newtonsoft.Json;

namespace Grievance;

public partial class DashboardPage : FlyoutPage
{
    user_login user_Login { get; set; }
    public DashboardPage()
    {
        InitializeComponent();
        user_Login = JsonConvert.DeserializeObject<user_login>(Preferences.Get("user_login", null));
        userRole.Text = "Logged by " + user_Login.roletype;
    }
    
    private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedCell = e.SelectedItem as ImageCell;
        if (selectedCell != null)
        {
            switch (selectedCell.Text)
            {
                case "Dashboard":
                    // Handle Dashboard click event
                    HandleDashboardClick();
                    break;
                case "View Issue":
                    // Handle View Issue click event
                    HandleViewIssueClick();
                    break;
                case "Report Issue":
                    // Handle Report Issue click event
                    HandleReportIssueClick();
                    break;
            }

            // Reset the selected item to null
            //((TableView)sender).se = null;
        }
    }

    private void HandleDashboardClick()
    {
        MainFlyoutPage.Detail = new NavigationPage(new ClinetDashBorad());
        // Handle Dashboard click event
        // Implement your logic here
    }

    private void HandleViewIssueClick()
    {
        
        // Handle View Issue click event
        // Implement your logic here
    }

    private void HandleReportIssueClick()
    {
        MainFlyoutPage.Detail = new NavigationPage(new ReportIssue());
        // Handle Report Issue click event
        // Implement your logic here
    }

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        Preferences.Clear();        
        Navigation.PopToRootAsync();
        Navigation.PushAsync(new LoginPage());
    }
}