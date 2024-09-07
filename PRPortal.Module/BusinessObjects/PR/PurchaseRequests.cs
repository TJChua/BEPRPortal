using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using PRPortal.Module.BusinessObjects.View;
using PRPortal.Module.BusinessObjects.Setup;

namespace PRPortal.Module.BusinessObjects.PR
{
    [DefaultClassOptions]
    [XafDisplayName("Purchase Request")]
    [NavigationItem("Purchase Request")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_ListView_PendApp")]
    [Appearance("HideNew1", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_ListView_Approved")]
    //[Appearance("HideNew2", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_ListView_Procurement")]
    [Appearance("HideNew3", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_PendApp")]
    [Appearance("HideNew4", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Approved")]
    //[Appearance("HideNew5", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Procurement")]

    [Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Criteria = "not (Status in (0, 7))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView")]
    [Appearance("HideEdit1", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_ListView_PendApp")]
    [Appearance("HideEdit2", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_ListView_Approved")]
    [Appearance("HideEdit3", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_PendApp")]
    [Appearance("HideEdit4", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Approved")]
    [Appearance("HideEdit5", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Criteria = "not (Status in (0, 8))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Procurement")]

    [Appearance("HideSubmit", AppearanceItemType.Action, "True", TargetItems = "SubmitPRReq", Criteria = "not (Status in (0, 7))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView")]
    [Appearance("HideCancel", AppearanceItemType.Action, "True", TargetItems = "CancelPR", Criteria = "not (Status in (0, 7))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView")]
    [Appearance("HideSubmit1", AppearanceItemType.Action, "True", TargetItems = "SubmitPR", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Approved")]
    [Appearance("HideCancel1", AppearanceItemType.Action, "True", TargetItems = "CancelPR", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Approved")]
    [Appearance("HideSubmit2", AppearanceItemType.Action, "True", TargetItems = "SubmitPR", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_PendApp")]
    [Appearance("HideCancel2", AppearanceItemType.Action, "True", TargetItems = "CancelPR", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_PendApp")]
    [Appearance("HideSubmit3", AppearanceItemType.Action, "True", TargetItems = "SubmitPR", Criteria = "not (Status in (0, 8))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Procurement")]
    [Appearance("HideCancel3", AppearanceItemType.Action, "True", TargetItems = "CancelPR", Criteria = "not (Status in (0, 8))", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "PurchaseRequests_DetailView_Procurement")]

    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]

     [Appearance("HideAddNew", AppearanceItemType.Action, "True", TargetItems = "AddItem", Criteria = "Remarks = null or EventorPurpose = null or Requester = null or ReqEmail = null or PIC = null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]

    public class PurchaseRequests : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PurchaseRequests(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (user != null)
            {
                CreateUser = user.Oid.ToString();
                //Requester = user.Staff.StaffName;
                if (user.Staff != null)
                {
                    if (user.Staff.Department != null)
                    {
                        Department = Session.FindObject<vwDepartment>(new BinaryOperator("DepartmentCode", user.Staff.Department.DepartmentCode));
                    }
                    if (user.Staff.CostCentre != null)
                    {
                        CostCentre = Session.FindObject<vwCostCentre>(new BinaryOperator("Code", user.Staff.CostCentre.Code));
                    }
                }
            }
            CreateDate = DateTime.Now;
            DocDate = DateTime.Now;
            DeliveryDate = DateTime.Now;
            DocType = Session.FindObject<DocTypes>(new BinaryOperator("BoCode", DocTypeList.PR));

            Status = DocStatus.Draft;
            AppStatus = ApprovalStatusType.Not_Applicable;
        }

        private string _CreateUser;
        [XafDisplayName("Create User")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(300), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CreateUser", Enabled = false)]
        public string CreateUser
        {
            get { return _CreateUser; }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        private DateTime? _CreateDate;
        [Index(301), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CreateDate", Enabled = false)]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        private string _UpdateUser;
        [XafDisplayName("Update User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(302), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("UpdateUser", Enabled = false)]
        public string UpdateUser
        {
            get { return _UpdateUser; }
            set
            {
                SetPropertyValue("UpdateUser", ref _UpdateUser, value);
            }
        }

        private DateTime? _UpdateDate;
        [Index(303), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("UpdateDate", Enabled = false)]
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                SetPropertyValue("UpdateDate", ref _UpdateDate, value);
            }
        }

        private DocTypes _DocType;
        [Appearance("DocType", Enabled = false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(304), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DocTypes DocType
        {
            get { return _DocType; }
            set
            {
                SetPropertyValue("DocType", ref _DocType, value);
            }
        }

        private string _DocNum;
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Document No")]
        [Appearance("DocNum", Enabled = false)]
        public string DocNum
        {
            get { return _DocNum; }
            set
            {
                SetPropertyValue("DocNum", ref _DocNum, value);
            }
        }

        private vwDepartment _Department;
        [NoForeignKey]
        [XafDisplayName("Department")]
        [Appearance("Department", Enabled = false)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(3), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public vwDepartment Department
        {
            get { return _Department; }
            set
            {
                SetPropertyValue("Department", ref _Department, value);
            }
        }

        private string _Requester;
        [XafDisplayName("Requester")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [Index(4), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Requester
        {
            get { return _Requester; }
            set
            {
                SetPropertyValue("Requester", ref _Requester, value);
            }
        }

        private string _PIC;
        [XafDisplayName("Procurement PIC")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [Index(5), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string PIC
        {
            get { return _PIC; }
            set
            {
                SetPropertyValue("PIC", ref _PIC, value);
            }
        }

        private string _ReqEmail;
        [XafDisplayName("Requester Email")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [Index(6), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string ReqEmail
        {
            get { return _ReqEmail; }
            set
            {
                SetPropertyValue("ReqEmail", ref _ReqEmail, value);
            }
        }

        private vwCostCentre _CostCentre;
        [NoForeignKey]
        [XafDisplayName("Cost Centre")]
        [Appearance("CostCentre", Enabled = false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(7), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public vwCostCentre CostCentre
        {
            get { return _CostCentre; }
            set
            {
                SetPropertyValue("CostCentre", ref _CostCentre, value);
            }
        }

        private DateTime _DocDate;
        [XafDisplayName("Document Date")]
        [Index(8), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DocDate
        {
            get { return _DocDate; }
            set
            {
                SetPropertyValue("DocDate", ref _DocDate, value);
            }
        }

        private DateTime _DeliveryDate;
        [XafDisplayName("Delivery Date")]
        [Index(10), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set
            {
                SetPropertyValue("DeliveryDate", ref _DeliveryDate, value);
            }
        }

        private DocStatus _Status;
        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [Index(13), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DocStatus Status
        {
            get { return _Status; }
            set
            {
                SetPropertyValue("Status", ref _Status, value);
            }
        }

        private ApprovalStatusType _AppStatus;
        [XafDisplayName("Approval Status")]
        [Appearance("AppStatus", Enabled = false)]
        [Index(15), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public ApprovalStatusType AppStatus
        {
            get { return _AppStatus; }
            set
            {
                SetPropertyValue("AppStatus", ref _AppStatus, value);
            }
        }

        private string _EventorPurpose;
        [XafDisplayName("Event/Purpose")]
        [Size(254)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(18), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string EventorPurpose
        {
            get { return _EventorPurpose; }
            set
            {
                SetPropertyValue("EventorPurpose", ref _EventorPurpose, value);
            }
        }

        private string _Remarks;
        [XafDisplayName("Remarks")]
        [Size(254)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(20), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                SetPropertyValue("Remarks", ref _Remarks, value);
            }
        }

        private string _DeliveryAddress;
        [XafDisplayName("Delivery Address")]
        [Size(254)]
        [ImmediatePostData]
        [Index(21), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string DeliveryAddress
        {
            get { return _DeliveryAddress; }
            set
            {
                SetPropertyValue("DeliveryAddress", ref _DeliveryAddress, value);
            }
        }

        private string _SAPDocNum;
        [XafDisplayName("SAP Doc Num")]
        [Appearance("SAPDocNum", Enabled = false)]
        [Index(25), VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string SAPDocNum
        {
            get { return _SAPDocNum; }
            set
            {
                SetPropertyValue("SAPDocNum", ref _SAPDocNum, value);
            }
        }

        private decimal _TotalAmount;
        [XafDisplayName("Total Amount")]
        [Index(28), VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:n2}")]
        [ModelDefault("EditMask", "{0:n2}")]
        [Appearance("TotalAmount", Enabled = false)]
        [DbType("numeric(19,6)")]
        public decimal TotalAmount
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    decimal temp = 0;
                    foreach (PurchaseRequestDetails dtl in this.PurchaseRequestDetails)
                    {
                        temp = temp + dtl.Total;
                    }
                    return temp;
                }
                else
                {
                    return _TotalAmount;
                }
            }
            set { SetPropertyValue("TotalAmount", ref _TotalAmount, value); }
        }

        private string _NextApprover;
        [XafDisplayName("Next Approver")]
        [Index(82), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("Next Approver", Enabled = false)]
        public string NextApprover
        {
            get { return _NextApprover; }
            set
            {
                SetPropertyValue("NextApprover", ref _NextApprover, value);
            }
        }

        private string _WhoApprove;
        [XafDisplayName("WhoApprove")]
        [Index(83), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("WhoApprove", Enabled = false)]
        public string WhoApprove
        {
            get { return _WhoApprove; }
            set
            {
                SetPropertyValue("WhoApprove", ref _WhoApprove, value);
            }
        }

        private bool _Sap;
        [XafDisplayName("Sap")]
        [Index(88), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool Sap
        {
            get { return _Sap; }
            set
            {
                SetPropertyValue("Sap", ref _Sap, value);
            }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }

        [Association("PurchaseRequests-PurchaseRequestDetails")]
        [XafDisplayName("Items")]
        public XPCollection<PurchaseRequestDetails> PurchaseRequestDetails
        {
            get { return GetCollection<PurchaseRequestDetails>("PurchaseRequestDetails"); }
        }

        [Association("PurchaseRequests-PurchaseRequestDocTrail")]
        [XafDisplayName("Status History")]
        public XPCollection<PurchaseRequestDocTrail> PurchaseRequestDocTrail
        {
            get { return GetCollection<PurchaseRequestDocTrail>("PurchaseRequestDocTrail"); }
        }

        [Association("PurchaseRequests-PurchaseRequestAppStage")]
        [XafDisplayName("Approval Stage")]
        public XPCollection<PurchaseRequestAppStage> PurchaseRequestAppStage
        {
            get { return GetCollection<PurchaseRequestAppStage>("PurchaseRequestAppStage"); }
        }

        [Association("PurchaseRequests-PurchaseRequestAppStatus")]
        [XafDisplayName("Approval Status")]
        public XPCollection<PurchaseRequestAppStatus> PurchaseRequestAppStatus
        {
            get { return GetCollection<PurchaseRequestAppStatus>("PurchaseRequestAppStatus"); }
        }

        [Association("PurchaseRequests-PurchaseRequestAttachment")]
        [XafDisplayName("Attachment")]
        public XPCollection<PurchaseRequestAttachment> PurchaseRequestAttachment
        {
            get { return GetCollection<PurchaseRequestAttachment>("PurchaseRequestAttachment"); }
        }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && (Session.ObjectLayer is SimpleObjectLayer)
                        )
            {
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                if (user != null)
                {
                    UpdateUser = user.Oid.ToString();
                }
                UpdateDate = DateTime.Now;

                if (Session.IsNewObject(this))
                {
                    PurchaseRequestDocTrail ds = new PurchaseRequestDocTrail(Session);
                    ds.DocStatus = DocStatus.Draft;
                    ds.DocRemarks = "";
                    if (user != null)
                    {
                        ds.CreateUser = Session.GetObjectByKey<SystemUsers>(user.Oid);
                        ds.UpdateUser = Session.GetObjectByKey<SystemUsers>(user.Oid);
                    }
                    ds.CreateDate = DateTime.Now;
                    ds.UpdateDate = DateTime.Now;
                    this.PurchaseRequestDocTrail.Add(ds);
                }
            }
        }
    }
}