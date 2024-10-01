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

namespace PRPortal.Module.BusinessObjects.PR
{
    [DefaultClassOptions]
    [Appearance("PRTrxEmaillog1", AppearanceItemType = "Action", TargetItems = "New", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("PRTrxEmaillog2", AppearanceItemType = "Action", TargetItems = "Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("PRTrxEmaillog3", AppearanceItemType = "Action", TargetItems = "Link", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("PRTrxEmaillog4", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("PRTrxEmaillog5", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [DefaultProperty("StatusInfo")]
    [XafDisplayName("PR Document Email Log")]
    public class PurchaseRequestEmailLog : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PurchaseRequestEmailLog(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private DateTime _EmailDateTime;
        [XafDisplayName("Email DateTime")]
        [Appearance("EmailDateTime", Enabled = false)]
        [Index(1), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime EmailDateTime
        {
            get { return _EmailDateTime; }
            set
            {
                SetPropertyValue("EmailDateTime", ref _EmailDateTime, value);
            }
        }

        private string _EmailAddress;
        [XafDisplayName("Email Address")]
        [Appearance("EmailAddress", Enabled = false)]
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                SetPropertyValue("EmailAddress", ref _EmailAddress, value);
            }
        }

        private string _EmailSubject;
        [XafDisplayName("Email Subject")]
        [Appearance("EmailSubject", Enabled = false)]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EmailSubject
        {
            get { return _EmailSubject; }
            set
            {
                SetPropertyValue("EmailSubject", ref _EmailSubject, value);
            }
        }

        private string _EmailContent;
        [XafDisplayName("Email Content")]
        [Appearance("EmailContent", Enabled = false)]
        [Size(9999)]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EmailContent
        {
            get { return _EmailContent; }
            set
            {
                SetPropertyValue("EmailContent", ref _EmailContent, value);
            }
        }

        private string _EmailType;
        [XafDisplayName("Email Type")]
        [Appearance("EmailType", Enabled = false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EmailType
        {
            get { return _EmailType; }
            set
            {
                SetPropertyValue("EmailType", ref _EmailType, value);
            }
        }

        private PurchaseRequests _PurchaseRequests;
        [Association("PurchaseRequests-PurchaseRequestEmailLog")]
        [Index(99), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PurchaseRequests", Enabled = false)]
        public PurchaseRequests PurchaseRequests
        {
            get { return _PurchaseRequests; }
            set { SetPropertyValue("PurchaseRequests", ref _PurchaseRequests, value); }
        }
    }
}