namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_REWORK
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
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY3 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView3 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView3 = new DevExpress.XtraCharts.LineSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMT_QUALITY_COCKPIT_REWORK));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.pnControl = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpYMDT = new DevExpress.XtraEditors.DateEdit();
            this.dtpYMD = new DevExpress.XtraEditors.DateEdit();
            this.cboLine = new System.Windows.Forms.ComboBox();
            this.cboPlant = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnTop = new System.Windows.Forms.Panel();
            this.cmdPm1 = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHeader = new DevExpress.XtraEditors.LabelControl();
            this.pnExport = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnBody1 = new System.Windows.Forms.Panel();
            this.pnGrid = new System.Windows.Forms.Panel();
            this.grdView = new DevExpress.XtraGrid.GridControl();
            this.gvwView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnC = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).BeginInit();
            this.pnControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMDT.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMDT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMD.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMD.Properties)).BeginInit();
            this.pnTop.SuspendLayout();
            this.pnExport.SuspendLayout();
            this.pnBody1.SuspendLayout();
            this.pnGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwView)).BeginInit();
            this.pnC.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartControl1
            // 
            this.chartControl1.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnLoad;
            this.chartControl1.AppearanceNameSerializable = "Chameleon";
            this.chartControl1.DataBindings = null;
            xyDiagram3.AxisX.Label.Angle = -45;
            xyDiagram3.AxisX.Label.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram3.AxisX.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram3.AxisX.Title.Text = "Date";
            xyDiagram3.AxisX.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            xyDiagram3.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.Label.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram3.AxisY.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            xyDiagram3.AxisY.Title.Text = "Rework Q\'ty (Prs)";
            xyDiagram3.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            xyDiagram3.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY3.AxisID = 0;
            secondaryAxisY3.Color = System.Drawing.Color.White;
            secondaryAxisY3.Label.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            secondaryAxisY3.Name = "Secondary AxisY 1";
            secondaryAxisY3.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            secondaryAxisY3.Title.Text = "Rework Rate (%)";
            secondaryAxisY3.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(108)))), ((int)(((byte)(9)))));
            secondaryAxisY3.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            secondaryAxisY3.VisibleInPanesSerializable = "-1";
            secondaryAxisY3.VisualRange.Auto = false;
            secondaryAxisY3.VisualRange.MaxValueSerializable = "9.8";
            secondaryAxisY3.VisualRange.MinValueSerializable = "0";
            secondaryAxisY3.WholeRange.Auto = false;
            secondaryAxisY3.WholeRange.MaxValueSerializable = "9.8";
            secondaryAxisY3.WholeRange.MinValueSerializable = "0";
            xyDiagram3.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY3});
            this.chartControl1.Diagram = xyDiagram3;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartControl1.Legend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Legend.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chartControl1.Legend.MarkerSize = new System.Drawing.Size(18, 12);
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Marquee";
            sideBySideBarSeriesLabel3.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            series5.Label = sideBySideBarSeriesLabel3;
            series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.Name = "Production";
            sideBySideBarSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            sideBySideBarSeriesView3.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
            series5.View = sideBySideBarSeriesView3;
            series6.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series6.Name = "Rework Rate";
            lineSeriesView3.AxisYName = "Secondary AxisY 1";
            lineSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            lineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            lineSeriesView3.LineMarkerOptions.Size = 9;
            lineSeriesView3.LineStyle.Thickness = 3;
            lineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            series6.View = lineSeriesView3;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5,
        series6};
            this.chartControl1.Size = new System.Drawing.Size(1888, 616);
            this.chartControl1.TabIndex = 12;
            // 
            // pnControl
            // 
            this.pnControl.BackColor = System.Drawing.Color.Transparent;
            this.pnControl.Controls.Add(this.btnSearch);
            this.pnControl.Controls.Add(this.dtpYMDT);
            this.pnControl.Controls.Add(this.dtpYMD);
            this.pnControl.Controls.Add(this.cboLine);
            this.pnControl.Controls.Add(this.cboPlant);
            this.pnControl.Controls.Add(this.label3);
            this.pnControl.Controls.Add(this.label4);
            this.pnControl.Controls.Add(this.label6);
            this.pnControl.Controls.Add(this.label2);
            this.pnControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnControl.Location = new System.Drawing.Point(0, 76);
            this.pnControl.Name = "pnControl";
            this.pnControl.Size = new System.Drawing.Size(1888, 50);
            this.pnControl.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::FORM.Properties.Resources.btnSearch1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(1046, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(123, 39);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpYMDT
            // 
            this.dtpYMDT.EditValue = null;
            this.dtpYMDT.Location = new System.Drawing.Point(466, 7);
            this.dtpYMDT.Name = "dtpYMDT";
            this.dtpYMDT.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dtpYMDT.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.Appearance.Options.UseBackColor = true;
            this.dtpYMDT.Properties.Appearance.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.Button.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.Button.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.ButtonHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.ButtonHighlighted.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.ButtonPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.ButtonPressed.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.CalendarHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.CalendarHeader.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCell.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCell.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellDisabled.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellDisabled.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellHoliday.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellHoliday.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellInactive.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellInactive.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellPressed.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSelected.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSelected.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecial.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecial.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialPressed.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialSelected.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellSpecialSelected.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellToday.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.DayCellToday.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.HeaderPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.HeaderPressed.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.WeekDay.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.WeekDay.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceFocused.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceFocused.Options.UseFont = true;
            this.dtpYMDT.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMDT.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dtpYMDT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpYMDT.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpYMDT.Properties.CellSize = new System.Drawing.Size(70, 50);
            this.dtpYMDT.Properties.TodayDate = new System.DateTime(2019, 9, 24, 9, 47, 32, 0);
            this.dtpYMDT.Size = new System.Drawing.Size(190, 36);
            this.dtpYMDT.TabIndex = 22;
            // 
            // dtpYMD
            // 
            this.dtpYMD.EditValue = null;
            this.dtpYMD.Location = new System.Drawing.Point(141, 7);
            this.dtpYMD.Name = "dtpYMD";
            this.dtpYMD.Properties.Appearance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dtpYMD.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.Appearance.Options.UseBackColor = true;
            this.dtpYMD.Properties.Appearance.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.Button.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.Button.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.ButtonHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.ButtonHighlighted.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.ButtonPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.ButtonPressed.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.CalendarHeader.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.CalendarHeader.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCell.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCell.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellDisabled.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellDisabled.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellHoliday.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellHoliday.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellInactive.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellInactive.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellPressed.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSelected.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSelected.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecial.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecial.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialPressed.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialSelected.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellSpecialSelected.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.DayCellToday.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.DayCellToday.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.HeaderPressed.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.HeaderPressed.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.WeekDay.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.WeekDay.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceDisabled.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceFocused.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceFocused.Options.UseFont = true;
            this.dtpYMD.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.dtpYMD.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dtpYMD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpYMD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpYMD.Properties.CellSize = new System.Drawing.Size(70, 50);
            this.dtpYMD.Properties.TodayDate = new System.DateTime(2019, 9, 24, 9, 47, 32, 0);
            this.dtpYMD.Size = new System.Drawing.Size(190, 36);
            this.dtpYMD.TabIndex = 22;
            // 
            // cboLine
            // 
            this.cboLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLine.Font = new System.Drawing.Font("Calibri", 17F, System.Drawing.FontStyle.Bold);
            this.cboLine.FormattingEnabled = true;
            this.cboLine.Location = new System.Drawing.Point(921, 6);
            this.cboLine.Name = "cboLine";
            this.cboLine.Size = new System.Drawing.Size(110, 36);
            this.cboLine.TabIndex = 21;
            // 
            // cboPlant
            // 
            this.cboPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPlant.Font = new System.Drawing.Font("Calibri", 17F, System.Drawing.FontStyle.Bold);
            this.cboPlant.FormattingEnabled = true;
            this.cboPlant.Location = new System.Drawing.Point(733, 6);
            this.cboPlant.Name = "cboPlant";
            this.cboPlant.Size = new System.Drawing.Size(110, 36);
            this.cboPlant.TabIndex = 21;
            this.cboPlant.SelectedIndexChanged += new System.EventHandler(this.cboPlant_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(337, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 36);
            this.label3.TabIndex = 20;
            this.label3.Text = "   Date To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(850, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 36);
            this.label4.TabIndex = 19;
            this.label4.Text = " Line";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 36);
            this.label6.TabIndex = 20;
            this.label6.Text = "Date From";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(662, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 36);
            this.label2.TabIndex = 19;
            this.label2.Text = " Plant";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.cmdPm1);
            this.pnTop.Controls.Add(this.lblDate);
            this.pnTop.Controls.Add(this.lblHeader);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(1888, 76);
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
            this.lblDate.Location = new System.Drawing.Point(1653, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(235, 76);
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
            this.lblHeader.Size = new System.Drawing.Size(1622, 76);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "        Daily Rework";
            // 
            // pnExport
            // 
            this.pnExport.Controls.Add(this.chartControl1);
            this.pnExport.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnExport.Location = new System.Drawing.Point(0, 0);
            this.pnExport.Name = "pnExport";
            this.pnExport.Size = new System.Drawing.Size(1888, 616);
            this.pnExport.TabIndex = 77;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnBody1
            // 
            this.pnBody1.Controls.Add(this.pnGrid);
            this.pnBody1.Controls.Add(this.pnC);
            this.pnBody1.Controls.Add(this.pnExport);
            this.pnBody1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody1.Location = new System.Drawing.Point(0, 126);
            this.pnBody1.Name = "pnBody1";
            this.pnBody1.Size = new System.Drawing.Size(1888, 916);
            this.pnBody1.TabIndex = 3;
            // 
            // pnGrid
            // 
            this.pnGrid.Controls.Add(this.grdView);
            this.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnGrid.Location = new System.Drawing.Point(0, 670);
            this.pnGrid.Name = "pnGrid";
            this.pnGrid.Size = new System.Drawing.Size(1888, 246);
            this.pnGrid.TabIndex = 78;
            // 
            // grdView
            // 
            this.grdView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdView.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridLevelNode1.RelationName = "Level1";
            this.grdView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grdView.Location = new System.Drawing.Point(0, 0);
            this.grdView.MainView = this.gvwView;
            this.grdView.Name = "grdView";
            this.grdView.Size = new System.Drawing.Size(1888, 246);
            this.grdView.TabIndex = 8;
            this.grdView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvwView});
            // 
            // gvwView
            // 
            this.gvwView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.gvwView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvwView.Appearance.Row.Font = new System.Drawing.Font("Calibri", 15.75F);
            this.gvwView.Appearance.Row.Options.UseFont = true;
            this.gvwView.ColumnPanelRowHeight = 50;
            this.gvwView.GridControl = this.grdView;
            this.gvwView.Name = "gvwView";
            this.gvwView.OptionsBehavior.Editable = false;
            this.gvwView.OptionsBehavior.ReadOnly = true;
            this.gvwView.OptionsCustomization.AllowColumnMoving = false;
            this.gvwView.OptionsCustomization.AllowFilter = false;
            this.gvwView.OptionsCustomization.AllowGroup = false;
            this.gvwView.OptionsView.ShowGroupPanel = false;
            this.gvwView.OptionsView.ShowIndicator = false;
            this.gvwView.RowHeight = 50;
            this.gvwView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvwView_RowCellStyle);
            // 
            // pnC
            // 
            this.pnC.Controls.Add(this.label7);
            this.pnC.Controls.Add(this.label1);
            this.pnC.Controls.Add(this.label5);
            this.pnC.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnC.Location = new System.Drawing.Point(0, 616);
            this.pnC.Name = "pnC";
            this.pnC.Size = new System.Drawing.Size(1888, 54);
            this.pnC.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Red;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1719, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 39);
            this.label7.TabIndex = 421;
            this.label7.Text = "Rate <=6%";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1537, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 39);
            this.label1.TabIndex = 420;
            this.label1.Text = "3% < Rate <=6%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1360, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 39);
            this.label5.TabIndex = 419;
            this.label5.Text = "Rate <=3%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SMT_QUALITY_COCKPIT_REWORK
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1888, 1042);
            this.Controls.Add(this.pnBody1);
            this.Controls.Add(this.pnControl);
            this.Controls.Add(this.pnTop);
            this.Name = "SMT_QUALITY_COCKPIT_REWORK";
            this.Text = "SMT_SCADA_COCKPIT_FORM2";
            this.Load += new System.EventHandler(this.SMT_QUALITY_COCKPIT_FORM1_Load);
            this.VisibleChanged += new System.EventHandler(this.SMT_QUALITY_COCKPIT_REWORK_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.pnControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMDT.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMDT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpYMD.Properties)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.pnExport.ResumeLayout(false);
            this.pnBody1.ResumeLayout(false);
            this.pnGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvwView)).EndInit();
            this.pnC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnControl;
        private System.Windows.Forms.Panel pnTop;
        public System.Windows.Forms.Button cmdPm1;
        private System.Windows.Forms.Label lblDate;
        private DevExpress.XtraEditors.LabelControl lblHeader;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private System.Windows.Forms.Panel pnExport;
        private System.Windows.Forms.Panel pnBody1;
        private System.Windows.Forms.Panel pnC;
        private System.Windows.Forms.Panel pnGrid;
        private DevExpress.XtraGrid.GridControl grdView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvwView;
        private DevExpress.XtraEditors.DateEdit dtpYMDT;
        private DevExpress.XtraEditors.DateEdit dtpYMD;
        private System.Windows.Forms.ComboBox cboLine;
        private System.Windows.Forms.ComboBox cboPlant;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}