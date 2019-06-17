using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cambro
{
    public partial class _getValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _AESEncryption aes = new _AESEncryption();
            if (TextBox2.Text != "") { TextBox1.Text = aes.DecryptString(TextBox2.Text); }
            if (TextBox1.Text != "") { TextBox2.Text = aes.EncryptToString(TextBox1.Text); }
        }
    }
}