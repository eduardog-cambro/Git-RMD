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
    public partial class _roleMaint : System.Web.UI.Page
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
            _global.ButtonClickEventGovenor(btnUserSave);
            _global.ButtonClickEventGovenor(btnPageSave);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblRoleError.Text = "";

            _admin.postResults results = new _admin.postResults();
            results = _admin.postRoles(_global.Fld2Int(hRoleId.Value), txtRoleName.Text);

            if (results.errorMessage != "")
            {
                lblRoleError.Text = _global.FormatError(results.errorMessage);
                mpRole.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = (_global.Fld2Int(hRoleId.Value) == -1) ? "Role added successfully!" : "Role saved successfully!";
                lblError.CssClass = "success";

                mpRole.Hide();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strError = _admin.deleteRoles(_global.Fld2Int(hRoleId.Value));
            if (strError != "")
            {
                lblRoleError.Text = _global.FormatError(strError);
                mpRole.Show();
            }
            else
            {
                loadGrid();

                lblError.Text = "Role deleted successfully!";
                lblError.CssClass = "success";
            }
        }

        protected void btnUserSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            lblUserError.Text = "";

            foreach (ListItem li in cblRoleUser.Items)
            {
                int isChecked = 0;
                if (li.Selected)
                {
                    isChecked = 1;
                }

                errorMessage = _admin.postRoleUser(_global.Fld2Int(li.Value), _global.Fld2Int(hRoleId.Value), _global.Fld2Int(isChecked));

                if (errorMessage != "")
                {
                    lblUserError.ForeColor = System.Drawing.Color.Red;
                    lblUserError.Style.Add("font-weight", "bold");
                    lblUserError.Text = _global.FormatError(errorMessage);
                }
                else
                {
                    lblUserError.ForeColor = System.Drawing.Color.DarkGreen;
                    lblUserError.Style.Add("font-weight", "bold");
                    lblUserError.Text = "Users saved successfully!";
                }
            }

            mpUser.Show();
        }

        protected void btnPageSave_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            lblPageError.Text = "";

            foreach (ListItem li in cblRolePage.Items)
            {
                int isChecked = 0;
                if (li.Selected)
                {
                    isChecked = 1;
                }

                errorMessage = _admin.postRolePage(_global.Fld2Int(li.Value), _global.Fld2Int(hRoleId.Value), _global.Fld2Int(isChecked));

                if (errorMessage != "")
                {
                    lblPageError.ForeColor = System.Drawing.Color.Red;
                    lblPageError.Style.Add("font-weight", "bold");
                    lblPageError.Text = _global.FormatError(errorMessage);
                }
                else
                {
                    lblPageError.ForeColor = System.Drawing.Color.DarkGreen;
                    lblPageError.Style.Add("font-weight", "bold");
                    lblPageError.Text = "Pages saved successfully!";
                }
            }

            mpPage.Show();
        }

        protected void rpData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblError.Text = "";
            lblRoleError.Text = "";
            lblUserError.Text = "";

            //load users in role
            if (e.CommandName == "users")
            {
                hRoleId.Value = e.CommandArgument.ToString();
                lblUserHeader.Text = ((LinkButton)(e.Item.FindControl("lnkRoleName"))).Text + " - Users Assigned";

                loadRoleUsers(_global.Fld2Int(hRoleId.Value));
                mpUser.Show();

                return;
            }

            //load pages in role
            if (e.CommandName == "pages")
            {
                hRoleId.Value = e.CommandArgument.ToString();
                lblPageHeader.Text = ((LinkButton)(e.Item.FindControl("lnkRoleName"))).Text + " - Pages Assigned";

                loadRolePages(_global.Fld2Int(hRoleId.Value));
                mpPage.Show();

                return;
            }

            //load for add new
            if (e.CommandName == "add_new")
            {
                hRoleId.Value = "";
                txtRoleName.Text = "";

                btnDelete.Visible = false;
                lblModalHeader.Text = "Add new role";
            }

            //load for editting
            if (e.CommandName == "edit")
            {
                hRoleId.Value = e.CommandArgument.ToString();
                txtRoleName.Text = ((LinkButton)(e.Item.FindControl("lnkRoleName"))).Text;

                btnDelete.Visible = true;
                lblModalHeader.Text = "Edit existing role";
            }

            mpRole.Show();
        }

        private void loadGrid()
        {
            lblError.Text = "";

            string strSQL = "shared.get_Roles ";
            strSQL += "@pUserId = " + _security.windowsUserId();

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL, _global.conString());

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
                dvGrid.Style.Add("display", "none");
            }

            oDs.Dispose();
        }

        private void loadRoleUsers(int intRoleId)
        {
            lblUserError.Text = "";

            string strSQL = "shared.get_roleUsers ";
            strSQL += "@roleId = " + _global.Fld2Int(intRoleId);

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL, _global.conString());
            cblRoleUser.DataTextField = "userName";
            cblRoleUser.DataValueField = "roleUserId";
            cblRoleUser.DataSource = oDs.Tables[0];
            cblRoleUser.DataBind();

            foreach (DataRow oRow in oDs.Tables[1].Rows)
            {
                foreach (DataColumn oCol in oDs.Tables[1].Columns)
                {
                    if (_global.Fld2IntStr(oRow["roleUserId"]) != "")
                    {
                        foreach (ListItem li in cblRoleUser.Items)
                        {
                            if (_global.Fld2Str(li.Value) == _global.Fld2IntStr(oRow["roleUserId"]))
                            {
                                li.Selected = true;
                            }
                        }
                    }
                }
            }

            oDs.Dispose();
        }

        private void loadRolePages(int intRoleId)
        {
            lblPageError.Text = "";

            string strSQL = "shared.get_rolePages ";
            strSQL += "@roleId = " + _global.Fld2Int(intRoleId);

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL, _global.conString());
            cblRolePage.DataTextField = "title";
            cblRolePage.DataValueField = "rolePageId";
            cblRolePage.DataSource = oDs.Tables[0];
            cblRolePage.DataBind();

            foreach (DataRow oRow in oDs.Tables[1].Rows)
            {
                foreach (DataColumn oCol in oDs.Tables[1].Columns)
                {
                    if (_global.Fld2IntStr(oRow["rolePageId"]) != "")
                    {
                        foreach (ListItem li in cblRolePage.Items)
                        {
                            if (_global.Fld2Str(li.Value) == _global.Fld2IntStr(oRow["rolePageId"]))
                            {
                                li.Selected = true;
                            }
                        }
                    }
                }
            }

            oDs.Dispose();
        }
    }
}
