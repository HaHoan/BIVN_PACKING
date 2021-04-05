using BIVN_PACKING.Business;
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
    public partial class Register : Form
    {
        BIVNEntities _db = new BIVNEntities();
        User Data = new User();
        public Register()
        {
            DataLogin();
            InitializeComponent();
        }
        public void DataLogin()
        {
            Data.UserName = Properties.Settings.Default.user;
        }

        private void btnRegister_Click(object sender, EventArgs e)
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
                lblthongbao.Text = "Employee code must be 5 characters";
                return;
            }
            if (txtPassword.Text.Length < 3)
            {
                lblthongbao.Text = "Password must be greater than 3 characters";
                return;
            }
            string password = Security.MD5Hash(txtPassword.Text);
            //string createPass = Security.MD5Hash("umcvn");
            var user = new Users() { USER_NAME = txtUsername.Text, PASSWORD = password };
            var checkuser = _db.Users.ToList();
            var boolcheck = false;
            foreach (var item in checkuser)
            {
                if (item.USER_NAME == txtUsername.Text)
                {
                    boolcheck = true;
                }
            }
            if (boolcheck == false)
            {
                user.CREATE_DATE = DateTime.Now;
                if (adadmin.Checked == true)
                {
                    user.IS_ADMIN = true;
                }
                else
                {
                    user.IS_ADMIN = false;
                }
                user.CREATE_BY = Data.UserName;
                user.ENABLE = true;
                _db.Users.Add(user);
                _db.SaveChanges();
                Close();
            }
            else
            {
                lblthongbao.Text = "Account already exists!";
                return;
            }
           
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ActiveControl = txtUsername;
        }

        private void txtUsername_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                adadmin.Focus();
            }
        }

        private void adadmin_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister.Focus();
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
     (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
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
