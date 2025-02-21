using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RogStock2025
{
    public partial class frmLogin : Form
    {
       private Graphics grpTemp;
       private Pen penTemp;
        public frmLogin()
        {
            InitializeComponent();
        }
        //other
        private void LoginUser()
        {
            /*
               Created 17/02/2025 By Roger Williams

               logins user in iv valid and creates record in login_current

             */
            Forms.frmMain frmTemp;

            if (Modules.clsData.CheckLogin(this.TXTUser.Text, this.TXTPassword.Text))
            {
                this.Visible = false;
                frmTemp = new Forms.frmMain();
                frmTemp.Visible = true;

                //create record in login_current
                Modules.clsData.CreateCurrentLoginRecord(this.TXTUser.Text);
            }
            else
            {
                MessageBox.Show("Invalid User Name or Password", "Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //form events
        private void BTNCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTNLogin_Click(object sender, EventArgs e)
        {
            LoginUser();  
        }

        private void frmLogin_Paint(object sender, PaintEventArgs e)
        {
            grpTemp = this.CreateGraphics();
            penTemp = new Pen(Color.Black, 1);
            penTemp.Color = Color.Black;
            grpTemp.DrawLine(penTemp, 0, 100, 300, 100);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
          

        }
    }
}
