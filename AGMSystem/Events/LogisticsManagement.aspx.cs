using AGMSystem.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem
{
    public partial class EventManagement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack=true;
            if (!IsPostBack)
            {
                txtComboID.Value = "0";
                txtEventID.Value = "0";
                GetEvents();
            }
        }

        #region alerts
        public void msgbox(string strMessage)
        {
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(" + strMessage + ");";
            strScript += "</script>";
            Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            Page.Controls.Add(lbl);
        }

        protected void RedAlert(string MsgFlg)
        {
            // lblComms.Text = "An Error occured: " + MsgFlg;
            // pnlComms.BackColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Error!', '" + MsgFlg + "', 'error');", true);


        }

        protected void AmberAlert(string MsgFlg)
        {
            // lblComms.Text = "Warning: " + MsgFlg;
            // pnlComms.BackColor = System.Drawing.Color.Orange;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Warning!', '" + MsgFlg + "', 'warning');", true);


        }

        protected void SuccessAlert(string MsgFlg)
        {
            //lblComms.Text = "Success: " + MsgFlg;
            //pnlComms.BackColor = System.Drawing.Color.Green;
            ScriptManager.RegisterStartupScript(this, GetType(), "showSuccess", "Swal.fire('Success!', '" + MsgFlg + "', 'success');", true);

        }
        #endregion
        private void GetEvents()
        {
            try
            {
                AGMEvents eve = new AGMEvents("cn", 1);
                DataSet ds = eve.getAllEvents();
                if (ds != null)
                {
                    ListItem li = new ListItem("Select Event ", "0");
                    dlEvents.DataSource = ds;
                    dlEvents.DataValueField = "ID";
                    dlEvents.DataTextField = "EventName";
                    dlEvents.DataBind();
                    dlEvents.Items.Insert(0, li);

                }
                else
                {
                    ListItem li = new ListItem("No Events Found", "0");
                    dlEvents.Items.Clear();
                    dlEvents.DataSource = null;
                    dlEvents.DataBind();
                    dlEvents.Items.Insert(0, li);
                }
            }
            catch (Exception sx)
            {

                RedAlert(sx.Message);
            }
        }

        private void GetCombos()
        {
            Combos lg = new Combos("cn", 1);
            DataSet xc = lg.GetCombos(int.Parse(dlEvents.SelectedValue));
            if (xc != null)
            {
                grdLogistics.DataSource = xc;
                grdLogistics.DataBind();
            }
            else 
            { grdLogistics.DataSource = null; grdLogistics.DataBind(); }
        }

        protected void dlEvents_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtEventID.Value = dlEvents.SelectedValue;
                int eventID = int.Parse(txtEventID.Value);
                getTransport();
                getAccomodation();
                GetCombos();
            }
            catch (Exception cx)
            {
                RedAlert(cx.Message);
            }
        }

        private void getAccomodation()
        {

            try
            {
                Accomodation agm = new Accomodation("cn", 1);
                DataSet ds = agm.getSavedAccomodation(int.Parse(dlEvents.SelectedValue));
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Accomodation", "0");
                    dlAccomodation.DataSource = ds;
                    dlAccomodation.DataValueField = "ID";
                    dlAccomodation.DataTextField = "Name";
                    dlAccomodation.DataBind();
                    dlAccomodation.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No Accomodation found", "0");
                    dlAccomodation.Items.Clear();
                    dlAccomodation.DataSource = null;
                    dlAccomodation.DataBind();
                    dlAccomodation.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                RedAlert(a.Message);
            }
        }
        private void getTransport()
        {

            try
            {
                Transport agm = new Transport("cn", 1);
                DataSet ds = agm.getSavedTransport(int.Parse(dlEvents.SelectedValue));
                if (ds != null)
                {
                    ListItem listItem = new ListItem("Select Transport", "0");
                    dlTransport.DataSource = ds;
                    dlTransport.DataValueField = "ID";
                    dlTransport.DataTextField = "Name";
                    dlTransport.DataBind();
                    dlTransport.Items.Insert(0, listItem);
                }
                else
                {
                    ListItem li = new ListItem("No Transport found", "0");
                    dlTransport.Items.Clear();
                    dlTransport.DataSource = null;
                    dlTransport.DataBind();
                    dlTransport.Items.Insert(0, li);
                }
            }
            catch (Exception a)
            {

                RedAlert(a.Message);
            }
        }

        protected void grdLogistics_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdLogistics_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                Combos c = new Combos("cn", 1);
                c.ID = int.Parse(txtComboID.Value);
                c.Combo = txtCombo.Text;
                c.Price =Math.Round( double.Parse(txtPrice.Text),2);
                c.EventID = int.Parse(dlEvents.SelectedValue);
                c.TransportID = int.Parse(dlTransport.SelectedValue);
                c.AccomodationID = int.Parse(dlAccomodation.SelectedValue);
                c.ComboCapacity = int.Parse(txtComboCapacity.Text);
                if (c.Save())
                {
                    SuccessAlert("Combo :" + txtCombo.Text + " added");
                    GetCombos();
                }
                else
                {
                    AmberAlert("failed to add: " + txtCombo.Text);
                }
            }
            catch (Exception ex)
            {

                RedAlert(ex.Message);
            }
        }
    }
}