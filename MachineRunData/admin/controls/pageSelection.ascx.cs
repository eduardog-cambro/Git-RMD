using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro.controls
{
    public partial class pageSelection : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public void loadForm(int intMenuId)
        {
            string strSQL = "shared.get_selectedPages ";
            strSQL += "@pUserId = " + _security.windowsUserId() + ", ";
            strSQL += "@menuId = " + intMenuId.ToString();

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL, _global.conString());

            dlPages.DataSource = oDs;
            dlPages.DataBind();
            oDs.Dispose();

            hMenuId.Value = intMenuId.ToString();
        }

        public string getSelectedPages()
        {
            string strPages = "";

            //return comma delimited string of pageIds
            foreach (DataListItem lItem in dlPages.Items)
            {
                CheckBox chkPageName = (CheckBox)(lItem.FindControl("chkPageName"));
                if (chkPageName.Checked)
                {
                    HiddenField hPageId = (HiddenField)(lItem.FindControl("hPageId"));

                    if (strPages != "") { strPages += ","; }
                    strPages += hPageId.Value;
                }
            }

            return strPages;
        }

        protected void dlPages_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //set as checked if user has this role
            CheckBox chkPageName = (CheckBox)(e.Item.FindControl("chkPageName"));
            HiddenField hSelected = (HiddenField)(e.Item.FindControl("hSelected"));
            chkPageName.Checked = _global.Int2Bol(hSelected.Value);
        }
    }
}