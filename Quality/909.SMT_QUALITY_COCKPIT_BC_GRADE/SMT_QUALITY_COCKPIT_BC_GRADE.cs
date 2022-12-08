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
        public SMT_QUALITY_COCKPIT_BC_GRADE()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "Monthly B&&C Grade";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sDate = "Q";
        DataTable dtWeek = null;

        #region Load-Visible Change-Timer
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {         
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //cboPlant.SelectedValue = ComVar.Var._strValue1;
                //cboLine.SelectedValue = ComVar.Var._strValue2;
                 _time = 28;
               

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Dispose();
            }

        }
        private void clear_chart()
        {
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart3.Series[0].Points.Clear();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if (_time >= 30)
            {              
                SetData();
                _time = 0;               

            }
        }

        #endregion

        #region Combo     

      
        #endregion


        #region

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region Database
       

        public DataSet sbGetBC_Grade(string ARG_QTYPE, string ARG_DATE)
        {

            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            DataSet ds_ret;
            try
            {
                string process_name = "LMES.PKG_SMT_QUALITY_COCKPIT_06.SP_GET_BC_GRADE_V2";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR1";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR2";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_DATE;
                MyOraDB.Parameter_Values[2] = "";
                MyOraDB.Parameter_Values[3] = "";
                MyOraDB.Parameter_Values[4] = "";

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






        #endregion DB
        private void SetChart(DataTable argDtChart)
        {
            chart1.DataSource = argDtChart;
            chart1.Series[0].ArgumentDataMember = "YMD";
            chart1.Series[0].ValueDataMembers.AddRange(new string[] { "BC_PPM" });
            chart1.Series[1].ArgumentDataMember = "YMD";
            chart1.Series[1].ValueDataMembers.AddRange(new string[] { "B_GRADE" });
            chart1.Series[2].ArgumentDataMember = "YMD";
            chart1.Series[2].ValueDataMembers.AddRange(new string[] { "C_GRADE" });
            ////DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);
            //chart1.DataSource = null;
            //chart1.Series[0].Points.Clear();
            //chart1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            //chart1.Series[1].Points.Clear();
            //chart1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            //chart1.Series[2].Points.Clear();
            //chart1.Series[2].ArgumentScaleType = ScaleType.Qualitative;
            //if (argDtChart == null) return;
            ////chart1.DataSource = argDtChart;

            //for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            //{
            //    //chart1.Series[0].ArgumentDataMember = "YMD";
            //    //chart1.Series[0].ValueDataMembers.AddRange(new string[] { "BC_PPM" });
            //    //chart1.Series[1].ArgumentDataMember = "YMD";
            //    //chart1.Series[1].ValueDataMembers.AddRange(new string[] { "B_GRADE" });
            //    //chart1.Series[2].ArgumentDataMember = "YMD";
            //    //chart1.Series[2].ValueDataMembers.AddRange(new string[] { "C_GRADE" });
            //    chart1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["BC_PPM"]));
            //    chart1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["B_GRADE"]));
            //    chart1.Series[2].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["C_GRADE"]));
            //}

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

        private async void SetData()
        {
            try
            {               

                DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate,"ALL"));
                if (dsData == null) return;               
                DataTable dtChart  = dsData.Tables[0];
                DataTable dtChart1 = dsData.Tables[1];
                DataTable dtChart2 = dsData.Tables[2];
                //dtWeek   = dsData.Tables[3];
                if (dtChart.Select("YMD = 'Total'").Count() > 0)
                {
                    DataTable _dtChart = dtChart.Select("YMD = 'Total'").CopyToDataTable();
                    lblTotalB.Text   = _dtChart.Rows[0]["B_GRADE"].ToString() +" (Prs)";
                    lblTotalC.Text   = _dtChart.Rows[0]["C_GRADE"].ToString() + " (Prs)";
                    lblTotalBC.Text = (double.Parse(_dtChart.Rows[0]["B_GRADE"].ToString())+ double.Parse(_dtChart.Rows[0]["C_GRADE"].ToString())) + " (Prs)";
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
                chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
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
               
            }
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            //btnDay.Enabled = false;
            //btnWeek.Enabled = true;
            //btnMonth.Enabled = false;
            //btnYear.Enabled = false;
            //sDate = "DAY";
            //lblHeader.Text = "HFPA by day";
            ////clear_chart();
            //SetData();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            //btnDay.Enabled = true;
            //btnWeek.Enabled = false;
            //btnMonth.Enabled = false;
            //btnYear.Enabled = false;
            //sDate = "WEEK";
            ////lblHeader.Text = "       HFPA by week";
            ////clear_chart();
            //SetData();
            
        }

        private async void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ChartHitInfo hit = chart1.CalcHitInfo(e.Location);
                SeriesPoint point = hit.SeriesPoint;
                if (point != null)
                {
                    string sYM = point.DateTimeArgument.ToString();
                    string sYM1 = point.Argument;
                    clear_chart();

                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, sYM));
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
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart2.Titles.AddRange(new ChartTitle[] { chartTitle});
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
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
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
                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, "ALL"));

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
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart2.Titles.AddRange(new ChartTitle[] { chartTitle});
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
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        chart3.Titles.AddRange(new ChartTitle[] { chartTitle });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
