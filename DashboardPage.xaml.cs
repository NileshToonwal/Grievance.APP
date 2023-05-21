using Newtonsoft.Json;

namespace Grievance;

public partial class DashboardPage : FlyoutPage
{
    public DashboardPage()
    {
        InitializeComponent();
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
        // Handle Report Issue click event
        // Implement your logic here
    }

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        if (Preferences.ContainsKey("user_login")) {
            Preferences.Clear();
        }
        , JsonConvert.SerializeObject(res.contentData) Preferences.Get("user_login");
        Navigation.PopToRootAsync();
        Navigation.PushAsync(new login());
    }
}