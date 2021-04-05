using BIVN_PACKING.Business;
using BIVN_PACKING.Entitis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIVN_PACKING
{
    public partial class SystemUser : Form
    {
        BIVNEntities _db = new BIVNEntities();
        public SystemUser()
        {
            InitializeComponent();
        }

        private void SystemUser_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            var getdata = _db.Users.ToList();
            getdata.Reverse();
            dataGridView1.DataSource = getdata;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnsavemodel_Click(object sender, EventArgs e)
        {
            try
            {
                var Result = dataGridView1.Rows.OfType<DataGridViewRow>().Select(
            r => r.Cells.OfType<DataGridViewCell>().Select(c => c.Value).ToArray()).ToList();
                foreach (var item in Result)
                {
                    string pass = item[1].ToString();
                    string code = item[0].ToString();
                    var id = _db.Users.FirstOrDefault(x => x.USER_NAME == code);
                    var checkmd5 = Regex.IsMatch(pass, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled);
                    if (checkmd5 == true)
                    {
                        id.PASSWORD = item[1].ToString();
                    }
                    else
                    {
                        var md5 = Security.MD5Hash(pass.Trim());
                        id.PASSWORD = md5;
                    }
                    id.IS_ADMIN = Boolean.Parse(item[2].ToString());
                    //id.CREATE_BY = item[3].ToString();
                    //id.CREATE_DATE = DateTime.Parse(item[4].ToString());
                    id.ENABLE = Boolean.Parse(item[5].ToString());
                    _db.Entry(id).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var search = _db.Users.Where(x => x.USER_NAME.Contains(textBox1.Text)).ToList();
                if (search == null)
                {
                    MessageBox.Show($"Không tìm thấy mã nhân viên [{textBox1.Text}]");
                    textBox1.SelectAll();
                    return;
                }
                dataGridView1.DataSource = search;
                textBox1.SelectAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nhập mã nhân viên trước!");
                return;
            }
            Users info = _db.Users.Where(x => x.USER_NAME == textBox1.Text).FirstOrDefault();
            DialogResult dialogResult = MessageBox.Show($"Do you really want to delete the serial [{info.USER_NAME}]?", "Delete Serial", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (info != null)
                {
                    _db.Users.Remove(info);
                    _db.SaveChanges();
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy mã nhân viên [{textBox1.Text}]");
                    return;
                }
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var search = _db.Users.Where(x => x.USER_NAME.Contains(textBox1.Text)).ToList();
            //if (search == null)
            //{
            //    MessageBox.Show($"Không tìm thấy mã nhân viên [{textBox1.Text}]");
            //    textBox1.SelectAll();
            //    return;
            //}
            dataGridView1.DataSource = search;
        }
    }
}
