using System.Collections.Generic;
using System.Web.Services;

namespace cambro
{
    [WebService(Namespace = "http://cambro.qa.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class _webMethods
    {
        [WebMethod(EnableSession = true)]
        public string renderReport(string machineType, string machineDataHeaderId)
        {
            return cambro._shared.renderReport(machineType, cambro._global.Fld2Int(machineDataHeaderId));
        }

        [WebMethod(EnableSession = true)]
        public List<string> getStatuses(string currentStatus)
        {
            var results = new List<string>();

            switch (currentStatus)
            {
                case "":
                    results.Add("VALIDATING");
                    break;

                case "VALIDATING":
                    results.Add("CERTIFIED");
                    results.Add("*blank");
                    break;

                case "CERTIFIED":
                    results.Add("VALIDATING");
                    results.Add("ECN APPROVED");
                    results.Add("*blank");
                    break;

                case "ECN APPROVED":
                    results.Add("VALIDATING");
                    results.Add("CERTIFIED");
                    results.Add("APPROVED");
                    results.Add("*blank");
                    break;

                default:
                    break;
            }

            return results;
        }

        [WebMethod(EnableSession = true)]
        public string updateStatus(string machineDataHeaderId, string newStatus)
        {
            string strError = "";

            //new status is required
            if (newStatus == "- Select One -")
            {
                strError = "Select a new status.";
                return strError;
            }

            strError = _database.updateMachineDataHeader(cambro._global.Fld2Int(machineDataHeaderId), newStatus);
            if (strError != "")
            {
                return strError;
            }
            else
            {
                return "";
            }
        }

        [WebMethod(EnableSession = true)]
        public string deleteRunSheet(string machineDataHeaderId)
        {
            return _database.deleteMachineDataHeader(cambro._global.Fld2Int(machineDataHeaderId));
        }
    }
}