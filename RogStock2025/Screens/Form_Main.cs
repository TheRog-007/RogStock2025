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
using RogStock2025.Screens;

namespace RogStock2025.Forms
{
    public partial class frmMain : Form
    {
        struct typPassword
        {
            public List<char> aryContents;
            public List<int> aryOrder;
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SqlCommand SQLCmd;
            typPassword typTemp;
            char chrTemp;

            typTemp.aryContents = new List<char>();
            typTemp.aryOrder = new List<int>();

            typTemp.aryOrder.Add(4);
            typTemp.aryOrder.Add(3);
            typTemp.aryOrder.Add(2);
            typTemp.aryOrder.Add(1);

            chrTemp = Convert.ToChar("A");
            typTemp.aryContents.Add(chrTemp);
            chrTemp = Convert.ToChar("B");
            typTemp.aryContents.Add(chrTemp);
            chrTemp = Convert.ToChar("C");
            typTemp.aryContents.Add(chrTemp);
            chrTemp = Convert.ToChar("D");
            typTemp.aryContents.Add(chrTemp);

            SQLCmd = ADOConn.CreateCommand();
            SQLCmd.CommandType = CommandType.Text;
       //     SQLCmd.CommandText = "UPDATE Login SET LOG_Test =" + Convert.to typTemp + " WHERE LOG_User = 'test'";
            ADOConn.Open();
            SQLCmd.ExecuteNonQuery();

            ADOConn.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection ADOConn = new SqlConnection(Modules.clsData.CNST_STR_ODBC);
            SqlCommand SQLCmd;
            SqlDataReader SQLRead;
            string strTemp = "";
            bool blnOk = false;

            ADOConn.Open();
            SQLCmd = ADOConn.CreateCommand();
            SQLCmd.CommandText = "SELECT * FROM Login WHERE LOG_User ='test'";
            SQLCmd.CommandType = CommandType.Text;
            SQLRead = SQLCmd.ExecuteReader();

        
        }
        private void MNUStockItems_Click(object sender, EventArgs e)
        {
            frmStockItems frmTemp;
            ToolStripMenuItem MNUTemp;

            frmTemp = new frmStockItems();

            MNUTemp = (ToolStripMenuItem)sender;
            Modules.clsView.AddToWindowMenu(MNUTemp.Text);
            //set form caption to match menu
            frmTemp.Text = MNUTemp.Text;
            frmTemp.Visible = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
           Modules.clsData.DeleteCurrentLoginRecord();
        }

        private void MNUStockDescription_Click(object sender, EventArgs e)
        {

        }

        private void MNUStockLocation_Click(object sender, EventArgs e)
        {
            frmLocMaintenance frmTemp;
            ToolStripMenuItem MNUTemp;

            frmTemp = new frmLocMaintenance();

            MNUTemp = (ToolStripMenuItem)sender;
            Modules.clsView.AddToWindowMenu(MNUTemp.Text);
            //set form caption to match menu
            frmTemp.Text = MNUTemp.Text;
            frmTemp.Visible = true;
        }

        private void adjustQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdjustQuantity frmTemp;
            ToolStripMenuItem MNUTemp;

            frmTemp = new frmAdjustQuantity();

            MNUTemp = (ToolStripMenuItem)sender;
            Modules.clsView.AddToWindowMenu(MNUTemp.Text);
            //set form caption to match menu
            frmTemp.Text = MNUTemp.Text;
            frmTemp.Visible = true;
        }
        //class end
    }
}
