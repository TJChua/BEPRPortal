using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using PRPortal.Module.BusinessObjects.PR;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    class Code
    {
        private SortedDictionary<string, List<string>> logs = new SortedDictionary<string, List<string>>();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ToString());

        public Code(SecurityStrategyComplex security, IObjectSpaceProvider ObjectSpaceProvider)
        {
            logs.Clear();
            WriteLog("[Log]", "--------------------------------------------------------------------------------");
            WriteLog("[Log]", "Post Begin:[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "]");

            #region Connect to SAP  
            SAPCompany sap = new SAPCompany();
            if (sap.connectSAP())
            {
                WriteLog("[Log]", "Connected to SAP:[" + sap.oCom.CompanyName + "] Time:[" + DateTime.Now.ToString("hh:mm:ss tt") + "]");
            }
            else
            {
                WriteLog("[Error]", "SAP Connection:[" + sap.oCom.CompanyDB + "] Message:[" + sap.errMsg + "] Time:[" + DateTime.Now.ToString("hh: mm:ss tt") + "]");
                sap.oCom = null;
                goto EndApplication;
            }
            #endregion

            try
            {
                string param = "";
                IObjectSpace ListObjectSpace = ObjectSpaceProvider.CreateObjectSpace();
                IObjectSpace securedObjectSpace = ObjectSpaceProvider.CreateObjectSpace();

                param = ConfigurationManager.AppSettings["PRPost"].ToString().ToUpper();
                if (param == "Y" || param == "YES" || param == "TRUE" || param == "1")
                {
                    WriteLog("[INFO]", "--PR Posting Start--");

                    #region PR 
                    IList<PurchaseRequests> pr = ListObjectSpace.GetObjects<PurchaseRequests>
                    (CriteriaOperator.Parse("Company.CompanyCode = ? and Sap = ? and ((Status = ? and AppStatus = ?) or (Status = ? and AppStatus = ?))",
                    ConfigurationManager.AppSettings["CompanyPortal"].ToString(), 0, 1, 0, 1, 1));

                    foreach (PurchaseRequests dtlpr in pr)
                    {
                        try
                        {
                            IObjectSpace os = ObjectSpaceProvider.CreateObjectSpace();
                            PurchaseRequests probj = os.GetObjectByKey<PurchaseRequests>(dtlpr.Oid);

                            #region Post PR
                            if (!sap.oCom.InTransaction) sap.oCom.StartTransaction();

                            int temp = 0;

                            temp = PostPRtoSAP(probj, ObjectSpaceProvider, sap);
                            if (temp == 1)
                            {
                                if (sap.oCom.InTransaction)
                                    sap.oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);

                                GC.Collect();
                            }
                            else if (temp <= 0)
                            {
                                if (sap.oCom.InTransaction)
                                    sap.oCom.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);

                                GC.Collect();
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            WriteLog("[Error]", "Message: POST PR Failed - DocNum : " + dtlpr.DocNum + " (" + ex.Message + ")");
                        }
                    }
                    #endregion

                    WriteLog("[INFO]", "--PR Posting End--");
                }

                #region Update DocNum
                SqlCommand TransactionNotification = new SqlCommand("", conn);
                TransactionNotification.CommandTimeout = 600;

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                try
                {
                    conn.Open();

                    TransactionNotification.CommandText = "EXEC sp_UpdSapDocNum '" + ConfigurationManager.AppSettings["CompanyDB"].ToString() + "'";

                    SqlDataReader reader = TransactionNotification.ExecuteReader();
                    reader.Dispose();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    WriteLog("[Error]", "Message:" + ex.Message);
                    conn.Close();
                }
                #endregion

            }
            catch (Exception ex)
            {
                WriteLog("[Error]", "Message:" + ex.Message);
            }

            // End Post ======================================================================================
            EndApplication:
            return;
        }

        private void WriteLog(string lvl, string str)
        {
            FileStream fileStream = null;

            string filePath = "C:\\" + ConfigurationManager.AppSettings["LogFileName"].ToString() + "\\";
            filePath = filePath + "[" + "Posting Status" + "] Log_" + System.DateTime.Today.ToString("yyyyMMdd") + "." + "txt";

            FileInfo fileInfo = new FileInfo(filePath);
            DirectoryInfo dirInfo = new DirectoryInfo(fileInfo.DirectoryName);
            if (!dirInfo.Exists) dirInfo.Create();

            if (!fileInfo.Exists)
            {
                fileStream = fileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(filePath, FileMode.Append);
            }

            StreamWriter log = new StreamWriter(fileStream);
            string status = lvl.ToString().Replace("[Log]", "");

            //For Portal_UpdateStatus_Log
            log.WriteLine("{0}{1}", status, str.ToString());

            log.Close();
        }

        public int PostPRtoSAP(PurchaseRequests oTargetDoc, IObjectSpaceProvider ObjectSpaceProvider, SAPCompany sap)
        {
            // return 0 = post nothing
            // return -1 = posting error
            // return 1 = posting successful
            try
            {
                if (!oTargetDoc.Sap)
                {
                    IObjectSpace fos = ObjectSpaceProvider.CreateObjectSpace();
                    DateTime postdate = DateTime.Now;

                    Guid g;
                    // Create and display the value of two GUIDs.
                    g = Guid.NewGuid();

                    if (oTargetDoc.PurchaseRequestAttachment != null && oTargetDoc.PurchaseRequestAttachment.Count > 0)
                    {
                        foreach (PurchaseRequestAttachment obj in oTargetDoc.PurchaseRequestAttachment)
                        {
                            string fullpath = ConfigurationManager.AppSettings["B1AttachmentPath"].ToString() + g.ToString() + obj.File.FileName;
                            using (System.IO.FileStream fs = System.IO.File.OpenWrite(fullpath))
                            {
                                obj.File.SaveToStream(fs);
                            }
                        }
                    }

                    int sapempid = 0;
                    SAPbobsCOM.Documents oDoc = null;
                    oDoc = (SAPbobsCOM.Documents)sap.oCom.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseRequest);

                    oDoc.DocType = BoDocumentTypes.dDocument_Items;
                    oDoc.RequesterName = oTargetDoc.Requester;
                    oDoc.DocDate = oTargetDoc.DocDate;
                    oDoc.DocDueDate = oTargetDoc.DeliveryDate;
                    oDoc.RequriedDate = oTargetDoc.DeliveryDate;
                    oDoc.Comments = oTargetDoc.Remarks;
                    //if (oTargetDoc.Currency != null)
                    //{
                    //    oDoc.DocCurrency = oTargetDoc.Currency.CurrCode;
                    //}
                    oDoc.UserFields.Fields.Item("U_PortalDocNum").Value = oTargetDoc.DocNum;

                    if (sapempid > 0)
                        oDoc.DocumentsOwner = sapempid;

                    int cnt = 0;
                    foreach (PurchaseRequestDetails dtl in oTargetDoc.PurchaseRequestDetails)
                    {
                        //if (dtl.LineAmount > 0)
                        //{
                        cnt++;
                        if (cnt == 1)
                        {
                        }
                        else
                        {
                            oDoc.Lines.SetCurrentLine(oDoc.Lines.Count - 1);
                        }

                        oDoc.Lines.RequiredDate = oTargetDoc.DocDate;
                        if (oTargetDoc.Department != null)
                        {
                            oDoc.Lines.CostingCode = oTargetDoc.Department.DepartmentCode;
                        }
                        oDoc.Lines.UserFields.Fields.Item("U_PortalItemLine").Value = dtl.Oid.ToString();
                        if (oTargetDoc.Currency != null)
                        {
                            oDoc.Lines.Currency = oTargetDoc.Currency.CurrCode;
                        }

                        oDoc.Lines.ItemCode = dtl.ItemCode.ItemCode;
                        oDoc.Lines.ItemDescription = dtl.ItemDesc;
                        oDoc.Lines.ItemDetails = dtl.Remarks;
                        oDoc.Lines.Quantity = (double)dtl.Quantity;
                        oDoc.Lines.UnitPrice = (double)dtl.UnitPrice;
                        oDoc.Lines.DiscountPercent = 0;

                        //oDoc.Lines.LineTotal = (double)dtl.Total;

                        oDoc.Lines.Add();
                    }

                    if (oTargetDoc.PurchaseRequestAttachment != null && oTargetDoc.PurchaseRequestAttachment.Count > 0)
                    {
                        cnt = 0;
                        SAPbobsCOM.Attachments2 oAtt = (SAPbobsCOM.Attachments2)sap.oCom.GetBusinessObject(BoObjectTypes.oAttachments2);
                        foreach (PurchaseRequestAttachment dtl in oTargetDoc.PurchaseRequestAttachment)
                        {

                            cnt++;
                            if (cnt == 1)
                            {
                                if (oAtt.Lines.Count == 0)
                                    oAtt.Lines.Add();
                            }
                            else
                                oAtt.Lines.Add();

                            string attfile = "";
                            string[] fexe = dtl.File.FileName.Split('.');
                            if (fexe.Length <= 2)
                                attfile = fexe[0];
                            else
                            {
                                for (int x = 0; x < fexe.Length - 1; x++)
                                {
                                    if (attfile == "")
                                        attfile = fexe[x];
                                    else
                                        attfile += "." + fexe[x];
                                }
                            }
                            oAtt.Lines.FileName = attfile;
                            if (fexe.Length > 1)
                                oAtt.Lines.FileExtension = fexe[fexe.Length - 1];
                            string path = ConfigurationManager.AppSettings["B1AttachmentPath"].ToString();
                            path = path.Replace("\\\\", "\\");
                            path = path.Substring(0, path.Length - 1);
                            oAtt.Lines.SourcePath = path;
                            oAtt.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;
                        }
                        int iAttEntry = -1;
                        if (oAtt.Add() == 0)
                        {
                            iAttEntry = int.Parse(sap.oCom.GetNewObjectKey());
                        }
                        else
                        {
                            string temp = sap.oCom.GetLastErrorDescription();
                            if (sap.oCom.InTransaction)
                            {
                                sap.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                            }

                            WriteLog("[Error]", "Message: POST PR Att Failed - DocNum : " + oTargetDoc.DocNum + " (" + temp + ")");

                            return -1;
                        }
                        oDoc.AttachmentEntry = iAttEntry;
                    }

                    int rc = oDoc.Add();
                    if (rc != 0)
                    {
                        string temp = sap.oCom.GetLastErrorDescription();
                        if (sap.oCom.InTransaction)
                        {
                            sap.oCom.EndTransaction(BoWfTransOpt.wf_RollBack);
                        }

                        WriteLog("[Error]", "Message: POST PR Failed - DocNum : " + oTargetDoc.DocNum + " (" + temp + ")");

                        return -1;
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog("[Error]", "Message: POST PR Failed - DocNum : " + oTargetDoc.DocNum + " (" + ex.Message + ")");

                return -1;
            }
        }
    }
}
