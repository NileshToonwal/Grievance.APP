using Entities.ExtendedModels;
using Entities.Models;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Net;

namespace Grievance;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void btn_register_Clicked(object sender, EventArgs e)
    {
        Registration reg = new Registration();
        reg.device_name = Common.getDeviceName();
        reg.userdetails = new user_details();
        reg.userdetails.fullname = entryName.Text.ToUpper();
        reg.userdetails.pan = entryPAN.Text.ToUpper();
        reg.userdetails.ucc = entryUCC.Text.ToUpper();
        reg.userdetails.mobile = entryMobile.Text;
        reg.userdetails.dob = myDatePicker.Date;
        reg.userdetails.email = entryEmail.Text.ToUpper();
        string apiEndPoint = @"/api/Common/RegisterUserLoginData";

        var res = Common.PostCommonApi<user_details>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint, JsonConvert.SerializeObject(reg));
        if (res != null)
        {
            if (res.showMsg && res.allowStatus == true)
            {
                DisplayAlert("Registration Status", res.msg, "Ok");
            }
            else if (res.showMsg && res.allowStatus == false)
            {
                DisplayAlert("Registration Status", res.msg, "Ok");
                return;
            }
            if (res.allowStatus == true)
            {                
                Navigation.PopToRootAsync();
                Navigation.PushAsync(new LoginPage());

            }
        }

    }
}