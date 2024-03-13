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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using PRPortal.Module.BusinessObjects.Setup;

namespace PRPortal.Module.BusinessObjects
{
    [DefaultClassOptions]

    public class SystemUsers : PermissionPolicyUser
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SystemUsers(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private StaffInfo _Staff;
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [RuleRequiredField(DefaultContexts.Save)]
        public StaffInfo Staff
        {
            get { return _Staff; }
            set { SetPropertyValue("Staff", ref _Staff, value); }
        }
    }
}