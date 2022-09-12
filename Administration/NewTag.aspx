<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewTag.aspx.cs" Inherits="Administration.NewTag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="display-4 text-center">Add a new tag </div>
    <div class="container my-2">
        <div class="form-group">
            <label>Tag type</label>
            <asp:DropDownList
                ID="ddlTagType"
                runat="server"
                CssClass="form-control"
                DataValueField="Id"
                DataTextField="NameEng">
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label>Tag name (in Croatian)</label>
            <asp:TextBox runat="server" ID="tbTagName" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTagName"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label>Tag name (in English)</label>
            <asp:TextBox runat="server" ID="tbTagNameEng" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTagNameEng"></asp:RequiredFieldValidator>
        </div>
        <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-secondary" Text="Cancel"/>
        <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="Add"/>

    </div>
</asp:Content>
