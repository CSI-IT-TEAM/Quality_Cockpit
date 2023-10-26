namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_BONDING_POPUP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMT_QUALITY_COCKPIT_BONDING_POPUP));
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine4 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine5 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine6 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView4 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            this.pnTop = new System.Windows.Forms.Panel();
            this.cmdPm1 = new System.Windows.Forms.Button();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pnExport = new System.Windows.Forms.Panel();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.pnBody2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer();
            this.pnBody1 = new System.Windows.Forms.Panel();
            this.pnTop.SuspendLayout();
            this.pnExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).BeginInit();
            this.pnBody1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(95)))));
            this.pnTop.Controls.Add(this.cmdPm1);
            this.pnTop.Controls.Add(this.lblHeader);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1288, 100);
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
            this.cmdPm1.Location = new System.Drawing.Point(0, 0);
            this.cmdPm1.Name = "cmdPm1";
            this.cmdPm1.Size = new System.Drawing.Size(100, 100);
            this.cmdPm1.TabIndex = 88;
            this.cmdPm1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPm1.UseVisualStyleBackColor = false;
            this.cmdPm1.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.Appearance.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Appearance.Options.UseFont = true;
            this.lblHeader.Appearance.Options.UseForeColor = true;
            this.lblHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblHeader.Location = new System.Drawing.Point(100, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1188, 100);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "  Daily Bonding";
            // 
            // pnExport
            // 
            this.pnExport.Controls.Add(this.chartControl1);
            this.pnExport.Controls.Add(this.pnBody2);
            this.pnExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnExport.Location = new System.Drawing.Point(0, 0);
            this.pnExport.Name = "pnExport";
            this.pnExport.Size = new System.Drawing.Size(1288, 548);
            this.pnExport.TabIndex = 77;
            // 
            // chartControl1
            // 
            this.chartControl1.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl1.AppearanceNameSerializable = "Chameleon";
            this.chartControl1.DataBindings = null;
            xyDiagram2.AxisX.Label.Font = new System.Drawing.Font("Calibri", 14F);
            xyDiagram2.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram2.AxisX.Title.Text = "Time";
            xyDiagram2.AxisX.Title.TextColor = System.Drawing.Color.DodgerBlue;
            xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            constantLine4.AxisValueSerializable = "6";
            constantLine4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            constantLine4.LineStyle.Thickness = 2;
            constantLine4.Name = "";
            constantLine4.Title.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            constantLine5.AxisValueSerializable = "0";
            constantLine5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            constantLine5.LineStyle.Thickness = 2;
            constantLine5.Name = "";
            constantLine5.Title.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            constantLine6.AxisValueSerializable = "3";
            constantLine6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
            constantLine6.LineStyle.Thickness = 2;
            constantLine6.Name = "";
            constantLine6.Title.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            xyDiagram2.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine4,
            constantLine5,
            constantLine6});
            xyDiagram2.AxisY.Label.Font = new System.Drawing.Font("Calibri", 14F);
            xyDiagram2.AxisY.NumericScaleOptions.AutoGrid = false;
            xyDiagram2.AxisY.NumericScaleOptions.GridSpacing = 2D;
            xyDiagram2.AxisY.Tickmarks.MinorVisible = false;
            xyDiagram2.AxisY.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram2.AxisY.Title.Text = "Bonding  (%)";
            xyDiagram2.AxisY.Title.TextColor = System.Drawing.Color.Orange;
            xyDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisualRange.Auto = false;
            xyDiagram2.AxisY.VisualRange.MaxValueSerializable = "10";
            xyDiagram2.AxisY.VisualRange.MinValueSerializable = "-2";
            xyDiagram2.AxisY.WholeRange.Auto = false;
            xyDiagram2.AxisY.WholeRange.MaxValueSerializable = "10";
            xyDiagram2.AxisY.WholeRange.MinValueSerializable = "-2";
            this.chartControl1.Diagram = xyDiagram2;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chartControl1.Legend.MarkerSize = new System.Drawing.Size(18, 12);
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Marquee";
            pointSeriesLabel2.Font = new System.Drawing.Font("Calibri", 14F);
            pointSeriesLabel2.TextPattern = "{V:#,0.##}%";
            series2.Label = pointSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Rework Rate";
            splineSeriesView3.Color = System.Drawing.Color.RoyalBlue;
            splineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            splineSeriesView3.LineMarkerOptions.Size = 9;
            splineSeriesView3.LineStyle.Thickness = 3;
            splineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.View = splineSeriesView3;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl1.SeriesTemplate.View = splineSeriesView4;
            this.chartControl1.Size = new System.Drawing.Size(1288, 548);
            this.chartControl1.TabIndex = 14;
            chartTitle2.Font = new System.Drawing.Font("Times New Roman", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            chartTitle2.Text = "SPC Chart";
            chartTitle2.TextColor = System.Drawing.Color.Blue;
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // pnBody2
            // 
            this.pnBody2.Location = new System.Drawing.Point(1521, 155);
            this.pnBody2.Name = "pnBody2";
            this.pnBody2.Size = new System.Drawing.Size(200, 100);
            this.pnBody2.TabIndex = 4;
            this.pnBody2.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnBody1
            // 
            this.pnBody1.Controls.Add(this.pnExport);
            this.pnBody1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody1.Location = new System.Drawing.Point(0, 100);
            this.pnBody1.Name = "pnBody1";
            this.pnBody1.Size = new System.Drawing.Size(1288, 525);
            this.pnBody1.TabIndex = 3;
            // 
            // SMT_QUALITY_COCKPIT_BONDING_POPUP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1288, 625);
            this.Controls.Add(this.pnBody1);
            this.Controls.Add(this.pnTop);
            this.Name = "SMT_QUALITY_COCKPIT_BONDING_POPUP";
            this.Text = "SMT_SCADA_COCKPIT_FORM2";
            this.Load += new System.EventHandler(this.SMT_QUALITY_COCKPIT_BONDING_Load);
            this.VisibleChanged += new System.EventHandler(this.SMT_QUALITY_COCKPIT_BONDING_VisibleChanged);
            this.pnTop.ResumeLayout(false);
            this.pnExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.pnBody1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnTop;
        public System.Windows.Forms.Button cmdPm1;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnExport;
        private System.Windows.Forms.Panel pnBody1;
        private System.Windows.Forms.Panel pnBody2;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}