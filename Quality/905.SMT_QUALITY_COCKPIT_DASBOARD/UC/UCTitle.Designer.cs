namespace FORM.UC
{
    partial class UCTitle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdText = new System.Windows.Forms.Button();
            this.cmdTitle = new System.Windows.Forms.Button();
            this.cmdStatus = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNextYear = new System.Windows.Forms.Button();
            this.btnPrevYear = new System.Windows.Forms.Button();
            this.lblYear = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.34899F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.65101F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Controls.Add(this.cmdTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdText, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(635, 49);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cmdText
            // 
            this.cmdText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdText.FlatAppearance.BorderSize = 0;
            this.cmdText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdText.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Italic);
            this.cmdText.Location = new System.Drawing.Point(500, 3);
            this.cmdText.Name = "cmdText";
            this.cmdText.Size = new System.Drawing.Size(132, 43);
            this.cmdText.TabIndex = 3;
            this.cmdText.Text = "*P-BOM, SB, CFM Shoe\r\n&& Lab Test";
            this.cmdText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdText.UseVisualStyleBackColor = true;
            // 
            // cmdTitle
            // 
            this.cmdTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdTitle.FlatAppearance.BorderSize = 0;
            this.cmdTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTitle.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.cmdTitle.Location = new System.Drawing.Point(53, 3);
            this.cmdTitle.Name = "cmdTitle";
            this.cmdTitle.Size = new System.Drawing.Size(228, 43);
            this.cmdTitle.TabIndex = 2;
            this.cmdTitle.Text = "New Colorway Readliness";
            this.cmdTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTitle.UseVisualStyleBackColor = true;
            // 
            // cmdStatus
            // 
            this.cmdStatus.BackColor = System.Drawing.Color.Green;
            this.cmdStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdStatus.FlatAppearance.BorderSize = 0;
            this.cmdStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStatus.Location = new System.Drawing.Point(3, 3);
            this.cmdStatus.Name = "cmdStatus";
            this.cmdStatus.Size = new System.Drawing.Size(44, 43);
            this.cmdStatus.TabIndex = 1;
            this.cmdStatus.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblYear);
            this.panel1.Controls.Add(this.btnNextYear);
            this.panel1.Controls.Add(this.btnPrevYear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(287, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 43);
            this.panel1.TabIndex = 4;
            // 
            // btnNextYear
            // 
            this.btnNextYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextYear.Location = new System.Drawing.Point(165, 0);
            this.btnNextYear.Name = "btnNextYear";
            this.btnNextYear.Size = new System.Drawing.Size(42, 42);
            this.btnNextYear.TabIndex = 6;
            this.btnNextYear.Text = ">>";
            this.btnNextYear.UseVisualStyleBackColor = true;
            this.btnNextYear.Click += new System.EventHandler(this.btnNextYear_Click);
            // 
            // btnPrevYear
            // 
            this.btnPrevYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevYear.Location = new System.Drawing.Point(1, 0);
            this.btnPrevYear.Name = "btnPrevYear";
            this.btnPrevYear.Size = new System.Drawing.Size(42, 42);
            this.btnPrevYear.TabIndex = 4;
            this.btnPrevYear.Text = "<<";
            this.btnPrevYear.UseVisualStyleBackColor = true;
            this.btnPrevYear.Click += new System.EventHandler(this.btnPrevYear_Click);
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.Silver;
            this.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYear.Font = new System.Drawing.Font("Calibri", 18.25F, System.Drawing.FontStyle.Bold);
            this.lblYear.ForeColor = System.Drawing.Color.Black;
            this.lblYear.Location = new System.Drawing.Point(43, 1);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(122, 40);
            this.lblYear.TabIndex = 7;
            this.lblYear.Text = "2018";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblYear.TextChanged += new System.EventHandler(this.lblYear_TextChanged);
            // 
            // UCTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCTitle";
            this.Size = new System.Drawing.Size(635, 49);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdText;
        private System.Windows.Forms.Button cmdTitle;
        private System.Windows.Forms.Button cmdStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Button btnNextYear;
        private System.Windows.Forms.Button btnPrevYear;
    }
}
