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
        ModClass[] Mods;
        string NorthStarModDirectory = "D:/origin/Titanfall2/R2Northstar/mods/";
        string ProgramLocation = "D:/origin/Titanfall2/Norhtstarmodbrowser/";
        string TempLocation;
        public Form1()
        {
            if (!System.IO.Directory.Exists(ProgramLocation)) System.IO.Directory.CreateDirectory(ProgramLocation);
            InitializeComponent();
            TempLocation = Path.GetTempPath();
            DownloadList();
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
            openFileDialog2.ShowDialog();
            if (File.Exists(openFileDialog2.FileName))ZipFile.ExtractToDirectory(openFileDialog2.FileName, NorthStarModDirectory);
        }
        private void createList()
        {
            int count = 0;
            IEnumerable<string> lines = File.ReadLines(ProgramLocation + "ModList.txt");
            Mods = new ModClass[lines.Count()];
            for (int i = 0; i < lines.Count(); i++)Mods[i] = new ModClass();
            foreach (string s in lines)
            {
                //read the file and set the mod
                string[] splits = s.Split(';');
                Mods[count].Name = splits[0];
                Mods[count].Owner = splits[1];
                Mods[count].Link = splits[2];
                Mods[count].ArrayId = count;
                Mods[count].Id = long.Parse(splits[3]);
                Mods[count].Mode = int.Parse(splits[4]);
                Mods[count].NewestVersion = getCommitCount(Mods[count].Id);
                Mods[count].x = 10;
                Mods[count].y = 100 + count * 20;
                // create the newest version label
                 System.Windows.Forms.Label text = new System.Windows.Forms.Label();
                text.Text = Mods[count].NewestVersion.ToString();
                text.Size = new Size(20, 20);
                text.Location = new Point(110, 100 + count * 20);
                text.BackColor = Color.FromArgb(0, 0, 0, 0);
                text.ForeColor = Color.White;
                Mods[count].newestVersionLabel = text;
                Controls.Add(text);
                
                Button but = new Button();
                but.Text =Mods[count].Name;
                but.Name = "i"+count.ToString();
                but.Size = new Size(100, 20);
                but.Location = new Point(10, 100 + count * 20);
                but.Click += new EventHandler(Button_Click);
                Controls.Add(but);
                Mods[count].button = but;
                count++;
            }
           foreach (string filepath in System.IO.Directory.GetFiles(NorthStarModDirectory,"nmodinfo.txt",SearchOption.AllDirectories))
            {
                string nameOfTheMod  = File.ReadAllLines(filepath)[0];
                int CurrentVersion = int.Parse (File.ReadAllLines(filepath)[1]);
               foreach(ModClass modToCheck in Mods)
                {
                    if (modToCheck.Name==nameOfTheMod)
                    {
                        modToCheck.CurrentVersion = CurrentVersion; //set the version of the mod 
                        //make a label showing the version of the installed mod
                        System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                        lbl.Text = CurrentVersion.ToString();
                        lbl.BackColor = Color.Transparent;
                        lbl.ForeColor = Color.White;
                        lbl.Size = new Size(20, 20);
                        lbl.Location = new Point(modToCheck.x + 120, modToCheck.y);
                        modToCheck.currentVersionLabel = lbl;
                        Controls.Add(lbl);

                        // make a delete button
                        Button butdlt = new Button();
                        butdlt.Name = "d" + modToCheck.ArrayId;
                        butdlt.Text = "Delete "+ nameOfTheMod;
                        butdlt.Size = new Size(100, 20);
                        butdlt.TextAlign = ContentAlignment.MiddleCenter;
                        butdlt.Location = new Point(modToCheck.x + 140, modToCheck.y);
                        butdlt.Click += Button_Click;
                        modToCheck.ButtonDelete = butdlt;
                        Controls.Add(butdlt);
                        //change the buttons color depending on the versions
                        if (CurrentVersion == modToCheck.NewestVersion) modToCheck.button.BackColor = Color.Green;
                        else modToCheck.button.BackColor = Color.Red;               
                    }
                }
            }
 

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            ModClass mod = Mods[int.Parse(but.Name.Substring(1))];
            if (but.Name[0] == 'i')  downloadAndInstallMod(mod);
            else RemoveMod(mod);
            
        }
       private void RemoveMod(ModClass modToRemove)
        {
            Directory.Delete(NorthStarModDirectory + modToRemove.Name,true);
            modToRemove.CurrentVersion = -1;
            modToRemove.currentVersionLabel.Text = "";
            Controls.Remove(modToRemove.ButtonDelete);
        }
        private  async void downloadAndInstallMod(ModClass modToInstall)
        {
            if(modToInstall.NewestVersion== modToInstall.CurrentVersion)
            {
              DialogResult dr =  System.Windows.Forms.MessageBox.Show("The currently installed version is up to date download and install the mod anyways", "Already Up To Date", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
            }
            // create a loading icon
            PictureBox pbloading = new PictureBox();
            pbloading.BackgroundImage = Properties.Resources.Triangles_indicator;
            pbloading.BackgroundImageLayout = ImageLayout.Stretch;
            pbloading.BackColor = Color.Transparent;

            pbloading.Location = new Point(modToInstall.x + 140, modToInstall.y);
            pbloading.Size = new Size(20 , 20);
            Controls.Add(pbloading);
            
            //find the link to the mods
            string url,name = modToInstall.Name;
            if (modToInstall.Mode == 0)url = getReleases(modToInstall.Id).Result.ZipballUrl;
            
            else url = modToInstall.Link;
          

            //download the mod
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Headers.Add("user-agent", "Anything");
            if (System.IO.Directory.Exists(ProgramLocation + name + ".zip")) System.IO.Directory.Delete(ProgramLocation + name + ".zip", true);
            await webClient.DownloadFileTaskAsync(new Uri(url), ProgramLocation + name + ".zip");
            
            //extract the mod
            if (System.IO.Directory.Exists(ProgramLocation + name)) System.IO.Directory.Delete(ProgramLocation + name, true);
            ZipFile.ExtractToDirectory(ProgramLocation + name + ".zip", ProgramLocation + name);

            //find the mod.json file and move that folder to mods folder
            string mainModFile = System.IO.Directory.GetFiles(ProgramLocation + name, "mod.json", SearchOption.AllDirectories)[0];
            mainModFile = mainModFile.Substring(0, mainModFile.Length - 8);
            if (System.IO.Directory.Exists(NorthStarModDirectory+name)) System.IO.Directory.Delete(NorthStarModDirectory+name,true);
            System.IO.Directory.Move(mainModFile, NorthStarModDirectory+name);
            File.WriteAllText(NorthStarModDirectory + name + "/nmodinfo.txt",modToInstall.Name+"\n"+ modToInstall.NewestVersion.ToString());

            //remove old files
            System.IO.File.Delete(ProgramLocation + name + ".zip");
            System.IO.Directory.Delete(ProgramLocation + name,true);

            // create a remove button if it doesnt exists already
           if( modToInstall.ButtonDelete== null)
            {
                // make a delete button
                Button butdlt = new Button();
                butdlt.Name = "d" + modToInstall.ArrayId;
                butdlt.Text = "Delete " + modToInstall;
                butdlt.Size = new Size(100, 20);
                butdlt.TextAlign = ContentAlignment.MiddleCenter;
                butdlt.Location = new Point(modToInstall.x + 140, modToInstall.y);
                butdlt.Click += Button_Click;
                modToInstall.ButtonDelete = butdlt;
                Controls.Add(butdlt);
            }
            modToInstall.CurrentVersion = modToInstall.NewestVersion;
            
            if(modToInstall.currentVersionLabel== null)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = modToInstall.CurrentVersion.ToString();
                lbl.BackColor = Color.Transparent;
                lbl.ForeColor = Color.White;
                lbl.Size = new Size(20, 20);
                lbl.Location = new Point(modToInstall.x + 120, modToInstall.y);
                modToInstall.currentVersionLabel = lbl;
                Controls.Add(lbl);
            }
            modToInstall.currentVersionLabel.Text = modToInstall.CurrentVersion.ToString();
            Controls.Remove(pbloading);
        }

    }
    class ModClass
    {
        public string Name = "";
        public string Owner = "";
        public long Id = 0,ArrayId;
        public string Link = "";
        public int Mode = 0;
        public int NewestVersion = 0,CurrentVersion;
        public int x = 0, y = 0;
        public Button button,ButtonDelete;
        public System.Windows.Forms.Label currentVersionLabel, newestVersionLabel;
    }

}


