using BIVN_PACKING.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIVN_PACKING
{
    public partial class frmSearch : Form
    {
        BIVNService.BIVNWebServiceSoapClient _bivnService = new BIVNService.BIVNWebServiceSoapClient();
        User Data = new User();
        string result = "";

        public frmSearch()
        {
            DataLogin();
            InitializeComponent();
        }
        private void ShowMessage(string key, string str_status, string str_message)
        {
            switch (key)
            {
                case "PASS":
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkGreen; }));
                    break;
                case "FAIL":
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkRed; }));
                    break;
                case "NULL":
                    this.BeginInvoke(new Action(() => { lblMessage.Text = str_message; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.DarkBlue; }));
                    break;
                case "Default":
                    this.BeginInvoke(new Action(() => { lblMessage.Text = "no results"; }));
                    this.BeginInvoke(new Action(() => { lblMessage.BackColor = Color.White; }));
                    this.BeginInvoke(new Action(() => { lblMessage.ForeColor = Color.Black; }));
                    break;
            }
        }
        public void DataLogin()
        {
            Data.UserName = Properties.Settings.Default.user;
        }
        private void DgvPerformance(DataGridView dgv)
        {
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void ressearch_Click(object sender, EventArgs e)
        {
            ShowMessage("Default", @"[N\A]", "no result");
            dataGridView1.DataSource = null;
            txtSearch.ResetText();
            txtSearch.Focus();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblMessage.Text == "")
            {
                ShowMessage("NULL", @"NULL", "Please enter the serial you want to delete...");
                lblMessage.ForeColor = Color.White;
                txtSearch.Focus();
                return;
            }
            var info = _bivnService.GetListPack("", "", "", result).ToList();
            DialogResult dialogResult = MessageBox.Show($"Do you really want to delete?", "Delete Serial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (info.Count != 0)
                {
                    foreach (var item in info)
                    {
                        //  _db.Produce.Remove(item);
                        // _db.SaveChanges();
                        lblMessage.Text = $"Xóa thành công!";
                    }
                }
                else
                {
                    ShowMessage("NULL", @"NULL", "No data found");
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void txtSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text != "")
                {
                    var data = _bivnService.GetListPack("", "", "", txtSearch.Text.Trim()).ToList();
                    data.Reverse();
                    dataGridView1.DataSource = data;
                    DgvPerformance(this.dataGridView1);
                    if (data.Count != 0)
                    {
                        result = txtSearch.Text;
                        lblMessage.Text = txtSearch.Text + " " + $"[{data.Count()}]";
                        lblMessage.ForeColor = Color.DarkGreen;
                        txtSearch.SelectAll();
                        txtSearch.Focus();
                    }
                    else
                    {
                        lblMessage.Text = "no results";
                        txtSearch.Focus();
                    }

                }

            }

        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void lblexport_Click(object sender, EventArgs e)
        {

            // Don't save if no data is returned
            var dgv = dataGridView1;
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].Name + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = result;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
                lblMessage.Text = "Đã xuất file csv thành công!";

            }

            //lblPathRoot.Text = @"C:\DATA_BIVN_PACKING_CSV_FILE";

        }


        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    var data = _db.Produce.Where(x => x.BOXID.Contains(txtSearch.Text) || x.MODEL.Contains(txtSearch.Text) || x.SERIAL.Contains(txtSearch.Text) || x.NAME_WO.Contains(txtSearch.Text)).ToList();
        //    if (data.Count != 0)
        //    {
        //        dataGridView1.DataSource = data;
        //    }
        //}

        private void dtptimestart_ValueChanged(object sender, EventArgs e)
        {
            List<BIVNService.BIVNPackEntity> listdata = new List<BIVNService.BIVNPackEntity>();
            string datestart = dtptimestart.Value.Date.ToString();
            string dateend = dtptimeend.Value.Date.ToString();
            DateTime dt1 = DateTime.Parse($"{datestart}").Date;
            DateTime dt2 = DateTime.Parse($"{dateend}").Date;
            var getdatabytime = _bivnService.GetListPack("", "", "", "").ToList();
            foreach (var item in getdatabytime)
            {
                DateTime? getdata = item.DATECREATE.Date;
                if (dt1 <= getdata && getdata <= dt2)
                {
                    var viewdata = new BIVNService.BIVNPackEntity()
                    {
                        BOXID = item.BOXID,
                        WO = item.WO,
                        MODEL = item.MODEL,
                        SERIAL_START = item.SERIAL_START,
                        SERIAL_END = item.SERIAL_END,
                        AMOUNT = item.AMOUNT,
                        SERIAL = item.SERIAL,
                        DATECREATE = item.DATECREATE,
                        USER_NAME = item.USER_NAME,
                        NAME_WO = item.NAME_WO
                    };
                    listdata.Add(viewdata);
                }
            }
            listdata.Reverse();
            dataGridView1.DataSource = listdata;
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            lblMessage.Text = listdata.Count().ToString();
            lblMessage.ForeColor = Color.DarkGreen;
        }

        private void dtptimeend_ValueChanged(object sender, EventArgs e)
        {
            var listdata = new List<BIVNService.BIVNPackEntity>();
            string datestart = dtptimestart.Value.Date.ToString();
            string dateend = dtptimeend.Value.Date.ToString();
            DateTime dt1 = DateTime.Parse($"{datestart}").Date;
            DateTime dt2 = DateTime.Parse($"{dateend}").Date;
            var getdatabytime = _bivnService.GetListPack("", "", "", "").ToList();
            foreach (var item in getdatabytime)
            {
                DateTime? getdata = item.DATECREATE.Date;
                if (dt1 <= getdata && getdata <= dt2)
                {
                    var viewdata = new BIVNService.BIVNPackEntity()
                    {
                        BOXID = item.BOXID,
                        WO = item.WO,
                        MODEL = item.MODEL,
                        SERIAL_START = item.SERIAL_START,
                        SERIAL_END = item.SERIAL_END,
                        AMOUNT = item.AMOUNT,
                        SERIAL = item.SERIAL,
                        DATECREATE = item.DATECREATE,
                        USER_NAME = item.USER_NAME,
                        NAME_WO = item.NAME_WO
                    };
                    listdata.Add(viewdata);
                }
            }
            listdata.Reverse();
            dataGridView1.DataSource = listdata;
            if (dataGridView1.DataSource != null)
            {
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            lblMessage.Text = listdata.Count().ToString();
            lblMessage.ForeColor = Color.DarkGreen;
        }
    }
}
