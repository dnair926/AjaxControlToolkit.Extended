<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.master" AutoEventWireup="true" CodeBehind="CapsLock.aspx.cs" Inherits="AjaxControlToolkit.SampleSite.CapsLock.CapsLock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DemoHeading" runat="server">
    Caps Lock Demonstration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DemoContent" runat="server">
    Turn on Caps Lock key and start typing to see the Caps Lock message:<br />
    <asp:TextBox ID="tbCapsLock" runat="server" style="width: 500px;" />
    <ate:CapsLockExtender ID="ceDemo" TargetControlID="tbCapsLock" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InfoContent" runat="server">
</asp:Content>
