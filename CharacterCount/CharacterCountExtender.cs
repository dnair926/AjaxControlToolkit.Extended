using AjaxControlToolkit.Extenders;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Constants.CharacterCountScriptName + Constants.JsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CharacterCountScriptName + Constants.MinifiedJsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CharacterCountStyleName + Constants.CssPostfix, "text/css")]
[assembly: WebResource(Constants.CharacterCountStyleName + Constants.MinCssPostfix, "text/css")]

namespace AjaxControlToolkit.Extenders
{
    [TargetControlType(typeof(TextBox))]
    [ClientScriptResource("WebExtenders.CharacterCountBehavior", Constants.CharacterCountScriptName)]
    [ClientCssResource(Constants.CharacterCountStyleName)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(PopupExtender), 1)]
    [RequiredScript(typeof(ThreadingScripts), 2)]
    public class CharacterCountExtender : ExtenderControlBase
    {

        [DefaultValue(2000)]
        [ExtenderControlProperty]
        [ClientPropertyName("showDelay")]
        public virtual int ShowDelay
        {
            get { return GetPropertyValue("ShowDelay", 2000); }
            set { SetPropertyValue("ShowDelay", value); }
        }
        private CharacterCountPosition _popupPosition;
        [ExtenderControlProperty]
        [ClientPropertyName("popupPosition")]
        [DefaultValue(CharacterCountPosition.BottomLeft)]
        [Description("Indicates where you want the popup displayed, bottom or top of the textbox.")]
        public virtual CharacterCountPosition PopupPosition
        {
            get { return _popupPosition; }
            set { _popupPosition = value; }
        }
        private string dataFormatStringField;
        [DefaultValue("{0} words, {1} characters")]
        [Description("The format to display the current count. Use {0} for the word count, {1} for the character count, {2} for the number of words remaining, {3} for the number of characters remaining, {4} for the maximum number of words allowed, {5} for the maximum number of characters allowed.")]
        [ExtenderControlProperty]
        [ClientPropertyName("dataFormatString")]
        public virtual string DataFormatString
        {
            get
            {

                if (string.IsNullOrEmpty(this.dataFormatStringField))
                    return "{5} total characters allowed. {1} characters entered.";
                else
                    return dataFormatStringField.Replace("'", "\\'");
            }
            set
            {
                this.dataFormatStringField = value;
            }
        }
        private bool treatCarriageReturnsAsOneCharacterField;
        [DefaultValue(false)]
        [ExtenderControlProperty]
        [ClientPropertyName("treatCarriageReturnsAsOneCharacter")]
        public virtual bool TreatCarriageReturnsAsOneCharacter
        {
            get
            {
                return treatCarriageReturnsAsOneCharacterField;
            }
            set
            {
                this.treatCarriageReturnsAsOneCharacterField = value;
            }
        }
        private int maxWordLengthField;
        [DefaultValue(false)]
        [ExtenderControlProperty]
        [ClientPropertyName("maxWordLength")]
        public virtual int MaxWordLength
        {
            get
            {
                return maxWordLengthField;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Value must be greater than or equal to 0.");
                this.maxWordLengthField = value;
            }
        }
        private int _maxCharLength;
        [DefaultValue(false)]
        [ExtenderControlProperty]
        [ClientPropertyName("maxCharLength")]
        public virtual int MaxCharLength
        {
            get
            {
                return _maxCharLength;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Value must be greater than or equal to 0.");
                this._maxCharLength = value;
            }
        }
        private int warningPercentageField;
        [DefaultValue(0)]
        [ExtenderControlProperty]
        [ClientPropertyName("warningPercentage")]
        public virtual int WarningPercentage
        {
            get
            {
                return this.warningPercentageField;
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("Value must be between 0 and 100.");

                this.warningPercentageField = value;
            }
        }
        private string toolTipCssClassField;
        [DefaultValue("ToolTipDefault")]
        [ExtenderControlProperty]
        [ClientPropertyName("toolTipCssClass")]
        public virtual string ToolTipCssClass
        {
            get
            {
                if (String.IsNullOrEmpty(this.toolTipCssClassField))
                    return "ToolTipDefault";
                else
                    return this.toolTipCssClassField;
            }
            set
            {
                this.toolTipCssClassField = value;
            }
        }
        private string toolTipCssClassForMaxField;
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("toolTipCssClassForMax")]
        public virtual string ToolTipCssClassForMax
        {
            get
            {

                return this.toolTipCssClassForMaxField;
            }
            set
            {
                this.toolTipCssClassForMaxField = value;
            }
        }
        private string toolTipCssClassForWarningField;
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("toolTipCssClassForWarning")]
        public virtual string ToolTipCssClassForWarning
        {
            get
            {
                return this.toolTipCssClassForWarningField;
            }
            set
            {
                this.toolTipCssClassForWarningField = value;
            }
        }
    }
}
