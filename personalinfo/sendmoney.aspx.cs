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
    public partial class sendmoney : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        string user,id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Session["username"].ToString();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM USERS_TBL WHERE NAME ='" + user + "'";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            AccNum.Text = reader["Id"].ToString();
                            Name.Text = reader["firstname"].ToString() +" "+ reader["lastname"].ToString();
                            DateRegistered.Text = reader["dateReg"].ToString();
                            CurrentBalance.Text = reader["bal"].ToString();
                            TotalSend.Text = reader["totalsend"].ToString();
                        }
                    }
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string id = TextBox1.Text.ToString();
            using(var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT ID,FIRSTNAME,LASTNAME FROM USERS_TBL WHERE ID =@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    hiding.Visible = true;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Label7.Text = reader["ID"].ToString();
                            Label9.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();
                        }
                    }
                }
            }
        }

        
        protected void Button2_Click(object sender, EventArgs e)
        {
            string user_id = TextBox1.Text.ToString();
            string pass = TextBox3.Text.ToString();
            decimal sendMoney = Convert.ToDecimal(TextBox2.Text);
            decimal receive = 0;
            decimal send = 0;
            decimal recBal = 0;
            decimal sendBal = 0;
            string password = string.Empty;
            string id = Session["ID"].ToString();
            string user = Session["Username"].ToString();
            id = Session["id"] as String;
            int ids = Convert.ToInt32(id);


            using (var db1=new SqlConnection(connString))
            {
                db1.Open();
                using(var cmd = db1.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT PASS FROM USERS_TBL WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["PASS"].ToString();
                        }
                    }
                }
            }

            if(pass==password)
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT BAL FROM USERS_TBL WHERE ID = @user_id";
                        cmd.Parameters.AddWithValue("@user_id", user_id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                recBal = Convert.ToDecimal(reader["BAL"]);
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Receiver ID')</script>");
                                return;
                            }
                        }
                    }

                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT BAL FROM USERS_TBL WHERE ID=@id";
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                sendBal = Convert.ToDecimal(reader["BAL"]);
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid Sender ID')</script>");
                                return;
                            }
                        }
                    }
                    if (sendMoney > 100)
                    {
                        if (sendMoney % 100 == 0)
                        {
                            if (sendMoney <= sendBal)
                            {
                                send = sendBal - sendMoney;

                                using (var cmd = db.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "UPDATE USERS_TBL SET BAL = @send,TOTALSEND = @totsend WHERE ID = @id";
                                    cmd.Parameters.AddWithValue("@send", send);
                                    cmd.Parameters.AddWithValue("@id", id);
                                    cmd.Parameters.AddWithValue("@totsend", send);

                                    var ctr = cmd.ExecuteNonQuery();
                                    if (ctr == 1)
                                    {
                                        receive = recBal + sendMoney;

                                        using (var cmd1 = db.CreateCommand())
                                        {
                                            cmd1.CommandType = CommandType.Text;
                                            cmd1.CommandText = "UPDATE USERS_TBL SET BAL = @receive WHERE ID = @user_id";
                                            cmd1.Parameters.AddWithValue("@receive", receive);
                                            cmd1.Parameters.AddWithValue("@user_id", user_id);

                                            var ctr1 = cmd1.ExecuteNonQuery();
                                            if (ctr1 == 1)
                                            {
                                                using (var cmd2 = db.CreateCommand())
                                                {
                                                    cmd2.CommandText = "INSERT INTO TRANS_TBL (trans_type, trans_value, trans_date, trans_senrec, Id) " +
                                                                        "VALUES (@trans_type, @trans_value, @trans_date, @trans_senrec, @Id)";
                                                    cmd2.Parameters.AddWithValue("@trans_type", "Send");
                                                    cmd2.Parameters.AddWithValue("@trans_value", sendMoney);
                                                    cmd2.Parameters.AddWithValue("@trans_date", DateTime.Now);
                                                    cmd2.Parameters.AddWithValue("@trans_senrec", user_id); // Receiver's ID
                                                    cmd2.Parameters.AddWithValue("@Id", AccNum.Text); // Sender's ID


                                                    var ctr2 = cmd2.ExecuteNonQuery();

                                                    if (ctr2 >= 1)
                                                    {
                                                        using (var cmd3 = db.CreateCommand())
                                                        {
                                                            cmd3.CommandText = "INSERT INTO TRANS_TBL (trans_type, trans_value, trans_date, trans_senrec, Id) " +
                                                                               "VALUES (@trans_type, @trans_value, @trans_date, @trans_senrec, @Id)";
                                                            cmd3.Parameters.AddWithValue("@trans_type", "Received");
                                                            cmd3.Parameters.AddWithValue("@trans_value", sendMoney);
                                                            cmd3.Parameters.AddWithValue("@trans_date", DateTime.Now);
                                                            cmd3.Parameters.AddWithValue("@trans_senrec", ids); // Sender's ID
                                                            cmd3.Parameters.AddWithValue("@Id", user_id); // Receiver's ID

                                                            var ctr3 = cmd3.ExecuteNonQuery();
                                                            if (ctr3 >= 1)
                                                            {
                                                                Response.Write("<script>alert('Recorded!')</script>");
                                                            }
                                                            else
                                                            {
                                                                Response.Write("<script>alert('Not Recorded!')</script>");
                                                            }
                                                        }
                                                    }
                                                }
                                            }                                    
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Send Money Unsuccessful!')</script>");
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Insufficient Balance')</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Cant send below 100')</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Incorrect Password!')</script>");
            }
        }
    }
}
