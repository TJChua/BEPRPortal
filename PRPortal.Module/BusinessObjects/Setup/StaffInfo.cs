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

namespace PRPortal.Module.BusinessObjects.Setup
{
    [DefaultClassOptions]
    [NavigationItem("Setup")]
    [DefaultProperty("StaffName")]
    [XafDisplayName("Staff Info")]
    //[Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideResetViewSetting", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("hideSaveAndClose", AppearanceItemType = "Action", TargetItems = "SaveAndClose", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("hideSaveAndNew", AppearanceItemType = "Action", TargetItems = "SaveAndNew", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    public class StaffInfo : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public StaffInfo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            IsActive = true;
        }

        private string _StaffID;
        [ImmediatePostData]
        [XafDisplayName("Staff ID")]
        [Appearance("StaffID", Enabled = false, Criteria = "not IsNew")]
        [Index(0)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string StaffID
        {
            get { return _StaffID; }
            set
            {
                SetPropertyValue("StaffID", ref _StaffID, value);
            }
        }

        private string _StaffName;
        [ImmediatePostData]
        [XafDisplayName("Staff Name")]
        [Index(3)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string StaffName
        {
            get { return _StaffName; }
            set
            {
                SetPropertyValue("StaffName", ref _StaffName, value);
            }
        }

        private string _EmployeeNo;
        [ImmediatePostData]
        [XafDisplayName("Employee No")]
        [Index(5)]
        public string EmployeeNo
        {
            get { return _EmployeeNo; }
            set
            {
                SetPropertyValue("EmployeeNo", ref _EmployeeNo, value);
            }
        }

        private string _StaffEmail;
        [ImmediatePostData]
        [XafDisplayName("Staff Email")]
        [Index(8)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string StaffEmail
        {
            get { return _StaffEmail; }
            set
            {
                SetPropertyValue("StaffEmail", ref _StaffEmail, value);
            }
        }

        private string _StaffContactNo;
        [ImmediatePostData]
        [XafDisplayName("Staff Contact No")]
        [Index(10)]
        public string StaffContactNo
        {
            get { return _StaffContactNo; }
            set
            {
                SetPropertyValue("StaffContactNo", ref _StaffContactNo, value);
            }
        }

        private vwDepartment _Department;
        [NoForeignKey]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [XafDisplayName("Department")]
        [DataSourceCriteria("CompanyCode = '@this.Company.CompanyCode'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(13), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public vwDepartment Department
        {
            get { return _Department; }
            set
            {
                SetPropertyValue("Department", ref _Department, value);
            }
        }

        private vwCostCentre _CostCentre;
        [NoForeignKey]
        [ImmediatePostData]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [XafDisplayName("Cost Centre")]
        [DataSourceCriteria("CompanyCode = '@this.Company.CompanyCode'")]
        [Index(15), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public vwCostCentre CostCentre
        {
            get { return _CostCentre; }
            set
            {
                SetPropertyValue("CostCentre", ref _CostCentre, value);
            }
        }

        private Company _Company;
        [XafDisplayName("Default Company")]
        [ImmediatePostData]
        [Index(18), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = 'True'")]
        public Company Company
        {
            get { return _Company; }
            set
            {
                SetPropertyValue("Company", ref _Company, value);
            }
        }

        private bool _IsActive;
        [XafDisplayName("Active")]
        [Index(30)]
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                SetPropertyValue("IsActive", ref _IsActive, value);
            }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }

        [Association("StaffInfo-StaffInfoCompany")]
        [XafDisplayName("Company")]
        public XPCollection<StaffInfoCompany> StaffInfoCompany
        {
            get { return GetCollection<StaffInfoCompany>("StaffInfoCompany"); }
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
    }
}