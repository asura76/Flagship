namespace FlagshipForm
{
    partial class PlaceShipForm
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
            this.ShipNameLabel = new System.Windows.Forms.Label();
            this.ShipNameTextbox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.bRadio = new System.Windows.Forms.RadioButton();
            this.cRadio = new System.Windows.Forms.RadioButton();
            this.acRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ShipNameLabel
            // 
            this.ShipNameLabel.AutoSize = true;
            this.ShipNameLabel.Location = new System.Drawing.Point(67, 52);
            this.ShipNameLabel.Name = "ShipNameLabel";
            this.ShipNameLabel.Size = new System.Drawing.Size(62, 13);
            this.ShipNameLabel.TabIndex = 0;
            this.ShipNameLabel.Text = "Ship Name:";
            // 
            // ShipNameTextbox
            // 
            this.ShipNameTextbox.Location = new System.Drawing.Point(135, 49);
            this.ShipNameTextbox.Name = "ShipNameTextbox";
            this.ShipNameTextbox.Size = new System.Drawing.Size(121, 20);
            this.ShipNameTextbox.TabIndex = 2;
            this.ShipNameTextbox.Text = "S.S. Ship";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(457, 244);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // bRadio
            // 
            this.bRadio.AutoSize = true;
            this.bRadio.Location = new System.Drawing.Point(352, 88);
            this.bRadio.Name = "bRadio";
            this.bRadio.Size = new System.Drawing.Size(71, 17);
            this.bRadio.TabIndex = 5;
            this.bRadio.TabStop = true;
            this.bRadio.Text = "Battleship";
            this.bRadio.UseVisualStyleBackColor = true;
            this.bRadio.CheckedChanged += new System.EventHandler(this.BRadio_CheckedChanged);
            // 
            // cRadio
            // 
            this.cRadio.AutoSize = true;
            this.cRadio.Location = new System.Drawing.Point(352, 129);
            this.cRadio.Name = "cRadio";
            this.cRadio.Size = new System.Drawing.Size(57, 17);
            this.cRadio.TabIndex = 6;
            this.cRadio.TabStop = true;
            this.cRadio.Text = "Cruiser";
            this.cRadio.UseVisualStyleBackColor = true;
            this.cRadio.CheckedChanged += new System.EventHandler(this.CRadio_CheckedChanged);
            // 
            // acRadio
            // 
            this.acRadio.AutoSize = true;
            this.acRadio.Location = new System.Drawing.Point(352, 169);
            this.acRadio.Name = "acRadio";
            this.acRadio.Size = new System.Drawing.Size(91, 17);
            this.acRadio.TabIndex = 7;
            this.acRadio.TabStop = true;
            this.acRadio.Text = "Aircraft Carrier";
            this.acRadio.UseVisualStyleBackColor = true;
            this.acRadio.CheckedChanged += new System.EventHandler(this.AcRadio_CheckedChanged);
            // 
            // PlaceShipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 288);
            this.Controls.Add(this.acRadio);
            this.Controls.Add(this.cRadio);
            this.Controls.Add(this.bRadio);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ShipNameTextbox);
            this.Controls.Add(this.ShipNameLabel);
            this.Name = "PlaceShipForm";
            this.Text = "PlaceShipForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label ShipNameLabel;
        public System.Windows.Forms.TextBox ShipNameTextbox;
        public System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.RadioButton bRadio;
        private System.Windows.Forms.RadioButton cRadio;
        private System.Windows.Forms.RadioButton acRadio;
    }
}