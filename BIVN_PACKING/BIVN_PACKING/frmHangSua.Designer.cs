namespace BIVN_PACKING
{
    partial class frmHangSua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHangSua));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblQtyBox = new System.Windows.Forms.Label();
            this.lblQtyBoxActual = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelBOXID = new System.Windows.Forms.Panel();
            this.llreset = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxid = new System.Windows.Forms.TextBox();
            this.panelBarcode = new System.Windows.Forms.Panel();
            this.lblResetBarcode = new System.Windows.Forms.LinkLabel();
            this.labba = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.dgrvListSerialInBox = new System.Windows.Forms.DataGridView();
            this.NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOXID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USER_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblModel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txbRepairer = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.panelBOXID.SuspendLayout();
            this.panelBarcode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvListSerialInBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblQtyBox);
            this.groupBox2.Controls.Add(this.lblQtyBoxActual);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(356, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 73);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SL / T";
            // 
            // lblQtyBox
            // 
            this.lblQtyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyBox.ForeColor = System.Drawing.Color.Green;
            this.lblQtyBox.Location = new System.Drawing.Point(130, 19);
            this.lblQtyBox.Name = "lblQtyBox";
            this.lblQtyBox.Size = new System.Drawing.Size(55, 31);
            this.lblQtyBox.TabIndex = 3;
            this.lblQtyBox.Text = "0";
            this.lblQtyBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQtyBoxActual
            // 
            this.lblQtyBoxActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyBoxActual.ForeColor = System.Drawing.Color.Blue;
            this.lblQtyBoxActual.Location = new System.Drawing.Point(52, 19);
            this.lblQtyBoxActual.Name = "lblQtyBoxActual";
            this.lblQtyBoxActual.Size = new System.Drawing.Size(57, 31);
            this.lblQtyBoxActual.TabIndex = 5;
            this.lblQtyBoxActual.Text = "0";
            this.lblQtyBoxActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(110, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 32);
            this.label10.TabIndex = 4;
            this.label10.Text = "/";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblMessage.Location = new System.Drawing.Point(146, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(5);
            this.lblMessage.Size = new System.Drawing.Size(522, 72);
            this.lblMessage.TabIndex = 77;
            this.lblMessage.Text = "no results";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(16, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 72);
            this.lblStatus.TabIndex = 76;
            this.lblStatus.Text = "[N\\A]";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBOXID
            // 
            this.panelBOXID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBOXID.Controls.Add(this.llreset);
            this.panelBOXID.Controls.Add(this.label5);
            this.panelBOXID.Controls.Add(this.txtBoxid);
            this.panelBOXID.Location = new System.Drawing.Point(14, 97);
            this.panelBOXID.Name = "panelBOXID";
            this.panelBOXID.Size = new System.Drawing.Size(324, 30);
            this.panelBOXID.TabIndex = 78;
            // 
            // llreset
            // 
            this.llreset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.llreset.Image = global::BIVN_PACKING.Properties.Resources.refesh_16;
            this.llreset.Location = new System.Drawing.Point(303, 4);
            this.llreset.Name = "llreset";
            this.llreset.Size = new System.Drawing.Size(22, 19);
            this.llreset.TabIndex = 63;
            this.llreset.Click += new System.EventHandler(this.llreset_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "BoxID:";
            // 
            // txtBoxid
            // 
            this.txtBoxid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBoxid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxid.Location = new System.Drawing.Point(49, 0);
            this.txtBoxid.Name = "txtBoxid";
            this.txtBoxid.Size = new System.Drawing.Size(248, 26);
            this.txtBoxid.TabIndex = 1;
            this.txtBoxid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtBoxid_PreviewKeyDown);
            // 
            // panelBarcode
            // 
            this.panelBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBarcode.Controls.Add(this.lblResetBarcode);
            this.panelBarcode.Controls.Add(this.labba);
            this.panelBarcode.Controls.Add(this.txtBarcode);
            this.panelBarcode.Enabled = false;
            this.panelBarcode.Location = new System.Drawing.Point(14, 177);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Size = new System.Drawing.Size(326, 31);
            this.panelBarcode.TabIndex = 79;
            // 
            // lblResetBarcode
            // 
            this.lblResetBarcode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblResetBarcode.Image = global::BIVN_PACKING.Properties.Resources.refesh_16;
            this.lblResetBarcode.Location = new System.Drawing.Point(303, 7);
            this.lblResetBarcode.Name = "lblResetBarcode";
            this.lblResetBarcode.Size = new System.Drawing.Size(22, 19);
            this.lblResetBarcode.TabIndex = 64;
            // 
            // labba
            // 
            this.labba.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labba.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.labba.Image = global::BIVN_PACKING.Properties.Resources.barcode_32;
            this.labba.Location = new System.Drawing.Point(7, 1);
            this.labba.Name = "labba";
            this.labba.Size = new System.Drawing.Size(33, 25);
            this.labba.TabIndex = 31;
            // 
            // txtBarcode
            // 
            this.txtBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(46, 0);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(251, 26);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtBarcode_PreviewKeyDown);
            // 
            // dgrvListSerialInBox
            // 
            this.dgrvListSerialInBox.AllowUserToAddRows = false;
            this.dgrvListSerialInBox.AllowUserToDeleteRows = false;
            this.dgrvListSerialInBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgrvListSerialInBox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvListSerialInBox.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NO,
            this.BOXID,
            this.serial,
            this.date,
            this.USER_NAME});
            this.dgrvListSerialInBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgrvListSerialInBox.Location = new System.Drawing.Point(0, 292);
            this.dgrvListSerialInBox.Name = "dgrvListSerialInBox";
            this.dgrvListSerialInBox.ReadOnly = true;
            this.dgrvListSerialInBox.RowHeadersVisible = false;
            this.dgrvListSerialInBox.Size = new System.Drawing.Size(695, 180);
            this.dgrvListSerialInBox.TabIndex = 80;
            // 
            // NO
            // 
            this.NO.DataPropertyName = "no";
            this.NO.HeaderText = "NO";
            this.NO.Name = "NO";
            this.NO.ReadOnly = true;
            this.NO.Width = 60;
            // 
            // BOXID
            // 
            this.BOXID.DataPropertyName = "BOXID";
            this.BOXID.HeaderText = "BOXID";
            this.BOXID.Name = "BOXID";
            this.BOXID.ReadOnly = true;
            // 
            // serial
            // 
            this.serial.DataPropertyName = "SERIAL";
            this.serial.HeaderText = "SERIAL";
            this.serial.Name = "serial";
            this.serial.ReadOnly = true;
            this.serial.Width = 200;
            // 
            // date
            // 
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.date.DataPropertyName = "DATECREATE";
            this.date.HeaderText = "DATE UPDATE";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // USER_NAME
            // 
            this.USER_NAME.DataPropertyName = "USERNAME";
            this.USER_NAME.HeaderText = "USERNAME";
            this.USER_NAME.Name = "USER_NAME";
            this.USER_NAME.ReadOnly = true;
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModel.Location = new System.Drawing.Point(358, 101);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(0, 16);
            this.lblModel.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "Tên người sửa";
            // 
            // txbRepairer
            // 
            this.txbRepairer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbRepairer.Location = new System.Drawing.Point(105, 139);
            this.txbRepairer.Name = "txbRepairer";
            this.txbRepairer.Size = new System.Drawing.Size(207, 26);
            this.txbRepairer.TabIndex = 83;
            this.txbRepairer.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txbRepairer_PreviewKeyDown);
            // 
            // frmHangSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 472);
            this.Controls.Add(this.txbRepairer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblModel);
            this.Controls.Add(this.dgrvListSerialInBox);
            this.Controls.Add(this.panelBarcode);
            this.Controls.Add(this.panelBOXID);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHangSua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nhập Hàng Sửa";
            this.groupBox2.ResumeLayout(false);
            this.panelBOXID.ResumeLayout(false);
            this.panelBOXID.PerformLayout();
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvListSerialInBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblQtyBox;
        private System.Windows.Forms.Label lblQtyBoxActual;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelBOXID;
        private System.Windows.Forms.LinkLabel llreset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxid;
        private System.Windows.Forms.Panel panelBarcode;
        private System.Windows.Forms.LinkLabel lblResetBarcode;
        private System.Windows.Forms.Label labba;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.DataGridView dgrvListSerialInBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOXID;
        private System.Windows.Forms.DataGridViewTextBoxColumn serial;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn USER_NAME;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbRepairer;
    }
}