<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="Leave_Management.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Academic Calendar</title>

    <style>

        body {
            font-family: Arial;
            background-color: #f2f2f2;
            margin: 0;
        }

        .container {
            width: 850px;
            margin: 40px auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px #cccccc;
        }

        h1 {
            text-align: center;
            color: #1f4e79;
        }

        h2 {
            text-align: center;
        }

        .welcome {
            display: block;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            margin: 20px;
        }

        .message {
            display: block;
            text-align: center;
            margin: 15px;
            font-weight: bold;
        }

        .button {
            background-color: #1f4e79;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .button:hover {
            background-color: #163a5a;
        }

        .center {
            text-align: center;
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="container">

        <h1>
            Academic Calendar & Leave Management System
        </h1>

        <asp:Label
            ID="lblWelcome"
            runat="server"
            CssClass="welcome">
        </asp:Label>

        <h2>Academic Calendar</h2>

        <div class="center">

            <asp:Calendar
                ID="calAcademic"
                runat="server"
                Width="700px"
                Height="350px"
                OnSelectionChanged="calAcademic_SelectionChanged">

                <TitleStyle
                    BackColor="#1f4e79"
                    ForeColor="White"
                    Font-Bold="True" />

                <DayHeaderStyle
                    BackColor="#d9eaf7"
                    Font-Bold="True" />

                <TodayDayStyle
                    BackColor="#90caf9"
                    Font-Bold="True" />

                <SelectedDayStyle
                    BackColor="#ff9800"
                    ForeColor="White"
                    Font-Bold="True" />

            </asp:Calendar>

        </div>

        <asp:Label
            ID="lblSelectedDate"
            runat="server"
            CssClass="message">
        </asp:Label>

        <div class="center">

            <asp:Button
                ID="btnLeave"
                runat="server"
                Text="Leave Application"
                CssClass="button"
                OnClick="btnLeave_Click" />

        </div>

    </div>

</form>

</body>
</html>
