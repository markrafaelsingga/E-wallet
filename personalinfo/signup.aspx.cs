using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.IO;

namespace personalinfo
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\source\repos\personalinfo\personalinfo\App_Data\Database2.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = picUpload.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string fileExt = Path.GetExtension(filename).ToLower();
            int filesize = postedFile.ContentLength;
            byte[] pic = new byte[picUpload.PostedFile.ContentLength];
            picUpload.PostedFile.InputStream.Read(pic, 0, picUpload.PostedFile.ContentLength);
            string lastname = TextBox1.Text;
            string firstname = TextBox2.Text;
            string gender = DropDownList7.SelectedValue;
            string civilstatus = DropDownList8.SelectedValue;
            string dob = TextBox9.Text;
            string email = TextBox6.Text;
            string phone = TextBox7.Text;
            string address = TextBox3.Text;
            string zipcode = TextBox8.Text;
            string name = TextBox10.Text;
            string pass = TextBox11.Text;
            DateTime dateReg = DateTime.Now;
            
           
            if (fileExt == ".bpm" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".jpeg")
            {
                using (var db = new SqlConnection(connString))
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO USERS_TBL VALUES(@lname,@fname,@gender,@civilstatus,@dob,@email,@phone,@address,@zipcode,@pic,@name,@pass,@dateReg,@bal,@totalsend)";                        
                        cmd.Parameters.AddWithValue("@lname", lastname);
                        cmd.Parameters.AddWithValue("@fname", firstname);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@civilstatus", civilstatus);
                        cmd.Parameters.AddWithValue("@dob", dob);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@zipcode", zipcode);
                        cmd.Parameters.AddWithValue("@pic", pic);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@pass", pass);
                        cmd.Parameters.AddWithValue("@dateReg", dateReg);
                        cmd.Parameters.AddWithValue("@bal", 0);
                        cmd.Parameters.AddWithValue("@totalsend", 0);



                        var ctr = cmd.ExecuteNonQuery();
                        if (ctr >= 1)
                        {
                            Response.Write("<script>alert('Data is saved')</script>");
                            Response.Redirect("login.aspx");
                        }
                    }
                }

            }
            else
            {
                Response.Write("<script>alert('FILE UPLOADED IS NOT AN IAMGE!)</script>");
            }
        }
    }
}
    
