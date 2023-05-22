using Entities.ExtendedModels;
using Entities.Models;
using Newtonsoft.Json;
using System.Net;

namespace Grievance;

public partial class SearchView : ContentPage
{
    user_login user_Login { get; set; }
    user_details user_details { get; set; }
    List<category_master> categoryMaster { get; set; }
    public SearchView()
	{
		InitializeComponent();
         
        string apiEndPoint = @"/api/Common/getDropDownValue";
        ApiCommonResponse<List<cfg_codevalue>> cfgCodeValueRes = Common.GetCommonApi<List<cfg_codevalue>>(CodeValueConstant.apiPortalBaseUrl, apiEndPoint);
        if (cfgCodeValueRes != null && cfgCodeValueRes.contentData != null)
        {
            dashboardFilterStatus.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "ISSUE_STATUS" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();            //complaintStatus.ItemsSource = cfgCodeValueRes.contentData.Where(x => x.codetype == "ISUUE_STATUS" && x.isactive == true && x.isenable == true).Select(o => o.codevaluedescription).ToList();
        }
    }

    

    

    private void dashboardSearchButton_Clicked(object sender, EventArgs e)
    {
        IssueListModel searchPayload = new IssueListModel();

        searchPayload.Status = (string)dashboardFilterStatus.SelectedItem;
        searchPayload.Summary = (string)dashboardFilterSearch.Text;
        searchPayload.IssueId = string.IsNullOrWhiteSpace(dashboardFilterIssueId.Text)?null: Convert.ToInt64(dashboardFilterIssueId.Text);
        searchPayload.UserId = string.IsNullOrWhiteSpace(dashboardFilterUserId.Text) ? null : Convert.ToInt64(dashboardFilterUserId.Text);
        
        ApiCommonResponse<List<issue_detail>> res = Common.PostCommonApi<List<issue_detail>>(CodeValueConstant.apiPortalBaseUrl, @"/api/Common/GetIssueList", JsonConvert.SerializeObject(searchPayload));
        if (res.showMsg && !string.IsNullOrWhiteSpace(res.msg))
        {
            if (!res.allowStatus)
            {
                DisplayAlert("Complaint Status", res.msg, "OK");
            }
        }
        if (res.allowStatus)
        {
            List<IssueListViewModel> issueList=res.contentData.Select(o=>new IssueListViewModel() { 
            IssueCreatedBy=o.issue_by,
            IssueId=o.issue_id,
            Status=o.status,
            Summary=o.summary
            }).ToList();

            issuelistView.ItemsSource = issueList;            
        }
        else
        {
            return;
        }

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("hello","jai shreee ram" ,"OK");
    }
}