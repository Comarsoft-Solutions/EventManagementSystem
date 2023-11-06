using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Net.Mime;
using AGMSystem.models;

namespace AGMSystem.Membership
{
    public partial class BulkUploads : System.Web.UI.Page
    {
        public static DataSet dsuld = new DataSet();
        public static DataSet MatchedAccounts;
        public static DataSet NewAccounts;
        public static DataSet UnmacthedTrades;
        static DataSet dsReady = new DataSet();
        static DataSet dsErrored = new DataSet();
        //private cn db = new cn();
        public SqlConnection myConnection = new SqlConnection();
        public SqlDataAdapter adp;
        public SqlCommand cmd;
        double countSuccess = 0;
        double SumSuccess = 0;
        double SumFailed = 0;
        double countFailed = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtFundID.Value = "0";
                txtSystemRef.Value = "0";
                txtbatchID.Value = "0";

                if (Request.QueryString["FundID"] != null)
                {
                    txtFundID.Value = Request.QueryString["FundID"].ToString();
                }
                if (Request.QueryString["SystemRef"] != null)
                {
                    txtSystemRef.Value = Request.QueryString["SystemRef"].ToString();
                    Session["SystemRef"] = Request.QueryString["SystemRef"].ToString();

                    //fs.Attributes["href"] = (string.Format("SingleMemberUpload?SystemRef={0}", Session["SystemRef"]));
                }
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
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFile();
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void UploadFile()
        {
            try
            {
                dsuld.Clear();
                pnlComms.BackColor = System.Drawing.Color.Transparent;
                lblComms.Text = "";
                if ((flContributionsUpload.HasFile))
                {
                }
                else
                {
                    WarningAlert("Please select a file for upload");
                    return;
                }

                //Upload and save the file
                string csvPath = Server.MapPath("~/FileUploads/") + Path.GetFileName(flContributionsUpload.PostedFile.FileName);
                string finename = Path.GetFileName(flContributionsUpload.PostedFile.FileName);
                txtFileName.Text = finename;
                flContributionsUpload.SaveAs(csvPath);

                string filePath = "FileUploads/" + finename;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                //Dim stream As FileStream = File.Open(csvPath, FileMode.Open, FileAccess.Read)
                txtFilePath.Text = csvPath;
                ExcelPackage pckage = new ExcelPackage(new FileInfo(csvPath));
                //ExcelWorksheet wksheets = default(ExcelWorksheet);
                List<string> wkBks = new List<string>();
                foreach (ExcelWorksheet wksheets in pckage.Workbook.Worksheets)
                {
                    wkBks.Add(wksheets.Name);
                }
                lstWrkSheets.DataSource = wkBks;
                lstWrkSheets.DataBind();
                if ((lstWrkSheets.Items.Count > 0))
                {
                    //lblComms.Text = "File Uploaded, select a worksheet to continue";
                    //lblComms.Text = "select a worksheet to continue";
                    //pnlComms.BackColor = System.Drawing.Color.Green;

                }
                else
                {
                    RedAlert("There was a problem reading the worksheets of the file");
                }


                if (lstWrkSheets.Items.Count >= 1)
                {
                    lstWrkSheets.Visible = true;
                    lblWrkSheetPrompt.Visible = true;
                }
                else
                {
                    lblWrkSheetPrompt.Visible = false;
                    lstWrkSheets.Visible = false;
                }
                return;
            }
            catch (Exception ex)
            {
                lblComms.Text = ex.Message;
                pnlComms.BackColor = System.Drawing.Color.Red;
            }
        }

        protected void lstWrkSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadExcelFiletoDataset(txtFilePath.Text, lstWrkSheets.SelectedItem.Text, txtFileName.Text);
        }
        protected void ReadExcelFiletoDataset(string FilePath, string wrkSheet, string FileName)
        {
            try
            {
                dsuld.Clear();

                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                string con = "";
                if ((Path.GetExtension(FilePath) == ".xls"))
                {
                    con = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + FilePath + "';Extended Properties='Excel 8.0;HDR=YES;'";
                }
                else if ((Path.GetExtension(FilePath) == ".xlsx"))
                {
                    con = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0;HDR=YES;'";
                }

                OleDbConnection olecon = default(OleDbConnection);
                OleDbCommand olecomm = default(OleDbCommand);

                OleDbDataAdapter oleadpt = default(OleDbDataAdapter);

                olecon = new OleDbConnection();
                olecon.ConnectionString = con;
                olecomm = new OleDbCommand();
                olecomm.CommandText = "Select * from [" + wrkSheet + "$" + "]";
                olecomm.Connection = olecon;

                oleadpt = new OleDbDataAdapter(olecomm);
                ds = new DataSet();
                olecon.Open();
                oleadpt.Fill(ds, wrkSheet + "$");

                if ((ds != null) && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dsuld = ds;
                    ProcessUploadData(dsuld, FileName);
                    olecon.Close();

                }
                else
                {
                    dsuld.Clear();
                }
                olecon.Close();


            }
            catch (Exception ex)
            {
                RedAlert( ex.Message);
                return;
            }
        }
        protected void ProcessUploadData(DataSet Ds, string FileUploadName)
        {
            try
            {
                long BatchUploadID = 0;
                List<string> CountSuccessArray = new List<string>();
                List<string> CountFailedArray = new List<string>();
                if ((dsReady != null))
                {
                    //We Create BatchUpload Record and Get Batch ID
                    //
                    string DatesCreated = DateTime.Now.ToString("yyyy-MM-dd");
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                    cmd = new SqlCommand("insert into EmployeesUploadBatch(BulkUploadFile,DateCreated,UploadStatus) values('" + FileUploadName + "','" + DatesCreated + "','1')", myConnection);

                    if ((myConnection.State == ConnectionState.Open))
                        myConnection.Close();
                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();


                    LookUp ebu = new LookUp("cn", 1);
                    DataSet y = ebu.GetUploadedEmployee();

                    foreach (DataRow rwset in y.Tables[0].Rows)
                    {
                        txtbatchID.Value = rwset["ID"].ToString();

                    }
                    DataSet x = null;
                    DataSet getFailed = null;

                    LookUp f = new LookUp("cn", 1);
                    //string RegNo = f.getRegNo(long.Parse(txtFundID.Value));
                    //string Regname = f.getRegName(long.Parse(txtFundID.Value));
                    int userid = int.Parse(txtSystemRef.Value);
                    foreach (DataRow rw in Ds.Tables[0].Rows)
                    {
                        

                        if (!rw[2].ToString().IsNullOrWhiteSpace())
                        {
                            if (rw[2].ToString().Trim() != "")
                            {
                                rw[2] = rw[2].ToString().Replace("-", "").Replace(" ", "");
                            }
                            if (rw[2].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"National ID is missing for LastName {rw[1]} ,FirstName: {rw[0]} ";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated) values('0','" + Msg + "','" + dt + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                string nationalID = rw[2].ToString();

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(nationalID);
                                }
                                pnlerror.Visible = true;
                                continue;

                            }
                            else if (rw[0].ToString() == "" || rw[1].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Specify both first name and surname for NationalID {rw[2]} of Company: {rw[5]}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated) values('" + rw[2].ToString() + "','" + Msg + "','" + dt + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                string nationalID = rw[2].ToString();

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(nationalID);
                                }
                                pnlerror.Visible = true;
                                continue;
                            }
                            else
                            {
                                if (f.ValidateMemberIDNumber(rw[2].ToString()))
                                {
                                    string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                    string Msg = $"Duplicate National Identity number: {rw[2]}. ID already exists for member: {rw[0]} {rw[1]}";
                                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                    cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated) values('" + rw[2].ToString() + "','" + Msg + "','" + dt + "')", myConnection);
                                    if ((myConnection.State == ConnectionState.Open))
                                        myConnection.Close();
                                    myConnection.Open();
                                    cmd.ExecuteNonQuery();
                                    myConnection.Close();

                                    string nationalID = rw[2].ToString();

                                    for (int runs = 0; runs < 1; runs++)
                                    {
                                        CountFailedArray.Add(nationalID);
                                    }
                                    pnlerror.Visible = true;
                                    continue;
                                }
                                else
                                {
                                    //Insert into new table ClientUploads
                                    int AddNewClientRecord = 1;
                                    int AddNewMemberRecord = 1;

                                    pnlClientsView.Visible = true;
                                    DateTime DC = DateTime.Now;
                                    string DCs = DC.ToString("yyyy-MM-dd");
                                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                    cmd = new SqlCommand("insert into ClientsFileUpload(Company,Surname,FirstName,NationalID,DateOfUpload,UploadBatchID,ProcessStatusID,addnewclientrecord,addnewmeberrecord,PensionFund,Email) values('" + rw[5].ToString() + "','" + rw[1].ToString() + "','" + rw[0].ToString() + "','" + rw[2].ToString() + "','" + DCs + "','" + Convert.ToInt32(txtbatchID.Value) + "','" + false + "','" + AddNewClientRecord + "','" + AddNewMemberRecord + "','" + rw[3].ToString() + "','" + rw[4].ToString() +"')", myConnection);
                                    if ((myConnection.State == ConnectionState.Open))
                                        myConnection.Close();
                                    myConnection.Open();
                                    cmd.ExecuteNonQuery();
                                    myConnection.Close();

                                    string nationalID = (rw[2].ToString());

                                    for (int runs = 0; runs < 1; runs++)
                                    {
                                        CountSuccessArray.Add(nationalID);
                                    }
                                }
                            }

                        }
                        else
                        {
                            WarningAlert("There are null or empty required fields in your uploaded file");
                            break;
                        }

                    }
                    string[] totalSucessarray = CountSuccessArray.ToArray();
                    countSuccess = totalSucessarray.Count();

                    string[] totalFailedarray = CountFailedArray.ToArray();
                    countFailed = totalFailedarray.Count();

                    if (totalFailedarray.Count() > 0)
                    {
                        WarningAlert( "Please check reason below for failed upload reason. Click reset to upload again");
                    }


                    lblCurrentMembers.Text = "Success Members: " + countSuccess.ToString("##,###,###.##");
                    lblFailedUploads.Text = "Failed Members: " + countFailed.ToString("##,###,###.##");

                    BatchUploadID = long.Parse(txtbatchID.Value);
                    x = f.getclientuploaded(BatchUploadID);
                    getFailed = f.GetFailedMemberUploads();

                    if (x != null)
                    {
                        grdClientsView.DataSource = x;
                        grdClientsView.DataBind();

                        grdClientsView.Visible = true;
                        pnlClientsView.Visible = true;
                        pnlerror.Visible = false;
                    }
                    else
                    {
                        grdClientsView.DataSource = null;
                        grdClientsView.DataBind();
                        grdClientsView.Visible = false;
                        pnlClientsView.Visible = false;

                    }
                    if (getFailed != null)
                    {
                        grdUploadError.DataSource = getFailed;
                        grdUploadError.DataBind();

                        grdUploadError.Visible = true;
                        pnlerror.Visible = true;
                        pnlClientsView.Visible=false;
                    }
                    else
                    {
                        grdUploadError.DataSource = null;
                        grdUploadError.DataBind();
                        grdUploadError.Visible = false;
                    }


                }
                else
                {

                    RedAlert("Problem fetching processing data");
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessNewEntrantsRecords();

            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }

        protected void ProcessNewEntrantsRecords()
        {
            try
            {

                LookUp f = new LookUp("cn", 1);
                int ids = int.Parse(txtbatchID.Value);
                DataSet clientupload = f.GetUploads(false, ids);

                if (clientupload != null)
                {
                    foreach (DataRow item in clientupload.Tables[0].Rows)
                    {
                        DataSet dsElig = new DataSet();
                            Guid guid = Guid.NewGuid();
                            string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                            SqlConnection sqlCon = null;
                            using (sqlCon = new SqlConnection(constr))
                            {
                                sqlCon.Open();
                                SqlCommand sql_cmnd = new SqlCommand("BulkMember_ins", sqlCon);
                                sql_cmnd.CommandType = CommandType.StoredProcedure;
                                
                                sql_cmnd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = int.Parse(txtSystemRef.Value);
                                sql_cmnd.Parameters.AddWithValue("@Company", SqlDbType.NVarChar).Value = item["Company"].ToString();
                                sql_cmnd.Parameters.AddWithValue("@DateUploaded", SqlDbType.DateTime).Value = Convert.ToDateTime(item["DateOfUpload"].ToString());
                                sql_cmnd.Parameters.AddWithValue("@StatusID", SqlDbType.Int).Value = 1;
                                sql_cmnd.Parameters.AddWithValue("@msrepl_tran_version", SqlDbType.UniqueIdentifier).Value = guid;
                                sql_cmnd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = item["Surname"].ToString();
                                sql_cmnd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = item["FirstName"].ToString();
                                sql_cmnd.Parameters.AddWithValue("@NationalID", SqlDbType.NVarChar).Value = item["NationalID"].ToString();
                                sql_cmnd.Parameters.AddWithValue("@PensionFund", SqlDbType.NVarChar).Value = item["PensionFund"].ToString();
                                sql_cmnd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = item["Email"].ToString();
                                sql_cmnd.ExecuteNonQuery();
                                sqlCon.Close();
                            }

                            updateclientupload(Convert.ToInt32(item["UploadBatchID"].ToString()));

                    }

                }

                SuccessAlert("Details Processed Successfully and awaiting approval");
               }
            catch (Exception ex)
            {
                RedAlert(ex.Message);

            }
        }
        private void updateclientupload(int tid)
        {
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                cmd = new SqlCommand("update ClientsFileUpload set ProcessStatusID ='1'  where UploadBatchID ='" + tid + "' ", myConnection);

                if ((myConnection.State == ConnectionState.Open))
                    myConnection.Close();
                myConnection.Open();
                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception x)
            {

                RedAlert(x.Message);
            }
            
        }
        private void DeleteMemberupload()
        {
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            cmd = new SqlCommand("Delete from ClientsFileUpload where ProcessStatusID=0 and UploadBatchID ='" + int.Parse(txtbatchID.Value) + "'", myConnection);
            if ((myConnection.State == ConnectionState.Open))
                myConnection.Close();
            myConnection.Open();
            cmd.ExecuteNonQuery();
            myConnection.Close();
        }

        private void DeleteFailedMemberupload()
        {
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            cmd = new SqlCommand("Truncate Table FailedMemberUploads ", myConnection);

            if ((myConnection.State == ConnectionState.Open))
                myConnection.Close();
            myConnection.Open();
            cmd.ExecuteNonQuery();
            myConnection.Close();
        }
        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            DeleteMemberupload();
            DeleteFailedMemberupload();
            Response.Redirect(string.Format("BulkUploads"));
            grdClientsView.DataSource = null;
            grdClientsView.DataBind();
            pnlClientsView.Visible = false;

            grdUploadError.DataSource = null;
            grdUploadError.DataBind();
            pnlerror.Visible = false;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            byte[] bytes = null;
            string fileName = null;
            string contentType = null;
            string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Data,ContentType FROM EntrantUploadTemplate";
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        if ((sdr.HasRows))
                        {
                            bytes = (byte[])sdr["Data"];
                            contentType = sdr["ContentType"].ToString();
                            fileName = sdr["Name"].ToString();
                        }
                        else
                        {
                            bytes = null;
                            contentType = null;
                            fileName = string.Empty;
                        }

                    }
                    con.Close();
                }
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AddHeader("content-disposition", "attachment;filename=\"" + fileName + "");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }


        }

        protected void grdUploadError_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUploadError.PageIndex = e.NewPageIndex;
            this.BindGridError(e.NewPageIndex);
        }
        private void BindGridError(int page = 0)
        {
            try
            {
                LookUp r = new LookUp("cn", 1);
                DataSet c = r.GetFailedMemberUploads();
                if (c != null)
                {
                    int maxPageIndex = grdClientsView.PageCount - 1;
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
                    grdUploadError.DataSource = c;
                    grdUploadError.PageIndex = page;
                    grdUploadError.DataBind();
                }
                else
                {
                    grdUploadError.DataSource = null;
                    grdUploadError.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }

        protected void grdClientsView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClientsView.PageIndex = e.NewPageIndex;
            this.BindGrid(e.NewPageIndex);
        }

        private void BindGrid(int page = 0)
        {
            try
            {
                LookUp r = new LookUp("cn", 1);
                DataSet c = r.getclientuploaded(long.Parse(txtbatchID.Value)); ;
                if (c != null)
                {
                    int maxPageIndex = grdClientsView.PageCount - 1;
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
                    grdClientsView.DataSource = c;
                    grdClientsView.PageIndex = page;
                    grdClientsView.DataBind();
                }
                else
                {
                    grdClientsView.DataSource = null;
                    grdClientsView.DataBind();
                }

            }
            catch (Exception ex)
            {
                WarningAlert("An error occured");
            }
        }
    }
}