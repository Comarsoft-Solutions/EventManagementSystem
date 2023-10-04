using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Nametags
{
    public partial class NameTagPrinting : System.Web.UI.Page
    {
        DataSet allMemmbers = new DataSet();
        DataSet companyMembers = new DataSet();
        DataSet singleMember = new DataSet();
        DataSet rsvpMembers = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                GetPrintOptions();
                getEvents();
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

        private void getEvents()
        {

            try
            {
                AGMEvents agm = new AGMEvents("cn", 1);
                DataSet ds = agm.getAllEvents();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Event", "0");
                    cboEvents.DataSource = ds;
                    cboEvents.DataValueField = "ID";
                    cboEvents.DataTextField = "EventName";
                    cboEvents.DataBind();
                    cboEvents.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No AGMS found", "0");
                    cboEvents.Items.Clear();
                    cboEvents.DataSource = null;
                    cboEvents.DataBind();
                    cboEvents.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                RedAlert(a.Message);
            }
        }
        protected void GetMembers()
        {
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                rsvpMembers = r.GetRsvps(int.Parse(cboPrintOptions.SelectedValue));
                if (rsvpMembers != null)
                {
                    grdMembers.DataSource = rsvpMembers;
                    grdMembers.DataBind();
                }
                else
                {
                    grdMembers.DataSource = null;
                    grdMembers.DataBind();
                    WarningAlert("No One RSVP'd");
                }
            }
            catch (Exception c)
            {

                RedAlert(c.Message);
            }
        }

        protected void GetPrintOptions()
        {
            try
            {
                Registration reg = new Registration("cn", 1);

                DataSet ds = reg.GetPrintOptions();
                if (ds != null)
                {
                   ListItem li = new ListItem("Select Print Options", "0");
                    cboPrintOptions.DataSource = ds;
                    cboPrintOptions.DataValueField = "ID";
                    cboPrintOptions.DataTextField = "PrintOption";
                    cboPrintOptions.DataBind();
                    cboPrintOptions.Items.Insert(0, li);
                }
                else
                {
                    ListItem li = new ListItem("No Print Options Found", "0");
                    cboPrintOptions.DataSource = null;
                    cboPrintOptions.Items.Clear();
                    cboPrintOptions.DataBind();
                    cboPrintOptions.Items.Insert(0, li);
                }

               
            }
            catch (Exception exxx)
            {

                WarningAlert(exxx.Message);
            }
        }

        protected void cboPrintOptions_TextChanged(object sender, EventArgs e)
        {
            if (cboPrintOptions.SelectedItem.Value == "1")
            {
                GetRegistration();
                pnlEvents.Visible = false;
                pnlCompanySearch.Visible = false;
                pnlMemberSearch.Visible = false;
                grdMemberSearch.Visible = false;
            }
            if (cboPrintOptions.SelectedItem.Value == "2")
            {
                pnlEvents.Visible = true;
                pnlCompanySearch.Visible = false;
                pnlMemberSearch.Visible = false;
                grdMemberSearch.Visible = false;
            }
            if (cboPrintOptions.SelectedItem.Value == "3")
            {
                pnlCompanySearch.Visible = true;
                pnlEvents.Visible = true;
                pnlMemberSearch.Visible = false;
                grdMemberSearch.Visible = false;
            }
            if (cboPrintOptions.SelectedItem.Value == "4")
            {
                pnlMemberSearch.Visible= true;
                pnlEvents.Visible = true;
                pnlCompanySearch.Visible = false;
                grdMembers.Visible = false;
            }
        }

        private void GetRegistration()
        {
            Registration reg = new Registration("cn",1);
            allMemmbers = reg.GetMembership();
            if (allMemmbers != null)
            {
                grdMembers.DataSource = allMemmbers;
                grdMembers.DataBind();
            }
            else
            {
                grdMembers.DataSource= null;
                grdMembers.DataBind();
                WarningAlert("No Members");
            }
        }

        protected void grdMembers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnMemberSearch_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration("cn", 1);
            singleMember = reg.GetMembersBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtNatID.Text);
            if (singleMember != null)
            {
                grdMembers.DataSource = singleMember;
                grdMembers.DataBind();
            }
            else
            {
                grdMembers.DataSource = null;
                grdMembers.DataBind();
                WarningAlert("Nothing found for these parameters");
            }
        }

        protected void btnCompanySearch_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration("cn", 1);
            companyMembers = reg.GetMembersByCompany(txtCompanySearch.Text);
            if (companyMembers != null)
            {
                grdMembers.DataSource = companyMembers;
                grdMembers.DataBind();
            }
            else
            {
                grdMembers.DataSource = null;
                grdMembers.DataBind();
                WarningAlert("Nothing found for these parameters");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboPrintOptions.SelectedItem.Value == "1")
                {
                    //foreach (DataRow item in allMemmbers.Tables[0].Rows)
                    //{
                        
                    //}
                    string Type = "AllMembers";

                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
                if (cboPrintOptions.SelectedItem.Value == "2")
                {
                    //foreach (DataRow item in rsvpMembers.Tables[0].Rows)
                    //{

                    //}
                    string Type = "RSVP";

                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "&eventID="+cboEvents.SelectedItem.Value+"');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
                if (cboPrintOptions.SelectedItem.Value == "3")
                {
                    //foreach (DataRow item in companyMembers.Tables[0].Rows)
                    //{


                    //}
                    string Type = "companyMembers";
                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "&company="+txtCompanySearch.Text+"');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
                if (cboPrintOptions.SelectedItem.Value == "4")
                {
                    //foreach (DataRow item in singleMember.Tables[0].Rows)
                    //{
                        
                        
                    //}
                    //string Type = "singleMember";

                    //string strscript = null;
                    //strscript = "<script langauage=JavaScript>";
                    //strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "');";
                    //strscript += "</script>";
                    //ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
            }
            catch (Exception xc)
            {

                RedAlert(xc.Message);
            }
        }
        private void getSavedRSVPList()
        {

            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetRsvps(int.Parse(cboEvents.SelectedValue));
                if (c != null)
                {
                    grdMembers.DataSource = c;
                    grdMembers.DataBind();
                }
                else
                {
                    grdMembers.DataSource = null;
                    grdMembers.DataBind();
                    WarningAlert("No One RSVP'd");
                }
            }
            catch (Exception c)
            {

                RedAlert(c.Message);
            }
        }
        protected void cboEvents_TextChanged(object sender, EventArgs e)
        {
            getSavedRSVPList();
        }

        protected void grdMemberSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdMemberSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectRecord")
            {
                btnPrint.Visible = false;
                string memberID = e.CommandArgument.ToString();

                string Type = "singleMember";

                string strscript = null;
                strscript = "<script langauage=JavaScript>";
                strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "& memberID="+ memberID + "');";
                strscript += "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
            }
        }
    }
}