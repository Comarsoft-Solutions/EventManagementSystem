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
        //protected void UploadFile()
        //{
        //    try
        //    {
        //        dsuld.Clear();
        //        pnlComms.BackColor = System.Drawing.Color.Transparent;
        //        lblComms.Text = "";
        //        if ((flContributionsUpload.HasFile))
        //        {
        //            //Upload and save the file
        //            string csvPath = Server.MapPath("~/FileUploads/") + Path.GetFileName(flContributionsUpload.PostedFile.FileName);
        //            string finename = Path.GetFileName(flContributionsUpload.PostedFile.FileName);
        //            txtFileName.Text = finename;
        //            flContributionsUpload.SaveAs(csvPath);

        //            string filePath = "FileUploads/" + finename;



        //            txtFilenames.Value = Path.GetFileName(flContributionsUpload.PostedFile.FileName);
        //            contentType.Value = flContributionsUpload.PostedFile.ContentType;
        //            HiddenField1.Value = flContributionsUpload.PostedFile.InputStream.ToString();
        //            SaveDocument();

        //            //save in UploadsTable 
        //            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //            //Dim stream As FileStream = File.Open(csvPath, FileMode.Open, FileAccess.Read)
        //            txtFilePath.Text = csvPath;
        //            ExcelPackage pckage = new ExcelPackage(new FileInfo(csvPath));
        //            //ExcelWorksheet wksheets = default(ExcelWorksheet);
        //            List<string> wkBks = new List<string>();
        //            foreach (ExcelWorksheet wksheets in pckage.Workbook.Worksheets)
        //            {
        //                wkBks.Add(wksheets.Name);
        //            }
        //            lstWrkSheets.DataSource = wkBks;
        //            lstWrkSheets.DataBind();
        //            if ((lstWrkSheets.Items.Count > 0))
        //            {
        //                SuccessAlert("File recorded, select a worksheet to continue");
        //            }
        //            else
        //            {
        //                RedAlert("There was a problem reading the worksheets of the file");
        //                return;
        //            }


        //            if (lstWrkSheets.Items.Count >= 1)
        //            {
        //                lstWrkSheets.Visible = true;
        //                lblWrkSheetPrompt.Visible = true;
        //            }
        //            else
        //            {
        //                lblWrkSheetPrompt.Visible = false;
        //                lstWrkSheets.Visible = false;
        //            }
        //            return;
        //        }
        //        else
        //        {
        //            WarningAlert("Please select a file for upload");
        //            return;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        RedAlert(ex.Message);
        //        return;
        //    }
        //}

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
                    lblComms.Text = "Please select a file for upload";
                    pnlComms.BackColor = System.Drawing.Color.Red;
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
        //private void SaveDocument()
        //{
        //    try
        //    {
        //        //if (txtDateOfUpload.Text == "")
        //        //{
        //        //    txtDateOfUpload.Text = DateTime.Today.ToString();
        //        //}
        //        using (Stream fs = flContributionsUpload.PostedFile.InputStream)
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //                string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        //                using (SqlConnection con = new SqlConnection(constr))
        //                {

        //                    string query = "insert into [FileUploads] values (@Name,@ContentType,@Data,@UploadReason,@DateCreated,@UploadedBy,@CompanyID,@FundID)";
        //                    using (SqlCommand cmd = new SqlCommand(query))
        //                    {
        //                        cmd.Connection = con;
        //                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = txtFilenames.Value;
        //                        cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contentType.Value;
        //                        cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes;
        //                        //cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = txtMemberID.Value;
        //                        cmd.Parameters.Add("@UploadReason", SqlDbType.VarChar).Value = "New Entrant";
        //                        cmd.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now;
        //                        cmd.Parameters.Add("@UploadedBy", SqlDbType.Int).Value = int.Parse(Session["userid"].ToString());
        //                        //cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = int.Parse(cboCompany.SelectedValue);
        //                        cmd.Parameters.Add("@FundID", SqlDbType.Int).Value = int.Parse(txtFundID.Value);
        //                        con.Open();
        //                        cmd.ExecuteNonQuery();
        //                        con.Close();

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        RedAlert(ex.Message);
        //        return;
        //    }
        //}s

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
                    //ProcessUploadData(dsuld, FileName, int.Parse(cboCompany.SelectedValue), cboCompany.SelectedItem.Text.ToString());
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
        protected void ProcessUploadData(DataSet Ds, string FileUploadName, int CompanyID, string CompanyName)
        {
            try
            {
                long BatchUploadID = 0;
                List<double> CountSuccessArray = new List<double>();
                List<double> CountFailedArray = new List<double>();
                if ((dsReady != null))
                {
                    //We Create BatchUpload Record and Get Batch ID
                    //
                    string DatesCreated = DateTime.Now.ToString("yyyy-MM-dd");
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                    cmd = new SqlCommand("insert into EmployeesUploadBatch(BulkUploadFile,DateCreated,DateUpload,UploadStatus) values('" + CompanyID + "','" + FileUploadName + "','" + DatesCreated + "','" + DatesCreated + "','1')", myConnection);

                    if ((myConnection.State == ConnectionState.Open))
                        myConnection.Close();
                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();


                    LookUp ebu = new LookUp("cn", 1);
                    DataSet y = ebu.GetUploadedEmployee(CompanyID);

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

                        if (Int32.TryParse(rw[0].ToString(), out int vv))
                        {
                            if (rw[6].ToString().Trim() != "")
                            {
                                rw[6] = rw[6].ToString().Replace("-", "").Replace(" ", "");
                            }
                            if (rw[6].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"National ID is missing for LastName {rw[4].ToString()} ,FirstName: {rw[5].ToString()} ";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated,CreatedBy) values('0','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }
                                continue;

                            }
                            else if (rw[4].ToString() == "" || rw[5].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Specify both first name and surname for NationalID {rw[6].ToString()} of Branch: {rw[0].ToString()}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,RegNo,Description,BranchCode,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }
                                continue;
                            }
                            else if (Convert.ToDateTime(rw[7].ToString()).ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Date of birth is missing for NationalID {rw[6].ToString()} of Branch: {rw[0].ToString()}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }
                                continue;
                            }
                            else if (Convert.ToDateTime(rw[9].ToString()).ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Specify Date of joining fund for NationalID {rw[6].ToString()} of Branch: {rw[0].ToString()}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }
                                continue;
                            }
                            else if (rw[0].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Specify Branch code for NationalID {rw[6].ToString()}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }
                                continue;
                            }
                            else if (rw[8].ToString() == "")
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                string Msg = $"Gender is missing for NationalID {rw[6].ToString()} of Branch: {rw[0].ToString()}";
                                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,BranchCode,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + dt + "','" + userid + "')", myConnection);
                                if ((myConnection.State == ConnectionState.Open))
                                    myConnection.Close();
                                myConnection.Open();
                                cmd.ExecuteNonQuery();
                                myConnection.Close();
                                double branchcode = double.Parse(rw[0].ToString());

                                for (int runs = 0; runs < 1; runs++)
                                {
                                    CountFailedArray.Add(branchcode);
                                }

                                continue;
                            }
                            else
                            {
                                if (f.ValidateMemberIDNumber(rw[6].ToString()))
                                {
                                    string dt = DateTime.Now.ToString("yyyy-MM-dd");
                                    string Msg = $"Duplicate National Identity number: {rw[6].ToString()}. ID already exists for member: {rw[4].ToString()} {rw[5].ToString()}";
                                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                    cmd = new SqlCommand("insert into FailedMemberUploads(NationalID,Description,BranchCode,DateCreated,CreatedBy) values('" + rw[6].ToString() + "','" + Msg + "','" + rw[0].ToString() + "','" + dt + "','" + userid + "')", myConnection);
                                    if ((myConnection.State == ConnectionState.Open))
                                        myConnection.Close();
                                    myConnection.Open();
                                    cmd.ExecuteNonQuery();
                                    myConnection.Close();

                                    double branchcode = double.Parse(rw[0].ToString());

                                    for (int runs = 0; runs < 1; runs++)
                                    {
                                        CountFailedArray.Add(branchcode);
                                    }

                                    continue;
                                }
                                else
                                {
                                    //Insert into new table ClientUploads
                                    int AddNewClientRecord = 1;
                                    int AddNewMemberRecord = 1;


                                    DateTime DJC = Convert.ToDateTime(rw[9].ToString());
                                    DateTime PSD = Convert.ToDateTime(rw[10].ToString());
                                    DateTime DOB = Convert.ToDateTime(rw[7].ToString());
                                    DateTime DC = DateTime.Now;
                                    string DJCs = DJC.ToString("yyyy-MM-dd");
                                    string DOBs = DOB.ToString("yyyy-MM-dd");
                                    string PSDs = PSD.ToString("yyyy-MM-dd");
                                    string DCs = DC.ToString("yyyy-MM-dd");
                                    if (rw[8].ToString() == "Male" || rw[8].ToString() == "MALE")
                                    {
                                        rw[8] = "M";
                                    }
                                    else if (rw[8].ToString() == "Female" || rw[8].ToString() == "FEMALE")
                                    {
                                        rw[8] = "F";
                                    }
                                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                                    cmd = new SqlCommand("insert into ClientsFileUpload(EmployerName,membershipcategory,Surname,Forenames,IDNumber,DateOfBirth,Gender,CompanyID,DateOfUpload,DateCreated,UploadBatchID,CreatedBy,ProcessStatusID,addnewclientrecord,addnewmeberrecord) values('" + rw[1].ToString() + "','" + Convert.ToInt32(rw[2].ToString()) + "','" + rw[4].ToString() + "','" + rw[5].ToString() + "','" + rw[6].ToString() + "','" + DOBs + "','" + rw[8].ToString() + "','" + CompanyID + "','" + DCs + "','" + DCs + "','" + Convert.ToInt32(txtbatchID.Value) + "','" + int.Parse(txtSystemRef.Value) + "','" + false + "','" + AddNewClientRecord + "','" + AddNewMemberRecord + "')", myConnection);
                                    if ((myConnection.State == ConnectionState.Open))
                                        myConnection.Close();
                                    myConnection.Open();
                                    cmd.ExecuteNonQuery();
                                    myConnection.Close();

                                    double branchcode = double.Parse(rw[0].ToString());

                                    for (int runs = 0; runs < 1; runs++)
                                    {
                                        CountSuccessArray.Add(branchcode);
                                    }
                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                    }
                    double[] totalSucessarray = CountSuccessArray.ToArray();
                    countSuccess = totalSucessarray.Count();

                    double[] totalFailedarray = CountFailedArray.ToArray();
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
                    }
                    else
                    {
                        grdClientsView.DataSource = null;
                        grdClientsView.DataBind();
                        grdClientsView.Visible = false;

                    }
                    if (getFailed != null)
                    {
                        grdUploadError.DataSource = getFailed;
                        grdUploadError.DataBind();

                        grdUploadError.Visible = true;
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

        //protected void ProcessNewEntrantsRecords()
        //{
        //    try
        //    {

        //        LookUp f = new LookUp("cn", 1);
        //        string RegNo = f.getRegNo(int.Parse(txtFundID.Value));
        //        //string getusername = Session["username"].ToString();
        //        int userid = 0;
        //        int ids = int.Parse(txtbatchID.Value);
        //        DataSet clientupload = f.GetUploads(false, ids);
        //        long ClientID = int.Parse(txtSystemRef.Value);

        //        if (clientupload != null)
        //        {
        //            foreach (DataRow item in clientupload.Tables[0].Rows)
        //            {
        //                DataSet dsElig = new DataSet();
        //                if (f.getFundEligibilityRequirements(RegNo) != null)
        //                {
        //                    int Title_Id = 0;
        //                    Guid guid = Guid.NewGuid();
        //                    dsElig = f.getFundEligibilityRequirements(RegNo);
        //                    DataRow rwE = dsElig.Tables[0].Rows[0];
        //                    string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        //                    SqlConnection sqlCon = null;
        //                    using (sqlCon = new SqlConnection(constr))
        //                    {
        //                        sqlCon.Open();
        //                        SqlCommand sql_cmnd = new SqlCommand("MemberPortal_ins", sqlCon);
        //                        sql_cmnd.CommandType = CommandType.StoredProcedure;
        //                        if (item["BranchCode"].ToString() == "F")
        //                        {
        //                            Title_Id = 3;

        //                        }
        //                        else
        //                        {

        //                            Title_Id = 1;
        //                        }
        //                        sql_cmnd.Parameters.AddWithValue("@ClientID", SqlDbType.Int).Value = Convert.ToInt32(ClientID);
        //                        sql_cmnd.Parameters.AddWithValue("@CompanyNo", SqlDbType.Int).Value = item["BranchCode"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@Company_ID", SqlDbType.Int).Value = item["CompanyID"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@BranchId", SqlDbType.Int).Value = item["BranchCode"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@FundCategory_ID", SqlDbType.Int).Value = item["membershipcategory"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@MaritalStatus_ID", SqlDbType.Int).Value = 1;
        //                        sql_cmnd.Parameters.AddWithValue("@AuthorisedBy", SqlDbType.Int).Value = 1;
        //                        sql_cmnd.Parameters.AddWithValue("@ModifiedBy", SqlDbType.Int).Value = int.Parse(txtSystemRef.Value);
        //                        sql_cmnd.Parameters.AddWithValue("@UploadedBy", SqlDbType.Int).Value = int.Parse(txtSystemRef.Value);
        //                        sql_cmnd.Parameters.AddWithValue("@SplittedID", SqlDbType.Int).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@AvcsTATUS", SqlDbType.Bit).Value = false;
        //                        sql_cmnd.Parameters.AddWithValue("@AVCMonthly", SqlDbType.Decimal).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@SplittedCompanyNo", SqlDbType.Int).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@NormalRetAge", SqlDbType.Int).Value = int.Parse(rwE["NormalRetirementAge"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@Title_Id", SqlDbType.Int).Value = Title_Id;
        //                        sql_cmnd.Parameters.AddWithValue("@MonthsWaiting", SqlDbType.Int).Value = int.Parse(rwE["WaitingPeriod"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@MonthsSuspended", SqlDbType.Int).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@IntExitCode", SqlDbType.Int).Value = 1;
        //                        sql_cmnd.Parameters.AddWithValue("@ExitCode", SqlDbType.Int).Value = -1;
        //                        sql_cmnd.Parameters.AddWithValue("@AnnualSalary", SqlDbType.Decimal).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@StartupMember", SqlDbType.Decimal).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@StartupEmployer", SqlDbType.Decimal).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@TotalStartup", SqlDbType.Decimal).Value = 0;
        //                        sql_cmnd.Parameters.AddWithValue("@DateOfBirth", SqlDbType.DateTime).Value = Convert.ToDateTime(item["DateOfBirth"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@DateJoinedCompany", SqlDbType.DateTime).Value = Convert.ToDateTime(item["DateJoinedCompany"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@PensionableServiceDate", SqlDbType.DateTime).Value = Convert.ToDateTime(item["PensionableserviceDate"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@TranferInDate", SqlDbType.DateTime).Value = Convert.ToDateTime(item["PensionableserviceDate"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@NormalRetDate", SqlDbType.DateTime).Value = Convert.ToDateTime((DateTime.Parse(item["DateOfBirth"].ToString()).AddYears(int.Parse(rwE["NormalRetirementAge"].ToString())).ToString()));
        //                        sql_cmnd.Parameters.AddWithValue("@DateModified", SqlDbType.DateTime).Value = Convert.ToDateTime(item["DateOfUpload"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@DateUploaded", SqlDbType.DateTime).Value = Convert.ToDateTime(item["DateOfUpload"].ToString());
        //                        sql_cmnd.Parameters.AddWithValue("@DOBConfirmed", SqlDbType.Bit).Value = true;
        //                        sql_cmnd.Parameters.AddWithValue("@StatusID", SqlDbType.Int).Value = 1;
        //                        sql_cmnd.Parameters.AddWithValue("@Authorised", SqlDbType.Bit).Value = true;
        //                        sql_cmnd.Parameters.AddWithValue("@Active", SqlDbType.Bit).Value = true;
        //                        sql_cmnd.Parameters.AddWithValue("@IsDeferred", SqlDbType.Bit).Value = false;
        //                        sql_cmnd.Parameters.AddWithValue("@msrepl_tran_version", SqlDbType.UniqueIdentifier).Value = guid;
        //                        sql_cmnd.Parameters.AddWithValue("@RegNo", SqlDbType.NVarChar).Value = RegNo;
        //                        sql_cmnd.Parameters.AddWithValue("@Gender_ID", SqlDbType.NVarChar).Value = item["Gender"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@EmployeeReferenceNumber", SqlDbType.NVarChar).Value = item["EmployeeNumber"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = item["Surname"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = item["Forenames"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@IdentityNo", SqlDbType.NVarChar).Value = item["IDNumber"].ToString();
        //                        sql_cmnd.Parameters.AddWithValue("@FundID", SqlDbType.Int).Value = int.Parse(txtFundID.Value);
        //                        sql_cmnd.ExecuteNonQuery();
        //                        sqlCon.Close();
        //                    }

        //                    updateclientupload(Convert.ToInt32(item["UploadBatchID"].ToString()));

        //                }
        //                else
        //                {
        //                    lblWarning.Text = "Please Enter Eligibility Rules First";
        //                    //msgbox("Please Enter Eligibility Rules First");
        //                    return;
        //                }
        //            }

        //        }

        //        SuccessAlert("Details Processed Successfully and awaiting approval");
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Details Processed Successfully');window.location ='./Employees.aspx?FundID=" + int.Parse(txtFundID.Value) + "';", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblDanger.Text = ex.Message;
        //        RedAlert(ex.Message);

        //    }
        //}
    }
}