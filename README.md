# AjaxControlToolkit.Extended
<p>This project includes some custom Ajax Extenders that I created to make my life easier while creating ASP.Net Webforms applications.  and is based on the [DevExpress AjaxControlToolkit](https://github.com/DevExpress/AjaxControlToolkit "DevExpress AjaxControlToolkit") project.</p>
<p>I had submitted pull requests for these extenders when this project was under CodePlex but was declined because it was deemed to be basic functionalities that do not need extenders [view](https://ajaxcontroltoolkit.codeplex.com/SourceControl/network/forks/dnair926/NewExtenders/contribution/1454 "view").  I do agree with the reasoning and think that it was judgment by the cover becuase they do more in addition to the basic functionality and also handles some other issues that comes with having a script in the application.</p>
<p>Having an extender prevents the need to hardcode element ids and eliminates the tedious task of changing ids in script when the extended element's unique id changes.  Additionally, it handles the issue with controls losing attributes set on client-side during a post-back on the page.</p>
<p>My colleagues have appreciated these extenders and is currently used in all applications that is created in our firm.</p>
<ol>
## Extenders:
<li>### Character Count:
<ol>
<li>Shows a tooltip when typing in a text box or text area that displays the number or characters entered and the number of characters allowed.</li>
<li>Tooltip can be styled to show that the allowed number of characters are about to run out and when the entered number of characters exceed the allowed number of characters.</li>
</ol></li>
<li>### Caps Lock:
<ol><li>Shows a warning when typing into a text box or text area with Caps Lock key ON (ideal for password fields)..</li></ol></li>
<li>### Visibility: <ol><li>Show/Hide an element based on the value selected in another element.</li><li>Clears the elements inside the hidden container</li><li>Set focus to an element inside the container element being shown.</li><li>Keeps the element visible/hidden state during postback</li></ol></li>
<li>### Custom: <ol><li>Allows to run custom scripts for elements that needs to run on client-side and also after a post-back to keep any attributes set on client-side.</li></ol></li>
</ol>
