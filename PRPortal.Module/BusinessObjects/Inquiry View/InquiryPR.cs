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

namespace PRPortal.Module.BusinessObjects.Inquiry_View
{
    [DefaultClassOptions]
    [NavigationItem("Reports")]
    [XafDisplayName("Purchase Requests Inquiry")]
    [DefaultProperty("PRNo")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [DevExpress.ExpressApp.ConditionalAppearance.Appearance("hideSave", AppearanceItemType = "Action", TargetItems = "Save", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideResetViewSetting", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]

    public class InquiryPR : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InquiryPR(Session session)
            : base(session) {
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("No")]
        [Appearance("No", Enabled = false)]
        [Index(0)]
        public string No
        {
            get; set;
        }

        [XafDisplayName("Approved PR Received Date")]
        [Appearance("ReceivedDate", Enabled = false)]
        [Index(3)]
        public DateTime ReceivedDate
        {
            get; set;
        }

        [XafDisplayName("PR No.")]
        [Appearance("PRNo", Enabled = false)]
        [Index(5)]
        public string PRNo
        {
            get; set;
        }

        [XafDisplayName("Requestor Email")]
        [Appearance("RequestorEmail", Enabled = false)]
        [Index(8)]
        public string RequestorEmail
        {
            get; set;
        }

        [XafDisplayName("Item Code")]
        [Appearance("ItemCode", Enabled = false)]
        [Index(10)]
        public string ItemCode
        {
            get; set;
        }

        [XafDisplayName("Description")]
        [Appearance("Description", Enabled = false)]
        [Index(13)]
        public string Description
        {
            get; set;
        }

        [XafDisplayName("Event/Purpose")]
        [Appearance("EventorPurpose", Enabled = false)]
        [Index(15)]
        public string EventorPurpose
        {
            get; set;
        }

        [XafDisplayName("Proc PIC")]
        [Appearance("ProcPIC", Enabled = false)]
        [Index(18)]
        public string ProcPIC
        {
            get; set;
        }

        [XafDisplayName("PR Status")]
        [Appearance("PRStatus", Enabled = false)]
        [Index(20)]
        public string PRStatus
        {
            get; set;
        }

        [XafDisplayName("Convert to PO")]
        [Appearance("ConvertToPO", Enabled = false)]
        [Index(23)]
        public string ConvertToPO
        {
            get; set;
        }

        [XafDisplayName("PO Number")]
        [Appearance("PONumber", Enabled = false)]
        [Index(25)]
        public string PONumber
        {
            get; set;
        }
    }
}