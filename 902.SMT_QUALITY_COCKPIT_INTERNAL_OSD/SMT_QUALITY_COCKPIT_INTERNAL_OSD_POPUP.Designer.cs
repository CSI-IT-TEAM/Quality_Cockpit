namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdBase = new DevExpress.XtraGrid.GridControl();
            this.gvwBase = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LINE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Line_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MLINE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Model_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Style_Code = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Op_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Sup_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Rew_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Size = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.C_Qty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnTop = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.Re_Qty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwBase)).BeginInit();
            this.pnTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdBase, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnTop, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1670, 619);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // grdBase
            // 
            this.grdBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBase.Font = new System.Drawing.Font("Calibri", 12.75F);
            gridLevelNode1.RelationName = "Level1";
            this.grdBase.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdBase.Location = new System.Drawing.Point(3, 81);
            this.grdBase.MainView = this.gvwBase;
            this.grdBase.Name = "grdBase";
            this.grdBase.Size = new System.Drawing.Size(1664, 535);
            this.grdBase.TabIndex = 3;
            this.grdBase.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwBase});
            // 
            // gvwBase
            // 
            this.gvwBase.Appearance.Row.Font = new System.Drawing.Font("Calibri", 15.75F);
            this.gvwBase.Appearance.Row.Options.UseFont = true;
            this.gvwBase.ColumnPanelRowHeight = 50;
            this.gvwBase.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LINE_CD,
            this.Line_Name,
            this.MLINE_CD,
            this.Model_Name,
            this.Style_Code,
            this.Op_Name,
            this.Sup_Name,
            this.Rew_Name,
            this.Size,
            this.LR,
            this.C_Qty,
            this.Re_Qty});
            this.gvwBase.GridControl = this.grdBase;
            this.gvwBase.Name = "gvwBase";
            this.gvwBase.OptionsBehavior.Editable = false;
            this.gvwBase.OptionsBehavior.ReadOnly = true;
            this.gvwBase.OptionsCustomization.AllowColumnMoving = false;
            this.gvwBase.OptionsCustomization.AllowFilter = false;
            this.gvwBase.OptionsCustomization.AllowGroup = false;
            this.gvwBase.OptionsView.ShowGroupPanel = false;
            this.gvwBase.OptionsView.ShowIndicator = false;
            this.gvwBase.RowHeight = 50;
            this.gvwBase.CellMerge += new DevExpress.XtraGrid.Views.Grid.CellMergeEventHandler(this.gvwBase_CellMerge);
            this.gvwBase.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvwBase_RowCellStyle);
            // 
            // LINE_CD
            // 
            this.LINE_CD.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.LINE_CD.AppearanceHeader.Options.UseFont = true;
            this.LINE_CD.AppearanceHeader.Options.UseTextOptions = true;
            this.LINE_CD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LINE_CD.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LINE_CD.Caption = "LINE_CD";
            this.LINE_CD.FieldName = "LINE_CD";
            this.LINE_CD.FieldNameSortGroup = "LINE_CD";
            this.LINE_CD.Name = "LINE_CD";
            this.LINE_CD.Width = 112;
            // 
            // Line_Name
            // 
            this.Line_Name.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Line_Name.AppearanceHeader.Options.UseFont = true;
            this.Line_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Line_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Line_Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Line_Name.Caption = "Plant";
            this.Line_Name.FieldName = "Line_Name";
            this.Line_Name.FieldNameSortGroup = "Line_Name";
            this.Line_Name.Name = "Line_Name";
            this.Line_Name.Visible = true;
            this.Line_Name.VisibleIndex = 0;
            this.Line_Name.Width = 122;
            // 
            // MLINE_CD
            // 
            this.MLINE_CD.AppearanceCell.Options.UseTextOptions = true;
            this.MLINE_CD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MLINE_CD.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MLINE_CD.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.MLINE_CD.AppearanceHeader.Options.UseFont = true;
            this.MLINE_CD.AppearanceHeader.Options.UseTextOptions = true;
            this.MLINE_CD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MLINE_CD.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MLINE_CD.Caption = "Line";
            this.MLINE_CD.FieldName = "MLINE_CD";
            this.MLINE_CD.FieldNameSortGroup = "MLINE_CD";
            this.MLINE_CD.Name = "MLINE_CD";
            this.MLINE_CD.Visible = true;
            this.MLINE_CD.VisibleIndex = 1;
            this.MLINE_CD.Width = 80;
            // 
            // Model_Name
            // 
            this.Model_Name.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Model_Name.AppearanceHeader.Options.UseFont = true;
            this.Model_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Model_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Model_Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Model_Name.Caption = "Model Name";
            this.Model_Name.FieldName = "Model_Name";
            this.Model_Name.Name = "Model_Name";
            this.Model_Name.Visible = true;
            this.Model_Name.VisibleIndex = 2;
            this.Model_Name.Width = 251;
            // 
            // Style_Code
            // 
            this.Style_Code.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Style_Code.AppearanceHeader.Options.UseFont = true;
            this.Style_Code.AppearanceHeader.Options.UseTextOptions = true;
            this.Style_Code.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Style_Code.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Style_Code.Caption = "Style Code";
            this.Style_Code.FieldName = "Style_Code";
            this.Style_Code.Name = "Style_Code";
            this.Style_Code.Visible = true;
            this.Style_Code.VisibleIndex = 3;
            this.Style_Code.Width = 140;
            // 
            // Op_Name
            // 
            this.Op_Name.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Op_Name.AppearanceHeader.Options.UseFont = true;
            this.Op_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Op_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Op_Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Op_Name.Caption = "Process";
            this.Op_Name.FieldName = "Op_Name";
            this.Op_Name.Name = "Op_Name";
            this.Op_Name.Visible = true;
            this.Op_Name.VisibleIndex = 4;
            this.Op_Name.Width = 106;
            // 
            // Sup_Name
            // 
            this.Sup_Name.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Sup_Name.AppearanceHeader.Options.UseFont = true;
            this.Sup_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Sup_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Sup_Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Sup_Name.Caption = "Suplier";
            this.Sup_Name.FieldName = "Sup_Name";
            this.Sup_Name.Name = "Sup_Name";
            this.Sup_Name.Visible = true;
            this.Sup_Name.VisibleIndex = 5;
            this.Sup_Name.Width = 113;
            // 
            // Rew_Name
            // 
            this.Rew_Name.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Rew_Name.AppearanceHeader.Options.UseFont = true;
            this.Rew_Name.AppearanceHeader.Options.UseTextOptions = true;
            this.Rew_Name.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Rew_Name.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Rew_Name.Caption = "Reason Name";
            this.Rew_Name.FieldName = "Rew_Name";
            this.Rew_Name.Name = "Rew_Name";
            this.Rew_Name.Visible = true;
            this.Rew_Name.VisibleIndex = 6;
            this.Rew_Name.Width = 274;
            // 
            // Size
            // 
            this.Size.AppearanceCell.Options.UseTextOptions = true;
            this.Size.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Size.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Size.AppearanceHeader.Options.UseFont = true;
            this.Size.AppearanceHeader.Options.UseTextOptions = true;
            this.Size.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Size.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Size.Caption = "Size";
            this.Size.FieldName = "Size";
            this.Size.Name = "Size";
            this.Size.Visible = true;
            this.Size.VisibleIndex = 7;
            this.Size.Width = 90;
            // 
            // LR
            // 
            this.LR.AppearanceCell.Options.UseTextOptions = true;
            this.LR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LR.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.LR.AppearanceHeader.Options.UseFont = true;
            this.LR.AppearanceHeader.Options.UseTextOptions = true;
            this.LR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LR.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LR.Caption = "Division";
            this.LR.FieldName = "L/R";
            this.LR.Name = "LR";
            this.LR.Visible = true;
            this.LR.VisibleIndex = 8;
            this.LR.Width = 94;
            // 
            // C_Qty
            // 
            this.C_Qty.AppearanceCell.Options.UseTextOptions = true;
            this.C_Qty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.C_Qty.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.C_Qty.AppearanceHeader.Options.UseFont = true;
            this.C_Qty.AppearanceHeader.Options.UseTextOptions = true;
            this.C_Qty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.C_Qty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.C_Qty.Caption = "OS&D Qty(Pcs)";
            this.C_Qty.FieldName = "C_Qty";
            this.C_Qty.Name = "C_Qty";
            this.C_Qty.Visible = true;
            this.C_Qty.VisibleIndex = 9;
            this.C_Qty.Width = 187;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.button1);
            this.pnTop.Controls.Add(this.lblHeader);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTop.Location = new System.Drawing.Point(3, 3);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1664, 72);
            this.pnTop.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(1274, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Export Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.LineVisible = true;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1664, 72);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "Internal OS&&D by Detail";
            // 
            // Re_Qty
            // 
            this.Re_Qty.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.Re_Qty.AppearanceHeader.Options.UseFont = true;
            this.Re_Qty.AppearanceHeader.Options.UseTextOptions = true;
            this.Re_Qty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Re_Qty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Re_Qty.Caption = "Re Qty(Pcs)";
            this.Re_Qty.FieldName = "Re_Qty";
            this.Re_Qty.Name = "Re_Qty";
            this.Re_Qty.Visible = true;
            this.Re_Qty.VisibleIndex = 10;
            this.Re_Qty.Width = 205;
            // 
            // SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1670, 619);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP_VisibleChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwBase)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnTop;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private DevExpress.XtraGrid.GridControl grdBase;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwBase;
        private DevExpress.XtraGrid.Columns.GridColumn Line_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Model_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Style_Code;
        private DevExpress.XtraGrid.Columns.GridColumn Op_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Sup_Name;
        private DevExpress.XtraGrid.Columns.GridColumn Rew_Name;
        private DevExpress.XtraGrid.Columns.GridColumn LINE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn MLINE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn Size;
        private DevExpress.XtraGrid.Columns.GridColumn LR;
        private DevExpress.XtraGrid.Columns.GridColumn C_Qty;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.Columns.GridColumn Re_Qty;
    }
}