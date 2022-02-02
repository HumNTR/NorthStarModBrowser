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
using System.Threading;
using Microsoft.VisualBasic;

namespace NorthStarModBrowser
{
    
    public partial class Form1 : Form
    {
        ModClass[] Mods;
        string NorthStarModDirectory = "R2Northstar/mods/";
        string ProgramLocation = "Norhtstarmodbrowser/";
        string TitanfallDir = "";
        GitHubClient git;
        int currentVersion=0,newestVersion;
        string gitHubToken;
        public void useOpenFileDialog()
        {
            start:
            openFileDialog1.ShowDialog();
            if (Path.GetFileName(openFileDialog1.FileName) == "Titanfall2.exe")
            {
                TitanfallDir = Path.GetFileName(openFileDialog1.FileName);
                ProgramLocation = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName), ProgramLocation);
                NorthStarModDirectory = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName), NorthStarModDirectory);
            }
            else
            {
                MessageBox.Show("Selected File wasnt Titanfall2.exe");
                goto start;
            }
            string[] temp = { currentVersion.ToString(), Path.GetDirectoryName(openFileDialog1.FileName) };
            File.WriteAllLines("NorthStarModBrowser.config", temp);
        }
        public void ReadConfigFile()
        {
            if (!System.IO.Directory.Exists(NorthStarModDirectory))
            {
                if (File.Exists("NorthStarModBrowser.config"))
                {

                    string[] lines = File.ReadAllLines("NorthStarModBrowser.config");
                    if (lines.Count() < 2)
                    {
                        if (!int.TryParse(lines[0], out currentVersion))
                        {
                            MessageBox.Show("The Config file seems to be corrupt please use the updater to fix it", "Config is corrupt");
                            System.Environment.Exit(1);
                        }
                        useOpenFileDialog();

                    }
                    else
                    {
                        if (int.TryParse(lines[0], out currentVersion))
                        {
                            TitanfallDir = lines[1];
                            if (!File.Exists(Path.Combine(TitanfallDir, "Titanfall2.exe"))) useOpenFileDialog();
                        }
                        else
                        {
                            MessageBox.Show("The Config file seems to be corrupt please use the updater to fix it", "Config is corrupt");
                            System.Environment.Exit(1);
                        }
                    }

                    if (File.Exists(Path.Combine(TitanfallDir, "Titanfall2.exe")))
                    {

                        NorthStarModDirectory = Path.Combine(TitanfallDir, NorthStarModDirectory);
                        ProgramLocation = Path.Combine(TitanfallDir, ProgramLocation);
                    }

                }
                else
                {
                    MessageBox.Show("Cant find NorthStarModBrowser.config either copy it from the zip or use the updater to make it automatically(it will update to the latest version)", "Cant Find Config File");
                    System.Environment.Exit(1);
                }
            }
            else
            {
                if (File.Exists("NorthStarModBrowser.config"))
                {
                    string[] lines = File.ReadAllLines("NorthStarModBrowser.config");
                    if (lines.Count() < 2)
                    {
                        if (!int.TryParse(lines[0], out currentVersion))
                        {
                            MessageBox.Show("The Config file seems to be corrupt please use the updater to fix it", "Config is corrupt");
                            System.Environment.Exit(1);
                        }
                        TitanfallDir = "";
                    }
                    else
                    {
                        if (int.TryParse(lines[0], out currentVersion))
                        {
                            TitanfallDir = lines[1];
                            if (!File.Exists(Path.Combine(TitanfallDir, "Titanfall2.exe"))) TitanfallDir = "";
                        }
                        else
                        {
                            MessageBox.Show("The Config file seems to be corrupt please use the updater to fix it", "Config is corrupt");
                            System.Environment.Exit(1);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Cant find NorthStarModBrowser.config either copy it from the zip or use the updater to make it automatically(it will update to the latest version)", "Cant Find Config File");
                    System.Environment.Exit(1);
                }
            }


           
        }
        public void ReadGithubConfig()
        {
            string path = Path.Combine(ProgramLocation, "ModBrowserGithub.config");
            Console.WriteLine(path);
            if (File.Exists(Path.Combine(ProgramLocation,"ModBrowserGithub.config")))
            {
                string[] lines = File.ReadAllLines(Path.Combine(ProgramLocation, "ModBrowserGithub.config"));
                if(lines.Length>=1)
                {
                    gitHubToken = lines[0];
                    var basicAuth = new Credentials(gitHubToken); // NOTE: not real credentials
                    git.Credentials = basicAuth;
                    try
                    {
                        if(git.Miscellaneous.GetRateLimits().Result.Rate.Limit>60) githubNameTextBox.Text = "Logged in succesfully"; 

                    }
                    catch (Exception b)
                    {

                        if (b.InnerException.GetType().Name == "AuthorizationException") MessageBox.Show("Github token that was in the config file doesnt work");
                        File.Delete(Path.Combine(ProgramLocation, "ModBrowserGithub.config"));
                    }

                }
            }
             newestVersion = getCommitCount(445768377, 0);
            ProgramVersionLabel.Text = "Program Version = " + currentVersion.ToString();
            NewestVersionLabel.Text = "Newest Version = "+ newestVersion.ToString();
            if (currentVersion < newestVersion)
            {
                MessageBox.Show("There is a newer version available pls update using the updater", "Update Available");
                updatingbutton.Enabled=true;
                updatingbutton.Visible = true;
            }
        }
        public Form1()
        {

            InitializeComponent();

            updatingbutton.Enabled = false;
            updatingbutton.Visible = false;
            //connect to git 
            System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
            git = new GitHubClient(new ProductHeaderValue("ModBrowser"));
            // get titanfalls location
            ReadConfigFile();
            ReadGithubConfig();
            if (!System.IO.Directory.Exists(ProgramLocation)) System.IO.Directory.CreateDirectory(ProgramLocation);


           // DownloadList();
            createList();
            //make horizontal scrolbarr go away
            panel1.AutoScroll = false;
            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
            panel1.HorizontalScroll.Maximum = 0;
            panel1.VerticalScroll.Enabled = false;
            panel1.VerticalScroll.Visible = false;
            panel1.VerticalScroll.Maximum = 0;
            panel1.AutoScroll = true;
            //set function to be called when size changed so the max height can be changed
            SizeChanged += sizeChanged;
            sizeChanged(null,null);
        }
        bool shouldUpdate=false;
        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (shouldUpdate)
            {
                var process = new ProcessStartInfo("NorthstarModBrowserUpdater.exe");
                process.WorkingDirectory= System.IO.Directory.GetCurrentDirectory();
                Process.Start(process);
            }
        }
      
        public void sizeChanged(object sender, System.EventArgs e)
        {
            panel1.MaximumSize = new Size(1000, Height/3);
        }
        private int getCommitCount(long id,int mod)
        {
            start:
            int version=0;
            try
            {
                if (mod == 1) version = git.Repository.Commit.GetAll(id).Result.Count;
                else if (mod == 0) version = git.Repository.Release.GetAll(id).Result.Count;
            }
            catch (Exception ex)
            {
                RateLimit r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;
                if (r.Remaining == 0)
                {
                    retry:
                    string gitToken = Interaction.InputBox("The api limit for github has been reached and will be reset at " + r.Reset.LocalDateTime + "\n but you can bypass it by logging with a github token ", "Api limit Reached", "Token");
                    git.Credentials = new Credentials(gitToken);
                    try
                    {
                        r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;

                    }
                    catch (Exception b)
                    {
                        if (b.InnerException.GetType().Name == "AuthorizationException")
                        {
                            if (MessageBox.Show("Token you entered is wrong please try again. \n Do you want to exit the program ?", "Cant Log In", MessageBoxButtons.YesNo) == DialogResult.Yes) System.Environment.Exit(1);
                            else goto retry;

                        }
                    }
                    if (r.Limit > 60)
                    {
                        githubNameTextBox.Text = "Logged in succesfully";
                        if (MessageBox.Show("Logged into github succesfully. Do you want the login info to be saved ?", "Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            File.WriteAllText(Path.Combine(ProgramLocation, "ModBrowserGithub.config"), gitToken);
                        }
                        goto start;
                    }
                }


            }

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
                        RateLimit r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;
                        MessageBox.Show("The api limit for github has been reached and will be reset at " + r.Reset, "Api limit has been reached");
                    
                }
            }
        }
        //this part is taken from haakonfp
        public async Task<Release> getReleases(long gitId)
        {
            start:
            try
            {
                // Retrieve a List of Releases in the Repository, and get latest using [0]-subscript
                var latest = git.Repository.Release.GetAll(gitId).Result[0];
                return latest;
            }
            catch(Exception ex)
            {
                RateLimit r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;
                if (r.Remaining == 0)
                {
                    retry:
                    string gitToken = Interaction.InputBox("The api limit for github has been reached and will be reset at " + r.Reset.LocalDateTime + "\n but you can bypass it by logging with a github token ", "Api limit Reached", "Token");
                    git.Credentials = new Credentials(gitToken);
                    try
                    {
                        r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;

                    }
                    catch (Exception b)
                    {
                        if (b.InnerException.GetType().Name == "AuthorizationException")
                        {
                            if (MessageBox.Show("Token you entered is wrong please try again. \n Do you want to exit the program ?", "Cant Log In", MessageBoxButtons.YesNo) == DialogResult.Yes) System.Environment.Exit(1);
                            else goto retry;

                        }
                    }
                    if (r.Limit > 60)
                    {
                        githubNameTextBox.Text = "Logged in succesfully";
                        if (MessageBox.Show("Logged into github succesfully. Do you want the login info to be saved ?", "Success", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            File.WriteAllText(Path.Combine(ProgramLocation, "ModBrowserGithub.config"), gitToken);
                        }
                        goto start;
                    }
                }


            }
            return null;
            
        }
        private void openDescriptionForm(object sender, EventArgs e)
        {
            ModClass mod = Mods[int.Parse(((Button)sender).Name)];

            if (mod.descForm != null && !mod.descForm.IsDisposed ) {
                    mod.descForm.Focus();
             }
            else
            {
                mod.descForm = new DescForm(mod.Owner, mod.Name, mod.Link, mod.desc);
                mod.descForm.Show();
            }
            
          
     }
        private void createList()
        {
            int count = 0;
            IEnumerable<string> lines = File.ReadAllText(ProgramLocation + "ModList.txt").Split('*');
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
                Mods[count].desc = splits[5];
                Mods[count].NewestVersion = getCommitCount(Mods[count].Id,Mods[count].Mode);
                Mods[count].x = 10;
                Mods[count].y = count * 40;
                //create info button 
                Button infoBut = new Button();
                infoBut.Name = count.ToString();
                infoBut.BackgroundImage = Properties.Resources.info;
                infoBut.BackgroundImageLayout = ImageLayout.Stretch;
                infoBut.BackColor = Color.Transparent;
                infoBut.Location= new Point(10, Mods[count].y);
                infoBut.Size = new Size(30, 30);
                Mods[count].ButInfo = infoBut;
                infoBut.Click += openDescriptionForm;
                panel1.Controls.Add(infoBut);
                //create install button
                Button but = new Button();
                but.Text =Mods[count].Name;
                but.Name = "i"+count.ToString();
                but.Size = new Size(100, 20);
                but.Click += new EventHandler(Button_Click);
                but.Location = new Point(infoBut.Bounds.Right+10, Mods[count].y);
                but.FlatStyle = FlatStyle.Flat;
                but.Size = new Size(150, 30);
                Mods[count].button = but;
                but.BackColor = Color.FromArgb(100, Color.White);
                panel1.Controls.Add(but);
                // create the newest version label
                System.Windows.Forms.Label text = new System.Windows.Forms.Label();
                text.Text = Mods[count].NewestVersion.ToString();
                text.Size = new Size(20, 20);
                text.Location = new Point(Mods[count].button.Bounds.Right + 10, Mods[count].y);
                text.BackColor = Color.FromArgb(0, 0, 0, 0);
                text.ForeColor = Color.White;
                Mods[count].newestVersionLabel = text;
                panel1.Controls.Add(text);

                //make a label showing the version that is installed but disable it cause mod may not be installed. will be enabled later if the mod is installed
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.BackColor = Color.Transparent;
                lbl.ForeColor = Color.White;
                lbl.Size = new Size(20, 20);
                lbl.Location = new Point(Mods[count].newestVersionLabel.Bounds.Right + 10, Mods[count].button.Bounds.Y);
                Mods[count].currentVersionLabel = lbl;
                lbl.Visible = false;
                panel1.Controls.Add(lbl);

                count++;
            }
            //set up for the mods that are installed
           foreach (string filepath in System.IO.Directory.GetFiles(NorthStarModDirectory,"nmodinfo.txt",SearchOption.AllDirectories))
            {
                string nameOfTheMod  = File.ReadAllLines(filepath)[0];
                int CurrentVersion = int.Parse (File.ReadAllLines(filepath)[1]);
               foreach(ModClass modToCheck in Mods)
                {
                    if (modToCheck.Name==nameOfTheMod)
                    {
                        modToCheck.CurrentVersion = CurrentVersion; //set the version of the mod 
                        modToCheck.currentVersionLabel.Text = CurrentVersion.ToString();
                        modToCheck.currentVersionLabel.Visible = true;


                        // make a delete button
                        Button butdlt = new Button();
                        butdlt.Name = "d" + modToCheck.ArrayId;
                        butdlt.Text = "Delete";
                        butdlt.TextAlign = ContentAlignment.MiddleCenter;
                        butdlt.Size = new Size(100, 30);
                        butdlt.TextAlign = ContentAlignment.MiddleCenter;
                        butdlt.Location = new Point(modToCheck.currentVersionLabel.Bounds.Right + 10, modToCheck.button.Bounds.Y);
                        butdlt.Click += Button_Click;
                        butdlt.FlatStyle = FlatStyle.Flat;
                        butdlt.BackColor = Color.FromArgb(100, Color.Red);
                        modToCheck.ButtonDelete = butdlt;
                        panel1.Controls.Add(butdlt);
                        //change the buttons color depending on the versions
                        if (CurrentVersion == modToCheck.NewestVersion) modToCheck.button.BackColor = Color.FromArgb(100,Color.Green);
                        else modToCheck.button.BackColor = Color.FromArgb(100,Color.Red);               
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
            modToRemove.currentVersionLabel.Visible = false;
            panel1.Controls.Remove(modToRemove.ButtonDelete);
            modToRemove.button.BackColor = Color.FromArgb(100,Color.White);
            
        }

        private void updatingbutton_Click(object sender, EventArgs e)
        {
            shouldUpdate = true;
            System.Windows.Forms.Application.Exit();
        }

        private void GithubLoginButton_Click(object sender, EventArgs e)
        {
            gitHubToken = githubNameTextBox.Text;
            if(gitHubToken== "")
            {
                MessageBox.Show("Token was empty", "Empty");
                return;
            }
            var basicAuth = new Credentials(gitHubToken); // NOTE: not real credentials
            git.Credentials = basicAuth;
            RateLimit r= new RateLimit();
            try
            {
              r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;
               
            }
            catch (Exception b)
            {

                if (b.InnerException.GetType().Name == "AuthorizationException") MessageBox.Show("Token you entered is wrong please try again","Cant Log In");
            }
            if (r.Limit>60)
            {
                if(MessageBox.Show("Logged into github succesfully. Do you want the login info to be saved ?", "Success",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    File.WriteAllText(Path.Combine(ProgramLocation, "ModBrowserGithub.config"), gitHubToken);
                }
                githubNameTextBox.Text = "Logged in succesfully";
            }
        }
        private void panel1_Scroll(object sender, MouseEventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.Empty;
            panel1.BackColor = System.Drawing.Color.Transparent;
        }

        private void githubNameTextBox_Enter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == "Github Token" || t.Text == "Logged in succesfully")
            {
                t.Text = "";
            }
        }

        private void githubNameTextBox_Leave(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text=="")
            {
                RateLimit r = new RateLimit();
                try
                {
                    r = git.Miscellaneous.GetRateLimits().Result.Resources.Core;

                }
                catch (Exception b)
                {

                    if (b.InnerException.GetType().Name == "AuthorizationException") MessageBox.Show("Token you entered is wrong please try again", "Cant Log In");
                }
                if (r.Limit > 60)
                {
                    t.Text = "Logged in succesfully";
                }
                else
                {
                    t.Text = "Github Token";
                }
            }
        }

        private async void downloadAndInstallMod(ModClass modToInstall)
        {
            if(modToInstall.NewestVersion== modToInstall.CurrentVersion)
            {
              DialogResult dr =  System.Windows.Forms.MessageBox.Show("The currently installed version is up to date download and install the mod anyways", "Already Up To Date", MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) return;
            }
            // create a loading icon
            PictureBox pbloading = new PictureBox();
            pbloading.Image = Properties.Resources.Triangles_indicator;
            pbloading.SizeMode = PictureBoxSizeMode.StretchImage;
            pbloading.BackColor = Color.Transparent;

            pbloading.Location = new Point(modToInstall.currentVersionLabel.Bounds.X, modToInstall.button.Bounds.Y);
            pbloading.Size = new Size(20 , 20);
            panel1.Controls.Add(pbloading);
            //make current version invisible so loading icon will be visible
            modToInstall.currentVersionLabel.Visible = false;

            //find the link to the mods
            string url,name = modToInstall.Name;
            if (modToInstall.Mode == 0) url = getReleases(modToInstall.Id).Result.ZipballUrl;

            else url = "https://api.github.com/repos/" + modToInstall.Owner + "/" + modToInstall.Name + "/zipball";


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
           if( modToInstall.CurrentVersion==-1)
            {
                if (modToInstall.ButtonDelete == null)
                {
                    // make a delete button
                    Button butdlt = new Button();
                    butdlt.Name = "d" + modToInstall.ArrayId;
                    butdlt.Text = "Delete";
                    butdlt.Size = new Size(100, 30);
                    butdlt.TextAlign = ContentAlignment.MiddleCenter;
                    butdlt.Location = new Point(modToInstall.currentVersionLabel.Bounds.Right + 10, modToInstall.button.Bounds.Y);
                    butdlt.Click += Button_Click;
                    butdlt.FlatStyle = FlatStyle.Flat;
                    butdlt.BackColor = Color.FromArgb(100, Color.Red);
                    modToInstall.ButtonDelete = butdlt;
                    panel1.Controls.Add(butdlt);
                }
                else
                {
                    modToInstall.ButtonDelete.Location= new Point(modToInstall.currentVersionLabel.Bounds.Right + 10, modToInstall.button.Bounds.Y);
                    panel1.Controls.Add(modToInstall.ButtonDelete);
                }
            }
            modToInstall.CurrentVersion = modToInstall.NewestVersion;
           //create or re enable current version label
            if(modToInstall.currentVersionLabel== null)
            {
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                lbl.Text = modToInstall.CurrentVersion.ToString();
                lbl.BackColor = Color.Transparent;
                lbl.ForeColor = Color.White;
                lbl.Size = new Size(20, 20);
                lbl.Location = new Point(modToInstall.newestVersionLabel.Bounds.Right+10, modToInstall.button.Bounds.Y);
                modToInstall.currentVersionLabel = lbl;
                panel1.Controls.Add(lbl);
            }
            modToInstall.currentVersionLabel.Location = new Point(modToInstall.newestVersionLabel.Bounds.Right+10, modToInstall.button.Bounds.Y);
            modToInstall.currentVersionLabel.Text = modToInstall.CurrentVersion.ToString();
            modToInstall.currentVersionLabel.Visible = true;
            panel1.Controls.Remove(pbloading);

            //change the button color to green to show that the mod is up to date
            modToInstall.button.BackColor = Color.FromArgb(100,Color.Green);
            //make current version visible
            modToInstall.currentVersionLabel.Visible = true;
        }
    }

    class ModClass
    {
        public string Name = "";
        public string Owner = "";
        public string desc="";
        public long Id = 0,ArrayId;
        public string Link = "";
        public int Mode = 0;
        public int NewestVersion = 0,CurrentVersion=-1;
        public int x = 0, y = 0;
        public Button button,ButtonDelete,ButInfo;
        public System.Windows.Forms.Label currentVersionLabel, newestVersionLabel;
        public DescForm descForm;
    }
    public class MyDisplay : Panel
    {
        public MyDisplay()
        {
            this.DoubleBuffered = true;
        }
    }
}