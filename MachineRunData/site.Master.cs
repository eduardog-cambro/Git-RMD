using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _security.checkSecurity();

                //if logged in
                if (_security.userId() != 0)
                {
                    if (_global.debugUser() != "") { LoadUsers(); }
                    lblWelcome.Text = "Welcome: " + _security.friendlyName();
                }
                else
                {
                    lblWelcome.Text = "Welcome: " + HttpContext.Current.User.Identity.Name.ToString();
                }

                ltMenu.Text = _admin.buildNavMenu();
            }
        }

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpContext.Current.Session["_spoofUser"] = ddlUsers.SelectedValue;
            Response.Redirect(Request.RawUrl, true);
        }

        private void LoadUsers()
        {
            DataSet oDs = null;
            oDs = _shared.getDataSet("[shared].[get_users] @pUserId = 'SPOOF_USERS'");
            ddlUsers.DataTextField = "FriendlyName";
            ddlUsers.DataValueField = "WindowsUserId";
            ddlUsers.DataSource = oDs;
            ddlUsers.DataBind();

            _global.dpFindValue(ref ddlUsers, _security.windowsUserId());
            dvSpoofUser.Style.Remove("display");
        }
    }
}