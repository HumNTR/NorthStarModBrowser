
namespace NorthStarModBrowser
{
    partial class DescForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DescForm));
            this.nameLabel = new System.Windows.Forms.Label();
            this.OwnerLabel = new System.Windows.Forms.Label();
            this.DescLabel = new System.Windows.Forms.Label();
            this.githubButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(12, 71);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(87, 24);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name = ";
            // 
            // OwnerLabel
            // 
            this.OwnerLabel.AutoSize = true;
            this.OwnerLabel.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.OwnerLabel.ForeColor = System.Drawing.Color.White;
            this.OwnerLabel.Location = new System.Drawing.Point(12, 95);
            this.OwnerLabel.Name = "OwnerLabel";
            this.OwnerLabel.Size = new System.Drawing.Size(96, 24);
            this.OwnerLabel.TabIndex = 1;
            this.OwnerLabel.Text = "Owner = ";
            // 
            // DescLabel
            // 
            this.DescLabel.AutoSize = true;
            this.DescLabel.CausesValidation = false;
            this.DescLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.DescLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DescLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.DescLabel.ForeColor = System.Drawing.Color.White;
            this.DescLabel.Location = new System.Drawing.Point(12, 134);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(371, 19);
            this.DescLabel.TabIndex = 2;
            this.DescLabel.Text = "Description = (There is currently no description)";
            // 
            // githubButton
            // 
            this.githubButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.githubButton.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.githubButton.ForeColor = System.Drawing.Color.White;
            this.githubButton.Location = new System.Drawing.Point(16, 9);
            this.githubButton.Name = "githubButton";
            this.githubButton.Size = new System.Drawing.Size(122, 59);
            this.githubButton.TabIndex = 3;
            this.githubButton.Text = "Open Mods Github page";
            this.githubButton.UseVisualStyleBackColor = true;
            this.githubButton.Click += new System.EventHandler(this.githubButton_Click);
            // 
            // DescForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(397, 202);
            this.Controls.Add(this.githubButton);
            this.Controls.Add(this.DescLabel);
            this.Controls.Add(this.OwnerLabel);
            this.Controls.Add(this.nameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.MinimumSize = new System.Drawing.Size(10, 10);
            this.Name = "DescForm";
            this.Text = "Temp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label OwnerLabel;
        private System.Windows.Forms.Label DescLabel;
        private System.Windows.Forms.Button githubButton;
    }
}