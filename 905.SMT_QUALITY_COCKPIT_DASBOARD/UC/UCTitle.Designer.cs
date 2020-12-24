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
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.72072F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.27928F));
            this.tableLayoutPanel1.Controls.Add(this.cmdText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdStatus, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(635, 49);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // cmdText
            // 
            this.cmdText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdText.FlatAppearance.BorderSize = 0;
            this.cmdText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdText.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Italic);
            this.cmdText.Location = new System.Drawing.Point(420, 3);
            this.cmdText.Name = "cmdText";
            this.cmdText.Size = new System.Drawing.Size(212, 43);
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
            this.cmdTitle.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.cmdTitle.Location = new System.Drawing.Point(83, 3);
            this.cmdTitle.Name = "cmdTitle";
            this.cmdTitle.Size = new System.Drawing.Size(331, 43);
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
            this.cmdStatus.Size = new System.Drawing.Size(74, 43);
            this.cmdStatus.TabIndex = 1;
            this.cmdStatus.UseVisualStyleBackColor = false;
            // 
            // UCTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCTitle";
            this.Size = new System.Drawing.Size(635, 49);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdText;
        private System.Windows.Forms.Button cmdTitle;
        private System.Windows.Forms.Button cmdStatus;
    }
}
