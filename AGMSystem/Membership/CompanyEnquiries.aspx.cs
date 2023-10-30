using AGMSystem.models;
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
        #region alerts
        protected void RedAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Error!', '" + MsgFlg + "', 'error');", true);

        }

        protected void WarningAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Warning!', '" + MsgFlg + "', 'warning');", true);

        }

        protected void SuccessAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + MsgFlg + "', 'success');", true);

        }
        #endregion

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
                WarningAlert("No Companies Found");
            }
        }

        protected void grdMemberEnquiries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCompanyEnquiries.PageIndex = e.NewPageIndex;
            this.BindGrid(e.NewPageIndex);
        }
        private void BindGrid(int page = 0)
        {
            try
            {
                CompanyRegistration cr = new CompanyRegistration("cn", 1);
                DataSet c = cr.getSavedCompanies();
                if (c != null)
                {
                    int maxPageIndex = grdCompanyEnquiries.PageCount - 1;
                    if (page < 0 || page > maxPageIndex)
                    {
                        if (maxPageIndex >= 0)
                        {
                            // Navigate to the last available page
                            page = maxPageIndex;
                        }
                        else
                        {
                            // No data available, reset to the first page
                            page = 0;
                        }
                    }
                    grdCompanyEnquiries.DataSource = c;
                    grdCompanyEnquiries.PageIndex = page;
                    grdCompanyEnquiries.DataBind();
                }
                else
                {
                    grdCompanyEnquiries.DataSource = null;
                    grdCompanyEnquiries.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void grdCompanyEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int companyID = int.Parse(e.CommandArgument.ToString());

            if (e.CommandName == "deleteRecord")
            {
                try
                {
                    CompanyRegistration delete = new CompanyRegistration("cn", 1);
                    delete.Delete(companyID);
                    GetCompanies();
                }
                catch (Exception x)
                {

                    WarningAlert("Oops  something went wrong");
                }
            }
        }
    }
}