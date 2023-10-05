using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class RsvpList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getEvents();

            }
        }
        #region alerts

        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }
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
        
        private void getSavedRSVPList()
        {
            
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetRsvps(int.Parse(txtEvents.SelectedValue));
                if (c != null)
                {
                    grdPaymentConfirmation.DataSource = c;
                    grdPaymentConfirmation.DataBind();
                    btnExport.Visible = true;
                }
                else
                {
                    grdPaymentConfirmation.DataSource = null;
                    grdPaymentConfirmation.DataBind();
                    msgbox("No One RSVP'd");
                    btnExport.Visible = false;
                }
            }
            catch (Exception c)
            {

                RedAlert(c.Message);
            }
        }
        private void getEvents()
        {

            try
            {
                AGMEvents agm = new AGMEvents("cn", 1);
                DataSet ds = agm.getAllEvents();
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Event", "0");
                    txtEvents.DataSource = ds;
                    txtEvents.DataValueField = "ID";
                    txtEvents.DataTextField = "EventName";
                    txtEvents.DataBind();
                    txtEvents.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No Events found", "0");
                    txtEvents.Items.Clear();
                    txtEvents.DataSource = null;
                    txtEvents.DataBind();
                    txtEvents.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                RedAlert(a.Message);
            }
        }

        protected void grdPaymentConfirmation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPaymentConfirmation.PageIndex = e.NewPageIndex;
            this.BindGrid(e.NewPageIndex);
        }
        private void BindGrid(int page = 0)
        {
            try
            {
                MemberRsvpSave r = new MemberRsvpSave("cn", 1);
                DataSet c = r.GetRsvps(int.Parse(txtEvents.SelectedValue));
                if (c != null)
                {
                    int maxPageIndex = grdPaymentConfirmation.PageCount - 1;
                    if (page < 0 || page > maxPageIndex)
                    {
                        if (maxPageIndex >= 0)
                        {
                            // Navigate to the last available page
                            page = maxPageIndex;
                        }
                        else
                        {
                            // No data available, reset to the first page
                            page = 0;
                        }
                    }
                    grdPaymentConfirmation.DataSource = c;
                    grdPaymentConfirmation.PageIndex = page;
                    grdPaymentConfirmation.DataBind();
                }
                else
                {
                    grdPaymentConfirmation.DataSource = null;
                    grdPaymentConfirmation.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                MemberRsvpSave reg = new MemberRsvpSave("cn", 1);
                DataSet ds = reg.GetRSVPSList(int.Parse(txtEvents.SelectedItem.Value), txtFnameSearch.Text, txtLnameSearch.Text, txtCompanySearch.Text);
                if (ds != null)
                {
                    grdPaymentConfirmation.DataSource = ds;
                    grdPaymentConfirmation.DataBind();
                }
                else
                {
                    grdPaymentConfirmation.DataSource = null;
                    grdPaymentConfirmation.DataBind();
                    WarningAlert("Nothing found for those search values");
                    btnExport.Visible = false;
                }

            }
            catch (Exception x)
            {
                WarningAlert(x.Message);
                throw;
            }
        }

        protected void grdPaymentConfirmation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectRecord")
            {
                try
                {

                    int index = int.Parse(e.CommandArgument.ToString());
                    MemberRsvpSave rs = new MemberRsvpSave("cn", 1);
                    rs.updateRsvp(index, true);
                    SuccessAlert("Check-in Successful");
                    getSavedRSVPList();
                }
                catch (Exception xc)
                {

                    RedAlert(xc.Message);
                }
            }
            if (e.CommandName == "deleteRecord")
            {
                int memberId = int.Parse(e.CommandArgument.ToString());
                MemberRsvpSave mem = new MemberRsvpSave("cn",1);
                mem.DeleteRecord(memberId);
                getSavedRSVPList();
            }
            if (e.CommandName == "editRecord")
            {
                int memberID = int.Parse(e.CommandArgument.ToString());
                Response.Redirect(string.Format("RsvpEdit?MemberID=" + memberID, memberID), false);
            }
        }

        protected void txtEvents_TextChanged(object sender, EventArgs e)
        {

            getSavedRSVPList();
            grdPaymentConfirmation.Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string filename = $"Assets_{DateTime.Now.Minute}.xls";
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdPaymentConfirmation.AllowPaging = false;
                getSavedRSVPList();

                grdPaymentConfirmation.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdPaymentConfirmation.HeaderRow.Cells)
                {
                    cell.BackColor = grdPaymentConfirmation.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdPaymentConfirmation.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdPaymentConfirmation.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdPaymentConfirmation.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdPaymentConfirmation.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}