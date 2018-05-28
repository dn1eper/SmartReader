namespace SmartReader.Client.Forms
{
    partial class AccountForm
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
            System.Windows.Forms.Button logoutButton;
            System.Windows.Forms.Button cancelButton;
            this.logoutGroupBox = new System.Windows.Forms.GroupBox();
            this.groupButtonsBox = new System.Windows.Forms.GroupBox();
            this.label = new System.Windows.Forms.Label();
            logoutButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            this.logoutGroupBox.SuspendLayout();
            this.groupButtonsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // logoutGroupBox
            // 
            this.logoutGroupBox.Controls.Add(this.label);
            this.logoutGroupBox.Controls.Add(logoutButton);
            this.logoutGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoutGroupBox.Location = new System.Drawing.Point(0, 0);
            this.logoutGroupBox.Name = "logoutGroupBox";
            this.logoutGroupBox.Size = new System.Drawing.Size(277, 136);
            this.logoutGroupBox.TabIndex = 7;
            this.logoutGroupBox.TabStop = false;
            // 
            // logoutButton
            // 
            logoutButton.DialogResult = System.Windows.Forms.DialogResult.No;
            logoutButton.Location = new System.Drawing.Point(95, 56);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new System.Drawing.Size(75, 23);
            logoutButton.TabIndex = 0;
            logoutButton.Text = "Logout";
            logoutButton.UseVisualStyleBackColor = true;
            // 
            // groupButtonsBox
            // 
            this.groupButtonsBox.Controls.Add(cancelButton);
            this.groupButtonsBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupButtonsBox.Location = new System.Drawing.Point(0, 94);
            this.groupButtonsBox.Name = "groupButtonsBox";
            this.groupButtonsBox.Size = new System.Drawing.Size(277, 42);
            this.groupButtonsBox.TabIndex = 8;
            this.groupButtonsBox.TabStop = false;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(196, 13);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label.Location = new System.Drawing.Point(83, 16);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(50, 18);
            this.label.TabIndex = 1;
            this.label.Text = "Hello, ";
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 136);
            this.Controls.Add(this.groupButtonsBox);
            this.Controls.Add(this.logoutGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.Text = "Account";
            this.logoutGroupBox.ResumeLayout(false);
            this.logoutGroupBox.PerformLayout();
            this.groupButtonsBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox logoutGroupBox;
        private System.Windows.Forms.GroupBox groupButtonsBox;
        private System.Windows.Forms.Label label;
    }
}