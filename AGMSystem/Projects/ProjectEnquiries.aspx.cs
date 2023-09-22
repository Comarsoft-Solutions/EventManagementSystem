using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class ProjectEnquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                AGMProjects proj = new AGMProjects("cn", 1);
                if (proj.getSavedProjects()!= null)
                {
                    grdProjectEnquiries.DataSource = proj.getSavedProjects();
                    grdProjectEnquiries.DataBind();
                }
                else
                {
                    grdProjectEnquiries.DataSource=null;
                    grdProjectEnquiries.DataBind();
                    msgbox("There are no Projects");
                }
            }
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

        protected void grdProjectEnquiries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdProjectEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "attachRecord")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Response.Redirect(string.Format("ProjectMembership?ProjectID=" + index, index), false);
            }
            if (e.CommandName == "viewRecord")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                Response.Redirect(string.Format("ProjectMembershipEnquiries?ProjectID=" + index, index), false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AGMProjects search = new AGMProjects("cn", 1);
            if (search.getProjecctsBySearch() !=null)
            {
                grdProjectEnquiries.DataSource = search.getProjecctsBySearch();
                grdProjectEnquiries.DataBind();
            }
            else
            {
                grdProjectEnquiries.DataSource=null;
                grdProjectEnquiries.DataBind();
                msgbox("No Projects with: " + txtProjectName.Text);
            }
        }
    }
}