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
    public partial class withdraw : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Session["username"].ToString();
            using(var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM USERS_TBL WHERE NAME ='"+user+"'";
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AccNum.Text = reader["Id"].ToString();
                            Name.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();
                            DateRegistered.Text = reader["DATEREG"].ToString();
                            CurrentBalance.Text = reader["BAL"].ToString();
                            TotalSend.Text = reader["totalsend"].ToString();
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string wd = TextBox1.Text;
            decimal withdraw = Convert.ToDecimal(wd);
            decimal bal = 0;
            string user = Session["username"].ToString();

            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT BAL FROM USERS_TBL WHERE NAME = @user";

                    cmd.Parameters.AddWithValue("@user", user);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bal = Convert.ToDecimal(reader["BAL"]);
                        }
                    }


                    if (withdraw > 100)
                    {
                        if (withdraw % 100 == 0 )
                        {
                      
                            if (withdraw <= bal)
                            {
                                decimal latestbal = bal - withdraw;
                                cmd.CommandText = "UPDATE USERS_TBL SET BAL = @latestbal WHERE NAME = @user";
                                cmd.Parameters.AddWithValue("@latestbal", latestbal);

                                int ctr1 = cmd.ExecuteNonQuery();
                                if (ctr1 >= 1)
                                {

                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO TRANS_TBL(TRANS_TYPE,TRANS_VALUE,TRANS_DATE,ID)"
                                        + "VALUES(@trans_type,@trans_value,@trans_date,@Id)";
                                    cmd.Parameters.AddWithValue("@trans_type", "Withdraw");
                                    cmd.Parameters.AddWithValue("@trans_value", withdraw);
                                    cmd.Parameters.AddWithValue("@trans_date", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@Id", AccNum.Text);

                                    var ctr2 = cmd.ExecuteNonQuery();
                                    if (ctr2 >= 1)
                                    {
                                        Response.Write("<script>alert('Withdraw Successful!')</script>");

                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Not Recorded!')</script>");
                                    }

                                }
                                else
                                {
                                    Response.Write("<script>alert('Withdraw Unsuccessful')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Insufficient Balance')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Withdrawal amount must be divisible by 100')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('You can't withdraw below 100')</script>");
                        
                    }
                }
            }
        }
    }
}