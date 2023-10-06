using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.QnAs
{
    public partial class QnASubmissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                getAllPendingQuestions();
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
        protected void getAllPendingQuestions()
        {
            try
            {
                QnAQuestions q = new QnAQuestions("cn", 1);
                DataSet ds = q.getSavedQuestionsForParticipantsPendingResponse();
                if (ds != null)
                {

                    grdCheckin.DataSource = ds;
                    grdCheckin.DataBind();

                }
                else
                {
                    grdCheckin.DataSource = null;
                    grdCheckin.DataBind();
                }

            }
            catch (Exception ex)
            {
                msgbox(ex.Message);
            }
        }
        protected void btnRespond_Click(object sender, EventArgs e)
        {
            try
            {
                QnAQuestions q = new QnAQuestions("cn", 1);
                if (q.UpdateResponse(int.Parse(txtQID.Value), txtResponse.Text.ToUpper()))
                {
                    msgbox("Question responded to");
                    getAllPendingQuestions();
                    lblQuestion.Text = "";
                    lblYouName.Text = "";
                    LBLSubmissionTime.Text = "";
                    txtResponse.Text = "";
                    txtQID.Value = "0";
                }
                else
                {
                    msgbox(q.MsgFlg);
                }
            }
            catch (Exception ex)
            {
                msgbox(ex.Message);
            }
        }

        protected void grdCheckin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdCheckin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "selectRecord")
                {
                    try
                    {

                        int index = int.Parse(e.CommandArgument.ToString());
                        QnAQuestions q = new QnAQuestions("cn", 1);
                        DataSet ds = q.getSelectedQuestion(index);
                        if (ds != null)
                        {
                            DataRow rw = ds.Tables[0].Rows[0];
                            txtQID.Value = rw["id"].ToString();
                            lblQuestion.Text = rw["Question"].ToString();
                            LBLSubmissionTime.Text = rw["DateCreated"].ToString();
                            lblYouName.Text = rw["Qname"].ToString() + " " + rw["Company"].ToString();
                            txtResponse.Text = "";


                        }


                    }
                    catch (Exception xc)
                    {

                        msgbox(xc.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                msgbox(ex.Message);
            }
        }
    }
}