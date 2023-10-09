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
    public partial class SMT_QUALITY_COCKPIT_DEFECTIVE : Form
    {
        #region ========= [Global Variable] ==============================================

        //private readonly string _strHeader = " Defective Report";
        int _time = 0;
        string _crr_date = "";
        string _crr_div = "OSP", _div_nm = "Outsole";
        DataTable _dtArea = null;
        bool _is_All_Load = false;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_DEFECTIVE()
        {
            InitializeComponent();
        }

        private void SMT_QUALITY_COCKPIT_DEFECTIVE_Load(object sender, EventArgs e)
        {
            cboDateTo.EditValue = DateTime.Now.AddDays(-1);
            cboDateFr.EditValue = DateTime.Now.AddDays(-1);
            //DateTime dt = DateTime.Now;
            //DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            //cboDateFr.EditValue = fistdate;
        }

        private void SMT_QUALITY_COCKPIT_DEFECTIVE_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 30;

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Dispose();
            }

        }
        #endregion ========= [Form Init] ==============================================

        #region ========= [Timer Event] ==========================================

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

        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================

        private void cboDateFr_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }
        private void cboDateTo_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            _time = 30;
        }
        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void gvwMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                BandedGridView _gvw = sender as BandedGridView;

                if (e.RowHandle < 0 || _gvw.RowCount == 0)
                    return;

                if (e.Column.FieldName != "DIV" && e.RowHandle == _gvw.RowCount - 1)
                {
                    e.DisplayText = e.CellValue + "%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void gvwMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (grdMain.DataSource == null || gvwMain.RowCount < 1) return;

                if (gvwMain.GetRowCellValue(e.RowHandle, "DIV").ToString().ToUpper().Contains("DEFECTIVE RATE"))
                {
                    e.Appearance.BackColor = Color.LightYellow;

                    if (e.Column.FieldName != "DIV")
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                }

                if (gvwMain.GetRowCellValue(e.RowHandle, "DIV").ToString().ToUpper().Contains("DEFECTIVE QUANTITY"))
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 218, 153);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void chartMain_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chartMain.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    _div_nm = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["COL_CAPTION"].ToString() == _div_nm)
                        {
                            _crr_div = _dtArea.Rows[iRow]["COL_NM"].ToString();
                        }
                    }
                }
                else
                {
                    if (hit.AxisLabelItem == null) return;
                    _div_nm = hit.AxisLabelItem.AxisValue.ToString();

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["COL_CAPTION"].ToString() == _div_nm)
                        {
                            _crr_div = _dtArea.Rows[iRow]["COL_NM"].ToString();
                        }
                    }
                }

                _time = 10;
                SetDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chartMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chartMain.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    _div_nm = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["COL_CAPTION"].ToString() == _div_nm)
                        {
                            _crr_div = _dtArea.Rows[iRow]["COL_NM"].ToString();
                        }
                    }
                }

                _time = 10;
                SetDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================

        public void SetData()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                _is_All_Load = true;

                DataTable _dtData = GetOracleData("Q1");
                DataTable _dtGrid = GetOracleData("Q2");

                if (_dtData != null && _dtData.Rows.Count > 0 && _dtGrid != null && _dtGrid.Rows.Count > 0)
                {
                    //Load Chart Main
                    LoadDataChart("Q1", _dtData);

                    //Load Grid Main
                    var distinctValues = _dtGrid.AsEnumerable()
                                        .Select(row => new
                                        {
                                            COL_NM = row.Field<string>("COL_NM"),
                                            COL_CAPTION = row.Field<string>("COL_CAPTION"),
                                            RN = row.Field<decimal>("RN"),
                                        })
                                        .Distinct().OrderBy(r => r.RN);
                    DataTable _dtHead = LINQResultToDataTable(distinctValues).Select("", "RN").CopyToDataTable();
                    _dtArea = _dtHead;

                    CreateGridSize(grdMain, gvwMain, _dtHead);
                    DataTable _dtf = BindingData(_dtGrid);
                    grdMain.DataSource = _dtf;
                    FormatGrid();

                    SetDataDetail();
                }

                _dtData = null;
                _dtGrid = null;

                splashScreenManager1.CloseWaitForm();
                _is_All_Load = false;
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                _is_All_Load = false;
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }

        public void SetDataDetail()
        {
            try
            {
                if (_is_All_Load == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                DataTable _dtModel = GetOracleData("Q3");
                DataTable _dtReason = GetOracleData("Q4");

                if (_dtModel != null && _dtModel.Rows.Count > 0)
                {
                    //Load Chart Model
                    LoadDataChart("Q3", _dtModel);
                }
                if (_dtReason != null && _dtReason.Rows.Count > 0)
                {
                    //Load Chart Model
                    LoadDataChart("Q4", _dtReason);
                }
                _dtModel = null;
                _dtReason = null;

                if (_is_All_Load == false)
                {
                    splashScreenManager1.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (_is_All_Load == false)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }

        public void FormatGrid()
        {
            try
            {
                grdMain.BeginUpdate();

                for (int i = 0; i < gvwMain.Columns.Count; i++)
                {
                    gvwMain.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwMain.Columns[i].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    gvwMain.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    gvwMain.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gvwMain.Columns[i].OptionsColumn.ReadOnly = true;
                    gvwMain.Columns[i].OptionsColumn.AllowEdit = false;
                    gvwMain.Columns[i].Width = 200;
                    if (i>0)
                    {
                        gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvwMain.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                        gvwMain.Columns[i].DisplayFormat.FormatString = "#,0.##";
                        gvwMain.Columns[i].Width = 125;
                    }

                    gvwMain.Columns[i].AppearanceCell.Font = new Font("Calibri", 14, FontStyle.Regular);
                    gvwMain.Columns[i].AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);
                }
                gvwMain.RowHeight = 45;
                gvwMain.TopRowIndex = 0;

                grdMain.EndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable BindingData(DataTable dtSource)
        {
            try
            {
                var distinctValues = dtSource.AsEnumerable()
                                        .Select(row => new
                                        {
                                            DIV = row.Field<string>("DIV"),
                                            OP_CD = row.Field<string>("OP_CD"),
                                            QTY = row.Field<decimal>("QTY"),
                                        })
                                        .Distinct();
                DataTable _dtf = LINQResultToDataTable(distinctValues).Select("").CopyToDataTable();
                DataTable _dtData = Pivot(_dtf, _dtf.Columns["OP_CD"], _dtf.Columns["QTY"]);

                return _dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void CreateGridSize(GridControl gridControl, BandedGridView gridView, DataTable dtSource)
        {
            //gridControl.Hide();
            gridView.BeginDataUpdate();
            try
            {
                gridView.Columns.Clear();
                gridView.Bands.Clear();
                gridControl.DataSource = null;

                while (gridView.Columns.Count > 0)
                {
                    gridView.Columns.RemoveAt(0);
                }

                gridView.OptionsView.ShowColumnHeaders = false;

                GridBand gridBand = null;
                BandedGridColumn colBand = new BandedGridColumn();

                gridBand = new GridBand() { Caption = "Division" };
                gridView.Bands.Add(gridBand);
                gridBand.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                gridBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gridBand.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridBand.AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);
                gridBand.AppearanceHeader.BackColor = Color.FromArgb(64, 64, 64);

                colBand = new BandedGridColumn() { FieldName = "DIV", Visible = true };
                colBand.Visible = true;
                colBand.Width = 150;

                gridBand.Columns.Add(colBand);

                for (int iRow = 0; iRow < dtSource.Rows.Count; iRow++)
                {
                    gridBand = new GridBand() { Caption = dtSource.Rows[iRow]["COL_CAPTION"].ToString() };
                    gridView.Bands.Add(gridBand);
                    gridBand.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    gridBand.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    gridBand.AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);
                    gridBand.AppearanceHeader.BackColor = Color.Teal;

                    colBand = new BandedGridColumn() { FieldName = dtSource.Rows[iRow]["COL_NM"].ToString(), Visible = true };
                    colBand.Visible = true;
                    colBand.Width = 120;

                    gridBand.Columns.Add(colBand);
                }
            }
            catch
            {
                //throw EX;
            }
            //gridControl.Show();
            gridView.EndDataUpdate();
            gridView.ExpandAllGroups();
        }

        public DataTable GetOracleData(string qtype)
        {
            try
            {
                DataTable _dtData = null;
                _dtData = GetBotDefective(qtype, cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd"), _crr_div);

                return _dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void LoadDataChart(string _qtype, DataTable _dtSource)
        {
            try
            {
                if (_qtype == "Q1")
                {
                    if (chartMain.Series[0].Points.Count > 0)
                    {
                        chartMain.Series[0].Points.Clear();
                    }
                    if (chartMain.Series[1].Points.Count > 0)
                    {
                        chartMain.Series[1].Points.Clear();
                    }
                    chartMain.DataSource = _dtSource;

                    for (int i = 0; i < _dtSource.Rows.Count; i++)
                    {
                        string Argument = _dtSource.Rows[i]["DIV"].ToString();
                        double Value1, Value2;
                        double.TryParse(_dtSource.Rows[i]["DEF_QTY"].ToString(), out Value1);
                        double.TryParse(_dtSource.Rows[i]["RATE"].ToString(), out Value2);

                        chartMain.Series[0].Points.Add(new SeriesPoint(Argument, Value1));
                        chartMain.Series[1].Points.Add(new SeriesPoint(Argument, Value2));
                    }

                    ((XYDiagram)chartMain.Diagram).AxisX.Label.Staggered = false;
                }
                else if (_qtype == "Q3")
                {
                    if (chartModel.Series[0].Points.Count > 0)
                    {
                        chartModel.Series[0].Points.Clear();
                    }
                    if (chartModel.Series[1].Points.Count > 0)
                    {
                        chartModel.Series[1].Points.Clear();
                    }
                    chartModel.DataSource = null;

                    for (int i = 0; i < _dtSource.Rows.Count; i++)
                    {
                        string Argument = _dtSource.Rows[i]["MODEL_NM"].ToString();
                        double Value1, Value2;
                        double.TryParse(_dtSource.Rows[i]["DEF_QTY"].ToString(), out Value1);
                        double.TryParse(_dtSource.Rows[i]["RATE"].ToString(), out Value2);

                        chartModel.Series[0].Points.Add(new SeriesPoint(Argument, Value1));
                        chartModel.Series[1].Points.Add(new SeriesPoint(Argument, Value2));
                    }
                    chartModel.DataSource = _dtSource;

                    DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                    chartTitle.Text = _div_nm + " By Top 10 Model";
                    chartModel.Titles.Clear();

                    // Define the alignment of the titles.
                    chartTitle.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                    chartModel.Titles.Add(chartTitle);

                    if (_dtSource.Rows.Count <= 6)
                    {
                        ((XYDiagram)chartModel.Diagram).AxisX.Label.Font = new Font("Calibri", 12F, FontStyle.Regular);
                    }
                    else
                    {
                        ((XYDiagram)chartModel.Diagram).AxisX.Label.Font = new Font("Calibri", 12F, FontStyle.Regular);
                    }
                    ((XYDiagram)chartModel.Diagram).AxisX.Label.Staggered = false;
                }
                else if (_qtype == "Q4")
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                    chartTitle.Text = _div_nm + " By Reason";
                    chartReason.Titles.Clear();

                    chartReason.DataSource = _dtSource;
                    chartReason.Series[0].ArgumentDataMember = "REASON_NM";
                    chartReason.Series[0].ValueDataMembers.AddRange(new string[] { "RATE" });

                    // Define the alignment of the titles.
                    chartTitle.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold ^ FontStyle.Italic);
                    chartReason.Titles.Add(chartTitle);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        DataTable GetDataTable(GridView view)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn c in view.Columns)
                dt.Columns.Add(c.FieldName, c.ColumnType);
            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt.Columns[c].ColumnName);
                dt.Rows.Add(rowValues);
            }
            return dt;
        }

        private DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;
            if (Linqlist == null) return dt;
            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }



        DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
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

       
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================

        public DataTable GetBotDefective(string V_P_TYPE, string V_P_DATE_FR, string V_P_DATE_TO, string V_P_DIV)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            MyOraDB.ShowErr = true;
            try
            {
                string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_DEFECTIVE";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                MyOraDB.Parameter_Name[3] = "V_P_DIV";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = V_P_TYPE;
                MyOraDB.Parameter_Values[1] = V_P_DATE_FR;
                MyOraDB.Parameter_Values[2] = V_P_DATE_TO;
                MyOraDB.Parameter_Values[3] = V_P_DIV;
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

        #endregion ========= [Procedure Call] ===========================================

    }
}
