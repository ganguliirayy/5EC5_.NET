<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="P9_PlacementDashboard.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Placement Dashboard</title>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f8fb;
            color: #2c3e50;
            line-height: 1.6;
            padding: 24px 16px;
        }

        .container {
            max-width: 1050px;
            margin: 0 auto;
            background: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 18px rgba(0, 0, 0, 0.07);
            border: 1px solid #dbe7f1;
            overflow: hidden;
        }

        .header {
            background-color: #e3f2fd;
            border-bottom: 2px solid #bce0fd;
            padding: 26px 32px;
            text-align: center;
        }

        .header h1 {
            color: #0d47a1;
            font-size: 28px;
            font-weight: 700;
            letter-spacing: 0.5px;
            margin-bottom: 4px;
        }

        .header p {
            color: #455a64;
            font-size: 14px;
            font-weight: 500;
        }

        .content {
            padding: 28px 32px 36px 32px;
        }

        .section-title {
            color: #1565c0;
            font-size: 18px;
            font-weight: 600;
            margin: 24px 0 14px 0;
            padding-bottom: 8px;
            border-bottom: 2px solid #e1f0fa;
        }

        .section-title:first-of-type {
            margin-top: 0;
        }

        /* Summary Cards */
        .summary-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(170px, 1fr));
            gap: 16px;
            margin-bottom: 24px;
        }

        .card {
            background: #ffffff;
            border: 1px solid #d9e6f2;
            border-top: 4px solid #0288d1;
            border-radius: 8px;
            padding: 16px 14px;
            text-align: center;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.04);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .card:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(2, 136, 209, 0.12);
        }

        .card-label {
            font-size: 12px;
            font-weight: 600;
            text-transform: uppercase;
            color: #607d8b;
            letter-spacing: 0.5px;
            margin-bottom: 8px;
        }

        .card-value {
            font-size: 26px;
            font-weight: 700;
            color: #0d47a1;
        }

        .unit {
            font-size: 14px;
            font-weight: 600;
            color: #546e7a;
            margin-left: 2px;
        }

        /* GridView Table */
        .table-container {
            overflow-x: auto;
            border-radius: 8px;
            border: 1px solid #d9e6f2;
            margin-bottom: 26px;
            background: #ffffff;
        }

        .custom-grid {
            width: 100%;
            border-collapse: collapse;
            font-size: 14px;
            text-align: left;
        }

        .custom-grid th {
            background-color: #e1f0fa;
            color: #0d47a1;
            font-weight: 600;
            padding: 12px 16px;
            border-bottom: 2px solid #bce0fd;
            white-space: nowrap;
        }

        .custom-grid td {
            padding: 11px 16px;
            border-bottom: 1px solid #edf2f7;
            color: #374151;
        }

        .custom-grid tr:last-child td {
            border-bottom: none;
        }

        .custom-grid tr:nth-child(even) {
            background-color: #f9fbfd;
        }

        .custom-grid tr:hover {
            background-color: #eef6fc;
        }

        /* Buttons & Actions */
        .action-row {
            margin-top: 10px;
            text-align: left;
        }

        .btn-link {
            display: inline-block;
            background-color: #0288d1;
            color: #ffffff !important;
            padding: 11px 22px;
            font-size: 14px;
            font-weight: 600;
            text-decoration: none;
            border-radius: 6px;
            box-shadow: 0 2px 6px rgba(2, 136, 209, 0.28);
            transition: background-color 0.2s ease, box-shadow 0.2s ease;
        }

        .btn-link:hover {
            background-color: #0277bd;
            box-shadow: 0 4px 10px rgba(2, 136, 209, 0.38);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <header class="header">
                <h1>Placement Dashboard</h1>
                <p>Campus Placement Training & Recruitment Portal</p>
            </header>

            <main class="content">
                <h3 class="section-title">Placement Summary</h3>

                <div class="summary-grid">
                    <div class="card">
                        <div class="card-label">Total Students</div>
                        <div class="card-value"><asp:Label ID="lblTotal" runat="server" /></div>
                    </div>

                    <div class="card">
                        <div class="card-label">Placed Students</div>
                        <div class="card-value"><asp:Label ID="lblPlaced" runat="server" /></div>
                    </div>

                    <div class="card">
                        <div class="card-label">Unplaced Students</div>
                        <div class="card-value"><asp:Label ID="lblUnplaced" runat="server" /></div>
                    </div>

                    <div class="card">
                        <div class="card-label">Placement Percentage</div>
                        <div class="card-value"><asp:Label ID="lblPercentage" runat="server" /></div>
                    </div>

                    <div class="card">
                        <div class="card-label">Highest Package</div>
                        <div class="card-value"><asp:Label ID="lblHighestPackage" runat="server" /> <span class="unit">LPA</span></div>
                    </div>
                </div>

                <h3 class="section-title">Student Placement Records</h3>

                <div class="table-container">
                    <asp:GridView ID="gvPlacements" runat="server"
                        AutoGenerateColumns="True"
                        EmptyDataText="No student records found."
                        CssClass="custom-grid"
                        GridLines="None">
                    </asp:GridView>
                </div>

                <div class="action-row">
                    <asp:HyperLink ID="lnkCompanies" runat="server"
                        NavigateUrl="~/Companies.aspx"
                        CssClass="btn-link">
                        View Company Summary &rarr;
                    </asp:HyperLink>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
