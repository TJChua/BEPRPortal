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

namespace PRPortal.Module.BusinessObjects
{
    public enum DocTypeList
    {
        [XafDisplayName("Purchase Request")] PR = 0,
        [XafDisplayName("Purchase Order")] PO = 1
    }

    public enum DocStatus
    {
        [XafDisplayName("Draft")] Draft = 0,
        [XafDisplayName("Submitted")] Submitted = 1,
        [XafDisplayName("Cancelled")] Cancelled = 2,
        [XafDisplayName("Closed")] Closed = 3,
        [XafDisplayName("Pending Post")] PendPost = 4,
        [XafDisplayName("Posted")] Post = 5,
        [XafDisplayName("Rejected")] Rejected = 6,
        [XafDisplayName("Revise")] Resubmit = 7,
        [XafDisplayName("Pending PIC")] PendingPIC = 8
    }

    public enum ApprovalStatusType
    {
        Not_Applicable = 0,
        Approved = 1,
        Required_Approval = 2,
        Rejected = 3
    }

    public enum ApprovalActions
    {
        [XafDisplayName("Please Select Action...")] NA = 0,
        [XafDisplayName("Approve")] Yes = 1,
        [XafDisplayName("Reject")] No = 2,
        [XafDisplayName("Resubmit")] Resubmit = 3
    }

    public enum ApprovalTypes
    {
        Document = 0
    }
}