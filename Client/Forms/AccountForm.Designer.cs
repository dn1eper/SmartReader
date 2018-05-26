namespace Client.Forms
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
            System.Windows.Forms.Button cancelButton;
            System.Windows.Forms.Label label;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Button registerButton;
            this.groupButtonsBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.loginGroupBox = new System.Windows.Forms.GroupBox();
            cancelButton = new System.Windows.Forms.Button();
            label = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            registerButton = new System.Windows.Forms.Button();
            this.groupButtonsBox.SuspendLayout();
            this.loginGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupButtonsBox
            // 
            this.groupButtonsBox.Controls.Add(registerButton);
            this.groupButtonsBox.Controls.Add(this.okButton);
            this.groupButtonsBox.Controls.Add(cancelButton);
            this.groupButtonsBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupButtonsBox.Location = new System.Drawing.Point(0, 105);
            this.groupButtonsBox.Name = "groupButtonsBox";
            this.groupButtonsBox.Size = new System.Drawing.Size(297, 42);
            this.groupButtonsBox.TabIndex = 2;
            this.groupButtonsBox.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(135, 13);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Location = new System.Drawing.Point(216, 13);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            label.Location = new System.Drawing.Point(6, 13);
            label.Name = "label";
            label.Padding = new System.Windows.Forms.Padding(3);
            label.Size = new System.Drawing.Size(67, 30);
            label.TabIndex = 3;
            label.Text = "Name";
            label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.passwordTextBox.Location = new System.Drawing.Point(110, 63);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(160, 24);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.OnChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            label1.Location = new System.Drawing.Point(6, 57);
            label1.Name = "label1";
            label1.Padding = new System.Windows.Forms.Padding(3);
            label1.Size = new System.Drawing.Size(98, 30);
            label1.TabIndex = 5;
            label1.Text = "Password";
            label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.nameTextBox.Location = new System.Drawing.Point(110, 19);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(160, 24);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TextChanged += new System.EventHandler(this.OnChanged);
            // 
            // loginGroupBox
            // 
            this.loginGroupBox.Controls.Add(this.nameTextBox);
            this.loginGroupBox.Controls.Add(label);
            this.loginGroupBox.Controls.Add(label1);
            this.loginGroupBox.Controls.Add(this.passwordTextBox);
            this.loginGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginGroupBox.Location = new System.Drawing.Point(0, 0);
            this.loginGroupBox.Name = "loginGroupBox";
            this.loginGroupBox.Size = new System.Drawing.Size(297, 105);
            this.loginGroupBox.TabIndex = 6;
            this.loginGroupBox.TabStop = false;
            // 
            // registerButton
            // 
            registerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            registerButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            registerButton.Location = new System.Drawing.Point(54, 13);
            registerButton.Name = "registerButton";
            registerButton.Size = new System.Drawing.Size(75, 23);
            registerButton.TabIndex = 5;
            registerButton.TabStop = false;
            registerButton.Text = "Register";
            registerButton.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.ClientSize = new System.Drawing.Size(297, 147);
            this.Controls.Add(this.loginGroupBox);
            this.Controls.Add(this.groupButtonsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.ShowIcon = false;
            this.Text = "Account";
            this.groupButtonsBox.ResumeLayout(false);
            this.loginGroupBox.ResumeLayout(false);
            this.loginGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupButtonsBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.GroupBox loginGroupBox;
        private System.Windows.Forms.Button okButton;
    }
}