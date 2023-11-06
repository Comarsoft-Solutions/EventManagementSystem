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
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services.Description;

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

        #region methods

        protected void UploadFile( int ID, string filename, string filePath)
        {
            try
            {
                if (flRsvpUpload.HasFiles)
                {

                                       
                    string contentType = flRsvpUpload.PostedFile.ContentType;

                    using (Stream fs = flRsvpUpload.PostedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr))
                            {
                                string query = "insert into EmailAttachments([BroadcastMessagesListID],[FileName],[FilePath]) values(@BroadcastMessagesListID,@FileName,@FilePath)";
                                using (SqlCommand cmd = new SqlCommand(query))
                                {
                                    cmd.Connection = con;
                                    cmd.Parameters.Add("@BroadcastMessagesListID", SqlDbType.Int).Value = ID;
                                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = filename;
                                    //cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = txtPhoneNumber.Text;
                                    cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = filePath;
                                    //cmd.Parameters.Add("@Attachments", SqlDbType.VarBinary).Value = bytes;

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    //SuccessAlert("member RSVP Successfully");
                                    //GetLogisticsCombos();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception cv)
            {

                RedAlert(cv.Message);
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
        private string PopulateBody(int MemberID,string Template)
        {
            string memberName = string.Empty;
            RegistrationSave reg = new RegistrationSave("cn", 1);
            if (reg.Retrieve(MemberID))
            {
                memberName = reg.FirstName + " " + reg.LastName;
            }

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/communication/Templates/" + Template)))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{BODY}", txtMessageBody.Text);
            body = body.Replace("{MEMBER}", memberName);
            return body;


        }
        private string PopulateBody(int MemberID)
        {
            string memberName = string.Empty;
            RegistrationSave reg = new RegistrationSave("cn", 1);
            if (reg.Retrieve(MemberID))
            {
                memberName = reg.FirstName + " " + reg.LastName;
            }

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/communication/Templates/" + cboHtmlTemplate.SelectedItem.Text)))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{MemberID}", MemberID.ToString());
            body = body.Replace("{Member}", memberName);
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

                //files
                //foreach (HttpPostedFile postedFile in flRsvpUpload.PostedFiles)
                //{
                //    if (postedFile.ContentLength > 0)
                //    {
                //        string fileName = Path.GetFileName(postedFile.FileName);
                //        string filePath = Server.MapPath("~/Attachments/" + fileName); // Save to a folder
                //        postedFile.SaveAs(filePath);

                //        // Insert file information into the database
                //        UploadFile(int.Parse(txtID.Value), fileName, filePath);

                //        // Attach the file to the email
                //        Message.Attachments.Add(new Attachment(filePath));
                //    }
                //}

                LookUp fl = new LookUp("cn", 1);
                DataSet ds = fl.GetFilePath(int.Parse(txtID.Value));
                if (ds!=null)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        string filePath = item["FilePath"].ToString();
                        Message.Attachments.Add(new Attachment(filePath));
                    }
                }


                //// Set the HTML body of the email

                body = (cboFormatType.SelectedValue == "1") ? PopulateBody(MemberId, "001_ZAPF_General_Template.html") : body = PopulateBody(MemberId);
                

                // Create an alternate view with the HTML body
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                Message.AlternateViews.Add(htmlView);
                try
                {

                    Client.Send(Message);
                SuccessAlert("Message sent");
                }
                catch (Exception e)
                {

                    WarningAlert(e.Message);
                }

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
                BroadcastMessagesList b = new BroadcastMessagesList("cn", 1);
                b.ID = int.Parse(txtID.Value);
                b.StatusID = 1;
                b.BroadcastMessgeTitle = txtHeader.Text;
                b.MessageType = cboMessageType.SelectedItem.Text;
                b.Format = int.Parse(cboFormatType.SelectedValue);
                b.Template = (cboFormatType.SelectedValue == "1") ? "001_ZAPF_General_Template.html" : cboHtmlTemplate.SelectedItem.Text;

                b.Message = txtMessageBody.Text;
                if (b.Save())
                {
                    txtID.Value = b.ID.ToString();
                    foreach (HttpPostedFile postedFile in flRsvpUpload.PostedFiles)
                    {
                        if (postedFile.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(postedFile.FileName);
                            string filePath = Server.MapPath("~/Communication/Attachments/" + fileName); // Save to a folder
                            postedFile.SaveAs(filePath);

                            // Insert file information into the database
                            UploadFile(int.Parse(txtID.Value), fileName, filePath);

                            // Attach the file to the email
                            //Message.Attachments.Add(new Attachment(filePath));
                        }
                    }
                    //UploadFile(int.Parse(txtID.Value));
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
                      //  string msgbdy = PopulateBody();
                        string msgbdy ="";
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
        #endregion

        protected void cboFormatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFormatType.SelectedValue == "1")
            {
                mess.Visible = true;
                tempp.Visible = false;
            }
            else
            {
                tempp.Visible = true;
                mess.Visible = false;
            }
        }
    }
}