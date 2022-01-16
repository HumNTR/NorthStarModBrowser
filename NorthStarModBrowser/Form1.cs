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


/*
class ModClass
{
    public string Name = "";
    public string Owner = "";
    public long Id = 0;
    public string Link = "";
    public int Mode = 0;
    public int version = 0;
    public int x = 0, y = 0;
}
*/
namespace NorthStarModBrowser
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {

        }
        /*
        ModClass[] Mods;
        string NorthStarModDirectory = "D:/origin/Titanfall2/R2Northstar/mods/";
        string ProgramLocation = "D:/origin/Titanfall2/Norhtstarmodbrowser/";
        string TempLocation;
        public Form1()
        {
            if (!System.IO.Directory.Exists(ProgramLocation)) System.IO.Directory.CreateDirectory(ProgramLocation);
            InitializeComponent();
            openFileDialog1.Filter = "NorthstarModFiles (*.nmod)|*.nmod";
            Console.WriteLine(ProgramLocation);
            TempLocation = Path.GetTempPath();
            // DownloadList();
            createList();
        }
        private int getCommitCount(long id)
        {
            GitHubClient git = new GitHubClient(new ProductHeaderValue("a"));
            int version = git.Repository.Commit.GetAll(id).Result.Count;
            return version;
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
            var latest = client.Repository.Release.GetAll(gitId).Result[0];

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
            int count = 0;
            IEnumerable<string> lines = File.ReadLines(ProgramLocation + "ModList.txt");
            Mods = new ModClass[lines.Count()];
            for (int i = 0; i < lines.Count(); i++)Mods[i] = new ModClass();
            foreach (string s in lines)
            {
                string[] splits = s.Split(';');
                Mods[count].Name = splits[0];
                Mods[count].Owner = splits[1];
                Mods[count].Link = splits[2];

                Mods[count].Id = long.Parse(splits[3]);
                Mods[count].Mode = int.Parse(splits[4]);
                Mods[count].version = getCommitCount(Mods[count].Id);
                TextBox text = new TextBox();
                text.Text = Mods[count].version.ToString();
                text.Size = new Size(100, 20);
                text.Location = new Point(110, 100 + count * 20);
                Controls.Add(text);
                Button but = new Button();
                but.Text = Mods[count].Name;
                but.Name = count.ToString();
                but.Size = new Size(100, 20);
                but.Location = new Point(10, 100 + count * 20);
                but.Click += new EventHandler(Button_Click);
                Controls.Add(but);
                count++;
            }
            foreach (string filepath in System.IO.Directory.GetFiles(NorthStarModDirectory,"nmodinfo.txt",SearchOption.AllDirectories))
            {
                string nameOfTheMod = File.ReadAllLines(filepath)[0];
               foreach(ModClass modToCheck in Mods)
                {
                    if (modToCheck.Name==nameOfTheMod)
                    {

                    }
                }
            }
 

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            downloadAndInstallMod(Mods[ int.Parse(but.Name)]);
        }
        public void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
        private async void downloadAndInstallMod(ModClass modToInstall)
        {

            //find the link to the mods
            string url,name = modToInstall.Name;
            if (modToInstall.Mode == 0)url = getReleases(modToInstall.Id).Result.ZipballUrl;
            
            else url = modToInstall.Link;
          

            //download the mod
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("user-agent", "Anything");
            if (System.IO.Directory.Exists(ProgramLocation + name + ".zip")) System.IO.Directory.Delete(ProgramLocation + name + ".zip", true);
            await webClient.DownloadFileTaskAsync(new Uri(url), ProgramLocation + name + ".zip");
            int version = getCommitCount(modToInstall.Id);
            //extract the mod
            if (System.IO.Directory.Exists(ProgramLocation + name)) System.IO.Directory.Delete(ProgramLocation + name, true);
            ZipFile.ExtractToDirectory(ProgramLocation + name + ".zip", ProgramLocation + name);

            //find the mod.json file and move that folder to mods folder
            string mainModFile = System.IO.Directory.GetFiles(ProgramLocation + name, "mod.json", SearchOption.AllDirectories)[0];
            mainModFile = mainModFile.Substring(0, mainModFile.Length - 8);
            if (System.IO.Directory.Exists(NorthStarModDirectory+name)) System.IO.Directory.Delete(NorthStarModDirectory+name,true);
            System.IO.Directory.Move(mainModFile, NorthStarModDirectory+name);
            File.WriteAllText(NorthStarModDirectory + name + "/nmodinfo.txt",modToInstall.Name+"\n"+ version.ToString());

            //remove old files
            System.IO.File.Delete(ProgramLocation + name + ".zip");
            System.IO.Directory.Delete(ProgramLocation + name,true);

        }*/
    }
}


