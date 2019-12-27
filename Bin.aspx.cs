using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace dms
{
    public partial class Bin : System.Web.UI.Page
    {
        string path;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uname"] == null)
            {
                Response.Redirect("login.aspx");
            }
            path = Session["path"].ToString();
            path += "\\Recyclebin";
            binTable(path);
        }

        public void binTable(string binpath)
        {
            TableRow headerrow = new TableRow();
            TableCell headercell1 = new TableCell();
            TableCell headercell2 = new TableCell();
            TableCell headercell3 = new TableCell();
            TableCell headercell4 = new TableCell();
            headercell1.Text = "Name";
            headercell2.Text = "Path";
            headercell3.Text = "Restore";
            headercell4.Text = "Restore Path";
            headerrow.Cells.Add(headercell1);
            headerrow.Cells.Add(headercell2);
            headerrow.Cells.Add(headercell3);
            headerrow.Cells.Add(headercell4);
            headerrow.BackColor = Color.LightGray;
            Table1.Rows.Add(headerrow);
            foreach (string data in Directory.GetDirectories(binpath))
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                Button btn = new Button();
                btn.Text = "Restore"; btn.CssClass = "btn";
                string foldername = Path.GetFileName(Path.GetDirectoryName(data + "\\"));
                cell1.Text = foldername;
                cell2.Text = data;
                cell3.Controls.Add(btn);
                string restorepath = Session["path"].ToString() + "\\" + cell1.Text;
                cell4.Text = restorepath;
                string data1 = cell2.Text;
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                Table1.Rows.Add(row);
                btn.Click += (se, ev) => restorebtnclick(se, ev, data1);
            }

            foreach (string data in Directory.GetFiles(binpath))
            {
                TableRow row = new TableRow();
                TableCell cell1 = new TableCell();
                TableCell cell2 = new TableCell();
                TableCell cell3 = new TableCell();
                TableCell cell4 = new TableCell();
                Button btn = new Button();
                btn.Text = "Restore"; btn.CssClass = "btn";
                cell1.Text = Path.GetFileNameWithoutExtension(data);
                cell2.Text = data;
                cell3.Controls.Add(btn);
                string restorepath = Session["path"].ToString() + "\\" + cell1.Text;
                cell4.Text = restorepath;
                string data1 = cell2.Text;
                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                Table1.Rows.Add(row);
                btn.Click += (se, ev) => restorebtnclick(se, ev, data1);
            }
        }

        protected void restorebtnclick(object sender, EventArgs e, string pathdata)
        {

            string last = pathdata.Split('\\').Last();
            string testpath = Session["path"].ToString() + "\\" + last;
            if (Directory.Exists(pathdata))
            {
                Directory.CreateDirectory(Session["path"].ToString() + "\\" + last);
                string[] folders = Directory.GetDirectories(pathdata);
                foreach (string Dir in folders)
                {
                    string name = Path.GetFileName(Dir);
                    string dest = Path.Combine(testpath, name);
                    //  copyfolder(Dir, dest);
                    Directory.Move(Dir, dest);
                }
                string[] files = Directory.GetFiles(pathdata);

                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(testpath, name);
                    File.Move(file, dest);
                }
                Directory.Delete(pathdata);
                Response.Redirect("Bin.aspx");
            }
            if (File.Exists(pathdata))
            {
                //File.Copy(pathdata, testpath2);
                File.Move(pathdata, testpath);
                Response.Redirect("Bin.aspx");
            }
        }
    }
}