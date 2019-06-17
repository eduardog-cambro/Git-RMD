using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro.controls
{
    public partial class roleSelection : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public void loadForm(string strType, int intRecId)
        {
            string strSQL = "shared.get_selectedRoles ";
            strSQL += "@pUserId = " + _security.windowsUserId() + ", ";
            strSQL += "@type = " + _global.Str2Fld(strType) + ", ";
            strSQL += "@recId = " + intRecId.ToString();

            DataSet oDs = new DataSet();
            oDs = _shared.getDataSet(strSQL, _global.conString());

            dlRoles.DataSource = oDs;
            dlRoles.DataBind();
            oDs.Dispose();

            hRecId.Value = intRecId.ToString();
        }

        public string getSelectedRoles()
        {
            string strRoles = "";

            //return comma delimited string of roleIds
            foreach (DataListItem lItem in dlRoles.Items)
            {
                CheckBox chkRoleName = (CheckBox)(lItem.FindControl("chkRoleName"));
                if (chkRoleName.Checked)
                {
                    HiddenField hRoleId = (HiddenField)(lItem.FindControl("hRoleId"));

                    if (strRoles != "") { strRoles += ","; }
                    strRoles += hRoleId.Value;
                }
            }

            return strRoles;
        }

        protected void dlRoles_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            //set as checked if user has this role
            CheckBox chkRoleName = (CheckBox)(e.Item.FindControl("chkRoleName"));
            HiddenField hSelected = (HiddenField)(e.Item.FindControl("hSelected"));
            chkRoleName.Checked = _global.Int2Bol(hSelected.Value);
        }
    }
}