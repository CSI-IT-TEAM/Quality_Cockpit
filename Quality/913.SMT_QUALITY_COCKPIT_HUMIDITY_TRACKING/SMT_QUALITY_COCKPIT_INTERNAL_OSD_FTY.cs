using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_INTERNAL_OSD_FTY : Form
    {
        int _time = 0;
        string _plant_cd = ComVar.Var._strValue1;
        DataTable _dtf = null;

        public SMT_QUALITY_COCKPIT_INTERNAL_OSD_FTY()
        {
            InitializeComponent();
        }

        private void SMT_QUALITY_COCKPIT_INTERNAL_OSD_FTY_Load(object sender, EventArgs e)
        {
            GET_COMBO_DATA("CBO_DATE");
        }

        #region Load-Visible Change-Timer

        public void LoadData()
        {
            try
            {
                DataTable _dtDate = Get_Internal_Data("Q_DATE", _plant_cd, dtpDateF.DateTime.ToString("yyyyMMdd"), dtpDateT.DateTime.ToString("yyyyMMdd"));
                DataTable _dtPlant = null;
                string _lasted_date = "";

                _dtf = _dtDate;

                if (_dtDate != null && _dtDate.Rows.Count > 0)
                {
                    SetChartDate(_dtDate);

                    _lasted_date = _dtDate.Rows[_dtDate.Rows.Count - 1]["YMD"].ToString();
                    _dtPlant = Get_Internal_Data("Q_PLANT", _plant_cd, _lasted_date, _lasted_date);
                }
                else
                {
                    chartDate.DataSource = null;
                    chartDate.Series[0].Points.Clear();
                    chartDate.Series[1].Points.Clear();
                }

                if (_dtPlant != null && _dtPlant.Rows.Count > 0)
                {
                    SetChartPlant(_dtPlant);

                    for(int iRow = 0; iRow < _dtDate.Rows.Count; iRow++)
                    {
                        if(_dtDate.Rows[iRow]["YMD"].ToString() == dtpDateT.DateTime.ToString("yyyyMMdd"))
                        {
                            lbl_title.Text = "Date: " + _dtDate.Rows[iRow]["MON"].ToString();
                            break;
                        }
                    }
                }
                else
                {
                    chartPlant.DataSource = null;
                    chartPlant.Series[0].Points.Clear();
                    chartPlant.Series[1].Points.Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GET_COMBO_DATA(string type)
        {
            if (type == "CBO_DATE")
            {
                DataTable dt = Get_Internal_Data(type, "", "", "");
                dtpDateT.EditValue = dt.Rows[0]["TODAY"];
                dtpDateF.EditValue = dt.Rows[0]["PREV_DAY"];
            }
        }

        private void SetChartDate(DataTable argData)
        {
            chartDate.DataSource = null;
            chartDate.Series[0].Points.Clear();
            chartDate.Series[1].Points.Clear();

            chartDate.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartDate.Series[1].ArgumentScaleType = ScaleType.Qualitative;

            if (argData == null) return;
            for (int i = 0; i <= argData.Rows.Count - 1; i++)
            {
                chartDate.Series[0].Points.Add(new SeriesPoint(argData.Rows[i]["MON"].ToString(), argData.Rows[i]["OSD"]));
                chartDate.Series[1].Points.Add(new SeriesPoint(argData.Rows[i]["MON"].ToString(), argData.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argData.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate > 2)
                {
                    chartDate.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate > 1)
                {
                    chartDate.Series[0].Points[i].Color = Color.Yellow;
                }
                else
                {
                    chartDate.Series[0].Points[i].Color = Color.Green;
                }
            }
            chartDate.RuntimeHitTesting = true;
        }

        private void SetChartPlant(DataTable argData)
        {
            chartPlant.DataSource = null;
            chartPlant.Series[0].Points.Clear();
            chartPlant.Series[1].Points.Clear();

            chartPlant.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartPlant.Series[1].ArgumentScaleType = ScaleType.Qualitative;

            if (argData == null) return;
            for (int i = 0; i <= argData.Rows.Count - 1; i++)
            {
                chartPlant.Series[0].Points.Add(new SeriesPoint(argData.Rows[i]["LINE_NM"].ToString(), argData.Rows[i]["OSD"]));
                chartPlant.Series[1].Points.Add(new SeriesPoint(argData.Rows[i]["LINE_NM"].ToString(), argData.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argData.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate > 2)
                {
                    chartPlant.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate > 1)
                {
                    chartPlant.Series[0].Points[i].Color = Color.Yellow;
                }
                else
                {
                    chartPlant.Series[0].Points[i].Color = Color.Green;
                }
            }
        }

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

        public DataTable Get_Internal_Data(string ARG_QTYPE, string ARG_FACTORY, string ARG_DATEF, string ARG_DATET)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            MyOraDB.ShowErr = true;
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.SEPHIROTH;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_05.SP_GET_INTERNAL_OSD";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "ARG_FACTORY";
                MyOraDB.Parameter_Name[2] = "ARG_DATEF";
                MyOraDB.Parameter_Name[3] = "ARG_DATET";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_FACTORY;
                MyOraDB.Parameter_Values[2] = ARG_DATEF;
                MyOraDB.Parameter_Values[3] = ARG_DATET;
                MyOraDB.Parameter_Values[4] = "";

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[process_name];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        #endregion DB

        #region Events

        private void SMT_QUALITY_COCKPIT_INTERNAL_OSD_FTY_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 30;
                timer1.Start();

                _plant_cd = ComVar.Var._strValue1;
                switch (_plant_cd)
                {
                    case "VJ1":
                        lblHeader.Text = "   Vinh Cuu Internal OS&&D";
                        break;
                    case "VJ2":
                        lblHeader.Text = "   Long Thanh Internal OS&&D";
                        break;
                    case "VJ3":
                        lblHeader.Text = "   Tan Phu Internal OS&&D";
                        break;
                    default:
                        lblHeader.Text = "   Internal OS&&D";
                        break;
                }
            }
            else
            {
                timer1.Stop();
                Dispose();
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                LoadData();
            }
        }

        private void btnEx_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "972";
        }

        private void chartDate_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;

                ChartHitInfo hit = chartDate.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                string _selected_date = "";

                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    string _col_nm = point.Argument;    /*Get Sub_Div*/

                    for(int iRow = 0; iRow < _dtf.Rows.Count; iRow++)
                    {
                        if(_dtf.Rows[iRow]["MON"].ToString() == _col_nm)
                        {
                            _selected_date = _dtf.Rows[iRow]["YMD"].ToString();
                            break;
                        }
                    }

                    DataTable _dtPlant = Get_Internal_Data("Q_PLANT", _plant_cd, _selected_date, _selected_date);

                    if (_dtPlant != null && _dtPlant.Rows.Count > 0)
                    {
                        SetChartPlant(_dtPlant);
                        lbl_title.Text = "Date: " + _col_nm;
                    }
                    else
                    {
                        chartPlant.DataSource = null;
                        chartPlant.Series[0].Points.Clear();
                        chartPlant.Series[1].Points.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
