﻿using DevExpress.XtraCharts;
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
        private readonly string _strHeader = "HFPA by day";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sDate = "DAY";
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
                cboPlant.SelectedValue = ComVar.Var._strValue1;
                cboLine.SelectedValue = ComVar.Var._strValue2;
                //clear_chart();
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
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            //chartControl2.Series[0].Points.Clear();
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
                string process_name = "LMES.PKG_SMT_QUALITY_COCKPIT_06.SP_GET_BC_GRADE";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR2";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR3";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR4";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_DATE;
                MyOraDB.Parameter_Values[2] = "";
                MyOraDB.Parameter_Values[3] = "";
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";

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

            //DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            // XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            //if(sDate.ToString() == "MONTH")
            //{
            //    diagram.AxisX.Title.Text = "Month";
            //}
            //else if (sDate.ToString() == "YEAR")
            //{
            //    diagram.AxisX.Title.Text = "Year";
            //}
            //else
            //    diagram.AxisX.Title.Text = "Line";

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chart1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["QTY"]));
            }
            chart1.RuntimeHitTesting = true;
        }

        private void SetChart1(DataTable argDtChart)
        {
            //chartControl2.DataSource = null;
            //chartControl2.Series[0].Points.Clear();          
            //chartControl2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            //if (argDtChart == null) return;
            //XYDiagram diagram = (XYDiagram)chartControl2.Diagram;

            //    diagram.AxisX.Title.Text = "Line";
            //for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            //{
            //    chartControl2.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["DEFECT_RATE"]));
            //    if (i >= 5)
            //    {
            //        chartControl2.Series[0].Points[i].Color = Color.Red;
            //    }
            //    else
            //    {
            //        chartControl2.Series[0].Points[i].Color = Color.Green;
            //    }
            //}
        }
        private void SetChart2(DataTable argDtChart)
        {
            //chartControl3.Series[0].Points.Clear();
            //chartControl3.Series[0].ArgumentScaleType = ScaleType.Qualitative;

            //if (argDtChart == null) return;
            //for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            //{
            //    chartControl3.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["REWORK_NAME"].ToString(), argDtChart.Rows[i]["REW_QTY"]));
            //    //chartControl3.DataSource = argDtChart;
            //    //chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
            //    //chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });
            //}
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
                dtWeek   = dsData.Tables[3];
                if(sDate=="WEEK")
                lblHeader.Text = string.Concat("HFPA by week", dtWeek.Rows[0]["TXT"].ToString());

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
                    //chartControl3.Titles.Clear();
                    //chartTitle.Text = "HFPA by Reason";
                    //chartControl3.DataSource = dtChart2;
                    //chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    //chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                    // Define the alignment of the titles.
                    chartTitle.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle.Antialiasing = true;
                    chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                    chartTitle.TextColor = Color.Blue;
                    chartTitle.Indent = 10;
                   // this.chartControl3.Titles.Add(chartTitle);
                }
                else
                {
                   // chartControl3.DataSource = null;
                }

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
                ChartHitInfo hit = chart1.CalcHitInfo(e.Location);
                SeriesPoint point = hit.SeriesPoint;
                if (point != null)
                {
                    string sYM = point.Argument;

                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, sYM));
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
                       // chartControl3.Titles.Clear();
                        if (sYM != null)
                            chartTitle.Text = "HFPA by Reason Nos " + sYM;
                        else
                            chartTitle.Text = "HFPA by Reason";
                      //  chartControl3.DataSource = dtChart2;
                       // chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                      //  chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                       // this.chartControl3.Titles.Add(chartTitle);
                        //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
                    }
                    else
                    {
                        //chartControl3.DataSource = null;
                    }
                }
                else if (hit.ChartTitle != null)
                {
                    DataSet dsData = await Task.Run(() => sbGetBC_Grade(sDate, "ALL"));

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
                        //chartControl3.Titles.Clear();
                        chartTitle.Text = "HFPA by Reason";
                        //chartControl3.DataSource = dtChart2;
                       // chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                       // chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                        // Define the alignment of the titles.
                        chartTitle.Alignment = StringAlignment.Center;

                        // Place the titles where it's required.
                        chartTitle.Dock = ChartTitleDockStyle.Top;

                        // Customize a title's appearance.
                        chartTitle.Antialiasing = true;
                        chartTitle.Font = new Font("Calibri", 22F, FontStyle.Bold);
                        chartTitle.TextColor = Color.Blue;
                        chartTitle.Indent = 10;
                        //this.chartControl3.Titles.Add(chartTitle);
                        //chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle});
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