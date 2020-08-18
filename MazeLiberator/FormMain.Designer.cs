namespace MazeLiberator
{
    partial class FormMain
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveActualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easylToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.lblDebug = new System.Windows.Forms.Label();
            this.menuStripMainForm.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(12, 30);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 500);
            this.mainPanel.TabIndex = 0;
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.resolveToolStripMenuItem,
            this.difficultyToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(823, 24);
            this.menuStripMainForm.TabIndex = 1;
            this.menuStripMainForm.Text = "menuStripMainForm";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mazeToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newToolStripMenuItem.Text = "New";
            // 
            // mazeToolStripMenuItem
            // 
            this.mazeToolStripMenuItem.Name = "mazeToolStripMenuItem";
            this.mazeToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.mazeToolStripMenuItem.Text = "Maze";
            this.mazeToolStripMenuItem.Click += new System.EventHandler(this.LabyrinthToolStripMenuItem_Click);
            // 
            // resolveToolStripMenuItem
            // 
            this.resolveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solveActualToolStripMenuItem});
            this.resolveToolStripMenuItem.Name = "resolveToolStripMenuItem";
            this.resolveToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.resolveToolStripMenuItem.Text = "Resolve";
            // 
            // solveActualToolStripMenuItem
            // 
            this.solveActualToolStripMenuItem.Name = "solveActualToolStripMenuItem";
            this.solveActualToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.solveActualToolStripMenuItem.Text = "Solve current";
            this.solveActualToolStripMenuItem.Click += new System.EventHandler(this.SolveCurrentToolStripMenuItem_Click);
            // 
            // difficultyToolStripMenuItem
            // 
            this.difficultyToolStripMenuItem.Checked = true;
            this.difficultyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.difficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easylToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hardToolStripMenuItem});
            this.difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
            this.difficultyToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.difficultyToolStripMenuItem.Text = "Difficulty";
            // 
            // easylToolStripMenuItem
            // 
            this.easylToolStripMenuItem.Checked = true;
            this.easylToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easylToolStripMenuItem.Name = "easylToolStripMenuItem";
            this.easylToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.easylToolStripMenuItem.Text = "Easy";
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hardToolStripMenuItem.Text = "Hard";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // pnlDebug
            // 
            this.pnlDebug.Controls.Add(this.lblDebug);
            this.pnlDebug.Location = new System.Drawing.Point(518, 414);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(297, 116);
            this.pnlDebug.TabIndex = 2;
            this.pnlDebug.Visible = false;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(16, 18);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(45, 13);
            this.lblDebug.TabIndex = 0;
            this.lblDebug.Text = "Debug: ";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 539);
            this.Controls.Add(this.pnlDebug);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStripMainForm);
            this.MainMenuStrip = this.menuStripMainForm;
            this.Name = "FormMain";
            this.Text = "MazeLiberator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resolveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveActualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easylToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.Label lblDebug;
    }
}

