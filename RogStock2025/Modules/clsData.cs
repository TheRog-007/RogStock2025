using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Drawing.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using System.Net;
using System.Data.SqlTypes;

namespace RogStock2025.Modules
{
    internal static class clsData
    {                                                //data source = server name
        public static readonly string CNST_STR_ODBC = "Data Source=DESKTOP-06NTVAV;Initial Catalog=RogStock;Persist Security Info=True;User ID=sa;Password=RogSQLServer1";
        //tables
        public static readonly string CNST_STR_LOGIN = "Login";
        public static readonly string CNST_STR_LOC_TRN = "Loc_TRN";
        public static readonly string CNST_STR_LOT_TRN = "Lot_TRN";
        public static readonly string CNST_STR_LOGINCURRENT = "Login_Current";
        public static readonly string CNST_STR_STOCK_LOC = "Stock_Loc";
        public static readonly string CNST_STR_STOCKLOT = "Stock_Lot";
        public static readonly string CNST_STR_STOCK_ITEMS = "Stock_Items";
        public static readonly string CNST_STR_STOCKVENDORS = "Stock_Vendors";
        public static readonly string CNST_STR_STOCK_DESCRIPTION = "Stock_Description";
        public static readonly string CNST_STR_STOCK_MEDIA = "Stock_Media";
        public static readonly string CNST_STR_STOCK_PRODUCTFAMILY = "Stock_ProductFamily";
        public static readonly string CNST_STR_STOCK_UOM = "Stock_UOM";
        //select queries
        public static readonly string CNST_STR_SELECT_STOCK_ITEMS = "SELECT * FROM " + CNST_STR_STOCK_ITEMS + ";";
        public static readonly string CNST_STR_SELECT_STOCK_DESCRIPTION = "SELECT * FROM " + CNST_STR_STOCK_DESCRIPTION + ";";
        public static readonly string CNST_STR_SELECT_STOCK_MEDIA = "SELECT * FROM " + CNST_STR_STOCK_MEDIA + ";";


        struct typPassword
        {
            public int intLength;
        public List<char> aryContents;
        public List<int> aryOrder;
        }
        //used in current login functions
        private static string strLoggedInUser = "";
        private static string strLoggedInIP = "";

        private static string EncryptPassword(string strPassword)
        {
            /*
             Created 14/02/2025 By Roger Williams

             encrypts passed password

             VARS

             strPassword   - password

             returns encrypted password
            */

            int intNum = 0;
            char chrTemp;
            typPassword typPwd;
            int intLoc = 0;
            string strTemp = strPassword;
            byte[] arybyteTemp;

            //store password length
            typPwd.intLength = strTemp.Length;
            typPwd.aryContents = new List<char>();

            //store chars "as is"
            for (intNum = 0; intNum != strTemp.Length;intNum++)
            {
                typPwd.aryContents.Add(strTemp[intNum]);

            }
            //resize the order array to match password length
            typPwd.aryOrder = new List<int>(intNum);

            //encrypt the password by sorting the array by ASC order ASCII value
            intNum = 0;
            //get ASCII values for the string
            arybyteTemp =  Encoding.ASCII.GetBytes(strTemp);

            while (intNum != typPwd.aryContents.Count +1)
            {
                if (intNum != typPwd.aryContents.Count)
                {
                    if (arybyteTemp[intNum] > arybyteTemp[intNum+1]) 
                    {
                        chrTemp = typPwd.aryContents[intNum + 1];
                        typPwd.aryContents[intNum + 1] = typPwd.aryContents[intNum];
                        typPwd.aryContents[intNum] = chrTemp;
                        //reset counter
                        intNum = 1;
                    }
                    else
                    {
                        intNum++;
                    }
                }
                else
                {
                    intNum++;
                }
            }

            intNum = 0;

            //store location of where characters where in original string
            while (intNum != typPwd.aryContents.Count+1)
            {
                intLoc = strTemp.IndexOf(typPwd.aryContents[intNum]);
                //erase char from strTemp
                strTemp.Remove(intLoc, 1);
                strTemp.Insert(intLoc, " ");
                typPwd.aryOrder.Add(intLoc);
                intNum++;
            }

            //return encrypted string
            strTemp = "";
            strTemp = typPwd.aryContents.ToString();
            return strTemp;
        }
        private static string DecryptPassword(string strPassword)
        {
            /*
             Created 14/02/2025 By Roger Williams

             decrypts passed password

             VARS

             strPassword   - password

             returns unencrypted password
            */

            return "w";
         
        }

        public static bool CheckLogin(string strUser, string strPassword)
        {
            /*
             Created 14/02/2025 By Roger Williams

             checks passed user and password are correct

             VARS

             strUser       - user name
             strPassword   - password


            */
            SqlConnection ADOConn;
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strTemp = "";
            bool blnOk = false;
            
            try
            {
                using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    ADOConn.Open();
                    SQLCmd = ADOConn.CreateCommand();
                    SQLCmd.CommandText = "SELECT * FROM " + CNST_STR_LOGIN + " WHERE LOG_User ='" + strUser + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    if (SQLRead.Read())
                    {
                        //get password
                        strTemp = SQLRead["LOG_Password"].ToString();
                        //decrypt
                        //    strTemp = EncryptPassword(strTemp);
                        //compare with strPassword
                        if (strTemp == strPassword)
                        {
                            blnOk = true;
                        }
                        else
                        {
                            blnOk = false;
                        }
                    }
                    else
                    {
                        blnOk = false;
                    }

                    SQLRead.Close();
                    ADOConn.Close();
                    return blnOk;
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server\n\n"+ex.Message, "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CreateCurrentLoginRecord(string strUser)
        {
            /*
             Created 18/02/2025 By Roger Williams

             creates user logged in record in login_current


             VARS
            
             struser    - name of user

             stores struser in var strLoggedInUser for use by other functions

            */
            SqlConnection ADOConn;
            SqlCommand SQLCmd;

               string GetLocalIP()
               {
                /*
                 Created 18/02/2025 By Roger Williams

                 Gets PCs IP address

                 Modified VB code copied from the internet!

                */
                string strIP = "";
                string strHostName = "";
                string strHost = "";
                IPHostEntry IPHost;

                strHostName = Dns.GetHostName();
                IPHost = Dns.GetHostEntry(strHostName);

                foreach (IPAddress IPATemp in IPHost.AddressList)
                {
                    //look for IP4 address only
                    if (IPATemp.AddressFamily == AddressFamily.InterNetwork)
                    {
                        strIP = IPATemp.ToString();
                        //store for later use
                        strLoggedInIP = IPATemp.ToString();
                        return strIP;
                    }
                }
                return strIP;
               }

            try
            {
                //store for later use elsewhere
                strLoggedInUser = strUser;

                using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    ADOConn.Open();
                    SQLCmd = ADOConn.CreateCommand();
                    SQLCmd.CommandText = "INSERT INTO " + CNST_STR_LOGINCURRENT + " (LOGC_User, LOGC_PCIP)  VALUES ('" + strUser + "','" + GetLocalIP() + "');";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    ADOConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void DeleteCurrentLoginRecord()
        {
            /*
              Created 18/02/2025 By Roger Williams

              deletes user logged in record in login_current

              uses var strLoggedInUser for delete
             */
            SqlConnection ADOConn;
            SqlCommand SQLCmd;
            try
            { 
                using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    ADOConn.Open();
                    SQLCmd = ADOConn.CreateCommand();
                    SQLCmd.CommandText = "DELETE FROM " + CNST_STR_LOGINCURRENT + " WHERE LOGC_User = '" + strLoggedInUser + "' AND LOGC_PCIP = '" + strLoggedInIP + "';";
                    SQLCmd.CommandType = CommandType.Text;
                    SQLCmd.ExecuteNonQuery();
                    ADOConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Database - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public static bool CheckForChanges(DataSet DSTTemp, string strTable)
        {
            /*
              Created 19/02/2025 By Roger Williams
              
              Checks dataset table for changes returns true if modified

              DSTTemp   - dataset to work with
              STRTable  - table to work with

            */

            DSTTemp.AcceptChanges();

            if (DSTTemp.Tables[strTable].GetChanges() != null)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        public static void ClearTable(DataSet DSTTemp, string STRTable)
        {
            /*
              Created 19/02/2025 By Roger Williams
              
              Clears the dataset table contentsrd

              VARS

              DSTTemp   - dataset to work with
              STRTable  - table to work with

            */

            DSTTemp.Tables[STRTable].Clear();
        }
        public static void CreateNewRecord(DataSet DSTTemp, string STRTable, string strPrimaryKey, string strPrimaryKeyValue)
        {
            /*
              Created 19/02/2025 By Roger Williams
              
              Creates new record in dataset table 

              VARS

              DSTTemp            - dataset to work with
              STRTable           - table to work with
              STRPrimaryKey      - primary key field name
              STRPrimaryKeyValue - primary key field name
            */
            DataRow DARRow;

            DARRow = DSTTemp.Tables[STRTable].NewRow();
            //set primary key value
            DARRow[strPrimaryKey] = strPrimaryKeyValue;

            DSTTemp.Tables[STRTable].Rows.Clear();
            DSTTemp.Tables[STRTable].Rows.Add(DARRow);
        }
        public static void PopulateComboBoxes(ComboBox CMBTemp, string strTable, string strKeyField, string strKeyFieldValue)
        {
            /*
              Created 17/02/2025 By Roger Williams

              Populates the comboboxes with table values using first non
              identity seed as column, unless user specifies a key field
              and optional sort value

              VARS

              CMBTemp             - combobox to populate
              strTable            - table to read from
          
              Optional:
             
              strKeyField         - key field name 
              strKeyFieldValue    - key field value always handled as text 
                                    in a commercial system would also pass
                                    data type


            */

            SqlConnection ADOConn; 
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;

            //clear combo
            CMBTemp.Items.Clear();

            try
            {
                using (ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC))
                {
                    ADOConn.Open();
                    SQLCmd = ADOConn.CreateCommand();

                    if (strKeyField.Length == 0)
                    { 
                        SQLCmd.CommandText = "SELECT * FROM " + strTable + ";";
                    }
                    else
                    {
                        SQLCmd.CommandText = "SELECT * FROM " + strTable + " WHERE " + strKeyField + " = '" + strKeyFieldValue +"';";
                    }   
                    
                    SQLCmd.CommandType = CommandType.Text;
                    SQLRead = SQLCmd.ExecuteReader();

                    while (SQLRead.Read())
                    {
                        if (strTable == CNST_STR_LOC_TRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_LOT_TRN)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_ITEMS)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCKLOT)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCKVENDORS)
                        {
                            CMBTemp.Items.Add(SQLRead[2].ToString());
                        }
                        //if (strTable == CNST_STR_STOCK_DESCRIPTION)
                        //{
                        //    CMBTemp.Items.Add(SQLRead[1].ToString());
                        //}
                        if (strTable == CNST_STR_STOCK_LOC)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        //if (strTable == CNST_STR_STOCK_MEDIA)
                        //{

                        //}
                        if (strTable == CNST_STR_STOCK_PRODUCTFAMILY)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }
                        if (strTable == CNST_STR_STOCK_UOM)
                        {
                            CMBTemp.Items.Add(SQLRead[1].ToString());
                        }

                    }

                    SQLRead.Close();
                    ADOConn.Close();
                }
            }
            catch (Exception ex)
            {
                //Whoops!
                MessageBox.Show("Error Opening Table " + strTable +  " - Check SQL Server", "Database Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //class end
    }
}
