using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace cambro
{
    public partial class _userMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //redirect if not logged in
            if (!_security.isSiteAdmin()) { Response.Redirect(_global.rootURL() + "default.aspx?target=NOTAUTHORIZED", true); }

            if (!Page.IsPostBack)
            {
                // load dropdowns
                bindData();
                // load grid
                loadGrid();
            }

            _global.ButtonClickEventGovenor(btnSearch);
            _global.ButtonClickEventGovenor(btnSave);
        }
 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblUserError.Text = "";

            _admin.postResults results = new _admin.postResults();
            results = _admin.postUsers(_global.Fld2Int(hUserId.Value), txtWindowsUserId.Text, txtFriendlyName.Text, txtPlantNumber.Text, chkIsDisabled.Checked, ucRoleSelection.getSelectedRoles());

            if (results.errorMessage != "")
            {
                lblUserError.Text = _global.FormatError(results.errorMessage);
                mpUser.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = (_global.Fld2Int(hUserId.Value) == -1) ? "User added successfully!" : "User saved successfully!";
                lblError.CssClass = "success";

                mpUser.Hide();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strError = _admin.deleteUsers(_global.Fld2Int(hUserId.Value));
            if (strError != "")
            {
                lblUserError.Text = _global.FormatError(strError);
            }
            else
            {
                loadGrid();

                lblError.Text = "User deleted successfully!";
                lblError.CssClass = "success";
            }
        }

        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Text = "";
            lblUserError.Text = "";

            //load for add new
            if (e.CommandName == "add_new")
            {
                hUserId.Value = "";
                chkIsDisabled.Checked = false;
                txtWindowsUserId.Text = "";
                txtFriendlyName.Text = "";
                txtPlantNumber.Text = "";

                btnDelete.Visible = false;
            }

            //load for editting
            if (e.CommandName == "edit")
            {
                hUserId.Value = e.CommandArgument.ToString();
                chkIsDisabled.Checked = _global.Fld2Bol(((HiddenField)(e.Item.FindControl("hIsDisabled"))).Value);

                txtWindowsUserId.Text = ((LinkButton)(e.Item.FindControl("lnkWindowsUserId"))).Text;
                txtFriendlyName.Text = ((Label)(e.Item.FindControl("lblFriendlyName"))).Text;
                txtPlantNumber.Text = ((Label)(e.Item.FindControl("lblPlantNumber"))).Text;

                btnDelete.Visible = true;
            }

            ucRoleSelection.loadForm("user", _global.Fld2Int(hUserId.Value));
            mpUser.Show();
        }

        private void loadGrid()
        {
            lblError.Text = "";
           
            string strSQL = "[shared].[get_users] ";
            strSQL += "@pUserId = " + _security.windowsUserId() + ", ";
            strSQL += "@search = " + _global.Str2Fld(txtSearch.Text);  

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL,_global.conString());

            rpData.DataSource = oDs;
            rpData.DataBind();

            if (oDs.Tables[0].Rows.Count != 0)
            {
                dvGrid.Style.Remove("display");
            }
            else
            {
                lblError.Text = "No data found for selection criteria.";
                lblError.CssClass = "error";
                dvGrid.Style.Add("display","none");
            }

            oDs.Dispose();
        }

        private void bindData()
        {
            // set field lengths
            txtWindowsUserId.MaxLength = 50;
            txtFriendlyName.MaxLength = 255;
            txtPlantNumber.MaxLength = 10;
        }
    }
}
