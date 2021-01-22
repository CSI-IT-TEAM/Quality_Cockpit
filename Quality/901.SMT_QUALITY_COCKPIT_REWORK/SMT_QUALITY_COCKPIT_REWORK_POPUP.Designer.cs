namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_REWORK_POPUP
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine2 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.ConstantLine constantLine3 = new DevExpress.XtraCharts.ConstantLine();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chartRW = new DevExpress.XtraCharts.ChartControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chartRW);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1253, 548);
            this.panel1.TabIndex = 0;
            // 
            // chartRW
            // 
            this.chartRW.DataBindings = null;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            constantLine1.AxisValueSerializable = "6";
            constantLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            constantLine1.Name = "Max";
            constantLine2.AxisValueSerializable = "0";
            constantLine2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            constantLine2.Name = "Min";
            constantLine3.AxisValueSerializable = "3";
            constantLine3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            constantLine3.Name = "Average";
            xyDiagram1.AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            constantLine1,
            constantLine2,
            constantLine3});
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisY.Title.Text = "REWORK(%)";
            xyDiagram1.AxisY.Title.TextColor = System.Drawing.Color.Blue;
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartRW.Diagram = xyDiagram1;
            this.chartRW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartRW.Legend.Name = "Default Legend";
            this.chartRW.Location = new System.Drawing.Point(0, 0);
            this.chartRW.Name = "chartRW";
            series1.Name = "REWORK";
            series1.View = splineSeriesView1;
            this.chartRW.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartRW.Size = new System.Drawing.Size(1253, 548);
            this.chartRW.TabIndex = 0;
            // 
            // SMT_QUALITY_COCKPIT_REWORK_POPUP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1253, 548);
            this.Controls.Add(this.panel1);
            this.Name = "SMT_QUALITY_COCKPIT_REWORK_POPUP";
            this.Text = "SMT_SCADA_COCKPIT_FORM2";
            this.Load += new System.EventHandler(this.SMT_QUALITY_COCKPIT_FORM1_Load);
            this.VisibleChanged += new System.EventHandler(this.SMT_QUALITY_COCKPIT_REWORK_POPUP_VisibleChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ChartControl chartControl1;
        private ChartControl chartRW;
    }
}