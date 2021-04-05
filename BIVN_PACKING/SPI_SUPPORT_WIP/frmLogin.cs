using SPI_SUPPORT_WIP.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPI_SUPPORT_WIP
{
    public partial class frmLogin : Form
    {
        SOFTWAREEntities _db = new SOFTWAREEntities();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var checkendsave = _db.USERs.Where(x => x.USERNAME == txtuser.Text && x.PASSWORD == txtpass.Text).FirstOrDefault();
            if (checkendsave != null)
            {
                Properties.Settings.Default.ONOFF = true;
                Properties.Settings.Default.Save();
                Close();
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtuser.ResetText();
                txtpass.ResetText();
                txtuser.Focus();
                return;
            }
        }

        private void txtuser_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpass.Focus();
            }
        }

        private void txtpass_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtuser;
        }
    }
}
