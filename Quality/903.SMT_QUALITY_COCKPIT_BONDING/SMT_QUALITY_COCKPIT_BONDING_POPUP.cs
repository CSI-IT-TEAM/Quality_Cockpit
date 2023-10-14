using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.Linq;
using DevExpress.XtraCharts;
using System.Threading.Tasks;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_BONDING_POPUP : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Daily Bonding";

        string _date, _plant_code, _line_code;
        int _time = 0;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_BONDING_POPUP()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        public SMT_QUALITY_COCKPIT_BONDING_POPUP(string date, string plant, string line)
        {
            InitializeComponent();
            _date = date;
            _plant_code = plant;
            _line_code = line;
        }
        private void SMT_QUALITY_COCKPIT_BONDING_Load(object sender, EventArgs e)
        {
        }
        private void SMT_QUALITY_COCKPIT_BONDING_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 30;
                timer1.Start();
            }
            else
            {
                timer1.Stop();
            }
        }
        #endregion ========= [Form Init] ==============================================
        
        #region ========= [Timer Event] ==========================================
        private void timer1_Tick(object sender, EventArgs e)
        {
            //lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                SetData();
            }

        }
        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================
        private void cmdBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================
        private async void SetData()
        {
            try
            {
                DataSet dsData = Data_Select("Q", _date, _date, _plant_code, _line_code);

                if (dsData == null) return;
                DataTable dtGrid = dsData.Tables[0];
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

        private void SetChart(DataTable argDtChart)
        {
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["HH"].ToString(), argDtChart.Rows[i]["BOND_QTY"]));
            }
        }
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        private DataSet Data_Select(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {

            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ReDim_Parameter(7);
            MyOraDB.Process_Name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_BONDING_GAP";//

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
            MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
            MyOraDB.Parameter_Name[3] = "V_P_PLANT";
            MyOraDB.Parameter_Name[4] = "V_P_LINE";
            MyOraDB.Parameter_Name[5] = "OUT_CURSOR";
            MyOraDB.Parameter_Name[6] = "OUT_CURSOR2";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
            MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = ARG_QTYPE;
            MyOraDB.Parameter_Values[1] = ARG_YMDF;
            MyOraDB.Parameter_Values[2] = ARG_YMDT;
            MyOraDB.Parameter_Values[3] = ARG_PLANT;
            MyOraDB.Parameter_Values[4] = ARG_LINE;
            MyOraDB.Parameter_Values[5] = "";
            MyOraDB.Parameter_Values[6] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS;
        }
        #endregion ========= [Procedure Call] ===========================================
    }
}
