using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FORM
{
    public partial class FORM_SMT_WEATHER_MGNT_STATICS_POPUP : Form
    {
        public FORM_SMT_WEATHER_MGNT_STATICS_POPUP()
        {
            InitializeComponent();
        }
        public string strDays = "", strTitle = "";
        private int cCount = 0;
        /*Binding Data*/
        private void BindingData(string argType, ChartControl chart)
        {
            try
            {
                /*Đổ dữ liệu cho Flow bên dưới*/
                DataTable dt1 = null;
                lblDays.Text = "";
                dt1 = SMT_MAT_WEATHER_MGNT_STATICS(argType, "ALL", ComVar.Var._strValue2, strDays);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    lblDays.Text = strTitle;
                    BindingDataChart(dt1, chart, argType);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                ComVar.Var.writeToLog(this.Name + "/Load_Form :    " + ex.ToString());
                Debug.WriteLine(ex.Message);
            }
        }
        private void BindingDataChart(DataTable dtChart, ChartControl _chart, string arg_Type = "")
        {
            try
            {
                _chart.DataSource = null;
                if (_chart.Series[0].Points.Count > 0 && _chart.Series[1].Points.Count > 0)
                {
                    _chart.Series[0].Points.Clear();
                    _chart.Series[1].Points.Clear();
                }
                if (dtChart != null && dtChart.Rows.Count > 0)
                {
                    _chart.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                    _chart.Series[1].ArgumentScaleType = ScaleType.Qualitative;

                    for (int i = 0; i < dtChart.Rows.Count; i++)
                    {
                        _chart.Series[0].Points.Add(new SeriesPoint(dtChart.Rows[i]["CURR_HH"].ToString(), dtChart.Rows[i]["TMP_VL"]));
                        _chart.Series[1].Points.Add(new SeriesPoint(dtChart.Rows[i]["CURR_HH"].ToString(), dtChart.Rows[i]["HUMI_VL"]));
                        double rate;
                        double.TryParse(dtChart.Rows[i]["HUMI_VL"].ToString(), out rate);

                        _chart.Series[0].Points[i].Color = Color.FromArgb(255, 192, 0);

                        if (rate <= 70)
                        {
                            _chart.Series[1].View.Color = Color.Lime;
                            _chart.Series[1].Points[i].Color = Color.Lime;
                        }
                        else
                        {
                            _chart.Series[1].View.Color = Color.FromArgb(250,55,30);
                            _chart.Series[1].Points[i].Color = Color.FromArgb(250,55,30);
                        }
                    }
                }
                tmrTick.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Database*/
        private DataTable SMT_MAT_WEATHER_MGNT_STATICS(string ARG_TYPE, string ARG_PLANT = "", string ARG_LINE = "", string ARG_DATE = "")
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            DataSet ds_ret;
            try
            {
                string process_name = "LMES.PKG_MAT_INV_TREND_MONITOR.SMT_MAT_WEATHER_MGNT";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_PLANT";
                MyOraDB.Parameter_Name[2] = "V_P_LINE";
                MyOraDB.Parameter_Name[3] = "V_P_DATE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR1";


                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_TYPE;
                MyOraDB.Parameter_Values[1] = ARG_PLANT;
                MyOraDB.Parameter_Values[2] = ARG_LINE;
                MyOraDB.Parameter_Values[3] = ARG_DATE;
                MyOraDB.Parameter_Values[4] = "";


                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[process_name];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            try
            {
                cCount++;
                if (cCount >= 10)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void FORM_SMT_WEATHER_MGNT_STATICS_Load(object sender, EventArgs e)
        {
            ((XYDiagram)chtDays.Diagram).AxisX.Title.Text = "Time";
            BindingData("C_POP", chtDays);
        }
    }
}
