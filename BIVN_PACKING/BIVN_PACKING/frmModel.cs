using BIVN_PACKING.PVSService;
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
    public partial class frmModel : Form
    {
        private DataTable dt;
        private PVSWebServiceSoapClient pvsWebService = new PVSWebServiceSoapClient();
        private int currentRowSelected = -1;
        public frmModel()
        {
            InitializeComponent();
            GridViewSetting();
            GetModelEmpty();
        }

        private void GetModelEmpty()
        {
            dt.Clear();
            var listModels = pvsWebService.GetModelInfos("CS000").Reverse();
            foreach (var model in listModels)
            {
                dt.Rows.Add(new object[] { model.Product_Id, model.Content_Length,model.Content, model.Is_Hexa == true ? "YES" : "NO" });
            }
            dgrvModel.Refresh();

        }
        private void GetModelByProductId(string productId)
        {
            dt.Clear();
            var listModels = pvsWebService.GetModelInfos("CS000").Where(m => m.Product_Id == productId.ToUpper() || m.Product_Id == productId.ToLower()).Reverse();
            
            foreach (var model in listModels)
            {
                dt.Rows.Add(new object[] { model.Product_Id, model.Content_Length,model.Content, model.Is_Hexa == true ? "YES" : "NO"  });
            }
            dgrvModel.Refresh();

        }

        private void GridViewSetting()
        {
            if (dt == null)
            {
                dt = new DataTable();
            }
            else
            {
                dt.Clear();
            }
            dt.Columns.Add("MODEL", typeof(string));
            dt.Columns.Add("Số kí tự đầu", typeof(string));
            dt.Columns.Add("Kí tự đầu", typeof(string));
            dt.Columns.Add("Is Hexa", typeof(string));
            dgrvModel.DefaultCellStyle.BackColor = Color.DarkGreen;
            dgrvModel.DefaultCellStyle.ForeColor = Color.White;
            dgrvModel.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frmEditModel = new frmEditModel("", "","","YES");
            frmEditModel.updateAfterClose = new Action(() =>
            {
                GetModelEmpty();
            });
            frmEditModel.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentRowSelected < 0)
            {
                currentRowSelected = 0;
            }

            try
            {
                var model = dgrvModel.Rows[currentRowSelected].Cells[0].Value.ToString();
                var contentLength = dgrvModel.Rows[currentRowSelected].Cells[1].Value.ToString();
                var modelChar = dgrvModel.Rows[currentRowSelected].Cells[2].Value.ToString();
                var isHexa = dgrvModel.Rows[currentRowSelected].Cells[3].Value.ToString();
                var frmEditModel = new frmEditModel(model, contentLength,modelChar,isHexa);
                frmEditModel.updateAfterClose = new Action(() =>
                {
                    GetModelEmpty();
                });
                frmEditModel.ShowDialog();
            }
            catch (Exception)
            {

            }

        }

        private void txbModel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txbModel.Text.Trim()))
                {
                    GetModelEmpty();
                }
                else
                {
                    GetModelByProductId(txbModel.Text.Trim());
                }

            }
        }

        private void dgrvModel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                currentRowSelected = e.RowIndex;
            }

        }
    }
}
