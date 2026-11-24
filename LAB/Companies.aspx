<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="P9_PlacementDashboard.Companies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Placement Dashboard - Companies</title>
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

        /* Table Styling */
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

        .badge-package {
            background-color: #e8f5e9;
            color: #2e7d32;
            font-weight: 600;
            padding: 3px 10px;
            border-radius: 12px;
            font-size: 13px;
            display: inline-block;
        }

        /* DataList Cards */
        .datalist-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0;
            margin-bottom: 24px;
        }

        .datalist-table td {
            padding: 8px;
            vertical-align: top;
        }

        .company-card {
            background: #ffffff;
            border: 1px solid #d9e6f2;
            border-left: 4px solid #0288d1;
            border-radius: 8px;
            padding: 16px 18px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.04);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .company-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(2, 136, 209, 0.12);
        }

        .company-title {
            font-size: 17px;
            font-weight: 700;
            color: #0d47a1;
            margin-bottom: 6px;
        }

        .company-role {
            font-size: 14px;
            color: #455a64;
            margin-bottom: 6px;
        }

        .company-package {
            font-size: 14px;
            color: #2e7d32;
            font-weight: 600;
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
                <p>Company Recruitment & Placement Summary</p>
            </header>

            <main class="content">
                <h3 class="section-title">Placement Records</h3>

                <div class="table-container">
                    <asp:Repeater ID="rptCompanies" runat="server">
                        <HeaderTemplate>
                            <table class="custom-grid">
                                <thead>
                                    <tr>
                                        <th>Placement ID</th>
                                        <th>Student ID</th>
                                        <th>Company</th>
                                        <th>Job Role</th>
                                        <th>Package (LPA)</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("PlacementId") %></td>
                                <td><%# Eval("StudentId") %></td>
                                <td><strong><%# Eval("CompanyName") %></strong></td>
                                <td><%# Eval("JobRole") %></td>
                                <td><span class="badge-package"><%# Eval("PackageLPA") %> LPA</span></td>
                            </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

                <h3 class="section-title">Company Details Using DataList</h3>

                <asp:DataList ID="dlPlacements" runat="server"
                    RepeatColumns="2"
                    RepeatDirection="Horizontal"
                    CssClass="datalist-table">
                    <ItemStyle Width="50%" />
                    <ItemTemplate>
                        <div class="company-card">
                            <div class="company-title"><%# Eval("CompanyName") %></div>
                            <div class="company-role"><strong>Role:</strong> <%# Eval("JobRole") %></div>
                            <div class="company-package"><strong>Package:</strong> <%# Eval("PackageLPA") %> LPA</div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>

                <div class="action-row">
                    <a href="Default.aspx" class="btn-link">&larr; Back to Dashboard</a>
                </div>
            </main>
        </div>
    </form>
</body>
</html>
