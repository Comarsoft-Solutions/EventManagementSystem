using AGMSystem.models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class MemberCreation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtMemberID.Value = "0";
                txtEventID.Value = "0";
                txtQueryID.Value = "0";
                pnlCompany.Visible = false;
                pnlMember.Visible = false;
                txtCompanyID.Value = "0";
                getCompanies();
                GetRegType();
                ClearAlert();
                //GetDesignation();
            }

        }
        protected void ClearAlert()
        {
            pnlDanger.Visible = false;
            lblDanger.Text = "";

            pnlSuccess.Visible = false;
            lblSuccess.Text = "";

            pnlWarning.Visible = false;
            lblWarning.Text = "";
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
        

        private void GetDesignation()
        {
            throw new NotImplementedException();
        }

        protected void ClearControls()
        {
            txtCompanyName.SelectedValue = "0";
            txtMemberID.Value = "0";
            txtFname.Text = "";
            txtLname.Text ="";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtNationalID.Text = "";
            txtTshirt.Text = "";
            
        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtRegType.Text=="1")
            {
                RegisterMember();
            }
            else
            {
                RegisterCompany();
            }

        }

        private void RegisterCompany()
        {
            try
            {
                if (txtCompanyReg.Text.IsNullOrWhiteSpace()&&txtCity.Text.IsNullOrWhiteSpace()&&txtAddress.Text.IsNullOrWhiteSpace())
                {
                    SuccessAlert("Fill in all Fields ");
                }
                else
                {
                    CompanyRegistration cr = new CompanyRegistration("cn", 1);
                    cr.ID = int.Parse(txtCompanyID.Value);
                    cr.Name = txtCompanyReg.Text;
                    cr.Address = txtAddress.Text;
                    cr.City = txtCity.Text;
                    //cr.ZipCode = txtZip.Text;
                    if (cr.Save())
                    {
                        SuccessAlert("Company Saved Successfully!");
                        clearFields();
                    }
                    else
                    {
                        WarningAlert(cr.Msgflg);
                    }
                }
            }
            catch (Exception c)
            {

                RedAlert(c.Message);
            }
        }

        private void clearFields()
        {
            txtCompanyReg.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            //txtZip.Text = "";
        }

        protected void GetRegType()
        {
            try
            {
                AGMQueries q = new AGMQueries("cn", 1);
                DataSet xx = q.GetRegTypes();
                if (xx!= null)
                {
                    ListItem item = new ListItem("Select Registration Type","0");
                    txtRegType.DataSource = xx;
                    txtRegType.DataValueField = "ID";
                    txtRegType.DataTextField = "Type";
                    txtRegType.DataBind();
                    txtRegType.Items.Insert(0,item);
                }
                else
                {
                    ListItem li = new ListItem("No Registration types Found", "0");
                    txtRegType.Items.Clear();
                    txtRegType.DataSource = null;
                    txtRegType.DataBind();
                    txtRegType.Items.Insert(0,li);
                }
                
            }
            catch (Exception x)
            {

                RedAlert(x.Message);
            }
        }
        protected void RegisterMember()
        {
            if ( txtFname.Text.IsNullOrWhiteSpace() || txtLname.Text.IsNullOrWhiteSpace() || txtPhoneNumber.Text.IsNullOrWhiteSpace() || txtEmail.Text.IsNullOrWhiteSpace() || txtDesignation.Text.IsNullOrWhiteSpace() || txtCompanyName.Text.IsNullOrWhiteSpace())
            {
                WarningAlert("Fill in all required fields");


                return;
            }
            else
            {
                CheckForMember();
                try
                {

                    Registration reg = new Registration("cn", 1);
                    reg.EventID = int.Parse(txtEventID.Value);
                    reg.Id = int.Parse(txtMemberID.Value);
                    reg.NationalID = txtNationalID.Text;
                    reg.FirstName = txtFname.Text.ToUpper();
                    reg.LastName = txtLname.Text.ToUpper();
                    reg.Email = txtEmail.Text.ToLower();
                    reg.PhoneNumber = txtPhoneNumber.Text;
                    reg.Designation = txtDesignation.Text;
                    reg.Company = txtCompanyName.SelectedItem.Text;
                    reg.MemberType = txtRegType.SelectedItem.Text;
                    reg.CompanyID = int.Parse( txtCompanyName.SelectedValue);
                    reg.PensionFund = txtPensionFund.Text;
                    reg.MemberAddress = txtMemAddress.Text;
                    reg.Tshirt = txtTshirt.Text;
                    if (chkGolf.Checked)
                    {
                        reg.Golf = true;
                    }
                    else
                    {
                        reg.Golf= false;
                    }

                    if (txtRegType.Text.Contains("Individual"))
                    {
                        if (txtCompanyName.SelectedValue =="0")
                        {
                            WarningAlert("Individual profiles should have a representing company");
                            return;
                        }
                    }

                    if (reg.Save())
                    {
                        txtMemberID.Value = reg.Id.ToString();
                        SuccessAlert(txtFname.Text + " Member Registered Successfully");
                        ClearControls();
                    }
                    else
                    {
                        RedAlert(reg.Msgflg);
                    }

                }
                catch (Exception exx)
                {
                    RedAlert(exx.Message);
                }

            }


        }
        protected void getCompanies()
        {

            try
            {
                CompanyRegistration r = new CompanyRegistration("cn", 1);
                DataSet xx = r.getSavedCompanies();
                if (xx != null) {
                    ListItem li = new ListItem("Select a company", "0");
                    txtCompanyName.DataSource = xx;
                    txtCompanyName.DataValueField = "ID";
                    txtCompanyName.DataTextField = "Name";
                    txtCompanyName.DataBind();

                    txtCompanyName.Items.Insert(0, li);
                }
                else

                {
                    ListItem li = new ListItem("There are no companies available", "0");
                    txtCompanyName.DataSource = null;
                    txtCompanyName.DataBind();
                    txtCompanyName.Items.Insert(0, li);
                }
            }
            catch (Exception ex)
            {
                WarningAlert(ex.Message);
            }
        }

        protected void txtRegType_TextChanged(object sender, EventArgs e)
        {
            ClearAlert();
            try
            {
                if (txtRegType.Text == "1")
                {
                    pnlCompany.Visible = false;
                    pnlMember.Visible = true;
                }
                else
                {
                    pnlCompany.Visible = true;
                    pnlMember.Visible = false;
                }
            }
            catch (Exception cv)
            {
                WarningAlert(cv.Message);
            }
        }

        private void CheckForMember()
        {
            Registration reg = new Registration("cn", 1);

            DataSet ds = reg.GetRegInfo(txtNationalID.Text);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataRow row = ds.Tables[0].Rows[0];
                WarningAlert(row["FirstName"].ToString() + " ALREADY EXISTS");
                ClearControls();
            }
        }

        protected void txtNationalID_TextChanged(object sender, EventArgs e)
        {
            CheckForMember();
        }
    }
}