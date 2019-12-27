using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dms
{
    public partial class login : System.Web.UI.Page
    {
        dmsdatabaseEntities en = new dmsdatabaseEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel p = (Panel)Master.FindControl("navpanel");
            p.Width = 0;
            p.Height = 0;
            p.Visible = false;
        }

        protected void loginbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                var row = (from x in en.profiles where x.username == username && x.password == password select x).FirstOrDefault();
                if (row != null)
                {
                    Session["uname"] = row.username;
                    Session["name"] = row.name;
                    Response.Redirect("home.aspx");
                    errorlabel.Text = "";
                }
                else
                {
                    errorlabel.Text = "Wrong username or password!";
                    passwordTextBox.Text = "";
                }
            }
            catch (Exception)
            {
                Response.Write("\"DB connection error\", please connect the database properly.");
            }
        }
    }
}