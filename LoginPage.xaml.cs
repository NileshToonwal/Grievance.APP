using Entities.ExtendedModels;
using Entities.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static Android.Renderscripts.ScriptGroup;

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
        string apiEndPoint = @"\api\Common\GetOtpLogin\" + (Pan.Text ?? "").ToUpper() + "\\" + OTP.Text;
        //Common common = new Common();
        var res = Common.GetCommonApi<user_login>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
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
                Preferences.Clear();
                Preferences.Set("user_login", JsonConvert.SerializeObject(res.contentData));
                string user_login_json = Preferences.Get("user_login", null);
                Navigation.PopToRootAsync();
                Navigation.PushAsync(new DashboardPage());
            }
        }
    }

    private void SendOTPBtn_Clicked(object sender, EventArgs e)
    {
        string deviceName = DeviceInfo.Name;
        string apiEndPoint = @"/api/Common/GenerateNewOtpLogin/" + (Pan.Text??"").ToUpper() + "/" + deviceName;
        //Common common = new Common();
        var res = Common.GetCommonApi<ApiCommonResponse<string>>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
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

    private void Pan_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = (Entry)sender;        
        Regex regex = new Regex("[^a-zA-Z0-9]");
        textBox.Text = regex.Replace(textBox.Text, "");
    }
}