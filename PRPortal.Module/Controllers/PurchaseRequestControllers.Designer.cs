namespace PRPortal.Module.Controllers
{
    partial class PurchaseRequestControllers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SubmitPR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.CancelPR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DuplicatePR = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ApprovePR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.AddItem = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.SearchItem = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.AddNewItem = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.EscalateUser = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.SaveDetails = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.NewItem = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SubmitPR
            // 
            this.SubmitPR.AcceptButtonCaption = null;
            this.SubmitPR.CancelButtonCaption = null;
            this.SubmitPR.Caption = "Submit";
            this.SubmitPR.Category = "ObjectsCreation";
            this.SubmitPR.ConfirmationMessage = null;
            this.SubmitPR.Id = "SubmitPR";
            this.SubmitPR.ToolTip = null;
            this.SubmitPR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.SubmitPR_CustomizePopupWindowParams);
            this.SubmitPR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.SubmitPR_Execute);
            // 
            // CancelPR
            // 
            this.CancelPR.AcceptButtonCaption = null;
            this.CancelPR.CancelButtonCaption = null;
            this.CancelPR.Caption = "Cancel";
            this.CancelPR.Category = "ObjectsCreation";
            this.CancelPR.ConfirmationMessage = null;
            this.CancelPR.Id = "CancelPR";
            this.CancelPR.ToolTip = null;
            this.CancelPR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CancelPR_CustomizePopupWindowParams);
            this.CancelPR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelPR_Execute);
            // 
            // DuplicatePR
            // 
            this.DuplicatePR.Caption = "Duplicate";
            this.DuplicatePR.Category = "ObjectsCreation";
            this.DuplicatePR.ConfirmationMessage = null;
            this.DuplicatePR.Id = "DuplicatePR";
            this.DuplicatePR.ToolTip = null;
            this.DuplicatePR.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DuplicatePR_Execute);
            // 
            // ApprovePR
            // 
            this.ApprovePR.AcceptButtonCaption = null;
            this.ApprovePR.CancelButtonCaption = null;
            this.ApprovePR.Caption = "Approve";
            this.ApprovePR.Category = "ObjectsCreation";
            this.ApprovePR.ConfirmationMessage = null;
            this.ApprovePR.Id = "ApprovePR";
            this.ApprovePR.ToolTip = null;
            this.ApprovePR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ApprovePR_CustomizePopupWindowParams);
            this.ApprovePR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApprovePR_Execute);
            // 
            // AddItem
            // 
            this.AddItem.AcceptButtonCaption = null;
            this.AddItem.CancelButtonCaption = null;
            this.AddItem.Caption = "Add Item";
            this.AddItem.Category = "ListView";
            this.AddItem.ConfirmationMessage = null;
            this.AddItem.Id = "AddItem";
            this.AddItem.ToolTip = null;
            this.AddItem.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddItem_CustomizePopupWindowParams);
            this.AddItem.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddItem_Execute);
            // 
            // SearchItem
            // 
            this.SearchItem.Caption = "Search";
            this.SearchItem.Category = "ListView";
            this.SearchItem.ConfirmationMessage = null;
            this.SearchItem.Id = "SearchItem";
            this.SearchItem.ToolTip = null;
            this.SearchItem.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SearchItem_Execute);
            // 
            // AddNewItem
            // 
            this.AddNewItem.Caption = "Add";
            this.AddNewItem.Category = "ListView";
            this.AddNewItem.ConfirmationMessage = null;
            this.AddNewItem.Id = "AddNewItem";
            this.AddNewItem.ToolTip = null;
            this.AddNewItem.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AddNewItem_Execute);
            // 
            // EscalateUser
            // 
            this.EscalateUser.AcceptButtonCaption = null;
            this.EscalateUser.CancelButtonCaption = null;
            this.EscalateUser.Caption = "Escalate User";
            this.EscalateUser.Category = "ObjectsCreation";
            this.EscalateUser.ConfirmationMessage = null;
            this.EscalateUser.Id = "EscalateUser";
            this.EscalateUser.ToolTip = null;
            this.EscalateUser.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.EscalateUser_CustomizePopupWindowParams);
            this.EscalateUser.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.EscalateUser_Execute);
            // 
            // SaveDetails
            // 
            this.SaveDetails.Caption = "Save";
            this.SaveDetails.Category = "PopupActions";
            this.SaveDetails.ConfirmationMessage = null;
            this.SaveDetails.Id = "SaveDetails";
            this.SaveDetails.ToolTip = null;
            this.SaveDetails.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SaveDetails_Execute);
            // 
            // NewItem
            // 
            this.NewItem.Caption = "Save";
            this.NewItem.Category = "PopupActions";
            this.NewItem.ConfirmationMessage = null;
            this.NewItem.Id = "NewItem";
            this.NewItem.ToolTip = null;
            this.NewItem.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.NewItem_Execute);
            // 
            // PurchaseRequestControllers
            // 
            this.Actions.Add(this.SubmitPR);
            this.Actions.Add(this.CancelPR);
            this.Actions.Add(this.DuplicatePR);
            this.Actions.Add(this.ApprovePR);
            this.Actions.Add(this.AddItem);
            this.Actions.Add(this.SearchItem);
            this.Actions.Add(this.AddNewItem);
            this.Actions.Add(this.EscalateUser);
            this.Actions.Add(this.SaveDetails);
            this.Actions.Add(this.NewItem);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction SubmitPR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelPR;
        private DevExpress.ExpressApp.Actions.SimpleAction DuplicatePR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApprovePR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddItem;
        private DevExpress.ExpressApp.Actions.SimpleAction SearchItem;
        private DevExpress.ExpressApp.Actions.SimpleAction AddNewItem;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction EscalateUser;
        private DevExpress.ExpressApp.Actions.SimpleAction SaveDetails;
        private DevExpress.ExpressApp.Actions.SimpleAction NewItem;
    }
}
