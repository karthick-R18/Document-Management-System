using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using System.Drawing;

namespace dms
{
    public partial class explorer : System.Web.UI.Page
    {
        string path = "";
        TableCell cell3;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uname"] == null)
            {
                Response.Redirect("login.aspx");
            }
            //int filecount = Directory.GetFiles(path).Length;
            path = Session["path"].ToString();
            if (!IsPostBack)
            {
                setupTable(path);
                ViewState["temppathvalue"] = "";
                backbtn.Visible = false;
            }
            else
            {
                if (ViewState["temppathvalue"].ToString() != "")
                {
                    path = ViewState["temppathvalue"].ToString();
                    setupTable(path);
                }
                else
                {
                    setupTable(path);
                }
            }
        }

        protected void uploadbtnClick(object sender, EventArgs e, string pathdata, HttpPostedFile httpPostedFile)
        {
            try
            {
                if (httpPostedFile != null)
                {
                    httpPostedFile.SaveAs(pathdata + "\\" + httpPostedFile.FileName);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File uploaded successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select the file please!')", true);
                }
            }
            catch (Exception) { }
        }

        protected void downloadbtnClick(object sender, EventArgs e, string pathdata, string filename)
        {
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            byte[] data = req.DownloadData(pathdata);
            response.BinaryWrite(data);
            response.End();
        }

        public void setupTable(string currentpath)
        {
            try
            {
                TableRow headerrow = new TableRow();
                TableCell headercell1 = new TableCell();
                TableCell headercell2 = new TableCell();
                TableCell headercell3 = new TableCell();
                TableCell headercell4 = new TableCell();
                headercell1.Text = "Name";
                headercell2.Text = "Action";
                headercell3.Text = "Path";
                headercell4.Text = "Delete";
                headerrow.Cells.Add(headercell1);
                headerrow.Cells.Add(headercell2);
                headerrow.Cells.Add(headercell3);
                headerrow.Cells.Add(headercell4);
                headerrow.BackColor = Color.LightGray;
                Table1.Rows.Add(headerrow);
                //headerrow.BackColor = '#C1B8B8';
                foreach (string data in Directory.GetDirectories(currentpath))
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell4 = new TableCell();
                    cell3 = new TableCell();
                    cell3.Text = data;
                    string data1 = cell3.Text;
                    LinkButton folderlink = new LinkButton();
                    folderlink.Text = Path.GetFileName(Path.GetDirectoryName(data + "\\"));
                    if (folderlink.Text == "Recyclebin")
                    {
                        continue;
                    }
                    cell1.Controls.Add(folderlink);
                    Button btn = new Button();
                    Button deletebtn = new Button();
                    btn.Text = "Upload";
                    deletebtn.Text = "Delete";
                    FileUpload fu = new FileUpload();
                    deletebtn.CssClass = btn.CssClass = "btn";
                    cell2.Controls.Add(fu);
                    cell2.Controls.Add(btn);
                    cell4.Controls.Add(deletebtn);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    Table1.Rows.Add(row);
                    btn.Click += (se, ev) => uploadbtnClick(se, ev, data1, fu.PostedFile);
                    deletebtn.Click += (se, ev) => deletebtnclick(se, ev, data1);
                    folderlink.Click += (se, ev) => nextFolderclick(se, ev, data1);
                }

                foreach (string data in Directory.GetFiles(currentpath))
                {
                    TableRow row = new TableRow();
                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell4 = new TableCell();
                    cell3 = new TableCell();
                    cell3.Text = data;
                    string data1 = cell3.Text;
                    string fname = Path.GetFileName(data);
                    cell1.Text = Path.GetFileNameWithoutExtension(data);
                    Button btn = new Button();
                    Button deletebtn = new Button();
                    deletebtn.Text = "Delete";
                    btn.Text = "Download";
                    deletebtn.CssClass = btn.CssClass = "btn";
                    cell4.Controls.Add(deletebtn);
                    btn.Click += (se, ev) => downloadbtnClick(se, ev, data, fname);
                    deletebtn.Click += (se, ev) => deletebtnclick(se, ev, data1);
                    cell2.Controls.Add(btn);
                    row.Cells.Add(cell1);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    row.Cells.Add(cell4);
                    Table1.Rows.Add(row);
                }
                string checkpath = cell3.Text;
                string backpath = Directory.GetParent(checkpath).ToString();
                if (backpath == (Session["path"].ToString()))
                {
                    backbtn.Visible = false;
                }
                else
                {
                    backbtn.Visible = true;
                }
                //ViewState["temppathvalue"] = "";
            }
            catch (Exception) { }
        }

        protected void nextFolderclick(object sender, EventArgs e, string pathdata)
        {
            Table1.Rows.Clear();
            ViewState["temppathvalue"] = pathdata;
            setupTable(pathdata);
        }

        protected void createfolderbtn_Click(object sender, EventArgs e)
        {
            if (newfoldertextbox.Text != "")
            {
                string newfoldername = newfoldertextbox.Text;
                Directory.CreateDirectory(path + "\\" + newfoldername);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('New folder created successfully.')", true);
                Response.Redirect("explorer.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please give the name for new folder.')", true);
            }
        }

        protected void deletebtnclick(object sender, EventArgs e, string pathdata)
        {
            string last = pathdata.Split('\\').Last();
            string testpath = Session["path"].ToString() + "\\Recyclebin\\" + last;
            if (Directory.Exists(pathdata))
            {
                Directory.CreateDirectory(Session["path"].ToString() + "\\Recyclebin\\" + last);
                string[] folders = Directory.GetDirectories(pathdata);
                foreach (string Dir in folders)
                {
                    string name = Path.GetFileName(Dir);
                    string dest = Path.Combine(testpath, name);
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
                Response.Redirect("explorer.aspx");
            }
            if (File.Exists(pathdata))
            {
                File.Move(pathdata, testpath);
                Response.Redirect("explorer.aspx");
            }
        }

        protected void backbtn_Click(object sender, EventArgs e)
        {
            string currentpath = cell3.Text;
            string backpath = Directory.GetParent(currentpath).ToString();
            if (backpath != Session["path"].ToString())
            {
                backpath = Directory.GetParent(backpath).ToString();
            }
            else
            {
                Response.Redirect("explorer.aspx");
            }
            Table1.Rows.Clear();
            setupTable(backpath);
        }
    }
}