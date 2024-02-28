using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class MemberEnquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack= true;
            if (!IsPostBack)
            {

                GetMemmbers();
            }
        }

        private void GetMemmbers()
        {
            AGMQueries enq = new AGMQueries("cn", 1);
            if (enq.GetEnquiries() != null)
            {
                grdMemberEnquiries.DataSource = enq.GetEnquiries();
                grdMemberEnquiries.DataBind();
            }
            else
            {
                grdMemberEnquiries.DataSource = null;
                grdMemberEnquiries.DataBind();
                msgbox("There are no members");

            }
        }

        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }

        protected void grdMemberEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName== "attachRecord")
            {
                long index = long.Parse(e.CommandArgument.ToString());
                Response.Redirect(string.Format("MembershipCompanyMapping?MemberID="+index, index),false);
            }
        }

        protected void grdMemberEnquiries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMemberEnquiries.PageIndex = e.NewPageIndex;

            this.BindGrid(e.NewPageIndex);
        }
        private void BindGrid(int page = 0)
        {
            try
            {
                AGMQueries enq = new AGMQueries("cn", 1);
                DataSet c = enq.GetEnquiries()  ;
                if (c != null)
                {
                    int maxPageIndex = grdMemberEnquiries.PageCount - 1;
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
                    grdMemberEnquiries.DataSource = c;
                    grdMemberEnquiries.PageIndex = page;
                    grdMemberEnquiries.DataBind();
                }
                else
                {
                    grdMemberEnquiries.DataSource = null;
                    grdMemberEnquiries.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
            AGMQueries search = new AGMQueries("cn", 1);
                if (search.getEnquiriesBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text) != null) 
                {

                    grdMemberEnquiries.DataSource = search.getEnquiriesBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text);
                    grdMemberEnquiries.DataBind();
                }
                else
                {
                    grdMemberEnquiries.DataSource=null;
                    grdMemberEnquiries.DataBind();
                    msgbox("Nothing found for these parameters");
                }

            }
            catch (Exception exx)
            {

                msgbox(exx.Message);
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
    }
}