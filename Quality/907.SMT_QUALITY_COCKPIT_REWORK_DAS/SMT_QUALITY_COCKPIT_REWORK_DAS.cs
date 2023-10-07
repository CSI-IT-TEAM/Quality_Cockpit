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
        string sType = "DAY";
        string sLine = "ALL", sLine_nm = "ALL", sPlant = "ALL", sDateF = "", sDateT = "";
        DataTable _dtArea = null;

        #region Load-Visible Change-Timer
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {         
            btnDay.Enabled = false;           
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;

            dtpYMDT.EditValue = DateTime.Now;
            DateTime dt = DateTime.Now;
            DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            dtpYMD.EditValue = fistdate;
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
       

        public async Task<DataSet> sbGetRework(string ARG_QTYPE,  string ARG_DATEF, string ARG_DATET,string ARG_PLANT, string ARG_LINE)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_REWORK_DAS_V3";

                    MyOraDB.ReDim_Parameter(8);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATEF";
                    MyOraDB.Parameter_Name[2] = "V_P_DATET";
                    MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[4] = "V_P_LINE";
                    MyOraDB.Parameter_Name[5] = "OUT_CURSOR";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR2";
                    MyOraDB.Parameter_Name[7] = "OUT_CURSOR3";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;
                   

                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = ARG_DATEF;
                    MyOraDB.Parameter_Values[2] = ARG_DATET;
                    MyOraDB.Parameter_Values[3] = ARG_PLANT;
                    MyOraDB.Parameter_Values[4] = ARG_LINE;                    
                    MyOraDB.Parameter_Values[5] = "";
                    MyOraDB.Parameter_Values[6] = "";
                    MyOraDB.Parameter_Values[7] = "";
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
           
            if(sType.ToString() == "MONTH")
            {
                diagram.AxisX.Title.Text = "Month";
            }
            else if (sType.ToString() == "YEAR")
            {
                diagram.AxisX.Title.Text = "Year";
            }
            else
                diagram.AxisX.Title.Text = "Plant";

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["REW_QTY"]));
                chartControl1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate >= 5)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate >= 4 && rate < 5)
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

            //if (sType.ToString() == "MONTH")
            //{
            //    diagram.AxisX.Title.Text = "Month";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else if (sType.ToString() == "YEAR")
            //{
            //    diagram.AxisX.Title.Text = "Year";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else
                diagram.AxisX.Title.Text = "Plant";
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
                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");

                DataSet dsData = await sbGetRework(sType,sDateF, sDateT, sPlant, sLine);
                if (dsData == null) return;               
                DataTable dtChart = dsData.Tables[0];

                if (dtChart.Select("LINE_CD <> 'TOTAL'", "LINE_CD").Count() > 0)
                {
                    DataTable _dtChart = dtChart.Select("LINE_CD <> 'TOTAL'", "LINE_CD").CopyToDataTable();

                    _dtArea = _dtChart;
                    SetChart(_dtChart);
                }
                if (dtChart.Select("LINE_CD = 'TOTAL'", "LINE_CD").Count() > 0)
                {
                    DataTable _dtLabel = dtChart.Select("LINE_CD = 'TOTAL'", "LINE_CD").CopyToDataTable();
                    lblTotalRework.Text = Convert.ToDouble(_dtLabel.Rows[0]["REW_QTY"].ToString()).ToString("###,##0.##") + " Pairs";
                    lblTotalProd.Text = Convert.ToDouble(_dtLabel.Rows[0]["PROD_QY"].ToString()).ToString("###,##0.##") + " Pairs";
                    lblTotalRate.Text = _dtLabel.Rows[0]["RATE"].ToString() + " %";




                }

                SetDataDetail();
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
            sType = "DAY";
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
            sType = "WEEK";
            lblHeader.Text = "       Rework by week";
            clear_chart();
            SetData();
        }

        private void dtpYMD_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            SetData();
        }

        private void dtpYMDT_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            SetData();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = true;
            sType = "MONTH";
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
            sType = "YEAR";
            lblHeader.Text = "       Rework by year";
            clear_chart();
            SetData();
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chartControl1.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    sLine_nm = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sLine_nm)
                        {
                            sLine = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                        }
                    }
                }
                else
                {
                    if(hit.AxisLabelItem == null)
                    {
                        sLine = "ALL";
                    }   
                    else
                    {
                        sLine_nm = hit.AxisLabelItem.AxisValue.ToString();
                        for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                        {
                            if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sLine_nm)
                            {
                                sLine = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                            }
                        }
                    }    
                    
                }
                //else
                //    sLine = "ALL";
                _time = 10;
                SetDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void SetDataDetail()
        {
            try
            {

                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");
                DataSet _dtSet = await sbGetRework(sType, sDateF, sDateT, sPlant, sLine);

                DataTable dtChart1 = _dtSet.Tables[1];
                DataTable dtChart2 = _dtSet.Tables[2];

                SetChart1(dtChart1);
                if (dtChart2 != null && dtChart2.Rows.Count > 0)
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
                    chartControl3.Titles.Clear();
                    if (sLine == "ALL")
                    {
                        chartTitle2.Text = "Rework By Reason";
                    }
                    else
                    {
                        if(int.Parse(sLine)<6)
                        {
                            chartTitle2.Text = "Plant " + sLine_nm + " By Reason";
                        } 
                        else
                            chartTitle2.Text = "Plant " + sLine_nm + " By Reason";
                    }
                    // Define the alignment of the titles.
                    chartTitle2.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle2.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle2.Antialiasing = true;
                    chartTitle2.Font = new Font("Calibri", 22F, FontStyle.Bold);
                    chartTitle2.TextColor = Color.Blue;
                    chartTitle2.Indent = 10;
                    chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle2 });

                    if (dtChart2 == null) return;
                    chartControl3.DataSource = dtChart2;
                    chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                }
                else
                {
                    chartControl3.DataSource = null;
                }

                dtChart1 = null;
                dtChart2 = null;

               
            }
            catch (Exception ex)
            {
               
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }
    }
}
