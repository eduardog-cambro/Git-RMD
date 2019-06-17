using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;

using System.Net;
using System.Net.Mail;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Services;

namespace cambro
{
    public class runSheetActions
    {
        public const int IsEdit = 1;
        public const int IsCopy = 2;
        public const int IsView = 3;
    }

    public class _global
    {
#pragma warning disable 0436
        // shared generic structures
        public struct getXMLResults
        {
            public string xml;
            public string errorMessage;
        }

        // controls/generSearch.ascx event handler
        public delegate void GenericSearchEventHandler(object sender, GenericSearchEventArgs e);
        public class GenericSearchEventArgs : EventArgs
        {
            public GenericSearchEventArgs(string selectedId, string selectedValue, string targetControl = "")
            {
                this.selectedId = selectedId;
                this.selectedValue = selectedValue;
                this.targetControl = targetControl;
            }

            public string selectedId;
            public string selectedValue;
            public string targetControl;
        }

        //month/year picker event handler
        public delegate void monthYearEventHandler(object sender, monthYearEventArgs e);
        public class monthYearEventArgs : EventArgs
        {
            public monthYearEventArgs(string month, string year)
            {
                this.month = month;
                this.year = year;
            }

            public string month;
            public string year;
        }

        //Web.config variables
        public static string conString()
        {
            string strValue = "";
            try
            {
                _AESEncryption aes = new _AESEncryption();
                string strPassword = aes.DecryptString(ConfigurationManager.AppSettings["conStringPassword"]);
                strValue = ConfigurationManager.AppSettings["conString"].Replace("~PASSWORD~", strPassword);
            }
            catch
            {
            }
            return strValue.Trim();
        }

        public static string server()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["server"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string database()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["database"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string userId()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["userId"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string password()
        {
            string strValue = "";
            try
            {
                _AESEncryption aes = new _AESEncryption();
                strValue = aes.DecryptString(ConfigurationManager.AppSettings["password"]);
            }
            catch
            {
            }
            return strValue.Trim();
        }

        public static string errorLog()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["errorLog"];
            }
            catch
            {
            }
            if (Fld2Str(strValue) == "")
                strValue = HttpContext.Current.Server.MapPath("") + "\\errorLog.txt";
            return strValue.Trim();
        }
        public static string debugUser()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["debugUser"].ToString();
            }
            catch
            {
            }
            return strValue;
        }
        public static string spoofUser()
        {
            string strValue = "";
            try
            {
                strValue = _global.Fld2Str(HttpContext.Current.Session["_spoofUser"]);
            }
            catch
            {
            }
            return strValue;
        }

        public static int gridPageSize()
        {
            int intValue = 0;
            try
            {
                intValue = _global.Fld2Int(ConfigurationManager.AppSettings["gridPageSize"]);
            }
            catch
            {
            }
            return ((intValue != 0) ? intValue : 20);
        }

        public static string errorEmailSubject()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["errorEmailSubject"];
            }
            catch
            {
            }
            if (Fld2Str(strValue) == "")
                strValue = "glenair - Error";
            return strValue.Trim();
        }
        public static string sendErrorEmail()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["sendErrorEmail"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string smtpServer()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["smtpServer"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string smtpServer_User()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["smtpServer_User"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static int smtpServer_Port()
        {
            int intValue = 0;
            try
            {
                intValue = Fld2Int(ConfigurationManager.AppSettings["smtpServer_Port"]);
            }
            catch
            {
            }
            return intValue;
        }
        public static bool smtpServer_SSL()
        {
            bool boolValue = false;
            try
            {
                boolValue = Int2Bol(ConfigurationManager.AppSettings["smtpServer_SSL"]);
            }
            catch
            {
            }
            return boolValue;
        }
        public static string smtpServer_Password()
        {
            string strValue = "";
            try
            {
                _AESEncryption aes = new _AESEncryption();
                strValue = aes.DecryptString(ConfigurationManager.AppSettings["smtpServer_Password"]);
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string smtpFromEmailAddr()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["smtpFromEmailAddr"];
            }
            catch
            {
            }
            return strValue.Trim();
        }

        public static string webmasterEmailAdr()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["webmasterEmailAdr"];
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string webmasterLogin()
        {
            string strValue = "";
            try
            {
                _AESEncryption aes = new _AESEncryption();
                strValue = aes.DecryptString(ConfigurationManager.AppSettings["webmasterLogin"]);
            }
            catch
            {
            }
            return strValue.Trim();
        }
        public static string LDAPServer()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["LDAP_Server"];
            }
            catch
            {
            }
            return strValue.Trim();
        }

        public static string rootURL()
        {
            var appPath = string.Empty;
            try
            {
                //Getting the current context of HTTP request
                var context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            context.Request.Url.Port == 80
                                                ? string.Empty
                                                : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                if (!appPath.EndsWith("/")) { appPath += "/"; }
            }
            catch
            {
            }
            return appPath;
        }
        public static string currentPage()
        {
            var appPath = string.Empty;
            try
            {
                //Getting the current context of HTTP request
                var context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = context.Request.RawUrl.Replace(context.Request.ApplicationPath, "").Replace("/", "");
                }
            }
            catch
            {
            }
            return (appPath == "" || appPath.StartsWith("default.aspx")) ? "default.aspx" : appPath;
        }

        public static string cachedVersion()
        {
            string strValue = "";
            try
            {
                strValue = ConfigurationManager.AppSettings["clearCached-CSSandJS"].ToString();
                strValue = ((strValue == "1") ? System.DateTime.Now.ToString("yyyyMMddHHmmss") : "1");
            }
            catch
            {
            }
            return strValue;
        }

        //Encrypt string 
        public static string EnryptString(string strString)
        {
            try
            {
                byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strString);
                string strReturn = Convert.ToBase64String(b);
                return strReturn;
            }
            catch
            {
                throw;
            }
        }

        //Decrypt string, use encrypt.exe to encrypt value for web.config
        public static string DecryptString(string strString)
        {
            try
            {
                byte[] b = Convert.FromBase64String(strString);
                string strReturn = System.Text.ASCIIEncoding.ASCII.GetString(b);
                return strReturn;
            }
            catch
            {
                throw;
            }
        }

        //Position DropDownList by text
        public static void dpFindString(ref DropDownList dpDropDown, string strValue)
        {
            try
            {
                // do case insensative search
                var itemMatch = dpDropDown.Items.Cast<ListItem>()
                                .FirstOrDefault(i => i.Text.Equals(strValue, StringComparison.InvariantCultureIgnoreCase));

                dpDropDown.SelectedIndex = dpDropDown.Items.IndexOf(((ListItem)itemMatch));
            }
            catch
            {
                dpDropDown.SelectedIndex = -1;
            }
        }

        //Position DropDownList by value
        public static void dpFindValue(ref DropDownList dpDropDown, string strValue)
        {
            try
            {
                // do case insensative search
                var itemMatch = dpDropDown.Items.Cast<ListItem>()
                                .FirstOrDefault(i => i.Value.Equals(strValue, StringComparison.InvariantCultureIgnoreCase));

                dpDropDown.SelectedIndex = dpDropDown.Items.IndexOf(((ListItem)itemMatch));
            }
            catch
            {
                dpDropDown.SelectedIndex = -1;
            }
        }

        //Position RadioButtonList by value
        public static void rdFindValue(ref RadioButtonList rdRadioButtonList, string strValue)
        {
            try
            {
                rdRadioButtonList.SelectedIndex = rdRadioButtonList.Items.IndexOf(rdRadioButtonList.Items.FindByValue(strValue));
            }
            catch
            {
                rdRadioButtonList.SelectedIndex = -1;
            }
        }

        //convert an object to a string
        public static string Fld2Str(object oValue)
        {
            return Fld2Str(oValue, "");
        }
        public static string Fld2Str(object oValue, string strDefault)
        {
            string strReturn = strDefault;
            try
            {
                if ((oValue != null))
                    strReturn = Convert.ToString(oValue);
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        //convert an object to a integer
        public static int Fld2Int(object oValue)
        {
            return Fld2Int(oValue, 0);
        }
        public static int Fld2Int(object oValue, int intDefault)
        {
            int intReturn = intDefault;
            try
            {
                if (oValue != null)
                    intReturn = int.Parse(Convert.ToString(oValue), System.Globalization.NumberStyles.AllowThousands);
            }
            catch
            {
            }
            return intReturn;
        }

        //convert an object to a float
        public static float Fld2Flt(object oValue)
        {
            return Fld2Flt(oValue, 0);
        }
        public static float Fld2Flt(object oValue, float fltDefault)
        {
            float fltReturn = fltDefault;
            try
            {
                if ((oValue != null))
                    fltReturn = float.Parse(Convert.ToString(oValue), System.Globalization.NumberStyles.AllowThousands);
            }
            catch
            {
            }
            return fltReturn;
        }

        //convert an object to a integer then the interger to a string
        public static string Fld2IntStr(object oValue)
        {
            string strReturn = "0";
            try
            {
                if ((oValue != null))
                    strReturn = Fld2Str(Convert.ToInt32(oValue), "0");
            }
            catch
            {
            }
            return strReturn;
        }

        //convert an object to a big integer
        public static Int64 Fld2BigInt(object oValue)
        {
            return Fld2BigInt(oValue, 0);
        }
        public static Int64 Fld2BigInt(object oValue, int intDefault)
        {
            Int64 intReturn = intDefault;
            try
            {
                if ((oValue != null))
                    intReturn = Int64.Parse(Convert.ToString(oValue), System.Globalization.NumberStyles.AllowThousands);
            }
            catch
            {
            }
            return intReturn;
        }

        //convert an object to a boolean
        public static bool Fld2Bol(object oValue)
        {
            return Fld2Bol(oValue, false);
        }
        public static bool Fld2Bol(object oValue, bool bolDefault)
        {
            bool bolReturn = bolDefault;
            try
            {
                if ((oValue != null))
                    bolReturn = Convert.ToBoolean(oValue);
            }
            catch
            {
            }
            return bolReturn;
        }

        //convert an object to a double
        public static double Fld2Dbl(object oValue)
        {
            return Fld2Dbl(oValue, 0);
        }
        public static double Fld2Dbl(object oValue, double dblDefault)
        {
            double dblReturn = dblDefault;
            try
            {
                if ((oValue != null))
                    dblReturn = double.Parse(Convert.ToString(oValue).Replace(",", ""), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
            }
            return dblReturn;
        }

        public static string Bol2YesNo(object oValue)
        {
            return Bol2YesNo(oValue, "");
        }

        public static string Bol2YesNo(object oValue, string NoValueDefault)
        {
            return ((Fld2Bol(oValue)) ? "Yes" : NoValueDefault);
        }

        //convert an object to date, return a formatted date string
        public static string Fld2Date(object oValue)
        {
            return Fld2Date(oValue, "");
        }
        public static string Fld2Date(object oValue, string strDefault)
        {
            string strReturn = strDefault;
            try
            {
                strReturn = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(oValue));
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        public static string Fld2MonthYear(object oValue, string strDefault)
        {
            string strReturn = strDefault;
            try
            {
                strReturn = String.Format("{0:MMM yyyy}", Convert.ToDateTime(oValue));
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        //convert an object to date, return a formatted tiem string
        public static string Fld2Time(object oValue)
        {
            string strReturn = "";
            try
            {
                strReturn = String.Format("{0:hh:mm tt}", Convert.ToDateTime(oValue));
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        //convert an object to a formatted number string
        public static string Fld2Num(object oValue)
        {
            return Fld2Num(oValue, 0, true, "");
        }
        public static string Fld2Num(object oValue, short intDecimalPoints)
        {
            return Fld2Num(oValue, intDecimalPoints, true, "");
        }
        public static string Fld2Num(object oValue, short intDecimalPoints, bool bolRemoveCommas)
        {
            return Fld2Num(oValue, intDecimalPoints, bolRemoveCommas, "");
        }
        public static string Fld2Num(object oValue, short intDecimalPoints, bool bolRemoveCommas, string strDefault)
        {
            string strReturn = strDefault;
            string strMask = "{0:N" + intDecimalPoints.ToString() + "}";
            try
            {
                if ((oValue != null))
                {
                    if (Fld2Dbl(oValue) != 0)
                    {
                        strReturn = String.Format(strMask, Convert.ToDecimal(oValue));
                        if (bolRemoveCommas) { strReturn = strReturn.Replace(",", ""); }
                    }
                }
            }
            catch
            {
            }

            //If zero return defaults value
            if (Fld2Dbl(strReturn) == 0) strReturn = strDefault;

            return strReturn.Trim();
        }

        //convert a string to a field, used when putting together a SQL string
        public static string Str2Fld(object oValue)
        {
            string strReturn = "NULL";
            try
            {
                if (oValue != null)
                {
                    strReturn = Convert.ToString(oValue);
                    strReturn = "'" + strReturn.Replace("'", "''").Trim() + "'";
                }
            }
            catch
            {
            }

            return strReturn.Trim();
        }

        //Encode sting for java script parameter
        public static string Str2JavaParm(object oValue)
        {
            string strReturn = "";
            try
            {
                if ((oValue != null))
                {
                    strReturn = Convert.ToString(oValue);
                    strReturn = strReturn.Replace("'", "\\'").Trim();
                    strReturn = strReturn.Replace("\"", "\\\"").Trim();
                    strReturn = strReturn.Replace(@"\", "\\\\").Trim();
                }
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        //convert a boolean to an integer true=1, false=0
        public static int Bol2Int(object oValue)
        {
            return Bol2Int(Fld2Bol(oValue));
        }
        public static int Bol2Int(bool bolValue)
        {
            int intReturn = 0;
            try
            {
                if (bolValue)
                    intReturn = 1;
            }
            catch
            {
            }
            return intReturn;
        }

        //convert a integer to a boolean false=0, anything else is true
        public static bool Int2Bol(object oValue)
        {
            bool bolReturn = false;
            try
            {
                if (Fld2Int(oValue) != 0)
                    bolReturn = true;
            }
            catch
            {
            }
            return bolReturn;
        }

        public static bool IsValidDate(object oDate)
        {
            bool bolReturn = false;
            if (IsDate(oDate))
            {
                // if valid, date must be above lowest valid SQL date of 01/01/1753
                if ((DateTime.Parse(oDate.ToString()) > DateTime.Parse("01/01/1753")))
                {
                    bolReturn = true;
                }
            }
            return bolReturn;
        }
        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string scrubColumnForExcel(object oValue)
        {
            string strReturn = _global.Fld2Str(oValue);
            try
            {
                if (_global.IsDate(strReturn))
                {
                    if (strReturn.Contains("12:00:00 AM"))
                    {
                        strReturn = _global.Fld2Date(oValue);
                    }
                }
            }
            catch
            {
            }
            return strReturn.Trim();
        }

        public static void WriteErrorLog(string strError)
        {
            WriteErrorLog(strError, -1, false);
        }
        public static void WriteErrorLog(string strError, int intUserId)
        {
            WriteErrorLog(strError, intUserId, false);
        }
        public static void WriteErrorLog(string strError, bool bolDontEmail)
        {
            WriteErrorLog(strError, -1, bolDontEmail);
        }
        public static void WriteErrorLog(string strError, int intUserId, bool bolDontEmail)
        {
            StreamWriter swError = null;

            try
            {
                swError = File.AppendText(errorLog());
                swError.WriteLine(System.DateTime.Now + "     " + strError);
                swError.Flush();

                // send error to webmaster
                if (_global.sendErrorEmail() == "yes" && !bolDontEmail)
                {
                    string strMessage = "<table border=\'1\' cellspacing=\'2\' cellpadding=\'2\' style=\'width:800px;\'><tr>";
                    strMessage += "<td style=\'font-weight:bold;width:200px;\'>Current user Id:</td><td>" + intUserId + "</td></tr>";
                    strMessage += "<td style=\'font-weight:bold;width:200px;\'>URL:</td><td>" + HttpContext.Current.Request.RawUrl + "</td></tr>";
                    strMessage += "<td style=\'font-weight:bold;width:200px;\'>Error:</td><td>" + strError + "</td></tr></table><br/>";

                    if (!_email.sendEmail(true, strError, _global.errorEmailSubject(), _global.smtpFromEmailAddr(), _global.webmasterEmailAdr()))
                    {
                        _global.WriteErrorLog("General failure! Error is: Unable to send error email", intUserId, true);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if ((swError != null))
                    swError.Close();
            }
        }

        public static string FormatError(string strError)
        {
            if (strError.Trim() == "") { return strError; }

            // format error message. Cap first letter and add a period
            return strError.Substring(0, 1).ToUpper() + strError.Substring(1, strError.Length - 1) + ".";
        }

        public static string getCheckedItems(ref CheckBoxList cblList)
        {
            return getCheckedItems(ref cblList, "~");
        }
        public static string getCheckedItems(ref CheckBoxList cblList, string strSepChar)
        {
            string strItems = "";
            for (int i = 0; (i <= (cblList.Items.Count - 1)); i++)
            {
                if (cblList.Items[i].Selected)
                {
                    if (strItems.Trim() != "")
                    {
                        strItems = (strItems + strSepChar);
                    }
                    strItems = (strItems + cblList.Items[i].Value);
                }
            }
            return strItems;
        }

        public static void setCheckedItems(ref CheckBoxList cblList, DataTable oTable, string strTextFieldName)
        {
            ArrayList aSelectedItems = new ArrayList();
            foreach (DataRow oRow in oTable.Rows)
            {
                aSelectedItems.Add(oRow[strTextFieldName].ToString());
            }
            // Select items in array, unselect in not in array
            for (int i = 0; (i <= (cblList.Items.Count - 1)); i++)
            {
                if ((aSelectedItems.IndexOf(cblList.Items[i].Value) != -1))
                {
                    cblList.Items[i].Selected = true;
                }
                else
                {
                    cblList.Items[i].Selected = false;
                }
            }
        }
        public static void setCheckedItems(ref CheckBoxList cblList, string strValues)
        {
            setCheckedItems(ref cblList, strValues, "~");
        }
        public static void setCheckedItems(ref CheckBoxList cblList, string strValues, string strSepChar)
        {
            string[] strValueArray = strValues.Split(strSepChar.ToCharArray());
            ArrayList aSelectedItems = new ArrayList();
            foreach (string strItem in strValueArray)
            {
                aSelectedItems.Add(strItem);
            }
            // Select items in array, unselect in not in array
            for (int i = 0; (i <= (cblList.Items.Count - 1)); i++)
            {
                if ((aSelectedItems.IndexOf(cblList.Items[i].Value) != -1))
                {
                    cblList.Items[i].Selected = true;
                }
                else
                {
                    cblList.Items[i].Selected = false;
                }
            }
        }

        public static void unSelectAllCheckedItems(ref CheckBoxList cblList)
        {
            for (int i = 0; (i <= (cblList.Items.Count - 1)); i++)
            {
                cblList.Items[i].Selected = false;
            }
        }

        public static bool isItemChecked(ref CheckBoxList cblList, string strItemText)
        {
            bool bolResults = false;
            for (int i = 0; (i <= (cblList.Items.Count - 1)); i++)
            {
                if (cblList.Items[i].Text == strItemText)
                {
                    bolResults = cblList.Items[i].Selected;
                    return bolResults;
                }
            }
            return bolResults;
        }

        public static ArrayList splitStringArray(string strString)
        {
            return splitStringArray(strString, "~");
        }
        public static ArrayList splitStringArray(string strString, string strSepChar)
        {
            ArrayList aResults = new ArrayList();
            string[] strStringArray;
            strStringArray = strString.Split(strSepChar.ToCharArray());
            for (int i = 0; (i <= strStringArray.Length); i++)
            {
                aResults.Add(strStringArray[i]);
            }
            return aResults;
        }

        public static string MapURL(string path)
        {
            path = HttpContext.Current.Server.MapPath(path);
            string approot = HttpContext.Current.Request.PhysicalApplicationPath.TrimEnd('\\');
            return path.Replace(approot, string.Empty).Replace('\\', '/');
        }

        //Disable submit asp:Button to avoid double post back
        public static void ButtonClickEventGovenor(System.Web.UI.WebControls.Button btn, bool PageHasValidators = false)
        {
            string strJavaScript = null;
            PostBackOptions oPostBackOptions = new PostBackOptions(btn);
            oPostBackOptions.ActionUrl = btn.PostBackUrl;

            if (PageHasValidators)
            {
                strJavaScript = "if (typeof(Page_ClientValidate) == 'function')";
                strJavaScript += "{";
                strJavaScript += " if (Page_ClientValidate()) ";
                strJavaScript += "{";
                //strJavaScript += " this.style.fontSize = '.9em';";
                strJavaScript += " this.value='Please wait';";
                strJavaScript += " this.disabled = true; ";
                strJavaScript += btn.Page.ClientScript.GetPostBackEventReference(oPostBackOptions) + ";";
                strJavaScript += " return false;";
                strJavaScript += "}";
                strJavaScript += "}";
            }
            else
            {
                //strJavaScript += " this.style.fontSize = '.9em';";
                strJavaScript += " this.value='Please wait';";
                strJavaScript += " this.disabled = true; ";
                strJavaScript += btn.Page.ClientScript.GetPostBackEventReference(oPostBackOptions) + ";";
            }

            btn.Attributes.Add("OnClick", strJavaScript);
        }

        //Disable submit asp:LinkButton to avoid double post back
        public static void LinkButtonClickEventGovenor(System.Web.UI.WebControls.LinkButton btn, bool PageHasValidators = false)
        {
            string strJavaScript = null;
            PostBackOptions oPostBackOptions = new PostBackOptions(btn);
            oPostBackOptions.ActionUrl = btn.PostBackUrl;

            if (PageHasValidators)
            {
                strJavaScript = "if (typeof(Page_ClientValidate) == 'function')";
                strJavaScript += "{";
                strJavaScript += " if (Page_ClientValidate()) ";
                strJavaScript += "{";
                strJavaScript += " this.value='Please wait...';";
                strJavaScript += " this.disabled = true; ";
                strJavaScript += btn.Page.ClientScript.GetPostBackEventReference(oPostBackOptions) + ";";
                strJavaScript += " return false;";
                strJavaScript += "}";
                strJavaScript += "}";
            }
            else
            {
                strJavaScript += " this.value='Please wait...';";
                strJavaScript += " this.disabled = true; ";
                strJavaScript += btn.Page.ClientScript.GetPostBackEventReference(oPostBackOptions) + ";";
            }

            btn.Attributes.Add("OnClick", strJavaScript);
        }

        public static void ListControls(Control oContainer)
        {
            foreach (Control uControl in oContainer.Controls)
            {
                if (_global.Fld2Str(uControl.ID) != "") { System.Diagnostics.Debug.WriteLine(uControl.ID); }

                if (uControl.Controls.Count > 0)
                {
                    _global.ListControls(uControl);
                }
            }
        }

        public static void InitTabIndex(Control oContainer)
        {
            foreach (Control uControl in oContainer.Controls)
            {
                if (_global.Fld2Str(uControl.ID) != "")
                {
                    var property = uControl.GetType().GetProperty("TabIndex");
                    if (property != null)
                    {
                        Int16 i = -1;
                        property.SetValue(uControl, i, null);
                    }
                }

                if (uControl.Controls.Count > 0)
                {
                    _global.InitTabIndex(uControl);
                }
            }
        }

        // ---------------------------------------------------------------------------
        // DEBUG CODE
        // ---------------------------------------------------------------------------

        //Code to build a SQL string from a SqlCommand object
        public static void debugSQL(string strSQL, SqlCommand oCmd)
        {
            int intLoop = 0;

            for (intLoop = 0; intLoop <= oCmd.Parameters.Count - 1; intLoop++)
            {
                strSQL += "\r\n" + oCmd.Parameters[intLoop].ParameterName + " = ";
                if (oCmd.Parameters[intLoop].SqlDbType == SqlDbType.NVarChar | oCmd.Parameters[intLoop].SqlDbType == SqlDbType.VarChar | oCmd.Parameters[intLoop].SqlDbType == SqlDbType.Char)
                {
                    strSQL += Str2Fld(oCmd.Parameters[intLoop].Value);
                }
                else
                {
                    strSQL += Fld2Num(oCmd.Parameters[intLoop].Value, oCmd.Parameters[intLoop].Scale, true, "0");
                }
                strSQL += ",";
            }

            strSQL = strSQL.Trim();
            System.Diagnostics.Debug.Print(strSQL.Substring(0, strSQL.Length - 1));
        }
    }
    public class _email
    {
#pragma warning disable 0436

        // event handler for controls\email.ascx
        public delegate void emailEventHandler(object sender, emailEventArgs e);
        public class emailEventArgs : EventArgs
        {
            public emailEventArgs(bool emailSent)
            {
                this.emailSent = emailSent;
                this.sentStatusMessage = "";
            }

            public emailEventArgs(bool emailSent, string sentStatusMessage)
            {
                this.emailSent = emailSent;
                this.sentStatusMessage = sentStatusMessage;
            }

            public bool emailSent;
            public string sentStatusMessage;
        }

        public static bool sendEmail(bool bolIsBodyHtml, string strMessage, string strSubject, string strFrom, string strTo)
        {
            // -- Send plain text or HTML email. To send to multiple "to" address seperate address with a semi-colon (;)
            return sendEmail(bolIsBodyHtml, strMessage, strSubject, strFrom, strTo, "", "");
        }
        public static bool sendEmail(bool bolIsBodyHtml, string strMessage, string strSubject, string strFrom, string strTo, string strCC)
        {
            return sendEmail(bolIsBodyHtml, strMessage, strSubject, strFrom, strTo, strCC, "");
        }
        public static bool sendEmail(bool bolIsBodyHtml, string strMessage, string strSubject, string strFrom, string strTo, string strCC, string strBCC)
        {
            bool bolResults = false;

            MailAddress maAddr = null;
            MailMessage mMessage = null;
            SmtpClient SmtpClient = null;

            try
            {
                mMessage = new MailMessage();
                maAddr = new MailAddress(strFrom.Trim());
                mMessage.From = maAddr;

                // add to email addresses
                string[] aTO = strTo.Split(';');
                foreach (string strEmail in aTO)
                {
                    if (strEmail.Trim() != "")
                    {
                        maAddr = new MailAddress(strEmail.Trim());
                        mMessage.To.Add(maAddr);
                    }
                }

                // add CC email addresses
                string[] aCC = strCC.Split(';');
                foreach (string strEmail in aCC)
                {
                    if (strEmail.Trim() != "")
                    {
                        maAddr = new MailAddress(strEmail.Trim());
                        mMessage.CC.Add(maAddr);
                    }
                }

                // add BCC email addresses
                string[] aBCC = strBCC.Split(';');
                foreach (string strEmail in aBCC)
                {
                    if (strEmail.Trim() != "")
                    {
                        maAddr = new MailAddress(strEmail.Trim());
                        mMessage.Bcc.Add(maAddr);
                    }
                }

                mMessage.IsBodyHtml = bolIsBodyHtml;
                mMessage.Body = strMessage;
                mMessage.Subject = strSubject;
                mMessage.Priority = MailPriority.High;

                // set up smtp client
                SmtpClient = new SmtpClient(_global.smtpServer(), _global.smtpServer_Port());

                // If smtp User defined add credentials
                if (_global.smtpServer_User() != "")
                {
                    SmtpClient.UseDefaultCredentials = false;
                    SmtpClient.Credentials = new NetworkCredential(_global.smtpServer_User(), _global.smtpServer_Password());
                    SmtpClient.EnableSsl = _global.smtpServer_SSL();
                }

                // send message
                SmtpClient.Send(mMessage);
                bolResults = true;
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_email.sendEmail() failed! Error is: " + ex.Message + " eMail Message: " + _global.Fld2Str(strMessage), -1, true);
            }
            finally
            {
                maAddr = null;
                mMessage = null;
                SmtpClient = null;
            }
            return bolResults;
        }
    }
    public class _shared
    {
#pragma warning disable 0436

        public static DataSet getDataSet(string strSQL)
        {
            return getDataSet(strSQL, _global.conString());
        }
        public static DataSet getDataSet(string strSQL, string strConString)
        {

            DataSet oDs = null;
            SqlConnection oCn = null;
            SqlDataAdapter oDa = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(strConString);

                oCmd = new SqlCommand();
                oCmd.CommandTimeout = 0;
                oCmd.CommandType = CommandType.Text;
                oCmd.CommandText = strSQL;
                oCmd.Connection = oCn;
                oCmd.Prepare();

                oDa = new SqlDataAdapter(oCmd);
                oDs = new DataSet();
                oDa.Fill(oDs);

            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                if ((oCn != null))
                    if (oCn.State == ConnectionState.Open)
                        oCn.Close();
                oCn.Dispose();
                oCmd.Dispose();
                oDa.Dispose();
            }

            return oDs;
        }

        public static bool executeSQL(string strSQL)
        {
            return executeSQL(strSQL, _global.conString());
        }
        public static bool executeSQL(string strSQL, string strConString)
        {
            bool bolResults = false;
            SqlConnection oCn = null;
            try
            {
                oCn = new SqlConnection(strConString);
                SqlCommand oCmd = new SqlCommand(strSQL, oCn);
                oCmd.Connection.Open();
                oCmd.ExecuteNonQuery();

                bolResults = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if ((oCn != null))
                    if (oCn.State == ConnectionState.Open)
                        oCn.Close();
                oCn.Dispose();
            }

            return bolResults;
        }

        public static _global.getXMLResults getXMLData(string strSQL)
        {
            return getXMLData(strSQL, _global.conString());
        }
        public static _global.getXMLResults getXMLData(string strSQL, string strConString)
        {
            _global.getXMLResults results = new _global.getXMLResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            XmlReader oXml = null;
            XmlDocument oDoc = null;

            try
            {
                oCn = new SqlConnection(strConString);
                oCmd = new SqlCommand(strSQL, oCn);
                oCmd.CommandType = CommandType.Text;

                oCn.Open();
                oXml = oCmd.ExecuteXmlReader();
                oDoc = new XmlDocument();
                oDoc.Load(oXml);

                // Get return values
                results.xml = oDoc.InnerXml;
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_shared.getXMLData() failed! Error is: " + ex.Message);
                results.errorMessage = ex.Message;
            }
            finally
            {
                if (!(oCn == null))
                {
                    if ((oCn.State == ConnectionState.Open))
                    {
                        oCn.Close();
                    }
                }
                if (!(oXml == null)) { oXml.Close(); }
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static void exportToCSV(string strSQL, string strFileName, string strConString)
        {
            string strData = "";
            bool bolFirstPass = true;

            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            SqlDataReader oDr = null;

            try
            {
                oCn = new SqlConnection(strConString);
                oCmd = new SqlCommand(strSQL, oCn);
                oCn.Open();

                oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDr.Read())
                {
                    // write column headings
                    if (bolFirstPass)
                    {
                        bolFirstPass = false;
                        for (int i = 0; (i <= (oDr.FieldCount - 1)); i++)
                        {
                            if (strData != "") { strData += ","; }
                            strData += @"""" + oDr.GetName(i).Replace("[", "").Replace("]", "") + @"""";
                        }
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "text/csv";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName.Replace(".xls", ".csv"));
                        HttpContext.Current.Response.Write(strData + "\r\n");
                        bolFirstPass = false;
                    }

                    // write actual data
                    strData = "";
                    for (int i = 0; (i <= (oDr.FieldCount - 1)); i++)
                    {
                        if (strData != "") { strData += ","; }
                        strData += @"""" + _global.Fld2Str(oDr[i]) + @"""";
                    }
                    HttpContext.Current.Response.Write(strData + "\r\n");
                }
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Thread was being aborted."))
                {
                    _global.WriteErrorLog("default.aspx.exportData() failed! Error is: " + ex.Message);
                }
            }
            finally
            {
                if (!(oCn == null))
                {
                    if ((oCn.State == ConnectionState.Open))
                    {
                        oCn.Close();
                    }
                }
                oDr.Close();
            }

        }

        public static string renderReport(string strMachineType, int intMachineDataHeaderId)
        {
            string strResults = "";

            string strReportFile = "../crystalReports/runData" + strMachineType + ".rpt";
            string strReportPath = HttpContext.Current.Server.MapPath(strReportFile);

            string strPDFFile = "../crystalReports/tempPDF/" + strMachineType + "_" + intMachineDataHeaderId.ToString() + "_.pdf";
            string strPDFPath = HttpContext.Current.Server.MapPath(strPDFFile);

            try
            {
                //load report
                ReportDocument oReport = new ReportDocument();
                oReport.Load(strReportPath, OpenReportMethod.OpenReportByTempCopy);

                //load connectionInfo
                ConnectionInfo crConnectionInfo = new ConnectionInfo();
                crConnectionInfo.ServerName = _global.server();
                crConnectionInfo.DatabaseName = _global.database();
                crConnectionInfo.UserID = _global.userId();
                crConnectionInfo.Password = _global.password();

                //set the ConnectionInfo for all tables in the main report        
                foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in oReport.Database.Tables)
                {
                    TableLogOnInfo crTableLogOnInfo = (TableLogOnInfo)crTable.LogOnInfo.Clone();
                    crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
                    crTable.ApplyLogOnInfo(crTableLogOnInfo);
                }

                //set parm values
                oReport.SetParameterValue("@MachineDataHeaderId", intMachineDataHeaderId);

                //set printer defaults
                oReport.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                oReport.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize;

                if (System.IO.File.Exists(strPDFFile)) { System.IO.File.Delete(strPDFFile); }
                oReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strPDFPath);

                strResults = strPDFFile;
            }
            catch (Exception ex)
            {
                strResults = "renderReport() failed! Error is: " + ex.Message;
            }

            return strResults;
        }
    }
}