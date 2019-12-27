using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace DmsTest
{
    public partial class fileExplorer : System.Web.UI.Page
    {
        String Envpath = "";
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            Envpath = @"C:\";
            if (!IsPostBack)
            {
                driveGrid();
                bindGrid(Envpath);
            }
        }

        public void bindGrid(string path)
        {
            string[] colNames = { "Name", "Action", "Path" };
            if (dt.Columns.Count < 1)
                dt.Columns.Add(colNames[]); 
            try{
                string[] allFolders = Directory.GetDirectories(path);            
                string[] allFiles = Directory.GetFiles(path);            
                if (allFolders.Length < 1 && allFiles.Length < 1)
                {
                    Response.Write("No content found in this folder!");
                    bindGrid(Directory.GetParent(path).ToString());   //Recursion for no content by changing path...
                }
                //fetch folder
                foreach (string i in allFolders)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = Path.GetFileName(Path.GetDirectoryName(i + "\\"));
                    dr["Action"] = "Upload";
                    dr["Path"] = i;
                    dt.Rows.Add(dr);
                }
                //fetch file
                foreach (string i in allFiles)
                {
                    DataRow dr = dt.NewRow();                
                    dr["Name"] = Path.GetFileNameWithoutExtension(i);
                    dr["Action"] = "Download";
                    dr["Path"] = i;                
                    dt.Rows.Add(dr);
                }
                GridView1.DataSource = dt;            
                GridView1.DataBind();  
            }
            catch(Exception e) {
                Response.Write("Sorry! You have no access to view this folder. Or something just went wrong!!!");
            }
        }

        public void driveGrid()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Name");
            foreach (DriveInfo d in allDrives)
            {
                DataRow dr = dt2.NewRow();
                dr["Name"] = d.Name;
                dt2.Rows.Add(dr);
            }
            GridView2.DataSource = dt2;
            GridView2.DataBind();
        }       

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TableCell openCell = e.Row.Cells[0];
            if(e.Row.Cells[2].Text == "Download")
                openCell.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.Rows[0];
            string currentpath = row.Cells[3].Text;
            string backpath = Directory.GetParent(currentpath).ToString();
            backpath = (backpath != Envpath) ? Directory.GetParent(backpath).ToString() : backpath;
            bindGrid(backpath);
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.NewSelectedIndex];
            string nextPath = row.Cells[3].Text;
            bindGrid(nextPath);
        }
    }
}