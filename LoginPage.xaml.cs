using Entities.ExtendedModels;
using Entities.Models;
using Grievance.Models;
using Grievance.Views;
using Newtonsoft.Json;

namespace Grievance;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    //private void OnButtonClicked(object sender, EventArgs e)
    //{
    //    Navigation.PushAsync(new HomePage());
    //}

   

    private void LoginBtn_ClickedAsync(object sender, EventArgs e)
    {
        //string apiUrl = "http://localhost:5008" + @"\api\Common\GetOtpLogin\" + Pan.Text + "\\" + OTP.Text;
        string apiEndPoint = @"\api\Common\GetOtpLogin\" + Pan.Text + "\\" + OTP.Text;        
        Common common = new Common();
        var res = common.GetCommonApi<user_login>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
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
                Preferences.Set("user_login", JsonConvert.SerializeObject(res.contentData));
                Navigation.PopToRootAsync();
                Navigation.PushAsync(new DashboardPage());
            }
        }
    }

    private void SendOTPBtn_Clicked(object sender, EventArgs e)
    {
        string deviceName = DeviceInfo.Name;
        string apiEndPoint = @"/api/Common/GenerateNewOtpLogin/" + Pan.Text + "/" + deviceName;
        Common common = new Common();
        var res = common.GetCommonApi<ApiCommonResponse<string>>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
        if (res != null)
        {
            if (res.showMsg && res.allowStatus == true)
            {
                DisplayAlert("OTP Status", res.msg, "Ok");
            }
            else if (res.showMsg && res.allowStatus == false)
            {
                DisplayAlert("OTP Status", res.msg, "Ok");
                return;
            }            
        }
    }
    private void OnSignUpTapped(object sender, EventArgs e)
    {
        // Handle the "Sign Up" click event
        // Implement your logic here

        Navigation.PopToRootAsync();
        Navigation.PushAsync(new RegistrationPage());
    }
}