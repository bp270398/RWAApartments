<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewApartment.aspx.cs" Inherits="Administration.NewApartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="display-4 text-center">New apartment </div>

    <div class="container jumbotron">

        <!--   General information     -->
        <div class="container m-1">
            <h3>General information</h3>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lOwner" runat="server" CssClass="display-5 text-center" Text="Owner"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbOwner" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbOwner" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label runat="server" Text="Apartment name (in English)"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbNameEng" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbNameEng" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lNameCro" runat="server" CssClass="display-5" Text="Apartment name (in Croatian)"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbNameCro" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbNameCro" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <div class="col">
                    <asp:Label ID="lStatus" runat="server" CssClass="display-5" Text="Apartment status"></asp:Label>
                </div>
                <div class="col">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col">
                </div>
                <div class="col">
                </div>
            </div>
        </div>

        <!--  Location     -->
        <div class="container m-1">
            <h3>Location</h3>

            <div class="row">
                <div class="col">
                    <asp:Label ID="lCity" runat="server" CssClass="display-5" Text="City" />
                </div>
                <div class="col">
                    <asp:TextBox ID="tbCity" runat="server" CssClass="form-control" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbNameCro" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lAddress" runat="server" CssClass="display-5" Text="Address" />
                </div>
                <div class="col">
                    <asp:TextBox  TextMode="Search" ID="tbAddress" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAddress" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lBeachDistance" runat="server" CssClass="display-5" Text="Beach distance (in meters)"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbBeachDistance" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    
                </div>
            </div>
        </div>

        <!--   Property information     -->
        <div class="container m-1">
            <h3>Property information</h3>

            <div class="row">
                <div class="col">
                    <asp:Label ID="lMaxAdults" runat="server" CssClass="display-5" Text="Max. adult occupancy"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbMaxAdults" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMaxAdults" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lMaxChildten" runat="server" CssClass="display-5" Text="Max. children occupancy"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbMaxChildren" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbMaxChildren" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lTotalRooms" runat="server" CssClass="display-5" Text="Total number of rooms"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbTotalRooms" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbTotalRooms" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>



        <!--   Price     -->
        <div class="container m-1 ">
            <h3>Price</h3>
            <div class="row">
                <div class="col">
                    <asp:Label ID="lPrice" runat="server" CssClass="display-5" Text="Price (per night)"></asp:Label>
                </div>
                <div class="col">
                    <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrice" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!--   Continue     -->
        <div class="container text-right" style="margin:1em 0 0 0;">
            <asp:Button ID="btnSubmit" runat="server" Text="Continue" CssClass="btn btn-lg btn-primary" OnClick="btnSubmit_Click"/>
        </div>
    </div>


</asp:Content>
