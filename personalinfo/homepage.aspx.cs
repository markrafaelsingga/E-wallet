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
    public partial class WebForm5 : System.Web.UI.Page
    {
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
                            DateRegistered.Text = reader["dateReg"].ToString();
                            CurrentBalance.Text = reader["bal"].ToString();
                            TotalSend.Text = reader["totalsend"].ToString();
                        }
                    }
                }
            }
        }
    }
}