using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace AGMSystem.Reports
{
    public partial class EventReports : System.Web.UI.Page
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument myReport;
        CrystalDecisions.CrystalReports.Engine.FieldObject a;
        CrystalDecisions.CrystalReports.Engine.TextObject b;
        string eventID = "";
        string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["eventID"] != null)
                {
                    eventID = Request.QueryString["eventID"].ToString();
                }
                if (Request.QueryString["type"] != null)
                {
                    type = Request.QueryString["type"].ToString();
                }
                
                ReportView(type);
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["reports"];
                NameTagView.ReportSource = doc;
            }
        }

        private void ReportView(string type)
        {
            myReport = new ReportDocument();

            if (type == "checkIn")
            {
                myReport.Load(Server.MapPath(@"../Reports/EventCheckIn.rpt"));

                string servername = ConfigurationManager.AppSettings["servername"].ToString();
                string ReportPass = ConfigurationManager.AppSettings["ReportPass"].ToString();
                string DBName = ConfigurationManager.AppSettings["DBName"].ToString();
                string DbUser = ConfigurationManager.AppSettings["DbUser"].ToString();

                myReport.SetDatabaseLogon(DbUser, ReportPass, servername, DBName);
                CrystalDecisions.Shared.ParameterFields myParameterFields = new CrystalDecisions.Shared.ParameterFields();
                CrystalDecisions.Shared.ParameterField myParameterField = new CrystalDecisions.Shared.ParameterField();
                CrystalDecisions.Shared.ParameterDiscreteValue myDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
                myParameterField.ParameterFieldName = "eventID";
                myDiscreteValue.Value = eventID;
                myParameterField.CurrentValues.Add(myDiscreteValue);
                myParameterFields.Add(myParameterField);

                NameTagView.ReportSource = myReport;
                NameTagView.ParameterFieldInfo = myParameterFields;
                NameTagView.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

                Session["reports"] = myReport;
            }


        }
    }
}