<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Administration.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

        <div class="display-4 text-center">Apartments </div>


        <!-- Search filtering -->
        <div class="container p-4" style="width: fit-content;">
            
            <div class="row m-1">
                <div class="col">
                    <asp:Label runat="server" Width="64">Status</asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList runat="server" CssClass="form-control" Width="160" ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col"></div>
            </div>
            <div class="row m-1">
                <div class="col">
                    <asp:Label runat="server" Width="64">City</asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList runat="server" CssClass="form-control" Width="160" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col"></div>
            </div>
        </div>

        <!-- Results -->
        <div class="container">
            <div class="table table-striped table-bordered dt-responsive nowrap">

                <asp:Repeater runat="server" ID="repeaterApartments" OnItemCommand="repeaterApartments_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover">
                            <thead>
                                <th>Name</th>
                                <th>City</th>
                                <th>Adults</th>
                                <th>Children</th>
                                <th>Rooms</th>
                                <th>Pictures</th>
                                <th>Price</th>
                                <th></th>
                                <th></th>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text='<% #Eval("NameEng") %>'></asp:Label></td>
                            <td>
                                <asp:Label runat="server" Text='<% #ShowCityName(Eval("City.Id").ToString()) %>'></asp:Label></td>
                            <td>
                                <asp:Label runat="server" Text='<% #Eval("MaxAdults") %>'></asp:Label></td>
                            <td>
                                <asp:Label runat="server" Text='<% #Eval("MaxChildren") %>'></asp:Label></td>
                            <td>
                                <asp:Label runat="server" Text='<% #Eval("TotalRooms") %>'></asp:Label></td>
                            <td>
                                <asp:Label runat="server" Text='<% #ShowApartmentPictures(Eval("Id").ToString()) %>'></asp:Label>
                            <td>
                                <asp:Label runat="server" Text='<%# Eval("Price") %>' ID="lPrice"></asp:Label></td>
                            <td>
                                <asp:Button runat="server" CssClass="btn btn-link" ID="btnOpen" Text="Open" CommandName="ViewApartment" CommandArgument='<%# Eval("Id") %>' UseSubmitBehavior="false"/>
                            </td>
                            <td>
                                <asp:Button runat="server" CssClass="btn btn-link" ID="btnEdit" Text="Edit" CommandName="EditApartment" CommandArgument='<%# Eval("Id") %>' UseSubmitBehavior="false" />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </tbody>
                </table>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:Label runat="server" ID="lblResult" Text="There is no items matching the filters." Visible="false" CssClass="m-4"></asp:Label>

                
            </div>
            <!-- Add btn -->
                <div class="row">
                    <asp:Button runat="server" Text="Add new apartment" CssClass="btn btn-primary w-100 m-3" ID="btnAddApartment" OnClick="btnAddApartment_Click" />
                </div>
        </div>



</asp:Content>
