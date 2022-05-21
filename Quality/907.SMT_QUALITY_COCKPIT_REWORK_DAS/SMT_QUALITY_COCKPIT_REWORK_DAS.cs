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
    public partial class SMT_QUALITY_COCKPIT_REWORK_DAS : Form
    {
        public SMT_QUALITY_COCKPIT_REWORK_DAS()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Rework by day";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sDate = "DAY";

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
                clear_chart();
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
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl2.Series[0].Points.Clear();
           // chartControl3.Series[1].Points.Clear();
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
       

        public async Task<DataSet> sbGetRework(string ARG_QTYPE)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_REWORK_DAS";

                    MyOraDB.ReDim_Parameter(4);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "OUT_CURSOR";
                    MyOraDB.Parameter_Name[2] = "OUT_CURSOR2";
                    MyOraDB.Parameter_Name[3] = "OUT_CURSOR3";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                   

                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = "";
                    MyOraDB.Parameter_Values[2] = "";
                    MyOraDB.Parameter_Values[3] = "";
                   ;

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



       

       
        #endregion DB
        private void SetChart(DataTable argDtChart)
        {

            //DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);
            chartControl1.DataSource = null;
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
           
            if(sDate.ToString() == "MONTH")
            {
                diagram.AxisX.Title.Text = "Month";
            }
            else if (sDate.ToString() == "YEAR")
            {
                diagram.AxisX.Title.Text = "Year";
            }
            else
                diagram.AxisX.Title.Text = "Line";

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["REW_QTY"]));
                chartControl1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate > 4)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate > 3)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Yellow;
                }
                else
                {
                    chartControl1.Series[0].Points[i].Color = Color.Green;
                }
            }
        }

        private void SetChart1(DataTable argDtChart)
        {
            chartControl2.DataSource = null;
            chartControl2.Series[0].Points.Clear();          
            chartControl2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl2.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;

            //if (sDate.ToString() == "MONTH")
            //{
            //    diagram.AxisX.Title.Text = "Month";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else if (sDate.ToString() == "YEAR")
            //{
            //    diagram.AxisX.Title.Text = "Year";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else
                diagram.AxisX.Title.Text = "Line";
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl2.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["RATE"]));
              
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
                //chartControl3.DataSource = argDtChart;
                //chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                //chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

            }
        }

        private async void SetData()
        {
            try
            {               

                DataSet dsData = await sbGetRework(sDate);
                if (dsData == null) return;               
                DataTable dtChart = dsData.Tables[0];
                DataTable dtChart1 = dsData.Tables[1];
                DataTable dtChart2 = dsData.Tables[2];               

                SetChart(dtChart);
                SetChart1(dtChart1);
                //SetChart2(dtChart2);
                if (dtChart2 != null && dtChart2.Rows.Count > 0)
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
                    chartControl3.DataSource = dtChart2;
                    chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

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
               
            }
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sDate = "DAY";
            lblHeader.Text = "       Rework by day";
            clear_chart();
            SetData();
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = false;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sDate = "WEEK";
            lblHeader.Text = "       Rework by week";
            clear_chart();
            SetData();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = true;
            sDate = "MONTH";
            lblHeader.Text = "       Rework by month";
            clear_chart();
            SetData();
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = true;
            btnMonth.Enabled = true;
            btnYear.Enabled = false;
            sDate = "YEAR";
            lblHeader.Text = "       Rework by year";
            clear_chart();
            SetData();
        }
    }
}
