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
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null && Request.UrlReferrer.AbsolutePath.Contains("logout"))
                {
                    Session.Clear();
                    Session.Abandon();
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {         
            string name = TextBox1.Text;
            string pass = TextBox2.Text;
            string id;
            using(var db = new SqlConnection(connStr))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT ID,NAME,PASS FROM USERS_TBL WHERE NAME = '"+name+"' AND PASS = '"+pass+"'";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            Response.Write("<script>alert('Success!')</script>");
                            Session["Username"] = name;
                            id = reader["ID"].ToString();
                            Session["ID"] = id;
                            Session["Password"] = pass;
                            Response.Redirect("homepage.aspx");
                        }
                        else
                        {
                           
                            Response.Write("<script>alert('Unsuccessful login!')</script>");
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("signup.aspx");
        }
    }
}