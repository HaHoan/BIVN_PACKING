using BIVN_PACKING.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BIVN_PACKING
{
    public partial class frmLogin : Form
    {
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        public frmLogin()
        {
            InitializeComponent();
        }
        public void DataLogin()
        {
            User Data = new User();
            Data.UserName = Properties.Settings.Default.user;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var UserEntity = _pvs_service.CheckUserLogin(txtUsername.Text, txtPassword.Text);
            if (UserEntity == null)
            {
                lblthongbao.Text = "Account does not exist";
                return;
            }
          
            Properties.Settings.Default.user = txtUsername.Text;
            Properties.Settings.Default.userID = UserEntity.ID;
            Properties.Settings.Default.Save();
            this.Hide();
            new frmMain().Show();
            txtPassword.ResetText();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ActiveControl = txtUsername;
            txtUsername.SelectAll();
        }

        private void txtUsername_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.SelectAll();
                txtPassword.Focus();

            }

        }

        private void txtPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }

        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "vnumc2019";
                txtPassword.ForeColor = Color.Silver;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "vnumc2019")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }
        }
    }
}
