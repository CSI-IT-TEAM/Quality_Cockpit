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
    public partial class SMT_QUALITY_COCKPIT_HFPA_POP : Form
    {
        public SMT_QUALITY_COCKPIT_HFPA_POP()
        {
            InitializeComponent();
            //lblHeader.Text = _strHeader;
        }
        string _date,_dateto, _plant_code, _line_code;
        public SMT_QUALITY_COCKPIT_HFPA_POP(string date,string dateto, string line)
        {
            InitializeComponent();
            _date = date;
            _line_code = line;
            _dateto = dateto;
        }
        private readonly string _strHeader = "       Daily HFPA";
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
        public async Task<DataSet> sbGetHFPA(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_LINE)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_HFPA";

                    MyOraDB.ReDim_Parameter(8);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                    MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                    MyOraDB.Parameter_Name[3] = "V_P_LINE_CD";
                    MyOraDB.Parameter_Name[4] = "OUT_CURSOR";
                    MyOraDB.Parameter_Name[5] = "OUT_CURSOR2";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR3";
                    MyOraDB.Parameter_Name[7] = "OUT_CURSOR4";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;


                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = ARG_YMDF;
                    MyOraDB.Parameter_Values[2] = ARG_YMDT;
                    MyOraDB.Parameter_Values[3] = ARG_LINE;
                    MyOraDB.Parameter_Values[4] = "";
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
            });
        }

        #endregion DB
        
   

        #endregion DB

        private void SetChart(DataTable dtChart, DataTable dtChart1)
        {
            try
            {
                chartControl1.Series.Clear();
                if (dtChart.Rows.Count <= 0|| dtChart1.Rows.Count <= 0) return;
                int minLevel = Convert.ToInt32(dtChart1.Compute("min([MIN_VAL])", string.Empty));
                int maxLevel = Convert.ToInt32(dtChart1.Compute("max([MAX_VAL])", string.Empty));
                chartControl1.DataSource = dtChart;
                for (int i = 1; i < dtChart.Columns.Count; i++)
                {
                    Series series = new Series(dtChart.Columns[i].ColumnName, ViewType.Spline);

                    series.ArgumentDataMember = "COL_DAY";
                    series.ValueDataMembers.AddRange(new string[] { dtChart.Columns[i].ColumnName });
                    series.CrosshairLabelPattern = "{S} : {V:#,0.#}";

                    // Customize the appearance of each series
                    LineSeriesView lineSeriesView = series.View as LineSeriesView;
                    series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                    series.Label.TextPattern = "{V:#,0.#}";
                    ((LineSeriesView)series.View).MarkerVisibility = DefaultBoolean.True;
                    //series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAroundPoint;
                    // Add the series to the chart control
                    chartControl1.Series.Add(series);
                }
                ((XYDiagram)chartControl1.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                ((XYDiagram)chartControl1.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                ((XYDiagram)chartControl1.Diagram).EnableAxisXScrolling = true;
                ((XYDiagram)chartControl1.Diagram).AxisX.VisualRange.Auto = false;
                ((XYDiagram)chartControl1.Diagram).AxisX.Label.Angle = 0;
                // Show Legend
                chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Right;
                chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
                chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
                chartControl1.Legend.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                // Customize the chart appearance
                ((XYDiagram)chartControl1.Diagram).AxisY.WholeRange.Auto = true;
                XYDiagram diagram = chartControl1.Diagram as XYDiagram;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.EnableAxisXScrolling = true;
                diagram.AxisX.VisualRange.Auto = false;
                diagram.AxisX.Label.Angle = -45;




                if (diagram != null)
                {
                    diagram.AxisX.Label.Font = new System.Drawing.Font("Calibri", 12F);
                    diagram.AxisX.Tickmarks.MinorVisible = false;
                    diagram.AxisX.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
                    diagram.AxisX.Title.Text = "Date";
                    diagram.AxisX.Title.TextColor = System.Drawing.Color.DodgerBlue;
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12F);
                    diagram.AxisY.Label.TextPattern = "{V:#,0}";
                    diagram.AxisY.Tickmarks.MinorVisible = false;
                    diagram.AxisY.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
                    diagram.AxisY.Title.Text = "Quality Rate(%)";
                    diagram.AxisY.Title.TextColor = System.Drawing.Color.Orange;
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    if (dtChart.Rows.Count > 20)
                    {
                        diagram.AxisX.VisualRange.SetMinMaxValues(dtChart.Rows[dtChart.Rows.Count - 21]["COL_DAY"], dtChart.Rows[dtChart.Rows.Count - 1]["COL_DAY"]);
                    }
                    else
                    {
                        diagram.AxisX.VisualRange.SetMinMaxValues(dtChart.Rows[0]["COL_DAY"], dtChart.Rows[dtChart.Rows.Count - 1]["COL_DAY"]);
                    }
                }
                ((DevExpress.XtraCharts.XYDiagram)chartControl1.Diagram).AxisY.WholeRange.Auto = false;
                ((DevExpress.XtraCharts.XYDiagram)chartControl1.Diagram).AxisY.WholeRange.MinValue = minLevel - 10;
                ((DevExpress.XtraCharts.XYDiagram)chartControl1.Diagram).AxisY.WholeRange.MaxValue = maxLevel + 10;
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

                DataSet dsData = await sbGetHFPA("POP", _date, _dateto, _line_code);

                if (dsData == null) return;
                DataTable dtChart = Pivot(dsData.Tables[0], dsData.Tables[0].Columns["MLINE_CD"], dsData.Tables[0].Columns["RATE"]); //dsData.Tables[0];
                DataTable dtChart1 = dsData.Tables[1];
                SetChart(dtChart, dtChart1);
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
