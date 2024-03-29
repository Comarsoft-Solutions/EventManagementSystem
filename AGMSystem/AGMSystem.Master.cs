﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class AGMSystem : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginCode"] != null)
            {

                em.Attributes["href"] = string.Format("Events/Enquiries");
                ca.Attributes["href"] = string.Format("AGM/CreateAGM");
                co.Attributes["href"] = string.Format("communication/CommunicationsCenter");
                rs.Attributes["href"] = (string.Format("AGM/Resolutions"));
                rl.Attributes["href"] = (string.Format("Registration/RsvpList"));
                ds.Attributes["href"] = (string.Format("Dashboard"));
                lg.Attributes["href"] = (string.Format("Dashboard"));
                ln.Attributes["href"] = (string.Format("Dashboard"));
                el.Attributes["href"] = (string.Format("Events/LogisticsManagement"));
                ci.Attributes["href"] = (string.Format("Registration/CheckIn"));
                pe.Attributes["href"] = (string.Format("Projects/ProjectEnquiries"));
                pc.Attributes["href"] = (string.Format("Projects/ProjectCreation"));
                me.Attributes["href"] = (string.Format("Membership/MemberEnquiries"));
                ce.Attributes["href"] = (string.Format("Membership/CompanyEnquiries"));
                mc.Attributes["href"] = (string.Format("Membership/MemberCreation"));
                db.Attributes["href"] = (string.Format("Events/AGMEvent"));
                pr.Attributes["href"] = (string.Format("Registration/PortalRegistration"));
                vt.Attributes["href"] = (string.Format("AGM/Voting"));
                chi.Attributes["href"] = (string.Format("reportSelect/CheckInRpt.aspx"));
                rsvp.Attributes["href"] = (string.Format("reportSelect/RsvpRpt.aspx"));
                qa.Attributes["href"] = (string.Format("QnAs/QnASubmissions.aspx"));
                acr.Attributes["href"] = (string.Format("Activities/Activities.aspx"));
                act.Attributes["href"] = (string.Format("Activities/ActivityTracking.aspx"));
                ee.Attributes["href"] = (string.Format("Events/EventEvaluationsEnquiries.aspx"));
                pre.Attributes["href"] = (string.Format("Events/AddPresenter.aspx"));
                prs.Attributes["href"] = (string.Format("Projects/ProjectRSVP.aspx"));
                //pn.Attributes["href"] = (string.Format("Enquiries?FundID={0}", Session["EventID"]));
            }
            else
            {
                //lblUsername.Text = "Taflo";
                Response.Redirect("../AGMLogin.aspx");
            }


        }
    }
}