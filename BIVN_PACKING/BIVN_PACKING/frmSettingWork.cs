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
   
    public partial class frmSettingWork : Form
    {
        public Action<string, string, string> updateAfterSetting;
        private List<string> _specialProduct = new List<string>()
        {
            "D0092S001","D00KHH004","D00KHJ001","D00KHL002"
        };
        private BIVN.BCLBFLMEntity boxInfo;
        public frmSettingWork(string WoNo, string Model, BIVN.BCLBFLMEntity boxInfo)
        {
            InitializeComponent();
            lblWoNo.Text = WoNo;
            lblModel.Text = Model;
            this.boxInfo = boxInfo;
            tbWoQty.SelectAll();
            tbWoQty.Focus();
        }
        private void txtWO_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
        private void SaveChanges()
        {
            string qty = tbWoQty.Text.Trim();
            string serialStart = tbSerialStart.Text.Trim();
            string serialEnd = tbSerialEnd.Text.Trim();
            if (string.IsNullOrEmpty(qty))
            {
                tbWoQty.Focus();
                return;
            }
            if (string.IsNullOrEmpty(serialStart))
            {
                tbSerialStart.Focus();
                return;
            }
            if (string.IsNullOrEmpty(serialEnd))
            {
                tbSerialEnd.Focus();
                return;
            }
            long decimaStart, decimaEnd;
            string boxID = boxInfo.BC_NO;
            string wo = lblWoNo.Text.Trim();
            if (_specialProduct.Contains(boxInfo.PART_NO))
            {
                decimaStart = long.Parse(serialStart.Right(6));
                decimaEnd = long.Parse(serialEnd.Right(6));
            }
            else
            {

                if (!long.TryParse(serialStart, System.Globalization.NumberStyles.HexNumber, null, out decimaStart))
                {
                    ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                    return;
                }
                if (!long.TryParse(serialEnd, System.Globalization.NumberStyles.HexNumber, null, out decimaEnd))
                {
                    ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                    return;
                }
            }
            if (decimaEnd <= decimaStart)
            {
                ShowMessage("FAIL", @"FAIL", $"Dải serial không hợp lệ!");
                return;
            }
           
            updateAfterSetting(qty, serialStart, serialEnd);
            Close();
        }

        private void ShowMessage(string v1, string v2, string v3)
        {
            lblError.Text = v3;
        }

        private void tbWoQty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(tbWoQty.Text))
            {
                tbSerialStart.SelectAll();
                tbSerialStart.Focus();
            }
        }

        private void tbSerialEnd_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveChanges();
            }
           
        }

        private void tbSerialStart_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(tbSerialStart.Text))
            {
                tbSerialEnd.SelectAll();
                tbSerialEnd.Focus();
            }
        }
    }
}
