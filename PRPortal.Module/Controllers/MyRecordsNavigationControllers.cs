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
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace PRPortal.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MyRecordsNavigationControllers : WebRecordsNavigationController
    {
        public MyRecordsNavigationControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            if (Frame is PopupWindow)
            {
                LinkToListViewController linkToListViewController = Frame.GetController<LinkToListViewController>();
                if (linkToListViewController.Link != null && linkToListViewController.Link.ListView.Model.DataAccessMode == CollectionSourceDataAccessMode.Client)
                {
                    var orderedObjects = ((IControlOrderProvider)linkToListViewController.Link.ListView.Editor).GetOrderedObjects();
                    OrderProviderSource.OrderProvider = new StandaloneOrderProvider(ObjectSpace, orderedObjects);
                    UpdateActionState();
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
