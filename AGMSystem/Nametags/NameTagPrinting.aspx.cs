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
        protected void Page_Load(object sender, EventArgs e)
        {

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
                DataSet c = r.GetRsvps(int.Parse(cboPrintOptions.SelectedValue));
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
            DataSet ds = reg.GetRegistration();
            if (ds!=null)
            {
                grdMembers.DataSource = ds;
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
            DataSet ds = reg.GetMembersBySearch(txtFnameSearch.Text, txtLnameSearch.Text, txtNatID.Text);
            if (ds!=null)
            {
                grdMembers.DataSource = ds;
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
            DataSet ds = reg.GetMembersByCompany(txtCompanySearch.Text);
            if (ds != null)
            {
                grdMembers.DataSource = ds;
                grdMembers.DataBind();
            }
            else
            {
                grdMembers.DataSource = null;
                grdMembers.DataBind();
                WarningAlert("Nothing found for these parameters");
            }
        }
    }
}