using AutoIt;
using BIVN_PACKING.Business;
using BIVN_PACKING.PVSService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIVN_PACKING
{
    public partial class frmMain : Form
    {
        USAPService.USAPWebServiceSoapClient _dbClient = new USAPService.USAPWebServiceSoapClient();
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        private BIVNService.BIVNWebServiceSoapClient _bivnService = new BIVNService.BIVNWebServiceSoapClient();
        int _woQty = 0, _boxQty = 0;
        User Data = new User();
        SearchView LinkData = new SearchView();
        List<string> listTask = new List<string>();
        private USAPService.BCLBFLMEntity _boxInfo = new USAPService.BCLBFLMEntity();

        private BackgroundWorker bgrwSoftInfo;

        private void ControlClick()
        {

            string mainTitle = "Line Production";
            string classControl = "[NAME:BtIncrease]";
            var mainHandle = AutoIt.AutoItX.WinGetHandle(mainTitle);
            var controlHandle = AutoIt.AutoItX.ControlGetHandle(mainHandle, classControl);
            var onscreenkeyboard = "On-Screen Keyboard";
            var oskHandle = AutoItX.WinGetHandle(onscreenkeyboard);
            AutoItX.WinSetOnTop(oskHandle, 1);
            AutoItX.ControlClick(mainHandle, controlHandle);

        }
        public frmMain()
        {
            InitializeComponent();
            DataLogin();
            lblVersion.Text = Common.GetRunningVersion();
            bgrwSoftInfo = new BackgroundWorker();
            bgrwSoftInfo.DoWork += BgrwSoftInfo_DoWork;
            bgrwSoftInfo.RunWorkerCompleted += BgrwSoftInfo_RunWorkerCompleted;
            bgrwSoftInfo.RunWorkerAsync();
            FileShare.Connect(FileShare.PATH, new System.Net.NetworkCredential(FileShare.USER, FileShare.PASSWORD));
            this.dgrvListSerialInBox.AutoGenerateColumns = false;
        }

        private void BgrwSoftInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as Base_Soft_InfoEntity;
            var rs = _pvs_service.RegisterSoftInfo(result);
            lblSoftInfo.Text = rs ? "OK" : "NG";
        }

        private void BgrwSoftInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            var param = new Base_Soft_InfoEntity()
            {
                ID = Common.GetGUIID(),
                HOST_NAME = Environment.MachineName,
                MAC_ADDRESS = Common.GetMACAddress(),
                IP_ADDRESS = Common.GetLocalIPAddress(),
                IS_WINDOWS_64 = Environment.Is64BitOperatingSystem,
                LOCAL_USER = Environment.UserName,
                SOFT_NAME = Text,
                UPDATE_TIME = _pvs_service.GetDateTime(),
                VERSION = Common.GetRunningVersion(),
                WINDOWS_EDITION = Common.GetOSFriendlyName(),
                IS_WINDOWS_ACTIVE = Common.IsWindowsActivated()
            };
            param.ID += param.MAC_ADDRESS;
            e.Result = param;
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
                            listTask.Add(sWinTitle);
                            //cbbsubprocess.Items.Add(sWinTitle);
                        }
                    }
                }
                // Look for the next child.
                nChildHandle = NativeWin32.GetWindow(nChildHandle, NativeWin32.GW_HWNDNEXT);
            }
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtBoxid.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter BoxID");
                return;
            }
            if (txbWoNo.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter WoNo");
                return;
            }

            if (txbWoQty.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter WO");
                return;
            }
            if (txbSerialStart.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter SerialStart");
                return;
            }
            if (txbSeriaEnd.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter SeriaEnd");
                return;
            }
            if (int.Parse(txbWoNo.Text) == 0)
            {
                ShowMessage("FAIL", @"FAIL", "Số lượng thùng không chính xác");
                return;
            }

            if (txbWoQty.Text.Length > 10)
            {
                ShowMessage("FAIL", @"FAIL", $"Số lượng WO [{txbWoQty.Text}] nhỏ hơn 10 ký tự");
                txbWoQty.Focus();
                return;
            }

            string boxID = txtBoxid.Text.Trim();
            var getFromData = _dbClient.GetByBcNo(boxID);
            if (getFromData == null)
            {
                MessageBox.Show($"Không tìm thấy Box [{boxID}] trên hệ thống. Vui lòng kiểm tra và thử lại!", "Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBoxid.ResetText();
                txtBoxid.Focus();
                return;
            }


            string serialstart = txbSerialStart.Text;
            string serialend = txbSeriaEnd.Text;
            string serial = txtBarcode.Text;
            try
            {
                //string lengserial = serial.Substring(serialstart.Length - 4);
                string lengstart = serialstart.Substring(serialstart.Length - 9);
                string lengend = serialend.Substring(serialend.Length - 9);
                long output;
                var hexcheckstart = long.TryParse(lengstart, System.Globalization.NumberStyles.HexNumber, null, out output);
                var hexcheckend = long.TryParse(lengend, System.Globalization.NumberStyles.HexNumber, null, out output);
                if (hexcheckstart == true && hexcheckend == true)
                {
                    var hexstart = long.Parse(lengstart, System.Globalization.NumberStyles.HexNumber);
                    var hexend = long.Parse(lengend, System.Globalization.NumberStyles.HexNumber);
                    if (hexstart > hexend)
                    {
                        ShowMessage("FAIL", @"FAIL", $"Đổi vị trí [{serialend}] cho [{serialstart}]");
                        txtBarcode.ResetText();
                        return;
                    }

                }
                else
                {
                    string lengstarthex = serialstart.Substring(serialstart.Length - 6);
                    string lengendhex = serialend.Substring(serialend.Length - 6);
                    if (int.Parse(lengstarthex) > int.Parse(lengendhex))
                    {
                        ShowMessage("FAIL", @"FAIL", $"Đổi vị trí [{serialend}] cho [{serialstart}]");
                        txtBarcode.ResetText();
                        return;
                    }
                }

                if (txbWoQty.Text != "" && txbModel.Text != "" && txbSerialStart.Text != "" && txbSeriaEnd.Text != "")
                {
                    txtBoxid.Enabled = false;
                    panelBarcode.Enabled = true;
                    txbWoQty.Enabled = false;
                    txbModel.Enabled = false;
                    txbSerialStart.Enabled = false;
                    txbSeriaEnd.Enabled = false;
                    txtBarcode.Enabled = true;

                    txtBarcode.Focus();
                    ShowMessage("Wait", @"WAIT", "Waiting for Serial...");
                }
            }
            catch
            {
                ShowMessage("FAIL", @"FAIL", $"Không thể convert Serial");
                return;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            txbWoQty.Enabled = true;
            txbModel.Enabled = true;
            txbSerialStart.Enabled = true;
            txbSeriaEnd.Enabled = true;
            ClearAll();
            panelBOXID.Enabled = true;
            txtBoxid.Enabled = true;
            panelBarcode.Enabled = false;
            groupBox1.Enabled = true;

            ShowMessage("Default", @"[N\A]", "no result");
            txtBoxid.Focus();
            txtBarcode.ResetText();
        }

        private void lblRefresh_Click(object sender, EventArgs e)
        {
            //txtSerial.Text = "";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.ActiveControl = txtBoxid;
            tstAccount.Text = Data.UserName;
            txbModel.Text = "";

        }

        public void DataLogin()
        {
            Data.UserName = Properties.Settings.Default.user;
        }

        private void resetnumber_Click(object sender, EventArgs e)
        {

        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            //Produce aa = new Produce() { BOXID = txtBoxid.Text};
            //_db.Produce.Add(aa);
            //_db.SaveChanges();
        }

        private List<BIVNService.BIVNPackEntity> _listSerialInBox;
        int _woQtyActual = 0;
        private void txtBoxid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgrvListSerialInBox.DataSource = new List<BIVNService.BIVNPackEntity>();
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

                    // lấy tên wo thông qua box
                    var woUsap = Convert.ToInt32(_boxInfo.TN_NO).ToString();

                    // list các serial trong wo đã bắn
                    var listWoActual = _bivnService.GetListPack("", "", woUsap, "").ToList();

                    // số lượng wo thực tế đã bắn
                    _woQtyActual = listWoActual.Count();

                    // nếu wo đã được bán rôi
                    if (listWoActual.Count() != 0)
                    {
                        // lấy thông tin wo 
                        var woInfo = listWoActual.FirstOrDefault();

                        // lấy số lượng serial cần bắn / wo
                        _woQty = int.Parse(woInfo.WO);

                        // nếu số lượng thực tế lớn hơn thì thông báo đầy và nhập lại box mới
                        if (_woQty <= _woQtyActual)
                        {
                            ShowMessage("PASS", "FULL", $"[{woUsap}] Đã đầy Wo [{_woQtyActual}] / [{_woQty}]");
                            txtBoxid.ResetText();
                            txtBoxid.Focus();
                            return;
                        }

                        // các thông tin của wo
                        lblQtyWO.Text = txbWoQty.Text = _woQty.ToString();
                        lblQtyWoActual.Text = _woQtyActual.ToString();
                        txbSerialStart.Text = woInfo.SERIAL_START.ToString();
                        txbSeriaEnd.Text = woInfo.SERIAL_END.ToString();
                        txbWoNo.Text = woUsap;
                        txbModel.Text = boxID;
                        lblQtyBox.Text = Convert.ToInt32(_boxInfo.OS_QTY).ToString();

                        // lấy số lượng serial đã bắn / box
                        _listSerialInBox = _bivnService.GetListPack(boxID, "", woUsap, "").ToList();
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
                        panelBarcode.Enabled = true;
                        txtBarcode.Focus();
                    }
                    else
                    {
                        ShowMessage("Wait", "Wait", $"Please complete infomation!");
                        frmSettingWork frmSettingWork = new frmSettingWork(_boxInfo);
                        frmSettingWork.updateAfterSetting = (woUsapSettting, modelSetting, qty, start, end) =>
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                lblQtyWO.Text = txbWoQty.Text = qty.ToString();
                                lblQtyWoActual.Text = "0";
                                txbSerialStart.Text = start;
                                txbSeriaEnd.Text = end;
                                txbWoNo.Text = woUsapSettting;
                                txbModel.Text = modelSetting;
                                lblQtyBox.Text = Convert.ToInt32(_boxInfo.OS_QTY).ToString();
                                _boxQty = 0;
                                lblQtyBoxActual.Text = _boxQty.ToString();
                                _listSerialInBox = new List<BIVNService.BIVNPackEntity>();
                                ListAllSerialInBox();
                                txtBoxid.Enabled = false;
                                panelBarcode.Enabled = true;
                                txtBarcode.Focus();
                             
                            }));


                        };
                        frmSettingWork.ShowDialog();

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
        private void SettingWorkInfo(string woUsap, string boxID, string Wo, string serialStart, string serialEnd)
        {
            try
            {


                lblQtyWO.Text = txbWoQty.Text = Wo.ToString();
                lblQtyWoActual.Text = _woQtyActual.ToString();
                txbSerialStart.Text = serialStart.ToString();
                txbSeriaEnd.Text = serialEnd.ToString();
                txbWoNo.Text = woUsap;
                txbModel.Text = boxID;
                lblQtyBox.Text = Convert.ToInt32(_boxInfo.OS_QTY).ToString();
                lblQtyBoxActual.Text = _boxQty.ToString();

                _listSerialInBox = _bivnService.GetListPack(boxID, "", woUsap, "").ToList();
                _boxQty = _listSerialInBox.Count();
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
                panelBarcode.Enabled = true;
                txtBarcode.Focus();
            }
            catch (Exception e)
            {
                ShowMessage("FAIL", @"FAIL", e.Message.ToString());

            }

        }
        private void txtWoNo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = (e.KeyChar == (char)Keys.Space);

        }

        private void txtSerialStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txtSeriaEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void llreset_Click(object sender, EventArgs e)
        {
            txtBoxid.Enabled = true;
            txtBoxid.ResetText();
            txtBoxid.Focus();
            txtBarcode.ResetText();
            lblQtyWO.Text = "0";
            lblQtyWoActual.Text = "0";
            lblQtyBox.Text = "0";
            lblQtyBoxActual.Text = "0";
            txbWoQty.ResetText();
            txbWoNo.ResetText();
            txbSerialStart.ResetText();
            txbSeriaEnd.ResetText();
            txbModel.ResetText();
            dgrvListSerialInBox.DataSource = null;
            dgrvListSerialInBox.Refresh();
            panelBarcode.Enabled = false;
        }

        private void txtWO_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txbModel.Focus();
            }
        }


        private void txtModel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txbSerialStart.Focus();
            }
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
                var serialstart = txbSerialStart.Text.Trim();
                var serialend = txbSeriaEnd.Text.Trim();
                var serial = txtBarcode.Text.Trim();
                long decimaStart, decimaEnd, decimaSerial;
                string boxID = txtBoxid.Text.Trim();
                string wo = txbWoNo.Text.Trim();
                var modelInfo = _pvs_service.GetModelInfo(_boxInfo.PART_NO);
                if (modelInfo != null)
                {
                    if (modelInfo.Content_Length is int contentLength)
                    {
                        if (!long.TryParse(serialstart.Right(serialstart.Length - contentLength), System.Globalization.NumberStyles.HexNumber, null, out decimaStart))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                            return;
                        }
                        if (!long.TryParse(serialend.Right(serialend.Length - contentLength), System.Globalization.NumberStyles.HexNumber, null, out decimaEnd))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                            return;
                        }
                        if (!long.TryParse(serial.Right(serial.Length - contentLength), System.Globalization.NumberStyles.HexNumber, null, out decimaSerial))
                        {
                            ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                            return;
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
                    if (!long.TryParse(serialstart, System.Globalization.NumberStyles.HexNumber, null, out decimaStart))
                    {
                        ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                        return;
                    }
                    if (!long.TryParse(serialend, System.Globalization.NumberStyles.HexNumber, null, out decimaEnd))
                    {
                        ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                        return;
                    }
                    if (!long.TryParse(serial, System.Globalization.NumberStyles.HexNumber, null, out decimaSerial))
                    {
                        ShowMessage("FAIL", @"FAIL", $"Serial không hợp lệ!");
                        txtBarcode.SelectAll();
                        txtBarcode.Focus();
                        return;
                    }
                }
                if (decimaEnd <= decimaStart)
                {
                    ShowMessage("FAIL", @"FAIL", $"Dải serial không hợp lệ!");
                    return;
                }
                if (decimaSerial > decimaEnd || decimaSerial < decimaStart)
                {
                    ShowMessage("FAIL", @"FAIL", $"Serial không nằm trong dải cho phép!");
                    return;
                }
                var serialExist = _bivnService.BarcodeExist(serial);
                if (serialExist)
                {
                    ShowMessage("FAIL", @"FAIL", $"Serial đã tồn tại.\nVui lòng bắn Serial khác.");
                    txtBarcode.SelectAll();
                    txtBarcode.Focus();
                    return;
                }
                BIVNService.BIVNPackEntity prod = new BIVNService.BIVNPackEntity()
                {
                    BOXID = txtBoxid.Text.Trim(),
                    NAME_WO = txbWoNo.Text.Trim(),
                    WO = txbWoQty.Text.Trim(),
                    MODEL = txbModel.Text.Trim(),
                    SERIAL_START = txbSerialStart.Text.Trim(),
                    SERIAL_END = txbSeriaEnd.Text.Trim(),
                    AMOUNT = lblQtyBox.Text.Trim(),
                    SERIAL = txtBarcode.Text.Trim(),
                    USER_NAME = Data.UserName,
                    DATECREATE = _pvs_service.GetDateTime()
                };
                try
                {
                    _bivnService.SaveForm("", prod);
                    ShowMessage("PASS", @"PASS", $"Serial [{prod.SERIAL}] Lưu thành công!");
                }
                catch (Exception ex)
                {

                    ShowMessage("FAIL", @"FAIL", $"Error: {ex.Message}");
                    return;
                }
                ControlClick();

                _listSerialInBox.Add(prod);
                ListAllSerialInBox();
                _boxQty++;
                _woQtyActual++;
                lblQtyBoxActual.Text = _boxQty.ToString();
                lblQtyWoActual.Text = _woQtyActual.ToString();
                if (_boxQty == Convert.ToInt32(_boxInfo.OS_QTY))
                {
                    ShowMessage("NULL", @"FULL", $"Số lượng bảng mạch trên thùng đã đầy.\nVui lòng bắn BoxID");
                    txtBarcode.ResetText();
                    panelBOXID.Enabled = true;
                    txtBoxid.Enabled = true;
                    txtBoxid.ResetText();
                    txtBoxid.Focus();
                    return;
                }

                txtBarcode.ResetText();
                txtBarcode.Focus();
            }
        }

        private void txtBoxid_TextChanged(object sender, EventArgs e)
        {
            ShowMessage("Wait", @"WAIT", "Waiting for Box ID...");
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
            new frmSearch().Show();
        }

        private void lblResetBarcode_Click(object sender, EventArgs e)
        {
            txtBarcode.ResetText();
            txtBarcode.Focus();
            ShowMessage("Wait", @"WAIT", "Waiting for Serial...");
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnTestSerial_Click(object sender, EventArgs e)
        {
            /*
            long decimaStart, decimaEnd, decimaSerial;
            var serialstart = "44E002267446";
            var serialend = "44E2002269745";

            if (!long.TryParse(serialstart, System.Globalization.NumberStyles.HexNumber, null, out decimaStart))
            {
                ShowMessage("FAIL", @"FAIL", $"Serial Start không hợp lệ!");
                return;
            }
            if (!long.TryParse(serialend, System.Globalization.NumberStyles.HexNumber, null, out decimaEnd))
            {
                ShowMessage("FAIL", @"FAIL", $"Serial End không hợp lệ!");
                return;
            }

            var list = _db.Produce.Where(m => m.NAME_WO == "2000722912").ToList();
            foreach (var serial in list)
            {
                if (!long.TryParse(serial.SERIAL, System.Globalization.NumberStyles.HexNumber, null, out decimaSerial))
                {
                    Console.WriteLine("Serial End không hợp lệ!");
                    Console.Write(serial.SERIAL);
                    return;
                }
                if (decimaSerial > decimaEnd || decimaSerial < decimaStart)
                {
                    Console.WriteLine("Serial không nằm trong dải cho phép!");
                    Console.Write(serial.SERIAL);
                    return;
                }
            }
            MessageBox.Show("OK");*/

            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.FilterIndex = 1;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                Common.ImportModel(sFileName);
            }

        }

        private void dgrvListSerialInBox_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //ContextMenu m = new ContextMenu();
                //m.MenuItems.Add(new MenuItem("Copy"));

                //int currentMouseOverRow = dgrvListSerialInBox.HitTest(e.X, e.Y).RowIndex;
                //int rowindex = dgrvListSerialInBox.CurrentCell.RowIndex;
                //int columnindex = dgrvListSerialInBox.CurrentCell.ColumnIndex;

                //string text = dgrvListSerialInBox.Rows[rowindex].Cells[columnindex].Value.ToString();

                //m.Show(dgrvListSerialInBox, new Point(e.X, e.Y), LeftRightAlignment.Right);


            }
        }

        private void cmsCopy_Click(object sender, EventArgs e)
        {

        }


        private void ClearAll()
        {
            txbWoNo.ResetText();
            txbWoQty.ResetText();
            txbModel.ResetText();
            txbSerialStart.ResetText();
            txbSeriaEnd.ResetText();
        }


    }
}

