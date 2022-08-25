<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="Administration.Tags" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <div class="display-4 text-center">Tags </div>
    <div class="container my-2">
        <asp:Repeater runat="server" ID="repeaterTags" OnItemDataBound="repeaterTags_ItemDataBound" OnItemCommand="repeaterTags_ItemCommand">
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
                        <asp:Button ID="btnDelete" runat="server" CommandName="DeleteTag" CommandArgument='<%# Eval("Id") %>' Text="Delete" UseSubmitBehavior="true" CssClass="btn btn-link" />
                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDelete" ConfirmText="Are you sure you want to delete this tag?" ConfirmOnFormSubmit="true" />
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
            <asp:LinkButton ID="btnOpenModalNewTagType" runat="server" Text="Add a new tag type" CssClass="col btn btn-secondary m-1" OnClick="btnOpenModalNewTagType_Click"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="mpe1" runat="server" TargetControlID="btnOpenModalNewTagType"
                PopupControlID="mpAddNewTagType" CancelControlID="btnClose1" />

            <asp:LinkButton ID="btnOpenModalNewTag" runat="server" Text="Add a new tag" CssClass="col btn btn-primary m-1" OnClick="btnOpenModalNewTag_Click"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="mpe2" runat="server" TargetControlID="btnOpenModalNewTag"
                PopupControlID="mpAddNewTag" CancelControlID="btnClose2" />
        </div>
    </div>

    <!-- Modal panel New Tag Type-->
    <div>
        <asp:Panel ID="mpAddNewTagType" runat="server" Width="500px" BackColor="White">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create a new tag type</h5>
                    </div>
                    <div class="modal-body">
                        <div class="m-4">
                            <asp:Label runat="server">Name (English)</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="tbTagTypeNameEng"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTagTypeNameEng" runat="server" ErrorMessage="This is a required field" ControlToValidate="tbTagTypeNameEng" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="m-4">
                            <asp:Label runat="server">Name (Croatian)</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="tbTagTypeNameCro"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTagTypeNameCro" runat="server" ErrorMessage="This is a required field" ControlToValidate="tbTagTypeNameCro" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnClose1" runat="server" Text="Close" CssClass="btn btn-secondary" />
                        <asp:Button ID="btnAddTagType" runat="server" OnClick="btnAddTagType_Click" CssClass="btn btn-primary" Text="Add tag type" />
                    </div>
                </div>
            </div>
        </asp:Panel>

    </div>
    <!-- Modal panel New Tag -->
    <div>
        <asp:Panel ID="mpAddNewTag" runat="server" Width="500px" BackColor="White">
            <form>
            </form>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Create a new tag</h5>
                    </div>
                    <div class="modal-body">
                        <div class="m-4">
                            <asp:Label runat="server">Tag name (English)</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="tbTagNameEng"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This is a required field" ControlToValidate="tbTagNameEng" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                        </div>
                        <div class="m-4">
                            <asp:Label runat="server">Tag name (Croatian)</asp:Label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="tbTagNameCro"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="This is a required field" ControlToValidate="tbTagNameCro" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <div class="m-4">
                            <asp:Label runat="server">Tag type</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlTagTypes" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnClose2" runat="server" Text="Close" CssClass="btn btn-secondary" />
                        <asp:Button ID="btnAddTag" runat="server" Text="Add tag" CssClass="btn btn-primary" OnClick="btnAddTag_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>


    </div>

    <script type="text/javascript">
        var launch = false;
        function launchModal() {
            launch = true;
        }
        function pageLoad() {
            if (launch) {
                $find("mpe1").show();
            }
            if (launch) {
                $find("mpe2").show();
            }
        }

    </script>

</asp:Content>

