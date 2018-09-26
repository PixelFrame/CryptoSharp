namespace CryptoSharp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.cbAlgorithm = new System.Windows.Forms.ComboBox();
			this.tbKey = new System.Windows.Forms.TextBox();
			this.tbIV = new System.Windows.Forms.TextBox();
			this.cbMode = new System.Windows.Forms.ComboBox();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.lbInput = new System.Windows.Forms.Label();
			this.lbOutput = new System.Windows.Forms.Label();
			this.btnEncrypt = new System.Windows.Forms.Button();
			this.btnDecrypt = new System.Windows.Forms.Button();
			this.ckbOutBASE64 = new System.Windows.Forms.CheckBox();
			this.btnRSA = new System.Windows.Forms.Button();
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.tlpRightSetB = new System.Windows.Forms.TableLayoutPanel();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnAbout = new System.Windows.Forms.Button();
			this.tlpRightSetA = new System.Windows.Forms.TableLayoutPanel();
			this.lbKey = new System.Windows.Forms.Label();
			this.lbIV = new System.Windows.Forms.Label();
			this.lbAlgorithm = new System.Windows.Forms.Label();
			this.lbMode = new System.Windows.Forms.Label();
			this.btnRandIV = new System.Windows.Forms.Button();
			this.btnRandKey = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ckbInBASE64 = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tlpMain.SuspendLayout();
			this.tlpRightSetB.SuspendLayout();
			this.tlpRightSetA.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbAlgorithm
			// 
			this.cbAlgorithm.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAlgorithm.FormattingEnabled = true;
			this.cbAlgorithm.Location = new System.Drawing.Point(77, 5);
			this.cbAlgorithm.Name = "cbAlgorithm";
			this.cbAlgorithm.Size = new System.Drawing.Size(121, 25);
			this.cbAlgorithm.TabIndex = 0;
			this.cbAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cbAlgorithm_SelectedIndexChanged);
			// 
			// tbKey
			// 
			this.tbKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tbKey.Location = new System.Drawing.Point(77, 111);
			this.tbKey.Multiline = true;
			this.tbKey.Name = "tbKey";
			this.tbKey.Size = new System.Drawing.Size(121, 43);
			this.tbKey.TabIndex = 3;
			this.tbKey.Validating += new System.ComponentModel.CancelEventHandler(this.tbKey_Validating);
			// 
			// tbIV
			// 
			this.tbIV.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tbIV.Location = new System.Drawing.Point(77, 63);
			this.tbIV.Multiline = true;
			this.tbIV.Name = "tbIV";
			this.tbIV.Size = new System.Drawing.Size(121, 42);
			this.tbIV.TabIndex = 2;
			this.tbIV.Validating += new System.ComponentModel.CancelEventHandler(this.tbIV_Validating);
			// 
			// cbMode
			// 
			this.cbMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMode.FormattingEnabled = true;
			this.cbMode.Location = new System.Drawing.Point(77, 35);
			this.cbMode.Name = "cbMode";
			this.cbMode.Size = new System.Drawing.Size(121, 25);
			this.cbMode.TabIndex = 1;
			// 
			// tbInput
			// 
			this.tbInput.AcceptsReturn = true;
			this.tbInput.AcceptsTab = true;
			this.tbInput.Location = new System.Drawing.Point(3, 32);
			this.tbInput.Multiline = true;
			this.tbInput.Name = "tbInput";
			this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbInput.Size = new System.Drawing.Size(416, 161);
			this.tbInput.TabIndex = 1;
			this.tbInput.TabStop = false;
			this.tbInput.WordWrap = false;
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(3, 228);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbOutput.Size = new System.Drawing.Size(416, 162);
			this.tbOutput.TabIndex = 5;
			this.tbOutput.TabStop = false;
			this.tbOutput.WordWrap = false;
			// 
			// lbInput
			// 
			this.lbInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbInput.AutoSize = true;
			this.lbInput.Location = new System.Drawing.Point(3, 4);
			this.lbInput.Margin = new System.Windows.Forms.Padding(3);
			this.lbInput.Name = "lbInput";
			this.lbInput.Size = new System.Drawing.Size(38, 17);
			this.lbInput.TabIndex = 6;
			this.lbInput.Text = "Input";
			// 
			// lbOutput
			// 
			this.lbOutput.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbOutput.AutoSize = true;
			this.lbOutput.Location = new System.Drawing.Point(3, 3);
			this.lbOutput.Name = "lbOutput";
			this.lbOutput.Size = new System.Drawing.Size(48, 17);
			this.lbOutput.TabIndex = 7;
			this.lbOutput.Text = "Output";
			// 
			// btnEncrypt
			// 
			this.btnEncrypt.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnEncrypt.Location = new System.Drawing.Point(3, 13);
			this.btnEncrypt.Name = "btnEncrypt";
			this.btnEncrypt.Size = new System.Drawing.Size(78, 27);
			this.btnEncrypt.TabIndex = 8;
			this.btnEncrypt.Text = "Encrypt";
			this.btnEncrypt.UseVisualStyleBackColor = true;
			this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
			// 
			// btnDecrypt
			// 
			this.btnDecrypt.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnDecrypt.Location = new System.Drawing.Point(87, 13);
			this.btnDecrypt.Name = "btnDecrypt";
			this.btnDecrypt.Size = new System.Drawing.Size(78, 27);
			this.btnDecrypt.TabIndex = 9;
			this.btnDecrypt.Text = "Decrypt";
			this.btnDecrypt.UseVisualStyleBackColor = true;
			// 
			// ckbOutBASE64
			// 
			this.ckbOutBASE64.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.ckbOutBASE64.Location = new System.Drawing.Point(211, 3);
			this.ckbOutBASE64.Name = "ckbOutBASE64";
			this.ckbOutBASE64.Size = new System.Drawing.Size(202, 18);
			this.ckbOutBASE64.TabIndex = 12;
			this.ckbOutBASE64.Text = "Show in BASE64";
			this.ckbOutBASE64.UseVisualStyleBackColor = true;
			// 
			// btnRSA
			// 
			this.btnRSA.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnRSA.Location = new System.Drawing.Point(172, 63);
			this.btnRSA.Name = "btnRSA";
			this.btnRSA.Size = new System.Drawing.Size(79, 36);
			this.btnRSA.TabIndex = 13;
			this.btnRSA.Text = "RSA Utils";
			this.btnRSA.UseVisualStyleBackColor = true;
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 2;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.90476F));
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.09524F));
			this.tlpMain.Controls.Add(this.tlpRightSetB, 1, 3);
			this.tlpMain.Controls.Add(this.tbOutput, 0, 3);
			this.tlpMain.Controls.Add(this.tlpRightSetA, 1, 1);
			this.tlpMain.Controls.Add(this.tbInput, 0, 1);
			this.tlpMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
			this.tlpMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tlpMain.Location = new System.Drawing.Point(12, 12);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 4;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
			this.tlpMain.Size = new System.Drawing.Size(683, 393);
			this.tlpMain.TabIndex = 14;
			// 
			// tlpRightSetB
			// 
			this.tlpRightSetB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.tlpRightSetB.ColumnCount = 3;
			this.tlpRightSetB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tlpRightSetB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tlpRightSetB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tlpRightSetB.Controls.Add(this.btnEncrypt, 0, 0);
			this.tlpRightSetB.Controls.Add(this.btnDecrypt, 1, 0);
			this.tlpRightSetB.Controls.Add(this.btnCopy, 2, 0);
			this.tlpRightSetB.Controls.Add(this.btnRSA, 2, 1);
			this.tlpRightSetB.Controls.Add(this.btnExit, 2, 2);
			this.tlpRightSetB.Controls.Add(this.btnAbout, 1, 2);
			this.tlpRightSetB.Location = new System.Drawing.Point(425, 228);
			this.tlpRightSetB.Name = "tlpRightSetB";
			this.tlpRightSetB.RowCount = 3;
			this.tlpRightSetB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tlpRightSetB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tlpRightSetB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tlpRightSetB.Size = new System.Drawing.Size(255, 162);
			this.tlpRightSetB.TabIndex = 15;
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnCopy.Location = new System.Drawing.Point(172, 13);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(79, 27);
			this.btnCopy.TabIndex = 14;
			this.btnCopy.Text = "Copy Result";
			this.btnCopy.UseVisualStyleBackColor = true;
			// 
			// btnExit
			// 
			this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnExit.Location = new System.Drawing.Point(172, 121);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(79, 27);
			this.btnExit.TabIndex = 11;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnAbout
			// 
			this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnAbout.Location = new System.Drawing.Point(87, 121);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(78, 27);
			this.btnAbout.TabIndex = 10;
			this.btnAbout.Text = "About";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// tlpRightSetA
			// 
			this.tlpRightSetA.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tlpRightSetA.ColumnCount = 3;
			this.tlpRightSetA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpRightSetA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpRightSetA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
			this.tlpRightSetA.Controls.Add(this.lbKey, 0, 3);
			this.tlpRightSetA.Controls.Add(this.lbIV, 0, 2);
			this.tlpRightSetA.Controls.Add(this.cbAlgorithm, 1, 0);
			this.tlpRightSetA.Controls.Add(this.tbKey, 1, 3);
			this.tlpRightSetA.Controls.Add(this.tbIV, 1, 2);
			this.tlpRightSetA.Controls.Add(this.cbMode, 1, 1);
			this.tlpRightSetA.Controls.Add(this.lbAlgorithm, 0, 0);
			this.tlpRightSetA.Controls.Add(this.lbMode, 0, 1);
			this.tlpRightSetA.Controls.Add(this.btnRandIV, 2, 2);
			this.tlpRightSetA.Controls.Add(this.btnRandKey, 2, 3);
			this.tlpRightSetA.Location = new System.Drawing.Point(425, 34);
			this.tlpRightSetA.Name = "tlpRightSetA";
			this.tlpRightSetA.RowCount = 4;
			this.tlpRightSetA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.23077F));
			this.tlpRightSetA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.23077F));
			this.tlpRightSetA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
			this.tlpRightSetA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.76923F));
			this.tlpRightSetA.Size = new System.Drawing.Size(253, 157);
			this.tlpRightSetA.TabIndex = 15;
			// 
			// lbKey
			// 
			this.lbKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbKey.AutoSize = true;
			this.lbKey.Location = new System.Drawing.Point(3, 124);
			this.lbKey.Name = "lbKey";
			this.lbKey.Size = new System.Drawing.Size(29, 17);
			this.lbKey.TabIndex = 7;
			this.lbKey.Text = "Key";
			// 
			// lbIV
			// 
			this.lbIV.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbIV.AutoSize = true;
			this.lbIV.Location = new System.Drawing.Point(3, 75);
			this.lbIV.Name = "lbIV";
			this.lbIV.Size = new System.Drawing.Size(68, 17);
			this.lbIV.TabIndex = 6;
			this.lbIV.Text = "Init Vector";
			// 
			// lbAlgorithm
			// 
			this.lbAlgorithm.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbAlgorithm.AutoSize = true;
			this.lbAlgorithm.Location = new System.Drawing.Point(3, 6);
			this.lbAlgorithm.Name = "lbAlgorithm";
			this.lbAlgorithm.Size = new System.Drawing.Size(65, 17);
			this.lbAlgorithm.TabIndex = 4;
			this.lbAlgorithm.Text = "Algorithm";
			// 
			// lbMode
			// 
			this.lbMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lbMode.AutoSize = true;
			this.lbMode.Location = new System.Drawing.Point(3, 36);
			this.lbMode.Name = "lbMode";
			this.lbMode.Size = new System.Drawing.Size(43, 17);
			this.lbMode.TabIndex = 5;
			this.lbMode.Text = "Mode";
			// 
			// btnRandIV
			// 
			this.btnRandIV.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnRandIV.Location = new System.Drawing.Point(204, 72);
			this.btnRandIV.Name = "btnRandIV";
			this.btnRandIV.Size = new System.Drawing.Size(46, 23);
			this.btnRandIV.TabIndex = 8;
			this.btnRandIV.Text = "Rand";
			this.btnRandIV.UseVisualStyleBackColor = true;
			this.btnRandIV.Click += new System.EventHandler(this.btnRandIV_Click);
			// 
			// btnRandKey
			// 
			this.btnRandKey.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnRandKey.Location = new System.Drawing.Point(204, 121);
			this.btnRandKey.Name = "btnRandKey";
			this.btnRandKey.Size = new System.Drawing.Size(46, 23);
			this.btnRandKey.TabIndex = 9;
			this.btnRandKey.Text = "Rand";
			this.btnRandKey.UseVisualStyleBackColor = true;
			this.btnRandKey.Click += new System.EventHandler(this.btnRandKey_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.lbInput, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ckbInBASE64, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 23);
			this.tableLayoutPanel1.TabIndex = 15;
			// 
			// ckbInBASE64
			// 
			this.ckbInBASE64.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.ckbInBASE64.Location = new System.Drawing.Point(211, 3);
			this.ckbInBASE64.Name = "ckbInBASE64";
			this.ckbInBASE64.Size = new System.Drawing.Size(202, 20);
			this.ckbInBASE64.TabIndex = 13;
			this.ckbInBASE64.Text = "BASE64 Decrypt First";
			this.ckbInBASE64.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.lbOutput, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.ckbOutBASE64, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 199);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(416, 23);
			this.tableLayoutPanel2.TabIndex = 16;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(707, 417);
			this.Controls.Add(this.tlpMain);
			this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "MainForm";
			this.Text = "CryptoSharp";
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			this.tlpRightSetB.ResumeLayout(false);
			this.tlpRightSetA.ResumeLayout(false);
			this.tlpRightSetA.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.ComboBox cbAlgorithm;
		private System.Windows.Forms.TextBox tbKey;
		private System.Windows.Forms.TextBox tbIV;
		private System.Windows.Forms.ComboBox cbMode;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.Label lbInput;
		private System.Windows.Forms.Label lbOutput;
		private System.Windows.Forms.Button btnEncrypt;
		private System.Windows.Forms.Button btnDecrypt;
		private System.Windows.Forms.CheckBox ckbOutBASE64;
		private System.Windows.Forms.Button btnRSA;
		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.TableLayoutPanel tlpRightSetA;
		private System.Windows.Forms.TableLayoutPanel tlpRightSetB;
		private System.Windows.Forms.Label lbKey;
		private System.Windows.Forms.Label lbIV;
		private System.Windows.Forms.Label lbAlgorithm;
		private System.Windows.Forms.Label lbMode;
		private System.Windows.Forms.Button btnRandIV;
		private System.Windows.Forms.Button btnRandKey;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox ckbInBASE64;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	}
}

