using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace personalinfo
{
    public partial class transaction : System.Web.UI.Page
    {
        string user;
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Session["id"] as string;
            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM USERS_TBL WHERE ID = @name";
                    cmd.Parameters.AddWithValue("@name", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AccNum.Text = reader["Id"].ToString();
                            Name.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();
                            DateRegistered.Text = reader["dateReg"].ToString();
                            CurrentBalance.Text = reader["bal"].ToString();
                            TotalSend.Text = reader["totalsend"].ToString();
                        }
                    }
                }
            
            }
        }






        protected void btnDepositWithdrawalReport_Click(object sender, EventArgs e)
        {
            string id = Session["id"] as string;
            string from = TextBox1.Text;
            string to = TextBox2.Text;

            using(var db = new SqlConnection(connStr))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                         "FROM TRANS_TBL " +
                         "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                         "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Withdraw', 'Deposit') " +
                         "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                    cmd.Parameters.AddWithValue("@name", id);
                    cmd.Parameters.AddWithValue("@fromDate", from);
                    cmd.Parameters.AddWithValue("@toDate", to);
                    DataTable dt = new DataTable();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    transType.Text = "Deposit/Withdraw";
                }
            }
        }

        protected void btnSendReceivedReport_Click(object sender, EventArgs e)
        {
            string id = Session["id"] as string;
            string from = TextBox1.Text;
            string to = TextBox2.Text;

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                         "FROM TRANS_TBL " +
                         "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                         "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Send', 'Received') " +
                         "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                    cmd.Parameters.AddWithValue("@name", id);
                    cmd.Parameters.AddWithValue("@fromDate", from);
                    cmd.Parameters.AddWithValue("@toDate", to);
                    DataTable dt = new DataTable();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    transType.Text = "Sent/Received";
                }
            }

        }

        protected void btnSOA_Click(object sender, EventArgs e)
        {
            string id = Session["id"] as string;
            string from = TextBox3.Text;
            string to = TextBox4.Text;
            //OR TRANS_TBL.TRANS_SENREC=@Id 
            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                          "FROM TRANS_TBL " +
                          "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                          "WHERE USERS_TBL.ID = @id AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate AND TRANS_TBL.TRANS_TYPE IN ('Send','Withdraw','Received')";
                    cmd.Parameters.AddWithValue("@id",id );
                    cmd.Parameters.AddWithValue("@fromDate", from);
                    cmd.Parameters.AddWithValue("@toDate", to); // Replace 'UserID' with the appropriate foreign key column name
                    
                    DataTable dt = new DataTable();

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    grdVw.DataSource = dt;
                    grdVw.DataBind();
                }
            }

        }

        protected void btnGen(object sender, EventArgs e)
        {
            if(selTrans.SelectedValue == "Deposit")
            {
                string id = Session["id"] as string;
                string from = TextBox1.Text;
                string to = TextBox2.Text;

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                             "FROM TRANS_TBL " +
                             "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                             "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Deposit') " +
                             "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                        cmd.Parameters.AddWithValue("@name", id);
                        cmd.Parameters.AddWithValue("@fromDate", from);
                        cmd.Parameters.AddWithValue("@toDate", to);
                        DataTable dt = new DataTable();

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        transType.Text = "Deposit";
                    }
                }
            }
            if (selTrans.SelectedValue == "Withdraw")
            {
                string id = Session["id"] as string;
                string from = TextBox1.Text;
                string to = TextBox2.Text;

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                             "FROM TRANS_TBL " +
                             "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                             "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Withdraw') " +
                             "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                        cmd.Parameters.AddWithValue("@name", id);
                        cmd.Parameters.AddWithValue("@fromDate", from);
                        cmd.Parameters.AddWithValue("@toDate", to);
                        DataTable dt = new DataTable();

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        transType.Text = "Withdraw";
                    }
                }
            }
            if (selTrans.SelectedValue == "Send")
            {
                string id = Session["id"] as string;
                string from = TextBox1.Text;
                string to = TextBox2.Text;

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                             "FROM TRANS_TBL " +
                             "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                             "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Send') " +
                             "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                        cmd.Parameters.AddWithValue("@name", id);
                        cmd.Parameters.AddWithValue("@fromDate", from);
                        cmd.Parameters.AddWithValue("@toDate", to);
                        DataTable dt = new DataTable();

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        transType.Text = "Sent";
                    }
                }
            }
            if (selTrans.SelectedValue == "Received")
            {
                string id = Session["id"] as string;
                string from = TextBox1.Text;
                string to = TextBox2.Text;

                using (var db = new SqlConnection(connStr))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT TRANS_TBL.ID, USERS_TBL.FIRSTNAME, USERS_TBL.LASTNAME, TRANS_TBL.TRANS_TYPE, TRANS_TBL.TRANS_VALUE, TRANS_TBL.TRANS_DATE,TRANS_TBL.TRANS_SENREC " +
                             "FROM TRANS_TBL " +
                             "JOIN USERS_TBL ON TRANS_TBL.ID = USERS_TBL.ID " +
                             "WHERE USERS_TBL.ID = @name AND TRANS_TBL.TRANS_TYPE IN ('Received') " +
                             "AND TRANS_TBL.TRANS_DATE BETWEEN @fromDate AND @toDate";
                        cmd.Parameters.AddWithValue("@name", id);
                        cmd.Parameters.AddWithValue("@fromDate", from);
                        cmd.Parameters.AddWithValue("@toDate", to);
                        DataTable dt = new DataTable();

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        transType.Text = "Received";
                    }
                }
            }
        }
    }
}