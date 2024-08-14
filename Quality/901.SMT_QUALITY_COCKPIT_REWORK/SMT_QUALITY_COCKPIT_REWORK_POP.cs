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

        #region ========= [Global Variable] ==============================================

        string _date, _plant_code, _line_code;
        private readonly string _strHeader = "  Daily Rework";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        //DataTable dsData = null;
        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================
        public SMT_QUALITY_COCKPIT_REWORK_POP()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        
        public SMT_QUALITY_COCKPIT_REWORK_POP(string date, string plant, string line)
        {
            InitializeComponent();
            _date = date;
            _plant_code = plant;
            _line_code = line;
        }
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {

        }

        private void SMT_QUALITY_COCKPIT_REWORK_POP_VisibleChanged(object sender, EventArgs e)
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

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================

        //private void SetChart(DataTable argDtChart)
        //{
        //    chartControl1.Series[0].Points.Clear();
        //    chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;

        //    if (argDtChart == null || argDtChart.Rows.Count == 0) return;

        //    // Bước 1: Tính toán giá trị Min và Max từ cột "REWORK_QTY"
        //    double minValue = Convert.ToDouble(argDtChart.Rows[0]["REWORK_QTY"]);
        //    double maxValue = minValue;

        //    foreach (DataRow row in argDtChart.Rows)
        //    {
        //        double value = Convert.ToDouble(row["REWORK_QTY"]);
        //        if (value < minValue)
        //        {
        //            minValue = value;
        //        }
        //        if (value > maxValue)
        //        {
        //            maxValue = value;
        //        }
        //    }

        //    // Kiểm tra nếu biểu đồ là dạng XYDiagram
        //    XYDiagram diagram = chartControl1.Diagram as XYDiagram;
        //    if (diagram != null)
        //    {
        //        // Bước 2: Thiết lập WholeRange và VisualRange cho trục Y
        //        diagram.AxisY.WholeRange.SetMinMaxValues(-2, maxValue);
        //        diagram.AxisY.VisualRange.SetMinMaxValues(-2, maxValue);
        //    }

        //    // Bước 3: Thêm dữ liệu vào biểu đồ
        //    foreach (DataRow row in argDtChart.Rows)
        //    {
        //        string argument = row["HH"].ToString();
        //        double value = Convert.ToDouble(row["REWORK_QTY"]);
        //        chartControl1.Series[0].Points.Add(new SeriesPoint(argument, value));
        //    }
        //}


        private void SetChart(DataTable argDtChart)
        {

            chartControl1.Series[0].Points.Clear();
            //chartControl1.Series[1].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            //chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["HH"].ToString(), argDtChart.Rows[i]["REWORK_QTY"]));
                //chartControl1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["RATE"]));

                //double rate;
                //double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                //if (rate > 6)
                //{
                //    chartControl1.Series[0].Points[i].Color = Color.Red;
                //}
                //else if (rate > 3)
                //{
                //    chartControl1.Series[0].Points[i].Color = Color.Yellow;
                //}
                //else
                //{
                //    chartControl1.Series[0].Points[i].Color = Color.Green;
                //}
            }
        }


        private void SetData()
        {
            try
            {

                DataSet dsData = Data_Select("Q_DETAIL", _date, _date, _plant_code, _line_code);

                if (dsData == null) return;
                DataTable dtGrid = dsData.Tables[1];
                DataTable dtChart = dsData.Tables[1];
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
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================

        public DataSet Data_Select(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {

            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_REWORK";

                MyOraDB.ReDim_Parameter(8);
                MyOraDB.Process_Name = process_name;

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
                MyOraDB.Parameter_Values[1] = ARG_YMDF;
                MyOraDB.Parameter_Values[2] = ARG_YMDT;
                MyOraDB.Parameter_Values[3] = ARG_PLANT;
                MyOraDB.Parameter_Values[4] = ARG_LINE;
                MyOraDB.Parameter_Values[5] = "";
                MyOraDB.Parameter_Values[6] = "";
                MyOraDB.Parameter_Values[7] = "";

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
        #endregion ========= [Procedure Call] ===========================================

    }
}
