<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentEditor.aspx.cs" Inherits="Administration.ApartmentEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="display-4 text-center">Apartment form </div>

    <div class="container">
        <div class="row">
            <div class="col p-4">
                <fieldset>
                    <legend>
                        <h4>General information</h4>
                    </legend>

                    <div class="form-group">
                        <label>Owner</label>
                        <asp:DropDownList
                            ID="ddlApartmentOwner"
                            runat="server"
                            CssClass="form-control"
                            DataValueField="Id"
                            DataTextField="Name">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Status</label>
                        <asp:DropDownList
                            ID="ddlStatus"
                            runat="server"
                            CssClass="form-control"
                            DataValueField="Id"
                            DataTextField="NameEng">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Name (Croatian)</label>
                        <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Name (English)</label>
                        <asp:TextBox ID="tbNameEng" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Address</label>
                        <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>City</label>
                        <asp:DropDownList
                            ID="ddlCity"
                            runat="server"
                            CssClass="form-control"
                            DataValueField="Id"
                            DataTextField="Name">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Price</label>
                        <div class="input-group">
                            <span class="input-group-addon" id="sizing-addon1">€</span>
                            <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Broj odraslih</label>
                        <asp:TextBox ID="tbMaxAdults" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Broj djece</label>
                        <asp:TextBox ID="tbMaxChildren" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Broj soba</label>
                        <asp:TextBox ID="tbTotalRooms" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Udaljenost od plaže</label>
                        <asp:TextBox ID="tbBeachDistance" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                    </div>


                </fieldset>
            </div>
            <div class="col p-4">
                <fieldset>
                    <legend>
                        <h4>Tags</h4>
                    </legend>
                    <div class="form-group">
                        <label>Choose tags</label>
                        <asp:DropDownList
                            ID="ddlTags"
                            runat="server"
                            CssClass="form-control"
                            DataValueField="Id"
                            DataTextField="Name" OnSelectedIndexChanged="ddlTags_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <asp:Repeater ID="repTags" runat="server">
                        <HeaderTemplate>
                            <ul class="list-group">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="list-group-item w-100 d-flex justify-content-between">
                                <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID") %>' />
                                <asp:Label runat="server" ID="txtTagName" Text='<%# Eval("Name") %>' />
                                <asp:LinkButton runat="server" ID="btnDeleteTag" CssClass="btn btn-link" OnClick="btnDeleteTag_Click">
                                 <span class="glyphicon glyphicon-trash"></span>
                                Remove tag
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>


                </fieldset>
            </div>
        </div>
        <div class="row m-2">
            <fieldset class="w-100 p-4">
                <legend>
                    <h4>Pictures</h4>
                </legend>
                <div class="form-group">
                    <label class="btn btn-default">
                        Upload pictures
                 <asp:FileUpload ID="uplImages" runat="server" CssClass="hidden" AllowMultiple="true" OnChange="handleFileSelect(this.files)" />
                    </label>
                    <div id="uplImageInfo"></div>
                    <script>
                        function handleFileSelect(files) {
                            for (var i = 0; i < files.length; i++) {
                                $span = $("<span class='label label-info'></span>").text(files[i].name);
                                $('#uplImageInfo').append($span);
                                $('#uplImageInfo').append("<br />");
                            }
                        }
                    </script>

                </div>
                <div class="d-flex flex-row flex-wrap">
                    <asp:Repeater ID="repApartmentPictures" runat="server">
                        <ItemTemplate>
                            <div class="card m-2" style="width:320px;">
                                <a href="<%# Eval("Path") %>">
                                    <asp:Image ID="imgApartmentPicture" runat="server" CssClass="card-img-top" ImageUrl='<%# Eval("Path") %>' Width="320" Height="200"  />
                                </a>
                                <div class="card-body">
                                    <div class="form-group">
                                        <asp:HiddenField runat="server" ID="hidApartmentPictureId" Value='<%# Eval("Id") %>' />
                                        <asp:TextBox ID="txtApartmentPicture" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>' placeholder="Title" />
                                    </div>

                                    <div class="form-group w-100">
                                        <asp:CheckBox ID="cbIsRepresentative" runat="server" CssClass="is-representative-picture" Checked='<%# Eval("IsRepresentative") %>' />
                                        Main photo
                                    </div>
                                    <div class="form-group">
                                        <asp:CheckBox ID="cbDelete" runat="server" Checked="false" />
                                        Delete
                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                    <script>
                        $(function () {
                            var repPicCheckboxes = $(".is-representative-picture > input[type=checkbox]");
                            repPicCheckboxes.change(function () {
                                currentCheckbox = this;
                                if (currentCheckbox.checked) {
                                    repPicCheckboxes.each(function () {
                                        otherCheckbox = this
                                        if (currentCheckbox != otherCheckbox && otherCheckbox.checked) {
                                            otherCheckbox.checked = false;
                                        }
                                    })
                                }
                            });
                        })
                    </script>
                </div>

            </fieldset>
        </div>
        <div class="row m-2">
            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary w-100 py-2" OnClick="btnSave_Click" />
        </div>
    </div>


</asp:Content>
