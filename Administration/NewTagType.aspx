<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewTagType.aspx.cs" Inherits="Administration.NewTagType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

     <div class="display-4 text-center">Add a new tag type</div>
    <div class="container my-2">

        <div class="form-group">
            <label>Tag type name (in Croatian)</label>
            <asp:TextBox runat="server" ID="tbTagTypeName" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTagTypeName"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label>Tag type name (in English)</label>
            <asp:TextBox runat="server" ID="tbTagTypeNameEng" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTagTypeNameEng"></asp:RequiredFieldValidator>
        </div>
        <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary" Text="Cancel"/>
        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="Add"/>

    </div>
</asp:Content>
