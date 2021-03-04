namespace NMSClientWithUI
{
    partial class NMSClientProject10
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.GetDescendentValuesBtn = new System.Windows.Forms.Button();
            this.GetExtentValuesBtn = new System.Windows.Forms.Button();
            this.GetValueBtn = new System.Windows.Forms.Button();
            this.GetValuesPanel = new System.Windows.Forms.Panel();
            this.GetValuesBtn2 = new System.Windows.Forms.Button();
            this.ModelCodesList = new System.Windows.Forms.CheckedListBox();
            this.OutputLbl = new System.Windows.Forms.Label();
            this.outputTB = new System.Windows.Forms.TextBox();
            this.CheckGidBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GiDTb = new System.Windows.Forms.TextBox();
            this.MainPanel.SuspendLayout();
            this.GetValuesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Controls.Add(this.GetDescendentValuesBtn);
            this.MainPanel.Controls.Add(this.GetExtentValuesBtn);
            this.MainPanel.Controls.Add(this.GetValueBtn);
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(833, 476);
            this.MainPanel.TabIndex = 0;
            // 
            // GetDescendentValuesBtn
            // 
            this.GetDescendentValuesBtn.Location = new System.Drawing.Point(344, 193);
            this.GetDescendentValuesBtn.Name = "GetDescendentValuesBtn";
            this.GetDescendentValuesBtn.Size = new System.Drawing.Size(161, 36);
            this.GetDescendentValuesBtn.TabIndex = 2;
            this.GetDescendentValuesBtn.Text = "Get Descendent Values";
            this.GetDescendentValuesBtn.UseVisualStyleBackColor = true;
            // 
            // GetExtentValuesBtn
            // 
            this.GetExtentValuesBtn.Location = new System.Drawing.Point(344, 151);
            this.GetExtentValuesBtn.Name = "GetExtentValuesBtn";
            this.GetExtentValuesBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GetExtentValuesBtn.Size = new System.Drawing.Size(161, 36);
            this.GetExtentValuesBtn.TabIndex = 1;
            this.GetExtentValuesBtn.Text = "Get Extent Values";
            this.GetExtentValuesBtn.UseVisualStyleBackColor = true;
            // 
            // GetValueBtn
            // 
            this.GetValueBtn.Location = new System.Drawing.Point(344, 109);
            this.GetValueBtn.Name = "GetValueBtn";
            this.GetValueBtn.Size = new System.Drawing.Size(161, 36);
            this.GetValueBtn.TabIndex = 0;
            this.GetValueBtn.Text = "Get Values";
            this.GetValueBtn.UseVisualStyleBackColor = true;
            this.GetValueBtn.Click += new System.EventHandler(this.GetValueBtn_Click);
            // 
            // GetValuesPanel
            // 
            this.GetValuesPanel.BackColor = System.Drawing.Color.Transparent;
            this.GetValuesPanel.Controls.Add(this.GetValuesBtn2);
            this.GetValuesPanel.Controls.Add(this.ModelCodesList);
            this.GetValuesPanel.Controls.Add(this.OutputLbl);
            this.GetValuesPanel.Controls.Add(this.outputTB);
            this.GetValuesPanel.Controls.Add(this.CheckGidBtn);
            this.GetValuesPanel.Controls.Add(this.label2);
            this.GetValuesPanel.Controls.Add(this.label1);
            this.GetValuesPanel.Controls.Add(this.GiDTb);
            this.GetValuesPanel.Location = new System.Drawing.Point(0, 0);
            this.GetValuesPanel.Name = "GetValuesPanel";
            this.GetValuesPanel.Size = new System.Drawing.Size(855, 511);
            this.GetValuesPanel.TabIndex = 3;
            // 
            // GetValuesBtn2
            // 
            this.GetValuesBtn2.Location = new System.Drawing.Point(430, 177);
            this.GetValuesBtn2.Name = "GetValuesBtn2";
            this.GetValuesBtn2.Size = new System.Drawing.Size(75, 23);
            this.GetValuesBtn2.TabIndex = 7;
            this.GetValuesBtn2.Text = "GetValues";
            this.GetValuesBtn2.UseVisualStyleBackColor = true;
            this.GetValuesBtn2.Click += new System.EventHandler(this.GetValuesBtn2_Click);
            // 
            // ModelCodesList
            // 
            this.ModelCodesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.ModelCodesList.FormattingEnabled = true;
            this.ModelCodesList.Location = new System.Drawing.Point(264, 151);
            this.ModelCodesList.Name = "ModelCodesList";
            this.ModelCodesList.Size = new System.Drawing.Size(160, 276);
            this.ModelCodesList.TabIndex = 6;
            this.ModelCodesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ModelCodesList_ItemCheck);
            // 
            // OutputLbl
            // 
            this.OutputLbl.AutoSize = true;
            this.OutputLbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.OutputLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.OutputLbl.Location = new System.Drawing.Point(511, 112);
            this.OutputLbl.Name = "OutputLbl";
            this.OutputLbl.Size = new System.Drawing.Size(77, 25);
            this.OutputLbl.TabIndex = 5;
            this.OutputLbl.Text = "Output:";
            // 
            // outputTB
            // 
            this.outputTB.Location = new System.Drawing.Point(511, 151);
            this.outputTB.Multiline = true;
            this.outputTB.Name = "outputTB";
            this.outputTB.Size = new System.Drawing.Size(265, 283);
            this.outputTB.TabIndex = 4;
            this.outputTB.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            // 
            // CheckGidBtn
            // 
            this.CheckGidBtn.Location = new System.Drawing.Point(183, 177);
            this.CheckGidBtn.Name = "CheckGidBtn";
            this.CheckGidBtn.Size = new System.Drawing.Size(75, 23);
            this.CheckGidBtn.TabIndex = 3;
            this.CheckGidBtn.Text = "Check";
            this.CheckGidBtn.UseVisualStyleBackColor = true;
            this.CheckGidBtn.Click += new System.EventHandler(this.CheckGidBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(792, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "To get values enter global ID than click check to see if it\'s valid. Next, select" +
    " ModelCodes of properties you want.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(70, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter global ID:";
            // 
            // GiDTb
            // 
            this.GiDTb.Location = new System.Drawing.Point(75, 151);
            this.GiDTb.Name = "GiDTb";
            this.GiDTb.Size = new System.Drawing.Size(183, 20);
            this.GiDTb.TabIndex = 0;
            // 
            // NMSClientProject10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 500);
            this.Controls.Add(this.GetValuesPanel);
            this.Controls.Add(this.MainPanel);
            this.Name = "NMSClientProject10";
            this.Text = "NMSClientProject10";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainPanel.ResumeLayout(false);
            this.GetValuesPanel.ResumeLayout(false);
            this.GetValuesPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button GetValueBtn;
        private System.Windows.Forms.Button GetExtentValuesBtn;
        private System.Windows.Forms.Button GetDescendentValuesBtn;
        private System.Windows.Forms.Panel GetValuesPanel;
        private System.Windows.Forms.TextBox GiDTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CheckGidBtn;
        private System.Windows.Forms.Label OutputLbl;
        private System.Windows.Forms.TextBox outputTB;
        private System.Windows.Forms.Button GetValuesBtn2;
        private System.Windows.Forms.CheckedListBox ModelCodesList;
    }
}

