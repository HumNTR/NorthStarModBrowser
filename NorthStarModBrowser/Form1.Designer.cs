
namespace NorthStarModBrowser
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.updatingbutton = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ProgramVersionLabel = new System.Windows.Forms.Label();
            this.NewestVersionLabel = new System.Windows.Forms.Label();
            this.githubNameTextBox = new System.Windows.Forms.TextBox();
            this.GithubLoginButton = new System.Windows.Forms.Button();
            this.panel1 = new NorthStarModBrowser.MyDisplay();
            this.SuspendLayout();
            // 
            // updatingbutton
            // 
            this.updatingbutton.BackColor = System.Drawing.Color.Transparent;
            this.updatingbutton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updatingbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updatingbutton.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.updatingbutton.ForeColor = System.Drawing.Color.White;
            this.updatingbutton.Location = new System.Drawing.Point(12, 62);
            this.updatingbutton.Name = "updatingbutton";
            this.updatingbutton.Size = new System.Drawing.Size(141, 23);
            this.updatingbutton.TabIndex = 5;
            this.updatingbutton.Text = "Update to the newest version";
            this.updatingbutton.UseVisualStyleBackColor = false;
            this.updatingbutton.Click += new System.EventHandler(this.updatingbutton_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "\"Zip files (*.zip)|*.zip";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ProgramVersionLabel
            // 
            this.ProgramVersionLabel.AutoSize = true;
            this.ProgramVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.ProgramVersionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProgramVersionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.ProgramVersionLabel.ForeColor = System.Drawing.Color.White;
            this.ProgramVersionLabel.Location = new System.Drawing.Point(12, 9);
            this.ProgramVersionLabel.Name = "ProgramVersionLabel";
            this.ProgramVersionLabel.Size = new System.Drawing.Size(154, 19);
            this.ProgramVersionLabel.TabIndex = 3;
            this.ProgramVersionLabel.Text = "Program Version = ";
            // 
            // NewestVersionLabel
            // 
            this.NewestVersionLabel.AutoSize = true;
            this.NewestVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.NewestVersionLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NewestVersionLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.NewestVersionLabel.ForeColor = System.Drawing.Color.White;
            this.NewestVersionLabel.Location = new System.Drawing.Point(12, 39);
            this.NewestVersionLabel.Name = "NewestVersionLabel";
            this.NewestVersionLabel.Size = new System.Drawing.Size(141, 19);
            this.NewestVersionLabel.TabIndex = 4;
            this.NewestVersionLabel.Text = "Newest Version =";
            // 
            // githubNameTextBox
            // 
            this.githubNameTextBox.AcceptsTab = true;
            this.githubNameTextBox.BackColor = System.Drawing.Color.Silver;
            this.githubNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.githubNameTextBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.githubNameTextBox.Location = new System.Drawing.Point(12, 91);
            this.githubNameTextBox.Name = "githubNameTextBox";
            this.githubNameTextBox.Size = new System.Drawing.Size(141, 20);
            this.githubNameTextBox.TabIndex = 6;
            this.githubNameTextBox.TabStop = false;
            this.githubNameTextBox.Text = "Github Token";
            this.githubNameTextBox.Enter += new System.EventHandler(this.githubNameTextBox_Enter);
            this.githubNameTextBox.Leave += new System.EventHandler(this.githubNameTextBox_Leave);
            // 
            // GithubLoginButton
            // 
            this.GithubLoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GithubLoginButton.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GithubLoginButton.ForeColor = System.Drawing.Color.White;
            this.GithubLoginButton.Location = new System.Drawing.Point(12, 117);
            this.GithubLoginButton.Name = "GithubLoginButton";
            this.GithubLoginButton.Size = new System.Drawing.Size(100, 23);
            this.GithubLoginButton.TabIndex = 8;
            this.GithubLoginButton.Text = "Login to GitHub";
            this.GithubLoginButton.UseVisualStyleBackColor = true;
            this.GithubLoginButton.Click += new System.EventHandler(this.GithubLoginButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 417);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 0);
            this.panel1.TabIndex = 11;
            this.panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panel1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(436, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GithubLoginButton);
            this.Controls.Add(this.githubNameTextBox);
            this.Controls.Add(this.updatingbutton);
            this.Controls.Add(this.NewestVersionLabel);
            this.Controls.Add(this.ProgramVersionLabel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NorthStar Mod Browser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label ProgramVersionLabel;
        private System.Windows.Forms.Label NewestVersionLabel;
        public System.Windows.Forms.Button updatingbutton;
        private System.Windows.Forms.TextBox githubNameTextBox;
        private System.Windows.Forms.Button GithubLoginButton;
        private MyDisplay panel1;
    }
}

