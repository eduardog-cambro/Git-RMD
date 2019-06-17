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
    public partial class _menuMaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // redirect is not authorized
            if (!_security.isSiteAdmin()) { Response.Redirect(_global.rootURL() + "default.aspx?target=NOTAUTHORIZED", true); }

            if (!Page.IsPostBack)
            {
                // load grid
                loadGrid();
            }

            txtTitle.MaxLength = 255;
            txtDisplayOrder.MaxLength = 3;

            _global.ButtonClickEventGovenor(btnSave);
        }
 
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblMenuError.Text = "";

            _admin.postResults results = new _admin.postResults();
            results = _admin.postMenus(_global.Fld2Int(hMenuId.Value), _global.Fld2Int(txtDisplayOrder.Text), txtTitle.Text, txtTarget.Text, chkIsPublic.Checked, chkIsDisabled.Checked, ucRoleSelection.getSelectedRoles(), ucPageSelection.getSelectedPages());

            if (results.errorMessage != "")
            {
                lblMenuError.Text = _global.FormatError(results.errorMessage);
                mpMenu.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = (_global.Fld2Int(hMenuId.Value) == -1) ? "Menu added successfully!" : "Menu saved successfully!";
                lblError.CssClass = "success";

                mpMenu.Hide();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strError = _admin.deleteMenus(_global.Fld2Int(hMenuId.Value));
            if (strError != "")
            {
                lblMenuError.Text = _global.FormatError(strError);
                mpMenu.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = "Menu deleted successfully!";
                lblError.CssClass = "success";
            }
        }

        protected void rpData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hIsPublic = ((HiddenField)e.Item.FindControl("hIsPublic"));
                ((Label)e.Item.FindControl("lblIsPublic")).Text = (_global.Bol2Int(hIsPublic.Value) == 1) ? "yes" : "";

                Label lblPageCount = ((Label)e.Item.FindControl("lblPageCount"));
                if (lblPageCount.Text == "0") { lblPageCount.Text = "none"; }
            }
        }
        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Text = "";
            lblMenuError.Text = "";

            //load for add new
            if (e.CommandName == "add_new")
            {
                hMenuId.Value = "";
                chkIsDisabled.Checked = false;
                txtDisplayOrder.Text = "999";
                txtTitle.Text = "";
                txtTarget.Text = "";
                chkIsPublic.Checked = false;

                btnDelete.Visible = false;
                lblModalHeader.Text = "Add new menu";
            }

            //load for editting
            if (e.CommandName == "edit")
            {
                hMenuId.Value = e.CommandArgument.ToString();
                chkIsDisabled.Checked = _global.Fld2Bol(((HiddenField)(e.Item.FindControl("hIsDisabled"))).Value);
                txtDisplayOrder.Text = ((Label)(e.Item.FindControl("lblDisplayOrder"))).Text;
                txtTitle.Text = ((LinkButton)(e.Item.FindControl("lnkTitle"))).Text;
                txtTarget.Text = ((Label)(e.Item.FindControl("lblTarget"))).Text;
                chkIsPublic.Checked = _global.Fld2Bol(((HiddenField)(e.Item.FindControl("hIsPublic"))).Value);

                btnDelete.Visible = true;
                lblModalHeader.Text = "Edit existing menu";
            }

            ucRoleSelection.loadForm("menu", _global.Fld2Int(hMenuId.Value));
            ucPageSelection.loadForm(_global.Fld2Int(hMenuId.Value));
            mpMenu.Show();
        }

        private void loadGrid()
        {
            lblError.Text = "";
           
            string strSQL = "shared.get_Menus ";
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
