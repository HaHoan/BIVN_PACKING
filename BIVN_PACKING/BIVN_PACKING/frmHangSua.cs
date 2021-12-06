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
    public partial class frmHangSua : Form
    {
        USAPService.USAPWebServiceSoapClient _dbClient = new USAPService.USAPWebServiceSoapClient();
        private USAPService.BCLBFLMEntity _boxInfo = new USAPService.BCLBFLMEntity();
        private PVSService.Base_ModelsEntity _modelInfo = new PVSService.Base_ModelsEntity();
        private BIVNService.BIVNWebServiceSoapClient _bivnService = new BIVNService.BIVNWebServiceSoapClient();
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        int _boxQty = 0;
        User Data = new User();
        public frmHangSua()
        {
            InitializeComponent();
            DataLogin();
            this.dgrvListSerialInBox.AutoGenerateColumns = false;
        }
        public void DataLogin()
        {
            Data.UserName = Properties.Settings.Default.user;
        }
        private void ShowMessage(string key, string str_status, string str_message)
        {
            switch (key)
            {
                case "PASS":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkGreen; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkGreen; }));
                    break;
                case "FAIL":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkRed; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkRed; }));
                    break;
                case "NULL":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.DarkBlue; }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkBlue; }));
                    break;
                case "Wait":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = str_status; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));
                    break;
                case "Default":
                    this.BeginInvoke(new Action(() => { lblStatus.Text = @"[N\A]"; }));
                    this.BeginInvoke(new Action(() => { lblStatus.BackColor = Color.FromArgb(255, 128, 0); }));
                    this.BeginInvoke(new Action(() => { lblMessage.Text = "no results"; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.FromArgb(255, 128, 0); }));
                    break;
            }
        }
        List<Repair> _listSerialInBox = new List<Repair>();
        private void txtBoxid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    using (var db = new BIVNEntities())
                    {
                        dgrvListSerialInBox.DataSource = new List<Repair>();
                        dgrvListSerialInBox.Refresh();
                        string boxID = txtBoxid.Text.Trim();
                        if (string.IsNullOrEmpty(boxID))
                        {
                            ShowMessage("FAIL", "NG", $"Vui lòng nhập BoxID");
                            return;
                        }
                        _boxInfo = _dbClient.GetByBcNo(boxID);
                        if (_boxInfo == null)
                        {
                            ShowMessage("FAIL", "NG", $"Không tìm thấy Box [{ boxID}] trên hệ thống.\nVui lòng kiểm tra và thử lại.");
                            txtBoxid.SelectAll();
                            txtBoxid.Focus();
                            return;
                        }
                        _modelInfo =  _pvs_service.GetModelInfo(_boxInfo.PART_NO);
                        lblModel.Text = _boxInfo.PART_NO;
                        lblQtyBox.Text = Convert.ToInt32(_boxInfo.OS_QTY).ToString();
                        // lấy số lượng serial đã bắn / box
                        _listSerialInBox = db.Repairs.Where(m => m.BOXID == boxID).ToList();
                        _boxQty = _listSerialInBox.Count();
                        lblQtyBoxActual.Text = _boxQty.ToString();
                        ListAllSerialInBox();
                        if (_boxQty >= Convert.ToInt32(_boxInfo.OS_QTY))
                        {
                            ShowMessage("PASS", "OK", $"Thùng {boxID} đã đầy [{_boxQty}] / [{Convert.ToInt32(_boxInfo.OS_QTY)}].\nVui lòng bắn thùng khác!");
                            txtBoxid.SelectAll();
                            txtBoxid.Focus();
                            return;
                        }

                        ShowMessage("PASS", "OK", $"BoxID {boxID} OK.\nInsert Serial!");
                        txtBoxid.Enabled = false;
                        txbRepairer.Enabled = true;
                        txbRepairer.Focus();


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListAllSerialInBox()
        {
            this.BeginInvoke(new Action(() =>
            {
                dgrvListSerialInBox.DataSource = null;
                dgrvListSerialInBox.DataSource = _listSerialInBox;
                for (int i = 0; i < _listSerialInBox.Count;)
                {
                    this.dgrvListSerialInBox.Rows[i].Cells["NO"].Value = (++i).ToString();
                }
            }));
        }
     
        private void txtBarcode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtBoxid.Text))
                {
                    ShowMessage("FAIL", @"FAIL", $"Please enter BoxID");
                    txtBoxid.ResetText();
                    return;
                }
                
                var serial = txtBarcode.Text.Trim();
                if(_modelInfo != null && _modelInfo.Content != null && _modelInfo.Content_Length is int length)
                {
                  
                    if(serial.Substring(0, length) != _modelInfo.Content)
                    {
                        new Error($"Serial cần bắt đầu bằng {_modelInfo.Content}").ShowDialog();
                        return;
                    }
                 
                }
                
                try
                {
                    using (var db = new BIVNEntities())
                    {
                        var prod = db.Repairs.Where(m => m.SERIAL == txtBarcode.Text.Trim()).FirstOrDefault();
                        if(prod != null)
                        {
                            ShowMessage("FAIL", @"FAIL", $"Bản mạch " + txtBarcode.Text.Trim() + " đã bắn rồi!");
                            txtBarcode.ResetText();
                            return;
                        }
                        prod = new Repair()
                        {
                            BOXID = txtBoxid.Text.Trim(),
                            AMOUNT = Convert.ToInt32(_boxInfo.OS_QTY),
                            SERIAL = txtBarcode.Text.Trim(),
                            USERNAME = Data.UserName,
                            DATECREATE = _pvs_service.GetDateTime(),
                            MODEL = _boxInfo.PART_NO,
                            REPAIRER = txbRepairer.Text
                        };

                        db.Repairs.Add(prod);
                        db.SaveChanges();
                        ShowMessage("PASS", @"PASS", $"Serial [{prod.SERIAL}] Lưu thành công!");
                        _listSerialInBox.Add(prod);
                        ListAllSerialInBox();
                        _boxQty++;
                        lblQtyBoxActual.Text = _boxQty.ToString();
                        if (_boxQty == Convert.ToInt32(_boxInfo.OS_QTY))
                        {
                            ShowMessage("NULL", @"FULL", $"Số lượng bảng mạch trên thùng đã đầy.\nVui lòng bắn BoxID");
                            txtBarcode.ResetText();
                            txtBarcode.Enabled = false;
                            panelBOXID.Enabled = true;
                            txbRepairer.ResetText();
                            txtBoxid.Enabled = true;
                            txtBoxid.ResetText();
                            txtBoxid.Focus();
                            return;
                        }
                        txtBarcode.ResetText();
                        txtBarcode.Focus();
                    }

                }
                catch (Exception ex)
                {

                    ShowMessage("FAIL", @"FAIL", $"Error: {ex.Message}");
                    return;
                }

                
            }
        }

      
        private void txbRepairer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txbRepairer.Enabled = false;
                panelBarcode.Enabled = true;
                txtBarcode.Focus();
            }
           
        }

        private void llreset_Click(object sender, EventArgs e)
        {
            txtBoxid.Enabled = true;
            txtBoxid.ResetText();
            txtBoxid.Focus();
            txtBarcode.ResetText();
            
            lblQtyBox.Text = "0";
            lblQtyBoxActual.Text = "0";
            txbRepairer.ResetText();
            txbRepairer.Enabled = false;
            dgrvListSerialInBox.DataSource = null;
            dgrvListSerialInBox.Refresh();
            panelBarcode.Enabled = false;
        }
    }
}


