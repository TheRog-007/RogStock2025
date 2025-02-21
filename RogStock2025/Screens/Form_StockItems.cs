using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

namespace RogStock2025.Forms
{
    public partial class frmStockItems : Form
    {
        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBSTKI_ItemID";
        //datasets used to checking if data changed
        DataSet DSTDataItems = new DataSet();
        DataSet DSTDataDescription = new DataSet();
        DataSet DSTDataMedia = new DataSet();
        bool blnNew = false;
        string strStatus = "";

        public frmStockItems()
        {
            InitializeComponent();
        }

        //data
        private void LoadRecord()
        {
            /*
              Created 18/02/2025 By Roger Williams
              
              - Populatess the form
              - Populates dataset with table and a COPY of the table
              - Binds controls to datasetcur 
              - Enables the form

            Note: recads from these tables:
                  - Stock_Items
                  - Stock_Description
                  - Stock_Media
                  - Stock_Vendors <- implement in version 2

            */
            SqlConnection ADOConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            DataRowCollection DRCRows;
            ListViewItem LVITemp;
            DataRow DARRow;
            SqlDataAdapter DADTemp;

            string strSQL1 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_ITEMS + " WHERE STKI_ItemID ='" + this.CMBSTKI_ItemID.Text + "';";
            string strSQL2 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + " WHERE STKD_ItemID ='" + this.CMBSTKI_ItemID.Text + "';";
            string strSQL3 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_MEDIA + " WHERE STKM_ItemID ='" + this.CMBSTKI_ItemID.Text + "';";

            //reset form
            Modules.clsView.ResetForm(this, "CMBSTKI_ItemID");


            try
            {
                using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    ADOConn.Open();
                    SQLCmd = ADOConn.CreateCommand();
                    SQLCmd.CommandText = strSQL1;
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead.Read())
                    {
                        SQLRead.Close();
                        //clear existing records
                        if (DSTDataItems.Tables.Count > 0 )
                        { 
                            DSTDataDescription.Tables[Modules.clsData.CNST_STR_STOCK_DESCRIPTION].Clear();
                            DSTDataItems.Tables[Modules.clsData.CNST_STR_STOCK_ITEMS].Clear();
                            DSTDataMedia.Tables[Modules.clsData.CNST_STR_STOCK_MEDIA].Clear();
                        }
                        ////get record into into dataset
                        DADTemp = new SqlDataAdapter(strSQL1, ADOConn);
                        DADTemp.SelectCommand.CommandText = strSQL1;
                        DADTemp.Fill(DSTDataItems, Modules.clsData.CNST_STR_STOCK_ITEMS);
                        DADTemp.SelectCommand.CommandText = strSQL2;
                        DADTemp.Fill(DSTDataDescription, Modules.clsData.CNST_STR_STOCK_DESCRIPTION);
                        DADTemp.SelectCommand.CommandText = strSQL3;
                        DADTemp.Fill(DSTDataMedia, Modules.clsData.CNST_STR_STOCK_MEDIA);
                        //populate form 
                        DARRow = DSTDataItems.Tables[Modules.clsData.CNST_STR_STOCK_ITEMS].Rows[0];
                        this.CMBSTKI_ProductFamily.Text = DARRow["STKI_ProductFamily"].ToString();
                        this.CMBSTKI_UOM.Text = DARRow["STKI_UOM"].ToString();
                        this.CHKSTKI_LocLot.Checked = (bool)DARRow["STKI_LocLot"];
                        this.NUDSTKI_Price.Value = (decimal)DARRow["STKI_Price"];
                        //get description data from datasetcur
                        //Note: only need to read first record as 1 - 1 relationship
                        DARRow = DSTDataDescription.Tables[Modules.clsData.CNST_STR_STOCK_DESCRIPTION].Rows[0];

                        this.TXTSTKD_Desc.Text = DARRow["STKD_Desc"].ToString();
                        this.TXTSTKD_LongDesc.Text = DARRow["STKD_LongDesc"].ToString();

                        ////read media into listview
                        DRCRows = DSTDataMedia.Tables[Modules.clsData.CNST_STR_STOCK_MEDIA].Rows;
                        //clear listview
                        this.LVMedia.Items.Clear();

                        foreach (DataRow DARTemp in DRCRows)
                        {
                            LVITemp = new ListViewItem();
                            LVITemp.Text = DARTemp["STKM_Path"].ToString();
                            LVITemp.SubItems.Add(DARTemp["STKM_Type"].ToString());
                            this.LVMedia.Items.Add(LVITemp);
                        }

                        ADOConn.Close();
                        //enable form
                        Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
                        //update hidden text control with item id
                        this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
                        strStatus = "old";
                    }
                    else
                    {
                        MessageBox.Show("No Records Found", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
;
        }

        private void SaveRecord()
        {
            /*
              Created 18/02/2025 By Roger Williams

              - Saves record to stock_teims and stock_description (stock_vendors -> v2)
              - clears form 
              - clears datasets

              if new just writes form contents if existing post dataset table changes
            */

            SqlConnection ADOConn; // = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SqlCommand SQLCmdItems;
            SqlCommand SQLCmdDesc;
            SqlCommand SQLCmdMedia;
            SqlTransaction SQLTRNSave;
            int intNum = 0;
            int intWritten = 0;
            SqlDataAdapter DADTemp1;
            SqlCommandBuilder SCBTemp1;
            SqlDataAdapter DADTemp2;
            SqlCommandBuilder SCBTemp2;
            int intBoolean = 0;
            string strSQL1 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_ITEMS+ " ORDER BY STKI_ItemID;";
            string strSQL2 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + ";";
            string strSQL3 = "SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_MEDIA + ";";

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                    {
                        ADOConn.Open();

                        //create data adapters for update
                        DADTemp1 = new SqlDataAdapter(strSQL1, ADOConn);
                        DADTemp2 = new SqlDataAdapter(strSQL2, ADOConn);
                        //create command objects for each table
                        SQLCmdItems = ADOConn.CreateCommand();
                        SQLCmdDesc = ADOConn.CreateCommand();
                        SQLCmdMedia = ADOConn.CreateCommand();
                        //start transction
                        SQLTRNSave = ADOConn.BeginTransaction();
                        //assign commands to the transaction
                        SQLCmdDesc.Transaction = SQLTRNSave;
                        SQLCmdItems.Transaction = SQLTRNSave;
                        SQLCmdMedia.Transaction = SQLTRNSave;


                        try
                        {
                            if (!blnNew)
                            {
                                //delete existing
                                SQLCmdItems.CommandText = "DELETE FROM " + Modules.clsData.CNST_STR_STOCK_ITEMS + "WHERE STKI_ItemID = '" + this.CMBSTKI_ItemID.Text + "';";
                                intWritten = SQLCmdMedia.ExecuteNonQuery();
                                SQLCmdDesc.CommandText = "DELETE FROM " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + "WHERE STKD_ItemID = '" + this.CMBSTKI_ItemID.Text + "';";
                                intWritten = SQLCmdMedia.ExecuteNonQuery();
                                SQLCmdMedia.CommandText = "DELETE FROM " + Modules.clsData.CNST_STR_STOCK_MEDIA + "WHERE STKM_ItemID = '" + this.CMBSTKI_ItemID.Text + "';";
                                intWritten = SQLCmdMedia.ExecuteNonQuery();
                            }
  
                            //insert records!
                            intBoolean = this.CHKSTKI_LocLot.Checked == true ? -1 : 0;
                            SQLCmdItems.CommandText = "INSERT INTO " + Modules.clsData.CNST_STR_STOCK_ITEMS + " (STKI_ItemID, STKI_UOM, STKI_ProductFamily, STKI_Price, STKI_LocLot) " +
                                                      "VALUES('" + this.CMBSTKI_ItemID.Text + "','" + this.CMBSTKI_UOM.Text + "','" + this.CMBSTKI_ProductFamily.Text + "'," + this.NUDSTKI_Price.Value + "," + intBoolean + ");";
                            SQLCmdDesc.CommandText = "INSERT INTO " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + " (STKD_ItemID, STKD_Desc, STKD_LongDesc) " +
                                                      "VALUES('" + this.CMBSTKI_ItemID.Text + "','" + this.TXTSTKD_Desc.Text + "','" + this.TXTSTKD_LongDesc.Text + "');";

                            intWritten = SQLCmdItems.ExecuteNonQuery();
                            intWritten = SQLCmdDesc.ExecuteNonQuery();

                            //store media files if any
                            for (intNum = 0; intNum != this.LVMedia.Items.Count; intNum++)
                            {
                                SQLCmdMedia.CommandText = "INSERT INTO " + Modules.clsData.CNST_STR_STOCK_MEDIA + " (STKM_ItemID, STKM_Path, STKM_Type) VALUES ('" + this.CMBSTKI_ItemID.Text + "','" + this.LVMedia.Items[intNum].Text + "','" + this.LVMedia.Items[intNum].SubItems[0].Text + "');";
                                intWritten = SQLCmdMedia.ExecuteNonQuery();
                            }

                            //write changes
                            SQLTRNSave.Commit();
                            MessageBox.Show("Record Saved!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                           SQLTRNSave.Rollback();
                           MessageBox.Show("Error Saving Data:\n" + ex.Message,"Save Failed!",MessageBoxButtons.OK,MessageBoxIcon.Error);  
                        }

                        ADOConn.Close();
                           //clear form
                        Modules.clsView.ResetForm(this, "CMBSTKI_ItemID");
                        //disable form
                        Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, false);
                    }
                }
                catch (Exception ex)
                {
                    //Whoops!
                }
            }
        }

        private bool CheckForChanges()
        {
            /*
              Created 19/02/2025 By Roger Williams

              Checks datasets and compares values to controls to see if any changes


            */

            int intChangesMade = 0;
            int intNum = 0;
            DataRow DARRow;

            //first check if dataset is initialied (data loaded from table)
            if (DSTDataItems is null)
            {
                return false;
            }
            //check stock items
            DARRow = DSTDataItems.Tables[Modules.clsData.CNST_STR_STOCK_ITEMS].Rows[0];
            if (DARRow["STKI_ProductFamily"].ToString() != this.CMBSTKI_ProductFamily.Text)
            {
                intChangesMade++;
            }
            if (DARRow["STKI_UOM"].ToString() != this.CMBSTKI_UOM.Text)
            {
                intChangesMade++;
            }
            if ((decimal)DARRow["STKI_Price"] != this.NUDSTKI_Price.Value)
            {
                intChangesMade++;
            }
            if ((bool)DARRow["STKI_LocLot"] != this.CHKSTKI_LocLot.Checked)
            {
                intChangesMade++;
            }
            //check description
            DARRow = DSTDataDescription.Tables[Modules.clsData.CNST_STR_STOCK_DESCRIPTION].Rows[0];
            if (DARRow["STKD_Desc"].ToString() != this.TXTSTKD_Desc.Text)
            {
                intChangesMade++;
            }
            if (DARRow["STKD_LongDesc"].ToString() != this.TXTSTKD_LongDesc.Text)
            {
                intChangesMade++;
            }
            //check media
            for (intNum = 0; intNum != this.LVMedia.Items.Count; intNum++)
            {
                if (DSTDataMedia.Tables[Modules.clsData.CNST_STR_STOCK_MEDIA].Select("STKM_ItemID ='" + this.CMBSTKI_ItemID.Text + "' AND STKM_Path ='" + this.LVMedia.Items[intNum].Text + "'").Length == 0)
                {
                    intChangesMade++;
                }
            }

            if (intChangesMade != 0)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }

        private void AddFile()
        {
                /*
                Created 17/02/2025 By Roger Williams

                Adds selected file to list

                */
                ListViewItem LVITemp;
            string strTemp1 = "";
            int intNum1 = 0;
            string[] aryFilter;

            this.OFDSelectFile.Title = "Select File To Associate With " + this.CMBSTKI_ItemID.Text;
            this.OFDSelectFile.FileName = "";
            this.OFDSelectFile.FilterIndex = 1;
            this.OFDSelectFile.ShowDialog();

            if (this.OFDSelectFile.FileName != "")
            {
                LVITemp = new ListViewItem();
                LVITemp.Text = this.OFDSelectFile.FileName;
                intNum1 = this.OFDSelectFile.FilterIndex;


                //extract filter first part
                if (intNum1 == 1)
                {
                    strTemp1 = this.OFDSelectFile.Filter.Substring(0, this.OFDSelectFile.Filter.IndexOf("|"));
                }
                else
                {
                    aryFilter = this.OFDSelectFile.Filter.Split('|');
                    intNum1 = intNum1 * 2 - 2;
                    strTemp1 = aryFilter[intNum1];
                }

                LVITemp.SubItems.Add(strTemp1);
                this.LVMedia.Items.Add(LVITemp);
            }
        }

        //other
        private void RemoveFile()
        {
            /*
               Created 17/02/2025 By Roger Williams

               Removes selectred item

             */
            if (this.LVMedia.SelectedItems.Count != 0)
            {
                this.LVMedia.Items.RemoveAt(this.LVMedia.SelectedItems[0].Index);
            }
        }

        //form events
        private void frmStockItems_Load(object sender, EventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Populates the comboboxes with table values and disables form and initialises the ADO datasets

            */

            Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ItemID, Modules.clsData.CNST_STR_STOCK_ITEMS);
            Modules.clsData.PopulateComboBoxes(this.CMBSTKI_ProductFamily, Modules.clsData.CNST_STR_STOCK_PRODUCTFAMILY);
            Modules.clsData.PopulateComboBoxes(this.CMBSTKI_UOM, Modules.clsData.CNST_STR_STOCK_UOM);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, false);
        }

        private void frmStockItems_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 17/02/2025 By Roger Williams
              
              Draws line across screen
            */
            Graphics grpTemp;
            Pen penTemp;

            grpTemp = this.CreateGraphics();

            //draw lines
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            grpTemp.DrawLine(penTemp, 0, 80, this.Width, 80);
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void BTNAddFile_Click(object sender, EventArgs e)
        {
            AddFile();
        }

        private void LVMedia_DoubleClick(object sender, EventArgs e)
        {
            //previews the file (if possible)

            this.WEBPreview.Navigate(this.LVMedia.SelectedItems[0].Text);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNRemoveFile_Click(object sender, EventArgs e)
        {
            RemoveFile();
        }

        private void CMBSTKI_ItemID_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   load record

               */

            //if (this.CMBSTKI_ItemID.SelectedIndex != -1)
            //{
            //    LoadRecord();
            //    //bind controls to datasetcur
            //    BindControls();
            //}
        }
        private void BTNNew_Click(object sender, EventArgs e)
        {
            blnNew = true;
            //enable form
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
            //reset form
            Modules.clsView.ResetForm(this, "CMBSTKI_ItemID");
        }

        private void CMBSTKI_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams

                   load record

            */
         

            if (blnNew)
            {
                //if new record do nothing
                return;
            }
    
            if (this.CMBSTKI_ItemID.SelectedIndex != -1)
            {

                if (this.TXTHidden.Text == this.CMBSTKI_ItemID.Text)
                {
                    return;
                }


                //if no records load!
                LoadRecord();
            }
        }
        private void CMBSTKI_ItemID_TextUpdate(object sender, EventArgs e)
        {
            /*
Created 17/02/2025 By Roger Williams

load record

*/

            //LoadRecord();
            ////bind controls to datasetcur
            //BindControls();
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            /*
            Created 19/02/2025 By Roger Williams

            undoes changes if made

            */

            if (blnNew || CheckForChanges())
            {
                if (MessageBox.Show("Lose Changes?", "Changes Made", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                   SaveRecord();
                }
            }
     
            //reset form
            Modules.clsView.ResetForm(this, "");
            Modules.clsView.EnableDisableForm(this, "CMBSTKI_ItemID", false);
            blnNew = false;
        }

        private void CMBSTKI_ItemID_TextChanged(object sender, EventArgs e)
        {
            //if new record set txthidden and its changed property
            if (blnNew)
            { 
              this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
              this.TXTHidden.Modified = true;
            }

        }

        private void CMBSTKI_ItemID_Enter(object sender, EventArgs e)
        {
            this.TXTHidden.Text = this.CMBSTKI_ItemID.Text;
        }


        //class end
    }
}
