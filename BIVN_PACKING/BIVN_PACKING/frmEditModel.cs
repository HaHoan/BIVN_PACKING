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
    public partial class frmEditModel : Form
    {
        private PVSWebServiceSoapClient pvsWebService = new PVSWebServiceSoapClient();
        private string model;
        private string isHexa;
        public Action updateAfterClose;
        public frmEditModel(string model, string contentLength,string modelChar, string isHexa)
        {
            InitializeComponent();
            this.model = model;
            this.isHexa = isHexa;
            cbHexa.Checked = isHexa == "YES" ? true : false;
            btnSaveChanged.Text = string.IsNullOrEmpty(this.model) ? "Create" : "Save change";
            txbModel.Enabled = string.IsNullOrEmpty(this.model) ? true : false;
            txbModel.Text = model;
            txbContentLength.Text = contentLength;
            txbModelChar.Text = modelChar;
            if (txbModel.Enabled)
            {
                txbModel.Focus();
            }
            else txbContentLength.Focus();
        }

        private void btnSaveChanged_Click(object sender, EventArgs e)
        {
            try
            {

                var entity = new Base_ModelsEntity();
                entity.Customer = "CS000";
                entity.Des = "Brother";
                entity.Pcb = 1;
                entity.Content_Index = 0;
                entity.Location = 1;
                entity.Is_Hexa = cbHexa.Checked;
                entity.Product_Id = txbModel.Text.Trim();
                entity.Content_Length = int.Parse(txbContentLength.Text.Trim());
                entity.Content = txbModelChar.Text.Trim();
                if (!string.IsNullOrEmpty(model))
                {
                    pvsWebService.SaveModelInfo(entity, entity.Product_Id);
                }
                else
                {
                    if (pvsWebService.GetModelInfo(txbModel.Text.Trim()) == null)
                    {
                        pvsWebService.SaveModelInfo(entity, "");
                    }
                    else
                    {
                        if (MessageBox.Show("Đã thêm model này rồi!") == DialogResult.OK)
                        {
                            txbModel.Focus();
                            txbModel.SelectAll();
                            txbContentLength.ResetText();
                        }
                    }
                }
                updateAfterClose();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void txbModel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txbContentLength.Focus();
            }
        }

        private void txbPCB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSaveChanged.Focus();
            }
        }
    }
}
