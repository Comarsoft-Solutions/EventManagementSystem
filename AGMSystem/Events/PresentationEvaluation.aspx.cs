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
    public partial class PresentationEvaluation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                GetRatings();
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

        protected void AmberAlert(string MsgFlg)
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
                    txtEvents.DataSource = ds;
                    txtEvents.DataValueField = "ID";
                    txtEvents.DataTextField = "EventName";
                    txtEvents.DataBind();
                    txtEvents.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No AGMS found", "0");
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
        private void GetRatings()
        {
            LookUp lk = new LookUp("cn",1);
            DataSet ds = lk.GetRatings();
            if (ds != null)
            {
                ListItem li = new ListItem("Select Rating","0");

                cbo1.DataSource = ds;
                cbo1.DataTextField = "Rating";
                cbo1.DataValueField = "ID";
                cbo1.DataBind();
                cbo1.Items.Insert(0,li);

                cbo2.DataSource = ds;
                cbo2.DataTextField = "Rating";
                cbo2.DataValueField = "ID";
                cbo2.DataBind();
                cbo2.Items.Insert(0,li);

                cbo3.DataSource = ds;
                cbo3.DataTextField = "Rating";
                cbo3.DataValueField = "ID";
                cbo3.DataBind();
                cbo3.Items.Insert(0,li);

                cbo4.DataSource = ds;
                cbo4.DataTextField = "Rating";
                cbo4.DataValueField = "ID";
                cbo4.DataBind();
                cbo4.Items.Insert(0,li);

                cbo5.DataSource = ds;
                cbo5.DataTextField = "Rating";
                cbo5.DataValueField = "ID";
                cbo5.DataBind();
                cbo5.Items.Insert(0,li);

                cbo6.DataSource = ds;
                cbo6.DataTextField = "Rating";
                cbo6.DataValueField = "ID";
                cbo6.DataBind();
                cbo6.Items.Insert(0,li);

                cbo7.DataSource = ds;
                cbo7.DataTextField = "Rating";
                cbo7.DataValueField = "ID";
                cbo7.DataBind();
                cbo7.Items.Insert(0,li);

                cbo8.DataSource = ds;
                cbo8.DataTextField = "Rating";
                cbo8.DataValueField = "ID";
                cbo8.DataBind();
                cbo8.Items.Insert(0,li);

                cbo9.DataSource = ds;
                cbo9.DataTextField = "Rating";
                cbo9.DataValueField = "ID";
                cbo9.DataBind();
                cbo9.Items.Insert(0,li);

            }
            else
            {
                ListItem li = new ListItem("No ratings found","0");

                cbo1.Items.Clear();
                cbo1.DataSource = null;
                cbo1.DataBind();
                cbo1 .Items.Insert(0,li);

                cbo2.Items.Clear();
                cbo2.DataSource = null;
                cbo2.DataBind();
                cbo2 .Items.Insert(0,li);

                cbo3.Items.Clear();
                cbo3.DataSource = null;
                cbo3.DataBind();
                cbo3 .Items.Insert(0,li);

                cbo4.Items.Clear();
                cbo4.DataSource = null;
                cbo4.DataBind();
                cbo4 .Items.Insert(0,li);

                cbo5.Items.Clear();
                cbo5.DataSource = null;
                cbo5.DataBind();
                cbo5 .Items.Insert(0,li);

                cbo6.Items.Clear();
                cbo6.DataSource = null;
                cbo6.DataBind();
                cbo6 .Items.Insert(0,li);

                cbo7.Items.Clear();
                cbo7.DataSource = null;
                cbo7.DataBind();
                cbo7 .Items.Insert(0,li);

                cbo8.Items.Clear();
                cbo8.DataSource = null;
                cbo8.DataBind();
                cbo8 .Items.Insert(0,li);

                cbo9.Items.Clear();
                cbo9.DataSource = null;
                cbo9.DataBind();
                cbo9 .Items.Insert(0,li);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LookUp lk = new LookUp("cn", 1);
            DataSet ds = lk.GetPresenterQuestions();
            int qn = 0;
            int Rating = 0;
            string comment = "";
            foreach (DataRow ds2 in ds.Tables[0].Rows)
            {
                qn = int.Parse(ds2["ID"].ToString());
                if(qn == 1)
                {
                    Rating = int.Parse(cbo1.SelectedValue);
                    comment = txt1.Text;
                }
                if(qn == 2)
                {
                    Rating = int.Parse(cbo2.SelectedValue);
                    comment = txt2.Text;
                }
                if(qn == 3)
                {
                    Rating = int.Parse(cbo3.SelectedValue);
                    comment = txt3.Text;
                }
                if(qn == 4)
                {
                    Rating = int.Parse(cbo4.SelectedValue);
                    comment = txt4.Text;
                }
                if(qn == 5)
                {
                    Rating = int.Parse(cbo5.SelectedValue);
                    comment = txt5.Text;
                }
                if(qn == 6)
                {
                    Rating = int.Parse(cbo6.SelectedValue);
                    comment = txt6.Text;
                }
                if(qn == 7)
                {
                    Rating = int.Parse(cbo7.SelectedValue);
                    comment = txt7.Text;
                }
                if(qn == 8)
                {
                    Rating = int.Parse(cbo8.SelectedValue);
                    comment = txt8.Text;
                }
                if(qn == 9)
                {
                    Rating = int.Parse(cbo9.SelectedValue);
                    comment = txt9.Text;
                }

                try
                {
                    //insert
                    lk.InsertPresenterEvaluation(int.Parse(txtMemberID.Value), qn, Rating, comment,int.Parse(txtEvents.SelectedValue));
                    SuccessAlert("Responses Submitted");
                }
                catch (Exception x)
                {

                    RedAlert(x.Message);
                }

            }   
            
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RegistrationSave reg = new RegistrationSave("cn", 1);
            DataSet ds = reg.GetRegInfo(txtFullName.Text);
            if (ds!=null)
            {
                DataRow rw = ds.Tables[0].Rows[0];
                txtMemberID.Value = rw["ID"].ToString();
                txtname.Value = rw["FirstName"].ToString();
                SuccessAlert(txtname.Value + "Found");
            }
            else
            {
                AmberAlert("Not Found");
            }
        }

        protected void txtPresenter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}