﻿namespace FORM
{
    partial class SMT_QUALITY_COCKPIT_MAIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMT_QUALITY_COCKPIT_MAIN));
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelComponent1 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
            this.arcScaleRangeBarComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.ascInv = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tblMenu = new System.Windows.Forms.TableLayoutPanel();
            this.pnLT = new System.Windows.Forms.Panel();
            this.gpExLT = new FORM.GroupBoxEx();
            this.advancedPanel5 = new FORM.AdvancedPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.pnF2 = new System.Windows.Forms.Panel();
            this.gpExF1 = new FORM.GroupBoxEx();
            this.aPn1 = new FORM.AdvancedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnF3 = new System.Windows.Forms.Panel();
            this.pnF5 = new System.Windows.Forms.Panel();
            this.gpExF2 = new FORM.GroupBoxEx();
            this.advancedPanel1 = new FORM.AdvancedPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.gpExF3 = new FORM.GroupBoxEx();
            this.advancedPanel2 = new FORM.AdvancedPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.gpExF4 = new FORM.GroupBoxEx();
            this.advancedPanel3 = new FORM.AdvancedPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.pnF4 = new System.Windows.Forms.Panel();
            this.pnF1 = new System.Windows.Forms.Panel();
            this.gpExF5 = new FORM.GroupBoxEx();
            this.advancedPanel4 = new FORM.AdvancedPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tmrBlink = new System.Windows.Forms.Timer(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmdDefective = new System.Windows.Forms.Button();
            this.pnVJ3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnVJ = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblVinhCuu = new System.Windows.Forms.Label();
            this.pnVJ2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdDasboard = new System.Windows.Forms.Button();
            this.cmdExternal = new System.Windows.Forms.Button();
            this.cmdRework = new System.Windows.Forms.Button();
            this.cmdHFPA = new System.Windows.Forms.Button();
            this.cmdBCGrade = new System.Windows.Forms.Button();
            this.btnHumidity = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmdF5 = new System.Windows.Forms.Button();
            this.cmdF4 = new System.Windows.Forms.Button();
            this.cmdF3 = new System.Windows.Forms.Button();
            this.cmdF2 = new System.Windows.Forms.Button();
            this.cmdF1 = new System.Windows.Forms.Button();
            this.btnVendor = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.labelComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ascInv)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblMenu.SuspendLayout();
            this.gpExLT.SuspendLayout();
            this.advancedPanel5.SuspendLayout();
            this.gpExF1.SuspendLayout();
            this.aPn1.SuspendLayout();
            this.gpExF2.SuspendLayout();
            this.advancedPanel1.SuspendLayout();
            this.gpExF3.SuspendLayout();
            this.advancedPanel2.SuspendLayout();
            this.gpExF4.SuspendLayout();
            this.advancedPanel3.SuspendLayout();
            this.gpExF5.SuspendLayout();
            this.advancedPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnVJ3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnVJ.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnVJ2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrTime
            // 
            this.tmrTime.Interval = 1000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(0)))), ((int)(((byte)(95)))));
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 100);
            this.panel1.TabIndex = 21;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmdBack);
            this.flowLayoutPanel1.Controls.Add(this.cmdF5);
            this.flowLayoutPanel1.Controls.Add(this.cmdF4);
            this.flowLayoutPanel1.Controls.Add(this.cmdF3);
            this.flowLayoutPanel1.Controls.Add(this.cmdF2);
            this.flowLayoutPanel1.Controls.Add(this.cmdF1);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(985, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 100);
            this.flowLayoutPanel1.TabIndex = 73;
            // 
            // lblDate
            // 
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 32.25F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(1695, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(225, 100);
            this.lblDate.TabIndex = 1;
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDate.DoubleClick += new System.EventHandler(this.lblDate_DoubleClick);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1065, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quality Cockpit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelComponent1
            // 
            this.labelComponent1.AppearanceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 44F);
            this.labelComponent1.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:DarkOrange");
            this.labelComponent1.Name = "circularGauge1_Label1";
            this.labelComponent1.Size = new System.Drawing.SizeF(140F, 60F);
            this.labelComponent1.Text = "0";
            this.labelComponent1.ZOrder = -1001;
            // 
            // arcScaleRangeBarComponent1
            // 
            this.arcScaleRangeBarComponent1.EndOffset = 4F;
            this.arcScaleRangeBarComponent1.Name = "circularGauge1_RangeBar2";
            this.arcScaleRangeBarComponent1.RoundedCaps = true;
            this.arcScaleRangeBarComponent1.ShowBackground = true;
            this.arcScaleRangeBarComponent1.StartOffset = 80F;
            this.arcScaleRangeBarComponent1.ZOrder = -10;
            // 
            // ascInv
            // 
            this.ascInv.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.ascInv.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.ascInv.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.ascInv.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.ascInv.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.ascInv.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A");
            this.ascInv.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.ascInv.EndAngle = 90F;
            this.ascInv.MajorTickCount = 0;
            this.ascInv.MajorTickmark.FormatString = "{0:F0}";
            this.ascInv.MajorTickmark.ShapeOffset = -14F;
            this.ascInv.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1;
            this.ascInv.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.ascInv.MaxValue = 2.5F;
            this.ascInv.MinorTickCount = 0;
            this.ascInv.MinorTickmark.ShapeOffset = -7F;
            this.ascInv.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2;
            this.ascInv.Name = "scale1";
            this.ascInv.StartAngle = -270F;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tblMenu, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1920, 980);
            this.tableLayoutPanel2.TabIndex = 23;
            // 
            // tblMenu
            // 
            this.tblMenu.BackColor = System.Drawing.Color.White;
            this.tblMenu.ColumnCount = 11;
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66681F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66681F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66681F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66681F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66681F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tblMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66594F));
            this.tblMenu.Controls.Add(this.pnLT, 10, 1);
            this.tblMenu.Controls.Add(this.gpExLT, 10, 0);
            this.tblMenu.Controls.Add(this.pnF2, 2, 1);
            this.tblMenu.Controls.Add(this.gpExF1, 0, 0);
            this.tblMenu.Controls.Add(this.pnF3, 4, 1);
            this.tblMenu.Controls.Add(this.pnF5, 8, 1);
            this.tblMenu.Controls.Add(this.gpExF2, 2, 0);
            this.tblMenu.Controls.Add(this.gpExF3, 4, 0);
            this.tblMenu.Controls.Add(this.gpExF4, 6, 0);
            this.tblMenu.Controls.Add(this.pnF4, 6, 1);
            this.tblMenu.Controls.Add(this.pnF1, 0, 1);
            this.tblMenu.Controls.Add(this.gpExF5, 8, 0);
            this.tblMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMenu.Location = new System.Drawing.Point(3, 143);
            this.tblMenu.Name = "tblMenu";
            this.tblMenu.RowCount = 2;
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.35437F));
            this.tblMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.64563F));
            this.tblMenu.Size = new System.Drawing.Size(1914, 834);
            this.tblMenu.TabIndex = 23;
            // 
            // pnLT
            // 
            this.pnLT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLT.Location = new System.Drawing.Point(1598, 147);
            this.pnLT.Name = "pnLT";
            this.pnLT.Size = new System.Drawing.Size(313, 684);
            this.pnLT.TabIndex = 19;
            // 
            // gpExLT
            // 
            this.gpExLT.BackgroundPanelImage = null;
            this.gpExLT.Controls.Add(this.advancedPanel5);
            this.gpExLT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExLT.DrawGroupBorder = true;
            this.gpExLT.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExLT.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExLT.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExLT.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExLT.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExLT.GroupPanelWith = 0F;
            this.gpExLT.Location = new System.Drawing.Point(1598, 3);
            this.gpExLT.Name = "gpExLT";
            this.gpExLT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExLT.Size = new System.Drawing.Size(313, 138);
            this.gpExLT.TabIndex = 18;
            this.gpExLT.TabStop = false;
            this.gpExLT.TextBackColor = System.Drawing.Color.White;
            this.gpExLT.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExLT.TextBorderWith = 2F;
            // 
            // advancedPanel5
            // 
            this.advancedPanel5.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.advancedPanel5.Controls.Add(this.label9);
            this.advancedPanel5.EdgeWidth = 1;
            this.advancedPanel5.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel5.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.advancedPanel5.Location = new System.Drawing.Point(27, 3);
            this.advancedPanel5.Name = "advancedPanel5";
            this.advancedPanel5.Padding = new System.Windows.Forms.Padding(2);
            this.advancedPanel5.RectRadius = 0;
            this.advancedPanel5.ShadowColor = System.Drawing.Color.DimGray;
            this.advancedPanel5.ShadowShift = 5;
            this.advancedPanel5.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.advancedPanel5.Size = new System.Drawing.Size(260, 52);
            this.advancedPanel5.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel5.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.advancedPanel5.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(2, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(256, 48);
            this.label9.TabIndex = 0;
            this.label9.Tag = "VJ2";
            this.label9.Text = "Long Thanh";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label9.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // pnF2
            // 
            this.pnF2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnF2.Location = new System.Drawing.Point(322, 147);
            this.pnF2.Name = "pnF2";
            this.pnF2.Size = new System.Drawing.Size(311, 684);
            this.pnF2.TabIndex = 10;
            // 
            // gpExF1
            // 
            this.gpExF1.BackgroundPanelImage = null;
            this.gpExF1.Controls.Add(this.aPn1);
            this.gpExF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExF1.DrawGroupBorder = true;
            this.gpExF1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExF1.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExF1.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF1.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF1.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExF1.GroupPanelWith = 0F;
            this.gpExF1.Location = new System.Drawing.Point(3, 3);
            this.gpExF1.Name = "gpExF1";
            this.gpExF1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExF1.Size = new System.Drawing.Size(311, 138);
            this.gpExF1.TabIndex = 3;
            this.gpExF1.TabStop = false;
            this.gpExF1.TextBackColor = System.Drawing.Color.White;
            this.gpExF1.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExF1.TextBorderWith = 2F;
            // 
            // aPn1
            // 
            this.aPn1.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.aPn1.Controls.Add(this.label2);
            this.aPn1.EdgeWidth = 1;
            this.aPn1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.aPn1.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.aPn1.Location = new System.Drawing.Point(25, 3);
            this.aPn1.Name = "aPn1";
            this.aPn1.Padding = new System.Windows.Forms.Padding(2);
            this.aPn1.RectRadius = 0;
            this.aPn1.ShadowColor = System.Drawing.Color.DimGray;
            this.aPn1.ShadowShift = 5;
            this.aPn1.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.aPn1.Size = new System.Drawing.Size(260, 52);
            this.aPn1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.aPn1.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.aPn1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 48);
            this.label2.TabIndex = 0;
            this.label2.Tag = "F1";
            this.label2.Text = "Factory 1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // pnF3
            // 
            this.pnF3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnF3.Location = new System.Drawing.Point(641, 147);
            this.pnF3.Name = "pnF3";
            this.pnF3.Size = new System.Drawing.Size(311, 684);
            this.pnF3.TabIndex = 11;
            // 
            // pnF5
            // 
            this.pnF5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnF5.Location = new System.Drawing.Point(1279, 147);
            this.pnF5.Name = "pnF5";
            this.pnF5.Size = new System.Drawing.Size(311, 684);
            this.pnF5.TabIndex = 13;
            // 
            // gpExF2
            // 
            this.gpExF2.BackgroundPanelImage = null;
            this.gpExF2.Controls.Add(this.advancedPanel1);
            this.gpExF2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExF2.DrawGroupBorder = true;
            this.gpExF2.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExF2.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExF2.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF2.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF2.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExF2.GroupPanelWith = 0F;
            this.gpExF2.Location = new System.Drawing.Point(322, 3);
            this.gpExF2.Name = "gpExF2";
            this.gpExF2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExF2.Size = new System.Drawing.Size(311, 138);
            this.gpExF2.TabIndex = 14;
            this.gpExF2.TabStop = false;
            this.gpExF2.TextBackColor = System.Drawing.Color.White;
            this.gpExF2.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExF2.TextBorderWith = 2F;
            // 
            // advancedPanel1
            // 
            this.advancedPanel1.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.advancedPanel1.Controls.Add(this.label5);
            this.advancedPanel1.EdgeWidth = 1;
            this.advancedPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel1.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.advancedPanel1.Location = new System.Drawing.Point(26, 3);
            this.advancedPanel1.Name = "advancedPanel1";
            this.advancedPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.advancedPanel1.RectRadius = 0;
            this.advancedPanel1.ShadowColor = System.Drawing.Color.DimGray;
            this.advancedPanel1.ShadowShift = 5;
            this.advancedPanel1.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.advancedPanel1.Size = new System.Drawing.Size(260, 52);
            this.advancedPanel1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel1.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.advancedPanel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(2, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(256, 48);
            this.label5.TabIndex = 0;
            this.label5.Tag = "F2";
            this.label5.Text = "Factory 2";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // gpExF3
            // 
            this.gpExF3.BackgroundPanelImage = null;
            this.gpExF3.Controls.Add(this.advancedPanel2);
            this.gpExF3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExF3.DrawGroupBorder = true;
            this.gpExF3.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExF3.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExF3.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF3.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF3.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExF3.GroupPanelWith = 0F;
            this.gpExF3.Location = new System.Drawing.Point(641, 3);
            this.gpExF3.Name = "gpExF3";
            this.gpExF3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExF3.Size = new System.Drawing.Size(311, 138);
            this.gpExF3.TabIndex = 15;
            this.gpExF3.TabStop = false;
            this.gpExF3.TextBackColor = System.Drawing.Color.White;
            this.gpExF3.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExF3.TextBorderWith = 2F;
            // 
            // advancedPanel2
            // 
            this.advancedPanel2.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.advancedPanel2.Controls.Add(this.label6);
            this.advancedPanel2.EdgeWidth = 1;
            this.advancedPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel2.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.advancedPanel2.Location = new System.Drawing.Point(26, 3);
            this.advancedPanel2.Name = "advancedPanel2";
            this.advancedPanel2.Padding = new System.Windows.Forms.Padding(2);
            this.advancedPanel2.RectRadius = 0;
            this.advancedPanel2.ShadowColor = System.Drawing.Color.DimGray;
            this.advancedPanel2.ShadowShift = 5;
            this.advancedPanel2.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.advancedPanel2.Size = new System.Drawing.Size(260, 52);
            this.advancedPanel2.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel2.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.advancedPanel2.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(256, 48);
            this.label6.TabIndex = 0;
            this.label6.Tag = "F3";
            this.label6.Text = "Factory 3";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label6.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // gpExF4
            // 
            this.gpExF4.BackgroundPanelImage = null;
            this.gpExF4.Controls.Add(this.advancedPanel3);
            this.gpExF4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExF4.DrawGroupBorder = true;
            this.gpExF4.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExF4.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExF4.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF4.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF4.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExF4.GroupPanelWith = 0F;
            this.gpExF4.Location = new System.Drawing.Point(960, 3);
            this.gpExF4.Name = "gpExF4";
            this.gpExF4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExF4.Size = new System.Drawing.Size(311, 138);
            this.gpExF4.TabIndex = 16;
            this.gpExF4.TabStop = false;
            this.gpExF4.TextBackColor = System.Drawing.Color.White;
            this.gpExF4.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExF4.TextBorderWith = 2F;
            // 
            // advancedPanel3
            // 
            this.advancedPanel3.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.advancedPanel3.Controls.Add(this.label7);
            this.advancedPanel3.EdgeWidth = 1;
            this.advancedPanel3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel3.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.advancedPanel3.Location = new System.Drawing.Point(24, 3);
            this.advancedPanel3.Name = "advancedPanel3";
            this.advancedPanel3.Padding = new System.Windows.Forms.Padding(2);
            this.advancedPanel3.RectRadius = 0;
            this.advancedPanel3.ShadowColor = System.Drawing.Color.DimGray;
            this.advancedPanel3.ShadowShift = 5;
            this.advancedPanel3.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.advancedPanel3.Size = new System.Drawing.Size(260, 52);
            this.advancedPanel3.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel3.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.advancedPanel3.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(256, 48);
            this.label7.TabIndex = 0;
            this.label7.Tag = "F4";
            this.label7.Text = "Factory 4";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label7.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // pnF4
            // 
            this.pnF4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnF4.Location = new System.Drawing.Point(960, 147);
            this.pnF4.Name = "pnF4";
            this.pnF4.Size = new System.Drawing.Size(311, 684);
            this.pnF4.TabIndex = 12;
            // 
            // pnF1
            // 
            this.pnF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnF1.Location = new System.Drawing.Point(3, 147);
            this.pnF1.Name = "pnF1";
            this.pnF1.Size = new System.Drawing.Size(311, 684);
            this.pnF1.TabIndex = 9;
            // 
            // gpExF5
            // 
            this.gpExF5.BackgroundPanelImage = null;
            this.gpExF5.Controls.Add(this.advancedPanel4);
            this.gpExF5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpExF5.DrawGroupBorder = true;
            this.gpExF5.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpExF5.ForeColor = System.Drawing.Color.DarkOrange;
            this.gpExF5.GroupBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF5.GroupPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(32)))), ((int)(((byte)(74)))));
            this.gpExF5.GroupPanelShape = FORM.GroupBoxEx.PanelType.Rounded;
            this.gpExF5.GroupPanelWith = 0F;
            this.gpExF5.Location = new System.Drawing.Point(1279, 3);
            this.gpExF5.Name = "gpExF5";
            this.gpExF5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gpExF5.Size = new System.Drawing.Size(311, 138);
            this.gpExF5.TabIndex = 17;
            this.gpExF5.TabStop = false;
            this.gpExF5.TextBackColor = System.Drawing.Color.White;
            this.gpExF5.TextBorderColor = System.Drawing.Color.Blue;
            this.gpExF5.TextBorderWith = 2F;
            // 
            // advancedPanel4
            // 
            this.advancedPanel4.BackgroundGradientMode = FORM.AdvancedPanel.PanelGradientMode.Vertical;
            this.advancedPanel4.Controls.Add(this.label8);
            this.advancedPanel4.EdgeWidth = 1;
            this.advancedPanel4.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel4.FlatBorderColor = System.Drawing.Color.Turquoise;
            this.advancedPanel4.Location = new System.Drawing.Point(27, 3);
            this.advancedPanel4.Name = "advancedPanel4";
            this.advancedPanel4.Padding = new System.Windows.Forms.Padding(2);
            this.advancedPanel4.RectRadius = 0;
            this.advancedPanel4.ShadowColor = System.Drawing.Color.DimGray;
            this.advancedPanel4.ShadowShift = 5;
            this.advancedPanel4.ShadowStyle = FORM.AdvancedPanel.ShadowMode.Dropped;
            this.advancedPanel4.Size = new System.Drawing.Size(260, 52);
            this.advancedPanel4.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(228)))), ((int)(((byte)(234)))));
            this.advancedPanel4.Style = FORM.AdvancedPanel.BevelStyle.Flat;
            this.advancedPanel4.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Calibri", 25.25F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 48);
            this.label8.TabIndex = 0;
            this.label8.Tag = "F5";
            this.label8.Text = "Factory 5";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label8.Click += new System.EventHandler(this.lblFTY_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 11;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tableLayoutPanel3.Controls.Add(this.panel7, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmdDefective, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnVJ3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnVJ, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnVJ2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmdDasboard, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmdRework, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmdHFPA, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmdBCGrade, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel6, 10, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1914, 134);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdExternal);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1387, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 128);
            this.panel2.TabIndex = 86;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnHumidity);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1733, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(178, 128);
            this.panel6.TabIndex = 86;
            // 
            // tmrBlink
            // 
            this.tmrBlink.Interval = 500;
            this.tmrBlink.Tick += new System.EventHandler(this.tmrBlink_Tick);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnVendor);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(1560, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(167, 128);
            this.panel7.TabIndex = 87;
            // 
            // cmdDefective
            // 
            this.cmdDefective.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDefective.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdDefective.BackgroundImage")));
            this.cmdDefective.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdDefective.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdDefective.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDefective.FlatAppearance.BorderSize = 0;
            this.cmdDefective.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDefective.Font = new System.Drawing.Font("Calibri", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDefective.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDefective.Location = new System.Drawing.Point(1214, 3);
            this.cmdDefective.Name = "cmdDefective";
            this.cmdDefective.Size = new System.Drawing.Size(167, 128);
            this.cmdDefective.TabIndex = 86;
            this.cmdDefective.UseVisualStyleBackColor = false;
            this.cmdDefective.Click += new System.EventHandler(this.cmdDefective_Click);
            // 
            // pnVJ3
            // 
            this.pnVJ3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnVJ3.BackgroundImage")));
            this.pnVJ3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnVJ3.Controls.Add(this.panel5);
            this.pnVJ3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnVJ3.Location = new System.Drawing.Point(349, 3);
            this.pnVJ3.Name = "pnVJ3";
            this.pnVJ3.Size = new System.Drawing.Size(167, 128);
            this.pnVJ3.TabIndex = 81;
            this.pnVJ3.Tag = "VJ3";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(167, 26);
            this.panel5.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(60)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(54, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 26);
            this.label4.TabIndex = 7;
            this.label4.Tag = "VJ3";
            this.label4.Text = "Tan Phu";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnVJ
            // 
            this.pnVJ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnVJ.BackgroundImage")));
            this.pnVJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnVJ.Controls.Add(this.panel3);
            this.pnVJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnVJ.Location = new System.Drawing.Point(3, 3);
            this.pnVJ.Name = "pnVJ";
            this.pnVJ.Size = new System.Drawing.Size(167, 128);
            this.pnVJ.TabIndex = 79;
            this.pnVJ.Tag = "VJ1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.lblVinhCuu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(167, 26);
            this.panel3.TabIndex = 8;
            // 
            // lblVinhCuu
            // 
            this.lblVinhCuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(60)))));
            this.lblVinhCuu.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblVinhCuu.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVinhCuu.ForeColor = System.Drawing.Color.White;
            this.lblVinhCuu.Location = new System.Drawing.Point(54, 0);
            this.lblVinhCuu.Name = "lblVinhCuu";
            this.lblVinhCuu.Size = new System.Drawing.Size(113, 26);
            this.lblVinhCuu.TabIndex = 7;
            this.lblVinhCuu.Tag = "VJ1";
            this.lblVinhCuu.Text = "Vinh Cuu";
            this.lblVinhCuu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnVJ2
            // 
            this.pnVJ2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnVJ2.BackgroundImage")));
            this.pnVJ2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnVJ2.Controls.Add(this.panel4);
            this.pnVJ2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnVJ2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnVJ2.Location = new System.Drawing.Point(176, 3);
            this.pnVJ2.Name = "pnVJ2";
            this.pnVJ2.Size = new System.Drawing.Size(167, 128);
            this.pnVJ2.TabIndex = 80;
            this.pnVJ2.Tag = "VJ2";
            this.pnVJ2.Click += new System.EventHandler(this.cmd_FTY_OSD_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(167, 26);
            this.panel4.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(33)))), ((int)(((byte)(60)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(54, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 26);
            this.label3.TabIndex = 8;
            this.label3.Tag = "VJ2";
            this.label3.Text = "Long Thanh";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.cmd_FTY_OSD_Label_Click);
            // 
            // cmdDasboard
            // 
            this.cmdDasboard.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDasboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdDasboard.BackgroundImage")));
            this.cmdDasboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdDasboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdDasboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDasboard.FlatAppearance.BorderSize = 0;
            this.cmdDasboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDasboard.Location = new System.Drawing.Point(522, 3);
            this.cmdDasboard.Name = "cmdDasboard";
            this.cmdDasboard.Size = new System.Drawing.Size(167, 128);
            this.cmdDasboard.TabIndex = 82;
            this.cmdDasboard.UseVisualStyleBackColor = false;
            this.cmdDasboard.Click += new System.EventHandler(this.CmdDasboard_Click);
            // 
            // cmdExternal
            // 
            this.cmdExternal.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExternal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdExternal.BackgroundImage")));
            this.cmdExternal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdExternal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdExternal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdExternal.FlatAppearance.BorderSize = 0;
            this.cmdExternal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdExternal.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExternal.ForeColor = System.Drawing.Color.White;
            this.cmdExternal.Location = new System.Drawing.Point(0, 0);
            this.cmdExternal.Name = "cmdExternal";
            this.cmdExternal.Size = new System.Drawing.Size(167, 128);
            this.cmdExternal.TabIndex = 87;
            this.cmdExternal.Text = "External OS&&D";
            this.cmdExternal.UseVisualStyleBackColor = false;
            this.cmdExternal.Click += new System.EventHandler(this.cmdExternal_Click);
            // 
            // cmdRework
            // 
            this.cmdRework.BackColor = System.Drawing.SystemColors.Control;
            this.cmdRework.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdRework.BackgroundImage")));
            this.cmdRework.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdRework.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdRework.FlatAppearance.BorderSize = 0;
            this.cmdRework.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdRework.Location = new System.Drawing.Point(695, 3);
            this.cmdRework.Name = "cmdRework";
            this.cmdRework.Size = new System.Drawing.Size(167, 128);
            this.cmdRework.TabIndex = 82;
            this.cmdRework.UseVisualStyleBackColor = false;
            this.cmdRework.Click += new System.EventHandler(this.cmdRework_Click);
            // 
            // cmdHFPA
            // 
            this.cmdHFPA.BackColor = System.Drawing.SystemColors.Control;
            this.cmdHFPA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdHFPA.BackgroundImage")));
            this.cmdHFPA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdHFPA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdHFPA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdHFPA.FlatAppearance.BorderSize = 0;
            this.cmdHFPA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdHFPA.Font = new System.Drawing.Font("Calibri", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHFPA.ForeColor = System.Drawing.Color.White;
            this.cmdHFPA.Location = new System.Drawing.Point(868, 3);
            this.cmdHFPA.Name = "cmdHFPA";
            this.cmdHFPA.Size = new System.Drawing.Size(167, 128);
            this.cmdHFPA.TabIndex = 84;
            this.cmdHFPA.Text = "HFPA";
            this.cmdHFPA.UseVisualStyleBackColor = false;
            this.cmdHFPA.Click += new System.EventHandler(this.cmdHFPA_Click_1);
            // 
            // cmdBCGrade
            // 
            this.cmdBCGrade.BackColor = System.Drawing.SystemColors.Control;
            this.cmdBCGrade.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBCGrade.BackgroundImage")));
            this.cmdBCGrade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdBCGrade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBCGrade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdBCGrade.FlatAppearance.BorderSize = 0;
            this.cmdBCGrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBCGrade.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBCGrade.ForeColor = System.Drawing.Color.White;
            this.cmdBCGrade.Location = new System.Drawing.Point(1041, 3);
            this.cmdBCGrade.Name = "cmdBCGrade";
            this.cmdBCGrade.Size = new System.Drawing.Size(167, 128);
            this.cmdBCGrade.TabIndex = 85;
            this.cmdBCGrade.UseVisualStyleBackColor = false;
            this.cmdBCGrade.Click += new System.EventHandler(this.cmdBCGrade_Click);
            // 
            // btnHumidity
            // 
            this.btnHumidity.BackColor = System.Drawing.SystemColors.Control;
            this.btnHumidity.BackgroundImage = global::FORM.Properties.Resources.humidity;
            this.btnHumidity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHumidity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHumidity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHumidity.FlatAppearance.BorderSize = 0;
            this.btnHumidity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHumidity.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHumidity.ForeColor = System.Drawing.Color.Black;
            this.btnHumidity.Location = new System.Drawing.Point(0, 0);
            this.btnHumidity.Name = "btnHumidity";
            this.btnHumidity.Size = new System.Drawing.Size(178, 128);
            this.btnHumidity.TabIndex = 87;
            this.btnHumidity.Text = "Humidity Tracking";
            this.btnHumidity.UseVisualStyleBackColor = false;
            this.btnHumidity.Click += new System.EventHandler(this.btnHumidity_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.BackColor = System.Drawing.Color.Transparent;
            this.cmdBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdBack.BackgroundImage")));
            this.cmdBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdBack.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdBack.FlatAppearance.BorderSize = 0;
            this.cmdBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBack.Location = new System.Drawing.Point(600, 0);
            this.cmdBack.Margin = new System.Windows.Forms.Padding(0);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(100, 100);
            this.cmdBack.TabIndex = 67;
            this.cmdBack.UseVisualStyleBackColor = false;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // cmdF5
            // 
            this.cmdF5.BackColor = System.Drawing.Color.Transparent;
            this.cmdF5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdF5.BackgroundImage")));
            this.cmdF5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdF5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdF5.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdF5.FlatAppearance.BorderSize = 0;
            this.cmdF5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF5.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdF5.ForeColor = System.Drawing.Color.Navy;
            this.cmdF5.Location = new System.Drawing.Point(507, 3);
            this.cmdF5.Name = "cmdF5";
            this.cmdF5.Size = new System.Drawing.Size(90, 90);
            this.cmdF5.TabIndex = 69;
            this.cmdF5.Text = "F5";
            this.cmdF5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdF5.UseVisualStyleBackColor = false;
            this.cmdF5.Visible = false;
            // 
            // cmdF4
            // 
            this.cmdF4.BackColor = System.Drawing.Color.Transparent;
            this.cmdF4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdF4.BackgroundImage")));
            this.cmdF4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdF4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdF4.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdF4.FlatAppearance.BorderSize = 0;
            this.cmdF4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF4.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdF4.ForeColor = System.Drawing.Color.Navy;
            this.cmdF4.Location = new System.Drawing.Point(411, 3);
            this.cmdF4.Name = "cmdF4";
            this.cmdF4.Size = new System.Drawing.Size(90, 90);
            this.cmdF4.TabIndex = 68;
            this.cmdF4.Text = "F4";
            this.cmdF4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdF4.UseVisualStyleBackColor = false;
            this.cmdF4.Visible = false;
            this.cmdF4.Click += new System.EventHandler(this.cmdF4_Click);
            // 
            // cmdF3
            // 
            this.cmdF3.BackColor = System.Drawing.Color.Transparent;
            this.cmdF3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdF3.BackgroundImage")));
            this.cmdF3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdF3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdF3.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdF3.FlatAppearance.BorderSize = 0;
            this.cmdF3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF3.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdF3.ForeColor = System.Drawing.Color.Navy;
            this.cmdF3.Location = new System.Drawing.Point(315, 3);
            this.cmdF3.Name = "cmdF3";
            this.cmdF3.Size = new System.Drawing.Size(90, 90);
            this.cmdF3.TabIndex = 70;
            this.cmdF3.Text = "F3";
            this.cmdF3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdF3.UseVisualStyleBackColor = false;
            this.cmdF3.Visible = false;
            // 
            // cmdF2
            // 
            this.cmdF2.BackColor = System.Drawing.Color.Transparent;
            this.cmdF2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdF2.BackgroundImage")));
            this.cmdF2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdF2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdF2.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdF2.FlatAppearance.BorderSize = 0;
            this.cmdF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF2.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdF2.ForeColor = System.Drawing.Color.Navy;
            this.cmdF2.Location = new System.Drawing.Point(219, 3);
            this.cmdF2.Name = "cmdF2";
            this.cmdF2.Size = new System.Drawing.Size(90, 90);
            this.cmdF2.TabIndex = 71;
            this.cmdF2.Text = "F2";
            this.cmdF2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdF2.UseVisualStyleBackColor = false;
            this.cmdF2.Visible = false;
            // 
            // cmdF1
            // 
            this.cmdF1.BackColor = System.Drawing.Color.Transparent;
            this.cmdF1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdF1.BackgroundImage")));
            this.cmdF1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cmdF1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdF1.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.cmdF1.FlatAppearance.BorderSize = 0;
            this.cmdF1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdF1.Font = new System.Drawing.Font("Calibri", 32.75F, System.Drawing.FontStyle.Bold);
            this.cmdF1.ForeColor = System.Drawing.Color.Navy;
            this.cmdF1.Location = new System.Drawing.Point(123, 3);
            this.cmdF1.Name = "cmdF1";
            this.cmdF1.Size = new System.Drawing.Size(90, 90);
            this.cmdF1.TabIndex = 72;
            this.cmdF1.Text = "F1";
            this.cmdF1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdF1.UseVisualStyleBackColor = false;
            this.cmdF1.Visible = false;
            // 
            // btnVendor
            // 
            this.btnVendor.BackColor = System.Drawing.SystemColors.Control;
            this.btnVendor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVendor.BackgroundImage")));
            this.btnVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVendor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnVendor.FlatAppearance.BorderSize = 0;
            this.btnVendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVendor.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold);
            this.btnVendor.ForeColor = System.Drawing.Color.Yellow;
            this.btnVendor.Location = new System.Drawing.Point(0, 0);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(167, 128);
            this.btnVendor.TabIndex = 85;
            this.btnVendor.Text = "Vendor Quality";
            this.btnVendor.UseVisualStyleBackColor = false;
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // SMT_QUALITY_COCKPIT_MAIN
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SMT_QUALITY_COCKPIT_MAIN";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SMT_QUALITY_COCKPIT_MAIN_Load);
            this.VisibleChanged += new System.EventHandler(this.SMT_SCADA_COCKPIT_MENU_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.labelComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ascInv)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tblMenu.ResumeLayout(false);
            this.gpExLT.ResumeLayout(false);
            this.advancedPanel5.ResumeLayout(false);
            this.gpExF1.ResumeLayout(false);
            this.aPn1.ResumeLayout(false);
            this.gpExF2.ResumeLayout(false);
            this.advancedPanel1.ResumeLayout(false);
            this.gpExF3.ResumeLayout(false);
            this.advancedPanel2.ResumeLayout(false);
            this.gpExF4.ResumeLayout(false);
            this.advancedPanel3.ResumeLayout(false);
            this.gpExF5.ResumeLayout(false);
            this.advancedPanel4.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.pnVJ3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnVJ.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnVJ2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand12;
        private System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button cmdBack;
        public System.Windows.Forms.Button cmdF4;
        public System.Windows.Forms.Button cmdF1;
        public System.Windows.Forms.Button cmdF2;
        public System.Windows.Forms.Button cmdF3;
        public System.Windows.Forms.Button cmdF5;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge1;
        private DevExpress.XtraGauges.Win.Base.LabelComponent labelComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent ascInv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tblMenu;
        private System.Windows.Forms.Panel pnF3;
        private System.Windows.Forms.Panel pnF4;
        private System.Windows.Forms.Panel pnF5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pnVJ;
        private System.Windows.Forms.Panel pnF1;
        private GroupBoxEx gpExF1;
        private AdvancedPanel aPn1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnF2;
        private System.Windows.Forms.Panel pnVJ2;
        private System.Windows.Forms.Panel pnVJ3;
        private GroupBoxEx gpExF2;
        private AdvancedPanel advancedPanel1;
        private System.Windows.Forms.Label label5;
        private GroupBoxEx gpExF3;
        private AdvancedPanel advancedPanel2;
        private System.Windows.Forms.Label label6;
        private GroupBoxEx gpExF4;
        private AdvancedPanel advancedPanel3;
        private System.Windows.Forms.Label label7;
        private GroupBoxEx gpExF5;
        private AdvancedPanel advancedPanel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer tmrBlink;
        private System.Windows.Forms.Button cmdDasboard;
        private GroupBoxEx gpExLT;
        private AdvancedPanel advancedPanel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnLT;
        private System.Windows.Forms.Button cmdRework;
        private System.Windows.Forms.Button cmdHFPA;
        private System.Windows.Forms.Button cmdBCGrade;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button cmdDefective;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblVinhCuu;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button cmdExternal;
        private System.Windows.Forms.Button btnHumidity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnVendor;
    }
}