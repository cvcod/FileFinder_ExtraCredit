using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Direc.Text = String.Empty;
                Extension.Text = String.Empty; ;
                Search.Text = String.Empty;
                lbFiles.Items.Clear();
            }
            catch(Exception ex)
            {
                Logger(ex.ToString(), DateTime.Now);
            }

        }



            private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Logger("Search Started", DateTime.Now);

                string path = Direc.Text; 
                string ext = Extension.Text;
                string searchtxt = Search.Text;

                FindFiles(path, ext, searchtxt);
                Subdirectories(path, ext, searchtxt);

                Logger("Search Ended", DateTime.Now);

            }
            catch (Exception ex)
            {
                Logger(ex.ToString(), DateTime.Now);
            }


        }



        private bool searchFile(string fileName, string searchString)
        {
            try
            {
                FileStream fstream = new FileStream(fileName, FileMode.Open);
                StreamReader stread = new StreamReader(fstream);
                string line;

                line = stread.ReadLine();

                while (line != null)
                {
                    if (line.ToLower().Contains(searchString.ToLower()))
                    {
                        stread.Close();
                        return true;
                    }

                    line = stread.ReadLine();
                }

                stread.Close();
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }




        private void FindFiles(string path, string ext, string searchtxt)
        {
            try
            {
                string[] files = Directory.GetFiles(path, "*" + ext);

                foreach (string file in files)
                {
                    if (searchFile(file, searchtxt))
                    { lbFiles.Items.Add(file); }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }




       private void Subdirectories(string path, string ext, string searchtxt)
        {
            try
            {
                string[] direcs = Directory.GetDirectories(path);

                foreach (string dir in direcs)
                {
                    FindFiles(dir, ext, searchtxt);
                    Subdirectories(dir, ext, searchtxt);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
       }




        private void Logger(string proc, DateTime dt)
        {
            try
            {
                FileStream fs = new FileStream("../../log/log.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine($"{dt}: {proc}");
                sw.Close();
            }
            catch( Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
