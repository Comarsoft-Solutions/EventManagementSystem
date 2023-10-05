using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class CheckIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
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
        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }

        private void getCheckinList()
        {
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetCheckin(int.Parse(txtEvents.SelectedValue));
                if (c != null)
                {
                    grdCheckin.DataSource = c;
                    grdCheckin.DataBind();
                }
                else
                {
                    grdCheckin.DataSource = null;
                    grdCheckin.DataBind();
                    msgbox("Noone In Checkin");
                }
            }
            catch (Exception c)
            {

                msgbox(c.Message);
            }
        }

        #region alerts
        protected void RedAlert(string MsgFlg)
        {
            // lblComms.Text = "An Error occured: " + MsgFlg;
            // pnlComms.BackColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Error!', '" + MsgFlg + "', 'error');", true);


        }

        protected void WarningAlert(string MsgFlg)
        {
            // lblComms.Text = "Warning: " + MsgFlg;
            // pnlComms.BackColor = System.Drawing.Color.Orange;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Warning!', '" + MsgFlg + "', 'warning');", true);


        }

        protected void SuccessAlert(string MsgFlg)
        {
            //lblComms.Text = "Success: " + MsgFlg;
            //pnlComms.BackColor = System.Drawing.Color.Green;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + MsgFlg + "', 'success');", true);

        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MemberRsvpSave reg = new MemberRsvpSave("cn",1);
                DataSet ds = reg.GetCheckin(int.Parse(txtEvents.SelectedItem.Value),txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text);
                if (ds!=null)
                {
                    grdCheckin.DataSource = ds;
                    grdCheckin.DataBind();
                }
                else
                {
                    grdCheckin = null;
                    grdCheckin.DataBind();
                    WarningAlert("Nothing found for those search values");
                }

            }
            catch (Exception x)
            {
                WarningAlert(x.Message);
                throw;
            }
        }

       
        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {

            getCheckinList();
            grdCheckin.Visible = true;
        }

        protected void grdCheckin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectRecord")
            {
                try
                {

                    int index = int.Parse(e.CommandArgument.ToString());
                    MemberRsvpSave rs = new MemberRsvpSave("cn", 1);
                    rs.updateCheckin(index, true);
                    msgbox("Member Checkin Status Updated");
                    getCheckinList();
                }
                catch (Exception xc)
                {

                    msgbox(xc.Message);
                }
            }
        }

        protected void grdCheckin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCheckin.PageIndex = e.NewPageIndex;
            this.BindGrid(e.NewPageIndex);
        }
        private void BindGrid(int page = 0)
        {
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetCheckin(int.Parse(txtEvents.SelectedValue));
                if (c != null)
                {
                    int maxPageIndex = grdCheckin.PageCount - 1;
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
                    grdCheckin.DataSource = c;
                    grdCheckin.PageIndex = page;
                    grdCheckin.DataBind();
                }
                else
                {
                    grdCheckin.DataSource = null;
                    grdCheckin.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }
    }
}