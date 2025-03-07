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

namespace PRPortal.Module.BusinessObjects.View
{
    [DefaultClassOptions]
    [XafDisplayName("Company View")]
    [DefaultProperty("CompanyName")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideResetViewSetting", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]

    public class vwCompany : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public vwCompany(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Prikey")]
        [Appearance("Prikey", Enabled = false)]
        [Index(0), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Prikey
        {
            get; set;
        }

        [XafDisplayName("Company Code")]
        [Appearance("CompanyCode", Enabled = false)]
        [Index(1)]
        public string CompanyCode
        {
            get; set;
        }

        [XafDisplayName("Company Name")]
        [Appearance("CompanyName", Enabled = false)]
        [Index(3)]
        public string CompanyName
        {
            get; set;
        }

        [XafDisplayName("Staff ID")]
        [Appearance("StaffID", Enabled = false)]
        [Index(5)]
        public string StaffID
        {
            get; set;
        }

        [XafDisplayName("Staff Name")]
        [Appearance("StaffName", Enabled = false)]
        [Index(8)]
        public string StaffName
        {
            get; set;
        }

        [XafDisplayName("IsActive")]
        [Appearance("IsActive", Enabled = false)]
        [Index(10)]
        public string IsActive
        {
            get; set;
        }

        [XafDisplayName("IsDefault")]
        [Appearance("IsDefault", Enabled = false)]
        [Index(13)]
        public string IsDefault
        {
            get; set;
        }

        [XafDisplayName("Currency")]
        [Appearance("Currency", Enabled = false)]
        [NoForeignKey]
        [Index(15)]
        public vwCurrency Currency
        {
            get; set;
        }

        [XafDisplayName("ReportSuffix")]
        [Appearance("ReportSuffix", Enabled = false)]
        [Index(18)]
        public string ReportSuffix
        {
            get; set;
        }
    }
}