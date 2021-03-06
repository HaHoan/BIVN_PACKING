using BIVN_PACKING.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
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
        USAPService.USAPWebServiceSoapClient _dbClient = new USAPService.USAPWebServiceSoapClient();
        private PVSService.PVSWebServiceSoapClient _pvs_service = new PVSService.PVSWebServiceSoapClient();
        private USAPService.BCLBFLMEntity _boxInfo = new USAPService.BCLBFLMEntity();
        User Data = new User();
        string result = "";

        public frmSearch()
        {
            DataLogin();
            InitializeComponent();
            cbbOption.SelectedIndex = 0;
            //updateModel();
        }
        private void updateModel()
        {
            using(var db = new BIVNEntities())
            {
                var list = db.Repairs.Where(m => string.IsNullOrEmpty(m.MODEL)).ToList();
                foreach(var re in list)
                {
                    var box = _dbClient.GetByBcNo(re.BOXID);
                    re.MODEL = box.PART_NO;
                    db.SaveChanges();
                }
            }
         
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


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblMessage.Text == "")
            {
                ShowMessage("NULL", @"NULL", "Please enter the serial you want to delete...");
                lblMessage.ForeColor = Color.White;
                txbSearch.Focus();
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



        private void updateDataBySearch(object result)
        {
            int count = 0;
            if (cbHangSua.Checked)
            {
                var data = (List<Repair>)result;
                data.Reverse();
                dataGridView1.DataSource = data;
                count = data.Count;
            }
            else
            {
                var data = (List<BIVNService.BIVNPackEntity>)result;
                data.Reverse();
                dataGridView1.DataSource = data;
                count = data.Count;
            }


            DgvPerformance(this.dataGridView1);
            if (count != 0)
            {
                result = txbSearch.Text;
                lblMessage.Text = txbSearch.Text + " " + $"[{count}]";
                lblMessage.ForeColor = Color.DarkGreen;
                txbSearch.SelectAll();
                txbSearch.Focus();
            }
            else
            {
                lblMessage.Text = "no results";
                txbSearch.Focus();
            }

        }

        private void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            var arg = e.Argument as string;
            if (cbHangSua.Checked)
            {
                using (var db = new BIVNEntities())
                {
                    if (arg == Constant.FILTER_BY_SERIAL)
                    {
                        var data = db.Repairs.Where(m => m.SERIAL == txbSearch.Text.Trim() && EntityFunctions.TruncateTime(m.DATECREATE) >= dtptimestart.Value.Date && EntityFunctions.TruncateTime(m.DATECREATE) <= dtptimeend.Value.Date).ToList();

                        e.Result = data;
                    }
                    else if(arg == Constant.FILTER_BY_MODEL)
                    {
                        var data = db.Repairs.Where(m => m.MODEL == txbSearch.Text.Trim() && EntityFunctions.TruncateTime(m.DATECREATE) >= dtptimestart.Value.Date && EntityFunctions.TruncateTime(m.DATECREATE) <= dtptimeend.Value.Date).ToList();

                        e.Result = data;
                    }else if(arg == Constant.FILTER_BY_REPAIRER)
                    {
                        var data = db.Repairs.Where(m => m.REPAIRER.Contains(txbSearch.Text.Trim()) && EntityFunctions.TruncateTime(m.DATECREATE) >= dtptimestart.Value.Date && EntityFunctions.TruncateTime(m.DATECREATE) <= dtptimeend.Value.Date).ToList();

                        e.Result = data;
                    }
                }
            }
            else
            {
                if (arg == Constant.FILTER_BY_MODEL)
                {
                    var data = _bivnService.GetListPack("", txbSearch.Text.Trim(), "", "").Where(m => m.DATECREATE.Date >= dtptimestart.Value.Date && m.DATECREATE.Date <= dtptimeend.Value.Date).ToList();
                    e.Result = data;
                }
                else if (arg == Constant.FILTER_BY_SERIAL)
                {
                    var data = _bivnService.GetListPack("", "", "", txbSearch.Text.Trim()).Where(m => m.DATECREATE.Date >= dtptimestart.Value.Date && m.DATECREATE.Date <= dtptimeend.Value.Date).ToList();
                    e.Result = data;
                }
            }

        }

        private void bgwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                updateDataBySearch(e.Result);

            }

            lblStatus.Text = "None";

        }


        string filterOption = Constant.FILTER_BY_SERIAL;
        private void cbbOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbOption.SelectedIndex == 1)
            {
                filterOption = Constant.FILTER_BY_SERIAL;

            }
            else if (cbbOption.SelectedIndex == 0)
            {
                filterOption = Constant.FILTER_BY_MODEL;
            }else if(cbbOption.SelectedIndex == 2)
            {
                filterOption = Constant.FILTER_BY_REPAIRER;
            }
            this.ActiveControl = txbSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbSearch.Text.Trim()))
            {
                lblStatus.Text = "Loading...";
                string arg = filterOption;
                bgwSearch.RunWorkerAsync(argument: arg);
            }

        }

        private void txbSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbSearch.Text.Trim()) && e.KeyCode == Keys.Enter)
            {
                lblStatus.Text = "Loading...";
                string arg = filterOption;
                bgwSearch.RunWorkerAsync(argument: arg);
            }
        }
    }
}
