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
    public partial class _pageMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //redirect if not logged in
            if (!_security.isSiteAdmin()) { Response.Redirect(_global.rootURL() + "default.aspx?target=NOTAUTHORIZED", true); }

            if (!Page.IsPostBack)
            {
                // load grid
                loadGrid();
            }

            _global.ButtonClickEventGovenor(btnSave);
        }
 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblPageError.Text = "";

            _admin.postResults results = new _admin.postResults();
            results = _admin.postPages(_global.Fld2Int(hPageId.Value), txtTitle.Text, txtTarget.Text, chkIsDisabled.Checked, chkShowLoadProgress.Checked, ucRoleSelection.getSelectedRoles());

            if (results.errorMessage != "")
            {
                lblPageError.Text = _global.FormatError(results.errorMessage);
                mpPage.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = (_global.Fld2Int(hPageId.Value) == -1) ? "Page added successfully!" : "Page saved successfully!";
                lblError.CssClass = "success";

                mpPage.Hide();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strError = _admin.deletePages(_global.Fld2Int(hPageId.Value));
            if (strError != "")
            {
                lblPageError.Text = _global.FormatError(strError);
                mpPage.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = "Page deleted successfully!";
                lblError.CssClass = "success";
            }
        }

        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Text = "";
            lblPageError.Text = "";

            //load for add new
            if (e.CommandName == "add_new")
            {
                hPageId.Value = "";
                chkIsDisabled.Checked = false;
                chkShowLoadProgress.Checked = false;
                txtTitle.Text = "";
                txtTarget.Text = "";

                btnDelete.Visible = false;
                lblModalHeader.Text = "Add new page";
            }

            //load for editting
            if (e.CommandName == "edit")
            {
                hPageId.Value = e.CommandArgument.ToString();
                chkIsDisabled.Checked = _global.Fld2Bol(((HiddenField)(e.Item.FindControl("hIsDisabled"))).Value);
                chkShowLoadProgress.Checked = _global.Fld2Bol(((HiddenField)(e.Item.FindControl("hShowLoadProgress"))).Value);
                txtTitle.Text = ((LinkButton)(e.Item.FindControl("lnkTitle"))).Text;
                txtTarget.Text = ((Label)(e.Item.FindControl("lblTarget"))).Text;

                btnDelete.Visible = true;
                lblModalHeader.Text = "Edit existing page";
            }

            ucRoleSelection.loadForm("page", _global.Fld2Int(hPageId.Value));
            mpPage.Show();
        }

        private void loadGrid()
        {
            lblError.Text = "";
           
            string strSQL = "shared.get_Pages ";
            strSQL += "@pUserId = " + _security.windowsUserId();

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
    }
}
