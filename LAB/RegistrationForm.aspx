<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="RegistrationForm.aspx.cs"
    Inherits="Lab_4.RegistrationForm" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Event Registration Portal</title>

    <style>
        body {
            margin: 0;
            background: #eef3f8;
            font-family: Arial, sans-serif;
        }

        .container {
            width: 650px;
            margin: 35px auto;
            background: white;
            border-radius: 8px;
            box-shadow: 0 4px 15px #bbb;
            overflow: hidden;
        }

        .header {
            background: #1976b9;
            color: white;
            text-align: center;
            padding: 20px;
        }

        .header h1 {
            margin: 0;
            font-size: 27px;
        }

        .header p {
            margin: 7px 0 0;
            font-size: 16px;
        }

        .form {
            padding: 25px 38px;
        }

        .field {
            margin-bottom: 16px;
        }

        label {
            display: block;
            font-weight: bold;
            margin-bottom: 6px;
        }

        .input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 14px;
        }

        .radio {
            margin-right: 15px;
        }

        .error {
            color: #d32f2f;
            font-size: 12px;
        }

        .button {
            background: #1976b9;
            color: white;
            border: none;
            padding: 11px 22px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 15px;
        }

        .button:hover {
            background: #125a8c;
        }

        .success {
            display: block;
            margin-top: 15px;
            padding: 12px;
            background: #e8f5e9;
            color: #2e7d32;
            border-radius: 4px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="container">

            <div class="header">
                <h1>MARWADI UNIVERSITY</h1>
                <p>Online Event Registration Portal</p>
            </div>

            <div class="form">

                <!-- Participant Name -->
                <div class="field">
                    <label>Participant Name</label>

                    <asp:TextBox ID="txtName" runat="server"
                        CssClass="input"
                        placeholder="Enter your full name">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="rfvName"
                        runat="server"
                        ControlToValidate="txtName"
                        ErrorMessage="Name is required."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <!-- Email -->
                <div class="field">
                    <label>Email Address</label>

                    <asp:TextBox ID="txtEmail" runat="server"
                        CssClass="input"
                        TextMode="Email"
                        placeholder="example@email.com">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="rfvEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Email is required."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator
                        ID="revEmail"
                        runat="server"
                        ControlToValidate="txtEmail"
                        ErrorMessage="Enter a valid email address."
                        ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>

                <!-- Mobile -->
                <div class="field">
                    <label>Mobile Number</label>

                    <asp:TextBox ID="txtMobile" runat="server"
                        CssClass="input"
                        MaxLength="10"
                        placeholder="Enter 10-digit mobile number">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="rfvMobile"
                        runat="server"
                        ControlToValidate="txtMobile"
                        ErrorMessage="Mobile number is required."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator
                        ID="revMobile"
                        runat="server"
                        ControlToValidate="txtMobile"
                        ErrorMessage="Enter a valid 10-digit mobile number."
                        ValidationExpression="^[0-9]{10}$"
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RegularExpressionValidator>
                </div>

                <!-- Event -->
                <div class="field">
                    <label>Select Event</label>

                    <asp:DropDownList ID="ddlEvent" runat="server"
                        CssClass="input">

                        <asp:ListItem Text="-- Select Event --" Value="">
                        </asp:ListItem>

                        <asp:ListItem Text="Tech Fest 2026"
                            Value="Tech Fest 2026">
                        </asp:ListItem>

                        <asp:ListItem Text="Code Hackathon"
                            Value="Code Hackathon">
                        </asp:ListItem>

                        <asp:ListItem Text="AI Workshop"
                            Value="AI Workshop">
                        </asp:ListItem>

                    </asp:DropDownList>

                    <asp:RequiredFieldValidator
                        ID="rfvEvent"
                        runat="server"
                        ControlToValidate="ddlEvent"
                        InitialValue=""
                        ErrorMessage="Please select an event."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <!-- Gender -->
                <div class="field">
                    <label>Gender</label>

                    <asp:RadioButtonList ID="rblGender" runat="server"
                        RepeatDirection="Horizontal">

                        <asp:ListItem Text="Male" Value="Male">
                        </asp:ListItem>

                        <asp:ListItem Text="Female" Value="Female">
                        </asp:ListItem>

                        <asp:ListItem Text="Other" Value="Other">
                        </asp:ListItem>

                    </asp:RadioButtonList>

                    <asp:RequiredFieldValidator
                        ID="rfvGender"
                        runat="server"
                        ControlToValidate="rblGender"
                        ErrorMessage="Please select gender."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>

                <!-- Age -->
                <div class="field">
                    <label>Age</label>

                    <asp:TextBox ID="txtAge" runat="server"
                        CssClass="input"
                        TextMode="Number"
                        placeholder="Enter age">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="rfvAge"
                        runat="server"
                        ControlToValidate="txtAge"
                        ErrorMessage="Age is required."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RequiredFieldValidator>

                    <asp:RangeValidator
                        ID="rvAge"
                        runat="server"
                        ControlToValidate="txtAge"
                        MinimumValue="10"
                        MaximumValue="100"
                        Type="Integer"
                        ErrorMessage="Age must be between 10 and 100."
                        CssClass="error"
                        Display="Dynamic">
                    </asp:RangeValidator>
                </div>

                <!-- Terms -->
                <div class="field">

                    <asp:CheckBox ID="chkTerms" runat="server"
                        Text=" I agree to the event terms and conditions." />

                    <br />

                    <asp:CustomValidator
                        ID="cvTerms"
                        runat="server"
                        ErrorMessage="Please accept the terms and conditions."
                        CssClass="error"
                        Display="Dynamic"
                        OnServerValidate="cvTerms_ServerValidate">
                    </asp:CustomValidator>

                </div>

                <!-- Register Button -->
                <asp:Button ID="btnRegister" runat="server"
                    Text="Register for Event"
                    CssClass="button"
                    OnClick="btnRegister_Click" />

                <!-- Result -->
                <asp:Label ID="lblMessage" runat="server"
                    CssClass="success"
                    Visible="false">
                </asp:Label>

            </div>
        </div>

    </form>
</body>
</html>