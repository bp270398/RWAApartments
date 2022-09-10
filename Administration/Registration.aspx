<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Administration.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container py-4">
        <!-- FORM -->
        <asp:Panel ID="PanelForma" runat="server" Visible="True">
            <fieldset class="p-4">
                <legend runat="server" >Registration</legend>
                <!-- Username -->
                <div class="mb-3">
                    <asp:Label ID="lblUsername" for="txtUsername" class="form-label"  runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server"  ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                <!-- Email -->
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblRmail" for="txtEmail" class="form-label"  runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="txtEmail" class="form-control" runat="server" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
                <!-- Phone number -->
                <div class="mb-3">
                    <asp:Label ID="lblPhoneNumber" for="ddlPhonenumber" class="form-label"  runat="server" Text="Phone number"></asp:Label>
                    <asp:TextBox runat="server" ID="txtPhoneNumber" class="form-control" TextMode="Phone"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhoneNumber" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                    <asp:CustomValidator
                        ID="CustomValidatorBannedUsername"
                        ClientValidationFunction="Username_Validation"
                        runat="server"
                        ControlToValidate="txtUsername"
                        Display="Dynamic"
                        ForeColor="Red"
                        
                        OnServerValidate="Username_ServerValidate"
                        ErrorMessage="This username is banned"></asp:CustomValidator>
                    <asp:CustomValidator
                        ID="CustomValidatorExistingUsername"
                        runat="server"
                        ControlToValidate="txtUsername"
                        Display="Dynamic"
                        ForeColor="Red"
                        ClientValidationFunction="CheckUser_Validation"
                        OnServerValidate="CheckUser_ServerValidate" 
                        ErrorMessage="This username is already taken"></asp:CustomValidator>


                </div>
                <!-- Address -->
                <div class="mb-3">
                    <asp:Label ID="Label2" for="txtAddress" class="form-label" runat="server" Text="Address"></asp:Label>
                    <asp:TextBox ID="txtAddress" class="form-control" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
                <!-- Password -->
                <div class="mb-3">
                    <asp:Label ID="lblPassword" for="txtPassword" class="form-label" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  runat="server" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblConfirmPassword" for="txtConfirmPassword" class="form-label"  runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" ErrorMessage="This is a required field"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorPassword" runat="server"
                        ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                        
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="The passwords don't match"></asp:CompareValidator>
                </div>

                <asp:Button ID="btnSend" runat="server" class="btn btn-warning"  Text="Submit" OnClick="btnSend_Click" />

            </fieldset>
        </asp:Panel>
        <!-- // -->

        <!-- PANEL PORUKA -->
        <asp:Panel ID="PanelIspis" CssClass="container mt-5" runat="server" Visible="False">
            <div class='alert alert-success' role='alert'>
                <asp:Label ID="lblSuccessLogin"  runat="server" Text="Registration successful."></asp:Label>
            </div>
        </asp:Panel>
        <!-- // -->

        <!-- USERNAME VALIDATION -->
        <script type="text/javascript">
            function Username_Validation(sender, args) {
                if (args.Value.toLowerCase() == "admin") {
                    args.IsValid = false;
                } else {
                    args.IsValid = true;
                }
            }
        </script>

        <!-- USER CHECK VALIDATION -->
        <script type="text/javascript">
            function CheckUser_Validation(sender, args) {
                const listOfUsers = JSON.parse('<%= Newtonsoft.Json.JsonConvert.SerializeObject(((ClassLibrary.DAL.IRepo)Application["database"]).SelectUsers()) %>');
                console.log(listOfUsers);

                if (listOfUsers.some(u => u.Username == args.Value)) {
                    args.IsValid = false;
                } else {
                    args.IsValid = true;
                }
            }
        </script>

    </div>

</asp:Content>

