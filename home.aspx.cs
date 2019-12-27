using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace dms
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uname"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                string uname = Session["uname"].ToString();
                string path = @"C:\";
                Session["path"] = path;
                path += "\\Recyclebin";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path+"\\Recyclebin");
                }
            }
        }
    }
}