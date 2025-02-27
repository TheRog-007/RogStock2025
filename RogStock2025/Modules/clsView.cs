using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
/*
   Created 17/02/2025 By Roger Williams

  handles GUI manipulation

*/
namespace RogStock2025.Modules
{
    internal static class clsView
    {
        //      public static List<string> lstOpenForms = new List<string>();
        //      public static List<string> lstOpenReports = new List<string>();

        //formcollection->openforms
        public static void OpenForm(string strWhat)
        {
            /*
               Created 17/02/2025 By Roger Williams

               opens form and stores name in open forms list
            */

            object objForm;


        }
        public static void WindowMenuItemClickHandler(object sender, EventArgs e)
        {
            /*
                 Created 17/02/2025 By Roger Williams

                 handles menu item click

            */

            sender = sender;
        }
        public static void RemoveFromWindowMenu(string strWhat)
        {
            /*
             Created 17/02/2025 By Roger Williams

             removes item from Window menu

            */

            int intNum = 0;
            ToolStripMenuItem MNUTemp;
            MenuStrip MNUMain;

            MNUMain = (MenuStrip)Forms.frmMain.ActiveForm.Controls["MNUMain"];
            intNum = MNUMain.Items.IndexOf(MNUMain.Items[strWhat]);

            if (intNum != -1)
            {
                MNUMain.Items.RemoveAt(intNum);
            }
        }
        public static void AddToWindowMenu(string strWhat)
        {
            /*
             Created 17/02/2025 By Roger Williams
         
             stores form name in open forms menu 
            */
            ToolStripMenuItem MNUTemp;
            ToolStripItem MNUTemp1;
            MenuStrip MNUMain;
            int intNum = 0;

            //see if already exists
            MNUMain = (MenuStrip)Application.OpenForms["frmMain"].Controls["MNUMain"];
            MNUTemp1 = MNUMain.Items[strWhat];

            if (MNUTemp1 is null)
            {
                MNUTemp = new ToolStripMenuItem();
                MNUTemp.Text = strWhat;
                MNUTemp.Click += new EventHandler(Modules.clsView.WindowMenuItemClickHandler);
                //get Window menu item
                MNUTemp1 = MNUMain.Items["MNUWindow"];
                intNum = MNUMain.Items.IndexOf(MNUTemp1);

                //add new item to it
                MNUMain.Items.Insert(intNum, MNUTemp);
            }
            else
            {
                return;
            }
        }
        public static void EnableDisableForm(Form frmTemp, string strFirstControl, bool blnEnable)
        {
            /*
               Created 18/02/2025 By Roger Williams

                enables/disables form controls except:
            
                btnNew, btnClose and control name passed in strFirstControl

            */

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp.Name != strFirstControl && ctlTemp.Name != "BTNNew" && ctlTemp.Name != "BTNClose")
                    {
                        ctlTemp.Enabled = blnEnable;
                    }
                }
                catch (Exception ex)
                {
                    //Whoops!
                }
            }


        }
        public static void ResetForm(Form frmTemp, string strIgnore)
        {
            /*
             Created 18/02/2025 By Roger Williams

             resets the passed form vars controls except strIgnore

              resets:
                - textbox
                - checkbox
                - listview
                - treeview
                - combobox
                - radiobutton

            */
            TreeView TRVTemp;
            ListView LSTTemp;
            CheckBox CHKTemp;
            RadioButton RBTNTemp;
            NumericUpDown NUDTemp;

            int intNum = 0;
            TabControl tabTemp;

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                if (ctlTemp is TabControl)
                {
                    //iterate through tab page controls
                    tabTemp = (TabControl)ctlTemp;

                    for (intNum = 0; intNum < tabTemp.TabPages.Count; intNum++)
                    {
                        try
                        {
                            if (ctlTemp is ComboBox || ctlTemp is TextBox)
                            {
                                ctlTemp.Text = "";
                            }
                            if (ctlTemp is CheckBox)
                            {
                                CHKTemp = (CheckBox)ctlTemp;
                                CHKTemp.Checked = false;
                            }
                            if (ctlTemp is RadioButton)
                            {
                                RBTNTemp = (RadioButton)ctlTemp;
                                RBTNTemp.Checked = false;
                            }
                            if (ctlTemp is ListView)
                            {
                                LSTTemp = (ListView)ctlTemp;
                                LSTTemp.Items.Clear();
                            }
                            if (ctlTemp is TreeView)
                            {
                                TRVTemp = (TreeView)ctlTemp;
                                TRVTemp.Nodes.Clear();
                            }
                            if (ctlTemp is NumericUpDown)
                            {
                                NUDTemp = (NumericUpDown)ctlTemp;
                                NUDTemp.Value = NUDTemp.Minimum;
                            }
                        }
                        catch (Exception ex)
                        {
                            //whoops!
                        }
                    }
                }    
                
                try
                {
                    if (ctlTemp.Name != strIgnore)
                    { 
                        if (ctlTemp is ComboBox || ctlTemp is TextBox)
                        {
                            ctlTemp.Text = "";
                        }
                        if (ctlTemp is CheckBox)
                        {
                            CHKTemp = (CheckBox)ctlTemp;
                            CHKTemp.Checked = false;
                        }
                        if (ctlTemp is RadioButton)
                        {
                            RBTNTemp = (RadioButton)ctlTemp;
                            RBTNTemp.Checked = false;
                        }
                        if (ctlTemp is ListView)
                        {
                            LSTTemp = (ListView)ctlTemp;
                            LSTTemp.Items.Clear();
                        }
                        if (ctlTemp is TreeView)
                        {
                            TRVTemp = (TreeView)ctlTemp;
                            TRVTemp.Nodes.Clear();
                        }
                        if (ctlTemp is NumericUpDown)
                        {
                            NUDTemp = (NumericUpDown)ctlTemp;
                            NUDTemp.Value = NUDTemp.Minimum;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //whoops!
                }
            }
        }
        public static bool ValidateRequiredFields(Form frmTemp)
        {
        /*
         Created 17/02/2025 By Roger Williams

         Validates form required fields populated - uses tag if 1 required field

         VARS

         frmTemp        - form to validate

         returns true if ok

        */
            List<string> lstErrors = new List<string>();
            string strTemp = "";
            int intNum = 0;
            TabControl tabTemp;

            foreach (Control ctlTemp in frmTemp.Controls)
            {
                try
                {
                    if (ctlTemp is TabControl)
                    {
                        //iterate through tab page controls
                        tabTemp = (TabControl)ctlTemp;

                        for (intNum = 0; intNum < tabTemp.TabPages.Count; intNum++)
                        {
                            tabTemp.SelectedIndex = intNum;

                            //check each page for required fields
                            foreach (Control ctlTab in tabTemp.SelectedTab.Controls)
                            {
                                try
                                {
                                    if (ctlTab.Tag.ToString() == "1")
                                    {
                                        strTemp = ctlTab.Name.Substring(3, ctlTab.Name.Length - 3);

                                        if (ctlTab.Text.Length == 0)
                                        {
                                            strTemp = tabTemp.SelectedTab.Controls["LBL" + strTemp].Text;
                                            lstErrors.Add(strTemp);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //whoops!
                                }
                            }
                        }
                        //reset tab selected
                        tabTemp.SelectedIndex = 0;
                    }
                    if (ctlTemp.Tag.ToString() == "1")
                    {
                        strTemp = ctlTemp.Name.Substring(3, ctlTemp.Name.Length - 3);

                        if (ctlTemp.Text.Length == 0)
                        {
                            strTemp = frmTemp.Controls["LBL" + strTemp].Text;
                            lstErrors.Add(strTemp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //whoops! ignore on purpose only here because control does not have the TAG property
                }
            }

            if (lstErrors.Count != 0)
            {
              strTemp = "";
              //create messagebox to user showing missing required fields
              for (intNum = 0; intNum != lstErrors.Count; intNum++)
              {
                  strTemp = strTemp + lstErrors[intNum].ToString() + "\n";
              }

              MessageBox.Show("These Fields Are Missing Required Data:\n\n" + strTemp, "Required Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }

        //class end
    }
    }
