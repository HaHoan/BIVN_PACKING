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
        public Action<string, string, string,string,string> updateAfterSetting;
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        private string Model;
      
        private USAPService.BCLBFLMEntity boxInfo;
        public frmSettingWork(USAPService.BCLBFLMEntity boxInfo)
        {
            InitializeComponent();
            this.boxInfo = boxInfo;
            lblWoNo.Text = Convert.ToInt32(boxInfo.TN_NO).ToString();
            lblModel.Text = boxInfo.PART_NO;
            this.Model = boxInfo.PART_NO;
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

            var modelInfo = _pvs_service.GetModelInfo(this.Model);
            
            if (modelInfo != null)
            {
                if (modelInfo.Content_Length is int contentLength)
                {
                    if (modelInfo.Is_Hexa == true)
                    {
                        if (!long.TryParse(serialStart.Right(serialStart.Length - contentLength), System.Globalization.NumberStyles.HexNumber, null, out decimaStart))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                            return;
                        }
                        if (!long.TryParse(serialEnd.Right(serialEnd.Length - contentLength), System.Globalization.NumberStyles.HexNumber, null, out decimaEnd))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                            return;
                        }
                    }
                    else
                    {
                        if (!long.TryParse(serialStart.Right(serialStart.Length - contentLength), System.Globalization.NumberStyles.Number, null, out decimaStart))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                            return;
                        }
                        if (!long.TryParse(serialEnd.Right(serialEnd.Length - contentLength), System.Globalization.NumberStyles.Number, null, out decimaEnd))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                            return;
                        }
                    }
                   
                }
                else
                {
                    ShowMessage("FAIL", @"FAIL", $"Xem lại thông tin Model");
                    return;
                }

            }

            else
            {
                ShowMessage("FAIL", @"FAIL", $"Chưa có thông tin Model!");
                return;
            }
            if (decimaEnd <= decimaStart)
            {
                ShowMessage("FAIL", @"FAIL", $"Dải serial không hợp lệ!");
                return;
            }
            long numberWork = decimaEnd - decimaStart + 1;
            if (numberWork != int.Parse(qty))
            {
                ShowMessage("FAIL", @"FAIL", $"Dải serial có "+numberWork + " bản mạch. Khác với số lượng work đã nhập");
                return;
            }
           
            updateAfterSetting(lblWoNo.Text.Trim(),lblModel.Text.Trim(),qty, serialStart, serialEnd);
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
