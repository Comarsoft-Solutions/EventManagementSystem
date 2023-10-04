using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace AGMSystem.Registration
{
    public partial class RsvpEdit : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            
            if (!IsPostBack)
            {
                if (Request.QueryString["memberID"] != null)
                {
                    txtMemberID.Value = Request.QueryString["memberID"].ToString();
                }
                GetMemberDetails();
            }
        }

        private void GetMemberDetails()
        {
            try
            {

                MemberRsvpSave member = new MemberRsvpSave("cn", 1);
                DataSet ds = member.GetMemberInfo(int.Parse(txtMemberID.Value));
                if (ds != null)
                {
                    DataRow rw = ds.Tables[0].Rows[0];

                    txtFirstname.Text= rw["FirstName"].ToString() ;
                    txtLastName.Text= rw["LastName"].ToString() ;
                    txtEmail.Text= rw["Email"].ToString() ;
                    txtPhoneNumber.Text= rw["PhoneNumber"].ToString() ;
                    txtCompany.Text= rw["Company"].ToString() ;
                    txtAccomodation.Text= rw["Accomodation"].ToString() ;
                    txtTransport.Text= rw["Transport"].ToString() ;
                    txtDesignation.Text = rw["Designation"].ToString() ;
                }
            }
            catch (Exception xc)
            {

                WarningAlert(xc.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MemberRsvpSave mem = new MemberRsvpSave("cn", 1);
                mem.UpdateMember(txtFirstname.Text,txtLastName.Text,txtEmail.Text,txtPhoneNumber.Text,txtCompany.Text,txtAccomodation.Text,txtTransport.Text,txtDesignation.Text,int.Parse(txtMemberID.Value));
                SuccessAlert("Attendee " + txtFirstname.Text + "Updated");
            }
            catch (Exception c)
            {

                WarningAlert(c.Message);
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
    }
}