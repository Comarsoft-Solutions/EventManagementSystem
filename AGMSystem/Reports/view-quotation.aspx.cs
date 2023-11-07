using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace AGMSystem.Reports
{
    public partial class view_quotation : System.Web.UI.Page
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument myReport;

        CrystalDecisions.CrystalReports.Engine.FieldObject a;
        CrystalDecisions.CrystalReports.Engine.TextObject b;

       
        string ID = "";
     
        //string RegNo = "";
        //protected void Page_Unload(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        myReport.Close();
        //        myReport.Dispose();
        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        msgbox(ex.Message);
        //    }
        //}
      
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["ID"] != null)
                {
                    ID = Request.QueryString["ID"].ToString();
                }
                ReportView(int.Parse(ID));
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["Invoice"];
                InvoiceViewer.ReportSource = doc;
            }
        }

        private void ReportView(int ID)
        {
            myReport = new ReportDocument();

            myReport.Load(Server.MapPath(@"../Reports/Invoice.rpt"));
            string servername = ConfigurationManager.AppSettings["servername"].ToString();
            string ReportPass = ConfigurationManager.AppSettings["ReportPass"].ToString();
            string DBName = ConfigurationManager.AppSettings["DBName"].ToString();
            string DbUser = ConfigurationManager.AppSettings["DbUser"].ToString();

            myReport.SetDatabaseLogon(DbUser, ReportPass, servername, DBName);
            CrystalDecisions.Shared.ParameterFields myParameterFields = new CrystalDecisions.Shared.ParameterFields();
            CrystalDecisions.Shared.ParameterField myParameterField = new CrystalDecisions.Shared.ParameterField();
            CrystalDecisions.Shared.ParameterDiscreteValue myDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            myParameterField.ParameterFieldName = "MemberID";
            myDiscreteValue.Value = ID;
            myParameterField.CurrentValues.Add(myDiscreteValue);
            myParameterFields.Add(myParameterField);

            InvoiceViewer.ReportSource = myReport;
            InvoiceViewer.ParameterFieldInfo = myParameterFields;
            InvoiceViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            Session["Invoice"] = myReport;
        }
        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(" + strMessage + ");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }
        
    }
}