using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using PRPortal.Module.BusinessObjects;
using PRPortal.Module.BusinessObjects.PR;
using PRPortal.Module.BusinessObjects.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integration
{
    static class Program
    {
        private static System.Threading.Mutex mutex = null;
        [STAThread]
        static void Main()
        {
            string appName = ConfigurationManager.AppSettings.Get("AppName").ToString();
            bool createdNew;

            mutex = new System.Threading.Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                Application.Exit();
                return;
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            RegisterEntities();
            AuthenticationStandard authentication = new AuthenticationStandard();
            SecurityStrategyComplex security = new SecurityStrategyComplex(typeof(PermissionPolicyUser), typeof(PermissionPolicyRole), authentication);
            security.RegisterXPOAdapterProviders();
            security.AllowAnonymousAccess = true;
            string connectionString = ConfigurationManager.ConnectionStrings["DataSourceConnectionString"].ConnectionString;
            IObjectSpaceProvider objectSpaceProvider = new SecuredObjectSpaceProvider(security, connectionString, null);

            #region Allow Store Proc
            ((SecuredObjectSpaceProvider)objectSpaceProvider).AllowICommandChannelDoWithSecurityContext = true;
            #endregion

            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Integration mainForm = new Integration(security, objectSpaceProvider);

            Application.Run(mainForm);
        }

        private static void RegisterEntities()
        {
            XpoTypesInfoHelper.GetXpoTypeInfoSource();

            // PR
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequests));
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequestAppStage));
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequestAppStatus));
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequestAttachment));
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequestDetails));
            XafTypesInfo.Instance.RegisterEntity(typeof(PurchaseRequestDocTrail));

            // View
            XafTypesInfo.Instance.RegisterEntity(typeof(vwBusinessPartner));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwCostCentre));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwDepartment));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwItemMasters));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwPaymentTerm));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwTax));
            XafTypesInfo.Instance.RegisterEntity(typeof(vwWarehouse));

            XafTypesInfo.Instance.RegisterEntity(typeof(SystemUsers));
            XafTypesInfo.Instance.RegisterEntity(typeof(PermissionPolicyUser));
            XafTypesInfo.Instance.RegisterEntity(typeof(PermissionPolicyRole));
        }
    }
}
