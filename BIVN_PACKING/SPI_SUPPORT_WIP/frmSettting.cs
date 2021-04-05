using SPI_SUPPORT_WIP.Database;
using SPI_SUPPORT_WIP.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPI_SUPPORT_WIP
{
    public partial class frmSettting : Form
    {
        SOFTWAREEntities _db = new SOFTWAREEntities();
        string namepc = Properties.Settings.Default.PCName;
        public frmSettting()
        {
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
                            //cbbprocess.Items.Add(sWinTitle);
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
            Category InfoData = new Category();
            InfoData.PathlogICT = Properties.Settings.Default.PathlogICT;
            InfoData.OutputLog = Properties.Settings.Default.OutputLog;
            InfoData.CurrentStation = Properties.Settings.Default.Station;
            InfoData.TimeSleep = Properties.Settings.Default.TimeSleep;
            InfoData.Process = Properties.Settings.Default.Process;
            InfoData.Model = Properties.Settings.Default.Model;
            InfoData.LengthBarcode = Properties.Settings.Default.LengBarcode;
            InfoData.Location = Properties.Settings.Default.Location;
            InfoData.Index = Properties.Settings.Default.Index;
            InfoData.To = Properties.Settings.Default.To;
            InfoData.PCPSheet = Properties.Settings.Default.PCPSheet;
            InfoData.PCName = Properties.Settings.Default.PCName;
            InfoData.StationIndex = Properties.Settings.Default.StationIndex;
            InfoData.StationTo = Properties.Settings.Default.StationTo;
            InfoData.COMPort = Properties.Settings.Default.COMPort;
            InfoData.EnableCOM = Properties.Settings.Default.EnableCOM;
            InfoData.DataMode = Properties.Settings.Default.DataModel;
            InfoData.DataModelStop = Properties.Settings.Default.DataModelStop;
            InfoData.OldStation = Properties.Settings.Default.OldStation;
           
            //InfoData.SPECIAL = Properties.Settings.Default.SPECIAL;
        }

        private void frmSettting_Load(object sender, EventArgs e)
        {
            List<string> listName = new List<string>();
            var namePC = from ALL_SETTINGS_THE_CUSTOMERS in _db.ALL_SETTINGS_THE_CUSTOMERS select ALL_SETTINGS_THE_CUSTOMERS.PC_NAME;
            var getName = namePC.ToList();
            foreach (var item in getName)
            {
                var checkSave = listName.Where(x => x.ToString() == item.ToString()).FirstOrDefault();
                if (checkSave == null)
                {
                    listName.Add(item.ToString());
                }
            }
            cbbNamePC.DataSource = listName;
            //

            //cbbprocess.Text = Properties.Settings.Default.Process;
            txtPathLogICT.Text = Properties.Settings.Default.PathlogICT;
            txtOutputLog.Text = Properties.Settings.Default.OutputLog;
            txtCurrentStation.Text = Properties.Settings.Default.Station;
            txtTimesleep.Text = Properties.Settings.Default.TimeSleep.ToString();
            txtlengthBarcode.Text = Properties.Settings.Default.LengBarcode.ToString();
            txtLocation.Text = Properties.Settings.Default.Location.ToString();
            txtIndex.Text = Properties.Settings.Default.Index.ToString();
            txtPCP.Text = Properties.Settings.Default.PCPSheet.ToString();
            txtTo.Text = Properties.Settings.Default.To.ToString();
            cbbModel.Text = Properties.Settings.Default.Model;
            cbbNamePC.Text = Properties.Settings.Default.PCName;
            //
            cboCOMPort.DataSource = SerialPort.GetPortNames();
            txtStationIndex.Text = Properties.Settings.Default.StationIndex.ToString();
            txtStationTo.Text = Properties.Settings.Default.StationTo.ToString();
            //
            cboCOMPort.Text = Properties.Settings.Default.COMPort;
            cbEnableCOM.Text = Properties.Settings.Default.EnableCOM.ToString();
            txtRun.Text = Properties.Settings.Default.DataModel;
            txtStop.Text = Properties.Settings.Default.DataModelStop;
            txtOldStation.Text = Properties.Settings.Default.OldStation;

           
            //cbisspecial.Checked = Properties.Settings.Default.SPECIAL;
            //
            if (Properties.Settings.Default.EnableCOM == true)
            {
                cbEnableCOM.Checked = true;
                groupBoxComSerialPortSettings.Enabled = true;
                groupBoxDataMode.Enabled = true;
            }
            else
            {
                cbEnableCOM.Checked = false;
                groupBoxComSerialPortSettings.Enabled = false;
                groupBoxDataMode.Enabled = false;
            }
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.Process = cbbprocess.Text;
            Properties.Settings.Default.PathlogICT = txtPathLogICT.Text;
            Properties.Settings.Default.OutputLog = txtOutputLog.Text;
            Properties.Settings.Default.Station = txtCurrentStation.Text;
            Properties.Settings.Default.TimeSleep = int.Parse(txtTimesleep.Text);
            Properties.Settings.Default.LengBarcode = int.Parse(txtlengthBarcode.Text);
            Properties.Settings.Default.Location = int.Parse(txtLocation.Text);
            Properties.Settings.Default.To = int.Parse(txtTo.Text);
            Properties.Settings.Default.PCPSheet = int.Parse(txtPCP.Text);
            Properties.Settings.Default.Index = int.Parse(txtIndex.Text);
            Properties.Settings.Default.Model = cbbModel.Text;
            Properties.Settings.Default.PCName = cbbNamePC.Text;
            Properties.Settings.Default.StationIndex = int.Parse(txtStationIndex.Text);
            Properties.Settings.Default.StationTo = int.Parse(txtStationTo.Text);
            Properties.Settings.Default.COMPort = cboCOMPort.Text;
            Properties.Settings.Default.EnableCOM = cbEnableCOM.Checked;
            Properties.Settings.Default.DataModel = txtRun.Text;
            Properties.Settings.Default.DataModelStop = txtStop.Text;
            Properties.Settings.Default.OldStation = txtOldStation.Text;

           
            //Properties.Settings.Default.SPECIAL = cbisspecial.Checked;
            Properties.Settings.Default.Save();

            if (txtCurrentStation.Text == "")
            {
                MessageBox.Show("Current Station not null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtCurrentStation.Text.Length > 25)
            {
                MessageBox.Show("Less than 25 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTimesleep.Text.Length != 3)
            {
                MessageBox.Show("Less than 3 characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(cbbModel.Text != "")
            {
                var datacheck = _db.ALL_SETTINGS_THE_CUSTOMERS.Where(x => x.MODEL.Contains(cbbModel.Text) && x.PC_NAME == cbbNamePC.Text.Trim()).FirstOrDefault();
                ALL_SETTINGS_THE_CUSTOMERS data = new ALL_SETTINGS_THE_CUSTOMERS() { PATH_LOG = txtPathLogICT.Text, OUTPUT_LOG = txtOutputLog.Text, STATION = txtCurrentStation.Text, TIME_SLEEP = int.Parse(txtTimesleep.Text), LOCATION = int.Parse(txtLocation.Text), MODEL = cbbModel.Text, LENGTH_SERIAL = int.Parse(txtlengthBarcode.Text), TO = int.Parse(txtTo.Text), PCP_SHEET = int.Parse(txtPCP.Text), INDEX = int.Parse(txtIndex.Text), /*PROCESS = cbbprocess.Text,*/ PC_NAME = cbbNamePC.Text, STATION_INDEX = int.Parse(txtStationIndex.Text), STATION_TO = int.Parse(txtStationTo.Text), OLD_STATION = txtOldStation.Text ,IS_SPECIAL = cbisspecial.Checked};

                if (datacheck == null)
                {
                    _db.ALL_SETTINGS_THE_CUSTOMERS.Add(data);
                    _db.SaveChanges();
                }
                else
                {
                    var id = datacheck.TABLE_ID;
                    var entity = _db.ALL_SETTINGS_THE_CUSTOMERS.FirstOrDefault(item => item.TABLE_ID == id);
                    //entity.PROCESS = cbbprocess.Text;
                    entity.PATH_LOG = txtPathLogICT.Text;
                    entity.OUTPUT_LOG = txtOutputLog.Text;
                    entity.STATION = txtCurrentStation.Text;
                    entity.TIME_SLEEP = int.Parse(txtTimesleep.Text);
                    entity.LENGTH_SERIAL = int.Parse(txtlengthBarcode.Text);
                    entity.LOCATION = int.Parse(txtLocation.Text);
                    entity.TO = int.Parse(txtTo.Text);
                    entity.PCP_SHEET = int.Parse(txtPCP.Text);
                    entity.INDEX = int.Parse(txtIndex.Text);
                    entity.OLD_STATION = txtOldStation.Text;
                    entity.STATION_INDEX = int.Parse(txtStationIndex.Text);
                    entity.STATION_TO = int.Parse(txtStationTo.Text);
                    entity.IS_SPECIAL = cbisspecial.Checked;
                    _db.Entry(entity).State = EntityState.Modified;
                    _db.SaveChanges();

                }
            }
            lblthongbao.Text = $"[{cbbModel.Text}] Lưu thành công!";
            lblthongbao.ForeColor = Color.DarkGreen;
            //Close();
        }

        private void txtFileExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ALL_SETTINGS_THE_CUSTOMERS info = _db.ALL_SETTINGS_THE_CUSTOMERS.Where(x => x.MODEL.Contains(cbbModel.Text)).FirstOrDefault();

            DialogResult dialogResult = MessageBox.Show($"Do you really want to delete the model [{cbbModel.Text}]?", "Delete Model", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (info != null)
                {
                    _db.ALL_SETTINGS_THE_CUSTOMERS.Remove(info);
                    _db.SaveChanges();
                    lbldeleteCompalete.Text = $"Model [{cbbModel.Text}]Delete successfull";
                    lbldeleteCompalete.ForeColor = Color.DarkGreen;
                }
            }
        }

        private void frmSettting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel == false)
            {
                lblname.Text = "";
                Properties.Settings.Default.ONOFF = false;
                Properties.Settings.Default.Save();
            }
        }

        private void cbbModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var viewData = _db.ALL_SETTINGS_THE_CUSTOMERS.Where(x => x.MODEL.Contains(cbbModel.Text) && x.PC_NAME == cbbNamePC.Text.Trim()).FirstOrDefault();
                if (viewData != null)
                {
                    //cbbprocess.Text = viewData.PROCESS;
                    txtPathLogICT.Text = viewData.PATH_LOG;
                    txtOutputLog.Text = viewData.OUTPUT_LOG;
                    txtCurrentStation.Text = viewData.STATION;
                    txtTimesleep.Text = viewData.TIME_SLEEP.ToString();
                    txtlengthBarcode.Text = viewData.LENGTH_SERIAL.ToString();
                    txtLocation.Text = viewData.LOCATION.ToString();
                    txtIndex.Text = viewData.INDEX.ToString();
                    txtPCP.Text = viewData.PCP_SHEET.ToString();
                    txtTo.Text = viewData.TO.ToString();
                    cbbNamePC.Text = viewData.PC_NAME;
                    txtStationIndex.Text = viewData.STATION_INDEX.ToString();
                    txtStationTo.Text = viewData.STATION_TO.ToString();
                    txtOldStation.Text = viewData.OLD_STATION;
                    cbisspecial.Checked = viewData.IS_SPECIAL.GetValueOrDefault();
                    lblchecknullmodel.Text = "OK";
                    lblchecknullmodel.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblchecknullmodel.Text = "NULL";
                    lblchecknullmodel.ForeColor = Color.DarkBlue;
                }
            }
            catch (Exception)
            {

            }

        }

        private void lblBrowseICTLog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog outputLog = new FolderBrowserDialog();
            DialogResult open = outputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtPathLogICT.Text = outputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtPathLogICT.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    txtPathLogICT.Focus();
                }
            }
        }

        private void lblInputLog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog outputLog = new FolderBrowserDialog();
            DialogResult open = outputLog.ShowDialog();
            if (open == DialogResult.OK)
            {
                txtOutputLog.Text = outputLog.SelectedPath;
                if (string.IsNullOrEmpty(txtOutputLog.Text))
                {
                    btnSaveChanged.Focus();
                }
                else
                {
                    lblInputLog.Focus();
                }
            }
        }
        public void SendValueCom(string portName, string data)
        {
            SerialPort com = new SerialPort(portName);
            if (!com.IsOpen) com.Open();
            com.Write(data);
            com.Close();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().ShowDialog();
            if (Properties.Settings.Default.ONOFF == true)
            {
                btnEdit.Visible = true;
                txtlengthBarcode.ReadOnly = false;
                txtLocation.ReadOnly = false;
                txtCurrentStation.ReadOnly = false;
                txtTimesleep.ReadOnly = false;
                txtIndex.ReadOnly = false;
                txtPCP.ReadOnly = false;
                txtTo.ReadOnly = false;
                btnLogin.Enabled = false;
                cbbNamePC.Enabled = true;
                txtStationIndex.ReadOnly = false;
                txtStationTo.ReadOnly = false;
                cbEnableCOM.Visible = true;
                txtOldStation.ReadOnly = false;
                bunifuCustomLabel1.Visible = true;
                if (Properties.Settings.Default.EnableCOM == true)
                {
                    cbEnableCOM.Checked = true;
                    groupBoxComSerialPortSettings.Enabled = true;
                    groupBoxDataMode.Enabled = true;
                }
            }
        }
        private void cbbNamePC_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var saveName = _db.ALL_SETTINGS_THE_CUSTOMERS.Where(x => x.PC_NAME == cbbNamePC.Text).FirstOrDefault();
            //    if (saveName != null)
            //    {
            //        var namep = from ALL_SETTINGS_THE_CUSTOMERS in _db.ALL_SETTINGS_THE_CUSTOMERS select ALL_SETTINGS_THE_CUSTOMERS.PC_NAME;
            //        var name = namep.ToList();
            //        var max = name.Max().ToString().Trim();
            //        string[] spt = max.Split('-');
            //        int view = int.Parse(spt[1]) + 1;
            //        lblname.Text = $"Gợi ý: PC-{view}";
            //    }
            //    //
            //}
            //catch (Exception)
            //{

            //}

        }

        private void cbbNamePC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var model = from ALL_SETTINGS_THE_CUSTOMERS in _db.ALL_SETTINGS_THE_CUSTOMERS where ALL_SETTINGS_THE_CUSTOMERS.PC_NAME == cbbNamePC.Text.Trim() select ALL_SETTINGS_THE_CUSTOMERS.MODEL;
                var rever = model.ToList();
                rever.Reverse();
                cbbModel.DataSource = rever;
                //
                //var name = from ALL_SETTINGS_THE_CUSTOMERS in _db.ALL_SETTINGS_THE_CUSTOMERS where ALL_SETTINGS_THE_CUSTOMERS.PC_NAME == cbbNamePC.Text.Trim() select ALL_SETTINGS_THE_CUSTOMERS.NOTE;
                //var names = name.ToList();
                //foreach (var item in names)
                //{
                //    if (item != null)
                //    {
                //        lblNameCustomer.Text = item;
                //    }
                //}
            }
            catch (Exception)
            {

            }

        }
        private void cbEnableCOM_OnChange(object sender, EventArgs e)
        {
            if (cbEnableCOM.Checked == true)
            {
                groupBoxComSerialPortSettings.Enabled = true;
                groupBoxDataMode.Enabled = true;
            }
            else
            {
                groupBoxComSerialPortSettings.Enabled = false;
                groupBoxDataMode.Enabled = false;
            }
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            cboCOMPort.DataSource = SerialPort.GetPortNames();
        }

        private void txtStationIndex_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtStationTo_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnTestSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendValueCom(cboCOMPort.Text, txtRun.Text);
                lblCheckTest.Text = "Check OK";
                lblCheckTest.ForeColor = Color.DarkGreen;
            }
            catch (Exception)
            {
                lblCheckTest.Text = "Check NG";
                lblCheckTest.ForeColor = Color.DarkRed;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                SendValueCom(cboCOMPort.Text, txtStop.Text);
                lblCheckTest.Text = "Check OK";
                lblCheckTest.ForeColor = Color.DarkGreen;
            }
            catch (Exception)
            {
                lblCheckTest.Text = "Check NG";
                lblCheckTest.ForeColor = Color.DarkRed;
            }
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {

        }
    }
}
