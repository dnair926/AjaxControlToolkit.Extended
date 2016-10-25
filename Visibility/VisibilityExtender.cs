using AjaxControlToolkit.Extenders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: WebResource(Constants.VisibilityScriptName + Constants.JsPostfix, "text/javascript")]
[assembly: WebResource(Constants.VisibilityScriptName + Constants.MinifiedJsPostfix, "text/javascript")]

namespace AjaxControlToolkit.Extenders
{
    [TargetControlType(typeof(WebControl))]
    [TargetControlType(typeof(HtmlControl))]
    [ClientScriptResource("WebExtenders.VisibilityBehavior", Constants.VisibilityScriptName)]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(CommonScripts))]
    public class VisibilityExtender : ExtenderControlBase
    {

        /// <summary>
        /// Specify if Client Actions should be ignored. Use this instead of disabling this control so that the TargetControl will be rendered based on the ParentControl value
        /// </summary>
        [DefaultValue(false)]
        [ExtenderControlProperty]
        public bool DisableClientEvent
        {
            get { return GetPropertyValue("DisableClientEvent", false); }
            set { SetPropertyValue("DisableClientEvent", value); }
        }

        /// <summary>
        /// Specify the type of control
        /// </summary>
        [DefaultValue(TargetControlTypes.Panel)]
        [ExtenderControlProperty]
        public TargetControlTypes TargetControlType
        {
            get { return GetPropertyValue("TargetControlType", TargetControlTypes.Panel); }
            set { SetPropertyValue("TargetControlType", value); }
        }

        /// <summary>
        /// Specify the type of control
        /// </summary>
        [DefaultValue(ParentControlTypes.DropdownList)]
        [ExtenderControlProperty]
        public ParentControlTypes ParentControlType
        {
            get { return GetPropertyValue("ParentControlType", ParentControlTypes.DropdownList); }
            set
            {
                SetPropertyValue("ParentControlType", value);
            }
        }

        /// <summary>
        /// Specify the type of control
        /// </summary>
        [DefaultValue(VisibilityMode.Show)]
        [ExtenderControlProperty]
        public VisibilityMode ActionOnValueSelected
        {
            get { return GetPropertyValue("ActionOnValueSelected", VisibilityMode.Show); }
            set { SetPropertyValue("ActionOnValueSelected", value); }
        }

        /// <summary>
        /// Comma delimited Values to check to make the target control visible/hidden. 
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        public string ValuesToCheck
        {
            get { return GetPropertyValue("ValuesToCheck", ""); }
            set { SetPropertyValue("ValuesToCheck", value); }
        }

        [IDReferenceProperty(typeof(WebControl))]
        [DefaultValue("")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        [ExtenderControlProperty]
        public string ParentControlID
        {
            get { return GetPropertyValue("ParentControlID", ""); }
            set { SetPropertyValue("ParentControlID", value); }
        }

        /// <summary>
        /// ID of the textbox control to set focus to when showing a panel.
        /// </summary>
        [IDReferenceProperty(typeof(TextBox))]
        [DefaultValue("")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", Justification = "Following ASP.NET AJAX pattern")]
        [ExtenderControlProperty]
        public string FocusControlID
        {
            get { return GetPropertyValue("FocusControlID", ""); }
            set { SetPropertyValue("FocusControlID", value); }
        }
        /// <summary>
        /// Handler to attach to the client-side itemSelected event
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlEvent]
        [ClientPropertyName("valueChanged")]
        public string OnValueChanged
        {
            get { return GetPropertyValue("OnValueChanged", string.Empty); }
            set { SetPropertyValue("OnValueChanged", value); }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SetControlVisibility();
        }

        private void SetControlVisibility()
        {
            bool collapse = true;
            if (this.Enabled)
            {
                string valueList = this.ValuesToCheck;
                List<string> valueArray = new List<string>();
                if (valueList != null && valueList.Trim().Length > 0)
                {
                    valueArray = new List<string>(valueList.Split(",".ToCharArray()));
                }
                bool valueSelected = false;
                Control parentControl = this.FindControl(this.ParentControlID);
                CheckBox cb = parentControl as CheckBox;
                if (cb != null)
                {
                    valueSelected = cb.Checked;
                }
                else if (valueArray.Count > 0)
                {
                    if (ParentControlType == ParentControlTypes.DropdownList)
                    {
                        DropDownList ddl = (DropDownList)parentControl;
                        valueSelected = valueArray.Contains(ddl.SelectedValue);
                    }
                    else if (ParentControlType == ParentControlTypes.RadiobuttonList)
                    {
                        RadioButtonList rbl = (RadioButtonList)parentControl;
                        valueSelected = valueArray.Contains(rbl.SelectedValue);
                    }
                    else if (ParentControlType == ParentControlTypes.CheckBoxList)
                    {
                        CheckBoxList checkBoxList = parentControl as CheckBoxList;
                        if (checkBoxList != null)
                        {
                            foreach (ListItem listItem in checkBoxList.Items)
                            {
                                if (listItem.Selected && valueArray.Contains(listItem.Value))
                                {
                                    valueSelected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                collapse = (valueSelected && ActionOnValueSelected == VisibilityMode.Hide) || (!valueSelected && ActionOnValueSelected == VisibilityMode.Show);
            }
            else
            {
                collapse = true;
            }

            using (WebControl webControl = this.TargetControl as WebControl)
            {
                if (webControl != null)
                {
                    if (collapse)
                    {
                        webControl.Style.Add("display", "none");
                        webControl.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    else
                    {
                        webControl.Style.Remove("display");
                        webControl.Style.Remove(HtmlTextWriterStyle.Display);
                    }
                    return;
                }
            }


            using (HtmlControl htmlControl = this.TargetControl as HtmlControl)
            {
                if (htmlControl != null)
                {
                    if (collapse)
                    {
                        htmlControl.Style.Add("display", "none");
                        htmlControl.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    else
                    {
                        htmlControl.Style.Remove("display");
                        htmlControl.Style.Remove(HtmlTextWriterStyle.Display);
                    }
                }
            }
        }

    }
}
