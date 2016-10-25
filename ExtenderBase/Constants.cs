namespace AjaxControlToolkit.Extenders
{
    public static class Constants
    {
        internal const string
            JsPostfix = ".js",
            MinifiedJsPostfix = ".min" + JsPostfix,

            CssPostfix = ".css",
            MinCssPostfix = ".min" + CssPostfix,

            baseNamespace = "AjaxControlToolkit.Extenders",

            ScriptsBaseName = baseNamespace + ".Scripts",

            StylesBaseName = baseNamespace + ".Styles",

            ImagesBaseName = baseNamespace + ".Images",
            
            CharacterCountScriptName = ScriptsBaseName + ".CharacterCountBehavior",
            CharacterCountStyleName = StylesBaseName + ".CharacterCountStyle",

            CommonScriptName = ScriptsBaseName + ".Common",

            CapsLockScriptName = ScriptsBaseName + ".CapsLockBehavior",
            CapsLockStyleName = StylesBaseName + ".CapsLockStyle",            
            CapsLockAlertLargeImage = ImagesBaseName + ".CapsLock.alert-large.gif",
            CapsLockAlertSmallImage = ImagesBaseName + ".CapsLock.alert-small.gif",
            CapsLockCloseImage = ImagesBaseName + ".CapsLock.close.gif",

            CustomScriptName = ScriptsBaseName + ".CustomBehavior",

            VisibilityScriptName = ScriptsBaseName + ".VisibilityBehavior",

            PlaceHolder = "";
    }
}
