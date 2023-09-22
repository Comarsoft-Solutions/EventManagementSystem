﻿using AGMSystem.models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Serialization;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace AGMSystem
{
    public partial class Registration1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtMemberID.Value = "0";
                txtEventID.Value = "0";
                txtQueryID.Value = "0";
                pnlConfirmed.Visible = false;
                if (Request.QueryString["EventID"] != null)
                {
                    txtEventID.Value = Request.QueryString["EventID"].ToString();

                    getEvents();
                    Session["EventID"] = txtEventID.Value;

                }
                else
                {
                    getEvents();
                }

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

        private void GetRegisteredMembers()
        {
            try
            {
                MemberRsvpSave tr = new MemberRsvpSave("Cn", 1);
                DataSet xc = tr.GetRsvps(int.Parse(txtEvents.SelectedValue));
                if (xc != null)
                {
                    GrdRegisteredMembers.DataSource = xc;
                    GrdRegisteredMembers.DataBind();
                    pnlConfirmed.Visible = true;

                }
                else
                {
                    GrdRegisteredMembers.DataSource = null;
                    GrdRegisteredMembers.DataBind();
                }

            }
            catch (Exception xc)
            {

                RedAlert(xc.Message);
            }

        }

        protected void SaveQuery()
        {
            try
            {
                AGMQueries query = new AGMQueries("cn", 1);
                query.RegistrationID = int.Parse(txtMemberID.Value);
                query.ID = int.Parse(txtQueryID.Value);
                query.EventID = int.Parse(txtEvents.SelectedValue);
                query.DateCreated = DateTime.Now.ToString();
                query.Query = txtQuery.Text;
                query.QueryType = "Payslip";



                if (query.Save())
                {
                    txtQueryID.Value = query.ID.ToString();
                    if (txtQuery.Text != "")
                    {


                        SuccessAlert("Query Sent to admin");

                    }
                }
            }
            catch (Exception xx)
            {

                RedAlert(xx.Message);
            }
        }

        protected void getSavedMember(string NatID)
        {
            try
            {
                Registration reg = new Registration("cn", 1);
                if (reg.GetRegistration() != null)
                {
                    txtFirstname.Text = reg.FirstName;
                    txtLastName.Text = reg.LastName;
                    txtEmail.Text = reg.Email;
                    txtPhoneNumber.Text = reg.PhoneNumber;
                    txtCategory.Text = reg.Designation;
                    txtCompany.Text = reg.Company;


                }
                else
                {
                    reg.Clear();
                    

                }

            }
            catch (Exception exx)
            {

                MsgBox.msgbox(exx.Message);
            }

        }

        protected void ClearForm()
        {
            txtFirstname.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtCategory.Text = "";
            txtCompany.Text = "";
            txtQuery.Text = string.Empty;
            txtNatID.Text = string.Empty;
            txtTshirtSize.Text = string.Empty;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //UploadFile();
            //Response.Redirect("RSVPList");
            try
            {

                MemberRsvpSave vs = new MemberRsvpSave("cn", 1);

                vs.UpdateRegMemberWithoutCombos(true,txtNatID.Text,int.Parse(txtEvents.SelectedValue),txtTshirtSize.Text);
                if (txtQuery.Text.Length > 3)
                {
                    SaveQuery();
                }

                //DataSet ds = vs.getMemberID(txtNatID.Text);
                //string ID = "1";
                //if (ds!=null)
                //{
                //    DataRow dt = ds.Tables[0].Rows[0];
                //    ID = dt["Id"].ToString();
                //}


                GetRegisteredMembers();
                SuccessAlert(txtFirstname.Text + " Successfully Registered");

                
                string strscript = null;
                strscript = "<script langauage=JavaScript>";
                strscript += "window.open('../Reports/view-quotation.aspx?ID=" + txtMemberID.Value + "');";
                strscript += "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
            }
            catch (Exception xx)
            {
                RedAlert(xx.Message);
                throw;
            }
           


        }
        protected void txtNatID_TextChanged(object sender, EventArgs e)
        {
            
            string natID = txtNatID.Text;

            
            AGMQueries query = new AGMQueries("cn", 1);

            Registration reg = new Registration("cn", 1);

            
            DataSet ds = reg.GetRegInfo(natID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                
                DataRow row = ds.Tables[0].Rows[0];

                
                txtFirstname.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhoneNumber.Text = row["PhoneNumber"].ToString();
                txtCategory.Text = row["Designation"].ToString();
                txtCompany.Text = row["Company"].ToString();
                txtMembershipType.Text = row["MembershipType"].ToString();
                txtQuery.Text = query.Query;
                txtQueryID.Value = query.ID.ToString();
                txtMemberID.Value = row["ID"].ToString();
                txtEventID.Value = query.EventID.ToString();
                txtTshirtSize.Text = row["TshirtSize"].ToString();
                
            }
            else
            {
                WarningAlert("Member not registered, Go to membership tab or try another identity number");
            }

        }


        protected void UploadFile()
        {
            try
            {
                if (flRsvpUpload.HasFile)
                {

                    string filename = Path.GetFileName(flRsvpUpload.PostedFile.FileName);
                    string contentType = flRsvpUpload.PostedFile.ContentType;

                    using (Stream fs = flRsvpUpload.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                string query = "insert into MemberRSVP(ProofOfPayment) values (@ProofOfPayment)";
                                using (SqlCommand cmd = new SqlCommand(query))
                                {
                                    cmd.Connection = con;
                                    cmd.Parameters.Add("@ProofOfPayment", SqlDbType.VarBinary).Value = bytes;

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    SuccessAlert("member RSVP Successfully");
                                    GetLogisticsCombos();
                                }
                            }
                        }
                    }
                }
                else
                {
                    WarningAlert("No file Selected");
                }
            }
            catch (Exception cv)
            {

                RedAlert(cv.Message);
            }
        }
        protected void GetLogisticsCombos()
        {
            try
            {
                Combos tr = new Combos("Cn", 1);
                DataSet xc = tr.GetCombos(int.Parse(txtEvents.SelectedValue));
                if (xc!=null)
                {
                    gridAccomodation.DataSource = xc;
                    gridAccomodation.DataBind();
                }
                else
                {
                    gridAccomodation.DataSource =null;
                    gridAccomodation.DataBind();
                    WarningAlert("No Logistics");
                }
                
            }
            catch (Exception xc)
            {

                RedAlert(xc.Message);
            }
        }
        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {

            GetLogisticsCombos();
            gridAccomodation.Visible = true;
        }



        protected void gridAccomodation_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "selectRecord")
            {
                //
                string commandArgument = e.CommandArgument.ToString();
                // Split the commandArgument to get individual parameters
                string[] parameters = commandArgument.Split('|');
                                
                string id = parameters[0];
                string accomodationID = parameters[1];
                string transportID = parameters[2];
                string combocapacity = parameters[3];

                //int index = int.Parse(e.CommandArgument.ToString());
                try
                {
                    MemberRsvpSave vs = new MemberRsvpSave("cn", 1);

                    vs.UpdateRegMember(int.Parse(id), true, txtNatID.Text, int.Parse(txtEvents.SelectedValue),txtTshirtSize.Text);
                    vs.UpdateAccomodation(int.Parse(txtEvents.SelectedValue), int.Parse(accomodationID), int.Parse(combocapacity));
                    vs.UpdateTransport(int.Parse(txtEvents.SelectedValue), int.Parse(transportID), int.Parse(combocapacity));
                    ClearForm();

                }
                catch (Exception cxc)
                {
                    RedAlert(cxc.Message);
                    throw;
                }
                
            }
        }

       
    }
    
}