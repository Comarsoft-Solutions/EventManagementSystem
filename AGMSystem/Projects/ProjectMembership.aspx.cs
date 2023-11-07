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
    public partial class ProjectMembership : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtID.Value = "0";
                txtProjectID.Value= "0";
                txtMemberID.Value = "0";
                if (Request.QueryString["ProjectID"]!=null)
                {
                    txtProjectID.Value = Request.QueryString["ProjectID"].ToString();
                    
                }
                else
                {
                    AmberAlert("No Project Selected");
                }
                GetProjects();
                GetAllMembers();
            }
        }

        private void GetAllMembers()
        {
            RegistrationSave mem = new RegistrationSave("cn",1);
            if (mem.GetAllMembers(int.Parse(txtProjectID.Value)) != null)
            {
                grdProjectMembership.DataSource = mem.GetAllMembers(int.Parse(txtProjectID.Value));
                grdProjectMembership.DataBind();

            }
            else
            {
                grdProjectMembership.DataSource = null;
                grdProjectMembership.DataBind();
                AmberAlert("No members Found");
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

        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }

        protected void GetProjects()
        {
            AGMProjects pr = new AGMProjects("cn", 1);
            DataSet xc = pr.GetAllProjects();
            if (xc!=null)
            {
                ListItem li = new ListItem("Select Project","0");
                cboProjects.DataSource = xc;
                cboProjects.DataValueField = "ID";
                cboProjects.DataTextField = "Name";
                cboProjects.DataBind();
                cboProjects.Items.Insert(0, li);

            }
            else
            {
                ListItem li = new ListItem("No Projects", "0");
                cboProjects.DataSource = null;
                cboProjects.DataBind();
                cboProjects.Items.Insert(0,li);
            }
        }

        protected void grdProjectMembership_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProjectMembership.PageIndex = e.NewPageIndex;
            this.GetAllMembers();
        }

        protected void grdProjectMembership_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "addMember")
            {
                int index= int.Parse(e.CommandArgument.ToString());
                AddMembers(index);
                GetAllMembers();
            }

        }

        protected void AddMembers(int index)
        {
            try
            {
                ProjectRsvpSave pro = new ProjectRsvpSave("cn", 1);
                if (pro.CheckForMember(int.Parse(txtProjectID.Value), index))
                {
                    AmberAlert("Member already registered");
                    //ClearForm();
                }
                else
                {
                    pro.ProjectID = int.Parse(txtProjectID.Value);
                    pro.MemberID = index;
                    //if (chkMmber.Checked)
                    //{
                    //    pro.IsMember = true;
                    //}
                    //else
                    //{
                    //    pro.IsMember = false;
                    //}
                    //if (chkPrivate.Checked)
                    //{
                    //    pro.OnlineStudy = true;
                    //}
                    //else
                    //{
                    //    pro.OnlineStudy = false;
                    //}

                    if (pro.Save())
                    {
                        SuccessAlert( " Registered");
                        //ClearForm();
                    }
                    else
                    {
                        //WarningAlert("Something happened");
                    }
                }

            }
            catch (Exception sd)
            {
                RedAlert(sd.Message);

            }
        }
        //protected void AddMembers(int index)
        //{
        //    try
        //    {

        //        ProjectMembers pm = new ProjectMembers("cn", 1);
        //        txtMemberID.Value = index.ToString();
        //        pm.MemberId = int.Parse(txtMemberID.Value);
        //        pm.ProjectID = int.Parse(txtProjectID.Value);
        //        pm.Id = int.Parse(txtID.Value);
        //        if (pm.Save())
        //        {
        //            SuccessAlert("Member added to project");
        //        }
        //        else
        //        {
        //            AmberAlert("failed to add Member");
        //        }

        //    }
        //    catch (Exception c)
        //    {

        //        RedAlert(c.Message);
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectEnquiries");
        }
    }
}