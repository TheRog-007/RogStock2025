namespace RogStock2025.Forms
{
    partial class frmMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.MNUMain = new System.Windows.Forms.MenuStrip();
            this.MNUItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockLot = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockItems = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockDescription = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockMedia = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockProductFamilies = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUStockVendors = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.adjustQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MNUMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(363, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MNUMain
            // 
            this.MNUMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUItems,
            this.MNUWindow});
            this.MNUMain.Location = new System.Drawing.Point(0, 0);
            this.MNUMain.Name = "MNUMain";
            this.MNUMain.Size = new System.Drawing.Size(800, 24);
            this.MNUMain.TabIndex = 2;
            this.MNUMain.Text = "menuStrip1";
            // 
            // MNUItems
            // 
            this.MNUItems.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MNUStockLocation,
            this.MNUStockLot,
            this.MNUStockItems,
            this.MNUStockDescription,
            this.MNUStockMedia,
            this.MNUStockProductFamilies,
            this.MNUStockVendors,
            this.adjustQuantityToolStripMenuItem});
            this.MNUItems.Name = "MNUItems";
            this.MNUItems.Size = new System.Drawing.Size(48, 20);
            this.MNUItems.Text = "Stock";
            // 
            // MNUStockLocation
            // 
            this.MNUStockLocation.Name = "MNUStockLocation";
            this.MNUStockLocation.Size = new System.Drawing.Size(224, 22);
            this.MNUStockLocation.Text = "Stock Location Maintenance";
            this.MNUStockLocation.Click += new System.EventHandler(this.MNUStockLocation_Click);
            // 
            // MNUStockLot
            // 
            this.MNUStockLot.Name = "MNUStockLot";
            this.MNUStockLot.Size = new System.Drawing.Size(224, 22);
            this.MNUStockLot.Text = "Stock Lot Maintenance";
            // 
            // MNUStockItems
            // 
            this.MNUStockItems.Name = "MNUStockItems";
            this.MNUStockItems.Size = new System.Drawing.Size(194, 22);
            this.MNUStockItems.Text = "Stock Items";
            this.MNUStockItems.Click += new System.EventHandler(this.MNUStockItems_Click);
            // 
            // MNUStockDescription
            // 
            this.MNUStockDescription.Name = "MNUStockDescription";
            this.MNUStockDescription.Size = new System.Drawing.Size(194, 22);
            this.MNUStockDescription.Text = "Stock Description";
            this.MNUStockDescription.Click += new System.EventHandler(this.MNUStockDescription_Click);
            // 
            // MNUStockMedia
            // 
            this.MNUStockMedia.Name = "MNUStockMedia";
            this.MNUStockMedia.Size = new System.Drawing.Size(194, 22);
            this.MNUStockMedia.Text = "Stock Media";
            // 
            // MNUStockProductFamilies
            // 
            this.MNUStockProductFamilies.Name = "MNUStockProductFamilies";
            this.MNUStockProductFamilies.Size = new System.Drawing.Size(194, 22);
            this.MNUStockProductFamilies.Text = "Stock Product Families";
            // 
            // MNUStockVendors
            // 
            this.MNUStockVendors.Name = "MNUStockVendors";
            this.MNUStockVendors.Size = new System.Drawing.Size(194, 22);
            this.MNUStockVendors.Text = "Stock Vendors";
            // 
            // MNUWindow
            // 
            this.MNUWindow.Name = "MNUWindow";
            this.MNUWindow.Size = new System.Drawing.Size(63, 20);
            this.MNUWindow.Text = "Window";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(649, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Exit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // adjustQuantityToolStripMenuItem
            // 
            this.adjustQuantityToolStripMenuItem.Name = "adjustQuantityToolStripMenuItem";
            this.adjustQuantityToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.adjustQuantityToolStripMenuItem.Text = "Adjust Quantity";
            this.adjustQuantityToolStripMenuItem.Click += new System.EventHandler(this.adjustQuantityToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MNUMain);
            this.MainMenuStrip = this.MNUMain;
            this.Name = "frmMain";
            this.Text = "Form_Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.MNUMain.ResumeLayout(false);
            this.MNUMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip MNUMain;
        private System.Windows.Forms.ToolStripMenuItem MNUItems;
        private System.Windows.Forms.ToolStripMenuItem MNUStockLocation;
        private System.Windows.Forms.ToolStripMenuItem MNUStockLot;
        private System.Windows.Forms.ToolStripMenuItem MNUStockItems;
        private System.Windows.Forms.ToolStripMenuItem MNUStockDescription;
        private System.Windows.Forms.ToolStripMenuItem MNUStockMedia;
        private System.Windows.Forms.ToolStripMenuItem MNUStockProductFamilies;
        private System.Windows.Forms.ToolStripMenuItem MNUStockVendors;
        private System.Windows.Forms.ToolStripMenuItem MNUWindow;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem adjustQuantityToolStripMenuItem;
    }
}