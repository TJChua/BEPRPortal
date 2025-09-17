using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace PRPortal.Module.BusinessObjects.Setup
{
    [DefaultClassOptions]
    [NavigationItem("Test Menu")] // This creates a new menu called "Test Menu"
    [XafDisplayName("Test Menu Item")]
    public class TestMenuItem : XPObject
    {
        public TestMenuItem(Session session) : base(session) { }

        public string Name { get; set; }
        public string Description { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (e.g. initialize default values)
        }



    }
}
