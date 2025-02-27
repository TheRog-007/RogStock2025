using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlTypes;

namespace RogStock2025.Screens
{
    public partial class frmLocMaintenance : Form
    {
        /*
           
          Created 25/02/2025 By Roger Williams

          Uses disconnected recordset to load ONE record into the form 

          User can crate new record/edit/delete as only affects dataset

          Dataset changes are saved to the database

          Uses:

           Bindingsource
           Dataset
           DataAdapter (to load record into dataset)
           SQl Command to load/save data

        */

        //first control on form (focus) used by enabledisableform
        readonly string CNST_STR_FIRSTCONTROL = "CMBLOC_ItemID";

        //data table vars
        SqlConnection SQLConn;
        SqlCommand SQLcmd;
        SqlDataAdapter DADLoc;
        DataSet DSTLoc;
        BindingSource BNSLoc = new BindingSource();
        bool blnNew = false;
        bool blnLoading = false;

        //holds current item id used if record deleted
        string strItemID = "";
        //holds current location used if renaming an existing location
        string strLocation = "";

        public frmLocMaintenance()
        {
            InitializeComponent();
        }

        //other
        private void ResetForm(string strKeep, bool blnEnable)
        {
            /*
              Created 25/02/2025 By Roger Williams

             Resets form 
             Enables/Disables form
             Undoes dataset changes

            VARS

            strKeep     - control to leave
            blnEnable   - enable or disable form

            */

            //undo changes
            if (this.DSTLoc != null)
            {
                if (this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].GetChanges() != null)
                {
                    this.BNSLoc.CancelEdit();
                }
            }
            //reset form
            Modules.clsView.ResetForm(this, strKeep);
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, blnEnable);
            blnNew = false;
            strItemID = "";
        }

        //data
        private void DeleteRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Deletes current record using a transaction
              - clears form 
              - clears dataset

            */
            if (DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].Rows.Count == 0)
            {
                return;
            }

            //delete from database
            SQLcmd.CommandText = "DELETE FROM Stock_Loc WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text + "' AND LOC_Location = '" + this.CMBLOC_Location.Text + "';";

            try
            {
                SQLcmd.ExecuteNonQuery();
                //undo form 
                this.BNSLoc.RemoveCurrent();

                //reset form
                ResetForm("", false);
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsData.CNST_STR_STOCK_ITEMS, "", "");
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text);
                MessageBox.Show("Record Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Deleting Data\n\n" + ex.Message, "Delete Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Saves record to stock_teims and stock_description (stock_vendors -> v2)
              - clears form 
              - clears dataset

            */

            int intBoolean = 0;

            //commit changes to dataset
            this.BNSLoc.EndEdit();



            if (DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].Rows.Count == 0)
            {
                return;
            }

            if (DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].Rows.Count == 0 && !blnNew)
            {
                return;
            }

            //check required fields completed
            if (Modules.clsView.ValidateRequiredFields(this))
            {
                try
                {
                    intBoolean = this.CHK_LOC_NonNet.Checked == true ? -1 : 0;

                    //save data to database
                    if (blnNew)
                    {
                        SQLcmd.CommandText = "INSERT INTO " + Modules.clsData.CNST_STR_STOCK_LOC + " (LOC_ItemID, LOC_Location, LOC_Qty, LOC_Description, LOC_NonNet) " +
                                             "VALUES ('" + this.CMBLOC_ItemID.Text + "','" + this.CMBLOC_Location.Text + "'," + this.NUDLOC_Qty.Value + ",'" + this.TXTLOC_Desc.Text + "'," + intBoolean + ")";
                    }
                    else
                    {
                        //if existing record check something to save!
                        if (this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].GetChanges() == null)
                        {
                            MessageBox.Show("Error Nothing Changed", "Nothing To Save!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SQLcmd.CommandText = "UPDATE " + Modules.clsData.CNST_STR_STOCK_LOC + " SET LOC_Location = '" + this.CMBLOC_Location.Text + "', " +
                                             "LOC_Qty = " + this.NUDLOC_Qty.Value + ", LOC_Description = '" + this.TXTLOC_Desc.Text + "', LOC_NonNet = " + intBoolean + " WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text +
                                             "' AND LOC_Location = '" + strLocation + "';";
                    }

                    SQLcmd.ExecuteNonQuery();

                    //reset form
                    ResetForm("", false);
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsData.CNST_STR_STOCK_ITEMS, "", "");
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text);
                    MessageBox.Show("Record Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Saving Data:\n\n" + ex.Message, "Save Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BindForm()
        {
            /*
              Created 25/02/2025 By Roger Williams

             opens ADO connection and binds form to table: Stock_Loc

            */


            //this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            //try
            //{
            //    SQLConn.Open();
            //    //load stock items
            //    this.SQLcmd = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_ITEMS + ";", SQLConn);
            //    this.DADLoc = new SqlDataAdapter(SQLcmd);
            //    this.DSTLoc = new DataSet();
            //    this.DADLoc.Fill(DSTLoc, Modules.clsData.CNST_STR_STOCK_ITEMS);

            //    //check stock item record exist
            //    if (this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_ITEMS].Rows.Count == 0)
            //    {
            //        {
            //            MessageBox.Show("Cannot Edit Locations As No Stock Items Created!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            this.Close();
            //            return;
            //        }
            //    }


            //load stock description data STKD_Desc

            //get stock description records
            //this.SQLcmd = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + ";", SQLConn);
            //this.DADLoc = new SqlDataAdapter(SQLcmd);
            //this.DSTLoc = new DataSet();
            //this.DADLoc.Fill(DSTLoc, Modules.clsData.CNST_STR_STOCK_DESCRIPTION);

            //this.BNSLoc.DataSource = this.DSTLoc;

            ////bind form controls to stock_loc
            //this.LBLSTKD_Desc.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc", true, DataSourceUpdateMode.OnPropertyChanged);


            //load locations
            //this.SQLcmd = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_LOC + ";", SQLConn);
            //this.DADLoc = new SqlDataAdapter(SQLcmd);
            //this.DSTLoc = new DataSet();
            //this.DADLoc.Fill(DSTLoc, Modules.clsData.CNST_STR_STOCK_LOC);


            //bind to bindingsource
            //     this.BNSLoc.DataSource = this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC];
            this.BNSLoc.DataSource = this.DSTLoc;

            if (this.LBLSTKD_Desc.DataBindings.Count == 0)
            { 
                //bind form controls to stock_loc
                this.LBLSTKD_Desc.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_DESCRIPTION + ".STKD_Desc", true, DataSourceUpdateMode.OnPropertyChanged);
                //bind form controls to stock_loc
                this.TXTHidden.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_LOC + ".LOC_ItemID", true, DataSourceUpdateMode.OnPropertyChanged);
                this.TXTLOC_Desc.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_LOC + ".LOC_Description", true, DataSourceUpdateMode.OnPropertyChanged);
                this.CMBLOC_Location.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_LOC + ".LOC_Location", true, DataSourceUpdateMode.OnPropertyChanged);
                this.CHK_LOC_NonNet.DataBindings.Add("checked", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_LOC + ".LOC_NonNet", false, DataSourceUpdateMode.OnPropertyChanged);
                this.NUDLOC_Qty.DataBindings.Add("text", this.BNSLoc, Modules.clsData.CNST_STR_STOCK_LOC + ".LOC_Qty", false, DataSourceUpdateMode.OnPropertyChanged);
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void LoadRecord()
        {
            /*
              Created 25/02/2025 By Roger Williams

              - Populates the form
              - Enables the form

            */
            string strTemp;

            strTemp = this.CMBLOC_Location.Text;
            ResetForm(CNST_STR_FIRSTCONTROL, false);
            this.CMBLOC_Location.Text = strTemp;
            this.SQLConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            blnLoading = true;

            try
            {
                SQLConn.Open();

                //get stock description records
                this.SQLcmd = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_DESCRIPTION + ";", SQLConn);
                this.DADLoc = new SqlDataAdapter(SQLcmd);
                this.DSTLoc = new DataSet();
                this.DADLoc.Fill(DSTLoc, Modules.clsData.CNST_STR_STOCK_DESCRIPTION);

                //get location data
                this.SQLcmd = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_LOC + " WHERE LOC_ItemID = '" + this.CMBLOC_ItemID.Text + "' AND LOC_Location ='" + this.CMBLOC_Location.Text + "';", SQLConn);
                this.DADLoc = new SqlDataAdapter(SQLcmd);
                //    this.DSTLoc = new DataSet();
                this.DADLoc.Fill(DSTLoc, Modules.clsData.CNST_STR_STOCK_LOC);
                //bind to bindingsource
                //    this.BNSLoc.DataSource = this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC];
                //populate combbox with available item ids
                Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsData.CNST_STR_STOCK_ITEMS, "", "");

                if (this.TXTHidden.Text.Length == 0)
                {
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "", "");
                }
                else
                {
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "LOC_ItemID", this.TXTHidden.Text);
                    this.CMBLOC_ItemID.Text = this.TXTHidden.Text;
                }

                BindForm();

                //enable form
                Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);

                //check if new record
                if (this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].Rows.Count == 0)
                {
                    if (MessageBox.Show("No Records Found Create New Record?", "No Matching Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.BTNNew_Click(this, new EventArgs());
                    }
                }
                //else
                //{
                //    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "LOC_ItemID", this.TXTHidden.Text);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Data:\n\n" + ex.Message, "load Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            blnLoading = false;
        }

        private void CheckForStockItems()
        /*

        */
        {
            SqlConnection SQLConnStock;
            SqlCommand SQLcmdStock;
            SqlDataAdapter DADLocStock;
            DataSet DSTLocStock;
            SQLConnStock = new SqlConnection(Modules.clsData.CNST_STR_ODBC);

            try
            {
                SQLConnStock.Open();
                //load stock items
                SQLcmdStock = new SqlCommand("SELECT * FROM " + Modules.clsData.CNST_STR_STOCK_ITEMS + ";", SQLConnStock);
                DADLocStock = new SqlDataAdapter(SQLcmdStock);
                DSTLocStock = new DataSet();
                DADLocStock.Fill(DSTLocStock, Modules.clsData.CNST_STR_STOCK_ITEMS);
                DADLocStock.Dispose();
                SQLConnStock.Close();

                //check stock item record exist
                if (DSTLocStock.Tables[Modules.clsData.CNST_STR_STOCK_ITEMS].Rows.Count == 0)
                {
                    {
                        MessageBox.Show("Cannot Edit Locations As No Stock Items Created!", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Opening Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        //form events
        private void Form_LocMaintenance_Load(object sender, EventArgs e)
        {

//            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, false);
          //  BindForm();
            ResetForm("", false);
            CheckForStockItems();
            Modules.clsData.PopulateComboBoxes(this.CMBLOC_ItemID, Modules.clsData.CNST_STR_STOCK_ITEMS, "", "");
        }

        private void Form_LocMaintenance_Paint(object sender, PaintEventArgs e)
        {
            /*
              Created 25/02/2025 By Roger Williams

              Draws line across screen
            
            */

            Graphics grpTemp;
            Pen penTemp;

            grpTemp = this.CreateGraphics();

            //draw lines
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            grpTemp.DrawLine(penTemp, 0, 60, this.Width, 60);
        }

        private void BTNClose_Click(object sender, EventArgs e)
        {
            try
            {
                DSTLoc.Clear();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
            }
                this.Close();
        }

        private void BTNSave_Click(object sender, EventArgs e)
        {
          SaveRecord();
        }

             private void TXTLOC_Location_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTNNew_Click(object sender, EventArgs e)
        {
            Modules.clsView.EnableDisableForm(this, CNST_STR_FIRSTCONTROL, true);
            //add new blank record
            this.BNSLoc.AddNew();
            blnNew = true;
            //set qty to 0
            this.NUDLOC_Qty.Value = 0;
        }

        private void BTNUndo_Click(object sender, EventArgs e)
        {
            //undo changes
            if (this.DSTLoc.Tables[Modules.clsData.CNST_STR_STOCK_LOC].GetChanges() != null)
            {
                if (MessageBox.Show("Changes Made Undo?","Lose Changes",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                { 
                   this.BNSLoc.CancelEdit();
                   blnNew = false;
                   strItemID = "";
                }
            }
        }

        private void BTNDelete_Click(object sender, EventArgs e)
        {
            if (blnNew)
            {
                //if new record just undo
                BTNUndo_Click(sender, e);
            }
            else
            { 
                if (MessageBox.Show("Delete Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    DeleteRecord();
                }
            }

           blnNew = false;
        }

        private void CMBLOC_ItemID_TextUpdate(object sender, EventArgs e)
        {
        /*
                Created 26/02/2025 By Roger Williams

                makes sure item id text is in the list

        */

            bool blnFound = false;


                foreach (object objItem in this.CMBLOC_ItemID.Items)
                {
                    if (objItem.ToString() != CMBLOC_ItemID.Text)
                    {
                        blnFound = true;
                        break;
                    }
                }

                if (blnFound)
                {
                    MessageBox.Show("Item Id Not In List", "Invalid Item ID", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.CMBLOC_ItemID.Text = strItemID;
                }
                else
                {
                    strItemID = this.CMBLOC_ItemID.Text;
                    this.TXTHidden.Text = strItemID;
                }
            }


        private void CMBLOC_ItemID_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void CMBLOC_ItemID_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 17/02/2025 By Roger Williams


            */

            strItemID = this.CMBLOC_ItemID.Text;
            
            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            this.CMBLOC_Location.Enabled = false;

            if (this.CMBLOC_ItemID.SelectedIndex != -1)
            {
                //if stock items available
                if (this.CMBLOC_ItemID.Items.Count > 0)
                {
                    //enable location combobox
                    this.CMBLOC_Location.Enabled = true;
                    //populate locations for item list
                    Modules.clsData.PopulateComboBoxes(this.CMBLOC_Location, Modules.clsData.CNST_STR_STOCK_LOC, "LOC_ItemID", this.CMBLOC_ItemID.Text);
                }
            }
        }
        private void CMBLOC_Location_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
                   Created 25/02/2025 By Roger Williams

                   load record

            */
            strLocation = this.CMBLOC_Location.Text;

            if (blnNew)
            {
                //if new record do nothing
                return;
            }

            if (this.CMBLOC_Location.SelectedIndex != -1)
            {
                if (!blnLoading)
                { 
                   //if no records load!
                   LoadRecord();
                }
            }
        }

        private void CMBLOC_Location_TextUpdate(object sender, EventArgs e)
        {

        }

        private void CMBLOC_Location_Enter(object sender, EventArgs e)
        {
          
        }
    }
}
