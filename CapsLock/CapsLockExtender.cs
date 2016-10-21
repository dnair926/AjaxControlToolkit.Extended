using AjaxControlToolkit.Extenders;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Constants.CapsLockScriptName + Constants.JsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CapsLockScriptName + Constants.MinifiedJsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CapsLockStyleName + Constants.CssPostfix, "text/css")]
[assembly: WebResource(Constants.CapsLockStyleName + Constants.MinCssPostfix, "text/css")]
[assembly: WebResource(Constants.CapsLockAlertLargeImage, "image/gif")]
[assembly: WebResource(Constants.CapsLockAlertSmallImage, "image/gif")]
[assembly: WebResource(Constants.CapsLockCloseImage, "image/gif")]

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
            get { return GetPropertyValue(nameof(WarningIconImageUrl), (string)null) ?? (DesignMode ? "" : Page.ClientScript.GetWebResourceUrl(typeof(CapsLockExtender), Constants.CapsLockAlertLargeImage)); }
            set { SetPropertyValue(nameof(WarningIconImageUrl), value); }
        }

        [DefaultValue("")]
        [UrlProperty]
        [ExtenderControlProperty]
        [ClientPropertyName("closeImageUrl")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Using string to avoid Uri complications")]
        public string CloseImageUrl
        {
            get { return GetPropertyValue(nameof(CloseImageUrl), (string)null) ?? (DesignMode ? "" : Page.ClientScript.GetWebResourceUrl(typeof(CapsLockExtender), Constants.CapsLockCloseImage)); }
            set { SetPropertyValue(nameof(CloseImageUrl), value); }
        }

        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("cssClass")]
        public string CssClass
        {
            get { return GetPropertyValue(nameof(CssClass), string.Empty); }
            set { SetPropertyValue(nameof(CssClass), value); }
        }

        [DefaultValue(typeof(Unit), "")]
        [ExtenderControlProperty]
        [ClientPropertyName("width")]
        public Unit Width
        {
            get { return GetPropertyValue(nameof(Width), Unit.Empty); }
            set { SetPropertyValue(nameof(Width), value); }
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
