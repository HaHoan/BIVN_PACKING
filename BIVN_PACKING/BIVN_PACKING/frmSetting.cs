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
    public partial class frmSetting : Form
    {
        BIVNEntities _db = new BIVNEntities();
        public frmSetting()
        {
            DataConnect();
            InitializeComponent();
            GetTaskWindows();
        }
        private void GetTaskWindows()
        {
            // Get the desktopwindow handle
            int nDeshWndHandle = NativeWin32.GetDesktopWindow();
            // Get the first child window
            int nChildHandle = NativeWin32.GetWindow(nDeshWndHandle, NativeWin32.GW_CHILD);
            while (nChildHandle != 0)
            {
                //If the child window is this (SendKeys) application then ignore it.
                if (nChildHandle == this.Handle.ToInt32())
                {
                    nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
                }

                // Get only visible windows
                if (NativeWin32.IsWindowVisible(nChildHandle) != 0)
                {
                    StringBuilder sbTitle = new StringBuilder(1024);
                    // Read the Title bar text on the windows to put in combobox
                    NativeWin32.GetWindowText(nChildHandle, sbTitle, sbTitle.Capacity);
                    string sWinTitle = sbTitle.ToString();
                    {
                        if (sWinTitle.Length > 0)
                        {
                            cbbprocess.Items.Add(sWinTitle);
                            //cbbsubprocess.Items.Add(sWinTitle);
                        }
                    }
                }
                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }

            DataConnect();
        }
        public void DataConnect()
        {
            SearchView InfoData = new SearchView();
            InfoData.timesleep = Properties.Settings.Default.timesleep;
            InfoData.model = Properties.Settings.Default.model;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.model = cbbmodel.Text;
            Properties.Settings.Default.timesleep = int.Parse(txttimesleep.Text);
            Properties.Settings.Default.Save();
            //MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //cbbmodel.Text = Properties.Settings.Default.model;
            txttimesleep.Text = Properties.Settings.Default.timesleep.ToString();
        }



        private void txttimesleep_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
