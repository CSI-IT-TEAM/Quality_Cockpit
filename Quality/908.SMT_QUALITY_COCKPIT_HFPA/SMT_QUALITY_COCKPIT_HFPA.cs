using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_HFPA : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  HFPA By Day";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sDate = "DAY";
        string _Line = "TOT";
        DataTable dtWeek = null;
        DataTable _dtArea = null;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_HFPA()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        


        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {         
            btnDay.Enabled = false;
            btnWeek.Enabled = false;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;

            cboDateTo.EditValue = DateTime.Now;
            DateTime dt = DateTime.Now;
            DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            cboDateFr.EditValue = fistdate;//DateTime.Now.AddDays(-1);
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //cboPlant.SelectedValue = ComVar.Var._strValue1;
                //cboLine.SelectedValue = ComVar.Var._strValue2;
                //clear_chart();
                _time = 30;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Dispose();
            }

        }

        #endregion ========= [Form Init] ==============================================
       
        #region ========= [Timer Event] ==========================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd")) + "\n\r" + string.Format(DateTime.Now.ToString("HH:mm:ss"));
            _time++;
            if (_time >= 30)
            {              
                SetData();
                _time = 0;               

            }
        }

        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================
        private void cboDateFr_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }

        private void cboDateTo_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }
        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnDay_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sDate = "DAY";
            lblHeader.Text = "HFPA by day";
            //clear_chart();
            SetData();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = false;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sDate = "WEEK";
            //lblHeader.Text = "       HFPA by week";
            //clear_chart();
            SetData();

        }

        private async void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chart1.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                string strdate = cboDateFr.DateTime.ToString("yyyyMMdd");
                string strdateto = cboDateTo.DateTime.ToString("yyyyMMdd");
                string sYM = "";
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    sYM = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
                        {
                            _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                        }
                    }
                }
                else
                {
                    if (hit.AxisLabelItem == null)
                    {
                        _Line = "TOT";
                    }
                    else
                    {
                        sYM = hit.AxisLabelItem.AxisValue.ToString();
                        for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                        {
                            if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
                            {
                                _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                            }
                        }
                    }

                }

                SMT_QUALITY_COCKPIT_HFPA_POP view = new SMT_QUALITY_COCKPIT_HFPA_POP(strdate, strdateto, _Line);
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            //    //splashScreenManager1.ShowWaitForm();
            //    ChartHitInfo hit = chart1.CalcHitInfo(e.Location);
            //    SeriesPoint point = hit.SeriesPoint;
            //    if (point != null)
            //    {
            //        string sYM = point.Argument;

            //        for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
            //        {
            //            if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
            //            {
            //                _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
            //            }
            //        }

            //        DataSet dsData = await sbGetHFPA(sDate, _Line);
            //        if (dsData == null) return;
            //        DataTable dtChart = dsData.Tables[0];
            //        DataTable dtChart1 = dsData.Tables[1];
            //        DataTable dtChart2 = dsData.Tables[2];
            //        dtWeek = dsData.Tables[3];
            //        if (sDate == "WEEK")
            //            lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

            //        //SetChart(dtChart);
            //        //SetChart1(dtChart1);
            //        if (dtChart2 != null && dtChart2.Rows.Count > 0)
            //        {
            //            DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
            //            chartControl3.Titles.Clear();
            //            if (sYM != null)

            //                if (sYM == "iD")
            //                    chartTitle.Text = "Plant ID" + " HFPA by Reason";
            //                else if (sYM == "Vj" || sYM == "VJ")
            //                    chartTitle.Text = "HFPA by Reason";
            //                else
            //                    chartTitle.Text = "Plant " + sYM + " HFPA by Reason";
            //            else
            //                chartTitle.Text = "HFPA by Reason";

            //            chartControl3.DataSource = dtChart2;
            //            chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
            //            chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

            //            // Define the alignment of the titles.
            //            chartTitle.Alignment = StringAlignment.Center;

            //            // Place the titles where it's required.
            //            chartTitle.Dock = ChartTitleDockStyle.Top;

            //            // Customize a title's appearance.
            //            chartTitle.Antialiasing = true;
            //            chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
            //            chartTitle.TextColor = Color.Blue;
            //            chartTitle.Indent = 10;
            //            this.chartControl3.Titles.Add(chartTitle);
            //            //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
            //        }
            //        else
            //        {
            //            chartControl3.DataSource = null;
            //        }
            //    }
            //    else if (hit.ChartTitle != null)
            //    {
            //        DataSet dsData = await sbGetHFPA(sDate, "TOT");

            //        if (dsData == null) return;
            //        DataTable dtChart = dsData.Tables[0];
            //        DataTable dtChart1 = dsData.Tables[1];
            //        DataTable dtChart2 = dsData.Tables[2];
            //        dtWeek = dsData.Tables[3];
            //        if (sDate == "WEEK")
            //            lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

            //        //SetChart(dtChart);
            //        //SetChart1(dtChart1);
            //        if (dtChart2 != null && dtChart2.Rows.Count > 0)
            //        {
            //            DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
            //            chartControl3.Titles.Clear();
            //            chartTitle.Text = "HFPA by Reason";
            //            chartControl3.DataSource = dtChart2;
            //            chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
            //            chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

            //            // Define the alignment of the titles.
            //            chartTitle.Alignment = StringAlignment.Center;

            //            // Place the titles where it's required.
            //            chartTitle.Dock = ChartTitleDockStyle.Top;

            //            // Customize a title's appearance.
            //            chartTitle.Antialiasing = true;
            //            chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
            //            chartTitle.TextColor = Color.Blue;
            //            chartTitle.Indent = 10;
            //            this.chartControl3.Titles.Add(chartTitle);
            //            //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
            //        }
            //    }
            //    else
            //    {
            //        if (hit.AxisLabelItem == null) return;
            //        string sYM = hit.AxisLabelItem.AxisValue.ToString();

            //        for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
            //        {
            //            if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
            //            {
            //                _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
            //            }
            //        }

            //        DataSet dsData = await sbGetHFPA(sDate, _Line);
            //        if (dsData == null) return;
            //        DataTable dtChart = dsData.Tables[0];
            //        DataTable dtChart1 = dsData.Tables[1];
            //        DataTable dtChart2 = dsData.Tables[2];
            //        dtWeek = dsData.Tables[3];
            //        if (sDate == "WEEK")
            //            lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

            //        //SetChart(dtChart);
            //        //SetChart1(dtChart1);
            //        if (dtChart2 != null && dtChart2.Rows.Count > 0)
            //        {
            //            DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
            //            chartControl3.Titles.Clear();
            //            if (sYM != null)
            //                if (sYM == "iD")
            //                {
            //                    chartTitle.Text = "Plant ID" + " HFPA by Reason";
            //                }
            //                else if (sYM == "Vj" || sYM == "VJ")
            //                    chartTitle.Text = "HFPA by Reason";
            //                else
            //                {
            //                    chartTitle.Text = "Plant " + sYM + " HFPA by Reason";
            //                }

            //            else
            //                chartTitle.Text = "HFPA by Reason";
            //            chartControl3.DataSource = dtChart2;
            //            chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
            //            chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

            //            // Define the alignment of the titles.
            //            chartTitle.Alignment = StringAlignment.Center;

            //            // Place the titles where it's required.
            //            chartTitle.Dock = ChartTitleDockStyle.Top;

            //            // Customize a title's appearance.
            //            chartTitle.Antialiasing = true;
            //            chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
            //            chartTitle.TextColor = Color.Blue;
            //            chartTitle.Indent = 10;
            //            this.chartControl3.Titles.Add(chartTitle);
            //            //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
            //        }
            //        else
            //        {
            //            chartControl3.DataSource = null;
            //        }
            //    }
            //    //splashScreenManager1.CloseWaitForm();
            //}
            //catch (Exception ex)
            //{
            //   // splashScreenManager1.CloseWaitForm();
            //    MessageBox.Show(ex.Message);
            //}
        }
        private async void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //splashScreenManager1.ShowWaitForm();
                ChartHitInfo hit = chart1.CalcHitInfo(e.Location);
                SeriesPoint point = hit.SeriesPoint;
                if (point != null)
                {
                    string sYM = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
                        {
                            _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                        }
                    }

                    DataSet dsData = await sbGetHFPA(sDate, _Line);
                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    dtWeek = dsData.Tables[3];
                    if (sDate == "WEEK")
                        lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

                    //SetChart(dtChart);
                    //SetChart1(dtChart1);
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chartControl3.Titles.Clear();
                        if (sYM != null)

                            if (sYM == "iD")
                                chartTitle.Text = "Plant ID" + " HFPA by Reason";
                            else if (sYM == "Vj" || sYM == "VJ")
                                chartTitle.Text = "HFPA by Reason";
                            else
                                chartTitle.Text = "Plant " + sYM + " HFPA by Reason";
                        else
                            chartTitle.Text = "HFPA by Reason";

                        chartControl3.DataSource = dtChart2;
                        chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                        chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        this.chartControl3.Titles.Add(chartTitle);
                        //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
                    }
                    else
                    {
                        chartControl3.DataSource = null;
                    }
                }
                else if (hit.ChartTitle != null)
                {
                    DataSet dsData = await sbGetHFPA(sDate, "TOT");

                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    dtWeek = dsData.Tables[3];
                    if (sDate == "WEEK")
                        lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

                    //SetChart(dtChart);
                    //SetChart1(dtChart1);
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chartControl3.Titles.Clear();
                        chartTitle.Text = "HFPA by Reason";
                        chartControl3.DataSource = dtChart2;
                        chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                        chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        this.chartControl3.Titles.Add(chartTitle);
                        //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
                    }
                }
                else
                {
                    if (hit.AxisLabelItem == null) return;
                    string sYM = hit.AxisLabelItem.AxisValue.ToString();

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sYM)
                        {
                            _Line = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                        }
                    }

                    DataSet dsData = await sbGetHFPA(sDate, _Line);
                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    dtWeek = dsData.Tables[3];
                    if (sDate == "WEEK")
                        lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

                    //SetChart(dtChart);
                    //SetChart1(dtChart1);
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chartControl3.Titles.Clear();
                        if (sYM != null)
                            if (sYM == "iD")
                            {
                                chartTitle.Text = "Plant ID" + " HFPA by Reason";
                            }
                            else if (sYM == "Vj" || sYM == "VJ")
                                chartTitle.Text = "HFPA by Reason";
                            else
                            {
                                chartTitle.Text = "Plant " + sYM + " HFPA by Reason";
                            }

                        else
                            chartTitle.Text = "HFPA by Reason";
                        chartControl3.DataSource = dtChart2;
                        chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                        chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        this.chartControl3.Titles.Add(chartTitle);
                        //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
                    }
                    else
                    {
                        chartControl3.DataSource = null;
                    }
                }
                //splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                // splashScreenManager1.CloseWaitForm();
                MessageBox.Show(ex.Message);
            }
        }


        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================
        private void clear_chart()
        {
            chart1.Series[0].Points.Clear();
            //chart1.Series[1].Points.Clear();
            chartControl2.Series[0].Points.Clear();
        }
        private async void SetData()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                DataSet dsData = await sbGetHFPA(sDate, _Line);
                if (dsData == null) return;
                DataTable dtChart = dsData.Tables[0];
                _dtArea = dsData.Tables[0];
                DataTable dtChart1 = dsData.Tables[1];
                DataTable dtChart2 = dsData.Tables[2];
                dtWeek = dsData.Tables[3];
                if (sDate == "WEEK")
                    lblHeader.Text = string.Concat("HFPA By Week", dtWeek.Rows[0]["TXT"].ToString());

                SetChart(dtChart);
                SetChart1(dtChart1);
                //SetChart2(dtChart2);
                if (dtChart2 != null && dtChart2.Rows.Count > 0)
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                    //chartControl3.DataSource = dtChart2;
                    //chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    //chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                    //
                    chartControl3.Titles.Clear();
                    chartTitle.Text = "HFPA by Reason";
                    chartControl3.DataSource = dtChart2;
                    chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                    // Define the alignment of the titles.
                    chartTitle.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle.Antialiasing = true;
                    chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                    chartTitle.TextColor = Color.Blue;
                    chartTitle.Indent = 10;
                    this.chartControl3.Titles.Add(chartTitle);
                }
                else
                {
                    chartControl3.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }
        private void SetChart(DataTable argDtChart)
        {

            //DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chart1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["QTY"]));
                if (Convert.ToDouble(argDtChart.Rows[i]["QTY"]) > 90)
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(20,200,110);
                }
                else if (Convert.ToDouble(argDtChart.Rows[i]["QTY"]) < 80)
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(250,55,30);
                }
                else
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(255,180,15);
                }
            }
            chart1.RuntimeHitTesting = true;
        }

        private void SetChart1(DataTable argDtChart)
        {
            chartControl2.DataSource = null;
            chartControl2.Series[0].Points.Clear();
            chartControl2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;

            diagram.AxisX.Title.Text = "Plant";
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl2.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["DEFECT_RATE"]));
                if (i >= 5)
                {
                    chartControl2.Series[0].Points[i].Color = Color.FromArgb(250,55,30);
                }
                else
                {
                    chartControl2.Series[0].Points[i].Color = Color.FromArgb(20,200,110);
                }
            }
        }
        private void SetChart2(DataTable argDtChart)
        {
            chartControl3.Series[0].Points.Clear();
            chartControl3.Series[0].ArgumentScaleType = ScaleType.Qualitative;

            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl3.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["REWORK_NAME"].ToString(), argDtChart.Rows[i]["REW_QTY"]));
            }
        }
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        public async Task<DataSet> sbGetHFPA(string ARG_QTYPE, string ARG_LINE)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_HFPA";

                    MyOraDB.ReDim_Parameter(8);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                    MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                    MyOraDB.Parameter_Name[3] = "V_P_LINE_CD";
                    MyOraDB.Parameter_Name[4] = "OUT_CURSOR";
                    MyOraDB.Parameter_Name[5] = "OUT_CURSOR2";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR3";
                    MyOraDB.Parameter_Name[7] = "OUT_CURSOR4";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;


                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = cboDateFr.DateTime.ToString("yyyyMMdd");
                    MyOraDB.Parameter_Values[2] = cboDateTo.DateTime.ToString("yyyyMMdd");
                    MyOraDB.Parameter_Values[3] = ARG_LINE;
                    MyOraDB.Parameter_Values[4] = "";
                    MyOraDB.Parameter_Values[5] = "";
                    MyOraDB.Parameter_Values[6] = "";
                    MyOraDB.Parameter_Values[7] = "";

                    MyOraDB.Add_Select_Parameter(true);
                    ds_ret = MyOraDB.Exe_Select_Procedure();

                    if (ds_ret == null) return null;
                    return ds_ret;
                }
                catch
                {
                    return null;
                }
            });
        }

        #endregion ========= [Procedure Call] ===========================================

       
    }
}
