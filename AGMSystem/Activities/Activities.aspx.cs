using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Events
{
    public partial class Activities : System.Web.UI.Page
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


        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {
            getActivities();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ActivitiesSave ac = new ActivitiesSave("cn", 1);
                ac.ID = 0;
                ac.EventID = int.Parse(txtEvents.SelectedItem.Value);
                ac.Name = txtName.Text;
                ac.StartDate = txtStartDate.Text;
                if (ac.Save())
                {
                    SuccessAlert(ac.Name+" Saved SuccessFully");
                    getActivities();
                }

            }
            catch (Exception ex)
            {
                RedAlert (ex.Message);
                throw;
            }

        }

        private void getActivities()
        {
            try
            {
                ActivitiesSave ac = new ActivitiesSave("cn", 1);
                DataSet ds = ac.getSavedActivities(int.Parse(txtEvents.SelectedItem.Value));

                if (ds!=null)
                {
                    grdActivities.DataSource = ds;
                    grdActivities.DataBind();
                }
                else
                {
                    grdActivities.DataSource= null;
                    grdActivities.DataBind();
                    
                }

            }
            catch (Exception s)
            {

                RedAlert(s.Message);
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

        protected void grdActivities_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}