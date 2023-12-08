<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deposit.aspx.cs" Inherits="personalinfo.deposit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposit</title>
<style>
       body {
            display: flex;
            justify-content: center;
            /* align-items: center; */
            height: 100vh;
        }

        .panel {
            border: none;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
            padding: 20px;
            width: 100%;
            max-width: 5000px;
            text-align: left;
            margin-top: 50px;
            border-radius: 10px;
        }

        

        .deppanel {
            border: none;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
            padding: 20px;
            width: 100%;
            max-width: 5000px;
            text-align: center;
            margin-top: 50px;
            border-radius: 10px;
        }

        .form-group {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: center;
            margin-bottom: 10px;
        }

        .form-group label {
            width: 150px;
            text-align: right;
            margin-right: 10px;
        }


        .form-group input[type="text"],
        .form-group input[type="password"],
        .form-group input[type="email"],
        .form-group input[type="tel"],
        .form-group select {
            width: 300px;
        }
        /* Styles for the sidebar */
        .sidebar {
            position: fixed;
            top: 0;
            left: -250px; /* Initial position outside the viewport */
            width: 250px;
            height: 100vh;
            background-color: #f2f2f2;
            transition: left 0.3s ease;
            z-index: 1000; /* Higher z-index value to place the sidebar on top */
        }
        
        /* Styles for the main content area */
        .main-content {
            margin-left: 0;
            transition: margin-left 0.3s ease;
        }
        
        /* Styles for the toggle button */
        .toggle-button {
            position: fixed;
            top: 20px;
            left: 20px;
            width: 40px;
            height: 40px;
            background-color: #f2f2f2;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            transition: left 0.3s ease;
            z-index: 1500; /* Higher z-index value to place the button on top of the sidebar items */
        }
        
        /* Styles for the toggle button icon */
        .toggle-button i {
            font-size: 20px;
            color: #333;
        }
        
        /* Styles for the expanded sidebar */
        .sidebar-expanded {
            left: 0; /* Show the sidebar by moving it into the viewport */
        }
        
        /* Styles for the shifted main content area */
        .main-content-shifted {
            margin-left: 250px; /* Shift the main content to the right when the sidebar is expanded */
        }
        h1 {
            text-align: center;
        }
        .sidebar ul {
            list-style-type: none;
            padding: 0;
        }

        .sidebar li {
            padding: 10px;
            transition: background-color 0.3s ease;
        }

        .sidebar li:hover {
            background-color: #ddd;
        }

        .sidebar li a {
            text-decoration: none;
            color: #333;
        }

        .sidebar li a:hover {
            color: #000;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">


        <div class="sidebar" id="sidebar">
            <!-- Sidebar content goes here -->
            <ul>
                <li><a href="<%= ResolveUrl("~/homepage.aspx") %>">Home</a></li>
                <li><a href="<%= ResolveUrl("~/sendmoney.aspx") %>">Send Money</a></li>
                <li><a href="<%= ResolveUrl("~/deposit.aspx") %>">Deposit</a></li>
                <li><a href="<%= ResolveUrl("~/withdraw.aspx") %>">Withdraw</a></li>
                 <li><a href="<%= ResolveUrl("~/transaction.aspx") %>">Transaction</a></li>
                <li><a href="#" onclick="logout()">Log Out</a></li>
            </ul>
        </div>
        
        <div class="main-content" id="mainContent">
            <!-- Main content goes here -->
             <div class ="panel">
            <asp:Label ID="Label1" runat="server" Text="Account Number: "></asp:Label>
            <asp:Label ID="AccNum" runat="server"></asp:Label>

                 &nbsp;&nbsp;&nbsp;
   
                 <asp:Label ID="Label2" runat="server" Text="Name: "></asp:Label>
            <asp:Label ID="Name" runat="server"></asp:Label>
            
         &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label3" runat="server" Text="Date Registered: "></asp:Label>
            <asp:Label ID="DateRegistered" runat="server"> </asp:Label>
                 
          &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label4" runat="server" Text="Current Balance: "></asp:Label>
            <asp:Label ID="CurrentBalance" runat="server"></asp:Label>
           &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label5" runat="server" Text="Total Send: "></asp:Label>
            <asp:Label ID="TotalSend" runat="server" ></asp:Label>
         </div>
            <div class ="deppanel">
                <div>
                    Deposit
                    <br /><br />
                    Enter Amount
                </div>
                <asp:TextBox ID="TextBox1" runat="server" min="100" max="10000" placeholder="Amount"></asp:TextBox>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Deposit" OnClick="Button1_Click"></asp:Button>
            </div>
            
        </div>
       
        <div class="toggle-button" id="toggleButton">
            <!-- Toggle button content goes here -->
            <img src="dashboards.png" style="width: 100%; height: 100%; object-fit: contain;"/>
            
        </div>
        
        <script>
            const toggleButton = document.getElementById('toggleButton');
            const sidebar = document.getElementById('sidebar');
            const mainContent = document.getElementById('mainContent');
            function logout() {
                // Perform a server-side logout action
                window.location.href = "logout.aspx"; // Redirect to logout.aspx page
            }

            toggleButton.addEventListener('click', () => {
                sidebar.classList.toggle('sidebar-expanded');
                mainContent.classList.toggle('main-content-shifted');

                const sidebarWidth = sidebar.offsetWidth;
                const toggleButtonWidth = toggleButton.offsetWidth;

                if (sidebar.classList.contains('sidebar-expanded')) {
                    toggleButton.style.left = sidebarWidth + 'px';
                } else {
                    toggleButton.style.left = '20px';
                }
            });
        </script>

        

        

        

    </form>
</body>
</html>
