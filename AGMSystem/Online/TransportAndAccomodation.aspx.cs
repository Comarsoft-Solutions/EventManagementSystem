using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Online
{
    public partial class TransportAndAccomodation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {

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
        protected void btnDone_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtFullName.Text = string.Empty;
            txtFirstname.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtCompany.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAccomodation.Text = string.Empty;
            txtTransport.Text = string.Empty;
        }

        protected void txtFullName_TextChanged(object sender, EventArgs e)
        {
            string natID = txtFullName.Text;


            AGMQueries query = new AGMQueries("cn", 1);

            MemberRsvpSave reg = new MemberRsvpSave("cn", 1);


            DataSet ds = reg.GetAccomodationAndTransportInfo(natID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataRow row = ds.Tables[0].Rows[0];


                txtFirstname.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhoneNumber.Text = row["PhoneNumber"].ToString();
                txtCompany.Text = row["Company"].ToString();
                txtAccomodation.Text = row["Accomodation"].ToString();
                txtQueryID.Value = query.ID.ToString();
                txtMemberID.Value = row["ID"].ToString();
                txtEventID.Value = query.EventID.ToString();
                //txtTransport.Text = row["Transport"].ToString();

            }
            else
            {
                WarningAlert("Member not registered, Go to membership tab or try another identity number");
            }
        }
    }
}