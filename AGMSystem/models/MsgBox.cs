using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AGMSystem.models
{
    public class MsgBox
    {
        public static void msgbox(string strMessage)
        {
            Page page = new Page();
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            page.Controls.Add(lbl);
        }



        //ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + strMessage + "', 'success');", true);
        }
}