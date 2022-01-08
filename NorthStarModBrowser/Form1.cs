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
using System.Diagnostics;
using Octokit;

namespace NorthStarModBrowser
{
    public partial class Form1 : Form
    {

        string NorthStarModDirectory = "D:/origin/Titanfall2/R2Northstar/mods";
        string tempLocation;
        string[] names;
        string[] GitNames;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "NorthstarModFiles (*.nmod)|*.nmod";
            tempLocation = Path.GetTempPath()+"NorthStarModBrowser";
            Console.WriteLine(tempLocation);
            createList();
        }
        //this part is taken from haakonfp
        public async Task<Release> getReleases(string gitName)
        {
            String workspaceName = "WishaWoshi";
            String repositoryName = "Speed-Is-Life";

            var client = new GitHubClient(new ProductHeaderValue("HumNTR"));

            // Retrieve a List of Releases in the Repository, and get latest using [0]-subscript
            var latest =   client.Repository.Release.GetAll(445016104).Result[0];
            
            return latest;
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
            int count=0;
           IEnumerable<string> lines = File.ReadLines("C:/Users/HumN/source/repos/NorthStarModBrowser/Mods.txt");
            names = new string[lines.Count()];
            GitNames = new string[lines.Count()];
            foreach (string s in lines)
            {
               string[] splits= s.Split(';');
                names[count] = splits[0];
                GitNames[count] = splits[1];
                count++;
            }
            for(int i =0;i< count;i++)
            {
                Button but = new Button();
                but.Text = names[i];
                but.Name = i.ToString();
                but.Size = new Size(100, 20);
                but.Location = new Point(10, 100 + i * 20);
                but.Click += new EventHandler(Button_Click);
                Controls.Add(but);
            }


        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            string url =getReleases(GitNames[int.Parse(but.Name)]).Result.ZipballUrl;
            downloadAndInstallMod(url);
        }
        private async void downloadAndInstallMod(string url)
        {

            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("user-agent", "Anything");
            await webClient.DownloadFileTaskAsync(new Uri(url), tempLocation + "/tobeunzipped.zip") ;
            Console.WriteLine(tempLocation + "/tobeunzipped.zip");

        }
    }
}
