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
using System.IO.Compression;

namespace NorthStarModBrowser
{
    public partial class Form1 : Form
    {
       
        string NorthStarModDirectory = "D:/origin/Titanfall2/R2Northstar/mods";
        string tempLocation;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "NorthstarModFiles (*.nmod)|*.nmod";
            tempLocation = Path.GetTempPath()+"NorthStarModBrowser";
            Console.WriteLine(tempLocation);
            createList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Console.WriteLine(openFileDialog1.FileName);
            ZipFile.ExtractToDirectory(openFileDialog1.FileName, NorthStarModDirectory);
        }
        int ButtonCount=0;
        private void createList()
        {

        }
    }
}
