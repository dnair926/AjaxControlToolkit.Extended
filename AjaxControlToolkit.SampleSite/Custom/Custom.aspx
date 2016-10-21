<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.master" AutoEventWireup="true" CodeBehind="Custom.aspx.cs" Inherits="AjaxControlToolkit.SampleSite.Custom.Custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DemoHeading" runat="server">
    Custom Extender Demonstration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DemoContent" runat="server">
    <script type="text/javascript">
        function appendName() {
            var firstNameElement = $get('Content_DemoContent_tbFirstName'),
                lastNameElement = $get('Content_DemoContent_tbLastName'),
                nameConcatenated = ((firstNameElement ? firstNameElement.value : '') + ' ' + (lastNameElement ? lastNameElement.value : '')).trim(),
                nameDisplayElement = $get('Content_DemoContent_lblConcatenatedName');
            if (nameDisplayElement) {
                nameDisplayElement.innerHTML = nameConcatenated;
            }
        }
    </script>
    Type in the fields and click outside to see the Custom Extender Demonstration:<br />
    First Name: <asp:TextBox ID="tbFirstName" runat="server" /><br />
    Last Name: <asp:TextBox ID="tbLastName" runat="server" /><br />
    Hello, <asp:Label ID="lblConcatenatedName" runat="server" /><br />
    <asp:Button ID="btnPostback" runat="server" Text="Cause Postback" />
    <ate:CustomExtender ID="ceDemo1" runat="server" TargetControlID="tbFirstName" OnPageInit="appendName" OnUserAction="appendName" />
    <ate:CustomExtender ID="ceDemo2" runat="server" TargetControlID="tbLastName" OnPageInit="appendName" OnUserAction="appendName" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InfoContent" runat="server">
</asp:Content>
