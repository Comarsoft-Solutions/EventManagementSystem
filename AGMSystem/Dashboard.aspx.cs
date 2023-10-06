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
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                GetStatistics();
                GetEvents();
                GetOngoingEvents();
            }
        }

        private void GetStatistics()
        {
            RSVP eg = new RSVP("cn",1);
            DataSet eve = eg.GetEventCount();
            DataSet mem = eg.GetMemberCount();
            DataSet pro = eg.GetProjectCount();
            DataSet agm = eg.GetAGMCount();
            if (mem != null )
            {
                foreach (DataRow dr in mem.Tables[0].Rows) 
                {
                    lblMembers.Text= dr["Count"].ToString();
                
                }
            }
            if (eve != null )
            {
                foreach (DataRow dr in eve.Tables[0].Rows) 
                {
                    lblEvents.Text= dr["Count"].ToString();
                
                }
            }
            if (pro != null )
            {
                foreach (DataRow dr in pro.Tables[0].Rows) 
                {
                    lblTotalProjects.Text= dr["Count"].ToString();
                
                }
            }
            if (agm != null )
            {
                foreach (DataRow dr in agm.Tables[0].Rows) 
                {
                    lblOpenProjects.Text= dr["Count"].ToString();
                
                }
            }
        }

        private void GetOngoingEvents()
        {
            RSVP eg = new RSVP("cn", 1);
            DataSet eve = eg.GetOngoingEvents();

            if (eve != null)
            {
                grdOngoingEvents.DataSource = eve;
                grdOngoingEvents.DataBind();
                pnlOnGoing.Visible = true;
            }
            else
            {
                grdOngoingEvents.DataSource = null;
                grdOngoingEvents.DataBind();
                pnlOnGoing.Visible = false;
                ////msgbox("There are no members");

            }

        }
        private void GetEvents()
        {
            RSVP eg = new RSVP("cn", 1);
            DataSet eve = eg.GetUpcommingEvents();

            if (eve != null)
            {
                grdEvents.DataSource = eve;
                grdEvents.DataBind();
                pnlUpcomming.Visible = true;
            }
            else
            {
                grdEvents.DataSource = null;
                grdEvents.DataBind();
                pnlUpcomming.Visible = false;
                ////msgbox("There are no members");

            }

        }

        protected void grdEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdEvents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdOngoingEvents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
    }
}