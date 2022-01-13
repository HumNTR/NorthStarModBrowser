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
    class ModClass
    {
       public string Name;
        public string Owner;
        public long Id;
        public string Link;
        public int Mode;
    }
    public partial class Form1 : Form
    {
        ModClass[] Mods;
        string NorthStarModDirectory = "D:/origin/Titanfall2/R2Northstar/mods";
        string ProgramLocation= "D:/origin/Titanfall2/Norhtstarmodbrowser/";
        public Form1()
        {
            if (!System.IO.Directory.Exists(ProgramLocation)) System.IO.Directory.CreateDirectory(ProgramLocation);
            InitializeComponent();
            openFileDialog1.Filter = "NorthstarModFiles (*.nmod)|*.nmod";
            Console.WriteLine(ProgramLocation);
            DownloadList();
            createList();
        }
        private void DownloadList()
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Headers.Add("user-agent", "Anything");
                try
                {
                    wc.DownloadFile("https://raw.githubusercontent.com/HumNTR/NorthStarModBrowser/main/NorthStarModBrowser/Mods.txt", ProgramLocation + "ModList.txt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        //this part is taken from haakonfp
        public async Task<Release> getReleases(long gitId)
        {
            var client = new GitHubClient(new ProductHeaderValue("HumNTR"));

            // Retrieve a List of Releases in the Repository, and get latest using [0]-subscript
            var latest =   client.Repository.Release.GetAll(gitId).Result[0];
            
            return latest;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Console.WriteLine(openFileDialog1.FileName);
            ZipFile.ExtractToDirectory(openFileDialog1.FileName, NorthStarModDirectory);
        }
        private void createList()
        {
            int count=0;
           IEnumerable<string> lines = File.ReadLines(ProgramLocation + "ModList.txt");
            Mods = new ModClass[lines.Count()];
            foreach (string s in lines)
            {
               string[] splits= s.Split(';');
                Mods[count].Name = splits[0];
                Mods[count].Owner = splits[1];
                Mods[count].Link = splits[2];
                Mods[count].Id = long.Parse(splits[3]);
                Mods[count].Mode =int.Parse(splits[4]);
                count++;
            }
            for(int i =0;i< count;i++)
            {
                Button but = new Button();
                but.Text = Mods[i].Name;
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
            string url;
            if (Mods[int.Parse(but.Name)].Mode == 0) url = getReleases(Mods[int.Parse(but.Name)].Id).Result.ZipballUrl;
            else url = Mods[int.Parse(but.Name)].Link;
            downloadAndInstallMod(url);
        }
        private async void downloadAndInstallMod(string url)
        {

            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("user-agent", "Anything");
            Console.WriteLine(ProgramLocation + url.Substring(29).Replace('/', '-'));
            
            await webClient.DownloadFileTaskAsync(new Uri(url), ProgramLocation + url.Substring(29).Replace('/','-')+".zip");
            ZipFile.ExtractToDirectory(ProgramLocation + url.Substring(29).Replace('/', '-') + ".zip",NorthStarModDirectory);

        }

    }
}
