using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Events
{
    public partial class MemberEvaluations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                if (Request.QueryString["memberID"] != null)
                {
                    txtmemberID.Value = Request.QueryString["memberID"].ToString();
                }
                else
                {
                    txtmemberID.Value = "0";
                }
                getEventEvaluations();
                getPresentationEvaluations();
                getMemberDetails();
            }
        }

        private void getMemberDetails()
        {
            RegistrationSave reg = new RegistrationSave("cn",1);
            DataSet ds = new DataSet();
            if (reg.Retrieve(int.Parse(txtmemberID.Value)))
            {
                txtFirstName.Text = reg.FirstName;
                txtLastName.Text = reg.LastName;
                txtCompany.Text = reg.PensionFund;
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

        

        private void getPresentationEvaluations()
        {
            LookUp lk = new LookUp("cn",1);
            DataSet ds = lk.GetMemberEventEvaluations(int.Parse(txtmemberID.Value));
            if (ds!=null)
            {
                grdEventEvaluations.DataSource = ds;
                grdEventEvaluations.DataBind();
            }
            else
            {
                grdEventEvaluations.DataSource=null;
                grdEventEvaluations.DataBind();
                AmberAlert("No event evaluations Found");
            }
        }

        private void getEventEvaluations()
        {
            LookUp lk = new LookUp("cn", 1);
            DataSet ds = lk.GetMemberPresentationtEvaluations(int.Parse(txtmemberID.Value));
            if(ds!=null)
            {
                grdPresentationEvaluation.DataSource = ds;
                grdPresentationEvaluation.DataBind();
            }
            else
            {
                grdPresentationEvaluation.DataSource=null;
                grdPresentationEvaluation.DataBind();
                AmberAlert("No Presentation Evaluations Found");
            }
        }
    }
}