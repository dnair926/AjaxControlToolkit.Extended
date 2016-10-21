using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Extenders
{
    [TargetControlType(typeof(TextBox))]
    [ClientScriptResource("WebExtenders.CapsLockBehavior", Constants.CapsLockScriptName)]
    [ClientCssResource(Constants.CapsLockStyleName)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(PopupExtender), 1)]
    [RequiredScript(typeof(ThreadingScripts), 2)]
    public class CapsLockExtender : ExtenderControlBase
    {
        /// <summary>
        /// Constructs a new CapsLockExtender.
        /// </summary>
        public CapsLockExtender()
        {
            EnableClientState = true;
        }

        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("warningIconImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string WarningIconImageUrl
        {
            get { return GetPropertyValue("WarningIconImageUrl", (string)null) ?? (DesignMode ? "" : Page.ClientScript.GetWebResourceUrl(typeof(CapsLockExtender), "AjaxControlToolkit.Images.alert-large.gif")); }
            set { SetPropertyValue("WarningIconImageUrl", value); }
        }

        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("closeImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string CloseImageUrl
        {
            get { return GetPropertyValue("CloseImageUrl", (string)null) ?? (DesignMode ? "" : Page.ClientScript.GetWebResourceUrl(typeof(CapsLockExtender), "AjaxControlToolkit.Images.close.gif")); }
            set { SetPropertyValue("CloseImageUrl", value); }
        }

        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("cssClass")]
        public string CssClass
        {
            get { return GetPropertyValue("CssClass", string.Empty); }
            set { SetPropertyValue("CssClass", value); }
        }

        [DefaultValue(typeof(Unit), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("width")]
        public Unit Width
        {
            get { return GetPropertyValue("Width", Unit.Empty); }
            set { SetPropertyValue("Width", value); }
        }

        /// <summary>
        /// Convert server IDs into ClientIDs for animations
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            // Get the associated BaseValidator and set ClientState accordingly
            BaseValidator baseValidator = TargetControl as BaseValidator;
            if ((null != baseValidator) && !baseValidator.IsValid)
            {
                ClientState = "INVALID";
            }
            else
            {
                ClientState = "";
            }

            base.OnPreRender(e);
        }
    }
}
