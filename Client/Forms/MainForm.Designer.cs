namespace Client.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem openMenuItem;
            System.Windows.Forms.ToolStripMenuItem eixitMenuItem;
            System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
            System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
            System.Windows.Forms.ToolStripMenuItem libraryMenuItem;
            System.Windows.Forms.ToolStripMenuItem accountMenuItem;
            this.bookmarksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBookmarkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.navigationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousPageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.previousPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eixitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            libraryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            accountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.bookContextMenu.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openMenuItem
            // 
            openMenuItem.Name = "openMenuItem";
            openMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            openMenuItem.Size = new System.Drawing.Size(146, 22);
            openMenuItem.Text = "&Open";
            openMenuItem.Click += new System.EventHandler(this.OnFileOpen);
            // 
            // eixitMenuItem
            // 
            eixitMenuItem.Name = "eixitMenuItem";
            eixitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            eixitMenuItem.Size = new System.Drawing.Size(146, 22);
            eixitMenuItem.Text = "&Eixit";
            eixitMenuItem.Click += new System.EventHandler(this.OnExit);
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new System.Drawing.Size(107, 22);
            aboutMenuItem.Text = "&About";
            aboutMenuItem.Click += new System.EventHandler(this.OnAboutDialog);
            // 
            // settingsMenuItem
            // 
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new System.Drawing.Size(146, 22);
            settingsMenuItem.Text = "&Settings";
            // 
            // libraryMenuItem
            // 
            libraryMenuItem.Name = "libraryMenuItem";
            libraryMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            libraryMenuItem.Size = new System.Drawing.Size(194, 22);
            libraryMenuItem.Text = "&Library";
            // 
            // accountMenuItem
            // 
            accountMenuItem.Name = "accountMenuItem";
            accountMenuItem.Size = new System.Drawing.Size(146, 22);
            accountMenuItem.Text = "&Account";
            // 
            // bookmarksMenuItem
            // 
            this.bookmarksMenuItem.Enabled = false;
            this.bookmarksMenuItem.Name = "bookmarksMenuItem";
            this.bookmarksMenuItem.Size = new System.Drawing.Size(194, 22);
            this.bookmarksMenuItem.Text = "&Bookmarks";
            // 
            // addBookmarkMenuItem
            // 
            this.addBookmarkMenuItem.Enabled = false;
            this.addBookmarkMenuItem.Name = "addBookmarkMenuItem";
            this.addBookmarkMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.addBookmarkMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addBookmarkMenuItem.Text = "&Add bookmark";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.libraryToolStripMenuItem,
            this.navigationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            openMenuItem,
            accountMenuItem,
            settingsMenuItem,
            eixitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // libraryToolStripMenuItem
            // 
            this.libraryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            libraryMenuItem,
            this.bookmarksMenuItem,
            this.addBookmarkMenuItem});
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.libraryToolStripMenuItem.Text = "&View";
            // 
            // navigationToolStripMenuItem
            // 
            this.navigationToolStripMenuItem.DropDown = this.bookContextMenu;
            this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
            this.navigationToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.navigationToolStripMenuItem.Text = "&Navigation";
            // 
            // bookContextMenu
            // 
            this.bookContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextPageMenuItem,
            this.previousPageMenuItem});
            this.bookContextMenu.Name = "bookContextMenu";
            this.bookContextMenu.OwnerItem = this.navigationToolStripMenuItem;
            this.bookContextMenu.Size = new System.Drawing.Size(149, 48);
            // 
            // nextPageMenuItem
            // 
            this.nextPageMenuItem.Enabled = false;
            this.nextPageMenuItem.Name = "nextPageMenuItem";
            this.nextPageMenuItem.Size = new System.Drawing.Size(148, 22);
            this.nextPageMenuItem.Text = "&Next page";
            this.nextPageMenuItem.Click += new System.EventHandler(this.OnNextPage);
            // 
            // previousPageMenuItem
            // 
            this.previousPageMenuItem.Enabled = false;
            this.previousPageMenuItem.Name = "previousPageMenuItem";
            this.previousPageMenuItem.Size = new System.Drawing.Size(148, 22);
            this.previousPageMenuItem.Text = "&Previous page";
            this.previousPageMenuItem.Click += new System.EventHandler(this.OnPreviousPage);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            aboutMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Text files (*.txt)|*.txt";
            this.openFileDialog.Title = "Открыть книгу";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 659);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(43, 17);
            this.statusLabel.Text = "Offline";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // richTextBox
            // 
            this.richTextBox.ContextMenuStrip = this.bookContextMenu;
            this.richTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Font = new System.Drawing.Font("Arial", 14F);
            this.richTextBox.Location = new System.Drawing.Point(0, 24);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox.Size = new System.Drawing.Size(1264, 635);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            this.richTextBox.SizeChanged += new System.EventHandler(this.OnResize);
            // 
            // previousPageToolStripMenuItem
            // 
            this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
            this.previousPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.previousPageToolStripMenuItem.Text = "&Previous page";
            this.previousPageToolStripMenuItem.Click += new System.EventHandler(this.OnPreviousPage);
            // 
            // nextPageToolStripMenuItem
            // 
            this.nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
            this.nextPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.nextPageToolStripMenuItem.Text = "&Next page";
            this.nextPageToolStripMenuItem.Click += new System.EventHandler(this.OnNextPage);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Text = "&Previous page";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.OnPreviousPage);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "MainForm";
            this.Text = "SmartReader";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.bookContextMenu.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem navigationToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ToolStripMenuItem previousPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem nextPageMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousPageMenuItem;
        private System.Windows.Forms.ContextMenuStrip bookContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addBookmarkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookmarksMenuItem;
    }
}

