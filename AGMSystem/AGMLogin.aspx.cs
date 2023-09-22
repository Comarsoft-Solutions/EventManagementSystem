using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class AGMLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            loginMember();

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
        protected void loginMember()
        {
            try
            {
                logins log = new logins("cn",1);
                AGMAccessUsers agm = new AGMAccessUsers("cn", 1);
                if(agm.ValidateUserLogin(txtUsername.Text, txtPassword.Text))
                {
                    Session["ID"] = agm.SystemRef;
                    Session["LoginCode"] = agm.Code;

                    if (agm.RoleType != null)
                    {

                        log.UserAction = "Logged In";
                        log.Message = agm.MsgFlg;
                        log.UserName = txtUsername.Text;
                        log.IpAddress = HttpContext.Current.Request.UserHostAddress;
                        log.Save();
                        //Response.Redirect("Dashboard?SystemRef=" + agm.SystemRef + "");
                        Response.Redirect("Dashboard");

                    }
                    SuccessAlert("Success");

                }
                else
                {
                    log.UserAction = "Attempted Login";
                    log.Message = agm.MsgFlg;
                    log.UserName = txtUsername.Text;
                    log.IpAddress = HttpContext.Current.Request.UserHostAddress;
                    log.Save();
                    AmberAlert("Failed to Login");
                }
            }
            catch (Exception ex)
            {
                //logins log = new logins("cn", 1);
                //log.UserAction = "Attempted Login";
                //log.Message = log.MsgFlg;
                //log.UserName = txtUsername.Text;
                //log.IpAddress = HttpContext.Current.Request.UserHostAddress;
                //log.Save();
                RedAlert(ex.Message);
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
    }
}