using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using PRPortal.Module.BusinessObjects;
using PRPortal.Module.BusinessObjects.PR;

namespace PRPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterControllers : ViewController
    {
        public FilterControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                if (View.Id == "PurchaseRequests_ListView")
                {
                    PermissionPolicyRole AdminRole = ObjectSpace.FindObject<PermissionPolicyRole>(CriteriaOperator.Parse("IsCurrentUserInRole('Administrators')"));

                    if (AdminRole == null)
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[CreateUser] = ?", user.Oid.ToString());
                    }
                }
            }

            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                if (View.Id == "PurchaseRequests_ListView_PendApp")
                {
                    PermissionPolicyRole AppRole = ObjectSpace.FindObject<PermissionPolicyRole>(CriteriaOperator.Parse("IsCurrentUserInRole('ApprovalUserRole')"));

                    if (AppRole != null)
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse(" [AppStatus] = ? and Contains([NextApprover],?)", 2, user.Staff.StaffName);
                    }
                }
            }

            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                if (View.Id == "PurchaseRequests_ListView_Approved")
                {
                    PermissionPolicyRole AppRole = ObjectSpace.FindObject<PermissionPolicyRole>(CriteriaOperator.Parse("IsCurrentUserInRole('ApprovalUserRole')"));

                    if (AppRole != null)
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Contains([WhoApprove],?)", user.Staff.StaffName);

                    }
                }
            }

            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                if (View.Id == "PurchaseRequests_ListView_Procurement")
                {
                    ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("(Status in (?, ?)) and not IsNullOrEmpty(PIC)", 0, 7);
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
