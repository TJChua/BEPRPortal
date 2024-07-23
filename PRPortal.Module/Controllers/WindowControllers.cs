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
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PRPortal.Module.BusinessObjects;

namespace PRPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WindowControllers : WindowController
    {
        public WindowControllers()
        { 
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            string ActionItemId = "MyDetails";

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

            MyDetailsController myDetailsController = Frame.GetController<MyDetailsController>();
            if (myDetailsController != null)
            {
                if (user.Staff != null && user.Staff.StaffName != null)
                {
                    myDetailsController.Actions[ActionItemId].Caption = user.Staff.StaffName;
                }
                else
                {
                    myDetailsController.Actions[ActionItemId].Caption = user.UserName;
                }
            }
        }
        protected override void OnDeactivated() {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
