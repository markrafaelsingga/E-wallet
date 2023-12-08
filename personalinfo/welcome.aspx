<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="personalinfo.welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage</title>
    <style>
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
                
                <li><a href ="login.aspx" >Log in</a></li>
                <li>About us</li>
            </ul>
        </div>
        
        <div class="main-content" id="mainContent">
            <!-- Main content goes here -->
            <div>
                <h1>PAPA P-WALLET</h1>
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
