<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApartmentInfoControl.ascx.cs" Inherits="Administration.User_Controls.ApartmentInfoControl" %>
<asp:ScriptManager ID="asm" runat="server"></asp:ScriptManager>

<div class="container">
    <div style="float: right;">
    </div>
    <div class="row my-4">
        <!-- Name -->
        <asp:Label ID="lNameEng" runat="server" CssClass="h1"></asp:Label>
        <!-- Status -->
        <asp:Label ID="lStatus" runat="server" CssClass="text-light badge bg-secondary m-2" Font-Size="X-Large"></asp:Label>
    </div>

    <div class="row my-4">
        <div class="col">
            <table class="table table-hover my-5">
                <tr>
                    <td>Created at:</td>
                    <td>
                        <!-- CreatedAt -->
                        <asp:Label ID="lCreatedAt" runat="server" CssClass="display-10 text-center" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>Owner: </td>
                    <td>
                        <!-- Owner -->
                        <asp:Label ID="lOwner" runat="server" CssClass="display-10 text-center"></asp:Label></td>
                </tr>
                <tr>
                    <td>Location: </td>
                    <td>
                        <!-- Address & City -->
                        <asp:Label ID="lAddress" runat="server" /></td>
                </tr>
                <asp:PlaceHolder runat="server" ID="trBeachDistance" OnLoad="trBeachDistance_Load">
                    <tr>
                        <td>Beach distance: </td>
                        <td>
                            <!-- BeachDistance -->
                            <div>
                                <i class="bi bi-tsunami"></i>
                                <asp:Label ID="lBeachDistance" runat="server"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="col">
            <!-- Price -->
            <div class="row card text-center mx-5 my-2">
                <div class="card-body">
                    <h5 class="card-title">Price</h5>
                    <asp:Label ID="lPrice" runat="server" CssClass="display-4 text-center" Text=""></asp:Label>
                    <br />
                    <asp:Label runat="server" CssClass="display-8 text-center" Text="EUR / night"></asp:Label>
                </div>
            </div>

            <div class="card text-center p-2 mx-5 my-2">
                <!-- MaxChildren -->
                <div class="row">
                    <!-- MaxAdults -->
                    <div class="col">
                        <asp:Label ID="lMaxAdults" runat="server" CssClass="display-4 text-center" Text=""></asp:Label><br />
                        <asp:Label runat="server" CssClass="display-5 text-center" Text="Max. adults"></asp:Label>

                    </div>
                    <!-- MaxChildren -->
                    <div class="col">
                        <asp:Label ID="lMaxChildten" runat="server" CssClass="display-4 text-center" Text=""></asp:Label><br />
                        <asp:Label runat="server" CssClass="display-5 text-center" Text="Max. children"></asp:Label>

                    </div>
                    <!-- TotalRooms -->
                    <div class="col">
                        <asp:Label ID="lTotalRooms" runat="server" CssClass="display-4 text-center" Text=""></asp:Label><br />
                        <asp:Label runat="server" CssClass="display-5 text-center" Text="Total rooms"></asp:Label>

                    </div>
                </div>
            </div>
        </div>
    </div>




    <div class="row my-4">
        <!--    Tags    -->
        <asp:Label runat="server" CssClass="h3" Text="Tags"></asp:Label>

        <asp:Label runat="server" ID="lInfoTags" CssClass="w-100"></asp:Label>
        <asp:Repeater runat="server" ID="rTag">
            <ItemTemplate>
                <asp:Label runat="server" class="badge badge-primary m-1 p-2 text-light" Text='<%#Eval("NameEng") %>' Font-Size="Medium" ></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="row my-4">
        <!--    Pictures    -->
        <asp:Label runat="server" CssClass="h3" Text="Pictures"></asp:Label>

        <asp:Label runat="server" ID="lInfoPictures" CssClass="w-100"></asp:Label>
        <div class="row">
            <asp:Repeater runat="server" ID="rPictures" OnItemDataBound="rPictures_ItemDataBound">
                <ItemTemplate>
                    <div class="card p-0">
                        <asp:Image runat="server" ImageUrl='<%#Eval("Path") %>' CssClass="m-0" Height="160" />
                        <asp:Label runat="server" Text='<%#Eval("Name") %>' CssClass="m-1"></asp:Label>
                        <asp:Label runat="server" Text="Main image" ID="lPictureName" CssClass="m-2 card-subtitle"></asp:Label>
                    </div>
                    
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <div class="row"></div>
    <!--    Actions    -->
    <div class="row my-4">
        <hr />
        <asp:Button runat="server" ID="btnBackToHomepage" OnClick="btnBackToHomepage_Click" CssClass="btn btn-secondary m-1" Text="Back to homepage" AutoPostBack="true" PostBackUrl="~\Default.aspx" />

        <asp:LinkButton runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary m-1" Text="Edit apartment information" AutoPostBack="false" />

        <ajaxToolkit:ConfirmButtonExtender ID="cfe" runat="server" TargetControlID="btnDeleteApartment" OnClientCancel="CancelDelete" ConfirmOnFormSubmit="true" ConfirmText="Are you sure you want to delete this apartment?" />
        <asp:LinkButton runat="server" ID="btnDeleteApartment" OnClick="btnDeleteApartment_Click" CssClass="btn btn-danger m-1" Text="Delete apartment" />
    </div>







</div>




