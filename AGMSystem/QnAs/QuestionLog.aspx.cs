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
    public partial class QuestionLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtRecID.Value = "0";
                txtQID.Value = "0";
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
        protected void btnSaveName_Click(object sender, EventArgs e)
        {
            try
            {
                QnAPariicipants q = new QnAPariicipants("cn", 1);
                if (q.ValidateExistanceOfName(txtQName.Text, txtYourCompany.Text) != null)
                {
                    DataRow rw = q.ValidateExistanceOfName(txtQName.Text, txtYourCompany.Text).Tables[0].Rows[0];
                    if (rw != null)
                    {
                        txtRecID.Value = rw["ID"].ToString();
                        txtQName.Text = rw["QName"].ToString();
                        txtYourCompany.Text = rw["Company"].ToString();
                        lblYouName.Text = rw["QNAME"].ToString() + " " + rw["Company"].ToString();
                        msgbox("Please enter your next question");
                        getMyQuestions(int.Parse(txtRecID.Value));

                    }

                }
                else
                {
                    q.ID = int.Parse(txtRecID.Value);
                    q.QName = txtQName.Text.ToUpper();
                    q.Company = txtYourCompany.Text.ToUpper();
                    if (q.Save())
                    {
                        txtRecID.Value = q.ID.ToString();
                        if (q.Retrieve(q.ID))
                        {
                            lblYouName.Text = q.QName.ToUpper() + " " + q.Company;
                        }
                        msgbox("Your name has been saved, please enter your question below:");
                    }
                    else
                    {
                        msgbox("There was an error saving your name: " + q.MsgFlg);
                    }
                }
            }
            catch (Exception ex)
            {
                msgbox(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRecID.Value == "" || int.Parse(txtRecID.Value) <= 0)
                {
                    msgbox("Save your name and company details first");
                }
                else
                {
                    QnAQuestions q = new QnAQuestions("cn", 1);
                    q.ID = int.Parse(txtQID.Value);
                    q.QnAParticipantID = int.Parse(txtRecID.Value);
                    q.Question = txtYourQuestion.Text.ToUpper();
                    q.ResponseStatusID = 1;
                    if (q.Save())
                    {
                        msgbox("Your Question has been submitted, please wait for your response");
                        txtYourQuestion.Text = "";
                        txtQID.Value = "0";
                        getMyQuestions(int.Parse(txtRecID.Value));
                    }
                    else
                    {
                        msgbox(q.MsgFlg);
                    }

                }
            }
            catch (Exception ex)
            {
                msgbox(ex.Message);
            }
        }

        protected void getMyQuestions(int MyID)
        {
            try
            {
                QnAQuestions q = new QnAQuestions("cn", 1);
                if (q.getSavedQuestionsForParticipants(MyID) != null)
                {
                    grdCheckin.DataSource = q.getSavedQuestionsForParticipants(MyID);
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
                        if (q.Retrieve(index))
                        {
                            lblQues.Text = q.Question;
                            lblResponse.Text = q.AnswerResponse.ToString();
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