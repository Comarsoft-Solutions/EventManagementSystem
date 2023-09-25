using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGMSystem.models;

namespace AGMSystem.communication
{
    public partial class BulkMessaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFundID.Value = "0";
                    txtID.Value = "0";

                    if (Request.QueryString["FundID"] != null)
                    {
                        txtFundID.Value = Request.QueryString["FundID"].ToString();

                    }
                    PopulateFileList();
                    getMessageTypes();
                    getMessageFormats();
                }

            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }


        protected void getMessageTypes()
        {
            try
            {
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                if (b.getMessageTypes() != null)
                {
                    ListItem li = new ListItem("Select a broadcast message type", "0");
                    cboMessageType.DataSource = b.getMessageTypes();
                    cboMessageType.DataTextField = "MessageTypes";
                    cboMessageType.DataValueField = "ID";
                    cboMessageType.DataBind();
                    cboMessageType.Items.Insert(0, li);
                }
                else
                {
                    ListItem li = new ListItem("Hakuna maMessage types ari defined", "0");
                    cboMessageType.DataSource = null;
                    cboMessageType.DataBind();
                    cboMessageType.Items.Insert(0, li);
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }
        protected void RedAlert(string MsgFlg)
        {
            lblComms.Text = "An Error occured: " + MsgFlg;
            pnlComms.BackColor = System.Drawing.Color.Red;
        }

        protected void AmberAlert(string MsgFlg)
        {
            lblComms.Text = "Warning: " + MsgFlg;
            pnlComms.BackColor = System.Drawing.Color.Orange;
        }

        protected void SuccessAlert(string MsgFlg)
        {
            lblComms.Text = "Success: " + MsgFlg;
            pnlComms.BackColor = System.Drawing.Color.Green;
        }


        private string PopulateBody()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/communication/Templates/" + cboHtmlTemplate.SelectedItem.Text)))
            {
                body = reader.ReadToEnd();
            }
            //body = body.Replace("{Pensioner}", Pensioner);
            //body = body.Replace("{PensionNo}", PensionNo.ToString());
            //body = body.Replace("{Subject}", Subject);
            //body = body.Replace("{Fund}", Fund);
            //body = body.Replace("{Description}", MsgBody);
            return body;




        }



        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body, string MessageBody, int MemberId)
        {

            try
            {

                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
                var AccessLink = " https://www.comartononline.com/AGMSystem/Registration/PortalRegistration";

                SmtpClient Client = new SmtpClient()
                {
                    Credentials = new NetworkCredential("training@zapf.co.zw", "Fuq97442"),
                    Port = 587,
                    Host = "smtp.office365.com",
                    EnableSsl = true,
                };

             

                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("training@zapf.co.zw", "ZAPF");
                Message.To.Add(recepientEmail);
                Message.Subject = subject;
                Message.IsBodyHtml = true;



                //// Set the HTML body of the email
                //body = $@"<html>
                //    <body>

                //        <p>Good Day,</p>
                //        <p>{MessageBody}</p>
                //        <p>Click Link to register,{AccessLink}</p>
                //        <p>Choose the event and enter your Identity Number to register.</p>
                //        <p>Regards,</p>
                //        <p>Comarton Consultants </p>
                //    </body>
                //</html>";

                body = PopulateBody();

                // Create an alternate view with the HTML body
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                Message.AlternateViews.Add(htmlView);

                Client.Send(Message);
                SuccessAlert("Message sent");

                BroadcastMessagesList bc = new BroadcastMessagesList("cn", 1);
                if (bc.UpdateEmailListStatus(int.Parse(txtID.Value), MemberId))
                {

                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
                BroadcastMessagesList bc = new BroadcastMessagesList("cn", 1);
                if (bc.UpdateEmailListStatusFailed(int.Parse(txtID.Value), MemberId))
                {

                }
            }
        }
        private void PopulateFileList()
        {
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/communication/Templates"));
            FileInfo[] fis = di.GetFiles();
            cboHtmlTemplate.DataSource = fis;
            cboHtmlTemplate.DataTextField = "Name";
            cboHtmlTemplate.DataValueField = "Name";
            cboHtmlTemplate.DataBind();
            cboHtmlTemplate.Items.Insert(0, new ListItem("Select a message template", "0"));
        }

        

        protected void lnkPersonalandPaymentDetails_Click(object sender, EventArgs e)
        {

        }
        protected void getMessageFormats()
        {
            try
            {
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                if (b.getMessageFormatTypes() != null)
                {
                    ListItem li = new ListItem("Select a message format type", "0");
                    cboFormatType.DataSource = b.getMessageFormatTypes();
                    cboFormatType.DataValueField = "ID";
                    cboFormatType.DataTextField = "MessageFormatTypes";
                    cboFormatType.DataBind();
                    cboFormatType.Items.Insert(0, li);

                }
                else
                {
                    ListItem li = new ListItem("Hakuna ma Message Format Types", "0");
                    cboFormatType.DataSource = null;
                    cboFormatType.DataBind();
                    cboFormatType.Items.Insert(0, li);
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMessageType.SelectedValue == "0")
                {
                    RedAlert("Please select a valid broadcast message type");
                    return;
                }
                if (cboMessageType.SelectedItem.Text.Contains("Email"))
                {
                    if (txtHeader.Text == "" || txtHeader.Text.Length <= 2)
                    {
                        RedAlert("Please enter a valid Email Subject");
                        return;
                    }
                }
                if (cboFormatType.SelectedItem.Text.Contains("HTML"))
                {
                    if (cboHtmlTemplate.SelectedValue == "0")
                    {
                        RedAlert("Please select a valid HTML template");
                        return;
                    }
                }
                //if (cboMessageType.SelectedItem.Text.Contains("SMS"))
                //{

                //    {
                //        Response.Redirect(string.Format("../HTMLTemplates/SendSMS?FundID={0}", txtFundID.Value));
                //    }
                //}
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                b.ID = int.Parse(txtID.Value);
                b.StatusID = 1;
                b.BroadcastMessgeTitle = txtHeader.Text;
                b.MessageType = cboMessageType.SelectedItem.Text;
                if (b.Save())
                {
                    txtID.Value = b.ID.ToString();
                    //We get all pensioners not yet assigned to the created list
                    getUnassignedContactstoTheMessage(int.Parse(txtID.Value));
                    getassignedContactstoTheMessage(int.Parse(txtID.Value));
                    SuccessAlert("Broadcast Message Created, add contacts to the list below and send out your message");
                }
                else
                {
                    RedAlert(b.MsgFlg);
                }

            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void getUnassignedContactstoTheMessage(int BroadcastList)
        {
            try
            {
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                if (b.getUnassignedContacts(BroadcastList) != null)
                {
                    lstUnassigned.DataSource = b.getUnassignedContacts(BroadcastList);
                    lstUnassigned.DataValueField = "ID";
                    lstUnassigned.DataTextField = "Member";
                    lstUnassigned.DataBind();

                }
                else
                {
                    lstUnassigned.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void getassignedContactstoTheMessage(int BroadcastList)
        {
            try
            {
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                if (b.getassignedContacts(BroadcastList) != null)
                {
                    lstMailingList.DataSource = b.getassignedContacts(BroadcastList);
                    lstMailingList.DataValueField = "ID";
                    lstMailingList.DataTextField = "Member";
                    lstMailingList.DataBind();

                }
                else
                {
                    lstMailingList.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BroadcastContacts b = new BroadcastContacts("cn", 1);
                if (int.Parse(txtID.Value) > 0)
                {
                    List<int> lipen = new List<int>();
                    foreach (ListItem item in lstUnassigned.Items)
                    {
                        if (item.Selected == true)
                        {
                            b.ID = 0;
                            b.BroadcastListID = int.Parse(txtID.Value);
                            b.MemberID = int.Parse(item.Value);
                            b.EmailAddress = item.Text;
                            b.MobileNo = item.Text;
                            b.StatusID = 1;
                            b.Save();
                        }
                    }
                    getUnassignedContactstoTheMessage(int.Parse(txtID.Value));
                    getassignedContactstoTheMessage(int.Parse(txtID.Value));

                    SuccessAlert("Contacts added to the sending list");
                }
                else
                {
                    RedAlert("Save a valid broadcast message first to enable the adding of pensioners to messaging list");
                    return;

                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            try
            {
                BroadcastContacts b = new BroadcastContacts("cn", 1);
                if (b.getBroadCastContactDetails(int.Parse(txtID.Value)) != null)
                {
                    foreach (DataRow rw in b.getBroadCastContactDetails(int.Parse(txtID.Value)).Tables[0].Rows)
                    {
                        string msgbdy = PopulateBody();
                        //string msgbdy = "";
                        SendHtmlFormattedEmail(rw["Email"].ToString(), txtHeader.Text, msgbdy, txtMessageBody.Text, int.Parse(rw["MemberID"].ToString()));

                    }


                }


            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void lnkEmails_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommunicationsCenter?FundID=" + txtFundID.Value + "");
        }

        protected void lnSMS_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendSMS?FundID=" + txtFundID.Value + "");
        }

        protected void lnkreports_Click(object sender, EventArgs e)
        {
            Response.Redirect("CommunicationReports?FundID=" + txtFundID.Value + "");
        }
    }
}