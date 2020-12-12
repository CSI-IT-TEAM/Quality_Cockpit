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
            this.LINE_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MLINE_CD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MODEL_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.STYLE_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SUP_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.REW_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CS_SIZE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DIV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.C_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RE_QTY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnTop = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
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
            this.LINE_NAME,
            this.MLINE_CD,
            this.MODEL_NAME,
            this.STYLE_CODE,
            this.OP_NAME,
            this.SUP_NAME,
            this.REW_NAME,
            this.CS_SIZE,
            this.DIV,
            this.C_QTY,
            this.RE_QTY});
            this.gvwBase.GridControl = this.grdBase;
            this.gvwBase.Name = "gvwBase";
            this.gvwBase.OptionsBehavior.Editable = false;
            this.gvwBase.OptionsBehavior.ReadOnly = true;
            this.gvwBase.OptionsCustomization.AllowColumnMoving = false;
            this.gvwBase.OptionsCustomization.AllowFilter = false;
            this.gvwBase.OptionsCustomization.AllowGroup = false;
            this.gvwBase.OptionsView.AllowCellMerge = true;
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
            // LINE_NAME
            // 
            this.LINE_NAME.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.LINE_NAME.AppearanceHeader.Options.UseFont = true;
            this.LINE_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.LINE_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LINE_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LINE_NAME.Caption = "Plant";
            this.LINE_NAME.FieldName = "LINE_NAME";
            this.LINE_NAME.FieldNameSortGroup = "LINE_NAME";
            this.LINE_NAME.Name = "LINE_NAME";
            this.LINE_NAME.Visible = true;
            this.LINE_NAME.VisibleIndex = 0;
            this.LINE_NAME.Width = 85;
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
            // MODEL_NAME
            // 
            this.MODEL_NAME.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.MODEL_NAME.AppearanceHeader.Options.UseFont = true;
            this.MODEL_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.MODEL_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MODEL_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MODEL_NAME.Caption = "Model Name";
            this.MODEL_NAME.FieldName = "MODEL_NAME";
            this.MODEL_NAME.Name = "MODEL_NAME";
            this.MODEL_NAME.Visible = true;
            this.MODEL_NAME.VisibleIndex = 2;
            this.MODEL_NAME.Width = 375;
            // 
            // STYLE_CODE
            // 
            this.STYLE_CODE.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.STYLE_CODE.AppearanceHeader.Options.UseFont = true;
            this.STYLE_CODE.AppearanceHeader.Options.UseTextOptions = true;
            this.STYLE_CODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.STYLE_CODE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.STYLE_CODE.Caption = "Style Code";
            this.STYLE_CODE.FieldName = "STYLE_CODE";
            this.STYLE_CODE.Name = "STYLE_CODE";
            this.STYLE_CODE.Visible = true;
            this.STYLE_CODE.VisibleIndex = 3;
            this.STYLE_CODE.Width = 119;
            // 
            // OP_NAME
            // 
            this.OP_NAME.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.OP_NAME.AppearanceHeader.Options.UseFont = true;
            this.OP_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.OP_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.OP_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.OP_NAME.Caption = "Process";
            this.OP_NAME.FieldName = "OP_NAME";
            this.OP_NAME.Name = "OP_NAME";
            this.OP_NAME.Visible = true;
            this.OP_NAME.VisibleIndex = 4;
            this.OP_NAME.Width = 88;
            // 
            // SUP_NAME
            // 
            this.SUP_NAME.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.SUP_NAME.AppearanceHeader.Options.UseFont = true;
            this.SUP_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.SUP_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SUP_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.SUP_NAME.Caption = "Suplier";
            this.SUP_NAME.FieldName = "SUP_NAME";
            this.SUP_NAME.Name = "SUP_NAME";
            this.SUP_NAME.Visible = true;
            this.SUP_NAME.VisibleIndex = 5;
            this.SUP_NAME.Width = 93;
            // 
            // REW_NAME
            // 
            this.REW_NAME.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.REW_NAME.AppearanceHeader.Options.UseFont = true;
            this.REW_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.REW_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.REW_NAME.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.REW_NAME.Caption = "Reason Name";
            this.REW_NAME.FieldName = "REW_NAME";
            this.REW_NAME.Name = "REW_NAME";
            this.REW_NAME.Visible = true;
            this.REW_NAME.VisibleIndex = 6;
            this.REW_NAME.Width = 291;
            // 
            // CS_SIZE
            // 
            this.CS_SIZE.AppearanceCell.Options.UseTextOptions = true;
            this.CS_SIZE.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.CS_SIZE.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.CS_SIZE.AppearanceHeader.Options.UseFont = true;
            this.CS_SIZE.AppearanceHeader.Options.UseTextOptions = true;
            this.CS_SIZE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.CS_SIZE.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.CS_SIZE.Caption = "Size";
            this.CS_SIZE.FieldName = "CS_SIZE";
            this.CS_SIZE.Name = "CS_SIZE";
            this.CS_SIZE.Visible = true;
            this.CS_SIZE.VisibleIndex = 7;
            this.CS_SIZE.Width = 102;
            // 
            // DIV
            // 
            this.DIV.AppearanceCell.Options.UseTextOptions = true;
            this.DIV.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DIV.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.DIV.AppearanceHeader.Options.UseFont = true;
            this.DIV.AppearanceHeader.Options.UseTextOptions = true;
            this.DIV.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DIV.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.DIV.Caption = "Division";
            this.DIV.FieldName = "DIV";
            this.DIV.Name = "DIV";
            this.DIV.Visible = true;
            this.DIV.VisibleIndex = 8;
            this.DIV.Width = 121;
            // 
            // C_QTY
            // 
            this.C_QTY.AppearanceCell.Options.UseTextOptions = true;
            this.C_QTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.C_QTY.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.C_QTY.AppearanceHeader.Options.UseFont = true;
            this.C_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.C_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.C_QTY.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.C_QTY.Caption = "OS&D Qty(Pcs)";
            this.C_QTY.FieldName = "C_QTY";
            this.C_QTY.Name = "C_QTY";
            this.C_QTY.Visible = true;
            this.C_QTY.VisibleIndex = 9;
            this.C_QTY.Width = 159;
            // 
            // RE_QTY
            // 
            this.RE_QTY.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.RE_QTY.AppearanceHeader.Options.UseFont = true;
            this.RE_QTY.AppearanceHeader.Options.UseTextOptions = true;
            this.RE_QTY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.RE_QTY.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.RE_QTY.Caption = "Re Qty(Pcs)";
            this.RE_QTY.FieldName = "RE_QTY";
            this.RE_QTY.Name = "RE_QTY";
            this.RE_QTY.Visible = true;
            this.RE_QTY.VisibleIndex = 10;
            this.RE_QTY.Width = 149;
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
        private DevExpress.XtraGrid.Columns.GridColumn LINE_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn MODEL_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn STYLE_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn OP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn SUP_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn REW_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn LINE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn MLINE_CD;
        private DevExpress.XtraGrid.Columns.GridColumn CS_SIZE;
        private DevExpress.XtraGrid.Columns.GridColumn DIV;
        private DevExpress.XtraGrid.Columns.GridColumn C_QTY;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.Columns.GridColumn RE_QTY;
    }
}