using AGMSystem.models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class CreateAGM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtID.Value = "0";
                txtEventID.Value = "0";
                pnlSearch.Visible = false;
            }
        }


        protected void btncreate_Click(object sender, EventArgs e)
        {
            SaveAGM();
            //Response.Redirect("ProjectMembership");
        }
        protected void SaveAGM()
        {
            try
            {
                if (txtName.Text.IsNullOrWhiteSpace() && TxtVenue.Text.IsNullOrWhiteSpace() && txtStartDate.Text.IsNullOrWhiteSpace() && txtEndDate.Text.IsNullOrWhiteSpace() )
                {
                    msgbox("Fill in all fields");
                }
                else
                {
                    if (rbEventSettings.SelectedValue.ToLower() == "true")
                    {

                        AGMEvents eve = new AGMEvents("cn", 1);
                        
                        DataSet xx = eve.getEventsBySearch(txtEventSearch.Text);
                        if (xx != null)
                        {
                            foreach (DataRow item in xx.Tables[0].Rows)
                            {
                                txtEventID.Value = item["ID"].ToString();
                            }
                        }
                    }

                    AGMS agm = new AGMS("cn",1);
                    if (DateTime.TryParse(txtStartDate.Text, out DateTime sd))
                    {
                        agm.StartDate = Convert.ToDateTime(txtStartDate.Text);
                    }
                    else
                    {
                        msgbox("Enter Valid Date");
                    }
                    if (DateTime.TryParse(txtEndDate.Text, out DateTime dte))
                    {

                        agm.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    }
                    else
                    {
                        msgbox("Enter Valid Date");
                    }
                    agm.Name = txtName.Text;
                    agm.Venue = TxtVenue.Text;
                    agm.ID = int.Parse(txtID.Value);
                    agm.EventID = int.Parse(txtEventID.Value);
                    agm.AttendeeSettings = bool.Parse(rbAttendeeSettings.SelectedValue);
                    agm.EventSettings = bool.Parse(rbEventSettings.SelectedValue);
                    if (agm.Save())
                    {
                        msgbox("AGM: " + txtName.Text + "Saved");
                        clearParams();
                    }
                    else
                    {
                        msgbox("There was an error on saving" + txtName.Text + " details: " + agm.Msgflg);
                    }
                }
            }
            catch (Exception cv)
            {

                msgbox(cv.Message);
            }
        }

        private void clearParams()
        {
            txtID.Value="0";
            txtEventID.Value="0";
            txtName.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            rbAttendeeSettings.GetDefaultValues();
            rbEventSettings.GetDefaultValues();
            pnlSearch.Visible = false;
            gridSelectedEvent.Visible = false;
            grdEventSelect.Visible = false;
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

        protected void rbEventSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEventSettings.SelectedValue.ToLower() == "true")
                {
                    pnlSearch.Visible = true;

                }
                else
                {
                    pnlSearch.Visible=false;
                }
            }
            catch (Exception xc )
            {

                msgbox(xc.Message);
            }
        }


        protected void btnSearchEvent_Click(object sender, EventArgs e)
        {
            try
            {
                AGMEvents eve = new AGMEvents("cn", 1);
                DataSet xx = eve.getEventsBySearch(txtEventSearch.Text);
                if (xx != null)
                {
                    grdEventSelect.DataSource = xx;
                    grdEventSelect.DataBind();
                }
                else
                {
                    grdEventSelect.DataSource = null;
                    grdEventSelect.DataBind();
                    msgbox("Event with parameters: " + txtEventSearch.Text + " Not Found");
                }
            }
            catch (Exception ex)
            {

                msgbox(ex.Message);
            }
        }

        protected void grdEventSelect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "selectEvent")
                {
                    string index = (e.CommandArgument.ToString());
                    grdEventSelect.Visible = false;
                    
                    AGMEvents pr = new AGMEvents("cn", 1);
                    if (pr.getEventsBySearch(index) != null)
                    {
                        gridSelectedEvent.DataSource = pr.getEventsBySearch(index);
                        gridSelectedEvent.DataBind();
                        AGMS ag = new AGMS("cn", 1);
                        txtEventID.Value = pr.ID.ToString();
                        ag.EventID=int.Parse(txtEventID.Value);
                        
                    }
                    else
                    {
                        gridSelectedEvent.DataSource = null;
                        gridSelectedEvent.DataBind();
                        msgbox("Error in Adding Event");
                    }


                }
            }
            catch (Exception x)
            {

                msgbox(x.Message);
            }
        }
    }
}