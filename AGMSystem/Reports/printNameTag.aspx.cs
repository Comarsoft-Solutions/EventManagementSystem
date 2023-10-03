using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Reports
{
    public partial class printNameTag : System.Web.UI.Page
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument myReport;
        CrystalDecisions.CrystalReports.Engine.FieldObject a;
        CrystalDecisions.CrystalReports.Engine.TextObject b;
        string ID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["ID"] != null)
                {
                    ID = Request.QueryString["ID"].ToString();
                }
                ReportView(ID);
            }
            else
            {
                ReportDocument doc = (ReportDocument)Session["nameTags"];
                NameTagView.ReportSource = doc;
            }
        }

        private void ReportView(string ID)
        {
            myReport = new ReportDocument();

            if (ID == "AllMembers")
            {
                myReport.Load(Server.MapPath(@"../Reports/AllMembersNametag.rpt"));
            }
            if (ID == "RSVP")
            {
                myReport.Load(Server.MapPath(@"../Reports/nametag.rpt"));
            }
            if (ID == "companyMembers")
            {
                myReport.Load(Server.MapPath(@"../Reports/nametag.rpt"));
            }
            if (ID == "singleMember")
            {
                myReport.Load(Server.MapPath(@"../Reports/nametag.rpt"));
            }

            //myReport.Load(Server.MapPath(@"../Reports/nametag.rpt"));
            string servername = ConfigurationManager.AppSettings["servername"].ToString();
            string ReportPass = ConfigurationManager.AppSettings["ReportPass"].ToString();
            string DBName = ConfigurationManager.AppSettings["DBName"].ToString();
            string DbUser = ConfigurationManager.AppSettings["DbUser"].ToString();

            myReport.SetDatabaseLogon(DbUser, ReportPass, servername, DBName);
            //CrystalDecisions.Shared.ParameterFields myParameterFields = new CrystalDecisions.Shared.ParameterFields();
            //CrystalDecisions.Shared.ParameterField myParameterField = new CrystalDecisions.Shared.ParameterField();
            //CrystalDecisions.Shared.ParameterDiscreteValue myDiscreteValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            //myParameterField.ParameterFieldName = "MemberID";
            //myDiscreteValue.Value = ID;
            //myParameterField.CurrentValues.Add(myDiscreteValue);
            //myParameterFields.Add(myParameterField);

            NameTagView.ReportSource = myReport;
            //NameTagView.ParameterFieldInfo = myParameterFields;
            NameTagView.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;

            Session["nameTags"] = myReport;
        }
    }
}