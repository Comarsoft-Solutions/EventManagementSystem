using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class Enquiries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;

            if (!IsPostBack)
            {
                GetAllSavedEvents();
                if (Request.QueryString["EventID"] != null)
                {
                    txtEventID.Value = Request.QueryString["EventID"].ToString();
                    getQueries(int.Parse(txtEventID.Value));
                    //cboEvent.SelectedValue = txtEventID.Value;
                    Session["EventID"]= txtEventID.Value;
                }



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
        //protected void GetSavedEvents()
        //{
        //    try
        //    {
        //        AGMEvents ag = new AGMEvents("CN", 1);
        //        DataSet ds = ag.getSavedEvents();
        //        if (ds != null)
        //        {
        //            ListItem li = new ListItem("Select an Event", "0");
        //            cboEvent.DataSource = ds;
        //            cboEvent.DataValueField = "ID";
        //            cboEvent.DataTextField = "EventName";
        //            cboEvent.DataBind();
        //            cboEvent.Items.Insert(0, li);
        //        }
        //        else
        //        {
        //            ListItem li = new ListItem("There are no defined events", "0");
        //            cboEvent.DataSource = null;
        //            cboEvent.DataBind();
        //            cboEvent.Items.Insert(0, li);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msgbox(ex.Message);

        //    }
        //}
        protected void grdEnquiries_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEnquiries.PageIndex = e.NewPageIndex;
            this.GetAllSavedEvents();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                AGMEvents enq = new AGMEvents("cn", 1);
                DataSet ds = enq.getEnquiriesByFilterSearch(txtFnameSearch.Text);

                if (ds != null)
                {
                    grdEnquiries.DataSource = ds;
                    grdEnquiries.DataBind();
                }
                else
                {
                    grdEnquiries.DataSource = null;
                    grdEnquiries.DataBind();
                    msgbox("No events found for search parameters ");
                }


            }
            catch (Exception cc)
            {

                msgbox(cc.Message);
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

        protected void grdEnquiries_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectrecord")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //Response.Redirect(string.Format("EmployerEmployeesReg?FundID=" + txtFundID.Value + "&SystemRef=" + txtParticipatingEmployerID.Value + "&MemberID={0}", index), false);

            }

        }

        protected void GetAllSavedEvents()
        {
            try
            {
                AGMEvents eve = new AGMEvents("cn", 1);
                if (eve.getAllEvents()!=null)
                {
                    grdEnquiries.DataSource = eve.getAllEvents();
                    grdEnquiries.DataBind();
                }
                else
                {
                    AmberAlert("No events");
                }
              

            }
            catch (Exception c)
            {

                RedAlert(c.Message);
            }
        }

        protected void getQueries(int EventID)
        {
            try
            {
                AGMQueries que = new AGMQueries("cn", 1);

                if (que.GetEnquiries(EventID) != null)
                {
                    grdEnquiries.DataSource = que.GetEnquiries(EventID);
                    grdEnquiries.DataBind();
                }
                else
                {
                    grdEnquiries.DataSource = null;
                    grdEnquiries.DataBind();
                    AmberAlert("There are no pending issues");
                }

            }
            catch (Exception xxx)
            {

                RedAlert(xxx.Message);
            }

        }

        protected void cboEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //txtEventID.Value = cboEvent.SelectedValue;
                getQueries(int.Parse(txtEventID.Value));
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void btnSearchVal_Click(object sender, EventArgs e)
        {
            try
            {
                AGMQueries search = new AGMQueries("cn", 1);
                search.getEnquiriesByFilterSearch();
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }
    }
}