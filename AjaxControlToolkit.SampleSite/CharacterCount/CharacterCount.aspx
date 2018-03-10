<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.master" AutoEventWireup="true" CodeBehind="CharacterCount.aspx.cs" Inherits="AjaxControlToolkit.SampleSite.CharacterCount.CharacterCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DemoHeading" runat="server">
    Character Count Demonstration
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="DemoContent" runat="server">
    Start typing to see the character count demonstration:<br />
    <asp:TextBox ID="tbCharacterCount" runat="server" style="width:500px;" CssClass="form-control" />
    <ate:CharacterCountExtender ID="cceDemo1" runat="server" TargetControlID="tbCharacterCount" MaxCharLength="100" WarningPercentage="75" PopupPosition="TopLeft" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="InfoContent" runat="server">
</asp:Content>
