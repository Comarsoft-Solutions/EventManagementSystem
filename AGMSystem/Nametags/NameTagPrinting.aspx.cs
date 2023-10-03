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

            }
            if (cboPrintOptions.SelectedItem.Value == "2")
            {
                GetMembers();
            }
            if (cboPrintOptions.SelectedItem.Value == "3")
            {
                pnlCompanySearch.Visible = true;
                pnlMemberSearch.Visible = false;
            }
            if (cboPrintOptions.SelectedItem.Value == "4")
            {
                pnlMemberSearch.Visible= true;
                pnlCompanySearch.Visible = false;
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
                if (allMemmbers != null)
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
                if (rsvpMembers != null)
                {
                    //foreach (DataRow item in rsvpMembers.Tables[0].Rows)
                    //{

                    //}
                    string Type = "RSVP";

                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
                if (companyMembers != null)
                {
                    //foreach (DataRow item in companyMembers.Tables[0].Rows)
                    //{


                    //}
                    string Type = "companyMembers";
                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
                if (singleMember != null)
                {
                    //foreach (DataRow item in singleMember.Tables[0].Rows)
                    //{
                        
                        
                    //}
                    string Type = "singleMember";

                    string strscript = null;
                    strscript = "<script langauage=JavaScript>";
                    strscript += "window.open('../Reports/printNameTag.aspx?ID=" + Type + "');";
                    strscript += "</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "newwin", strscript);
                }
            }
            catch (Exception xc)
            {

                RedAlert(xc.Message);
            }
        }
    }
}