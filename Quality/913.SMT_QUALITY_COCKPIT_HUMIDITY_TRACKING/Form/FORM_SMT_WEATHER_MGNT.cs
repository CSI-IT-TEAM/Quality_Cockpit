using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FORM
{
    public partial class FORM_SMT_WEATHER_MGNT : Form
    {
        public FORM_SMT_WEATHER_MGNT()
        {
            InitializeComponent();
            //btnCheckLocate.Enabled = false;
        }
        /*Variables*/
        int _time = 0;
        bool _first_load = true;
        string strNavPageCurr = "PAGE1";
        string PLANT_CD = "ALL";
        string WH_CD = ComVar.Var._Area;
        string strDate = ComVar.Var._strValue1;
        string strCHK = ComVar.Var._strValue5;
        List<UC.UC_WEATHER> ucWeatherList = new List<UC.UC_WEATHER>();

        private void FORM_SMT_WEATHER_MGNT_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadCHKPoint();
                //_first_load = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Clear Data*/
        private void ClearData()
        {
            try
            {
                fplWeather.Visible = false;
                for (int i = fplWeather.Controls.Count - 1; i >= 0; --i)
                {
                    var ctl = fplWeather.Controls[i];
                    ctl.Dispose();
                }
                if (chtDaily.Series[0].Points.Count > 0)
                {
                    chtDaily.Series[0].Points.Clear();
                }
                if (chtDaily.Series[1].Points.Count > 0)
                {
                    chtDaily.Series[1].Points.Clear();
                }
                if (chartHourly.Series[0].Points.Count > 0)
                {
                    chartHourly.Series[0].Points.Clear();
                }
                if (chartHourly.Series[1].Points.Count > 0)
                {
                    chartHourly.Series[1].Points.Clear();
                }
                lblTemp.Text = "";
                lblHu_Qty.Text = "";
                GauHumi.Value = 0;
                GauTmp.Value = 0;
                lblTemp.Text = "";
                lblHu_Qty.Text = "";
                GauHumi.Value = 0;
                lblHu.Text = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void LoadCHKPoint()
        {
            try
            {
                DataTable dtCHK = null;
                dtCHK = SMT_MAT_WEATHER_MGNT("CBO_CHK", PLANT_CD, WH_CD);
                if (dtCHK != null && dtCHK.Rows.Count > 1)
                {
                    btnCheckLocate.Properties.DataSource = dtCHK;
                    btnCheckLocate.Properties.ValueMember = "CODE";
                    btnCheckLocate.Properties.DisplayMember = "NAME";
                    btnCheckLocate.ItemIndex = 0;
                    for (int iRow = 0; iRow < dtCHK.Rows.Count; iRow++)
                    {
                        if (dtCHK.Rows[iRow]["CODE"].ToString() == strCHK)
                        {
                            btnCheckLocate.EditValue = strCHK;
                            break;
                        }
                    }
                }
                _first_load = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*ADD UC*/
        private void AddUC(DataTable dt)
        {
            try
            {
                //string strShow = "";
                if (dt == null && dt.Rows.Count < 0) return;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UC.UC_WEATHER _UC_WEATHER = new UC.UC_WEATHER();
                    _UC_WEATHER.Tag = i;
                    _UC_WEATHER.BindingData(i, dt);
                    _UC_WEATHER.ucClickDate += ClickDate;
                    fplWeather.Controls.Add(_UC_WEATHER);
                    ucWeatherList.Add(_UC_WEATHER);
                    //if (strDate.Equals(dt.Rows[i]["CAL_DATE"].ToString()))
                    //{
                    //    fplWeather.Controls.SetChildIndex(_UC_WEATHER, i);
                    //    break;
                    //}
                }
                hScrollBar1.Minimum = fplWeather.HorizontalScroll.Minimum;
                hScrollBar1.Maximum = fplWeather.HorizontalScroll.Maximum;
                hScrollBar1.LargeChange = fplWeather.HorizontalScroll.LargeChange;
                hScrollBar1.RightToLeft = RightToLeft.Yes;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        void ClickDate(UserControl uc, Label label)
        {
            try
            {
                //Debug.WriteLine(label.Text.Replace("-", ""));
                foreach (UC.UC_WEATHER ucItem in ucWeatherList)
                {
                    if (ucItem.Tag != null)
                    {
                        ucItem.PaintBorder(ucItem.Tag.Equals(uc.Tag));
                        ucItem.Refresh();
                    }
                }
                GetDataDate(label.Text.Replace("-", ""));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        void GetDataDate(string ymd)
        {
            try
            {
                chtDaily.DataSource = null;
                chartHourly.DataSource = null;
                DataTable dtChart = null;
                DataTable dt1 = null, dtCurrHumi = null, dtCurr = null;
                dt1 = SMT_MAT_WEATHER_MGNT("Q_FLOW", PLANT_CD, WH_CD, ymd, btnCheckLocate.EditValue.ToString());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    dtChart = dt1.Copy();
                    dtChart = dtChart.Select("HH <> 'AVG'", "HH").CopyToDataTable();
                    BindingChart(chtDaily, dtChart);
                }
                /*Get current Humidity*/
                dtCurrHumi = SMT_MAT_WEATHER_MGNT("Q_FLOW", PLANT_CD, WH_CD, DateTime.Now.ToString("yyyyMMdd"), btnCheckLocate.EditValue.ToString());
                if (dtCurrHumi != null && dtCurrHumi.Rows.Count >= 0)
                {
                    BindingLabel(dtCurrHumi);
                }
                dtCurr = SMT_MAT_WEATHER_MGNT("Q_CURR", PLANT_CD, WH_CD, ymd, btnCheckLocate.EditValue.ToString());
                if (dtCurr != null && dtCurr.Rows.Count >= 0)
                {
                    BindingLabelTemp(ymd, dtCurr);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void BindingLabelTemp(string strDate, DataTable dt)
        {
            try
            {
                /*Binding Label Left*/
                GauTmp.Value = 0;
                lblTemp.Text = "";
                lblHu_Qty.Text = "";
                GauHumi.Value = 0;

                if (dt != null)
                {
                    lblTemp.Text = dt.Rows[0]["TMP_VL"].ToString();
                    GauTmp.Value = int.Parse(dt.Rows[0]["TMP_VL"].ToString());
                    lblHu_Qty.Text = dt.Rows[0]["HUMI_VL"].ToString();
                    GauHumi.Value = int.Parse(dt.Rows[0]["HUMI_VL"].ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Binding Data*/
        private void BindingData(string strPageCurr)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClearData();
                if (btnCheckLocate.EditValue == null) return;
                DataTable dt1 = null, dt2 = null;
                if (strPageCurr.ToUpper().Equals("PAGE1"))
                {
                    /*Đổ dữ liệu cho Flow bên dưới*/
                    //chtDaily.DataSource = null;
                    //chartHourly.DataSource = null;
                    //fplWeather.Visible = false;
                    //fplWeather.Controls.Clear();
                    dt1 = SMT_MAT_WEATHER_MGNT("Q1_1", PLANT_CD, WH_CD, DateTime.Now.ToString("yyyyMMdd"), btnCheckLocate.EditValue.ToString());
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        GetDataDate(dt1.Rows[dt1.Rows.Count - 1]["CAL_DATE"].ToString());
                        AddUC(dt1);
                        btnDaily.Enabled = false;
                        btnHourly.Enabled = true;
                    }
                    fplWeather.Visible = true;
                }
                else
                {
                    dt2 = SMT_MAT_WEATHER_MGNT("Q2", "ALL", WH_CD, "", btnCheckLocate.EditValue.ToString());
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        DataTable dttmp = dt2.Copy();
                        dttmp = dttmp.Select("HH <> 'AVG'", "HH").CopyToDataTable();
                        BindingChart(chartHourly, dttmp);
                        DataTable dtCurr = SMT_MAT_WEATHER_MGNT("Q_CURR", PLANT_CD, WH_CD, DateTime.Now.ToString("yyyyMMdd"), btnCheckLocate.EditValue.ToString());
                        if (dtCurr != null && dtCurr.Rows.Count > 0)
                        {
                            BindingLabelTemp(DateTime.Now.ToString("yyyyMMdd"), dtCurr);
                        }
                        btnDaily.Enabled = true;
                        btnHourly.Enabled = false;
                    }
                }
                _first_load = false;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Binding Label*/
        private void BindingLabel(DataTable dtLabel)
        {
            try
            {
                lblHu.Text = "";
                if (dtLabel != null && dtLabel.Rows.Count > 0)
                {
                    /*Current*/
                    DataTable dtAVG = dtLabel.Copy();
                    dtAVG = dtAVG.Select("HH = 'AVG'", "").CopyToDataTable();
                    lblHu.Text = dtAVG.Rows[0]["HUMI_VL"].ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Binding Chart*/
        private void BindingChart(ChartControl _chart, DataTable dtChart)
        {
            try
            {
                if (_chart.Series[0].Points.Count > 0)
                {
                    _chart.Series[0].Points.Clear();
                }
                if (_chart.Series[1].Points.Count > 0)
                {
                    _chart.Series[1].Points.Clear();
                }
                ((DevExpress.XtraCharts.XYDiagram)_chart.Diagram).AxisX.QualitativeScaleOptions.AutoGrid = false;
                if (dtChart != null && dtChart.Rows.Count > 0)
                {
                    _chart.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                    _chart.Series[1].ArgumentScaleType = ScaleType.Qualitative;

                    for (int i = 0; i < dtChart.Rows.Count; i++)
                    {
                        _chart.Series[0].Points.Add(new SeriesPoint(dtChart.Rows[i]["HH"].ToString(), dtChart.Rows[i]["TMP_VL"]));
                        _chart.Series[1].Points.Add(new SeriesPoint(dtChart.Rows[i]["HH"].ToString(), dtChart.Rows[i]["HUMI_VL"]));
                        double rate;
                        double.TryParse(dtChart.Rows[i]["HUMI_VL"].ToString(), out rate);

                        _chart.Series[0].Points[i].Color = Color.Yellow;

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
                    double _drawColor;
                    double.TryParse(dtChart.Rows[dtChart.Rows.Count - 1]["HUMI_VL"].ToString(), out _drawColor);
                    if (_drawColor <= 70)
                    {
                        lblHu_Qty.ForeColor = Color.Lime;
                        lblHu_Qty.BackColor = Color.White;
                        lblpt.ForeColor = Color.Lime;
                        tmrHumindity.Stop();
                    }
                    else
                    {
                        tmrHumindity.Start();
                    }
                }
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
                    BindingData(strNavPageCurr);
                    _time = 0;
                    tmrTick.Start();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void tmrHumindity_Tick(object sender, EventArgs e)
        {
            try
            {
                if(lblHu_Qty.ForeColor == Color.Lime)
                {
                    lblHu_Qty.ForeColor = Color.Red;
                    lblHu_Qty.BackColor = Color.White;
                    lblpt.ForeColor = Color.Red;
                }
                else
                {
                    lblHu_Qty.ForeColor = Color.Lime;
                    lblHu_Qty.BackColor = Color.White;
                    lblpt.ForeColor = Color.Lime;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void SMT_WEATHER_MGNT_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible)
                {
                    _time = 60;
                    WH_CD = ComVar.Var._Area;
                    strDate = ComVar.Var._strValue1;
                    strCHK = ComVar.Var._strValue5;
                    LoadCHKPoint();
                    //navigationFrame1.SelectedPage = navigationPage1;
                    //BindingData("PAGE1");
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
        /*Button Prev, Next*/
        private void btnDaily_Click(object sender, EventArgs e)
        {
            try
            {
                navigationFrame1.SelectedPage = navigationPage1;
                strNavPageCurr = "PAGE1";
                BindingData(strNavPageCurr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void btnHourly_Click(object sender, EventArgs e)
        {
            try
            {
                navigationFrame1.SelectedPage = navigationPage2;
                strNavPageCurr = "PAGE2";
                BindingData(strNavPageCurr);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "minimized";
        }
        /*Database*/
        private DataTable SMT_MAT_WEATHER_MGNT(string ARG_TYPE, string ARG_PLANT = "", string ARG_LINE = "", string ARG_DATE = "", string ARG_SEQ = "")
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
                MyOraDB.Parameter_Values[3] = ARG_DATE;
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

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                fplWeather.HorizontalScroll.Value = fplWeather.HorizontalScroll.Maximum - hScrollBar1.Value - (hScrollBar1.LargeChange) < 0 ? 0 : fplWeather.HorizontalScroll.Maximum - hScrollBar1.Value - (hScrollBar1.LargeChange);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            //ComVar.Var.PLANT_CD = PLANT_CD;
            ComVar.Var._strValue2 = WH_CD;
            ComVar.Var._strValue3 = btnCheckLocate.EditValue.ToString();
            ComVar.Var._strValue4 = strDate;
            ComVar.Var.callForm = "915";
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void btnCheckLocate_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            try
            {
                if (!_first_load)
                {
                    navigationFrame1.SelectedPage = navigationPage1;
                    strNavPageCurr = "PAGE1";
                    BindingData("PAGE1");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
