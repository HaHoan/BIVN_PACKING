using AutoIt;
using BIVN_PACKING.Business;
using BIVN_PACKING.DAL;
using BIVN_PACKING.Entitis;
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
        BIVN.USAPWebServiceSoapClient _dbClient = new BIVN.USAPWebServiceSoapClient();
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        BIVNEntities _db = new BIVNEntities();
        private Database _database = new Database();
        int _woQty = 0, _boxQty = 0;
        User Data = new User();
        SearchView LinkData = new SearchView();
        List<string> listTask = new List<string>();
        private BIVN.BCLBFLMEntity _boxInfo = new BIVN.BCLBFLMEntity();
        private List<string> _specialProduct = new List<string>()
        {
            "D0092S001","D00KHH004","D00KHJ001","D00KHL002"
        };
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
            DataConnect();
            InitializeComponent();
            DataLogin();
            lblVersion.Text = Database.GetRunningVersion();
            bgrwSoftInfo = new BackgroundWorker();
            bgrwSoftInfo.DoWork += BgrwSoftInfo_DoWork;
            bgrwSoftInfo.RunWorkerCompleted += BgrwSoftInfo_RunWorkerCompleted;
            bgrwSoftInfo.RunWorkerAsync();
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
        public void DataConnect()
        {
            LinkData.timesleep = Properties.Settings.Default.timesleep;
            LinkData.model = Properties.Settings.Default.model;
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
        private void lblConfigs_Click(object sender, EventArgs e)
        {
            DataConnect();
            new frmSetting().ShowDialog();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtBoxid.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter BoxID");
                return;
            }
            if (txtWoNo.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter WoNo");
                return;
            }

            if (txtWoQty.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter WO");
                return;
            }
            if (txtSerialStart.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter SerialStart");
                return;
            }
            if (txtSeriaEnd.Text == "")
            {
                ShowMessage("FAIL", @"FAIL", "Please enter SeriaEnd");
                return;
            }
            if (int.Parse(txtWoNo.Text) == 0)
            {
                ShowMessage("FAIL", @"FAIL", "Số lượng thùng không chính xác");
                return;
            }

            if (txtWoQty.Text.Length > 10)
            {
                ShowMessage("FAIL", @"FAIL", $"Số lượng WO [{txtWoQty.Text}] nhỏ hơn 10 ký tự");
                txtWoQty.Focus();
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


            string serialstart = txtSerialStart.Text;
            string serialend = txtSeriaEnd.Text;
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

                if (txtWoQty.Text != "" && txtModel.Text != "" && txtSerialStart.Text != "" && txtSeriaEnd.Text != "")
                {
                    txtBoxid.Enabled = false;
                    panelBarcode.Enabled = true;
                    txtWoQty.Enabled = false;
                    txtModel.Enabled = false;
                    txtSerialStart.Enabled = false;
                    txtSeriaEnd.Enabled = false;
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
            txtWoQty.Enabled = true;
            txtModel.Enabled = true;
            txtSerialStart.Enabled = true;
            txtSeriaEnd.Enabled = true;
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
            Users user = new Users();
            tstAccount.Text = Data.UserName;
            user = _db.Users.Where(x => x.USER_NAME == Data.UserName).FirstOrDefault();
            if (user.IS_ADMIN == true)
            {
                toolStripStatusLabel7.Visible = true;
                register.Visible = true;
                tuong.Visible = true;
            }
            txtModel.Text = "";

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

        private void register_Click(object sender, EventArgs e)
        {
            new Register().ShowDialog();
        }

        private void lblSetting_Click(object sender, EventArgs e)
        {
            new frmSetting().ShowDialog();
            DataConnect();
        }



        private void txtBoxid_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgrvListSerialInBox.DataSource = new List<Dataview>();
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
                    _boxQty = _db.Produce.Where(x => x.BOXID == boxID).Count();

                    var woUsap = Convert.ToInt32(_boxInfo.TN_NO).ToString();
                    txtWoNo.Text = woUsap;
                    txtModel.Text = _boxInfo.PART_NO;
                    lblQtyBoxUsap.Text = Convert.ToInt32(_boxInfo.OS_QTY).ToString();
                    lblQtyBox.Text = _boxQty.ToString();
                    txtWoQty.ResetText();
                    var woExist = _db.Produce.Where(r => r.NAME_WO == woUsap).FirstOrDefault();

                    if (woExist != null)
                    {
                        var woQtyActual = _db.Produce.Where(r => r.NAME_WO == woUsap).Count();
                        SettingWorkInfo(woUsap, woQtyActual, boxID, woExist.WO, woExist.SERIAL_START, woExist.SERIAL_END);
                    }
                    else
                    {

                        ShowMessage("Wait", "Wait", $"Please complete infomation!");
                        frmSettingWork frmSettingWork = new frmSettingWork(txtWoNo.Text, txtModel.Text, _boxInfo);
                        bool isFocusBarcode = false;
                        frmSettingWork.updateAfterSetting = (qty, start, end) =>
                        {
                            SettingWorkInfo(woUsap,0 , boxID, qty, start, end);
                            isFocusBarcode = true;
                           
                        };
                        frmSettingWork.ShowDialog();
                        if (isFocusBarcode)
                        {
                            txtBarcode.Enabled = true;
                            panelBarcode.Enabled = true;
                            txtBarcode.Focus();

                        }
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
            var listSerialInBox = _db.Produce.Where(m => m.BOXID == _boxInfo.BC_NO).OrderByDescending(m => m.DATECREATE).ToList();
            int i = 1;
            var catagorydata = new List<Dataview>();
            foreach (var s in listSerialInBox)
            {
                if (s.DATECREATE is DateTime date)
                {
                    catagorydata.Add(new Dataview()
                    {
                        No = i.ToString(),
                        serial = s.SERIAL,
                        date = date,
                        BOXID = s.BOXID
                    });
                }

                i++;
            }

            this.BeginInvoke(new Action(() =>
            {
                dgrvListSerialInBox.DataSource = catagorydata;

            }));
        }
        private void SettingWorkInfo(string woUsap, int woQtyActual, string boxID, string Wo, string serialStart, string serialEnd)
        {
            try
            {
                _woQty = int.Parse(Wo);

                lblQtyWO.Text = txtWoQty.Text = Wo.ToString();
                txtSerialStart.Text = serialStart.ToString();
                txtSeriaEnd.Text = serialEnd.ToString();
                lblQty.Text = woQtyActual.ToString();

                txtWoQty.Enabled = txtSerialStart.Enabled = txtSeriaEnd.Enabled = false;
                if (_woQty == woQtyActual)
                {
                    ShowMessage("PASS", "FULL", $"[{woUsap}] Đã đầy Wo [{woQtyActual}] / [{_woQty}]");
                    txtBoxid.ResetText();
                    txtBoxid.Focus();
                    return;
                }
                ListAllSerialInBox();
                if (_boxQty == Convert.ToInt32(_boxInfo.OS_QTY))
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
            panelBarcode.Enabled = false;
        }

        private void txtWO_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtModel.Focus();
            }
        }

        private void lblsetting_Click_1(object sender, EventArgs e)
        {
            new frmSetting().ShowDialog();
            DataConnect();
            txtModel.Text = Properties.Settings.Default.model;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetTaskWindows();
            var check_Task = listTask.Where(x => x.ToString().Contains("Line")).FirstOrDefault();
            if (check_Task != null)
            {
                LinkData.process = check_Task.ToString();
            }
        }

        private void txtModel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerialStart.Focus();
            }
        }

        private void toolStripStatusLabel7_Click(object sender, EventArgs e)
        {
            new SystemUser().ShowDialog();
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
                var serialstart = txtSerialStart.Text.Trim();
                var serialend = txtSeriaEnd.Text.Trim();
                var serial = txtBarcode.Text.Trim();
                long decimaStart, decimaEnd, decimaSerial;
                string boxID = txtBoxid.Text.Trim();
                string wo = txtWoNo.Text.Trim();
                if (_specialProduct.Contains(_boxInfo.PART_NO))
                {
                    decimaStart = long.Parse(serialstart.Right(6));
                    decimaEnd = long.Parse(serialend.Right(6));
                    decimaSerial = long.Parse(serial.Right(6));
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
                var serialExist = _db.Produce.FirstOrDefault(r => r.SERIAL == serial);
                if (serialExist != null)
                {
                    ShowMessage("FAIL", @"FAIL", $"Serial đã tồn tại.\nVui lòng bắn Serial khác.");
                    txtBarcode.SelectAll();
                    txtBarcode.Focus();
                    return;
                }
                var allList = new List<Produce>();
                Produce prod = new Produce() { BOXID = txtBoxid.Text, NAME_WO = txtWoNo.Text, WO = txtWoQty.Text, MODEL = txtModel.Text, SERIAL_START = txtSerialStart.Text, SERIAL_END = txtSeriaEnd.Text, AMOUNT = lblQtyBoxUsap.Text, SERIAL = txtBarcode.Text, USER_NAME = Data.UserName };
                allList.Add(prod);
                prod.DATECREATE = _pvs_service.GetDateTime();
                try
                {
                    _db.Produce.Add(prod);
                    _db.SaveChanges();
                    ShowMessage("PASS", @"PASS", $"Serial [{prod.SERIAL}] Lưu thành công!");
                }
                catch (Exception ex)
                {

                    ShowMessage("FAIL", @"FAIL", $"Error: {ex.Message}");
                    return;
                }
                ControlClick();
                ListAllSerialInBox();
                _boxQty = _db.Produce.Where(r => r.BOXID == boxID).Count();
                _woQty = _db.Produce.Where(r => r.NAME_WO == wo).Count();
                lblQtyBox.Text = _boxQty.ToString();
                lblQty.Text = _woQty.ToString();
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

        private void ClearAll()
        {
            txtWoNo.ResetText();
            txtWoQty.ResetText();
            txtModel.ResetText();
            txtSerialStart.ResetText();
            txtSeriaEnd.ResetText();
        }


    }
}

