using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Events
{
    public partial class AddPresenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack=true;
            if (!IsPostBack)
            {
                getEvents();
            }
        }

        private void getEvents()
        {

            try
            {

                AGMEvents agm = new AGMEvents("cn", 1);
                DataSet ds = agm.getAllEvents();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Event", "0");
                    txtEvents.DataSource = ds;
                    txtEvents.DataValueField = "ID";
                    txtEvents.DataTextField = "EventName";
                    txtEvents.DataBind();
                    txtEvents.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No AGMS found", "0");
                    txtEvents.Items.Clear();
                    txtEvents.DataSource = null;
                    txtEvents.DataBind();
                    txtEvents.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                msgbox(a.Message);
            }
        }
        private void GetPresenters()
        {
            PresenterSave sv = new PresenterSave("cn",1);
            DataSet ds = sv.GetPresenter(int.Parse(txtEvents.SelectedValue));
            if (ds!=null)
            {
                grdPresenters.DataSource = ds;
                grdPresenters.DataBind();
            }
            else
            {
                grdPresenters.DataSource=null;
                grdPresenters.DataBind();
                
            }
        }
        #region alerts

        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }
        protected void RedAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Error!', '" + MsgFlg + "', 'error');", true);

        }

        protected void AmberAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Warning!', '" + MsgFlg + "', 'warning');", true);

        }

        protected void SuccessAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + MsgFlg + "', 'success');", true);

        }
        #endregion


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AGMQueries search = new AGMQueries("cn", 1);
                if (search.getEnquiriesBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text) != null)
                {

                    grdMemberEnquiries.DataSource = search.getEnquiriesBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text);
                    grdMemberEnquiries.DataBind();
                    pnlEnquiries.Visible = true;
                }
                else
                {
                    grdMemberEnquiries.DataSource = null;
                    grdMemberEnquiries.DataBind();
                    AmberAlert("Nothing found for these parameters");
                }

            }
            catch (Exception exx)
            {

                RedAlert(exx.Message);
            }
        }

        protected void grdMemberEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int memberID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName== "select")
            {
                RegistrationSave reg = new RegistrationSave("cn",1);
                DataSet ds = reg.CheckPresenter(int.Parse(txtEvents.SelectedValue),memberID);
                if (ds!=null)
                {
                    AmberAlert("Presenter already added for this event");
                }
                else
                {
                    PresenterSave pre = new PresenterSave("cn", 1);
                    if (reg.Retrieve(memberID))
                    {
                        pre.FullName = reg.FirstName + " " + reg.LastName;
                        pre.Company = reg.PensionFund;

                    }
                }
            }
        }

        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {
            GetPresenters();
        }
    }
}