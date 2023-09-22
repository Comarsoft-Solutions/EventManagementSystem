using AGMSystem.models;
using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGMSystem.Membership
{
    public partial class MembershipCompanyMapping : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                txtMemberID.Value = "0";
                try
                {
                    if (Request.QueryString["MemberID"] != null)
                    {
                        txtMemberID.Value = Request.QueryString["MemberID"].ToString();
                        getMemberMappedCompanies(long.Parse(txtMemberID.Value));
                        GetCompanies();

                    }

                }
                catch (Exception ex)
                {

                    RedAlert(ex.Message);
                }
               
            }
        }

        protected void getMemberMappedCompanies(long ID)
        {
            try
            {
                Registration r = new Registration("cn", 1);
                MembershipCompaniesMapping re = new MembershipCompaniesMapping("cn", 1);
                if (r.Retrieve (ID))
                {
                    txtName.Text = r.FirstName + ' ' + r.LastName;
                    txtDesignation.Text = r.Designation;
                    txtMemberID.Value = r.Id.ToString();
                }
                if (re.getMemberMappedCompanies(ID)!=null)
                {
                    lstAssignedComps.DataSource = re.getMemberMappedCompanies(ID);
                    lstAssignedComps.DataValueField = "ID";
                    lstAssignedComps.DataTextField = "Company";
                    lstAssignedComps.DataBind();
                }
                else
                {
                    lstAssignedComps.Items.Clear();
                    lstAssignedComps.DataSource = null;
                    lstAssignedComps.DataBind();
                    //MsgErr("no companies found ");
                }
            }
            catch(
            Exception ex)
            {
                RedAlert(ex.Message);

            }
        }

        protected void GetCompanies()
        {
            try
            {
                MembershipCompaniesMapping reg = new MembershipCompaniesMapping("cn", 1);
                DataSet xx = new DataSet();

                xx = reg.GetCompanies(long.Parse(txtMemberID.Value));

                if (xx != null)
                {
                    UnassignedCompanies.DataSource = xx;
                    UnassignedCompanies.DataValueField = "ID";
                    UnassignedCompanies.DataTextField = "company";
                    UnassignedCompanies.DataBind();
                }
                else
                {
                    UnassignedCompanies.Items.Clear();
                    UnassignedCompanies.DataSource = null;
                    UnassignedCompanies.DataBind();
                    //MsgErr("no companies found ");
                }
            }
            catch (Exception wx)
            {

                RedAlert(wx.Message);
            }
            

        }

        #region alerts
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
        protected void MsgErr(string str)
        {
            lblComms.Text = str;
            lblComms.Font.Bold = true;
            lblComms.ForeColor = System.Drawing.Color.Red;
        }
        protected void MsgSucc(string str)
        {
            lblComms.Text = str;
            lblComms.Font.Bold = true;
            lblComms.ForeColor = System.Drawing.Color.Green;
        }
        public void msgbox(string strMessage)
        {
            Page page = new Page();
            string strScript = "<script language=JavaScript>";
            strScript += "window.alert(\"" + strMessage + "\");";
            strScript += "</script>";
            System.Web.UI.WebControls.Label lbl = new System.Web.UI.WebControls.Label();
            lbl.Text = strScript;
            page.Controls.Add(lbl);
        }

        protected void grdCompanyView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipCompaniesMapping re = new MembershipCompaniesMapping("cn", 1);
                foreach (ListItem item in UnassignedCompanies.Items)
                {
                    if (item.Selected)
                    {
                        re.ID = 0;
                        re.MemberID = long.Parse(txtMemberID.Value);
                        re.CompanyID = int.Parse(item.Value);
                        if (re.Save())
                        {
                            SuccessAlert("Companies Mapped");

                            GetCompanies();
                            getMemberMappedCompanies(long.Parse(txtMemberID.Value));

                        }
                        else

                        {
                            AmberAlert(re.Msgflg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                RedAlert(ex.Message);
            }
            
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
           
            try
            {
                // Get the selected items in the assignedCompanies ListBox
                List<ListItem> selectedItems = new List<ListItem>();
                foreach (ListItem item in lstAssignedComps.Items)
                {
                    if (item.Selected)
                    {
                        selectedItems.Add(item);
                    }
                }

                // Remove the selected mappings and update the ListBox
                foreach (ListItem selectedItem in selectedItems)
                {
                    int companyId = int.Parse(selectedItem.Value);


                    RemoveMapping(companyId);

                    lstAssignedComps.Items.Remove(selectedItem);
                    UnassignedCompanies.Items.Add(selectedItem);
                }
            }
            catch (Exception ex)
            {
                RedAlert(ex.Message);
            }
        }
        protected void RemoveMapping(int companyId)
        {

            MembershipCompaniesMapping mapping = new MembershipCompaniesMapping("cn", 1);
            DataSet removed = mapping.RemoveMapping(companyId);

            if (removed!=null)
            {
                SuccessAlert("Mapping removed for company ID: " + companyId);
                
            }
            else
            {
                RedAlert("Failed to remove mapping for company ID: " + companyId);
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberEnquiries.aspx");
        }
    }
}