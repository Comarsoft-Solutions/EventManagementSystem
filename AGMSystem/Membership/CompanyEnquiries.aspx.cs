using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Membership
{
    public partial class CompanyEnquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack =true;
            if (!IsPostBack)
            {
                GetCompanies();
            }
        }
        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(" + strMessage + ");";
            strScript += "</script>";
            Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }

        private void GetCompanies()
        {
            CompanyRegistration cr = new CompanyRegistration("cn",1);
            DataSet ds = cr.getSavedCompanies();
            if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows!=null)
            {
                grdCompanyEnquiries.DataSource = ds;
                grdCompanyEnquiries.DataBind();
            }
            else
            {
                grdCompanyEnquiries.DataSource = null;
                grdCompanyEnquiries.DataBind();
                msgbox("No Companies Found");
            }
        }

        protected void grdMemberEnquiries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void grdCompanyEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}