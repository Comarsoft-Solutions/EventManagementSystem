using AGMSystem.models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class ProjectCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtID.Value = "0";
                txtStatusID.Value = "0";
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        protected void SaveProject()
        {
            try
            {
                if (txtProjName.Text.IsNullOrWhiteSpace() || txtDescription.Text.IsNullOrWhiteSpace())
                {
                    AmberAlert("Fill in all fields");
                }
                else
                {
                    AGMProjects proj = new AGMProjects("cn", 1);
                    if (DateTime.TryParse(txtStartDate.Text, out DateTime dt))
                    {
                        proj.StartDate = Convert.ToDateTime(txtStartDate.Text);
                    }
                    else
                    {
                        AmberAlert("Enter Valid Date");
                    }
                    if (DateTime.TryParse(txtExamDate.Text, out DateTime edt))
                    {
                        proj.ExamDate = Convert.ToDateTime(txtExamDate.Text);
                    }
                    else
                    {
                        AmberAlert("Enter Valid Date");
                    }
                    if (DateTime.TryParse(txtMaturity.Text, out DateTime dte))
                    {

                        proj.MaturityDate = Convert.ToDateTime(txtMaturity.Text);

                        
                    }
                    if (DateTime.Parse(txtMaturity.Text)< DateTime.Parse(txtStartDate.Text))
                    {
                        AmberAlert("Enter Valid End Date");
                    }
                    else
                    {
                        AmberAlert("Enter Valid Date");
                    }
                    proj.Name = txtProjName.Text;
                    proj.Description = txtDescription.Text;
                    proj.StatusID = Convert.ToInt32(txtStatusID.Value);
                    proj.ID = Convert.ToInt32(txtID.Value);
                    proj.Venue = txtVenue.Text;
                    if (proj.Save())
                    {
                        SuccessAlert(txtProjName.Text + " Saved Sucessfully");
                        clearForm();
                    }
                    else
                    {
                        AmberAlert("There was an error on saving" + txtProjName.Text + " details: " + proj.MsgFlg);
                    }
                }


            }
            catch (Exception xxx)
            {

                RedAlert(xxx.Message);
            }
        }


        #region alerts
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

        private void clearForm()
        {
            txtProjName.Text="";
            txtStartDate.Text = "";
            txtMaturity.Text = "";
            txtExamDate.Text = "";
            txtDescription.Text = "";
            txtVenue.Text = "";
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
    }
}