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
    [XafDisplayName("Purchase Orders Inquiry")]
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

    public class InquiryPO : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InquiryPO(Session session)
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
        [XafDisplayName("No")]
        [Appearance("No", Enabled = false)]
        [Index(0)]
        public string No
        {
            get; set;
        }

        [XafDisplayName("PO Sent Date")]
        [Appearance("POSentDate", Enabled = false)]
        [Index(3)]
        public DateTime POSentDate
        {
            get; set;
        }

        [XafDisplayName("PO No.")]
        [Appearance("PONo", Enabled = false)]
        [Index(5)]
        public string PONo
        {
            get; set;
        }

        [XafDisplayName("Date Required")]
        [Appearance("DateRequired", Enabled = false)]
        [Index(8)]
        public DateTime DateRequired
        {
            get; set;
        }

        [XafDisplayName("Supplier")]
        [Appearance("Supplier", Enabled = false)]
        [Index(10)]
        public string Supplier
        {
            get; set;
        }

        [XafDisplayName("Item Code")]
        [Appearance("ItemCode", Enabled = false)]
        [Index(13)]
        public string ItemCode
        {
            get; set;
        }

        [XafDisplayName("Description")]
        [Appearance("Description", Enabled = false)]
        [Index(15)]
        public string Description
        {
            get; set;
        }

        [XafDisplayName("Quantity")]
        [Appearance("Quantity", Enabled = false)]
        [DbType("numeric(18,6)")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [ModelDefault("EditMask", "d")]
        [Index(18)]
        public decimal Quantity
        {
            get; set;
        }

        [XafDisplayName("Unit Price")]
        [DbType("numeric(18,6)")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [ModelDefault("EditMask", "d")]
        [Appearance("UnitPrice", Enabled = false)]
        [Index(20)]
        public decimal UnitPrice
        {
            get; set;
        }

        [XafDisplayName("Total Price")]
        [DbType("numeric(18,6)")]
        [ModelDefault("DisplayFormat", "{0:N0}")]
        [ModelDefault("EditMask", "d")]
        [Appearance("TotalPrice", Enabled = false)]
        [Index(23)]
        public decimal TotalPrice
        {
            get; set;
        }

        [XafDisplayName("Payment Term")]
        [Appearance("PaymentTerm", Enabled = false)]
        [Index(25)]
        public string PaymentTerm
        {
            get; set;
        }

        [XafDisplayName("ETA/Lead Time")]
        [Appearance("ETA", Enabled = false)]
        [Index(28)]
        public string ETA
        {
            get; set;
        }

        [XafDisplayName("Good Received Date")]
        [Appearance("GoodReceivedDate", Enabled = false)]
        [Index(30)]
        public DateTime GoodReceivedDate
        {
            get; set;
        }

        [XafDisplayName("On Time")]
        [Appearance("OnTime", Enabled = false)]
        [Index(33)]
        public string OnTime
        {
            get; set;
        }

        [XafDisplayName("Delivery Qty before")]
        [Appearance("DeliveryQtybefore", Enabled = false)]
        [Index(35)]
        public int DeliveryQtybefore
        {
            get; set;
        }

        [XafDisplayName("On the Required Date")]
        [Appearance("OntheRequiredDate", Enabled = false)]
        [Index(38)]
        public string OntheRequiredDate
        {
            get; set;
        }

        [XafDisplayName("OTIF")]
        [Appearance("OTIF", Enabled = false)]
        [Index(40)]
        public string OTIF
        {
            get; set;
        }

        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [Index(43)]
        public string Status
        {
            get; set;
        }
    }
}