using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strFile = "NOTAUTHORIZED";

                //make sure user can access page
                if (_security.checkSecurity(_global.currentPage()).isAuthorized)
                {
                    //get requested page, default to home if none requested
                    if (Request.QueryString["target"] != null)
                    {
                        strFile = Request.QueryString["target"].ToString();
                    }
                    else { strFile = "HOME"; }
                }

                //if logged in check for authenticated version of the target file
                if (strFile != "NOTAUTHORIZED" && _security.userId() != 0)
                {
                    string strAuthenticatedContent = "authenticated_" + strFile;
                    if (System.IO.File.Exists(Server.MapPath("~/html/" + strAuthenticatedContent + ".html")))
                    {
                        strFile = strAuthenticatedContent;
                    }
                }

                //load static content
                string strFullPath = Server.MapPath("~/html/" + strFile + ".html");
                ltHTML.Text = File.ReadAllText(strFullPath);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_Default.Page_Load() failed! Error is: " + ex.Message);
                ltHTML.Text = "<span class='error'>Unable to load content at this time. User support has been contacted.</span>";
            }
        }
    }
}