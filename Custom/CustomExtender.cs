using AjaxControlToolkit.Extenders;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Constants.CustomScriptName + Constants.JsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CustomScriptName + Constants.MinifiedJsPostfix, "text/javascript")]

namespace AjaxControlToolkit.Extenders
{
    [ClientScriptResource("WebExtenders.CustomBehavior", Constants.CustomScriptName)]
    [TargetControlType(typeof(WebControl))]
    public class CustomExtender : ExtenderControlBase
    {
        /// <summary>
        /// Specify the type of control
        /// </summary>
        [DefaultValue(CustomTargetControlType.DropdownList)]
        [ExtenderControlProperty]
        public CustomTargetControlType TargetControlType
        {
            get { return GetPropertyValue(nameof(TargetControlType), CustomTargetControlType.DropdownList); }
            set { SetPropertyValue(nameof(TargetControlType), value); }
        }

        /// <summary>
        /// Specify the type of control
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string ArgumentValues
        {
            get { return GetPropertyValue(nameof(ArgumentValues), ""); }
            set { SetPropertyValue(nameof(ArgumentValues), value); }
        }

        /// <summary>
        /// Handler to attach to the client-side itemSelected event
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlEvent]
        [ClientPropertyName("userAction")]
        public string OnUserAction
        {
            get { return GetPropertyValue(nameof(OnUserAction), string.Empty); }
            set { SetPropertyValue(nameof(OnUserAction), value); }
        }

        /// <summary>
        /// Handler to attach to the client-side itemSelected event
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlEvent]
        [ClientPropertyName("pageInit")]
        public string OnPageInit
        {
            get { return GetPropertyValue(nameof(OnPageInit), string.Empty); }
            set { SetPropertyValue(nameof(OnPageInit), value); }
        }
    }
}
