namespace FlagshipForm
{
    partial class NewGameInfo
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
            this.RowLabel = new System.Windows.Forms.Label();
            this.ColumnLabel = new System.Windows.Forms.Label();
            this.RowNum = new System.Windows.Forms.NumericUpDown();
            this.ColumnNum = new System.Windows.Forms.NumericUpDown();
            this.ShipNumberLabel = new System.Windows.Forms.Label();
            this.ShipNumber = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RowNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // RowLabel
            // 
            this.RowLabel.AutoSize = true;
            this.RowLabel.Location = new System.Drawing.Point(18, 32);
            this.RowLabel.Name = "RowLabel";
            this.RowLabel.Size = new System.Drawing.Size(34, 13);
            this.RowLabel.TabIndex = 0;
            this.RowLabel.Text = "Rows";
            // 
            // ColumnLabel
            // 
            this.ColumnLabel.AutoSize = true;
            this.ColumnLabel.Location = new System.Drawing.Point(18, 85);
            this.ColumnLabel.Name = "ColumnLabel";
            this.ColumnLabel.Size = new System.Drawing.Size(47, 13);
            this.ColumnLabel.TabIndex = 1;
            this.ColumnLabel.Text = "Columns";
            // 
            // RowNum
            // 
            this.RowNum.Location = new System.Drawing.Point(74, 30);
            this.RowNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RowNum.Name = "RowNum";
            this.RowNum.Size = new System.Drawing.Size(41, 20);
            this.RowNum.TabIndex = 2;
            this.RowNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ColumnNum
            // 
            this.ColumnNum.Location = new System.Drawing.Point(74, 78);
            this.ColumnNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ColumnNum.Name = "ColumnNum";
            this.ColumnNum.Size = new System.Drawing.Size(41, 20);
            this.ColumnNum.TabIndex = 3;
            this.ColumnNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ShipNumberLabel
            // 
            this.ShipNumberLabel.AutoSize = true;
            this.ShipNumberLabel.Location = new System.Drawing.Point(18, 128);
            this.ShipNumberLabel.Name = "ShipNumberLabel";
            this.ShipNumberLabel.Size = new System.Drawing.Size(88, 13);
            this.ShipNumberLabel.TabIndex = 4;
            this.ShipNumberLabel.Text = "Ships per player: ";
            // 
            // ShipNumber
            // 
            this.ShipNumber.Location = new System.Drawing.Point(112, 126);
            this.ShipNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ShipNumber.Name = "ShipNumber";
            this.ShipNumber.Size = new System.Drawing.Size(40, 20);
            this.ShipNumber.TabIndex = 5;
            this.ShipNumber.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(112, 186);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(21, 186);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NewGameInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 221);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ShipNumber);
            this.Controls.Add(this.ShipNumberLabel);
            this.Controls.Add(this.ColumnNum);
            this.Controls.Add(this.RowNum);
            this.Controls.Add(this.ColumnLabel);
            this.Controls.Add(this.RowLabel);
            this.Name = "NewGameInfo";
            this.Text = "New Game";
            ((System.ComponentModel.ISupportInitialize)(this.RowNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label RowLabel;
        public System.Windows.Forms.Label ColumnLabel;
        public System.Windows.Forms.NumericUpDown RowNum;
        public System.Windows.Forms.NumericUpDown ColumnNum;
        private System.Windows.Forms.Label ShipNumberLabel;
        public System.Windows.Forms.NumericUpDown ShipNumber;
        public System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.Button CancelButton;
    }
}