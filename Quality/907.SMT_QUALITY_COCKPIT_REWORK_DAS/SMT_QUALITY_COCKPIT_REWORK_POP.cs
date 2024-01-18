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
    public partial class SMT_QUALITY_COCKPIT_REWORK_POP : Form
    {
        public SMT_QUALITY_COCKPIT_REWORK_POP()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        string _date,_dateto, _plant_code, _line_code;
        public SMT_QUALITY_COCKPIT_REWORK_POP(string date,string dateto, string plant, string line)
        {
            InitializeComponent();
            _date = date;
            _plant_code = plant;
            _line_code = line;
            _dateto = dateto;
        }
        private readonly string _strHeader = "       Daily Rework";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");

        #region Load-Visible Change-Timer
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
           
        }

        private void SMT_QUALITY_COCKPIT_REWORK_POP_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                
               // chartControl1.Series[0].Points.Clear();
                SetData();
                //chartControl1.Series[1].Points.Clear();

                _time = 0;
             

                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            _time++;
            if (_time >= 30)
            {
                
                _time = 0;
                SetData();

            }
        }

        #endregion

        #region Combo
       

       
        #endregion


        #region

        private void cmdBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region Database
      
       



        #region DB
        private DataSet Data_Select(string ARG_QTYPE, string ARG_DATEF, string ARG_DATET, string ARG_PLANT, string ARG_LINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(8);
            MyOraDB.Process_Name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_REWORK";//

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
            MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
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


            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null)
                return null;
            return retDS;
        }

        #endregion DB
        public async Task<DataTable> sbGetRework_Chart(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_GET_REWORK_CHART";

                    MyOraDB.ReDim_Parameter(6);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATEF";
                    MyOraDB.Parameter_Name[2] = "V_P_DATET";
                    MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[4] = "V_P_LINE";
                    MyOraDB.Parameter_Name[5] = "OUT_CURSOR";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = ARG_YMDF;
                    MyOraDB.Parameter_Values[2] = ARG_YMDT;
                    MyOraDB.Parameter_Values[3] = ARG_PLANT;
                    MyOraDB.Parameter_Values[4] = ARG_LINE;
                    MyOraDB.Parameter_Values[5] = "";

                    MyOraDB.Add_Select_Parameter(true);
                    ds_ret = MyOraDB.Exe_Select_Procedure();

                    if (ds_ret == null) return null;
                    return ds_ret.Tables[process_name];
                }
                catch
                {
                    return null;
                }
            });
        }
   

        #endregion DB


        

      
        //private void SetChart(DataTable argDtChart)
        //{
        //   // string YMDF, YMDT, PLANT_CD, LINE_CD;
      

        //    //DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);

        //    chartControl1.Series[0].Points.Clear();
        //    //chartControl1.Series[1].Points.Clear();
        //    chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
        //    //chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
        //    if (argDtChart == null) return;
        //    for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
        //    {
        //        chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["HH"].ToString(), argDtChart.Rows[i]["REWORK_QTY"]));
        //        //chartControl1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["RATE"]));

        //        //double rate;
        //        //double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

        //        //if (rate > 6)
        //        //{
        //        //    chartControl1.Series[0].Points[i].Color = Color.Red;
        //        //}
        //        //else if (rate > 3)
        //        //{
        //        //    chartControl1.Series[0].Points[i].Color = Color.Yellow;
        //        //}
        //        //else
        //        //{
        //        //    chartControl1.Series[0].Points[i].Color = Color.Green;
        //        //}
        //    }
        //}

        private void SetChart(DataTable dt)
        {
            try
            {
                chartControl1.DataSource = dt;
                chartControl1.SeriesDataMember = "MLINE_NM";
                chartControl1.SeriesTemplate.ArgumentDataMember = "COL_DAY";
                chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "RATE" });
                LineSeriesView lineseriesView = new LineSeriesView();
                DevExpress.XtraCharts.XYMarkerSlideAnimation xyMarkerSlideAnimation2 = new DevExpress.XtraCharts.XYMarkerSlideAnimation();
                DevExpress.XtraCharts.CircleEasingFunction easingfunc = new DevExpress.XtraCharts.CircleEasingFunction();
                xyMarkerSlideAnimation2.Direction = DevExpress.XtraCharts.XYMarkerSlideAnimationDirection.FromBottomCenter;
                xyMarkerSlideAnimation2.EasingFunction = easingfunc;
                lineseriesView.SeriesPointAnimation = xyMarkerSlideAnimation2;
                chartControl1.SeriesTemplate.CrosshairLabelPattern = "{S}:{V:#,0.##}";
                chartControl1.SeriesTemplate.View = lineseriesView;
                lineseriesView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                lineseriesView.SeriesPointAnimation.EasingFunction.EasingMode = EasingMode.InOut;
                lineseriesView.SeriesPointAnimation.Enabled = true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async void SetData()
        {
            try
            {
              
                string YMD, PLANT_CD, LINE_CD;
                int total = 0;
                double PER = 0;

                DataSet dsData = Data_Select("Q_POP", _date, _dateto,_plant_code, _line_code);

                if (dsData == null) return;
                DataTable dtChart = dsData.Tables[0];
                SetChart(dtChart);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                
            }
        }

       
      



        private void gvwDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.Caption == "Total")
            //{
            //    e.Appearance.ForeColor = Color.Blue;

            //}
        }

        private void pnC_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
