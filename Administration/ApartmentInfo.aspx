<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentInfo.aspx.cs" Inherits="Administration.ApartmentInfo" %>
<%@ Register Src="~/User_Controls/ApartmentInfoControl.ascx" TagPrefix="uc" TagName="Apartment"%>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <uc:Apartment ID="apartmentInfoControl" runat="server">
    </uc:Apartment>


</asp:Content>
