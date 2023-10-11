using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.AGM
{
    public partial class MemberVoting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            GetQuestions();
        }

        private void GetQuestions()
        {
            LookUp lk = new LookUp("cn",1);
            DataSet ds = lk.GetEventQuestions();

            if (ds != null )
            {
                foreach (DataRow dr in ds.Tables[0].Rows) 
                {
                    // Create a Label
                    Label dynamicLabel = new Label();
                    dynamicLabel.ID = "lblDynamic"+ dr["ID"];
                    dynamicLabel.Text = "Dynamic Label: " + dr["Question"];

                    // Create a TextBox
                    TextBox dynamicTextBox = new TextBox();
                    dynamicTextBox.ID = "txtDynamic" + dr["ID"];

                    // Add the controls to the Page
                    form1.Controls.Add(dynamicLabel);
                    form1.Controls.Add(dynamicTextBox);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // You can access the dynamic controls' values here
            string labelText = ((Label)form1.FindControl("lblDynamic")).Text;
            string textBoxValue = ((TextBox)form1.FindControl("txtDynamic")).Text;

            // Do something with the values
            // For example, you can display them in a different Label
            resultLabel.Text = labelText + textBoxValue;
        }
    }
}