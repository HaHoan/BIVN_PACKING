using BIVN_PACKING.Business;
using BIVN_PACKING.DAL;
using BIVN_PACKING.Entitis;
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
         Database _database = new Database();
        BIVNEntities _db = new BIVNEntities();
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
            if (txtUsername.Text == "")
            {
                lblthongbao.Text = "Please enter the staff code";
                return;
            }
            if (txtPassword.Text == "")
            {
                lblthongbao.Text = "Please enter a password";
                return;
            }
            if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 5)
            {
                lblthongbao.Text = "Employee code is 5 characters long";
                return;
            }
            if (txtPassword.Text.Length < 3)
            {
                lblthongbao.Text = "Password must be greater than 3 characters";
                return;
            }
            string password = Security.MD5Hash(txtPassword.Text);
            //string createPass = Security.MD5Hash("umcvn");
            Users user = new Users() { USER_NAME = txtUsername.Text, PASSWORD = password };
            var UserEntity = _db.Users.Where(x => x.USER_NAME == txtUsername.Text.Trim() && x.PASSWORD == password.Trim()).FirstOrDefault();
            if (UserEntity == null)
            {
                lblthongbao.Text = "Account does not exist";
                return;
            }
            if (UserEntity.ENABLE == false)
            {
                lblthongbao.Text = "The account has been locked, please contact PE-IT for assistance";
                return;
            }
            else
            {
                Properties.Settings.Default.user = txtUsername.Text;
                Properties.Settings.Default.Save();
                //MessageBox.Show("Đăng nhập thành công", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                new frmMain().ShowDialog();
                txtPassword.ResetText();
               
                this.Close();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ActiveControl = txtUsername;
            //txtUsername.Text = Properties.Settings.Default.user;
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
      //      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      //(e.KeyChar != '.'))
      //      {
      //          e.Handled = true;
      //      }

      //      // only allow one decimal point
      //      if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
      //      {
      //          e.Handled = true;
      //      }
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
