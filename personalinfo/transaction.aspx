<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="transaction.aspx.cs" Inherits="personalinfo.transaction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transaction</title>
<style>
        body {
            display: flex;
            justify-content: center;
           /* align-items: center;*/
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
        .panel1 {
            border: none;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
            padding: 20px;
            width: 100%;
            max-width: 5000px;
            text-align: center;
            margin-top: 90px;
            border-radius: 10px;
        }

        .transpanel {
            border: none;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.15);
            padding: 20px;
            width: 100%;
            max-width: 5000px;
            text-align: center;
            margin-top: 70px;
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
                <li><a href="<%= ResolveUrl("~/change.aspx") %>">Change Password</a></li>
                <li><a href="<%= ResolveUrl("~/transaction.aspx") %>">Change Password</a></li>
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
            <asp:Label ID="DateRegistered" runat="server"></asp:Label>
                 
          &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label4" runat="server" Text="Current Balance: "></asp:Label>
            <asp:Label ID="CurrentBalance" runat="server"></asp:Label>
           &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label5" runat="server" Text="Total Send: "></asp:Label>
            <asp:Label ID="TotalSend" runat="server" Text="500.00"></asp:Label>
         </div>
            <div class="transpanel">
                <h3>STATEMENT OF ACCOUNT</h3>
                <br />
               <div style="height: 300px; overflow-y: auto;">
    <asp:GridView ID="grdVw" runat="server" align="center" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Account Number" />
            <asp:BoundField DataField="firstname" HeaderText="Firstname" />
            <asp:BoundField DataField="lastname" HeaderText="Lastname" />    
            <asp:BoundField DataField="trans_type" HeaderText="Transaction Type" />
            <asp:BoundField DataField="trans_value" HeaderText="Amount" />
            <asp:BoundField DataField="trans_date" HeaderText="Date" />
            <asp:BoundField DataField="trans_senrec" HeaderText="Sender/Receiver" />
        </Columns>
    </asp:GridView>
                   <br />
                    <asp:TextBox ID="TextBox3" runat="server" placeholder="From" type="date" style="text-align: center;"
    max='<%= DateTime.Now.ToString("yyyy-MM-dd") %>'></asp:TextBox>
&nbsp;
<asp:TextBox ID="TextBox4" runat="server" placeholder="To" type="date" style="text-align: center;"
    max='<%= DateTime.Now.ToString("yyyy-MM-dd") %>'></asp:TextBox>
                   <br />
                     <asp:Button ID="Button1" runat="server" Text="Generate Statement of Account" OnClick="btnSOA_Click"></asp:Button>
</div>
                </div>

                <div class="panel1">
                    <br />
                    <asp:DropDownList ID="selTrans" runat="server">
                        <asp:ListItem Text="--Select Transaction--" Value="" Selected="False"></asp:ListItem>
                        <asp:ListItem>Deposit</asp:ListItem>
                        <asp:ListItem>Withdraw</asp:ListItem>
                        <asp:ListItem>Send</asp:ListItem>
                        <asp:ListItem>Received</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="transType" runat="server"></asp:Label>
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="From" type="date" style="text-align: center;"
    max='<%= DateTime.Now.ToString("yyyy-MM-dd") %>'></asp:TextBox> &nbsp;
                    <asp:TextBox ID="TextBox2" runat="server" placeholder="To" type="date" style="text-align: center;"
    max='<%= DateTime.Now.ToString("yyyy-MM-dd") %>'></asp:TextBox>
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="Generate" OnClick="btnGen" />
                    <br /><br />
                    <asp:GridView ID="GridView1" runat="server" align="center" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Account Number" />
            <asp:BoundField DataField="firstname" HeaderText="Firstname" />
            <asp:BoundField DataField="lastname" HeaderText="Lastname" />    
            <asp:BoundField DataField="trans_type" HeaderText="Transaction Type" />
            <asp:BoundField DataField="trans_value" HeaderText="Amount" />
            <asp:BoundField DataField="trans_date" HeaderText="Date" />
            <asp:BoundField DataField="trans_senrec" HeaderText="Sender/Receiver" />
        </Columns>
    </asp:GridView>
                    <br />
                
               
              
             
                <asp:Button ID="btnDepositWithdrawalReport" runat="server" Text="Generate Deposit/Withdrawal Report" OnClick="btnDepositWithdrawalReport_Click" />
<asp:Button ID="btnSendReceivedReport" runat="server" Text="Generate Send/Received Report" OnClick="btnSendReceivedReport_Click"/>

                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    

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
                window.location.href = "login.aspx"; // Redirect to logout.aspx page
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
