using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class Enquiries1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ma.Attributes["href"] = (string.Format("AGMEvent"));
                db.Attributes["href"] = (string.Format("AGMEvent"));
                ep.Attributes["href"] = (string.Format("PortalRegistration?EventID={0}", Session["EventID"]));
                pn.Attributes["href"] = (string.Format("Enquiries?FundID={0}", Session["EventID"]));

            }
            
        }
    }
}