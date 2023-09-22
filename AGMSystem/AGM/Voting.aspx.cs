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
    public partial class Voting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getAgms();
            }

        }

        private void getAgms()
        {

            try
            {
                AGMS agm = new AGMS("cn", 1);
                DataSet ds = agm.GetAGMS();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select AGM", "0");
                    txtAgms.DataSource = ds;
                    txtAgms.DataValueField = "ID";
                    txtAgms.DataTextField = "Name";
                    txtAgms.DataBind();
                    txtAgms.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No AGMS found", "0");
                    txtAgms.Items.Clear();
                    txtAgms.DataSource = null;
                    txtAgms.DataBind();
                    txtAgms.Items.Insert(0, li);
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

        private void getResolutions()
        {
            try
            {
                
                ResolutionSave rs = new ResolutionSave("cn", 1);
                DataSet data = rs.GetAllREsolutions(int.Parse(txtAgms.SelectedValue));

                if (data!=null)
                {
                    grdResolutions.DataSource = data;
                    grdResolutions.DataBind();
                }
                else
                {
                    grdResolutions.DataSource = null;
                    grdResolutions.DataBind();
                    msgbox("No Resolutions");
                }
            }
            catch (Exception xc)
            {

                msgbox(xc.Message);
            }
        }

        protected void txtAgms_TextChanged(object sender, EventArgs e)
        {

            getResolutions();
            grdResolutions.Visible = true;
        }

        protected void grdResolutions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdResolutions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "yes")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                VotingSave vs = new VotingSave("cn", 1);
                vs.AGMID = int.Parse(txtAgms.SelectedValue);
                vs.ResolutionID = index;
                vs.VotedBy = int.Parse(Session["ID"].ToString());
                vs.Vote = true;
                if (vs.Save())
                {
                    msgbox("Vote Added");
                }
                else
                {
                    msgbox("Failed to cast vote");
                }
            }
            if (e.CommandName == "no")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                VotingSave vs = new VotingSave("cn", 1);
                vs.AGMID = int.Parse(txtAgms.SelectedValue);
                vs.ResolutionID = index;
                vs.VotedBy = int.Parse(Session["ID"].ToString());
                vs.Vote = false;
                if (vs.Save())
                {
                    msgbox("Vote Added");
                }
                else
                {
                    msgbox("Failed to cast vote");
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            msgbox("Voting Data Saved");
        }
    }
}