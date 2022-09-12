<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Administration.Tags" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="display-4 text-center">Tags </div>
    <div class="container my-2">
        <asp:Repeater runat="server" ID="repeaterTags" OnItemDataBound="repeaterTags_ItemDataBound">
            <HeaderTemplate>
                <table id="myTable" class="table table-hover w-100">
                    <thead>
                        <th>Id</th>
                        <th>Tag name</th>
                        <th>Usage</th>
                        <th></th>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label runat="server" Text='<%# Eval("Id") %>' ID="Id"></asp:Label></td>
                    </td>
                    <td>
                        <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                    <td>
                        <asp:Label runat="server" Text='<%# Eval("Usage") %>' ID="Usage"></asp:Label></td>
                    <td>
                        <asp:LinkButton runat="server" ID="btnDeleteTag" OnClick="btnDeleteTag_Click" Text="Delete" CssClass="btn btn-outline-danger" ></asp:LinkButton>
                       
                        
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="row my-4">
            <asp:LinkButton ID="btnAddTag" runat="server" Text="Add a new tag" CssClass="col btn btn-primary m-1" OnClick="btnAddTag_Click1"></asp:LinkButton>
            
        </div>
    </div>

</asp:Content>

