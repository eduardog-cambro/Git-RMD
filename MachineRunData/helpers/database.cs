using System;
using System.IO;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace cambro
{
    public class _database
    {
        public struct postResults
        {
            public int recId;
            public string boilerPlate;
            public string errorMessage;
        }
        public static postResults postMachineDataHeader(int MachineDataHeaderId, string PlantNumber, string PartNumber, string ToolNumber, string MaterialNumber, string MaterialDescription, string MachineNumber, string MachineType, string TechName, string ResinAdditiveNumber, string ResinAdditiveDesc, string ColorantNumber, string ColorantDesc, double ColorPercentage, int ToolCavities, double McGuireSetting, double CertPartsWeightGm, double CycleTimeMin, double CycleTimeMid, double CycleTimeMax, double CycleTimeStd, double CycleTimeCert, double ShotWeightStd, double ShotWeightCert, double RunnerWeightGm, double FinalPartsWeightMin, double FinalPartsWeightMid, double FinalPartsWeightMax, string Comments, string RunComments1, string RunCommentsDate1, double RunCycleTime1, double RunShotWeight1, string RunComments2, string RunCommentsDate2, double RunCycleTime2, double RunShotWeight2, string RunComments3, string RunCommentsDate3, double RunCycleTime3, double RunShotWeight3)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_MachineDataHeader", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@PlantNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@PlantNumber"].Value = PlantNumber;

                oCmd.Parameters.Add(new SqlParameter("@PartNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@PartNumber"].Value = PartNumber.ToUpper();

                oCmd.Parameters.Add(new SqlParameter("@ToolNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@ToolNumber"].Value = ToolNumber.ToUpper();

                oCmd.Parameters.Add(new SqlParameter("@MaterialNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@MaterialNumber"].Value = MaterialNumber;

                oCmd.Parameters.Add(new SqlParameter("@MaterialDescription", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@MaterialDescription"].Value = MaterialDescription;

                oCmd.Parameters.Add(new SqlParameter("@MachineNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@MachineNumber"].Value = MachineNumber.ToUpper();

                oCmd.Parameters.Add(new SqlParameter("@MachineType", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@MachineType"].Value = MachineType;

                oCmd.Parameters.Add(new SqlParameter("@TechName", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@TechName"].Value = TechName;

                oCmd.Parameters.Add(new SqlParameter("@ResinAdditiveNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@ResinAdditiveNumber"].Value = ResinAdditiveNumber;

                oCmd.Parameters.Add(new SqlParameter("@ResinAdditiveDesc", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@ResinAdditiveDesc"].Value = ResinAdditiveDesc;

                oCmd.Parameters.Add(new SqlParameter("@ColorantNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@ColorantNumber"].Value = ColorantNumber;

                oCmd.Parameters.Add(new SqlParameter("@ColorantDesc", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@ColorantDesc"].Value = ColorantDesc;

                oCmd.Parameters.Add(new SqlParameter("@ColorPercentage", SqlDbType.Decimal));
                oCmd.Parameters["@ColorPercentage"].Value = ColorPercentage;

                oCmd.Parameters.Add(new SqlParameter("@ToolCavities", SqlDbType.Int));
                oCmd.Parameters["@ToolCavities"].Value = ToolCavities;

                oCmd.Parameters.Add(new SqlParameter("@McGuireSetting", SqlDbType.Decimal));
                oCmd.Parameters["@McGuireSetting"].Value = McGuireSetting;

                oCmd.Parameters.Add(new SqlParameter("@CertPartsWeightGm", SqlDbType.Decimal));
                oCmd.Parameters["@CertPartsWeightGm"].Value = CertPartsWeightGm;

                oCmd.Parameters.Add(new SqlParameter("@CycleTimeMin", SqlDbType.Decimal));
                oCmd.Parameters["@CycleTimeMin"].Value = CycleTimeMin;

                oCmd.Parameters.Add(new SqlParameter("@CycleTimeMid", SqlDbType.Decimal));
                oCmd.Parameters["@CycleTimeMid"].Value = CycleTimeMax;

                oCmd.Parameters.Add(new SqlParameter("@CycleTimeMax", SqlDbType.Decimal));
                oCmd.Parameters["@CycleTimeMax"].Value = CycleTimeMax;

                oCmd.Parameters.Add(new SqlParameter("@CycleTimeStd", SqlDbType.Decimal));
                oCmd.Parameters["@CycleTimeStd"].Value = CycleTimeStd;

                oCmd.Parameters.Add(new SqlParameter("@CycleTimeCert", SqlDbType.Decimal));
                oCmd.Parameters["@CycleTimeCert"].Value = CycleTimeCert;

                oCmd.Parameters.Add(new SqlParameter("@ShotWeightStd", SqlDbType.Decimal));
                oCmd.Parameters["@ShotWeightStd"].Value = ShotWeightStd;

                oCmd.Parameters.Add(new SqlParameter("@ShotWeightCert", SqlDbType.Decimal));
                oCmd.Parameters["@ShotWeightCert"].Value = ShotWeightCert;

                oCmd.Parameters.Add(new SqlParameter("@RunnerWeightGm", SqlDbType.Decimal));
                oCmd.Parameters["@RunnerWeightGm"].Value = RunnerWeightGm;

                oCmd.Parameters.Add(new SqlParameter("@FinalPartsWeightMin", SqlDbType.Decimal));
                oCmd.Parameters["@FinalPartsWeightMin"].Value = FinalPartsWeightMin;

                oCmd.Parameters.Add(new SqlParameter("@FinalPartsWeightMid", SqlDbType.Decimal));
                oCmd.Parameters["@FinalPartsWeightMid"].Value = FinalPartsWeightMid;

                oCmd.Parameters.Add(new SqlParameter("@FinalPartsWeightMax", SqlDbType.Decimal));
                oCmd.Parameters["@FinalPartsWeightMax"].Value = FinalPartsWeightMax;

                oCmd.Parameters.Add(new SqlParameter("@Comments", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@Comments"].Value = Comments;

                oCmd.Parameters.Add(new SqlParameter("@RunComments1", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@RunComments1"].Value = RunComments1;

                oCmd.Parameters.Add(new SqlParameter("@RunCommentsDate1", SqlDbType.VarChar, 10));
                oCmd.Parameters["@RunCommentsDate1"].Value = RunCommentsDate1;

                oCmd.Parameters.Add(new SqlParameter("@RunCycleTime1", SqlDbType.Decimal));
                oCmd.Parameters["@RunCycleTime1"].Value = RunCycleTime1;

                oCmd.Parameters.Add(new SqlParameter("@RunShotWeight1", SqlDbType.Decimal));
                oCmd.Parameters["@RunShotWeight1"].Value = RunShotWeight1;

                oCmd.Parameters.Add(new SqlParameter("@RunComments2", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@RunComments2"].Value = RunComments2;

                oCmd.Parameters.Add(new SqlParameter("@RunCommentsDate2", SqlDbType.VarChar, 10));
                oCmd.Parameters["@RunCommentsDate2"].Value = RunCommentsDate2;

                oCmd.Parameters.Add(new SqlParameter("@RunCycleTime2", SqlDbType.Decimal));
                oCmd.Parameters["@RunCycleTime2"].Value = RunCycleTime2;

                oCmd.Parameters.Add(new SqlParameter("@RunShotWeight2", SqlDbType.Decimal));
                oCmd.Parameters["@RunShotWeight2"].Value = RunShotWeight2;

                oCmd.Parameters.Add(new SqlParameter("@RunComments3", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@RunComments3"].Value = RunComments3;

                oCmd.Parameters.Add(new SqlParameter("@RunCommentsDate3", SqlDbType.VarChar, 10));
                oCmd.Parameters["@RunCommentsDate3"].Value = RunCommentsDate3;

                oCmd.Parameters.Add(new SqlParameter("@RunCycleTime3", SqlDbType.Decimal));
                oCmd.Parameters["@RunCycleTime3"].Value = RunCycleTime3;

                oCmd.Parameters.Add(new SqlParameter("@RunShotWeight3", SqlDbType.Decimal));
                oCmd.Parameters["@RunShotWeight3"].Value = RunShotWeight3;

                oCmd.Parameters.Add(new SqlParameter("@_MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@_MachineDataHeaderId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_MachineDataHeaderId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postMachineDataHeader() failed! Error is: " + ex.Message));
                results.errorMessage = "postMachineDataHeader() failed. contact user support";
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
            }
            return results;
        }
        public static string deleteMachineDataHeader(int MachineDataHeaderId)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("delete_MachineDataHeader", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.deleteMachineDataHeader() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }
        public static string updateMachineDataHeader(int MachineDataHeaderId, string NewStatus)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("update_MachineDataHeader", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@NewStatus", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@NewStatus"].Value = NewStatus;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.updateMachineDataHeader() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static postResults postBarrelTemperatures(int RecId, int MachineDataHeaderId, string NozzleTipPercent, string NozzleBodyPercent, string Adapter, int SHHeat, int BarrelHead, int BarrelFront, int BarrelCenter1, int BarrelCenter2, int BarrelCenter3, int BarrelCenter4, int BarrelCenter5, int BarrelRear)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_BarrelTemperatures", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@NozzleTipPercent", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@NozzleTipPercent"].Value = NozzleTipPercent;

                oCmd.Parameters.Add(new SqlParameter("@NozzleBodyPercent", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@NozzleBodyPercent"].Value = NozzleBodyPercent;

                oCmd.Parameters.Add(new SqlParameter("@Adapter", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@Adapter"].Value = Adapter;

                oCmd.Parameters.Add(new SqlParameter("@SHHeat", SqlDbType.Int));
                oCmd.Parameters["@SHHeat"].Value = SHHeat;

                oCmd.Parameters.Add(new SqlParameter("@BarrelHead", SqlDbType.Int));
                oCmd.Parameters["@BarrelHead"].Value = BarrelHead;

                oCmd.Parameters.Add(new SqlParameter("@BarrelFront", SqlDbType.Int));
                oCmd.Parameters["@BarrelFront"].Value = BarrelFront;

                oCmd.Parameters.Add(new SqlParameter("@BarrelCenter1", SqlDbType.Int));
                oCmd.Parameters["@BarrelCenter1"].Value = BarrelCenter1;

                oCmd.Parameters.Add(new SqlParameter("@BarrelCenter2", SqlDbType.Int));
                oCmd.Parameters["@BarrelCenter2"].Value = BarrelCenter2;

                oCmd.Parameters.Add(new SqlParameter("@BarrelCenter3", SqlDbType.Int));
                oCmd.Parameters["@BarrelCenter3"].Value = BarrelCenter3;

                oCmd.Parameters.Add(new SqlParameter("@BarrelCenter4", SqlDbType.Int));
                oCmd.Parameters["@BarrelCenter4"].Value = BarrelCenter4;

                oCmd.Parameters.Add(new SqlParameter("@BarrelCenter5", SqlDbType.Int));
                oCmd.Parameters["@BarrelCenter5"].Value = BarrelCenter5;

                oCmd.Parameters.Add(new SqlParameter("@BarrelRear", SqlDbType.Int));
                oCmd.Parameters["@BarrelRear"].Value = BarrelRear;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postBarrelTemperatures() failed! Error is: " + ex.Message));
                results.errorMessage = "postBarrelTemperatures() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postGasAssistData(int RecId, int MachineDataHeaderId, double Delay_C1, double Hold_C1, double Exhaust_C1, double Pressure1_C1, double Time1_C1, double Pressure2_C1, double Time2_C1, double Delay_C2, double Hold_C2, double Exhaust_C2, double Pressure1_C2, double Time1_C2, double Pressure2_C2, double Time2_C2, double Delay_C3, double Hold_C3, double Exhaust_C3, double Pressure1_C3, double Time1_C3, double Pressure2_C3, double Time2_C3, double Delay_C4, double Hold_C4, double Exhaust_C4, double Pressure1_C4, double Time1_C4, double Pressure2_C4, double Time2_C4)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_GasAssistData", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@Delay_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Delay_C1"].Value = Delay_C1;

                oCmd.Parameters.Add(new SqlParameter("@Hold_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Hold_C1"].Value = Hold_C1;

                oCmd.Parameters.Add(new SqlParameter("@Exhaust_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Exhaust_C1"].Value = Exhaust_C1;

                oCmd.Parameters.Add(new SqlParameter("@Pressure1_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure1_C1"].Value = Pressure1_C1;

                oCmd.Parameters.Add(new SqlParameter("@Time1_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Time1_C1"].Value = Time1_C1;

                oCmd.Parameters.Add(new SqlParameter("@Pressure2_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure2_C1"].Value = Pressure2_C1;

                oCmd.Parameters.Add(new SqlParameter("@Time2_C1", SqlDbType.Decimal));
                oCmd.Parameters["@Time2_C1"].Value = Time2_C1;

                oCmd.Parameters.Add(new SqlParameter("@Delay_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Delay_C2"].Value = Delay_C2;

                oCmd.Parameters.Add(new SqlParameter("@Hold_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Hold_C2"].Value = Hold_C2;

                oCmd.Parameters.Add(new SqlParameter("@Exhaust_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Exhaust_C2"].Value = Exhaust_C2;

                oCmd.Parameters.Add(new SqlParameter("@Pressure1_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure1_C2"].Value = Pressure1_C2;

                oCmd.Parameters.Add(new SqlParameter("@Time1_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Time1_C2"].Value = Time1_C2;

                oCmd.Parameters.Add(new SqlParameter("@Pressure2_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure2_C2"].Value = Pressure2_C2;

                oCmd.Parameters.Add(new SqlParameter("@Time2_C2", SqlDbType.Decimal));
                oCmd.Parameters["@Time2_C2"].Value = Time2_C2;

                oCmd.Parameters.Add(new SqlParameter("@Delay_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Delay_C3"].Value = Delay_C3;

                oCmd.Parameters.Add(new SqlParameter("@Hold_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Hold_C3"].Value = Hold_C3;

                oCmd.Parameters.Add(new SqlParameter("@Exhaust_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Exhaust_C3"].Value = Exhaust_C3;

                oCmd.Parameters.Add(new SqlParameter("@Pressure1_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure1_C3"].Value = Pressure1_C3;

                oCmd.Parameters.Add(new SqlParameter("@Time1_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Time1_C3"].Value = Time1_C3;

                oCmd.Parameters.Add(new SqlParameter("@Pressure2_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure2_C3"].Value = Pressure2_C3;

                oCmd.Parameters.Add(new SqlParameter("@Time2_C3", SqlDbType.Decimal));
                oCmd.Parameters["@Time2_C3"].Value = Time2_C3;

                oCmd.Parameters.Add(new SqlParameter("@Delay_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Delay_C4"].Value = Delay_C4;

                oCmd.Parameters.Add(new SqlParameter("@Hold_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Hold_C4"].Value = Hold_C4;

                oCmd.Parameters.Add(new SqlParameter("@Exhaust_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Exhaust_C4"].Value = Exhaust_C4;

                oCmd.Parameters.Add(new SqlParameter("@Pressure1_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure1_C4"].Value = Pressure1_C4;

                oCmd.Parameters.Add(new SqlParameter("@Time1_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Time1_C4"].Value = Time1_C4;

                oCmd.Parameters.Add(new SqlParameter("@Pressure2_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Pressure2_C4"].Value = Pressure2_C4;

                oCmd.Parameters.Add(new SqlParameter("@Time2_C4", SqlDbType.Decimal));
                oCmd.Parameters["@Time2_C4"].Value = Time2_C4;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postGasAssistData() failed! Error is: " + ex.Message));
                results.errorMessage = "postGasAssistData() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postHotTipController(int RecId, int MachineDataHeaderId, int Gate_HB_Man_Pos1_Box1, int Gate_HB_Man_Pos2_Box1, int Gate_HB_Man_Pos3_Box1, int Gate_HB_Man_Pos4_Box1, int Gate_HB_Man_Pos5_Box1, int Gate_HB_Man_Pos6_Box1, int Gate_HB_Man_Pos1_Box2, int Gate_HB_Man_Pos2_Box2, int Gate_HB_Man_Pos3_Box2, int Gate_HB_Man_Pos4_Box2, int Gate_HB_Man_Pos5_Box2, int Gate_HB_Man_Pos6_Box2, int Gate_HB_Man_Pos1_Box3, int Gate_HB_Man_Pos2_Box3, int Gate_HB_Man_Pos3_Box3, int Gate_HB_Man_Pos4_Box3, int Gate_HB_Man_Pos5_Box3, int Gate_HB_Man_Pos6_Box3, int Gate_HB_Man_Pos1_Box4, int Gate_HB_Man_Pos2_Box4, int Gate_HB_Man_Pos3_Box4, int Gate_HB_Man_Pos4_Box4, int Gate_HB_Man_Pos5_Box4, int Gate_HB_Man_Pos6_Box4, int Gate_HB_Man_Pos1_Box5, int Gate_HB_Man_Pos2_Box5, int Gate_HB_Man_Pos3_Box5, int Gate_HB_Man_Pos4_Box5, int Gate_HB_Man_Pos5_Box5, int Gate_HB_Man_Pos6_Box5, int Gate_HB_Man_Pos1_Box6, int Gate_HB_Man_Pos2_Box6, int Gate_HB_Man_Pos3_Box6, int Gate_HB_Man_Pos4_Box6, int Gate_HB_Man_Pos5_Box6, int Gate_HB_Man_Pos6_Box6)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_HotTipController", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box1"].Value = Gate_HB_Man_Pos1_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box1"].Value = Gate_HB_Man_Pos2_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box1"].Value = Gate_HB_Man_Pos3_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box1"].Value = Gate_HB_Man_Pos4_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box1"].Value = Gate_HB_Man_Pos5_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box1", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box1"].Value = Gate_HB_Man_Pos6_Box1;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box2"].Value = Gate_HB_Man_Pos1_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box2"].Value = Gate_HB_Man_Pos2_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box2"].Value = Gate_HB_Man_Pos3_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box2"].Value = Gate_HB_Man_Pos4_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box2"].Value = Gate_HB_Man_Pos5_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box2", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box2"].Value = Gate_HB_Man_Pos6_Box2;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box3"].Value = Gate_HB_Man_Pos1_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box3"].Value = Gate_HB_Man_Pos2_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box3"].Value = Gate_HB_Man_Pos3_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box3"].Value = Gate_HB_Man_Pos4_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box3"].Value = Gate_HB_Man_Pos5_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box3", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box3"].Value = Gate_HB_Man_Pos6_Box3;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box4"].Value = Gate_HB_Man_Pos1_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box4"].Value = Gate_HB_Man_Pos2_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box4"].Value = Gate_HB_Man_Pos3_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box4"].Value = Gate_HB_Man_Pos4_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box4"].Value = Gate_HB_Man_Pos5_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box4", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box4"].Value = Gate_HB_Man_Pos6_Box4;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box5"].Value = Gate_HB_Man_Pos1_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box5"].Value = Gate_HB_Man_Pos2_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box5"].Value = Gate_HB_Man_Pos3_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box5"].Value = Gate_HB_Man_Pos4_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box5"].Value = Gate_HB_Man_Pos5_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box5", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box5"].Value = Gate_HB_Man_Pos6_Box5;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos1_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos1_Box6"].Value = Gate_HB_Man_Pos1_Box6;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos2_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos2_Box6"].Value = Gate_HB_Man_Pos2_Box6;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos3_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos3_Box6"].Value = Gate_HB_Man_Pos3_Box6;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos4_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos4_Box6"].Value = Gate_HB_Man_Pos4_Box6;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos5_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos5_Box6"].Value = Gate_HB_Man_Pos5_Box6;

                oCmd.Parameters.Add(new SqlParameter("@Gate_HB_Man_Pos6_Box6", SqlDbType.Int));
                oCmd.Parameters["@Gate_HB_Man_Pos6_Box6"].Value = Gate_HB_Man_Pos6_Box6;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postHotTipController() failed! Error is: " + ex.Message));
                results.errorMessage = "postHotTipController() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postInjectionProfile(int RecId, int MachineDataHeaderId, double ShotSize, double InjectionVelocity1, double InjectionVelocity2, double InjectionVelocity3, double InjectionVelocity4, double InjectionVelocity5, double InjectionChangePos1, double InjectionChangePos2, double InjectionChangePos3, double InjectionChangePos4, double InjectionPress1, double InjectionPress2, double InjectionPress3, double InjectionPress4, double InjectionPress5, int InjectionFlow1, int InjectionFlow2, int InjectionFlow3, int InjectionFlow4, int InjectionFlow5, string TransMode, double TransPosition, int IPS_PSI, double InjectionPressureThreshold, double InjectionPositionThreshold, double InjectionTimeThreshold, int InjectionPressLimit, int InjectionPSIAtTransfer, double InjectionTimeAct, int HoldPress1_PSI, int HoldPress2_PSI, int HoldPress1_Percent, int HoldPress2_Percent, double HoldPress1_Seconds, double HoldPress2_Seconds, int HoldFlow1, int HoldFlow2, double HoldTime1, double HoldTime2, double InjectionHoldTime, double FinalCushionMM)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_InjectionProfile", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@ShotSize", SqlDbType.Decimal));
                oCmd.Parameters["@ShotSize"].Value = ShotSize;

                oCmd.Parameters.Add(new SqlParameter("@InjectionVelocity1", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionVelocity1"].Value = InjectionVelocity1;

                oCmd.Parameters.Add(new SqlParameter("@InjectionVelocity2", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionVelocity2"].Value = InjectionVelocity2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionVelocity3", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionVelocity3"].Value = InjectionVelocity3;

                oCmd.Parameters.Add(new SqlParameter("@InjectionVelocity4", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionVelocity4"].Value = InjectionVelocity4;

                oCmd.Parameters.Add(new SqlParameter("@InjectionVelocity5", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionVelocity5"].Value = InjectionVelocity5;

                oCmd.Parameters.Add(new SqlParameter("@InjectionChangePos1", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionChangePos1"].Value = InjectionChangePos1;

                oCmd.Parameters.Add(new SqlParameter("@InjectionChangePos2", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionChangePos2"].Value = InjectionChangePos2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionChangePos3", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionChangePos3"].Value = InjectionChangePos3;

                oCmd.Parameters.Add(new SqlParameter("@InjectionChangePos4", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionChangePos4"].Value = InjectionChangePos4;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPress1", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPress1"].Value = InjectionPress1;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPress2", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPress2"].Value = InjectionPress2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPress3", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPress3"].Value = InjectionPress3;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPress4", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPress4"].Value = InjectionPress4;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPress5", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPress5"].Value = InjectionPress5;

                oCmd.Parameters.Add(new SqlParameter("@InjectionFlow1", SqlDbType.Int));
                oCmd.Parameters["@InjectionFlow1"].Value = InjectionFlow1;

                oCmd.Parameters.Add(new SqlParameter("@InjectionFlow2", SqlDbType.Int));
                oCmd.Parameters["@InjectionFlow2"].Value = InjectionFlow2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionFlow3", SqlDbType.Int));
                oCmd.Parameters["@InjectionFlow3"].Value = InjectionFlow3;

                oCmd.Parameters.Add(new SqlParameter("@InjectionFlow4", SqlDbType.Int));
                oCmd.Parameters["@InjectionFlow4"].Value = InjectionFlow4;

                oCmd.Parameters.Add(new SqlParameter("@InjectionFlow5", SqlDbType.Int));
                oCmd.Parameters["@InjectionFlow5"].Value = InjectionFlow5;

                oCmd.Parameters.Add(new SqlParameter("@TransMode", SqlDbType.VarChar, 20));
                oCmd.Parameters["@TransMode"].Value = TransMode;

                oCmd.Parameters.Add(new SqlParameter("@TransPosition", SqlDbType.Decimal));
                oCmd.Parameters["@TransPosition"].Value = TransPosition;

                oCmd.Parameters.Add(new SqlParameter("@IPS_PSI", SqlDbType.Int));
                oCmd.Parameters["@IPS_PSI"].Value = IPS_PSI;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPressureThreshold", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPressureThreshold"].Value = InjectionPressureThreshold;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPositionThreshold", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionPositionThreshold"].Value = InjectionPositionThreshold;

                oCmd.Parameters.Add(new SqlParameter("@InjectionTimeThreshold", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionTimeThreshold"].Value = InjectionTimeThreshold;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPressLimit", SqlDbType.Int));
                oCmd.Parameters["@InjectionPressLimit"].Value = InjectionPressLimit;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPSIAtTransfer", SqlDbType.Int));
                oCmd.Parameters["@InjectionPSIAtTransfer"].Value = InjectionPSIAtTransfer;

                oCmd.Parameters.Add(new SqlParameter("@InjectionTimeAct", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionTimeAct"].Value = InjectionTimeAct;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress1_PSI", SqlDbType.Int));
                oCmd.Parameters["@HoldPress1_PSI"].Value = HoldPress1_PSI;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress2_PSI", SqlDbType.Int));
                oCmd.Parameters["@HoldPress2_PSI"].Value = HoldPress2_PSI;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress1_Percent", SqlDbType.Int));
                oCmd.Parameters["@HoldPress1_Percent"].Value = HoldPress1_Percent;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress2_Percent", SqlDbType.Int));
                oCmd.Parameters["@HoldPress2_Percent"].Value = HoldPress2_Percent;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress1_Seconds", SqlDbType.Decimal));
                oCmd.Parameters["@HoldPress1_Seconds"].Value = HoldPress1_Seconds;

                oCmd.Parameters.Add(new SqlParameter("@HoldPress2_Seconds", SqlDbType.Decimal));
                oCmd.Parameters["@HoldPress2_Seconds"].Value = HoldPress2_Seconds;

                oCmd.Parameters.Add(new SqlParameter("@HoldFlow1", SqlDbType.Int));
                oCmd.Parameters["@HoldFlow1"].Value = HoldFlow1;

                oCmd.Parameters.Add(new SqlParameter("@HoldFlow2", SqlDbType.Int));
                oCmd.Parameters["@HoldFlow2"].Value = HoldFlow2;

                oCmd.Parameters.Add(new SqlParameter("@HoldTime1", SqlDbType.Decimal));
                oCmd.Parameters["@HoldTime1"].Value = HoldTime1;

                oCmd.Parameters.Add(new SqlParameter("@HoldTime2", SqlDbType.Decimal));
                oCmd.Parameters["@HoldTime2"].Value = HoldTime2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionHoldTime", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionHoldTime"].Value = InjectionHoldTime;

                oCmd.Parameters.Add(new SqlParameter("@FinalCushionMM", SqlDbType.Decimal));
                oCmd.Parameters["@FinalCushionMM"].Value = FinalCushionMM;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postInjectionProfile() failed! Error is: " + ex.Message));
                results.errorMessage = "postInjectionProfile() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postMoldCoolingTemps(int RecId, int MachineDataHeaderId, string MoldGateTempF, string MoldFixedHalfTempF, string MoldMovingHalfTempF, string StripperOrOtherTempF)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_MoldCoolingTemps", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@MoldGateTempF", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@MoldGateTempF"].Value = MoldGateTempF;

                oCmd.Parameters.Add(new SqlParameter("@MoldFixedHalfTempF", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@MoldFixedHalfTempF"].Value = MoldFixedHalfTempF;

                oCmd.Parameters.Add(new SqlParameter("@MoldMovingHalfTempF", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@MoldMovingHalfTempF"].Value = MoldMovingHalfTempF;

                oCmd.Parameters.Add(new SqlParameter("@StripperOrOtherTempF", SqlDbType.NVarChar, 20));
                oCmd.Parameters["@StripperOrOtherTempF"].Value = StripperOrOtherTempF;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postMoldCoolingTemps() failed! Error is: " + ex.Message));
                results.errorMessage = "postMoldCoolingTemps() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postNozzleInformation(int RecId, int MachineDataHeaderId, string NozzleTipType, string NozzleTipLengthOALInches, string NozzleTipOrificeSizeInches, string NozzleBodyLengthOALInches)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_NozzleInformation", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@NozzleTipType", SqlDbType.NVarChar, 10));
                oCmd.Parameters["@NozzleTipType"].Value = NozzleTipType;

                oCmd.Parameters.Add(new SqlParameter("@NozzleTipLengthOALInches", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@NozzleTipLengthOALInches"].Value = NozzleTipLengthOALInches;

                oCmd.Parameters.Add(new SqlParameter("@NozzleTipOrificeSizeInches", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@NozzleTipOrificeSizeInches"].Value = NozzleTipOrificeSizeInches;

                oCmd.Parameters.Add(new SqlParameter("@NozzleBodyLengthOALInches", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@NozzleBodyLengthOALInches"].Value = NozzleBodyLengthOALInches;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postNozzleInformation() failed! Error is: " + ex.Message));
                results.errorMessage = "postNozzleInformation() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postRecoveryClampProfile(int RecId, int MachineDataHeaderId, double PrePullback, double PrePullbackStroke, double SuckbackBeforePosition, double SuckbackAfterPosition, double ScrewStartDelay, double ChargePosition1, double ChargePosition2, int ChargePressure1, int ChargePressure2, int ChargeFlow1, int ChargeFlow2, double ScrewRecovery1Percent, double ScrewRecovery2Percent, int ScrewRecovery1RPM, int ScrewRecovery2RPM, double BackPressure1, double BackPressure2, double PostPullback, double PostPullbackSpeedPercent, double PostPullbackStroke, double ScrewPositonAfter, double ScrewRecoveryTime, double ScrewChargeTime, double CoolingTimeSeconds, double MoldProtectPressure, int MoldProtectFlow, double MoldProtectTime, int ClampTonnage, int ClampHighPressurePSI, int ClampHighPressureFlow, double ClampOpenPosition, double EjectorStroke, int InjectionPressureGaugePSI, int HoldingGauge1_PSI, int HoldingGauge2_PSI, int BackPressureGauge1_PSI, int BackPressureGauge2_PSI, double FinalCushion)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_RecoveryClampProfile", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@PrePullback", SqlDbType.Decimal));
                oCmd.Parameters["@PrePullback"].Value = PrePullback;

                oCmd.Parameters.Add(new SqlParameter("@PrePullbackStroke", SqlDbType.Decimal));
                oCmd.Parameters["@PrePullbackStroke"].Value = PrePullbackStroke;

                oCmd.Parameters.Add(new SqlParameter("@SuckbackBeforePosition", SqlDbType.Decimal));
                oCmd.Parameters["@SuckbackBeforePosition"].Value = SuckbackBeforePosition;

                oCmd.Parameters.Add(new SqlParameter("@SuckbackAfterPosition", SqlDbType.Decimal));
                oCmd.Parameters["@SuckbackAfterPosition"].Value = SuckbackAfterPosition;

                oCmd.Parameters.Add(new SqlParameter("@ScrewStartDelay", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewStartDelay"].Value = ScrewStartDelay;

                oCmd.Parameters.Add(new SqlParameter("@ChargePosition1", SqlDbType.Decimal));
                oCmd.Parameters["@ChargePosition1"].Value = ChargePosition1;

                oCmd.Parameters.Add(new SqlParameter("@ChargePosition2", SqlDbType.Decimal));
                oCmd.Parameters["@ChargePosition2"].Value = ChargePosition2;

                oCmd.Parameters.Add(new SqlParameter("@ChargePressure1", SqlDbType.Int));
                oCmd.Parameters["@ChargePressure1"].Value = ChargePressure1;

                oCmd.Parameters.Add(new SqlParameter("@ChargePressure2", SqlDbType.Int));
                oCmd.Parameters["@ChargePressure2"].Value = ChargePressure2;

                oCmd.Parameters.Add(new SqlParameter("@ChargeFlow1", SqlDbType.Int));
                oCmd.Parameters["@ChargeFlow1"].Value = ChargeFlow1;

                oCmd.Parameters.Add(new SqlParameter("@ChargeFlow2", SqlDbType.Int));
                oCmd.Parameters["@ChargeFlow2"].Value = ChargeFlow2;

                oCmd.Parameters.Add(new SqlParameter("@ScrewRecovery1Percent", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewRecovery1Percent"].Value = ScrewRecovery1Percent;

                oCmd.Parameters.Add(new SqlParameter("@ScrewRecovery2Percent", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewRecovery2Percent"].Value = ScrewRecovery2Percent;

                oCmd.Parameters.Add(new SqlParameter("@ScrewRecovery1RPM", SqlDbType.Int));
                oCmd.Parameters["@ScrewRecovery1RPM"].Value = ScrewRecovery1RPM;

                oCmd.Parameters.Add(new SqlParameter("@ScrewRecovery2RPM", SqlDbType.Int));
                oCmd.Parameters["@ScrewRecovery2RPM"].Value = ScrewRecovery2RPM;

                oCmd.Parameters.Add(new SqlParameter("@BackPressure1", SqlDbType.Decimal));
                oCmd.Parameters["@BackPressure1"].Value = BackPressure1;

                oCmd.Parameters.Add(new SqlParameter("@BackPressure2", SqlDbType.Decimal));
                oCmd.Parameters["@BackPressure2"].Value = BackPressure2;

                oCmd.Parameters.Add(new SqlParameter("@PostPullback", SqlDbType.Decimal));
                oCmd.Parameters["@PostPullback"].Value = PostPullback;

                oCmd.Parameters.Add(new SqlParameter("@PostPullbackSpeedPercent", SqlDbType.Decimal));
                oCmd.Parameters["@PostPullbackSpeedPercent"].Value = PostPullbackSpeedPercent;

                oCmd.Parameters.Add(new SqlParameter("@PostPullbackStroke", SqlDbType.Decimal));
                oCmd.Parameters["@PostPullbackStroke"].Value = PostPullbackStroke;

                oCmd.Parameters.Add(new SqlParameter("@ScrewPositonAfter", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewPositonAfter"].Value = ScrewPositonAfter;

                oCmd.Parameters.Add(new SqlParameter("@ScrewRecoveryTime", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewRecoveryTime"].Value = ScrewRecoveryTime;

                oCmd.Parameters.Add(new SqlParameter("@ScrewChargeTime", SqlDbType.Decimal));
                oCmd.Parameters["@ScrewChargeTime"].Value = ScrewChargeTime;

                oCmd.Parameters.Add(new SqlParameter("@CoolingTimeSeconds", SqlDbType.Decimal));
                oCmd.Parameters["@CoolingTimeSeconds"].Value = CoolingTimeSeconds;

                oCmd.Parameters.Add(new SqlParameter("@MoldProtectPressure", SqlDbType.Decimal));
                oCmd.Parameters["@MoldProtectPressure"].Value = MoldProtectPressure;

                oCmd.Parameters.Add(new SqlParameter("@MoldProtectFlow", SqlDbType.Int));
                oCmd.Parameters["@MoldProtectFlow"].Value = MoldProtectFlow;

                oCmd.Parameters.Add(new SqlParameter("@MoldProtectTime", SqlDbType.Decimal));
                oCmd.Parameters["@MoldProtectTime"].Value = MoldProtectTime;

                oCmd.Parameters.Add(new SqlParameter("@ClampTonnage", SqlDbType.Int));
                oCmd.Parameters["@ClampTonnage"].Value = ClampTonnage;

                oCmd.Parameters.Add(new SqlParameter("@ClampHighPressurePSI", SqlDbType.Int));
                oCmd.Parameters["@ClampHighPressurePSI"].Value = ClampHighPressurePSI;

                oCmd.Parameters.Add(new SqlParameter("@ClampHighPressureFlow", SqlDbType.Int));
                oCmd.Parameters["@ClampHighPressureFlow"].Value = ClampHighPressureFlow;

                oCmd.Parameters.Add(new SqlParameter("@ClampOpenPosition", SqlDbType.Decimal));
                oCmd.Parameters["@ClampOpenPosition"].Value = ClampOpenPosition;

                oCmd.Parameters.Add(new SqlParameter("@EjectorStroke", SqlDbType.Decimal));
                oCmd.Parameters["@EjectorStroke"].Value = EjectorStroke;

                oCmd.Parameters.Add(new SqlParameter("@InjectionPressureGaugePSI", SqlDbType.Int));
                oCmd.Parameters["@InjectionPressureGaugePSI"].Value = InjectionPressureGaugePSI;

                oCmd.Parameters.Add(new SqlParameter("@HoldingGauge1_PSI", SqlDbType.Int));
                oCmd.Parameters["@HoldingGauge1_PSI"].Value = HoldingGauge1_PSI;

                oCmd.Parameters.Add(new SqlParameter("@HoldingGauge2_PSI", SqlDbType.Int));
                oCmd.Parameters["@HoldingGauge2_PSI"].Value = HoldingGauge2_PSI;

                oCmd.Parameters.Add(new SqlParameter("@BackPressureGauge1_PSI", SqlDbType.Int));
                oCmd.Parameters["@BackPressureGauge1_PSI"].Value = BackPressureGauge1_PSI;

                oCmd.Parameters.Add(new SqlParameter("@BackPressureGauge2_PSI", SqlDbType.Int));
                oCmd.Parameters["@BackPressureGauge2_PSI"].Value = BackPressureGauge2_PSI;

                oCmd.Parameters.Add(new SqlParameter("@FinalCushion", SqlDbType.Decimal));
                oCmd.Parameters["@FinalCushion"].Value = FinalCushion;


                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.debugSQL("post_RecoveryClampProfile", oCmd);

                _global.WriteErrorLog(("_database.postRecoveryClampProfile() failed! Error is: " + ex.Message));
                results.errorMessage = "postRecoveryClampProfile() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postReferenceData(int RecId, int MachineDataHeaderId, double FillOnlyTime, double FillOnlyWeight, int SteelTempSide_A, int SteelTempSide_B, int MeltTemp)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_ReferenceData", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@FillOnlyTime", SqlDbType.Decimal));
                oCmd.Parameters["@FillOnlyTime"].Value = FillOnlyTime;

                oCmd.Parameters.Add(new SqlParameter("@FillOnlyWeight", SqlDbType.Decimal));
                oCmd.Parameters["@FillOnlyWeight"].Value = FillOnlyWeight;

                oCmd.Parameters.Add(new SqlParameter("@SteelTempSide_A", SqlDbType.Int));
                oCmd.Parameters["@SteelTempSide_A"].Value = SteelTempSide_A;

                oCmd.Parameters.Add(new SqlParameter("@SteelTempSide_B", SqlDbType.Int));
                oCmd.Parameters["@SteelTempSide_B"].Value = SteelTempSide_B;

                oCmd.Parameters.Add(new SqlParameter("@MeltTemp", SqlDbType.Int));
                oCmd.Parameters["@MeltTemp"].Value = MeltTemp;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postReferenceData() failed! Error is: " + ex.Message));
                results.errorMessage = "postReferenceData() failed. contact user support";
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
            }
            return results;
        }
        public static postResults postValveGateData(int RecId, int MachineDataHeaderId, double OpenDelay_VG1, double OpenTime_VG1, double OpenPostion_VG1, double OpenHold_VG1, double CloseDelay_VG1, double CloseTime_VG1, double ClosePostion_VG1, double CloseHold_VG1, double HoldDelay_OnOff_VG1, double HoldActive_OnOff_VG1, double AdvCloseTime_VG1, double InjectionHPEndTime_VG1, double Transfer_VG1, double PSI_VG1, double OpenDelay_VG2, double OpenTime_VG2, double OpenPostion_VG2, double OpenHold_VG2, double CloseDelay_VG2, double CloseTime_VG2, double ClosePostion_VG2, double CloseHold_VG2, double HoldDelay_OnOff_VG2, double HoldActive_OnOff_VG2, double AdvCloseTime_VG2, double InjectionHPEndTime_VG2, double Transfer_VG2, double PSI_VG2, double OpenDelay_VG3, double OpenTime_VG3, double OpenPostion_VG3, double OpenHold_VG3, double CloseDelay_VG3, double CloseTime_VG3, double ClosePostion_VG3, double CloseHold_VG3, double HoldDelay_OnOff_VG3, double HoldActive_OnOff_VG3, double AdvCloseTime_VG3, double InjectionHPEndTime_VG3, double Transfer_VG3, double PSI_VG3, double OpenDelay_VG4, double OpenTime_VG4, double OpenPostion_VG4, double OpenHold_VG4, double CloseDelay_VG4, double CloseTime_VG4, double ClosePostion_VG4, double CloseHold_VG4, double HoldDelay_OnOff_VG4, double HoldActive_OnOff_VG4, double AdvCloseTime_VG4, double InjectionHPEndTime_VG4, double Transfer_VG4, double PSI_VG4)
        {
            postResults results = new postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;
            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("post_ValveGateData", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@UserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RecId", SqlDbType.Int));
                oCmd.Parameters["@RecId"].Value = RecId;

                oCmd.Parameters.Add(new SqlParameter("@MachineDataHeaderId", SqlDbType.Int));
                oCmd.Parameters["@MachineDataHeaderId"].Value = MachineDataHeaderId;

                oCmd.Parameters.Add(new SqlParameter("@OpenDelay_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@OpenDelay_VG1"].Value = OpenDelay_VG1;

                oCmd.Parameters.Add(new SqlParameter("@OpenTime_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@OpenTime_VG1"].Value = OpenTime_VG1;

                oCmd.Parameters.Add(new SqlParameter("@OpenPostion_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@OpenPostion_VG1"].Value = OpenPostion_VG1;

                oCmd.Parameters.Add(new SqlParameter("@OpenHold_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@OpenHold_VG1"].Value = OpenHold_VG1;

                oCmd.Parameters.Add(new SqlParameter("@CloseDelay_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@CloseDelay_VG1"].Value = CloseDelay_VG1;

                oCmd.Parameters.Add(new SqlParameter("@CloseTime_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@CloseTime_VG1"].Value = CloseTime_VG1;

                oCmd.Parameters.Add(new SqlParameter("@ClosePostion_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@ClosePostion_VG1"].Value = ClosePostion_VG1;

                oCmd.Parameters.Add(new SqlParameter("@CloseHold_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@CloseHold_VG1"].Value = CloseHold_VG1;

                oCmd.Parameters.Add(new SqlParameter("@HoldDelay_OnOff_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@HoldDelay_OnOff_VG1"].Value = HoldDelay_OnOff_VG1;

                oCmd.Parameters.Add(new SqlParameter("@HoldActive_OnOff_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@HoldActive_OnOff_VG1"].Value = HoldActive_OnOff_VG1;

                oCmd.Parameters.Add(new SqlParameter("@AdvCloseTime_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@AdvCloseTime_VG1"].Value = AdvCloseTime_VG1;

                oCmd.Parameters.Add(new SqlParameter("@InjectionHPEndTime_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionHPEndTime_VG1"].Value = InjectionHPEndTime_VG1;

                oCmd.Parameters.Add(new SqlParameter("@Transfer_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@Transfer_VG1"].Value = Transfer_VG1;

                oCmd.Parameters.Add(new SqlParameter("@PSI_VG1", SqlDbType.Decimal));
                oCmd.Parameters["@PSI_VG1"].Value = PSI_VG1;

                oCmd.Parameters.Add(new SqlParameter("@OpenDelay_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@OpenDelay_VG2"].Value = OpenDelay_VG2;

                oCmd.Parameters.Add(new SqlParameter("@OpenTime_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@OpenTime_VG2"].Value = OpenTime_VG2;

                oCmd.Parameters.Add(new SqlParameter("@OpenPostion_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@OpenPostion_VG2"].Value = OpenPostion_VG2;

                oCmd.Parameters.Add(new SqlParameter("@OpenHold_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@OpenHold_VG2"].Value = OpenHold_VG2;

                oCmd.Parameters.Add(new SqlParameter("@CloseDelay_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@CloseDelay_VG2"].Value = CloseDelay_VG2;

                oCmd.Parameters.Add(new SqlParameter("@CloseTime_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@CloseTime_VG2"].Value = CloseTime_VG2;

                oCmd.Parameters.Add(new SqlParameter("@ClosePostion_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@ClosePostion_VG2"].Value = ClosePostion_VG2;

                oCmd.Parameters.Add(new SqlParameter("@CloseHold_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@CloseHold_VG2"].Value = CloseHold_VG2;

                oCmd.Parameters.Add(new SqlParameter("@HoldDelay_OnOff_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@HoldDelay_OnOff_VG2"].Value = HoldDelay_OnOff_VG2;

                oCmd.Parameters.Add(new SqlParameter("@HoldActive_OnOff_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@HoldActive_OnOff_VG2"].Value = HoldActive_OnOff_VG2;

                oCmd.Parameters.Add(new SqlParameter("@AdvCloseTime_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@AdvCloseTime_VG2"].Value = AdvCloseTime_VG2;

                oCmd.Parameters.Add(new SqlParameter("@InjectionHPEndTime_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionHPEndTime_VG2"].Value = InjectionHPEndTime_VG2;

                oCmd.Parameters.Add(new SqlParameter("@Transfer_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@Transfer_VG2"].Value = Transfer_VG2;

                oCmd.Parameters.Add(new SqlParameter("@PSI_VG2", SqlDbType.Decimal));
                oCmd.Parameters["@PSI_VG2"].Value = PSI_VG2;

                oCmd.Parameters.Add(new SqlParameter("@OpenDelay_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@OpenDelay_VG3"].Value = OpenDelay_VG3;

                oCmd.Parameters.Add(new SqlParameter("@OpenTime_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@OpenTime_VG3"].Value = OpenTime_VG3;

                oCmd.Parameters.Add(new SqlParameter("@OpenPostion_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@OpenPostion_VG3"].Value = OpenPostion_VG3;

                oCmd.Parameters.Add(new SqlParameter("@OpenHold_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@OpenHold_VG3"].Value = OpenHold_VG3;

                oCmd.Parameters.Add(new SqlParameter("@CloseDelay_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@CloseDelay_VG3"].Value = CloseDelay_VG3;

                oCmd.Parameters.Add(new SqlParameter("@CloseTime_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@CloseTime_VG3"].Value = CloseTime_VG3;

                oCmd.Parameters.Add(new SqlParameter("@ClosePostion_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@ClosePostion_VG3"].Value = ClosePostion_VG3;

                oCmd.Parameters.Add(new SqlParameter("@CloseHold_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@CloseHold_VG3"].Value = CloseHold_VG3;

                oCmd.Parameters.Add(new SqlParameter("@HoldDelay_OnOff_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@HoldDelay_OnOff_VG3"].Value = HoldDelay_OnOff_VG3;

                oCmd.Parameters.Add(new SqlParameter("@HoldActive_OnOff_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@HoldActive_OnOff_VG3"].Value = HoldActive_OnOff_VG3;

                oCmd.Parameters.Add(new SqlParameter("@AdvCloseTime_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@AdvCloseTime_VG3"].Value = AdvCloseTime_VG3;

                oCmd.Parameters.Add(new SqlParameter("@InjectionHPEndTime_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionHPEndTime_VG3"].Value = InjectionHPEndTime_VG3;

                oCmd.Parameters.Add(new SqlParameter("@Transfer_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@Transfer_VG3"].Value = Transfer_VG3;

                oCmd.Parameters.Add(new SqlParameter("@PSI_VG3", SqlDbType.Decimal));
                oCmd.Parameters["@PSI_VG3"].Value = PSI_VG3;

                oCmd.Parameters.Add(new SqlParameter("@OpenDelay_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@OpenDelay_VG4"].Value = OpenDelay_VG4;

                oCmd.Parameters.Add(new SqlParameter("@OpenTime_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@OpenTime_VG4"].Value = OpenTime_VG4;

                oCmd.Parameters.Add(new SqlParameter("@OpenPostion_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@OpenPostion_VG4"].Value = OpenPostion_VG4;

                oCmd.Parameters.Add(new SqlParameter("@OpenHold_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@OpenHold_VG4"].Value = OpenHold_VG4;

                oCmd.Parameters.Add(new SqlParameter("@CloseDelay_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@CloseDelay_VG4"].Value = CloseDelay_VG4;

                oCmd.Parameters.Add(new SqlParameter("@CloseTime_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@CloseTime_VG4"].Value = CloseTime_VG4;

                oCmd.Parameters.Add(new SqlParameter("@ClosePostion_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@ClosePostion_VG4"].Value = ClosePostion_VG4;

                oCmd.Parameters.Add(new SqlParameter("@CloseHold_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@CloseHold_VG4"].Value = CloseHold_VG4;

                oCmd.Parameters.Add(new SqlParameter("@HoldDelay_OnOff_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@HoldDelay_OnOff_VG4"].Value = HoldDelay_OnOff_VG4;

                oCmd.Parameters.Add(new SqlParameter("@HoldActive_OnOff_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@HoldActive_OnOff_VG4"].Value = HoldActive_OnOff_VG4;

                oCmd.Parameters.Add(new SqlParameter("@AdvCloseTime_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@AdvCloseTime_VG4"].Value = AdvCloseTime_VG4;

                oCmd.Parameters.Add(new SqlParameter("@InjectionHPEndTime_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@InjectionHPEndTime_VG4"].Value = InjectionHPEndTime_VG4;

                oCmd.Parameters.Add(new SqlParameter("@Transfer_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@Transfer_VG4"].Value = Transfer_VG4;

                oCmd.Parameters.Add(new SqlParameter("@PSI_VG4", SqlDbType.Decimal));
                oCmd.Parameters["@PSI_VG4"].Value = PSI_VG4;

                oCmd.Parameters.Add(new SqlParameter("@_RecId", SqlDbType.Int));
                oCmd.Parameters["@_RecId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, -1));
                oCmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                results.recId = _global.Fld2Int(oCmd.Parameters["@_RecId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@ErrorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog(("_database.postValveGateData() failed! Error is: " + ex.Message));
                results.errorMessage = "postValveGateData() failed. contact user support";
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
            }
            return results;
        }
    }
    public class _admin
    {
        public struct postResults
        {
            public int recId;
            public string errorMessage;
        }
        public static _admin.postResults postUsers(int intUserId, string strWindowsUserId, string strFriendlyName, string strPlantNumber, bool bolIsDisabled, string strRoles)
        {
            _admin.postResults results = new _admin.postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_users", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                oCmd.Parameters["@userId"].Value = intUserId;

                oCmd.Parameters.Add(new SqlParameter("@windowsUserId", SqlDbType.NVarChar, 30));
                oCmd.Parameters["@windowsUserId"].Value = strWindowsUserId;

                oCmd.Parameters.Add(new SqlParameter("@friendlyName", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@friendlyName"].Value = strFriendlyName;

                oCmd.Parameters.Add(new SqlParameter("@plantNumber", SqlDbType.NVarChar, 50));
                oCmd.Parameters["@plantNumber"].Value = strPlantNumber;

                oCmd.Parameters.Add(new SqlParameter("@isDisabled", SqlDbType.Int));
                oCmd.Parameters["@isDisabled"].Value = _global.Bol2Int(bolIsDisabled);

                oCmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@roles"].Value = strRoles;

                oCmd.Parameters.Add(new SqlParameter("@_userId", SqlDbType.Int));
                oCmd.Parameters["@_userId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results.recId = _global.Fld2Int(oCmd.Parameters["@_userId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.postUsers() failed! Error is: " + ex.Message);
                results.errorMessage = "Unable to process request at this time. User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }
        public static string deleteUsers(int intUserId)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.delete_users", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                oCmd.Parameters["@userId"].Value = intUserId;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.deleteUsers() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static _admin.postResults postPages(int intPageId, string strTitle, string strTarget, bool bolIsDisabled, bool bolShowLoadProgress, string strRoles)
        {
            _admin.postResults results = new _admin.postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_pages", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@pageId", SqlDbType.Int));
                oCmd.Parameters["@pageId"].Value = intPageId;

                oCmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@title"].Value = strTitle;

                oCmd.Parameters.Add(new SqlParameter("@target", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@target"].Value = strTarget;

                oCmd.Parameters.Add(new SqlParameter("@isDisabled", SqlDbType.Int));
                oCmd.Parameters["@isDisabled"].Value = _global.Bol2Int(bolIsDisabled);

                oCmd.Parameters.Add(new SqlParameter("@showLoadProgress", SqlDbType.Int));
                oCmd.Parameters["@showLoadProgress"].Value = _global.Bol2Int(bolShowLoadProgress);

                oCmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@roles"].Value = strRoles;

                oCmd.Parameters.Add(new SqlParameter("@_pageId", SqlDbType.Int));
                oCmd.Parameters["@_pageId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results.recId = _global.Fld2Int(oCmd.Parameters["@_pageId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.postPages() failed! Error is: " + ex.Message);
                results.errorMessage = "Unable to process request at this time. User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }
        public static string deletePages(int intPageId)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.delete_pages", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@pageId", SqlDbType.Int));
                oCmd.Parameters["@pageId"].Value = intPageId;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.deletePages() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static _admin.postResults postMenus(int intMenuId, int intDisplayOrder, string strTitle, string strTarget, bool bolIsPublic, bool bolIsDisabled, string strRoles, string strPages)
        {
            _admin.postResults results = new _admin.postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_Menus", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@MenuId", SqlDbType.Int));
                oCmd.Parameters["@MenuId"].Value = intMenuId;

                oCmd.Parameters.Add(new SqlParameter("@displayOrder", SqlDbType.Int));
                oCmd.Parameters["@displayOrder"].Value = intDisplayOrder;

                oCmd.Parameters.Add(new SqlParameter("@title", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@title"].Value = strTitle;

                oCmd.Parameters.Add(new SqlParameter("@target", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@target"].Value = strTarget;

                oCmd.Parameters.Add(new SqlParameter("@isPublic", SqlDbType.Int));
                oCmd.Parameters["@isPublic"].Value = _global.Bol2Int(bolIsPublic);

                oCmd.Parameters.Add(new SqlParameter("@isDisabled", SqlDbType.Int));
                oCmd.Parameters["@isDisabled"].Value = _global.Bol2Int(bolIsDisabled);

                oCmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@roles"].Value = strRoles;

                oCmd.Parameters.Add(new SqlParameter("@pages", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pages"].Value = strPages;

                oCmd.Parameters.Add(new SqlParameter("@_MenuId", SqlDbType.Int));
                oCmd.Parameters["@_MenuId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results.recId = _global.Fld2Int(oCmd.Parameters["@_MenuId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.postMenus() failed! Error is: " + ex.Message);
                results.errorMessage = "Unable to process request at this time. User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }
        public static string deleteMenus(int intMenuId)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.delete_Menus", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@menuId", SqlDbType.Int));
                oCmd.Parameters["@menuId"].Value = intMenuId;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.deleteMenus() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static _admin.postResults postRoles(int intRoleId, string strRoleName)
        {
            _admin.postResults results = new _admin.postResults();
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_Roles", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int));
                oCmd.Parameters["@RoleId"].Value = intRoleId;

                oCmd.Parameters.Add(new SqlParameter("@roleName", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@roleName"].Value = strRoleName;

                oCmd.Parameters.Add(new SqlParameter("@_RoleId", SqlDbType.Int));
                oCmd.Parameters["@_RoleId"].Direction = ParameterDirection.Output;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results.recId = _global.Fld2Int(oCmd.Parameters["@_RoleId"].Value);
                results.errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_database.postRoles() failed! Error is: " + ex.Message);
                results.errorMessage = "Unable to process request at this time. User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }
        public static string postRoleUser(int intRoleUserId, int intRoleId, int intIscheck)
        {
            string errorMessage;
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_roleUsers", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@roleUserId", SqlDbType.Int));
                oCmd.Parameters["@roleUserId"].Value = intRoleUserId;

                oCmd.Parameters.Add(new SqlParameter("@roleId", SqlDbType.Int));
                oCmd.Parameters["@roleId"].Value = intRoleId;

                oCmd.Parameters.Add(new SqlParameter("@checked", SqlDbType.Int));
                oCmd.Parameters["@checked"].Value = intIscheck;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.VarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.postRoleUser failed! Error is:" + ex.Message);
                errorMessage = "Unable to process at this time.  User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }

            return errorMessage;
        }
        public static string postRolePage(int intRolePageId, int intRoleId, int intIscheck)
        {
            string errorMessage;
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.post_rolePages", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@rolePageId", SqlDbType.Int));
                oCmd.Parameters["@rolePageId"].Value = intRolePageId;

                oCmd.Parameters.Add(new SqlParameter("@roleId", SqlDbType.Int));
                oCmd.Parameters["@roleId"].Value = intRoleId;

                oCmd.Parameters.Add(new SqlParameter("@checked", SqlDbType.Int));
                oCmd.Parameters["@checked"].Value = intIscheck;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.VarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                errorMessage = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.postRolePage failed! Error is:" + ex.Message);
                errorMessage = "Unable to process at this time.  User support has been notified.";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }

            return errorMessage;
        }
        public static string deleteRoles(int intRoleId)
        {
            string results = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.delete_Roles", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@pUserId", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@pUserId"].Value = _security.windowsUserId();

                oCmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int));
                oCmd.Parameters["@RoleId"].Value = intRoleId;

                oCmd.Parameters.Add(new SqlParameter("@errorMessage", SqlDbType.NVarChar, 1024));
                oCmd.Parameters["@errorMessage"].Direction = ParameterDirection.Output;

                oCn.Open();
                oCmd.ExecuteNonQuery();

                // Get results
                results = _global.Fld2Str(oCmd.Parameters["@errorMessage"].Value);
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.deleteRoles() failed! Error is: " + ex.Message);
                results = "Unable to process request at this time. User support has been notified";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return results;
        }

        public static string buildNavMenu()
        {
            string strMenu = "";
            SqlConnection oCn = null;
            SqlCommand oCmd = null;

            try
            {
                oCn = new SqlConnection(_global.conString());
                oCmd = new SqlCommand("shared.get_menuData", oCn);
                oCmd.CommandType = CommandType.StoredProcedure;

                oCmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                oCmd.Parameters["@userId"].Value = _security.userId();

                oCmd.Parameters.Add(new SqlParameter("@rootURL", SqlDbType.NVarChar, 255));
                oCmd.Parameters["@rootURL"].Value = _global.rootURL();

                SqlDataReader oDr;
                oCn.Open();
                oDr = oCmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (oDr.Read())
                {
                    strMenu += _global.Fld2Str(oDr["navMenu"]);
                }
                oDr.Close();
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("_admin.buildNavMenu() failed! Error is: " + ex.Message);
                strMenu = "<div style='height:20px;padding-top:5px;font-size:12px;color:#fff;'>Unable to process request at this time. User support has been notified</div>";
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
                if (!(oCmd == null)) { oCmd.Dispose(); }
            }
            return strMenu;
        }
    }
    public class _security
    {
        public static string windowsUserId()
        {
            string strValue = "";
            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    strValue = HttpContext.Current.User.Identity.Name.Split('\\')[1];
                }

                //debug...
                if (_global.debugUser() != "") { strValue = _global.debugUser(); }

                //spoof user
                if (_global.spoofUser() != "") { strValue = _global.spoofUser(); }
            }
            catch { }

            return strValue.Trim();
        }

        public static int userId()
        {
            int intValue = 0;
            try
            {
                intValue = _global.Fld2Int(HttpContext.Current.Session["_userId"]);
            }
            catch { }

            return intValue;
        }
        public static string friendlyName()
        {
            string strValue = "";
            try
            {
                strValue = _global.Fld2Str(HttpContext.Current.Session["_friendlyName"]);
            }
            catch { }

            return strValue;
        }
        public static bool isSiteAdmin()
        {
            bool bolValue = false;
            try
            {
                bolValue = _global.Int2Bol(HttpContext.Current.Session["_isSiteAdmin"]);
            }
            catch { }

            return bolValue;
        }
        public static string plantNumber()
        {
            string strValue = "";
            try
            {
                strValue = _global.Fld2Str(HttpContext.Current.Session["_plantNumber"]);
                //if no user default get selected plant number
                if (strValue == "")
                {
                    strValue = _global.Fld2Str(HttpContext.Current.Session["_selectedPlantNumber"]);
                }
            }
            catch { }

            return strValue;
        }
        public static string userRoles()
        {
            string strValue = "";
            try
            {
                strValue = _global.Fld2Str(HttpContext.Current.Session["_userRoles"]);
            }
            catch
            {
            }
            return strValue;
        }
        public static bool isInRole(string strRoles)
        {
            //pass a comma seperated list of roles (or a single role). 
            // if the current user is in any of the roles true is returned

            bool bolResults = false;
            string[] aRole = strRoles.Split(',');
            string[] aUserRoles = _security.userRoles().Split(',');

            foreach (string strRole in aRole)
            {
                var results = Array.FindAll(aUserRoles, s => s.Equals(strRole));
                bolResults = (results.Length != 0) ? true : false;
                if (bolResults) break;
            }
            return bolResults;
        }

        public struct securitySettings
        {
            public bool isAuthorized;
            public string errorMessage;
        }
        public static securitySettings checkSecurity()
        {
            return checkSecurity("");
        }
        public static securitySettings checkSecurity(string pageTarget)
        {
            securitySettings security = new securitySettings();

            try
            {
                string strSQL = "shared.check_pageAuthorization ";
                strSQL += "@windowsUserId = " + _global.Str2Fld(_security.windowsUserId()) + ", ";
                strSQL += "@pageTarget = " + _global.Str2Fld(pageTarget);

                DataSet oDs = _shared.getDataSet(strSQL, _global.conString());
                foreach (DataRow oRow in oDs.Tables[0].Rows)
                {
                    security.isAuthorized = _global.Int2Bol(oRow["isAuthorized"]);

                    HttpContext.Current.Session["_userId"] = _global.Fld2Int(oRow["userId"]);
                    HttpContext.Current.Session["_isSiteAdmin"] = _global.Bol2Int(oRow["isSiteAdmin"]);
                    HttpContext.Current.Session["_friendlyName"] = _global.Fld2Str(oRow["friendlyName"]);
                    HttpContext.Current.Session["_plantNumber"] = _global.Fld2Str(oRow["plantNumber"]);
                    HttpContext.Current.Session["_userRoles"] = _global.Fld2Str(oRow["_userRoles"]);
                }
            }
            catch (Exception ex)
            {
                _global.WriteErrorLog("getSecuritySettings() failed! Error is: " + ex.Message);
                security.errorMessage = ex.Message;
            }
            return security;
        }
    }
    public class _AESEncryption
    {
        // Encryption keys
        private byte[] Key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
        private byte[] Vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 101, 112, 79, 32, 114, 156 };

        private ICryptoTransform EncryptorTransform, DecryptorTransform;
        private System.Text.UTF8Encoding UTFEncoder;

        public _AESEncryption()
        {
            //Encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
            DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();
        }

        /// -------------- Two Utility Methods (not used but may be useful) -----------
        /// Generates an encryption key.
        static public byte[] GenerateEncryptionKey()
        {
            //Generate a Key.
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateKey();
            return rm.Key;
        }

        /// Generates a unique encryption vector
        static public byte[] GenerateEncryptionVector()
        {
            //Generate a Vector
            RijndaelManaged rm = new RijndaelManaged();
            rm.GenerateIV();
            return rm.IV;
        }

        /// ----------- The commonly used methods ------------------------------    
        /// Encrypt some text and return a string suitable for passing in a URL.
        public string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        /// Encrypt some text and return an encrypted byte array.
        public byte[] Encrypt(string TextValue)
        {
            //Translates our text value into a byte array.
            Byte[] bytes = UTFEncoder.GetBytes(TextValue);

            //Used to stream the data in and out of the CryptoStream.
            MemoryStream memoryStream = new MemoryStream();

            //We will have to write the unencrypted bytes to the stream,
            //then read the encrypted result back from the stream.
            #region Write the decrypted value to the encryption stream
            CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            #endregion

            #region Read encrypted value back out of the stream
            memoryStream.Position = 0;
            byte[] encrypted = new byte[memoryStream.Length];
            memoryStream.Read(encrypted, 0, encrypted.Length);
            #endregion

            //Clean up.
            cs.Close();
            memoryStream.Close();

            return encrypted;
        }

        /// The other side: Decryption methods
        public string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        //Decryption when working with byte arrays.    
        public string Decrypt(byte[] EncryptedValue)
        {
            #region Write the encrypted value to the decryption stream
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write);
            decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
            decryptStream.FlushFinalBlock();
            #endregion

            #region Read the decrypted value from the stream.
            encryptedStream.Position = 0;
            Byte[] decryptedBytes = new Byte[encryptedStream.Length];
            encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
            encryptedStream.Close();
            #endregion
            return UTFEncoder.GetString(decryptedBytes);
        }

        // Convert a string to a byte array.  NOTE: Normally we'd create a Byte Array from a string using an ASCII encoding (like so).
        //      System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        //      return encoding.GetBytes(str);
        // However, this results in character values that cannot be passed in a URL.  So, instead, I just
        // lay out all of the byte values in a long string of numbers (three per - must pad numbers less than 100).
        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        // Same comment as above.  Normally the conversion would use an ASCII encoding in the other direction:
        //      System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //      return enc.GetString(byteArr);    
        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
    }
}