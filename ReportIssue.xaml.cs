using Entities.ExtendedModels;
using Entities.Models;
using Newtonsoft.Json;

namespace Grievance;

public partial class ReportIssue : ContentPage
{
    user_login user_Login { get; set; }
    user_details user_details { get; set; }
    List<category_master> categoryMaster { get; set; }
    public ReportIssue()
    {
        InitializeComponent();
        string user_loginJson = Preferences.Get("user_login", null);
        user_Login = JsonConvert.DeserializeObject<user_login>(user_loginJson);
        if (user_Login == null || user_Login.user_id_ref == 0)
        {
            DisplayAlert("Session Expired", "User details not found!", "ok");
            return;
        }
        string apiEndPoint = $"/api/Common/GetUserDetailesById/{user_Login.user_id_ref}";
        ApiCommonResponse<user_details> res = Common.GetCommonApi<user_details>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
        if (res == null || res.contentData == null)
        {
            DisplayAlert("Session Expired", "User details not found", "ok");
            return;
        }
        else
        {
            Preferences.Set("user_details", JsonConvert.SerializeObject(res.contentData));
            user_details = res.contentData;
        }
        complaintPanNumber.Text = user_details.pan;
        complaintName.Text = user_details.fullname;
        complaintUcc.Text = user_details.ucc;

        apiEndPoint = @"/api/Common/getDropDownValue";
        ApiCommonResponse<List<cfg_codevalue>> cfgCodeValueRes = Common.GetCommonApi<List<cfg_codevalue>>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
        if (cfgCodeValueRes != null && cfgCodeValueRes.contentData != null)
        {
            complaintStatus.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "ISSUE_STATUS" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();
            complaintExchange.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "EXCH" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();
            complaintSegmentType.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "SEG" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();
            complaintMode.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "MODE" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();

            //complaintStatus.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "ISUUE_STATUS" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();
        }
        else
        {
            DisplayAlert("Masters Load Status", "Enable to fetch masters data!", "ok");
            return;
        }

        apiEndPoint = @"/api/Common/catrgoryItem";
        ApiCommonResponse<List<category_master>> categoryMasterRes = Common.GetCommonApi<List<category_master>>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
        if (categoryMasterRes != null && categoryMasterRes.contentData != null)
        {
            List<category_master> categoryMaster = categoryMasterRes.contentData;
            complaintCategory.ItemsSource = categoryMaster.Select(o => o.category).Distinct().ToList();
        }
        else
        {
            DisplayAlert("Masters Load Status", "Enable to fetch category master data!", "ok");
            return;
        }



    }

    private void AddComplaintBtn_Clicked(object sender, EventArgs e)
    {
        issue_detail issue = new issue_detail();
        issue.user_id_ref = user_Login.user_id_ref;
        issue.pan = complaintPanNumber.Text;
        issue.issue_by = complaintName.Text;
        issue.fullname = complaintName.Text;

        issue.summary = complaintIssueSummary.Text;
        issue.category = (string)complaintCategory.SelectedItem;
        issue.subcategory = (string)complaintSubCategory.SelectedItem;
        issue.details = complaintIssueDetails.Text;
        issue.exchange = (string)complaintExchange.SelectedItem;
        issue.segment = (string)complaintSegmentType.SelectedItem;
        issue.mode = (string)complaintMode.SelectedItem;
        issue.ucc = complaintUcc.Text;
        issue.issue_created_dt = DateTime.Now;
        issue.targate_date = DateTime.Now.AddDays(3);
        issue.isactive = true;
        issue.status = (string)complaintStatus.SelectedItem;

        ApiCommonResponse<issue_detail> res = Common.PostCommonApi<issue_detail>(CodeValueConstant.apiPortalBaseUrl, @"/api/Common/RegisterIssue", JsonConvert.SerializeObject(issue));
        if (res.showMsg && string.IsNullOrWhiteSpace(res.msg))
        {
            if (res.allowStatus)
            {
                DisplayAlert("Issue Registration Status", res.msg, "OK");
            }
        }
        if (res.allowStatus)
        {
            Navigation.PopToRootAsync();
            Navigation.PushAsync(new DashboardPage());
        }
        else
        {
            return;
        }


    }

    private void complaintCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mainCategory = (string)complaintCategory.SelectedItem;
        complaintSubCategory.ItemsSource = categoryMaster.Where(x => x.category == mainCategory).Select(o => o.subcategory).Distinct().ToList();
    }
}