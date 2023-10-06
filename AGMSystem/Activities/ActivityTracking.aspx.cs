using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Activities
{
    public partial class Activity_tracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
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

                RedAlert(a.Message);
            }
        }
        private void getActivities()
        {
            try
            {
                ActivitiesSave ac = new ActivitiesSave("cn", 1);
                DataSet ds = ac.getSavedActivities(int.Parse(txtEvents.SelectedItem.Value));

                if (ds != null)
                {
                    grdActivities.DataSource = ds;
                    grdActivities.DataBind();
                    pnlActivities.Visible = true;
                }
                else
                {
                    grdActivities.DataSource = null;
                    grdActivities.DataBind();
                    pnlActivities.Visible = false;
                    AmberAlert("No Activities Found");

                }

            }
            catch (Exception s)
            {

                RedAlert(s.Message);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Activitytracking reg = new Activitytracking("cn", 1);
            DataSet ds = reg.CheckMemberInActivity(txtBarcode.Text, int.Parse(txtActivityID.Value),int.Parse(txtEvents.SelectedItem.Value));
            if (ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                AmberAlert(txtBarcode.Text+" Already Added");
            }
            else
            {
                reg.ID = 0;
                reg.ActivityID = int.Parse(txtActivityID.Value);
                reg.EventID = int.Parse(txtEvents.SelectedItem.Value);
                reg.MemberCode = txtBarcode.Text;
                if (reg.Save())
                {
                    SuccessAlert(txtBarcode.Text+" Added Successfully");
                    GetActivityTracking();

                }
            }
        }

        private void GetActivityTracking()
        {
            Activitytracking ac = new Activitytracking("cn",1);
            DataSet ds = ac.getSavedActivityTracking(int.Parse(txtEvents.SelectedItem.Value), int.Parse(txtActivityID.Value));
            if (ds!=null)
            {
                grdActivityTracking.DataSource = ds;
                grdActivityTracking.DataBind();
            }
            else
            {
                grdActivityTracking.DataSource= null;
                grdActivityTracking.DataBind();
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

        protected void txtActivities_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void grdActivities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdActivities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int activityID = int.Parse(e.CommandArgument.ToString());
            txtActivityID.Value = activityID.ToString();
            ActivitiesSave act = new ActivitiesSave("cn", 1);
            DataSet ds = act.getActivityDetails(activityID);
            if (ds!=null)
            {
                DataRow item = ds.Tables[0].Rows[0];
                txtActivity.Text = item["Name"].ToString();
                txtDate.Text = item["StartDate"].ToString();
                pnltracking.Visible = true;
                pnlActivities.Visible = false;

            }
            GetActivityTracking();

        }

        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {
            getActivities();
        }

        protected void grdActivityTracking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdActivityTracking_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}