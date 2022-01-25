
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
            this.updatingbutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ProgramVersionLabel = new System.Windows.Forms.Label();
            this.NewestVersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updatingbutton
            // 
            this.updatingbutton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.updatingbutton.Location = new System.Drawing.Point(162, 38);
            this.updatingbutton.Name = "updatingbutton";
            this.updatingbutton.Size = new System.Drawing.Size(120, 23);
            this.updatingbutton.TabIndex = 5;
            this.updatingbutton.Text = "Update to the newest version";
            this.updatingbutton.UseVisualStyleBackColor = true;
            this.updatingbutton.Click += new System.EventHandler(this.updatingbutton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.button1.ForeColor = System.Drawing.Color.Coral;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Install From Zip File";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(0, 10);
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel1.Location = new System.Drawing.Point(12, 353);
            this.panel1.MaximumSize = new System.Drawing.Size(1000, 120);
            this.panel1.MinimumSize = new System.Drawing.Size(120, 30);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 7);
            this.panel1.Size = new System.Drawing.Size(120, 30);
            this.panel1.TabIndex = 2;
            // 
            // ProgramVersionLabel
            // 
            this.ProgramVersionLabel.AutoSize = true;
            this.ProgramVersionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(249)))), ((int)(((byte)(151)))));
            this.ProgramVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ProgramVersionLabel.Location = new System.Drawing.Point(12, 38);
            this.ProgramVersionLabel.Name = "ProgramVersionLabel";
            this.ProgramVersionLabel.Size = new System.Drawing.Size(144, 20);
            this.ProgramVersionLabel.TabIndex = 3;
            this.ProgramVersionLabel.Text = "Program Version = ";
            // 
            // NewestVersionLabel
            // 
            this.NewestVersionLabel.AutoSize = true;
            this.NewestVersionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(117)))), ((int)(((byte)(11)))));
            this.NewestVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.NewestVersionLabel.Location = new System.Drawing.Point(12, 67);
            this.NewestVersionLabel.Name = "NewestVersionLabel";
            this.NewestVersionLabel.Size = new System.Drawing.Size(133, 20);
            this.NewestVersionLabel.TabIndex = 4;
            this.NewestVersionLabel.Text = "Newest Version =";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::NorthStarModBrowser.Properties.Resources._728597;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.updatingbutton);
            this.Controls.Add(this.NewestVersionLabel);
            this.Controls.Add(this.ProgramVersionLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ProgramVersionLabel;
        private System.Windows.Forms.Label NewestVersionLabel;
        public System.Windows.Forms.Button updatingbutton;
    }
}

