namespace BIVN_PACKING
{
    partial class frmEditModel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSaveChanged = new System.Windows.Forms.Button();
            this.txbContentLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbModel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbHexa = new System.Windows.Forms.CheckBox();
            this.txbModelChar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveChanged
            // 
            this.btnSaveChanged.BackColor = System.Drawing.Color.Green;
            this.btnSaveChanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveChanged.ForeColor = System.Drawing.Color.White;
            this.btnSaveChanged.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveChanged.Location = new System.Drawing.Point(94, 178);
            this.btnSaveChanged.Name = "btnSaveChanged";
            this.btnSaveChanged.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaveChanged.Size = new System.Drawing.Size(102, 30);
            this.btnSaveChanged.TabIndex = 55;
            this.btnSaveChanged.Text = "Save change";
            this.btnSaveChanged.UseVisualStyleBackColor = false;
            this.btnSaveChanged.Click += new System.EventHandler(this.btnSaveChanged_Click);
            // 
            // txbContentLength
            // 
            this.txbContentLength.Location = new System.Drawing.Point(94, 69);
            this.txbContentLength.Name = "txbContentLength";
            this.txbContentLength.Size = new System.Drawing.Size(189, 20);
            this.txbContentLength.TabIndex = 54;
            this.txbContentLength.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txbPCB_PreviewKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Content Length";
            // 
            // txbModel
            // 
            this.txbModel.Location = new System.Drawing.Point(94, 21);
            this.txbModel.Name = "txbModel";
            this.txbModel.Size = new System.Drawing.Size(189, 20);
            this.txbModel.TabIndex = 52;
            this.txbModel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txbModel_PreviewKeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Model";
            // 
            // cbHexa
            // 
            this.cbHexa.AutoSize = true;
            this.cbHexa.Location = new System.Drawing.Point(94, 150);
            this.cbHexa.Name = "cbHexa";
            this.cbHexa.Size = new System.Drawing.Size(62, 17);
            this.cbHexa.TabIndex = 57;
            this.cbHexa.Text = "Is Hexa";
            this.cbHexa.UseVisualStyleBackColor = true;
            // 
            // txbModelChar
            // 
            this.txbModelChar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbModelChar.Location = new System.Drawing.Point(97, 112);
            this.txbModelChar.Name = "txbModelChar";
            this.txbModelChar.Size = new System.Drawing.Size(189, 20);
            this.txbModelChar.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 58;
            this.label3.Text = "Kí tự đầu";
            // 
            // frmEditModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 281);
            this.Controls.Add(this.txbModelChar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbHexa);
            this.Controls.Add(this.btnSaveChanged);
            this.Controls.Add(this.txbContentLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbModel);
            this.Controls.Add(this.label1);
            this.Name = "frmEditModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEditModel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveChanged;
        private System.Windows.Forms.TextBox txbContentLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbHexa;
        private System.Windows.Forms.TextBox txbModelChar;
        private System.Windows.Forms.Label label3;
    }
}