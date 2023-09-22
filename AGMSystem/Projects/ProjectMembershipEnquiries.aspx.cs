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
    public partial class ProjectMembershipEnquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                if (Request.QueryString["ProjectID"] != null)
                {

                    txtProjectID.Value = Request.QueryString["ProjectID"].ToString();
                }
                GetAllMembers(int.Parse(txtProjectID.Value));
                GetProjects();
            }

        }
        private void GetAllMembers(int ProjectID)
        {
            Registration mem = new Registration("cn", 1);
            //AGMQueries mem = new AGMQueries("cn", 1);
            if (mem.GetSomeMembers(int.Parse(txtProjectID.Value)) != null)
            {
                grdProjectMembership.DataSource = mem.GetSomeMembers(int.Parse(txtProjectID.Value));
                grdProjectMembership.DataBind();

            }
            else
            {
                grdProjectMembership.DataSource = null;
                grdProjectMembership.DataBind();
                msgbox("No members Found");
            }
        }

        protected void GetProjects()
        {
            AGMProjects pr = new AGMProjects("cn", 1);
            DataSet xc = pr.GetAllProjects();
            //if (xc != null)
            //{
            //    ListItem li = new ListItem("Select Project", "0");
            //    cboProjects.DataSource = xc;
            //    cboProjects.DataValueField = "ID";
            //    cboProjects.DataTextField = "Name";
            //    cboProjects.DataBind();
            //    cboProjects.Items.Insert(0, li);

            //}
            //else
            //{
            //    ListItem li = new ListItem("No Projects", "0");
            //    cboProjects.DataSource = null;
            //    cboProjects.DataBind();
            //    cboProjects.Items.Insert(0, li);
            //}
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
                try
                {
                    EventProjects search = new EventProjects("cn", 1);
                    if (search.getBySearch(int.Parse(txtProjectID.Value),txtFnameSearch.Text, txtLnameSearch.Text, txtNationalID.Text) != null)
                    {

                            grdProjectMembership.DataSource = search.getBySearch(int.Parse(txtProjectID.Value), txtFnameSearch.Text, txtLnameSearch.Text, txtNationalID.Text);
                            grdProjectMembership.DataBind();
                    }
                    else
                    {
                        grdProjectMembership.DataSource = null;
                        grdProjectMembership.DataBind();
                        msgbox("Nothing found for these parameters");
                    }

                }
                catch (Exception exx)
                {

                    msgbox(exx.Message);
                }
            
        }
    }
}