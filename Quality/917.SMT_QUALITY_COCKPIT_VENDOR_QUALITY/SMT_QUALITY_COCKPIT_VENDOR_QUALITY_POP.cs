using DevExpress.Utils;
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
    public partial class SMT_QUALITY_COCKPIT_VENDOR_QUALITY_POP : Form
    {
        public SMT_QUALITY_COCKPIT_VENDOR_QUALITY_POP()
        {
            InitializeComponent();
            //lblHeader.Text = _strHeader;
        }
        string _date,_dateto, _plant_code, _line_code, _vendor;
        public SMT_QUALITY_COCKPIT_VENDOR_QUALITY_POP(string date,string dateto, string line, string vendor)
        {
            InitializeComponent();
            _date = date;
            _line_code = line;
            _dateto = dateto;
            _vendor = vendor;
        }
        private readonly string _strHeader = "       Vendor Quality Detail";
        int _time = 0;

        #region Load-Visible Change-Timer
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
           
        }

        private void SMT_QUALITY_COCKPIT_REWORK_POP_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                SetData();

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
        public async Task<DataSet> DataSelect(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_LINE,string ARG_VENDOR)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
                MyOraDB.ShowErr = true;
                DataSet ds_ret;
                try
                {
                    string process_name = "PKG_SMT_QUALITY_COCKPIT.SMT_QUA_VENDOR_QUALITY";

                    MyOraDB.ReDim_Parameter(6);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                    MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                    MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[4] = "V_P_VENDOR";
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
                    MyOraDB.Parameter_Values[3] = ARG_LINE;
                    MyOraDB.Parameter_Values[4] = ARG_VENDOR;
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
            });
        }

        #endregion DB
        
   

        #endregion DB

        private void SetChart(DataTable dtChart)
        {
            try
            {
                chart.DataSource = null;
                if (dtChart == null) return;
                chart.DataSource = dtChart;
                chart.Series[0].ArgumentDataMember = "INCOME_YMD";
                chart.Series[0].ValueDataMembers.AddRange(new string[] { "INS_QTY" });
                chart.Series[1].ArgumentDataMember = "INCOME_YMD";
                chart.Series[1].ValueDataMembers.AddRange(new string[] { "RE_QTY" });
                // Customize the chart appearance

                XYDiagram diagram = chart.Diagram as XYDiagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.EnableAxisXScrolling = true;
                diagram.AxisX.VisualRange.Auto = false;
                diagram.AxisX.Label.Angle = -45;

                if (dtChart.Rows.Count > 20)
                {
                    diagram.AxisX.VisualRange.SetMinMaxValues(dtChart.Rows[dtChart.Rows.Count - 21]["INCOME_YMD"], dtChart.Rows[dtChart.Rows.Count - 1]["INCOME_YMD"]);
                }
                else
                {
                    diagram.AxisX.VisualRange.SetMinMaxValues(dtChart.Rows[0]["INCOME_YMD"], dtChart.Rows[dtChart.Rows.Count - 1]["INCOME_YMD"]);
                }
                ((DevExpress.XtraCharts.XYDiagram)chart.Diagram).AxisX.QualitativeScaleOptions.AutoGrid = false;
            }
            catch {
            }
        }


        private async void SetData()
        {
            try
            {

                DataSet dsData = await DataSelect("Q_POP", _date, _dateto, _line_code,_vendor);

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
        private DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            // find primary key columns 
            //(i.e. everything but pivot column and pivot value)
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
            .Select(c => c.ColumnName)
            .ToArray();

            // prep results table
            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();
            dt.AsEnumerable()
            .Select(r => r[pivotColumn.ColumnName].ToString())
            .Distinct().ToList()
            .ForEach(c => result.Columns.Add(c, pivotValue.DataType));
            //.ForEach(c => result.Columns.Add(c, pivotColumn.DataType));

            // load it
            foreach (DataRow row in dt.Rows)
            {
                // find row to update
                DataRow aggRow = result.Rows.Find(
                pkColumnNames
                .Select(c => row[c])
                .ToArray());
                // the aggregate used here is LATEST 
                // adjust the next line if you want (SUM, MAX, etc...)
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];


            }

            return result;
        }
       
    }
}
