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
    public partial class change : System.Web.UI.Page
    {
        string id;
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Session["id"] as string;
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

        protected void btnCP_Click(object sender, EventArgs e)
        {
            string oldPass = TextBox1.Text;
            string newPass = TextBox2.Text;
            string password = string.Empty;
            string id = Session["ID"].ToString();

            using (var db = new SqlConnection(connStr))
            {
                db.Open();

                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT PASS FROM USERS_TBL WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            password = reader["PASS"].ToString();
                        }
                    }
                }
            }

            if (oldPass == password)
            {
                using (var db1 = new SqlConnection(connStr))
                {
                    db1.Open();

                    using (var cmd1 = db1.CreateCommand())
                    {
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "SELECT ID FROM USERS_TBL WHERE ID = @id AND PASS = @password";
                        cmd1.Parameters.AddWithValue("@id", id);
                        cmd1.Parameters.AddWithValue("@password", password);

                        object result = cmd1.ExecuteScalar();

                        if (result != null)
                        {
                            using (var cmd2 = db1.CreateCommand())
                            {
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = "UPDATE USERS_TBL SET PASS = @newPass WHERE ID = @id";
                                cmd2.Parameters.AddWithValue("@newPass", newPass);
                                cmd2.Parameters.AddWithValue("@id", id);

                                int ctr1 = cmd2.ExecuteNonQuery();

                                if (ctr1 >= 1)
                                {
                                    Response.Redirect("homepage.aspx");
                                    Response.Write("<script>alert('Password changed!')</script>");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Unsuccessful!')</script>");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Incorrect old password!')</script>");
            }
        }

    }
}