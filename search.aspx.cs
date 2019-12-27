using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Drawing;

namespace dms
{
    public partial class search : System.Web.UI.Page
    {
        string path; int type;
        Table tab;
        Button btn = new Button();
        Button deletebtn = new Button();
        string filefullpath, fileactualname;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uname"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!IsPostBack)
            {
                typeradiobutton.SelectedIndex = 0;
                filefullpath = fileactualname = "";
                ViewState["data1"] = "";
            }
            else
            {
                if ((ViewState["data1"].ToString()) != "")
                {
                    filefullpath = ViewState["data1"].ToString();
                    fileactualname = ViewState["data2"].ToString();
                }
                setTableview(filefullpath, fileactualname, type);
            }
            path = Session["path"].ToString();
            //string fileName = currentFile.Substring(sourceDirectory.Length + 1);             
        }

        protected void searchbtn_Click(object sender, EventArgs e)
        {
            String searchstr = searchTextBox.Text;
            type = typeradiobutton.SelectedIndex;
            try
            {
                if (type == 0) //file
                {
                    string result = "";
                    foreach (string file in Directory.GetFiles(path, searchstr + ".*", SearchOption.AllDirectories))
                    {
                        if (file.Contains(searchstr))
                        {
                            result = Path.GetFileNameWithoutExtension(file);
                            filefullpath = file;
                            fileactualname = result;
                            ViewState["data1"] = filefullpath;
                            ViewState["data2"] = fileactualname;
                            ViewState["type"] = type;
                            setTableview(file, result, type);
                        }
                    }
                    if (result == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File not found.')", true);                        //Response.Write("file Not found");
                    }
                }
                else
                {
                    string result = "";
                    foreach (string file in Directory.GetDirectories(path, searchstr + ".*", SearchOption.AllDirectories))
                    {
                        if (file.Contains(searchstr))
                        {
                            result = Path.GetFileName(Path.GetDirectoryName(file + "\\"));
                            filefullpath = file;
                            fileactualname = result;
                            ViewState["data1"] = filefullpath;
                            ViewState["data2"] = fileactualname;
                            ViewState["type"] = type;
                            setTableview(file, result, type);
                        }
                    }
                    if (result == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Folder not found.')", true);
                    }
                }
            }
            catch (Exception){   }
        }

        public void setTableview(string fullfile, string filename, int filetype)
        {
            try
            {
                tab = new Table();
                TableRow row1 = new TableRow();
                TableRow row2 = new TableRow();
                row1.BackColor = Color.LightGray;
                TableCell cell1=new TableCell();TableCell cell2=new TableCell();
                TableCell cell3=new TableCell();TableCell headerdeletecell=new TableCell();
                TableCell cell4=new TableCell();TableCell cell5=new TableCell();
                TableCell cell6=new TableCell();TableCell cell7=new TableCell();
                FileUpload fu = new FileUpload();
                cell1.Text = "Full path";
                if (filetype == 0 && (Convert.ToInt32(ViewState["type"])) == 0)
                {
                    cell2.Text = "File name";
                    btn.Text = "Download";
                }
                else
                {
                    cell2.Text = "Folder name";
                    btn.Text = "Upload";
                    cell6.Controls.Add(fu);
                }
                btn.ID = "mybtn"; deletebtn.ID = "deletebtn"; deletebtn.Text = "Delete";
                deletebtn.CssClass=btn.CssClass = "btn";
                tab.ID = "myTable";
                tab.BorderWidth = 2;
                tab.BorderStyle = BorderStyle.Solid;
                tab.CellPadding = tab.CellSpacing = 20;
                tab.CssClass = "table table-hovered";
                cell3.Text = "Action";
                cell4.Text = fullfile;
                cell5.Text = filename;
                headerdeletecell.Text = "Delete";
                cell7.Controls.Add(deletebtn);
                cell6.Controls.Add(btn);
                row1.Cells.Add(cell1);
                row1.Cells.Add(cell2);
                row1.Cells.Add(cell3);
                row1.Cells.Add(headerdeletecell);
                row2.Cells.Add(cell4);
                row2.Cells.Add(cell5);
                row2.Cells.Add(cell6);
                row2.Cells.Add(cell7);
                cell1.BorderWidth = cell2.BorderWidth = cell3.BorderWidth = cell4.BorderWidth = cell5.BorderWidth = cell6.BorderWidth=cell7.BorderWidth= headerdeletecell.BorderWidth= 2;
                tab.Rows.Add(row1);
                tab.Rows.Add(row2);
                Panel1.Controls.Add(tab);
                filefullpath = ViewState["data1"].ToString();
                fileactualname = ViewState["data2"].ToString();
                btn.Click += (se, ev) => downloadbtnclick(se, ev, filefullpath, fileactualname, fu.PostedFile);
                deletebtn.Click += (se, ev) => deletebtnclick(se, ev, filefullpath, fileactualname);
            }
            catch (Exception) { tab.Rows.Clear(); }
        }

        protected void downloadbtnclick(object sender, EventArgs e, string pathdata, string filename, HttpPostedFile httpPostedFile)
        {
            type = Convert.ToInt32(ViewState["type"]);
            if (type == 0)
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(pathdata));
                byte[] data = req.DownloadData(pathdata);
                response.BinaryWrite(data);
                response.End();
            }
            else
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
        }

        protected void deletebtnclick(object sender, EventArgs e, string pathdata, string filename)
        {
            //code to move content to recycle bin folder...
        }
    }
}