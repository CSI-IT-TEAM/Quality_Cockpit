namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.pnTop = new System.Windows.Forms.Panel();
            this.cmdPm1 = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnBody = new System.Windows.Forms.Panel();
            this.tblBody = new System.Windows.Forms.TableLayoutPanel();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.btnLocation_N = new FORM.AdvancedPanel();
            this.btnLocation_F = new FORM.AdvancedPanel();
            this.btnLocation_E = new FORM.AdvancedPanel();
            this.btnLocation_K = new FORM.AdvancedPanel();
            this.btnLocation_J = new FORM.AdvancedPanel();
            this.btnLocation_I = new FORM.AdvancedPanel();
            this.btnLocation_H = new FORM.AdvancedPanel();
            this.btnLocation_G = new FORM.AdvancedPanel();
            this.btnLocation_FTY1 = new FORM.AdvancedPanel();
            this.btnLocation_B = new FORM.AdvancedPanel();
            this.btnLocation_C = new FORM.AdvancedPanel();
            this.btnLocation_M = new FORM.AdvancedPanel();
            this.btnLocation_L = new FORM.AdvancedPanel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.tblRight = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.tblLine = new System.Windows.Forms.TableLayoutPanel();
            this.pnChart = new System.Windows.Forms.Panel();
            this.chtHumi = new DevExpress.XtraCharts.ChartControl();
            this.pnFac = new System.Windows.Forms.Panel();
            this.grdMain = new DevExpress.XtraGrid.GridControl();
            this.gvwMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.FAC_NM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.LINE_NM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.Average = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.HUMIDITY = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnT = new System.Windows.Forms.Panel();
            this.tmrWarning = new System.Windows.Forms.Timer(this.components);
            this.pnTop.SuspendLayout();
            this.pnBody.SuspendLayout();
            this.tblBody.SuspendLayout();
            this.pnLeft.SuspendLayout();
            this.pnRight.SuspendLayout();
            this.tblRight.SuspendLayout();
            this.tblLine.SuspendLayout();
            this.pnChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtHumi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            this.pnFac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.cmdPm1);
            this.pnTop.Controls.Add(this.lblDate);
            this.pnTop.Controls.Add(this.lblHeader);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1920, 76);
            this.pnTop.TabIndex = 2;
            // 
            // cmdPm1
            // 
            this.cmdPm1.BackColor = System.Drawing.Color.Transparent;
            this.cmdPm1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdPm1.BackgroundImage")));
            this.cmdPm1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdPm1.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdPm1.FlatAppearance.BorderSize = 0;
            this.cmdPm1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdPm1.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdPm1.ForeColor = System.Drawing.Color.Navy;
            this.cmdPm1.Location = new System.Drawing.Point(3, 3);
            this.cmdPm1.Name = "cmdPm1";
            this.cmdPm1.Size = new System.Drawing.Size(77, 70);
            this.cmdPm1.TabIndex = 88;
            this.cmdPm1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPm1.UseVisualStyleBackColor = false;
            this.cmdPm1.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // lblDate
            // 
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Black;
            this.lblDate.Location = new System.Drawing.Point(1725, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(195, 76);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "2020-07-22\r\n10:00:00";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.DoubleClick += new System.EventHandler(this.lblDate_DoubleClick);
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblHeader.LineVisible = true;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1226, 76);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "        Humidity Tracking";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnBody
            // 
            this.pnBody.BackColor = System.Drawing.Color.White;
            this.pnBody.Controls.Add(this.tblBody);
            this.pnBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody.Location = new System.Drawing.Point(0, 126);
            this.pnBody.Name = "pnBody";
            this.pnBody.Size = new System.Drawing.Size(1920, 954);
            this.pnBody.TabIndex = 3;
            // 
            // tblBody
            // 
            this.tblBody.BackColor = System.Drawing.SystemColors.Control;
            this.tblBody.ColumnCount = 2;
            this.tblBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblBody.Controls.Add(this.pnLeft, 0, 0);
            this.tblBody.Controls.Add(this.pnRight, 1, 0);
            this.tblBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBody.Location = new System.Drawing.Point(0, 0);
            this.tblBody.Name = "tblBody";
            this.tblBody.RowCount = 1;
            this.tblBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBody.Size = new System.Drawing.Size(1920, 954);
            this.tblBody.TabIndex = 0;
            // 
            // pnLeft
            // 
            this.pnLeft.BackgroundImage = global::FORM.Properties.Resources.Draft1;
            this.pnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnLeft.Controls.Add(this.btnLocation_N);
            this.pnLeft.Controls.Add(this.btnLocation_F);
            this.pnLeft.Controls.Add(this.btnLocation_E);
            this.pnLeft.Controls.Add(this.btnLocation_K);
            this.pnLeft.Controls.Add(this.btnLocation_J);
            this.pnLeft.Controls.Add(this.btnLocation_I);
            this.pnLeft.Controls.Add(this.btnLocation_H);
            this.pnLeft.Controls.Add(this.btnLocation_G);
            this.pnLeft.Controls.Add(this.btnLocation_FTY1);
            this.pnLeft.Controls.Add(this.btnLocation_B);
            this.pnLeft.Controls.Add(this.btnLocation_C);
            this.pnLeft.Controls.Add(this.btnLocation_M);
            this.pnLeft.Controls.Add(this.btnLocation_L);
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLeft.Location = new System.Drawing.Point(3, 3);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(1242, 948);
            this.pnLeft.TabIndex = 0;
            // 
            // btnLocation_N
            // 
            this.btnLocation_N.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_N.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_N.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_N.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_N.EdgeWidth = 2;
            this.btnLocation_N.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_N.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_N.Location = new System.Drawing.Point(767, 393);
            this.btnLocation_N.Name = "btnLocation_N";
            this.btnLocation_N.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_N.RectRadius = 17;
            this.btnLocation_N.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_N.ShadowShift = 1;
            this.btnLocation_N.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_N.Size = new System.Drawing.Size(37, 35);
            this.btnLocation_N.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_N.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_N.TabIndex = 0;
            this.btnLocation_N.Tag = "099";
            this.btnLocation_N.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_F
            // 
            this.btnLocation_F.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_F.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_F.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_F.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_F.EdgeWidth = 2;
            this.btnLocation_F.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_F.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_F.Location = new System.Drawing.Point(716, 486);
            this.btnLocation_F.Name = "btnLocation_F";
            this.btnLocation_F.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_F.RectRadius = 17;
            this.btnLocation_F.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_F.ShadowShift = 1;
            this.btnLocation_F.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_F.Size = new System.Drawing.Size(37, 35);
            this.btnLocation_F.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_F.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_F.TabIndex = 0;
            this.btnLocation_F.Tag = "012";
            this.btnLocation_F.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_E
            // 
            this.btnLocation_E.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_E.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_E.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_E.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_E.EdgeWidth = 2;
            this.btnLocation_E.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_E.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_E.Location = new System.Drawing.Point(736, 442);
            this.btnLocation_E.Name = "btnLocation_E";
            this.btnLocation_E.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_E.RectRadius = 17;
            this.btnLocation_E.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_E.ShadowShift = 1;
            this.btnLocation_E.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_E.Size = new System.Drawing.Size(37, 35);
            this.btnLocation_E.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_E.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_E.TabIndex = 0;
            this.btnLocation_E.Tag = "011";
            this.btnLocation_E.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_K
            // 
            this.btnLocation_K.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_K.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_K.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_K.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_K.EdgeWidth = 2;
            this.btnLocation_K.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_K.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_K.Location = new System.Drawing.Point(578, 195);
            this.btnLocation_K.Name = "btnLocation_K";
            this.btnLocation_K.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_K.RectRadius = 12;
            this.btnLocation_K.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_K.ShadowShift = 1;
            this.btnLocation_K.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_K.Size = new System.Drawing.Size(29, 26);
            this.btnLocation_K.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_K.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_K.TabIndex = 0;
            this.btnLocation_K.Tag = "009";
            this.btnLocation_K.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_J
            // 
            this.btnLocation_J.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_J.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_J.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_J.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_J.EdgeWidth = 2;
            this.btnLocation_J.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_J.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_J.Location = new System.Drawing.Point(616, 171);
            this.btnLocation_J.Name = "btnLocation_J";
            this.btnLocation_J.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_J.RectRadius = 12;
            this.btnLocation_J.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_J.ShadowShift = 1;
            this.btnLocation_J.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_J.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_J.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_J.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_J.TabIndex = 0;
            this.btnLocation_J.Tag = "016";
            this.btnLocation_J.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_I
            // 
            this.btnLocation_I.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_I.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_I.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_I.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_I.EdgeWidth = 2;
            this.btnLocation_I.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_I.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_I.Location = new System.Drawing.Point(643, 145);
            this.btnLocation_I.Name = "btnLocation_I";
            this.btnLocation_I.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_I.RectRadius = 12;
            this.btnLocation_I.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_I.ShadowShift = 1;
            this.btnLocation_I.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_I.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_I.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_I.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_I.TabIndex = 0;
            this.btnLocation_I.Tag = "015";
            this.btnLocation_I.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_H
            // 
            this.btnLocation_H.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_H.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_H.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_H.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_H.EdgeWidth = 2;
            this.btnLocation_H.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_H.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_H.Location = new System.Drawing.Point(667, 119);
            this.btnLocation_H.Name = "btnLocation_H";
            this.btnLocation_H.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_H.RectRadius = 12;
            this.btnLocation_H.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_H.ShadowShift = 1;
            this.btnLocation_H.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_H.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_H.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_H.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_H.TabIndex = 0;
            this.btnLocation_H.Tag = "014";
            this.btnLocation_H.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_G
            // 
            this.btnLocation_G.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_G.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_G.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_G.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_G.EdgeWidth = 2;
            this.btnLocation_G.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_G.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_G.Location = new System.Drawing.Point(692, 95);
            this.btnLocation_G.Name = "btnLocation_G";
            this.btnLocation_G.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_G.RectRadius = 12;
            this.btnLocation_G.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_G.ShadowShift = 1;
            this.btnLocation_G.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_G.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_G.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_G.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_G.TabIndex = 0;
            this.btnLocation_G.Tag = "013";
            this.btnLocation_G.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_FTY1
            // 
            this.btnLocation_FTY1.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_FTY1.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_FTY1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_FTY1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_FTY1.EdgeWidth = 2;
            this.btnLocation_FTY1.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_FTY1.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_FTY1.Location = new System.Drawing.Point(503, 345);
            this.btnLocation_FTY1.Name = "btnLocation_FTY1";
            this.btnLocation_FTY1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_FTY1.RectRadius = 12;
            this.btnLocation_FTY1.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_FTY1.ShadowShift = 1;
            this.btnLocation_FTY1.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_FTY1.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_FTY1.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_FTY1.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_FTY1.TabIndex = 0;
            this.btnLocation_FTY1.Tag = "000";
            this.btnLocation_FTY1.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_B
            // 
            this.btnLocation_B.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_B.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_B.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_B.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_B.EdgeWidth = 2;
            this.btnLocation_B.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_B.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_B.Location = new System.Drawing.Point(556, 258);
            this.btnLocation_B.Name = "btnLocation_B";
            this.btnLocation_B.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_B.RectRadius = 12;
            this.btnLocation_B.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_B.ShadowShift = 1;
            this.btnLocation_B.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_B.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_B.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_B.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_B.TabIndex = 0;
            this.btnLocation_B.Tag = "007";
            this.btnLocation_B.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_C
            // 
            this.btnLocation_C.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_C.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_C.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_C.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_C.EdgeWidth = 2;
            this.btnLocation_C.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_C.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_C.Location = new System.Drawing.Point(594, 273);
            this.btnLocation_C.Name = "btnLocation_C";
            this.btnLocation_C.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_C.RectRadius = 12;
            this.btnLocation_C.ShadowColor = System.Drawing.Color.Gray;
            this.btnLocation_C.ShadowShift = 1;
            this.btnLocation_C.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_C.Size = new System.Drawing.Size(29, 28);
            this.btnLocation_C.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_C.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_C.TabIndex = 0;
            this.btnLocation_C.Tag = "008";
            this.btnLocation_C.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_M
            // 
            this.btnLocation_M.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_M.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_M.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_M.EdgeWidth = 2;
            this.btnLocation_M.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_M.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_M.Location = new System.Drawing.Point(683, 527);
            this.btnLocation_M.Name = "btnLocation_M";
            this.btnLocation_M.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_M.RectRadius = 17;
            this.btnLocation_M.ShadowColor = System.Drawing.Color.Transparent;
            this.btnLocation_M.ShadowShift = 1;
            this.btnLocation_M.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_M.Size = new System.Drawing.Size(37, 35);
            this.btnLocation_M.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_M.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_M.TabIndex = 0;
            this.btnLocation_M.Tag = "019";
            this.btnLocation_M.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // btnLocation_L
            // 
            this.btnLocation_L.BackColor = System.Drawing.Color.Transparent;
            this.btnLocation_L.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.ForwardDiagonal;
            this.btnLocation_L.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLocation_L.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation_L.EdgeWidth = 2;
            this.btnLocation_L.EndColor = System.Drawing.Color.LightGray;
            this.btnLocation_L.FlatBorderColor = System.Drawing.Color.Gray;
            this.btnLocation_L.Location = new System.Drawing.Point(741, 554);
            this.btnLocation_L.Name = "btnLocation_L";
            this.btnLocation_L.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnLocation_L.RectRadius = 17;
            this.btnLocation_L.ShadowColor = System.Drawing.Color.Transparent;
            this.btnLocation_L.ShadowShift = 1;
            this.btnLocation_L.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.btnLocation_L.Size = new System.Drawing.Size(37, 35);
            this.btnLocation_L.StartColor = System.Drawing.Color.Gray;
            this.btnLocation_L.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.btnLocation_L.TabIndex = 0;
            this.btnLocation_L.Tag = "018";
            this.btnLocation_L.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // pnRight
            // 
            this.pnRight.Controls.Add(this.tblRight);
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRight.Location = new System.Drawing.Point(1251, 3);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(666, 948);
            this.pnRight.TabIndex = 1;
            // 
            // tblRight
            // 
            this.tblRight.ColumnCount = 1;
            this.tblRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.Controls.Add(this.lbl_title, 0, 0);
            this.tblRight.Controls.Add(this.tblLine, 0, 1);
            this.tblRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRight.Location = new System.Drawing.Point(0, 0);
            this.tblRight.Name = "tblRight";
            this.tblRight.RowCount = 2;
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblRight.Size = new System.Drawing.Size(666, 948);
            this.tblRight.TabIndex = 0;
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_title.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_title.Location = new System.Drawing.Point(3, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(660, 30);
            this.lbl_title.TabIndex = 11;
            this.lbl_title.Text = "Humidity Average By Line";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLine
            // 
            this.tblLine.ColumnCount = 2;
            this.tblLine.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLine.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLine.Controls.Add(this.pnChart, 1, 0);
            this.tblLine.Controls.Add(this.pnFac, 0, 0);
            this.tblLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLine.Location = new System.Drawing.Point(3, 33);
            this.tblLine.Name = "tblLine";
            this.tblLine.RowCount = 1;
            this.tblLine.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLine.Size = new System.Drawing.Size(660, 912);
            this.tblLine.TabIndex = 13;
            // 
            // pnChart
            // 
            this.pnChart.Controls.Add(this.chtHumi);
            this.pnChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnChart.Location = new System.Drawing.Point(333, 3);
            this.pnChart.Name = "pnChart";
            this.pnChart.Size = new System.Drawing.Size(324, 906);
            this.pnChart.TabIndex = 0;
            // 
            // chtHumi
            // 
            this.chtHumi.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chtHumi.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.chtHumi.BorderOptions.Thickness = 2;
            this.chtHumi.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chtHumi.DataBindings = null;
            xyDiagram1.AxisX.Label.Angle = -25;
            xyDiagram1.AxisX.Label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisX.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisX.Reverse = true;
            xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
            xyDiagram1.AxisX.Title.Text = "Line";
            xyDiagram1.AxisX.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Alignment = DevExpress.XtraCharts.AxisAlignment.Far;
            constantLine1.AxisValueSerializable = "70";
            constantLine1.Color = System.Drawing.Color.Red;
            constantLine1.LineStyle.Thickness = 3;
            constantLine1.Name = "Constant Line 1";
            constantLine1.Title.Alignment = DevExpress.XtraCharts.ConstantLineTitleAlignment.Far;
            constantLine1.Title.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            constantLine1.Title.Text = "Within 70%";
            constantLine1.Title.TextColor = System.Drawing.Color.Red;
            xyDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1});
            xyDiagram1.AxisY.Label.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram1.AxisY.NumericScaleOptions.GridSpacing = 20D;
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold);
            xyDiagram1.AxisY.Title.Text = "Humidity (%)";
            xyDiagram1.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            xyDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisualRange.Auto = false;
            xyDiagram1.AxisY.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisY.VisualRange.MaxValueSerializable = "100";
            xyDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.VisualRange.SideMarginsValue = 0D;
            xyDiagram1.AxisY.WholeRange.AlwaysShowZeroLevel = false;
            xyDiagram1.AxisY.WholeRange.Auto = false;
            xyDiagram1.AxisY.WholeRange.AutoSideMargins = false;
            xyDiagram1.AxisY.WholeRange.MaxValueSerializable = "100";
            xyDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.WholeRange.SideMarginsValue = 0D;
            xyDiagram1.EnableAxisXScrolling = true;
            xyDiagram1.EnableAxisXZooming = true;
            xyDiagram1.EnableAxisYScrolling = true;
            xyDiagram1.EnableAxisYZooming = true;
            xyDiagram1.Rotated = true;
            this.chtHumi.Diagram = xyDiagram1;
            this.chtHumi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtHumi.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chtHumi.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chtHumi.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chtHumi.Legend.Name = "Default Legend";
            this.chtHumi.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chtHumi.Location = new System.Drawing.Point(0, 0);
            this.chtHumi.Name = "chtHumi";
            this.chtHumi.RuntimeHitTesting = true;
            series1.CrosshairLabelPattern = "{V:#,0.#}%";
            sideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            sideBySideBarSeriesLabel1.TextPattern = "{V:#,0.#}%";
            series1.Label = sideBySideBarSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Name = "Humidity By Line";
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            sideBySideBarSeriesView1.ColorEach = true;
            series1.View = sideBySideBarSeriesView1;
            this.chtHumi.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chtHumi.Size = new System.Drawing.Size(324, 906);
            this.chtHumi.TabIndex = 25;
            chartTitle1.Font = new System.Drawing.Font("Times New Roman", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            chartTitle1.Text = "Inventory (Finish Goods)";
            chartTitle1.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chtHumi.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // pnFac
            // 
            this.pnFac.Controls.Add(this.grdMain);
            this.pnFac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnFac.Location = new System.Drawing.Point(3, 3);
            this.pnFac.Name = "pnFac";
            this.pnFac.Size = new System.Drawing.Size(324, 906);
            this.pnFac.TabIndex = 0;
            // 
            // grdMain
            // 
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Font = new System.Drawing.Font("Calibri", 16F);
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.LookAndFeel.SkinName = "Office 2010 Blue";
            this.grdMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.grdMain.MainView = this.gvwMain;
            this.grdMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(324, 906);
            this.grdMain.TabIndex = 3;
            this.grdMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwMain,
            this.gridView2});
            // 
            // gvwMain
            // 
            this.gvwMain.Appearance.BandPanel.Font = new System.Drawing.Font("Calibri", 12F);
            this.gvwMain.Appearance.BandPanel.Options.UseFont = true;
            this.gvwMain.Appearance.BandPanel.Options.UseTextOptions = true;
            this.gvwMain.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvwMain.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.Average});
            this.gvwMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvwMain.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.FAC_NM,
            this.LINE_NM,
            this.HUMIDITY});
            this.gvwMain.GridControl = this.grdMain;
            this.gvwMain.Name = "gvwMain";
            this.gvwMain.OptionsBehavior.Editable = false;
            this.gvwMain.OptionsBehavior.ReadOnly = true;
            this.gvwMain.OptionsSelection.MultiSelect = true;
            this.gvwMain.OptionsView.AllowCellMerge = true;
            this.gvwMain.OptionsView.ColumnAutoWidth = false;
            this.gvwMain.OptionsView.ShowColumnHeaders = false;
            this.gvwMain.OptionsView.ShowGroupPanel = false;
            this.gvwMain.OptionsView.ShowIndicator = false;
            this.gvwMain.RowHeight = 28;
            this.gvwMain.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvwMain_CustomDrawCell);
            // 
            // gridBand1
            // 
            this.gridBand1.Columns.Add(this.FAC_NM);
            this.gridBand1.Columns.Add(this.LINE_NM);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 223;
            // 
            // FAC_NM
            // 
            this.FAC_NM.Caption = "FAC_NM";
            this.FAC_NM.FieldName = "FAC_NM";
            this.FAC_NM.Name = "FAC_NM";
            this.FAC_NM.Visible = true;
            this.FAC_NM.Width = 126;
            // 
            // LINE_NM
            // 
            this.LINE_NM.Caption = "LINE_NM";
            this.LINE_NM.FieldName = "LINE_NM";
            this.LINE_NM.Name = "LINE_NM";
            this.LINE_NM.Visible = true;
            this.LINE_NM.Width = 97;
            // 
            // Average
            // 
            this.Average.AppearanceHeader.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Average.AppearanceHeader.Options.UseFont = true;
            this.Average.AppearanceHeader.Options.UseTextOptions = true;
            this.Average.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Average.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.Average.Caption = "Average";
            this.Average.Columns.Add(this.HUMIDITY);
            this.Average.Name = "Average";
            this.Average.VisibleIndex = 1;
            this.Average.Width = 96;
            // 
            // HUMIDITY
            // 
            this.HUMIDITY.Caption = "HUMIDITY";
            this.HUMIDITY.FieldName = "HUMIDITY";
            this.HUMIDITY.Name = "HUMIDITY";
            this.HUMIDITY.Visible = true;
            this.HUMIDITY.Width = 96;
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdMain;
            this.gridView2.Name = "gridView2";
            // 
            // pnT
            // 
            this.pnT.BackColor = System.Drawing.SystemColors.Control;
            this.pnT.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnT.Location = new System.Drawing.Point(0, 76);
            this.pnT.Name = "pnT";
            this.pnT.Size = new System.Drawing.Size(1920, 50);
            this.pnT.TabIndex = 4;
            // 
            // tmrWarning
            // 
            this.tmrWarning.Enabled = true;
            this.tmrWarning.Interval = 500;
            this.tmrWarning.Tick += new System.EventHandler(this.tmrWarning_Tick);
            // 
            // SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pnBody);
            this.Controls.Add(this.pnT);
            this.Controls.Add(this.pnTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING_Load);
            this.VisibleChanged += new System.EventHandler(this.SMT_QUALITY_COCKPIT_EXTERNAL_OSD_VisibleChanged);
            this.pnTop.ResumeLayout(false);
            this.pnBody.ResumeLayout(false);
            this.tblBody.ResumeLayout(false);
            this.pnLeft.ResumeLayout(false);
            this.pnRight.ResumeLayout(false);
            this.tblRight.ResumeLayout(false);
            this.tblLine.ResumeLayout(false);
            this.pnChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chtHumi)).EndInit();
            this.pnFac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnTop;
        public System.Windows.Forms.Button cmdPm1;
        private System.Windows.Forms.Label lblDate;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnBody;
        private System.Windows.Forms.TableLayoutPanel tblBody;
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel pnT;
        private System.Windows.Forms.TableLayoutPanel tblRight;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.TableLayoutPanel tblLine;
        private System.Windows.Forms.Panel pnChart;
        private System.Windows.Forms.Panel pnFac;
        private DevExpress.XtraCharts.ChartControl chtHumi;
        private DevExpress.XtraGrid.GridControl grdMain;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvwMain;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn FAC_NM;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn LINE_NM;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand Average;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn HUMIDITY;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private AdvancedPanel btnLocation_L;
        private System.Windows.Forms.Timer tmrWarning;
        private AdvancedPanel btnLocation_M;
        private AdvancedPanel btnLocation_E;
        private AdvancedPanel btnLocation_C;
        private AdvancedPanel btnLocation_K;
        private AdvancedPanel btnLocation_J;
        private AdvancedPanel btnLocation_I;
        private AdvancedPanel btnLocation_H;
        private AdvancedPanel btnLocation_G;
        private AdvancedPanel btnLocation_B;
        private AdvancedPanel btnLocation_FTY1;
        private AdvancedPanel btnLocation_N;
        private AdvancedPanel btnLocation_F;
    }
}