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
    public partial class RsvpList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getEvents();

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

        private void getSavedRSVPList()
        {
            
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetRsvps(int.Parse(txtEvents.SelectedValue));
                if (c != null)
                {
                    grdPaymentConfirmation.DataSource = c;
                    grdPaymentConfirmation.DataBind();
                }
                else
                {
                    grdPaymentConfirmation.DataSource = null;
                    grdPaymentConfirmation.DataBind();
                    msgbox("No One RSVP'd");
                }
            }
            catch (Exception c)
            {

                msgbox(c.Message);
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

        protected void grdPaymentConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void grdPaymentConfirmation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectRecord")
            {
                try
                {

                    int index = int.Parse(e.CommandArgument.ToString());
                    MemberRsvpSave rs = new MemberRsvpSave("cn", 1);
                    rs.updateRsvp(index, true);
                    msgbox("Member Payment Status Updated");
                    getSavedRSVPList();
                }
                catch (Exception xc)
                {

                    msgbox(xc.Message);
                }
            }
            
        }

        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {

            getSavedRSVPList();
            grdPaymentConfirmation.Visible = true;
        }
    }
}