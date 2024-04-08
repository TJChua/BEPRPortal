using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Web;
using DevExpress.Xpo.DB;
using PRPortal.Module.BusinessObjects;
using PRPortal.Module.BusinessObjects.PR;
using PRPortal.Module.BusinessObjects.View;

namespace PRPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PurchaseRequestControllers : ViewController
    {
        GeneralControllers genCon;
        public PurchaseRequestControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.SubmitPR.Active.SetItemValue("Enabled", false);
            this.CancelPR.Active.SetItemValue("Enabled", false);
            this.DuplicatePR.Active.SetItemValue("Enabled", false);
            this.ApprovePR.Active.SetItemValue("Enabled", false);
            this.AddItem.Active.SetItemValue("Enabled", false);
            this.SearchItem.Active.SetItemValue("Enabled", false);
            this.AddNewItem.Active.SetItemValue("Enabled", false);
            this.EscalateUser.Active.SetItemValue("Enabled", false);
            this.SaveDetails.Active.SetItemValue("Enabled", false);
            this.NewItem.Active.SetItemValue("Enabled", false);

            //// For PK
            //if (View.Id == "InputItem_DetailView")
            //{
            //    View.ControlsCreated += new EventHandler(View_ControlsCreated);
            //}
            //// For PK
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            genCon = Frame.GetController<GeneralControllers>();

            if (View.Id == "PurchaseRequests_DetailView")
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.View)
                {
                    this.SubmitPR.Active.SetItemValue("Enabled", true);
                    this.CancelPR.Active.SetItemValue("Enabled", true);
                    this.DuplicatePR.Active.SetItemValue("Enabled", true);
                    this.AddItem.Active.SetItemValue("Enabled", false);
                }
                else
                {
                    this.SubmitPR.Active.SetItemValue("Enabled", false);
                    this.CancelPR.Active.SetItemValue("Enabled", false);
                    this.DuplicatePR.Active.SetItemValue("Enabled", false);
                    this.AddItem.Active.SetItemValue("Enabled", true);
                }
            }
            else if (View.Id == "InputItem_DetailView")
            {
                //this.SearchItem.Active.SetItemValue("Enabled", true);
                //this.SearchItem.ActionMeaning = ActionMeaning.Accept;
                this.AddNewItem.Active.SetItemValue("Enabled", true);
                this.AddNewItem.ActionMeaning = ActionMeaning.Accept;
            }
            else if (View.Id == "PurchaseRequests_ListView_PendApp")
            {
                this.ApprovePR.Active.SetItemValue("Enabled", true);
                this.EscalateUser.Active.SetItemValue("Enabled", true);
            }
            else if (View.Id == "PurchaseRequests_DetailView_PendApp")
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.View)
                {
                    this.ApprovePR.Active.SetItemValue("Enabled", true);
                }
                else
                {
                    this.ApprovePR.Active.SetItemValue("Enabled", false);
                }
            }
            else if (View.Id == "PurchaseRequestDetails_DetailView")
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    this.SaveDetails.Active.SetItemValue("Enabled", true);
                    this.NewItem.Active.SetItemValue("Enabled", true);
                }
                else
                {
                    this.SaveDetails.Active.SetItemValue("Enabled", false);
                    this.NewItem.Active.SetItemValue("Enabled", false);
                }
            }
            if (View.Id == "PurchaseRequests_DetailView_Procurement")
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.View)
                {
                    this.SubmitPR.Active.SetItemValue("Enabled", true);
                    this.CancelPR.Active.SetItemValue("Enabled", true);
                    this.AddItem.Active.SetItemValue("Enabled", false);
                }
                else
                {
                    this.SubmitPR.Active.SetItemValue("Enabled", false);
                    this.CancelPR.Active.SetItemValue("Enabled", false);
                    this.AddItem.Active.SetItemValue("Enabled", true);
                }
            }
            else
            {
                this.SubmitPR.Active.SetItemValue("Enabled", false);
                this.CancelPR.Active.SetItemValue("Enabled", false);
                this.DuplicatePR.Active.SetItemValue("Enabled", false);
                this.AddItem.Active.SetItemValue("Enabled", false);
                this.SearchItem.Active.SetItemValue("Enabled", false);
                this.SaveDetails.Active.SetItemValue("Enabled", false);
                this.NewItem.Active.SetItemValue("Enabled", false);
            }
        }
        protected override void OnDeactivated()
        {
            //// For PK
            //View.ControlsCreated -= new EventHandler(View_ControlsCreated);
            //// For PK
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        //// For PK
        //void View_ControlsCreated(object sender, EventArgs e)
        //{
        //    ((Control)View.Control).PreRender += new EventHandler(ViewController1_PreRender);
        //}
        //void ViewController1_PreRender(object sender, EventArgs e)
        //{
        //    string fieldname = View.CurrentObject.GetType().GetProperty("Field").GetValue(View.CurrentObject).ToString();
        //    ((Control)View.Control).PreRender -= new EventHandler(ViewController1_PreRender);
        //    DetailView detailView = (DetailView)View;

        //    if (fieldname != null)
        //    {
        //        string[] field = fieldname.Split(',');
        //        foreach (string checkfield in field)
        //        {
        //            SetEditorReadOnlyByName(detailView, checkfield.ToString(), false);
        //        }
        //    }
        //}
        //private void SetEditorReadOnlyByName(DetailView detailView, string targetPropertyName, bool value)
        //{
        //    foreach (PropertyEditor editor in detailView.GetItems<PropertyEditor>())
        //    {
        //        if (editor.PropertyName == targetPropertyName)
        //        {
        //            //editor.SetReadOnlyByKey("", value);  
        //            //(editor.Control as Control).Visible = !value;
        //            ((IAppearanceVisibility)editor).Visibility = ViewItemVisibility.Hide;
        //            editor.Refresh();
        //        }
        //    }
        //}
        //// For PK

        private void SubmitPR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            selectedObject.Status = DocStatus.Submitted;
            PurchaseRequestDocTrail ds = ObjectSpace.CreateObject<PurchaseRequestDocTrail>();
            ds.DocStatus = DocStatus.Submitted;
            ds.DocRemarks = p.ParamString;
            selectedObject.PurchaseRequestDocTrail.Add(ds);

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();

            #region Get approval
            List<string> ToEmails = new List<string>();
            string emailbody = "";
            string emailsubject = "";
            string emailaddress = "";
            Guid emailuser;
            DateTime emailtime = DateTime.Now;

            string getapproval = "EXEC sp_GetApproval '" + selectedObject.CreateUser + "', '" + selectedObject.Oid + "', 'PurchaseRequests'";
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            SqlCommand cmd = new SqlCommand(getapproval, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(1) != "")
                {
                    emailbody = "Dear Sir/Madam, " + System.Environment.NewLine + System.Environment.NewLine +
                           reader.GetString(3) + System.Environment.NewLine + GeneralSettings.appurl + reader.GetString(2) +
                           System.Environment.NewLine + System.Environment.NewLine;

                    emailsubject = "Purchase Requests Approval";
                    emailaddress = reader.GetString(1);
                    emailuser = reader.GetGuid(0);

                    ToEmails.Add(emailaddress);
                }
            }
            conn.Close();

            if (ToEmails.Count > 0)
            {
                if (genCon.SendEmail(emailsubject, emailbody, ToEmails) == 1)
                {
                }
            }

            #endregion

            IObjectSpace pos = Application.CreateObjectSpace();
            PurchaseRequests ptrx = pos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", selectedObject.Oid));
            genCon.openNewView(pos, ptrx, ViewEditMode.View);
            genCon.showMsg("Successful", "Submit Done.", InformationType.Success);
        }

        private void SubmitPR_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var os = Application.CreateObjectSpace(typeof(StringParameters));
            StringParameters message = os.CreateObject<StringParameters>();

            DetailView dv = Application.CreateDetailView(os, message);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = false;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;
        }

        private void CancelPR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            selectedObject.Status = DocStatus.Cancelled;
            PurchaseRequestDocTrail ds = ObjectSpace.CreateObject<PurchaseRequestDocTrail>();
            ds.DocStatus = DocStatus.Cancelled;
            ds.DocRemarks = p.ParamString;
            selectedObject.PurchaseRequestDocTrail.Add(ds);

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();

            IObjectSpace os = Application.CreateObjectSpace();
            PurchaseRequests trx = os.FindObject<PurchaseRequests>(new BinaryOperator("Oid", selectedObject.Oid));
            genCon.openNewView(os, trx, ViewEditMode.View);
            genCon.showMsg("Successful", "Cancel Done.", InformationType.Success);
        }

        private void CancelPR_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var os = Application.CreateObjectSpace(typeof(StringParameters));
            StringParameters message = os.CreateObject<StringParameters>();

            DetailView dv = Application.CreateDetailView(os, message);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = false;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;
        }

        private void DuplicatePR_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (e.SelectedObjects.Count == 1)
            {
                try
                {
                    PurchaseRequests pr = (PurchaseRequests)View.CurrentObject;
                    IObjectSpace os = Application.CreateObjectSpace();
                    PurchaseRequests newpr = os.CreateObject<PurchaseRequests>();

                    newpr.EventorPurpose = pr.EventorPurpose;
                    newpr.Requester = pr.Requester;
                    newpr.Remarks = pr.Remarks;

                    foreach (PurchaseRequestDetails dtl in pr.PurchaseRequestDetails)
                    {
                        PurchaseRequestDetails newprdetails = os.CreateObject<PurchaseRequestDetails>();

                        newprdetails.ItemCode = newprdetails.Session.GetObjectByKey<vwItemMasters>(dtl.ItemCode.ItemCode);
                        newprdetails.ItemDesc = dtl.ItemDesc;
                        newprdetails.UOM = dtl.UOM;
                        newprdetails.Quantity = dtl.Quantity;
                        newprdetails.UnitPrice = dtl.UnitPrice;
                        newpr.PurchaseRequestDetails.Add(newprdetails);
                    }

                    ShowViewParameters svp = new ShowViewParameters();
                    DetailView dv = Application.CreateDetailView(os, newpr);
                    dv.ViewEditMode = ViewEditMode.Edit;
                    dv.IsRoot = true;
                    svp.CreatedView = dv;

                    Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
                    genCon.showMsg("Success", "Duplicate Success.", InformationType.Success);
                }
                catch (Exception)
                {
                    genCon.showMsg("Fail", "Duplicate Fail.", InformationType.Error);
                }
            }
            else if (e.SelectedObjects.Count > 1)
            {
                genCon.showMsg("Fail", "Duplicate Fail, Selected item more than 1.", InformationType.Error);
            }
            else
            {
                genCon.showMsg("Fail", "Duplicate Fail, No selected item.", InformationType.Error);
            }
        }

        private void ApprovePR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (e.SelectedObjects.Count > 1)
            {
                int totaldoc = 0;

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                ApprovalParameters p = (ApprovalParameters)e.PopupWindow.View.CurrentObject;

                foreach (PurchaseRequests dtl in e.SelectedObjects)
                {
                    IObjectSpace pos = Application.CreateObjectSpace();
                    PurchaseRequests pr = pos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", dtl.Oid));

                    if (pr.NextApprover != null)
                    {
                        pr.WhoApprove = pr.WhoApprove + user.Staff.StaffName + ", ";
                        ApprovalStatusType appstatus = ApprovalStatusType.Required_Approval;

                        if (appstatus == ApprovalStatusType.Not_Applicable)
                            appstatus = ApprovalStatusType.Required_Approval;


                        if (p.IsErr) return;
                        if (appstatus == ApprovalStatusType.Required_Approval && p.AppStatus == ApprovalActions.NA)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        else if (appstatus == ApprovalStatusType.Approved && p.AppStatus == ApprovalActions.Yes)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        else if (appstatus == ApprovalStatusType.Rejected && p.AppStatus == ApprovalActions.No)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        if (p.AppStatus == ApprovalActions.NA)
                        {
                            appstatus = ApprovalStatusType.Required_Approval;
                        }
                        if (p.AppStatus == ApprovalActions.Yes)
                        {
                            appstatus = ApprovalStatusType.Approved;
                        }
                        if (p.AppStatus == ApprovalActions.No)
                        {
                            appstatus = ApprovalStatusType.Rejected;
                        }
                        if (p.AppStatus == ApprovalActions.No)
                        {
                            appstatus = ApprovalStatusType.Not_Applicable;
                        }

                        PurchaseRequestAppStatus ds = pos.CreateObject<PurchaseRequestAppStatus>();
                        ds.PurchaseRequests = pos.GetObjectByKey<PurchaseRequests>(pr.Oid);
                        ds.AppStatus = appstatus;
                        if (appstatus == ApprovalStatusType.Rejected)
                        {
                            pr.Status = DocStatus.Rejected;
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Reject User: " + user.Staff.StaffName + ")" +
                                System.Environment.NewLine + "(Reason: Approval Rejected)";
                            ds.CreateUser = pos.GetObjectByKey<SystemUsers>(Guid.Parse("8CD3074B-57B4-4154-B5D9-404A114F60DC"));
                        }
                        else if (appstatus == ApprovalStatusType.Not_Applicable)
                        {
                            pr.Status = DocStatus.Resubmit;
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Resubmit User: " + user.Staff.StaffName + ")";
                        }
                        else
                        {
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Approved User: " + user.Staff.StaffName + ")";
                        }
                        pr.PurchaseRequestAppStatus.Add(ds);

                        pos.CommitChanges();
                        pos.Refresh();

                        totaldoc++;

                        #region approval

                        List<string> ToEmails = new List<string>();
                        string emailbody = "";
                        string emailsubject = "";
                        string emailaddress = "";
                        Guid emailuser;
                        DateTime emailtime = DateTime.Now;

                        string getapproval = "EXEC sp_Approval '" + user.UserName + "', '" + pr.Oid + "', 'PurchaseRequests', " + ((int)appstatus);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(getapproval, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            emailbody = "Dear Sir/Madam, " + System.Environment.NewLine + System.Environment.NewLine +
                                      reader.GetString(3) + System.Environment.NewLine + GeneralSettings.appurl + reader.GetString(2) +
                                      System.Environment.NewLine + System.Environment.NewLine +
                                      "Regards" + System.Environment.NewLine +
                                      "PR Portal";

                            if (appstatus == ApprovalStatusType.Approved)
                                emailsubject = "Purchase Request Approval";
                            else if (appstatus == ApprovalStatusType.Rejected)
                                emailsubject = "Purchase Request Approval Rejected";

                            emailaddress = reader.GetString(1);
                            emailuser = reader.GetGuid(0);

                            ToEmails.Add(emailaddress);
                        }
                        conn.Close();

                        if (ToEmails.Count > 0)
                        {
                            if (genCon.SendEmail(emailsubject, emailbody, ToEmails) == 1)
                            {
                            }
                        }
                        #endregion
                    }

                    //ObjectSpace.CommitChanges(); //This line persists created object(s).
                    //ObjectSpace.Refresh();

                    genCon.showMsg("Info", "Total Document : " + totaldoc + " Approval Done.", InformationType.Info);

                    ((DevExpress.ExpressApp.ListView)Frame.View).ObjectSpace.Refresh();
                }
            }
            else if (e.SelectedObjects.Count == 1)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                ApprovalParameters p = (ApprovalParameters)e.PopupWindow.View.CurrentObject;

                foreach (PurchaseRequests dtl in e.SelectedObjects)
                {
                    IObjectSpace pos = Application.CreateObjectSpace();
                    PurchaseRequests pr = pos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", dtl.Oid));

                    if (pr.NextApprover != null)
                    {
                        pr.WhoApprove = pr.WhoApprove + user.Staff.StaffName + ", ";
                        ApprovalStatusType appstatus = ApprovalStatusType.Required_Approval;

                        if (appstatus == ApprovalStatusType.Not_Applicable)
                            appstatus = ApprovalStatusType.Required_Approval;


                        if (p.IsErr) return;
                        if (appstatus == ApprovalStatusType.Required_Approval && p.AppStatus == ApprovalActions.NA)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        else if (appstatus == ApprovalStatusType.Approved && p.AppStatus == ApprovalActions.Yes)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        else if (appstatus == ApprovalStatusType.Rejected && p.AppStatus == ApprovalActions.No)
                        {
                            genCon.showMsg("Failed", "Same Approval Status is not allowed.", InformationType.Error);
                            return;
                        }
                        if (p.AppStatus == ApprovalActions.NA)
                        {
                            appstatus = ApprovalStatusType.Required_Approval;
                        }
                        if (p.AppStatus == ApprovalActions.Yes)
                        {
                            appstatus = ApprovalStatusType.Approved;
                        }
                        if (p.AppStatus == ApprovalActions.No)
                        {
                            appstatus = ApprovalStatusType.Rejected;
                        }
                        if (p.AppStatus == ApprovalActions.Resubmit)
                        {
                            appstatus = ApprovalStatusType.Not_Applicable;
                        }

                        PurchaseRequestAppStatus ds = pos.CreateObject<PurchaseRequestAppStatus>();
                        ds.PurchaseRequests = pos.GetObjectByKey<PurchaseRequests>(pr.Oid);
                        ds.AppStatus = appstatus;
                        if (appstatus == ApprovalStatusType.Rejected)
                        {
                            pr.Status = DocStatus.Rejected;
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Reject User: " + user.Staff.StaffName + ")" +
                                System.Environment.NewLine + "(Reason: Approval Rejected)";
                            ds.CreateUser = pos.GetObjectByKey<SystemUsers>(Guid.Parse("8CD3074B-57B4-4154-B5D9-404A114F60DC"));
                        }
                        else if (appstatus == ApprovalStatusType.Not_Applicable)
                        {
                            pr.Status = DocStatus.Resubmit;
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Resubmit User: " + user.Staff.StaffName + ")";
                        }
                        else
                        {
                            ds.AppRemarks = p.ParamString +
                                System.Environment.NewLine + "(Approved User: " + user.Staff.StaffName + ")";
                        }
                        pr.PurchaseRequestAppStatus.Add(ds);

                        pos.CommitChanges();
                        pos.Refresh();

                        #region approval

                        List<string> ToEmails = new List<string>();
                        string emailbody = "";
                        string emailsubject = "";
                        string emailaddress = "";
                        Guid emailuser;
                        DateTime emailtime = DateTime.Now;

                        string getapproval = "EXEC sp_Approval '" + user.UserName + "', '" + pr.Oid + "', 'PurchaseRequests', " + ((int)appstatus);
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(getapproval, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            emailbody = "Dear Sir/Madam, " + System.Environment.NewLine + System.Environment.NewLine +
                                      reader.GetString(3) + System.Environment.NewLine + GeneralSettings.appurl + reader.GetString(2) +
                                      System.Environment.NewLine + System.Environment.NewLine +
                                      "Regards" + System.Environment.NewLine +
                                      "PR Portal";

                            if (appstatus == ApprovalStatusType.Approved)
                                emailsubject = "Purchase Request Approval";
                            else if (appstatus == ApprovalStatusType.Rejected)
                                emailsubject = "Purchase Request Approval Rejected";

                            emailaddress = reader.GetString(1);
                            emailuser = reader.GetGuid(0);

                            ToEmails.Add(emailaddress);
                        }
                        conn.Close();

                        if (ToEmails.Count > 0)
                        {
                            if (genCon.SendEmail(emailsubject, emailbody, ToEmails) == 1)
                            {
                            }
                        }
                        #endregion

                        IObjectSpace tos = Application.CreateObjectSpace();
                        PurchaseRequests trx = tos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", pr.Oid));

                        ShowViewParameters svp = new ShowViewParameters();
                        DetailView dv = Application.CreateDetailView(tos, "PurchaseRequests_DetailView_Approved", true, trx);
                        dv.ViewEditMode = ViewEditMode.View;
                        dv.IsRoot = true;
                        svp.CreatedView = dv;

                        Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));

                        genCon.showMsg("Successful", "Approve Done.", InformationType.Success);


                    }
                }
            }
            else
            {
                genCon.showMsg("Fail", "No PR selected.", InformationType.Error);
            }
        }

        private void ApprovePR_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            PurchaseRequests selectedObject = (PurchaseRequests)View.CurrentObject;

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            ApprovalStatusType appstatus = ApprovalStatusType.Required_Approval;

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, os.CreateObject<ApprovalParameters>(), true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            switch (appstatus)
            {
                case ApprovalStatusType.Required_Approval:
                    ((ApprovalParameters)dv.CurrentObject).AppStatus = ApprovalActions.NA;
                    break;
                case ApprovalStatusType.Approved:
                    ((ApprovalParameters)dv.CurrentObject).AppStatus = ApprovalActions.Yes;
                    break;
                case ApprovalStatusType.Rejected:
                    ((ApprovalParameters)dv.CurrentObject).AppStatus = ApprovalActions.No;
                    break;
            }
            ((ApprovalParameters)dv.CurrentObject).IsErr = err;
            ((ApprovalParameters)dv.CurrentObject).ActionMessage = "Press Choose From Approval Status 'Approve or Reject' and Press OK to CONFIRM the action and SAVE else press Cancel.";

            e.View = dv;
        }

        private void AddItem_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests PR = (PurchaseRequests)View.CurrentObject;
            InputItem addnew = (InputItem)e.PopupWindow.View.CurrentObject;

            foreach (InputItemDetails dtl in addnew.Results)
            {
                PurchaseRequestDetails PODetails = ObjectSpace.CreateObject<PurchaseRequestDetails>();
                PODetails.ItemCode = PODetails.Session.GetObjectByKey<vwItemMasters>(dtl.ItemCode);
                PODetails.Quantity = dtl.OrderQty;
                PODetails.UnitPrice = dtl.UnitPrice;
                PODetails.Remarks = dtl.Remarks;
                PR.PurchaseRequestDetails.Add(PODetails);
            }

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }

        private void AddItem_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            PurchaseRequests trx = (PurchaseRequests)View.CurrentObject;

            if (trx.DocNum == null)
            {
                trx.DocNum = genCon.GenerateDocNum(DocTypeList.PR, ObjectSpace);
            }

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();

            IObjectSpace nonPersistentOS = Application.CreateObjectSpace(typeof(InputItem));
            InputItem itemlist = nonPersistentOS.CreateObject<InputItem>();
         
            DetailView detailView = Application.CreateDetailView(nonPersistentOS, itemlist);
            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((InputItem)detailView.CurrentObject).OrderQty = 1;
            ((InputItem)detailView.CurrentObject).DocNum = trx.DocNum;
            ((InputItem)detailView.CurrentObject).Doctype = DocTypeList.PR;

            //// For PK
            //DetailView trxdetailView = (DetailView)View;
            //foreach (PropertyEditor editor in trxdetailView.GetItems<PropertyEditor>())
            //{
            //    ((InputItem)detailView.CurrentObject).Field = "ItemCode";
            //    if (((InputItem)detailView.CurrentObject).Field != null)
            //    {
            //        ((InputItem)detailView.CurrentObject).Field = ((InputItem)detailView.CurrentObject).Field + "," + editor.PropertyName;
            //    }
            //    else
            //    {
            //        ((InputItem)detailView.CurrentObject).Field = editor.PropertyName;
            //    }
            //}
            //// For PK

            e.DialogController.SaveOnAccept = false;
            e.DialogController.CancelAction.Active["NothingToCancel"] = false;
            e.DialogController.AcceptAction.ActionMeaning = ActionMeaning.Unknown;

            e.Maximized = true;

            e.View = detailView;
        }

        private void SearchItem_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //ItemList selectedObject = (ItemList)e.CurrentObject;

            //if (selectedObject.Search != null)
            //{
            //    //selectedObject.items.Clear();
            //    XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace();
            //    SelectedData sprocData = persistentObjectSpace.Session.ExecuteSproc("sp_GetItem", new OperandValue(selectedObject.Search));

            //    if (sprocData.ResultSet.Count() > 0)
            //    {
            //        if (sprocData.ResultSet[0].Rows.Count() > 0)
            //        {
            //            foreach (SelectStatementResultRow row in sprocData.ResultSet[0].Rows)
            //            {
            //                ItemCodes searchitem = ObjectSpace.CreateObject<ItemCodes>();
            //                searchitem.ItemCode = row.Values[0].ToString();
            //                searchitem.ItemName = row.Values[1].ToString();
            //                searchitem.Price = (decimal)row.Values[2];

            //                selectedObject.items.Add(searchitem);
            //            }
            //        }
            //    }

            //    ObjectSpace.CommitChanges();
            //    ObjectSpace.Refresh();
            //    View.Refresh();
            //}
            //else
            //{
            //    genCon.showMsg("Warning", "Search empty.", InformationType.Warning);
            //}
        }

        private void AddNewItem_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            InputItem currentobject = (InputItem)e.CurrentObject;

            if (currentobject.ItemCode == null)
            {
                genCon.showMsg("Error", "No item selected.", InformationType.Error);
                return;
            }

            if (currentobject.OrderQty <= 0)
            {
                genCon.showMsg("Error", "Order qty cannot less than 1.", InformationType.Error);
                return;
            }

            InputItemDetails addnewitem = ObjectSpace.CreateObject<InputItemDetails>();
            addnewitem.ItemCode = currentobject.ItemCode.ItemCode;
            addnewitem.ItemName = currentobject.ItemCode.ItemName;
            addnewitem.OrderQty = currentobject.OrderQty;
            addnewitem.UnitPrice = currentobject.UnitPrice;
            addnewitem.Remarks = currentobject.Remarks;

            currentobject.Results.Add(addnewitem);

            genCon.showMsg("Success", "Added to Cart.", InformationType.Success);

            currentobject.ItemCode = null;
            currentobject.OrderQty = 1;
            currentobject.UnitPrice = 0;

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }

        private void EscalateUser_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            e.View = Application.CreateListView(typeof(SystemUsers), true);
        }

        private void EscalateUser_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (e.SelectedObjects.Count >= 1)
            {
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                SystemUsers p = (SystemUsers)e.PopupWindow.View.CurrentObject;

                if (e.PopupWindow.View.SelectedObjects.Count == 1)
                {
                    foreach (PurchaseRequests dtl in e.SelectedObjects)
                    {
                        IObjectSpace pos = Application.CreateObjectSpace();
                        PurchaseRequests CurrObject = pos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", dtl.Oid));

                        if (CurrObject.AppStatus == ApprovalStatusType.Required_Approval)
                        {
                            PurchaseRequestDocTrail ds = pos.CreateObject<PurchaseRequestDocTrail>();
                            ds.DocStatus = CurrObject.Status;
                            ds.DocRemarks = "(System Escalated Info " + "- " + user.Staff.StaffName + " )";
                            CurrObject.PurchaseRequestDocTrail.Add(ds);

                            SystemUsers escalate = pos.FindObject<SystemUsers>(CriteriaOperator.Parse("Staff.StaffName = ?", p.Staff.StaffName));
                            CurrObject.NextApprover = escalate.Staff.StaffName + ", ";

                            pos.CommitChanges();
                            pos.Refresh();

                            genCon.showMsg("Successful", "Escalate Done.", InformationType.Success);

                            IObjectSpace eos = Application.CreateObjectSpace();
                            PurchaseRequests po = eos.FindObject<PurchaseRequests>(new BinaryOperator("Oid", dtl.Oid));
                        }
                    }
                }
                else if (e.PopupWindow.View.SelectedObjects.Count > 1)
                {
                    genCon.showMsg("Fail", "Cannot select more than 1 user.", InformationType.Error);
                }
                else
                {
                    genCon.showMsg("Fail", "No user selected.", InformationType.Error);
                }

               ((DevExpress.ExpressApp.ListView)Frame.View).ObjectSpace.Refresh();
            }
            else
            {
                genCon.showMsg("Fail", "No PO selected.", InformationType.Error);
            }
        }

        private void SaveDetails_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            PurchaseRequestDetails currentobject = (PurchaseRequestDetails)e.CurrentObject;

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();
        }

        private void NewItem_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            PurchaseRequestDetails currentobject = (PurchaseRequestDetails)e.CurrentObject;

            ObjectSpace.CommitChanges();
            ObjectSpace.Refresh();

            IObjectSpace objectSpaceForNewObject = Application.CreateObjectSpace(typeof(PurchaseRequestDetails));
            var newObject = objectSpaceForNewObject.CreateObject<PurchaseRequestDetails>();
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpaceForNewObject, newObject, View);
            e.ShowViewParameters.TargetWindow = TargetWindow.Current;
        }
    }
}
