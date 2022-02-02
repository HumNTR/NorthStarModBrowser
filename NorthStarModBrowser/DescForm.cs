using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthStarModBrowser
{
    public partial class DescForm : Form
    {
        string gitHubLink;
        public DescForm(string Creator, string Name, string GithubLink,string Desctription)
        {
            InitializeComponent();
            this.Text = Name;
            OwnerLabel.Text ="Creator = "+ Creator;
            nameLabel.Text = "Mods Name = "+Name;
            gitHubLink =GithubLink;
            DescLabel.Text = "Description :"+ Desctription;
        }

        private void githubButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(gitHubLink);
        }
    }
}
