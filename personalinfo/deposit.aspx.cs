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
    public partial class deposit : System.Web.UI.Page
    {
        string user;
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Session["username"].ToString();
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM USERS_TBL WHERE NAME = '" + user + "'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AccNum.Text = reader["Id"].ToString();
                            Name.Text = reader["firstname"].ToString() + " " + reader["lastname"].ToString();
                            DateRegistered.Text = reader["DATEREG"].ToString();
                            CurrentBalance.Text = reader["BAL"].ToString();
                           // TotalSend.Text = reader["SENTMONEY"].ToString();

                        }
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string dep = TextBox1.Text;
            decimal deposit = Convert.ToDecimal(dep);
            decimal bal = 0;
            decimal finaldepos = 0;
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

                    finaldepos = deposit + bal;
                    if (deposit > 100)
                    {
                        if (finaldepos % 100 == 0)
                        {
                            if (finaldepos <= 50000)
                            {
                                cmd.CommandText = "UPDATE USERS_TBL SET BAL = @finaldepos WHERE NAME = @user";
                                cmd.Parameters.AddWithValue("@finaldepos", finaldepos);

                                int ctr = cmd.ExecuteNonQuery();
                                if (ctr >= 1)
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO TRANS_TBL(TRANS_TYPE,TRANS_VALUE,TRANS_DATE,ID)"
                                         + "VALUES(@trans_type,@trans_value,@trans_date,@Id)";
                                    cmd.Parameters.AddWithValue("@trans_type", "Deposit");
                                    cmd.Parameters.AddWithValue("@trans_value", deposit);
                                    cmd.Parameters.AddWithValue("@trans_date", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@Id", AccNum.Text);

                                    var ctr2 = cmd.ExecuteNonQuery();
                                    if (ctr2 >= 1)
                                    {
                                        Response.Write("<script>alert('Recorded!')</script>");

                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Not Recorded!')</script>");
                                    }
                                }
                                else
                                {
                                    Response.Write("<script>alert('Deposit Unsuccessful')</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Total current balance must not exceed 50000')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Deposit amount must be divisible by 100')</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Cant deposit below 100')</script>");
                    }
                }
            }
        }
    }
}
