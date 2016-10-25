using AjaxControlToolkit.Extenders;
using System.Web.UI;

[assembly: WebResource(Constants.CommonScriptName + Constants.JsPostfix, "text/javascript")]
[assembly: WebResource(Constants.CommonScriptName + Constants.MinifiedJsPostfix, "text/javascript")]

namespace AjaxControlToolkit.Extenders
{
    [ClientScriptResource(null, Constants.CommonScriptName)]
    public class CommonScripts
    {
    }
}
