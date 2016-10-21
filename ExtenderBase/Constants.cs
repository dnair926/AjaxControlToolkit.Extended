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
            
            CharacterCountScriptName = baseNamespace + ".Scripts.CharacterCountBehavior",

            CharacterCountStyleName = baseNamespace + ".Styles.CharacterCountStyle",

            CapsLockScriptName = baseNamespace + ".Scripts.CapsLockBehavior",

            CapsLockStyleName = baseNamespace + ".Styles.CapsLockStyle";
    }
}
