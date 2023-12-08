using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace personalinfo
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database1.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var id = TextBox1.Text;
            using (var db = new SqlConnection(connString))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM USERS WHERE ID = '" + id + "'";
                    using (SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        byte[] pic = (byte[])reader["PIC"];
                        string str = Convert.ToBase64String(pic);
                        Label1.Text = reader["LASTNAME"].ToString();
                        Label2.Text = reader["FIRSTNAME"].ToString();

                    }
                }
            }
        }
    }
}