<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Administration.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="display-3 text-center" meta:resourcekey="ttlRegisteredUsers">Registered users </div>


    <div class="container p-3">
        <div class="row">
            <div class="col-4">
                <div class="form-group">

                    <fieldset class="p-4">
                        <legend>List of users</legend>
                        <asp:ListBox runat="server"
                            Height="500"
                            ID="lbUsers"
                            CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="lbUsers_SelectedIndexChanged" />
                    </fieldset>


                </div>
            </div>


            <div class="col">
                <fieldset class="p-4">
                    <legend>Edit User</legend>
                    <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100"></asp:Label>
                    <div class="row m-1">
                        <asp:Label ID="lblUsername" for="txtUsername" class="col form-label" runat="server" Text="Username"></asp:Label>
                        <asp:TextBox ID="txtUsername" class="col-8 form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-1">
                        <asp:Label ID="lblEmail" for="txtEmail" class="col form-label" runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtEmail" class="col-8 form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-1">
                        <asp:Label ID="lblPhoneNumber" meta:resourcekey="lblPhoneNumber" class="col form-label" runat="server" Text="Phone number"></asp:Label>
                        <asp:TextBox ID="txtPhoneNumber" class="col-8 form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-1">
                        <asp:Label ID="lblAddress" meta:resourcekey="lblAddress" class="col form-label" runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txtAddress" class="col-8 form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-2">
                        <asp:Button ID="updateUser" class="btn btn-primary w-100" runat="server" Text="Update" OnClick="updateUser_Click" />

                    </div>
                </fieldset>
            </div>
        </div>

    </div>
</asp:Content>

