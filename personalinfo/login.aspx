<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="personalinfo.WebForm3" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .center {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
        }

        .container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .panel {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #fff;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
            padding: 20px;
            width : 66%;
            max-width: 1000px; /* Adjust the max-width value as needed */
            text-align: center;
            border-radius: 10px;
            transition: transform 0.3s ease;
        }

        .panel:hover {
            transform: translateY(-10px);
        }

        .panel h2 {
            margin-top: 0;
            margin-bottom: 30px;
            color: #333;
            font-size: 24px;
        }
    
        .panel input[type="text"],
        .panel input[type="password"],
        .panel button {
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
            transition: border-color 0.3s ease;
        }

        .panel input[type="text"]:focus,
        .panel input[type="password"]:focus,
        .panel button:focus {
            outline: none;
            border-color: #4CAF50;
        }

        .panel button {
            background-color: #4CAF50;
            color: #ff006e;
            border: none;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            padding: 12px;
            transition: background-color 0.3s ease;
        }

        .panel button:hover {
            background-color: red;
        }
    

        /* Styles for the sidebar */
        .sidebar {
            position: fixed;
            top: 0;
            left: -250px; /* Initial position outside the viewport */
            width: 250px;
            height: 100vh;
            background-color: darkred;
            transition: left 0.3s ease;
            z-index: 1000; /* Higher z-index value to place the sidebar on top */
            color: white;
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
            background-color: #000;
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
            color: #fff;
        }
        
        /* Styles for the expanded sidebar */
        .sidebar-expanded {
            left: 0; /* Show the sidebar by moving it into the viewport */
        }
        
        /* Styles for the shifted main content area */
        .main-content-shifted {
            margin-left: 250px; /* Shift the main content to the right when the sidebar is expanded */
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
            background-color: #fff;
        }

        .sidebar li a {
            text-decoration: none;
            color: #000;
        }

        .sidebar li a:hover {
            color: #000;
        }

        .custom-button {}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="sidebar" id="sidebar">
            <!-- Sidebar content goes here -->
            <ul>
               
               <li><a href="<%= ResolveUrl("~/login.aspx") %>">Log In</a></li>
                <li>About</li>
            </ul>
        </div>
        <div class="main-content" id="mainContent">
            <div class="center">
                <div class="panel">
                    <div>
                        Username
                    </div>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Username" Height="38px" Width="400px"></asp:TextBox>
                    <br />
                    <br />
                    Password<br />
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter Password" type="password" Height="36px" Width="402px"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <div style="text-align: center;">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Log in" Height="40px" Width="95px" />
                        &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Sign up"
    CssClass="custom-button" Height="40px" Width="95px" />
                    </div>
                </div>
            </div>
        </div>
   <div class="toggle-button" id="toggleButton">
    <!-- Toggle button content goes here -->
    <img src="new_icon.png" style="width: 190%; height: 120%; object-fit: contain;" />
</div>
        <script>
            const toggleButton = document.getElementById('toggleButton');
            const sidebar = document.getElementById('sidebar');
            const mainContent = document.getElementById('mainContent');

            toggleButton.addEventListener('click', () => {
                sidebar.classList.toggle('sidebar-expanded');
                mainContent.classList.toggle('main-content-shifted');

                const sidebarWidth = sidebar.offsetWidth;
                const toggleButtonWidth = toggleButton.offsetWidth;

                if (sidebar.classList.contains('sidebar-expanded')) {
                    toggleButton.style.left = sidebarWidth + toggleButtonWidth + 'px';
                } else {
                    toggleButton.style.left = '20px';
                }
            });
        </script>
    </form>
</body>
</html>