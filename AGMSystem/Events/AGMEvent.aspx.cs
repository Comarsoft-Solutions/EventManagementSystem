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
    public partial class AGMEventsaspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtID.Value = "0";
                txtTransportID.Value = "0";
                txtAccomodationID.Value = "0";
                txtEPID.Value = "0";
                GetLogisticsType();
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
            // lblComms.Text = "An Error occured: " + MsgFlg;
            // pnlComms.BackColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Error!', '" + MsgFlg + "', 'error');", true);


        }

        protected void AmberAlert(string MsgFlg)
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
        protected void GetLogisticsType()
        {
            try
            {
                Logistics logi = new Logistics("cn", 1);
                if (logi.GetLogisticsType() != null)
                {
                    ListItem pro = new ListItem("Select Logistics Type", "0");
                    txtLogisticsType.DataSource = logi.GetLogisticsType();
                    txtLogisticsType.DataValueField = "ID";
                    txtLogisticsType.DataTextField = "Type";
                    txtLogisticsType.DataBind();
                    txtLogisticsType.Items.Insert(0, pro);
                }
                else
                {
                    ListItem pro = new ListItem("There are no Types available", "0");
                    txtLogisticsType.DataSource = null;
                    txtLogisticsType.DataBind();
                    txtLogisticsType.Items.Insert(0, pro);
                }
            }
            catch (Exception ex)
            {

                RedAlert(ex.Message);
            }
           
        }
        protected void SaveEvent()
        {
            try
            {
                //CheckEventAvailability();
                AGMEvents eve = new AGMEvents("cn", 1);
                if (int.Parse(txtID.Value ) == eve.ID)
                {
                    
                    eve.ID = int.Parse(txtID.Value);
                    eve.EventName = txtEventname.Text;
                    eve.StartDate = txtStartDate.Text;
                    eve.EndDate = txtEndDate.Text;
                    eve.StatusID = true;
                    eve.AttendanceFee = double.Parse(txtAttendanceFee.Text);
                    eve.Venue=TxtVenue.Text;
                    //if (RadioInvitation.Checked)
                    //{
                    //    eve.AttendeeSettings = true;
                    //}
                    //if (RadioOpen.Checked) 
                    //{
                    //    eve.AttendeeSettings= false;
                    //    AmberAlert("Are you sure you want event to be open. Press Ok to Continue");
                    //}
                    if (eve.Save())
                    {
                        SuccessAlert("Event details saved");
                        getSavedEventID();

                    }
                    else
                    {
                        RedAlert("There was an error on saving event details: " + eve.MsgFlg);
                    }

                }
                else
                {
                    if (txtEndDate.Text.IsNullOrWhiteSpace() || txtStartDate.Text.IsNullOrWhiteSpace() || txtEventname.Text.IsNullOrWhiteSpace())
                    {
                        AmberAlert("Fill in all the fields");

                    }
                    else
                    {
                        if (DateTime.TryParse(txtStartDate.Text, out DateTime dt))
                        {

                        }
                        else
                        {
                            AmberAlert("Enter Valid Date");

                        }

                        if (DateTime.TryParse(txtEndDate.Text, out DateTime dte))
                        {
                            if (DateTime.Parse(txtEndDate.Text)< DateTime.Parse(txtStartDate.Text))
                            {
                                AmberAlert("Enter Valid End Date");
                            }
                        }
                        else
                        {
                            AmberAlert("Enter Valid Date");
                        }
                        AGMEvents ag = new AGMEvents("cn", 1);
                        ag.ID = int.Parse(txtID.Value);
                        ag.EventName = txtEventname.Text;
                        ag.StartDate = txtStartDate.Text;
                        ag.EndDate = txtEndDate.Text;
                        ag.StatusID = true;
                        ag.AttendanceFee = double.Parse(txtAttendanceFee.Text);
                        ag.Venue = TxtVenue.Text;
                        //if (RadioInvitation.Checked)
                        //{
                        //    ag.AttendeeSettings = true;
                        //}
                        //else
                        //{
                        //    ag.AttendeeSettings = false;
                        //}
                        if (ag.Save())
                        {
                            txtID.Value = ag.ID.ToString();
                            getSavedEventID();
                            SuccessAlert($"Event {txtEventname.Text} saved sucessfully!");
                        }
                        else
                        {
                            SuccessAlert("Error Occured");
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void getSavedEventID()
        {
            try
            {
                AGMEvents ag = new AGMEvents("cn", 1);
                DataSet xxx = ag.getSavedEventID(txtEventname.Text);
                if (xxx!= null)
                {
                    foreach (DataRow item in xxx.Tables[0].Rows)
                    {
                        txtID.Value = item["ID"].ToString();
                    }
                    
                }
                else
                {
                    AmberAlert("No Saved events");
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void grdViewEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


        protected void btnSearchProject_Click(object sender, EventArgs e)
        {
            if (!(txtEventname.Text.IsNullOrWhiteSpace() && txtStartDate.Text.IsNullOrWhiteSpace()))
            {
                
                try
                {
                    AGMProjects projs = new AGMProjects("cn", 1);
                    if (projs.getProjecctsBySearch(txtProjectListSearch.Text) != null)
                    {

                        gridProjectList.DataSource = projs.getProjecctsBySearch(txtProjectListSearch.Text);
                        gridProjectList.DataBind();
                        gridProjectList.Visible = true;
                    }
                    else
                    {
                        gridProjectList.DataSource = null;
                        gridProjectList.DataBind();
                        AmberAlert("No Project with name"+ txtProjectListSearch.Text);
                    }

                }
                catch (Exception exx)
                {

                    RedAlert(exx.Message);
                }
            }
            else
            {
                AmberAlert("Can not add project without Event");
            }

        }

        protected void grdAddedProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }



        protected void btnAddLogistics_Click(object sender, EventArgs e)
        {
            AddLogistics();
           
        }

        private void AddLogistics()
        {
            if (!(txtEventname.Text.IsNullOrWhiteSpace() && txtStartDate.Text.IsNullOrWhiteSpace()))
            {
                try
                {
                    if (txtLogisticsType.Text == "1")
                    {
                        AGMEvents eve = new AGMEvents("cn", 1);
                        eve.getSavedEventID(txtEventname.Text);
                        DataSet xx = new DataSet();
                        xx = eve.getSavedEventID(txtEventname.Text);
                        foreach (DataRow item in xx.Tables[0].Rows)
                        {
                            txtID.Value = item["ID"].ToString();
                        }
                        Transport ag = new Transport("cn", 1)
                        {
                            ID = int.Parse(txtTransportID.Value),
                            Name = txtServiceProvider.Text,
                            Capacity = int.Parse(txtCapacity.Text),
                            Available = int.Parse(txtCapacity.Text),
                            EventID = int.Parse(txtID.Value),
                        };
                        if (ag.Save())
                        {
                            txtID.Value = ag.ID.ToString();
                            getSavedLogistics();
                            SuccessAlert($"Logistic {txtServiceProvider.Text} added sucessfully!");
                        }
                        else
                        {
                            RedAlert("Error Occured");
                        }

                    }
                    else
                    {
                        AGMEvents eve = new AGMEvents("cn", 1);
                        eve.getSavedEventID(txtEventname.Text);
                        DataSet xx = new DataSet();
                        xx = eve.getSavedEventID(txtEventname.Text);
                        if (xx != null)
                        {
                            foreach (DataRow dr in xx.Tables[0].Rows)
                            {
                                txtID.Value = dr["ID"].ToString();

                            }
                        }
                        Accomodation ag = new Accomodation("cn", 1)
                        {
                            ID = int.Parse(txtTransportID.Value),
                            Name = txtServiceProvider.Text,
                            Capacity = int.Parse(txtCapacity.Text),
                            Available = int.Parse(txtCapacity.Text),
                            EventID = int.Parse(txtID.Value),

                        };
                        if (ag.Save())
                        {
                            txtID.Value = ag.ID.ToString();
                            getSavedLogistics();
                            SuccessAlert($"Accomodation {txtServiceProvider.Text} added sucessfully!");
                        }
                        else
                        {
                            RedAlert("Error Occured");
                        }
                    }

                }
                catch (Exception ex)
                {

                    RedAlert(ex.Message);
                }
            }
            else
            {
                AmberAlert("Can't add logistics without Event");
            }
        }

        protected void getSavedLogistics()
        {
            try
            {
                Transport tra = new Transport("cn", 1);
                AGMEvents eve = new AGMEvents("cn", 1);
                
                eve.getSavedEventID(txtEventname.Text);
                DataSet xxc = new DataSet();
                xxc =eve.getSavedEventID(txtEventname.Text);
                if (xxc!=null)
                {
                    foreach (DataRow item in xxc.Tables[0].Rows)
                    {

                        txtID.Value = item["ID"].ToString();
                    }
                }
                //}
                Logistics ac = new Logistics("cn", 1);
                if (ac.getSavedAccoAndTrans(int.Parse(txtID.Value)) != null)
                {
                    gridAccomodation.DataSource = ac.getSavedAccoAndTrans(int.Parse(txtID.Value));
                    gridAccomodation.DataBind();
                }
                else
                {
                    gridAccomodation.DataSource = null;
                    gridAccomodation.DataBind();
                }
            }
            catch (Exception ex)
            {

                RedAlert(ex.Message);
            }
        }

        protected void btnSaveEvemt_Click(object sender, EventArgs e)
        {
            SaveEvent();

        }

        protected void btnFinish_Click(object sender, EventArgs e)
        {
            if (true)
            {
                SuccessAlert("Saved Successfully");
            }
            
            Response.Redirect("Enquiries");

        }


        protected void gridProjectList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SelectProject")
                {
                    string commandArgument = e.CommandArgument.ToString();
                    string[] fieldValues = commandArgument.Split(',');
                    string indexID = fieldValues[0];
                    string index = fieldValues[1];
                    gridProjectList.Visible = false;
                    AGMProjects pr = new AGMProjects("cn", 1);
                    if (pr.getProjecctsBySearch(index)!=null)
                    {
                        getSavedEventID();

                        AGMEvents eve = new AGMEvents("cn", 1);
                        EventProjects ep = new EventProjects("cn",1);
                        DataSet xx =pr.getProjecctsBySearch(index);
                        ep.ID = int.Parse(txtEPID.Value);
                        ep.EventID = int.Parse(txtID.Value);
                        txtProjectID.Value = indexID;
                        ep.ProjectID = int.Parse(txtProjectID.Value);
                        if (ep.Save())
                        {
                            SuccessAlert("Added Project Successfully");
                            GetAssignedProjects(int.Parse(txtProjectID.Value),int.Parse(txtID.Value));
                        }
                    }
                    else
                    {
                        RedAlert("Error in Adding project");
                    }
                    
                    
                }
            }
            catch (Exception x)
            {

                RedAlert(x.Message);
            }
            
        }


        protected void GetAssignedProjects(int projectID,int ID)
        {
            try
            {
                AGMProjects pr = new AGMProjects("cn", 1);
                EventProjects ep = new EventProjects("cn", 1);

                if (pr.Retrieve(ID))
                {
                    txtID.Value = pr.ID.ToString();
                }
                if (ep.getEventMappedProjects(projectID, ID) != null)
                {
                    gridAddedProjects.DataSource = ep.getEventMappedProjects(projectID,ID);
                    gridAddedProjects.DataBind();
                }
                else
                {
                    gridAddedProjects.DataSource = null;
                    gridAddedProjects.DataBind();
                }
            }
            catch (Exception xx)
            {

                RedAlert(xx.Message);
            }
        }

        protected void RadioOpen_CheckedChanged(object sender, EventArgs e)
        {
            //if (RadioOpen.Checked)
            //{
                
            //    AmberAlert("Are you sure you want event to be open. Press Ok to Continue");
            //}
        }

        protected void CheckEventAvailability()
        {
            AGMEvents eve = new AGMEvents("cn", 1);

            DataSet ds = eve.GetEventInfo(txtEventname.Text);
            if (ds != null)
            {
                DataRow row = ds.Tables[0].Rows[0];

                txtEndDate.Text = row["EndDate"].ToString();
                txtStartDate.Text = row["StartDate"].ToString();
                TxtVenue.Text = row["Venue"].ToString();
                txtAttendanceFee.Text = row["AttendanceFee"].ToString();
                //if (bool.Parse(row["AttendeeSettings"].ToString()) == true)
                //{
                //    _ = RadioInvitation.Checked;
                //}
                //else
                //{
                //    _ = RadioOpen.Checked;
                //}
            }
        }

        protected void txtEventname_TextChanged(object sender, EventArgs e)
        {
            CheckEventAvailability();

        }
    }
}