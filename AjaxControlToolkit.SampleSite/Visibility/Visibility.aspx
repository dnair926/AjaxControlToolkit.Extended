<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.master" AutoEventWireup="true" CodeBehind="Visibility.aspx.cs" Inherits="AjaxControlToolkit.SampleSite.Visibility.Visibility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DemoHeading" runat="server">
    Visibility Demonstration
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="DemoContent" runat="server">
    <asp:RadioButtonList ID="rblVisibilityDemo" runat="server">
        <asp:ListItem Value="label" Text="Show Label"></asp:ListItem>
        <asp:ListItem Text="Show Form" Value="panel" />
    </asp:RadioButtonList>
    <ate:VisibilityExtender ID="veMain" runat="server" TargetControlID="lblInfo" ActionOnValueSelected="Show" TargetControlType="Label" 
        ParentControlID="rblVisibilityDemo" ParentControlType="RadiobuttonList" ValuesToCheck="label" />
    <ate:VisibilityExtender ID="veForm" runat="server" TargetControlID="pnlInfo" ActionOnValueSelected="Show" TargetControlType="Panel" 
        ParentControlID="rblVisibilityDemo" ParentControlType="RadiobuttonList" ValuesToCheck="panel" FocusControlID="tbFName" />
    <asp:Panel ID="pnlInfo" runat="server">
        <h3>Panel with information</h3>
        <table>
            <tr>
                <td>First Name:</td>
                <td><asp:TextBox ID="tbFName" runat="server" /></td>
            </tr>
            <tr>
                <td>Last Name:</td>
                <td><asp:TextBox ID="tbLName" runat="server" /></td>
            </tr>
            <tr>
                <td>Are you at least 13 years or older?</td>
                <td>
                    <asp:DropDownList ID="rblAge" runat="server">
                        <asp:ListItem Text="" Value="" />
                        <asp:ListItem Text="Yes" Value="1" />
                        <asp:ListItem Text="No" Value="0" />
                    </asp:DropDownList>  
                    <ate:VisibilityExtender id="veAge" runat="server" TargetControlID="trInfo" TargetControlType="TableRow" ParentControlID="rblAge" ParentControlType="DropdownList" ActionOnValueSelected="Show" ValuesToCheck="0" />
                    <ate:VisibilityExtender id="veSubmit" runat="server" TargetControlID="trSubmit" TargetControlType="TableRow" ParentControlID="rblAge" ParentControlType="DropdownList" ActionOnValueSelected="Show" ValuesToCheck="1" />
                </td>
            </tr>
            <asp:TableRow ID="trInfo" runat="server">
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><p>You must be at least 13 years old to submit this form.</p></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="trSubmit" runat="server">
                <asp:TableCell></asp:TableCell>
                <asp:TableCell><asp:Button ID="btnSubmit" Text="Submit" runat="server" /></asp:TableCell>
            </asp:TableRow>
        </table>
    </asp:Panel>
    <asp:Label ID="lblInfo" runat="server">
        <p>Lorem ipsum dolor sit amet, quo hinc iusto id. Ei lorem animal convenire vis. Eius errem mei ea, possit intellegebat sea id. No vim hinc mandamus deterruisset, laudem torquatos voluptaria et his. Ut quo solum delenit tincidunt, ei quo veniam utinam iisque. Decore eleifend ea mel. Ad per veri postea deserunt.</p>
        <p>Mei ut libris blandit, ne unum invenire his. Ex qui dicam oratio tempor, te ius labitur partiendo. Quis electram adipiscing no qui, eos congue moderatius efficiantur ei, pri impetus virtute aliquando an. In sea detracto maiestatis quaerendum, cu esse legere est. Prompta pertinax efficiantur ad per.</p>
    </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InfoContent" runat="server">
</asp:Content>
