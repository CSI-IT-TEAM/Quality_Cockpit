using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace FORM
{
    public partial class FORM_SMT_WEATHER_MGNT_STATICS : Form
    {
        public FORM_SMT_WEATHER_MGNT_STATICS()
        {
            InitializeComponent();
            dtp_Ym.Enabled = true;
        }
        int _time = 0;
        /*Binding Data*/
        private void BindingData(string argType, ChartControl chart)
        {
            try
            {
                /*Đổ dữ liệu cho Flow bên dưới*/
                DataTable dt1 = null;
                dt1 = SMT_MAT_WEATHER_MGNT_STATICS(argType, "ALL", ComVar.Var._strValue2, ComVar.Var._strValue4, ComVar.Var._strValue3);
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    BindingDataChart(dt1, chart, argType);
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
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
                        _chart.Series[0].Points.Add(new SeriesPoint(dtChart.Rows[i]["CAL_MONTH_NM"].ToString(), dtChart.Rows[i]["TMP_VL"]));
                        _chart.Series[1].Points.Add(new SeriesPoint(dtChart.Rows[i]["CAL_MONTH_NM"].ToString(), dtChart.Rows[i]["HUMI_VL"]));
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
                            _chart.Series[1].View.Color = Color.Red;
                            _chart.Series[1].Points[i].Color = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void cmdBack_Click(object sender, EventArgs e)
        {
            try
            {
                ComVar.Var.callForm = "back";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void tmrTick_Tick(object sender, EventArgs e)
        {
            try
            {
                _time++;
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss")); //Gán dữ liệu giờ cho label ngày giờ
                if (_time >= 59)
                {
                    tmrTick.Stop();
                    dtp_Ym.EditValue = DateTime.Now.ToString("yyyy-MM");
                    btnMonth.Enabled = true;
                    btnYear.Enabled = false;
                    lblMonth.Visible = false;
                    dtp_Ym.Visible = false;
                    lblTitle.Text = "Temperature And Humidity Trends by Year";
                    navigationFrame1.SelectedPage = navigationPage1;
                    ((XYDiagram)chtYear.Diagram).AxisX.Title.Text = "Month";
                    BindingData("C_YYYY", chtYear);
                    _time = 0;
                    tmrTick.Start();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void FORM_SMT_WEATHER_MGNT_STATICS_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible)
                {
                    _time = 60;
                    tmrTick.Start();
                }
                else
                    tmrTick.Stop();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                tmrTick.Stop();
            }
        }
        /*Database*/
        private DataTable SMT_MAT_WEATHER_MGNT_STATICS(string ARG_TYPE, string ARG_PLANT = "", string ARG_LINE = "", string ARG_DATE = "", string ARG_SEQ = "")
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            DataSet ds_ret;
            try
            {
                string process_name = "LMES.PKG_MAT_INV_TREND_MONITOR.SMT_MAT_WEATHER_MGNT_V2";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_PLANT";
                MyOraDB.Parameter_Name[2] = "V_P_LINE";
                MyOraDB.Parameter_Name[3] = "V_P_DATE";
                MyOraDB.Parameter_Name[4] = "V_P_SEQ";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR1";


                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_TYPE;
                MyOraDB.Parameter_Values[1] = ARG_PLANT;
                MyOraDB.Parameter_Values[2] = ARG_LINE;
                MyOraDB.Parameter_Values[3] = ARG_DATE.Substring(0, 6);
                MyOraDB.Parameter_Values[4] = ARG_SEQ;
                MyOraDB.Parameter_Values[5] = "";


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

        private void btnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                btnMonth.Enabled = false;
                btnYear.Enabled = true;
                lblMonth.Visible = true;
                dtp_Ym.Visible = true;
                lblTitle.Text = "Temperature And Humidity Trends by Month";
                navigationFrame1.SelectedPage = navigationPage2;
                ((XYDiagram)chtMonth.Diagram).AxisX.Title.Text = "Days";
                BindingData("C_GETDATE", chtMonth);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            try
            {
                btnMonth.Enabled = true;
                btnYear.Enabled = false;
                lblMonth.Visible = false;
                dtp_Ym.Visible = false;
                lblTitle.Text = "Temperature And Humidity Trends by Year";
                navigationFrame1.SelectedPage = navigationPage1;
                ((XYDiagram)chtYear.Diagram).AxisX.Title.Text = "Month";
                BindingData("C_YYYY", chtYear);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void FORM_SMT_WEATHER_MGNT_STATICS_Load(object sender, EventArgs e)
        {
            dtp_Ym.EditValue = DateTime.Now.ToString("yyyy-MM");
            chtMonth.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            chtMonth.RuntimeHitTesting = true;
        }

        private void dtp_Ym_EditValueChanged(object sender, EventArgs e)
        {
            ((XYDiagram)chtMonth.Diagram).AxisX.Title.Text = "Days";
            BindingData("C_GETDATE", chtMonth);
        }
        private void chtMonth_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chtMonth.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    DateTime myDate = DateTime.ParseExact(point.Argument, "yyyy-MMM-dd", CultureInfo.InvariantCulture);
                    ComVar.Var._strValue3 = myDate.ToString("yyyyMMdd");
                    ComVar.Var._strValue4 = point.Argument;
                }
                using (FORM_SMT_WEATHER_MGNT_STATICS_POPUP popup = new FORM_SMT_WEATHER_MGNT_STATICS_POPUP())
                {
                    popup.strDays  = ComVar.Var._strValue3;
                    popup.strTitle = ComVar.Var._strValue4;
                    popup.ShowDialog();
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
