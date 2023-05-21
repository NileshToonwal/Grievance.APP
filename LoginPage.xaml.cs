using Entities.Models;
using Grievance.Models;
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

    private async Task OnSignUpTappedAsync(object sender, TappedEventArgs e)
    {

    }

    private void LoginBtn_ClickedAsync(object sender, EventArgs e)
    {
        string apiUrl = CodeValueConstant.apiPortalBaseUrl + @"\api\Common\GetOtpLogin\" + Pan.Text + "\\" + OTP.Text;
        Common common = new Common();
        var res = common.GetCommonApi<ApiCommonResponse<user_login>>(apiUrl);
        if (res != null)
        {
            if (res.showMsg && res.allowStatus == true)
            {
                DisplayAlert("Login Status", res.msg, "Ok");
            }
            else if (res.showMsg && res.allowStatus == false)
            {
                DisplayAlert("Login Status", res.msg, "Ok");
                return;
            }
            if (res.allowStatus == true)
            {
                Navigation.PopToRootAsync();
                Navigation.PushAsync(new DashboardPage());
            }
        }

    }

    private void SendOTPBtn_Clicked(object sender, EventArgs e)
    {

    }
    private void OnSignUpTapped(object sender, EventArgs e)
    {
        // Handle the "Sign Up" click event
        // Implement your logic here
    }
}