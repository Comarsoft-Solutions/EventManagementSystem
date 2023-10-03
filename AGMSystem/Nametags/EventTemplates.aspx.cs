using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Nametags
{
    public partial class EventTemplates : System.Web.UI.Page
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

                RedAlert(a.Message);
            }
        }

        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {

            EventID.Value = txtEvents.SelectedItem.ToString();
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
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                AGMEvents eve = new AGMEvents("cn", 1);
                eve.updateEvent(txtEventTheme.Text, txtSponsor.Text, int.Parse(EventID.Value));
            }
            catch (Exception c)
            {

                WarningAlert(c.Message);
            }
            


        }
    }
}