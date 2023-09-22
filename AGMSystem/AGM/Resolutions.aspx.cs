using AGMSystem.models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AGMSystem
{
    public partial class Resolutions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtAGMID.Value = "0";
                getAgms();
            }
        }

        protected void grdResolutions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdResolutions_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveResolution();

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

        private void getAgms()
        {
            
            try
            {
                AGMS agm = new AGMS("cn", 1);
                DataSet ds = agm.GetAGMS();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select AGM","0");
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

        private void SaveResolution()
        {
            try
            {
                try
                {
                    if (txtDetails.Text.IsNullOrWhiteSpace() && txtResolution.Text.IsNullOrWhiteSpace() )
                    {
                        msgbox("Fill in all fields");
                    }
                    else
                    {
                       
                        ResolutionSave rs = new ResolutionSave("cn", 1);
                        rs.AGMID = int.Parse(txtAgms.SelectedValue);
                        rs.Resolution = txtResolution.Text;
                        rs.Details = txtDetails.Text;
                        
                        if (rs.Save())
                        {
                            msgbox("AGM: " + txtResolution.Text + "Saved");
                            getResolutions();
                            clearParams();
                        }
                        else
                        {
                            msgbox("There was an error on saving" + txtResolution.Text + " details: " + rs.Msgflg);
                        }
                    }
                }
                catch (Exception cv)
                {

                    msgbox(cv.Message);
                }
            }
            catch (Exception c)
            {

                msgbox(c.Message);
            }
        }

        private void getResolutions()
        {
            try
            {
                ResolutionSave rs = new ResolutionSave("cn", 1);
                DataSet ds = rs.GetAllREsolutions(int.Parse(txtAgms.SelectedValue));

                if (ds!=null && ds.Tables[0]!=null && ds.Tables[0].Rows!=null)
                {
                    grdResolutions.DataSource = ds;
                    grdResolutions.DataBind();
                }
                else
                {
                    grdResolutions.DataSource=null;
                    grdResolutions.DataBind();
                }
            }
            catch (Exception xx)
            {

                msgbox(xx.Message);
            }
        }

        private void clearParams()
        {
           txtResolution.Text="";
           txtDetails.Text="";
        }

        protected void txtAgms_TextChanged(object sender, EventArgs e)
        {

            getResolutions();
            grdResolutions.Visible = true;
        }
    }
}