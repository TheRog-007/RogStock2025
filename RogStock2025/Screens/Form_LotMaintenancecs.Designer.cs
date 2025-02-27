namespace RogStock2025.Screens
{
    partial class frmLotMaintenancecs
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
            this.CMBLOT_Nbr = new System.Windows.Forms.ComboBox();
            this.LBLLOT_Nbr = new System.Windows.Forms.Label();
            this.TXTLOC_Desc = new System.Windows.Forms.TextBox();
            this.LBLLOC_Desc = new System.Windows.Forms.Label();
            this.TXTHidden = new System.Windows.Forms.TextBox();
            this.BTNDelete = new System.Windows.Forms.Button();
            this.BTNUndo = new System.Windows.Forms.Button();
            this.CHK_LOC_NonNet = new System.Windows.Forms.CheckBox();
            this.NUDLOC_Qty = new System.Windows.Forms.NumericUpDown();
            this.LBLLOC_Qty = new System.Windows.Forms.Label();
            this.BTNNew = new System.Windows.Forms.Button();
            this.CMBLOT_ItemID = new System.Windows.Forms.ComboBox();
            this.LBLLOT_ItemID = new System.Windows.Forms.Label();
            this.BTNClose = new System.Windows.Forms.Button();
            this.BTNSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).BeginInit();
            this.SuspendLayout();
            // 
            // CMBLOT_Nbr
            // 
            this.CMBLOT_Nbr.FormattingEnabled = true;
            this.CMBLOT_Nbr.Location = new System.Drawing.Point(453, 13);
            this.CMBLOT_Nbr.Name = "CMBLOT_Nbr";
            this.CMBLOT_Nbr.Size = new System.Drawing.Size(200, 21);
            this.CMBLOT_Nbr.TabIndex = 32;
            // 
            // LBLLOT_Nbr
            // 
            this.LBLLOT_Nbr.AutoSize = true;
            this.LBLLOT_Nbr.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_Nbr.Location = new System.Drawing.Point(368, 17);
            this.LBLLOT_Nbr.Name = "LBLLOT_Nbr";
            this.LBLLOT_Nbr.Size = new System.Drawing.Size(65, 13);
            this.LBLLOT_Nbr.TabIndex = 45;
            this.LBLLOT_Nbr.Text = "Lot Number:";
            // 
            // TXTLOC_Desc
            // 
            this.TXTLOC_Desc.Location = new System.Drawing.Point(7, 63);
            this.TXTLOC_Desc.MaxLength = 523;
            this.TXTLOC_Desc.Multiline = true;
            this.TXTLOC_Desc.Name = "TXTLOC_Desc";
            this.TXTLOC_Desc.Size = new System.Drawing.Size(686, 84);
            this.TXTLOC_Desc.TabIndex = 34;
            this.TXTLOC_Desc.Tag = "0";
            // 
            // LBLLOC_Desc
            // 
            this.LBLLOC_Desc.AutoSize = true;
            this.LBLLOC_Desc.ForeColor = System.Drawing.Color.Black;
            this.LBLLOC_Desc.Location = new System.Drawing.Point(4, 46);
            this.LBLLOC_Desc.Name = "LBLLOC_Desc";
            this.LBLLOC_Desc.Size = new System.Drawing.Size(60, 13);
            this.LBLLOC_Desc.TabIndex = 44;
            this.LBLLOC_Desc.Text = "Description";
            // 
            // TXTHidden
            // 
            this.TXTHidden.BackColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTHidden.ForeColor = System.Drawing.SystemColors.Control;
            this.TXTHidden.Location = new System.Drawing.Point(502, 167);
            this.TXTHidden.Name = "TXTHidden";
            this.TXTHidden.Size = new System.Drawing.Size(10, 13);
            this.TXTHidden.TabIndex = 43;
            this.TXTHidden.TabStop = false;
            // 
            // BTNDelete
            // 
            this.BTNDelete.Location = new System.Drawing.Point(249, 219);
            this.BTNDelete.Name = "BTNDelete";
            this.BTNDelete.Size = new System.Drawing.Size(75, 23);
            this.BTNDelete.TabIndex = 39;
            this.BTNDelete.Text = "Delete";
            this.BTNDelete.UseVisualStyleBackColor = true;
            // 
            // BTNUndo
            // 
            this.BTNUndo.Location = new System.Drawing.Point(145, 219);
            this.BTNUndo.Name = "BTNUndo";
            this.BTNUndo.Size = new System.Drawing.Size(75, 23);
            this.BTNUndo.TabIndex = 38;
            this.BTNUndo.Text = "Undo";
            this.BTNUndo.UseVisualStyleBackColor = true;
            // 
            // CHK_LOC_NonNet
            // 
            this.CHK_LOC_NonNet.AutoSize = true;
            this.CHK_LOC_NonNet.Checked = true;
            this.CHK_LOC_NonNet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_LOC_NonNet.Location = new System.Drawing.Point(159, 170);
            this.CHK_LOC_NonNet.Name = "CHK_LOC_NonNet";
            this.CHK_LOC_NonNet.Size = new System.Drawing.Size(72, 17);
            this.CHK_LOC_NonNet.TabIndex = 36;
            this.CHK_LOC_NonNet.Text = "Non Net?";
            this.CHK_LOC_NonNet.UseVisualStyleBackColor = true;
            // 
            // NUDLOC_Qty
            // 
            this.NUDLOC_Qty.Location = new System.Drawing.Point(78, 166);
            this.NUDLOC_Qty.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUDLOC_Qty.Name = "NUDLOC_Qty";
            this.NUDLOC_Qty.Size = new System.Drawing.Size(56, 20);
            this.NUDLOC_Qty.TabIndex = 35;
            this.NUDLOC_Qty.Tag = "1";
            // 
            // LBLLOC_Qty
            // 
            this.LBLLOC_Qty.AutoSize = true;
            this.LBLLOC_Qty.ForeColor = System.Drawing.Color.Red;
            this.LBLLOC_Qty.Location = new System.Drawing.Point(4, 170);
            this.LBLLOC_Qty.Name = "LBLLOC_Qty";
            this.LBLLOC_Qty.Size = new System.Drawing.Size(49, 13);
            this.LBLLOC_Qty.TabIndex = 42;
            this.LBLLOC_Qty.Text = "Quantity:";
            // 
            // BTNNew
            // 
            this.BTNNew.Location = new System.Drawing.Point(660, 14);
            this.BTNNew.Name = "BTNNew";
            this.BTNNew.Size = new System.Drawing.Size(39, 20);
            this.BTNNew.TabIndex = 33;
            this.BTNNew.Text = "New";
            this.BTNNew.UseVisualStyleBackColor = true;
            // 
            // CMBLOT_ItemID
            // 
            this.CMBLOT_ItemID.FormattingEnabled = true;
            this.CMBLOT_ItemID.Location = new System.Drawing.Point(54, 12);
            this.CMBLOT_ItemID.MaxLength = 50;
            this.CMBLOT_ItemID.Name = "CMBLOT_ItemID";
            this.CMBLOT_ItemID.Size = new System.Drawing.Size(304, 21);
            this.CMBLOT_ItemID.TabIndex = 31;
            this.CMBLOT_ItemID.Tag = "1";
            // 
            // LBLLOT_ItemID
            // 
            this.LBLLOT_ItemID.AutoSize = true;
            this.LBLLOT_ItemID.ForeColor = System.Drawing.Color.Red;
            this.LBLLOT_ItemID.Location = new System.Drawing.Point(4, 15);
            this.LBLLOT_ItemID.Name = "LBLLOT_ItemID";
            this.LBLLOT_ItemID.Size = new System.Drawing.Size(41, 13);
            this.LBLLOT_ItemID.TabIndex = 41;
            this.LBLLOT_ItemID.Text = "Item ID";
            // 
            // BTNClose
            // 
            this.BTNClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTNClose.Location = new System.Drawing.Point(610, 218);
            this.BTNClose.Name = "BTNClose";
            this.BTNClose.Size = new System.Drawing.Size(75, 23);
            this.BTNClose.TabIndex = 40;
            this.BTNClose.Text = "Close";
            this.BTNClose.UseVisualStyleBackColor = true;
            // 
            // BTNSave
            // 
            this.BTNSave.Location = new System.Drawing.Point(6, 219);
            this.BTNSave.Name = "BTNSave";
            this.BTNSave.Size = new System.Drawing.Size(75, 23);
            this.BTNSave.TabIndex = 37;
            this.BTNSave.Text = "Save";
            this.BTNSave.UseVisualStyleBackColor = true;
            // 
            // frmLotMaintenancecs
            // 
            this.AcceptButton = this.BTNSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BTNClose;
            this.ClientSize = new System.Drawing.Size(709, 254);
            this.Controls.Add(this.CMBLOT_Nbr);
            this.Controls.Add(this.LBLLOT_Nbr);
            this.Controls.Add(this.TXTLOC_Desc);
            this.Controls.Add(this.LBLLOC_Desc);
            this.Controls.Add(this.TXTHidden);
            this.Controls.Add(this.BTNDelete);
            this.Controls.Add(this.BTNUndo);
            this.Controls.Add(this.CHK_LOC_NonNet);
            this.Controls.Add(this.NUDLOC_Qty);
            this.Controls.Add(this.LBLLOC_Qty);
            this.Controls.Add(this.BTNNew);
            this.Controls.Add(this.CMBLOT_ItemID);
            this.Controls.Add(this.LBLLOT_ItemID);
            this.Controls.Add(this.BTNClose);
            this.Controls.Add(this.BTNSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLotMaintenancecs";
            this.ShowInTaskbar = false;
            this.Text = "Form_LotMaintenancecs";
            ((System.ComponentModel.ISupportInitialize)(this.NUDLOC_Qty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMBLOT_Nbr;
        private System.Windows.Forms.Label LBLLOT_Nbr;
        private System.Windows.Forms.TextBox TXTLOC_Desc;
        private System.Windows.Forms.Label LBLLOC_Desc;
        private System.Windows.Forms.TextBox TXTHidden;
        private System.Windows.Forms.Button BTNDelete;
        private System.Windows.Forms.Button BTNUndo;
        private System.Windows.Forms.CheckBox CHK_LOC_NonNet;
        private System.Windows.Forms.NumericUpDown NUDLOC_Qty;
        private System.Windows.Forms.Label LBLLOC_Qty;
        private System.Windows.Forms.Button BTNNew;
        private System.Windows.Forms.ComboBox CMBLOT_ItemID;
        private System.Windows.Forms.Label LBLLOT_ItemID;
        private System.Windows.Forms.Button BTNClose;
        private System.Windows.Forms.Button BTNSave;
    }
}