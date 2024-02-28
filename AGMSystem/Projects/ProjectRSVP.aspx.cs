using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Projects
{
    public partial class ProjectRSVP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getProjects();
                getQualifications();
            }
        }

        private void getQualifications()
        {
            try
            {
                LookUp lk = new LookUp("cn", 1);
                DataSet ds = lk.GetQualifications();
                if (ds!=null)
                {
                    ListItem li = new ListItem("Select Highest Education Qualification", "0");
                    txtEducation.DataSource = ds;
                    txtEducation.DataTextField = "Qualifications";
                    txtEducation.DataValueField = "ID";
                    txtEducation.DataBind();
                    txtEducation.Items.Insert(0, li);
                }
                else
                {
                    ListItem li = new ListItem("No Qualifications", "0");
                    txtEducation.Items.Clear();
                    txtEducation.DataSource = null;
                    txtEducation.DataBind();
                    txtEducation.Items.Insert(0, li);

                }
            }
            catch (Exception x)
            {

                RedAlert(x.Message);
            }
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

        protected void txtNatID_TextChanged(object sender, EventArgs e)
        {

            string natID = txtNatID.Text;


            AGMQueries query = new AGMQueries("cn", 1);

            RegistrationSave reg = new RegistrationSave("cn", 1);


            DataSet ds = reg.GetRegInfo(natID);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                DataRow row = ds.Tables[0].Rows[0];

                txtmemberID.Value = row["ID"].ToString();
                txtFirstname.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtPhoneNumber.Text = row["PhoneNumber"].ToString();
                //txtEducation.Text = row["Education"].ToString();
                txtCompany.Text = row["Company"].ToString();
                txtQuery.Text = query.Query;
                txtQueryID.Value = query.ID.ToString();
                txtProjectID.Value = txtProjects.SelectedValue;
                txtPensionFund.Text = row["PensionFund"].ToString();

            }
            else
            {
                WarningAlert("Member not registered, try another identity number");
            }

        }
        private void getProjects()
        {

            try
            {
                AGMProjects agm = new AGMProjects("cn", 1);
                DataSet ds = agm.GetAllProjects();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Projects", "0");
                    txtProjects.DataSource = ds;
                    txtProjects.DataValueField = "ID";
                    txtProjects.DataTextField = "Name";
                    txtProjects.DataBind();
                    txtProjects.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No Projects found", "0");
                    txtProjects.Items.Clear();
                    txtProjects.DataSource = null;
                    txtProjects.DataBind();
                    txtProjects.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                RedAlert(a.Message);
            }
        }
        protected void txtProjects_TextChanged(object sender, EventArgs e)
        {
            txtProjectID.Value = txtProjects.SelectedValue.ToString();
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectRsvpSave pro = new ProjectRsvpSave("cn", 1);
                if (pro.CheckForMember(int.Parse(txtProjects.SelectedValue),int.Parse(txtmemberID.Value)))
                {
                    WarningAlert("Member already registered");
                    ClearForm();
                }
                else
                {
                    pro.ProjectID = int.Parse(txtProjects.SelectedValue);
                    pro.MemberID = int.Parse(txtmemberID.Value);
                    if (chkMmber.Checked)
                    {
                        pro.IsMember = true;
                    }
                    else
                    {
                        pro.IsMember = false;
                    }
                    if (chkPrivate.Checked)
                    {
                        pro.OnlineStudy = true;
                    }
                    else
                    {
                        pro.OnlineStudy = false;
                    }

                    if (pro.Save())
                    {
                        SuccessAlert(txtFirstname.Text + " Registered");
                        ClearForm();
                    }
                    else
                    {
                        WarningAlert("Something happened");
                    }
                }

            }
            catch (Exception sd)
            {
                RedAlert (sd.Message);
                
            }
        }


        private void ClearForm()
        {
            txtFirstname.Text=string.Empty;
            txtLastName.Text=string.Empty;
            txtNatID.Text=string.Empty;
            txtPhoneNumber.Text=string.Empty;
            txtPhoneNumber.Text=string.Empty;
            txtEmail.Text=string.Empty;
            txtEducation.SelectedValue="0";
            txtCompany.Text=string.Empty;
            txtQuery.Text=string.Empty;
            txtPensionFund.Text=string.Empty;
            chkMmber.Checked = false;
            chkPrivate.Checked = false;

        }
    }
}