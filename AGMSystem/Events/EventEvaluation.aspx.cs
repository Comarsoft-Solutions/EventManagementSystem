using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace AGMSystem.Events
{
    public partial class EventEvaluation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                if (Request.QueryString["eventID"]!=null)
                {
                    txtEventID.Value = Request.QueryString["EventID"];
                }
                else
                {
                    txtEventID.Value = "0";
                }
                if (Request.QueryString["memberID"]!=null)
                {
                    txtMemberID.Value = Request.QueryString["memberID"];
                }
                else
                {
                    txtMemberID.Value="0";
                }
                getRegInfo();
                GetEventName();
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

        protected void AmberAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Warning!', '" + MsgFlg + "', 'warning');", true);

        }

        protected void SuccessAlert(string MsgFlg)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + MsgFlg + "', 'success');", true);

        }
        #endregion

        //private void getEvents()
        //{

        //    try
        //    {
        //        AGMEvents agm = new AGMEvents("cn", 1);
        //        DataSet ds = agm.getAllEvents();
        //        if (ds != null)
        //        {
        //            ListItem listItem = new ListItem("Select Event", "0");
        //            txtEvents.DataSource = ds;
        //            txtEvents.DataValueField = "ID";
        //            txtEvents.DataTextField = "EventName";
        //            txtEvents.DataBind();
        //            txtEvents.Items.Insert(0, listItem);
        //        }
        //        else
        //        {
        //            ListItem li = new ListItem("No Events found", "0");
        //            txtEvents.Items.Clear();
        //            txtEvents.DataSource = null;
        //            txtEvents.DataBind();
        //            txtEvents.Items.Insert(0, li);
        //        }
        //    }
        //    catch (Exception a)
        //    {

        //        RedAlert(a.Message);
        //    }
        //}
        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    RegistrationSave reg = new RegistrationSave("cn", 1);
        //    DataSet ds = reg.GetRegInfo(txtFullName.Text);
        //    if (ds != null)
        //    {
        //        DataRow rw = ds.Tables[0].Rows[0];
        //        txtMemberID.Value = rw["ID"].ToString();
        //        txtname.Value = rw["FirstName"].ToString();
        //        SuccessAlert(txtname.Value + "Found");
        //    }
        //    else
        //    {
        //        AmberAlert("Not Found");
        //    }
        //}
        protected void getRegInfo()
        {
            RegistrationSave reg = new RegistrationSave("cn", 1);
            if (reg.Retrieve(int.Parse(txtMemberID.Value)))
            {
                txtname.Value = reg.FirstName;
            }
            else
            {
                AmberAlert("Member Not Found");
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LookUp lk = new LookUp("cn", 1);
            DataSet ds = lk.GetEventQuestions();
            DataSet da = lk.CheckEventValidations(int.Parse(txtEventID.Value), int.Parse(txtMemberID.Value));
            if (da==null)
            {
                int qn = 0;
                string comment = "";
                foreach (DataRow ds2 in ds.Tables[0].Rows)
                {
                    qn = int.Parse(ds2["ID"].ToString());
                    if (qn == 1)
                    {
                        comment = txt1.Text;
                    }
                    if (qn == 2)
                    {
                        comment = txt2.Text;
                    }
                    if (qn == 3)
                    {
                        comment = txt3.Text;
                    }
                    if (qn == 4)
                    {
                        comment = txt4.Text;
                    }
                    if (qn == 5)
                    {
                        comment = txt5.Text;
                    }

                    try
                    {
                        //insert
                        lk.InsertEventEvaluation(int.Parse(txtMemberID.Value), qn, comment, int.Parse(txtEventID.Value));
                        SuccessAlert("Responses Submitted");
                    }
                    catch (Exception x)
                    {
                        ClearForm();
                        RedAlert(x.Message);
                    }

                }
            }
            else
            {
                AmberAlert("Already Answered");
            }
            

            Session["memberID"] = txtMemberID.Value;
            Session["eventID"] = txtEventID.Value;
            Response.Redirect("PresentationEvaluation?eventID=" + txtEventID.Value + "&memberID=" + txtMemberID.Value + "");

            ClearForm();
        }

        private void ClearForm()
        {
            txt1.Text = string.Empty;
            txt2.Text = string.Empty;
            txt3.Text = string.Empty;
            txt4.Text = string.Empty;
            txt5.Text = string.Empty;
            txtEventName.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtOrganiser.Text = string.Empty;
            //txtFullName.Text = string.Empty;
        }

        protected void GetEventName()
        {
            AGMEvents eve = new AGMEvents("cn", 1);
            DataSet ds = eve.GetEventName(int.Parse(txtEventID.Value));
            if (ds != null)
            {
                DataRow rw = ds.Tables[0].Rows[0];
                pnlEventDetails.Visible = true;
                txtEventName.Text = rw["EventName"].ToString();
                txtDate.Text= rw["StartDate"].ToString() + " to " + rw["EndDate"].ToString();
            }
            else
            {
                AmberAlert("Oops");
            }
        }
    }
}