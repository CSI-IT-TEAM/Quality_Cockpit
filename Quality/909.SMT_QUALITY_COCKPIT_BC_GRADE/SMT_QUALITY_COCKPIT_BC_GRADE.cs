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
    public partial class SMT_QUALITY_COCKPIT_BC_GRADE : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  B&&C Grade By Day";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sDate = "Q";
        DataTable dtWeek = null;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_BC_GRADE()
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

            DateTime dt = DateTime.Now;
            DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            cboDateFr.EditValue = fistdate;
            //cboDateFr.EditValue = DateTime.Now;
            cboDateTo.EditValue = DateTime.Now.AddDays(-1);
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                 _time = 28;
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

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }
        private void cboDateFr_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }

        private void cboDateTo_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }
        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private async void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //splashScreenManager1.ShowWaitForm();
                ChartHitInfo hit = chart1.CalcHitInfo(e.Location);
                SeriesPoint point = hit.SeriesPoint;
                if (point != null)
                {
                    string sYM = point.DateTimeArgument.ToString("yyyyMMdd");
                    string sYM1 = point.Argument;
                    clear_chart();

                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, sYM, cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd")));
                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    //dtWeek = dsData.Tables[3];
                    if (dtChart1 != null && dtChart1.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chart2.Titles.Clear();
                        if (sYM != null)
                            chartTitle.Text = "B/C Grade " + sYM1;
                        else
                            chartTitle.Text = "B/C Grade by Line";
                        SetChart1(dtChart1);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart2.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                    else
                    {
                        chart2.DataSource = null;
                    }
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chart3.Titles.Clear();
                        if (sYM != null)
                            chartTitle.Text = "Top 10 by Reason " + sYM1;
                        else
                            chartTitle.Text = "Top 10 by Reason";
                        SetChart2(dtChart2);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Right;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart3.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                    else
                    {
                        chart3.DataSource = null;
                    }
                }
                else if (hit.ChartTitle != null)
                {
                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, "ALL", cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd")));

                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    //dtWeek = dsData.Tables[3];
                    if (dtChart1 != null && dtChart1.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chart2.Titles.Clear();
                        chartTitle.Text = "B/C Grade by Line";
                        SetChart1(dtChart1);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart2.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        chart3.Titles.Clear();
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();

                        chartTitle.Text = "Top 10 by Reason";
                        SetChart2(dtChart2);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Right;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart3.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                }
                else
                {
                    string sYM = DateTime.Parse(hit.AxisLabelItem.AxisValue.ToString()).ToString();
                    string sYM1 = hit.AxisLabelItem.AxisValue.ToString();
                    clear_chart();

                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, sYM, cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd")));
                    if (dsData == null) return;
                    DataTable dtChart = dsData.Tables[0];
                    DataTable dtChart1 = dsData.Tables[1];
                    DataTable dtChart2 = dsData.Tables[2];
                    //dtWeek = dsData.Tables[3];
                    if (dtChart1 != null && dtChart1.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chart2.Titles.Clear();
                        if (sYM != null)
                            chartTitle.Text = "B/C Grade " + sYM1;
                        else
                            chartTitle.Text = "B/C Grade by Line";
                        SetChart1(dtChart1);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart2.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                    else
                    {
                        chart2.DataSource = null;
                    }
                    if (dtChart2 != null && dtChart2.Rows.Count > 0)
                    {
                        DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                        chart3.Titles.Clear();
                        if (sYM != null)
                            chartTitle.Text = "Top 10 by Reason " + sYM1;
                        else
                            chartTitle.Text = "Top 10 by Reason";
                        SetChart2(dtChart2);

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Right;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart3.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                    else
                    {
                        chart3.DataSource = null;
                    }
                }
                //splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //splashScreenManager1.CloseWaitForm();
            }
        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================

        private async void SetData()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, "ALL", cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd")));
                if (dsData == null) return;
                DataTable dtChart = dsData.Tables[0];
                DataTable dtChart1 = dsData.Tables[1];
                DataTable dtChart2 = dsData.Tables[2];
                if (dtChart.Select("YMD = 'Total'").Count() > 0)
                {
                    DataTable _dtChart = dtChart.Select("YMD = 'Total'").CopyToDataTable();
                    lblTotalB.Text = _dtChart.Rows[0]["B_GRADE"].ToString() + " (Pairs)";
                    lblTotalC.Text = _dtChart.Rows[0]["C_GRADE"].ToString() + " (Pairs)";
                    lblTotalBC.Text = (double.Parse(_dtChart.Rows[0]["B_GRADE"].ToString()) + double.Parse(_dtChart.Rows[0]["C_GRADE"].ToString())) + " (Pairs)";
                    lblTotalPPM.Text = _dtChart.Rows[0]["BC_PPM"].ToString() + " (PPM)";
                }
                dtChart.Rows.RemoveAt(dtChart.Rows.Count - 1);
                SetChart(dtChart);
                SetChart1(dtChart1);
                SetChart2(dtChart2);

                DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                chart3.Titles.Clear();
                chartTitle.Text = "Top 10 by Reason";
                // Define the alignment of the titles.
                chartTitle.Alignment = StringAlignment.Center;

                // Place the titles where it's required.
                chartTitle.Dock = ChartTitleDockStyle.Right;

                // Customize a title's appearance.
                chartTitle.Antialiasing = true;
                chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                chartTitle.TextColor = Color.Blue;
                chartTitle.Indent = 10;
                chart3.Titles.AddRange(new ChartTitle[] { chartTitle });

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
        private void clear_chart()
        {
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart3.Series[0].Points.Clear();
        }
        private void SetChart(DataTable argDtChart)
        {
            chart1.DataSource = argDtChart;
            chart1.Series[0].ArgumentDataMember = "YMD";
            chart1.Series[0].ValueDataMembers.AddRange(new string[] { "BC_PPM" });
            chart1.Series[1].ArgumentDataMember = "YMD";
            chart1.Series[1].ValueDataMembers.AddRange(new string[] { "B_GRADE" });
            chart1.Series[2].ArgumentDataMember = "YMD";
            chart1.Series[2].ValueDataMembers.AddRange(new string[] { "C_GRADE" });

            ((DevExpress.XtraCharts.XYDiagram)chart1.Diagram).AxisX.QualitativeScaleOptions.AutoGrid = false;


            chart1.RuntimeHitTesting = true;
        }

        private void SetChart1(DataTable argDtChart)
        {
            chart2.DataSource = null;
            chart2.Series[0].Points.Clear();
            chart2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chart2.Series[1].Points.Clear();
            chart2.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            chart2.Series[2].Points.Clear();
            chart2.Series[2].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            chart2.Titles.Clear();

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chart2.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NAME"].ToString(), argDtChart.Rows[i]["BC_PPM"]));
                chart2.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NAME"].ToString(), argDtChart.Rows[i]["B_GRADE"]));
                chart2.Series[2].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NAME"].ToString(), argDtChart.Rows[i]["C_GRADE"]));
            }
        }
        private void SetChart2(DataTable argDtChart)
        {
            chart3.DataSource = null;
            chart3.Series[0].Points.Clear();
            chart3.Series[0].ArgumentScaleType = ScaleType.Qualitative;

            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chart3.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["ERR_NM"].ToString(), argDtChart.Rows[i]["QTY"]));

            }
        }
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        public DataSet sbGetBC_Grade(string V_P_TYPE, string V_P_DATE, string V_P_DATE_FR, string V_P_DATE_TO)
        {

            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_BC_GRADE";

                MyOraDB.ReDim_Parameter(7);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE";
                MyOraDB.Parameter_Name[2] = "V_P_DATE_FR";
                MyOraDB.Parameter_Name[3] = "V_P_DATE_TO";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR1";
                MyOraDB.Parameter_Name[6] = "OUT_CURSOR2";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = V_P_TYPE;
                MyOraDB.Parameter_Values[1] = V_P_DATE;
                MyOraDB.Parameter_Values[2] = V_P_DATE_FR;
                MyOraDB.Parameter_Values[3] = V_P_DATE_TO;
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";
                MyOraDB.Parameter_Values[6] = "";

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret;
            }
            catch
            {
                return null;
            }
        }
        #endregion ========= [Procedure Call] ===========================================

    }
}
