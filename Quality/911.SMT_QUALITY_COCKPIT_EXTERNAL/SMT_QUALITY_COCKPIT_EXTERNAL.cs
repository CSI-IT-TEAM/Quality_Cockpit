using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
    public partial class SMT_QUALITY_COCKPIT_EXTERNAL : Form
    {
       

        public SMT_QUALITY_COCKPIT_EXTERNAL()
        {
            InitializeComponent();
        }
        #region Global Variant
        int _time = 0;
        string _crr_date = "";
        string _crr_div = "OSP", _div_nm = "Outsole";
        DataTable _dtArea = null;
        bool _is_All_Load = false;
        int _start_column = 0;
        #endregion Global Variant

        #region Load-Visible Change-Timer

        private void SMT_QUALITY_COCKPIT_DEFECTIVE_Load(object sender, EventArgs e)
        {
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            _crr_date = DateTime.Now.ToString("yyyyMMdd");
            InitCombo("C_FACTORY", cboFactory);
        }
        private void SMT_QUALITY_COCKPIT_DEFECTIVE_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //clear_chart();
                _time = 28;

                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Dispose();
            }

        }

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

        #endregion Load-Visible Change-Timer

        #region Function

        private void InitCombo(string type, LookUpEdit cboname)
        {
            DataTable dtdata = GET_COMBOBOX( V_P_TYPE :type,V_P_CONDITION:"",V_P_CONDITION1:"");
            SetCombobox(dtdata, cboname);
        }
        private void SetCombobox(DataTable dtdata, LookUpEdit cbname)//ham set combo
        {
            if (cbname.IsPopupOpen) return;
            if (dtdata == null)
            {
                cbname.Properties.DataSource = dtdata;
                return;

            }
            cbname.Properties.Columns.Clear();//xoa du lieu
            cbname.Properties.DataSource = dtdata;//truyen du lieu moi
            cbname.Properties.ValueMember = dtdata.Columns[0].ColumnName;//gan cot can hien thi
            cbname.Properties.DisplayMember = dtdata.Columns[1].ColumnName;//gia tri can hien thi
            cbname.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(dtdata.Columns[0].ColumnName));
            cbname.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(dtdata.Columns[1].ColumnName));
            cbname.Properties.Columns[dtdata.Columns[0].ColumnName].Caption = "Name";
            cbname.Properties.Columns[dtdata.Columns[0].ColumnName].Visible = false;

            cbname.ItemIndex = 0;
        }
        private async void SetData()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                _is_All_Load = true;

                DataSet dsData = GetOracleData();
                if (dsData == null) return;
                DataTable _dtData = dsData.Tables[0];

                //if (_dtData != null && _dtData.Rows.Count > 0)
                //{
                    //Load Chart Main
                    if (_dtData.Select("DIV <> 1 AND LOCATE <> 'TOTAL'", "RN1, RN2").Count() > 0)
                    {
                        DataTable dtChart = _dtData.Select("DIV <>1 AND LOCATE <> 'TOTAL'", "RN1, RN2").CopyToDataTable();
                        dtChart.Columns.Remove("DISTINCTROW");
                        DataTable _dtPivot = Pivot(dtChart, dtChart.Columns["DIV"], dtChart.Columns["QTY"]);
                        LoadDataChart("Q1", _dtPivot);
                    }

                    //Load Grid Main
                    DataTable dtSource = new DataTable();
                    if (CreateGrid(_dtData, grdMain, gvwMain))
                    {
                        dtSource = GetDataTable(gvwMain);
                        if (bindingData_Detail(dtSource, _dtData, _start_column))
                        {
                            grdMain.DataSource= dtSource;
                            FormatGrid();

                            SetDataDetail();
                        }
                    }
               // }

                _dtData = null;

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
        public DataSet GetOracleData()
        {
            try
            {
                DataSet _dtData = null;
                _dtData = GetExternalOSD("Q", _crr_date,cboFactory.EditValue.ToString(), _crr_div);

                return _dtData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private bool CreateGrid(DataTable dtSource, GridControl gridControl, BandedGridView gridView)
        {
            try
            {
                gridControl.BeginUpdate();
                gridView.OptionsView.ShowGroupPanel = false;
                gridView.OptionsView.AllowCellMerge = true;
                //gridView.BandPanelRowHeight = 35;
                gridView.Bands.Clear();
                gridView.Columns.Clear();
                gridControl.DataSource = null;
                gridView.OptionsView.ShowColumnHeaders = false;
                GridBand band = null;
                GridBand bandchlid1 = null;
                string distinct_row = "", sCol = "";
                _start_column = int.Parse(dtSource.Rows[0]["START_COLUMN"].ToString());

                var distinctValues = dtSource.AsEnumerable()
                                   .Select(row => new
                                   {
                                       RN1 = row.Field<string>("RN1"),
                                       RN2 = row.Field<string>("RN2"),
                                       PARENT_CAPTION = row.Field<string>("PARENT_CAPTION"),
                                       COL_NM = row.Field<string>("COL_NM"),
                                       LABEL_CHART = row.Field<string>("LABEL_CHART"),
                                       COL_CAPTION = row.Field<string>("COL_CAPTION")
                                   })
                                   .Distinct().OrderBy(r => r.RN1 + r.RN2);
                DataTable dttmp = LINQResultToDataTable(distinctValues);
                _dtArea = dttmp;
                band = new GridBand() { Caption = "Division" };
                gridView.Bands.Add(band);
                for (int i = 0; i < _start_column; i++)
                {
                    band.Columns.Add(new BandedGridColumn() { FieldName = dtSource.Columns[i].ColumnName, Visible = true, Caption = "" });
                    band.Fixed = FixedStyle.Left;
                    if (i < 1)
                        gridView.Columns[i].Visible = false;
                }
                int cnt = -1;
                for (int i = 0; i < dttmp.Rows.Count; i++)
                {
                    if (!dttmp.Rows[i]["COL_NM"].ToString().Contains("TOTAL"))
                    {
                        if (!distinct_row.Equals(dttmp.Rows[i]["PARENT_CAPTION"].ToString()))
                        {
                            cnt++;
                            distinct_row = dttmp.Rows[i]["PARENT_CAPTION"].ToString();
                            band = new GridBand() { Caption = dttmp.Rows[i]["PARENT_CAPTION"].ToString() };
                            gridView.Bands.Add(band);
                        }
                        if (!sCol.Equals(dttmp.Rows[i]["COL_NM"].ToString()))
                        {
                            sCol = dttmp.Rows[i]["COL_NM"].ToString();
                            bandchlid1 = new GridBand() { Caption = dttmp.Rows[i]["COL_CAPTION"].ToString() };
                            band.Children.Add(bandchlid1);
                            bandchlid1.Columns.Add(new BandedGridColumn() { FieldName = dttmp.Rows[i]["COL_NM"].ToString(), Visible = true, Caption = dttmp.Rows[i]["COL_CAPTION"].ToString() });
                        }
                    }
                    else
                    {
                        band = new GridBand() { Caption = dttmp.Rows[i]["PARENT_CAPTION"].ToString() };
                        gridView.Bands.Add(band);
                        band.Columns.Add(new BandedGridColumn() { FieldName = dttmp.Rows[i]["COL_NM"].ToString(), Visible = true, Caption = dttmp.Rows[i]["COL_CAPTION"].ToString() });
                    }
                }


                foreach (GridBand gb in gridView.Bands)
                {
                    FormatBand(gb);
                }
                gridControl.EndUpdate();
                return true;
            }
            catch (Exception EX) { return false; }
        }
        private void FormatBand(GridBand root)
        {
            root.AppearanceHeader.Options.UseTextOptions = true;
            root.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            root.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            root.OptionsBand.FixedWidth = true;
            if (root.Children.Count > 0)
            {
                foreach (GridBand child in root.Children)
                {
                    FormatBand(child);
                    child.Width = 55;
                }
            }
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
        private bool bindingData_Detail(DataTable dtSource, DataTable dt, int startcolumn)
        {
            try
            {
                int[] rowtotal = new int[dtSource.Columns.Count];
                string distinct_row = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!distinct_row.Equals(dt.Rows[i]["DISTINCTROW"].ToString()))
                    {
                        dtSource.Rows.Add();
                    }
                    distinct_row = dt.Rows[i]["DISTINCTROW"].ToString();
                    for (int col = 0; col < startcolumn; col++)
                    {
                        dtSource.Rows[dtSource.Rows.Count - 1][dt.Columns[col].ColumnName] = dt.Rows[i][dt.Columns[col].ColumnName].ToString();
                    }
                    dtSource.Rows[dtSource.Rows.Count - 1][dt.Rows[i]["COL_NM"].ToString()] = dt.Rows[i]["QTY"];
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
        public void FormatGrid()
        {
            try
            {
                grdMain.BeginUpdate();

                for (int i = 0; i < gvwMain.Columns.Count; i++)
                {
                    gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gvwMain.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwMain.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                    gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwMain.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    gvwMain.Columns[i].OptionsColumn.ReadOnly = false;
                    gvwMain.Columns[i].OptionsColumn.AllowEdit = false;
                    gvwMain.Columns[i].OptionsFilter.AllowFilter = false;
                    if (i < _start_column)
                    {
                        gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                    {
                        gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        gvwMain.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                        gvwMain.Columns[i].DisplayFormat.FormatString = "#,0.##";
                    }

                    gvwMain.Columns[i].AppearanceCell.Font = new Font("Calibri", 16, FontStyle.Regular);
                    gvwMain.Columns[i].AppearanceHeader.Font = new Font("Calibri", 18, FontStyle.Bold);
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
        #endregion Function

        public void SetDataDetail()
        {
            try
            {
                if (_is_All_Load == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                DataSet _dtSet = GetOracleData();
                DataTable _dtModel = _dtSet.Tables[1];
                DataTable _dtReason = _dtSet.Tables[2];
                
                //Load Chart Model
                LoadDataChart("Q2", _dtModel);
                
                //Load Chart Model
                LoadDataChart("Q3", _dtReason);

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



        public void LoadDataChart(string _qtype, DataTable _dtSource)
        {
            try
            {
                if (_qtype == "Q1")
                {
                    chartMain.DataSource = null;
                    if (_dtSource == null) return;
                    chartMain.DataSource = _dtSource;
                    chartMain.Series[0].ArgumentDataMember = "LABEL_CHART";
                    chartMain.Series[0].ValueDataMembers.AddRange(new string[] { "2" });
                    chartMain.Series[1].ArgumentDataMember = "LABEL_CHART";
                    chartMain.Series[1].ValueDataMembers.AddRange(new string[] { "3" });
                }
                else if (_qtype == "Q2")
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
                    if (_dtSource == null) return;

                    for (int i = 0; i < _dtSource.Rows.Count; i++)
                    {
                        string Argument = _dtSource.Rows[i]["MODEL_NAME"].ToString();
                        double Value1, Value2;
                        double.TryParse(_dtSource.Rows[i]["OSD"].ToString(), out Value1);
                        double.TryParse(_dtSource.Rows[i]["OSD_RATE"].ToString(), out Value2);

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
                    chartTitle.Font = new Font("Times New Roman", 26F, FontStyle.Bold ^ FontStyle.Italic);
                    chartModel.Titles.Add(chartTitle);

                    if (_dtSource.Rows.Count <= 6)
                    {
                        ((XYDiagram)chartModel.Diagram).AxisX.Label.Font = new Font("Calibri", 14, FontStyle.Regular);
                    }
                    else
                    {
                        ((XYDiagram)chartModel.Diagram).AxisX.Label.Font = new Font("Calibri", 11, FontStyle.Regular);
                    }
                    ((XYDiagram)chartModel.Diagram).AxisX.Label.Staggered = false;
                }
                else if (_qtype == "Q3")
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                    chartTitle.Text = _div_nm + " By Reason";
                    chartReason.Titles.Clear();
                    chartModel.DataSource = null;
                    if (_dtSource == null) return;
                    chartReason.DataSource = _dtSource;
                    chartReason.Series[0].ArgumentDataMember = "DEFECTIVE";
                    chartReason.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });

                    // Define the alignment of the titles.
                    chartTitle.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle.Font = new Font("Times New Roman", 26F, FontStyle.Bold ^ FontStyle.Italic);
                    chartReason.Titles.Add(chartTitle);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region Database
        private DataTable GET_COMBOBOX(string V_P_TYPE, string V_P_CONDITION, string V_P_CONDITION1)
        {
            COM.OraDB oradb = new COM.OraDB();
            DataSet dtset;
            oradb.ShowErr = false;
            try
            {
                string process_name = "PKG_SMT_QUALITY_COCKPIT_06.SP_CBO_EXTERNAL_OSD";
                oradb.ReDim_Parameter(4);
                oradb.Process_Name = process_name;

                oradb.Parameter_Name[0] = "V_P_TYPE";
                oradb.Parameter_Name[1] = "V_P_CONDITION";
                oradb.Parameter_Name[2] = "V_P_CONDITION1";
                oradb.Parameter_Name[3] = "OUT_CURSOR";

                oradb.Parameter_Type[0] = (int)OracleType.VarChar;
                oradb.Parameter_Type[1] = (int)OracleType.VarChar;
                oradb.Parameter_Type[2] = (int)OracleType.VarChar;
                oradb.Parameter_Type[3] = (int)OracleType.Cursor;

                oradb.Parameter_Values[0] = V_P_TYPE;
                oradb.Parameter_Values[1] = V_P_CONDITION;
                oradb.Parameter_Values[2] = V_P_CONDITION1;
                oradb.Parameter_Values[3] = "";

                oradb.Add_Select_Parameter(true);
                dtset = oradb.Exe_Select_Procedure();
                if (dtset == null) return null;
                return dtset.Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetExternalOSD(string ARG_QTYPE,string ARG_DATE, string V_P_PLANT, string ARG_LINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ShowErr = true;
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_06.SP_GET_EXTERNAL_OSD";

                MyOraDB.ReDim_Parameter(7);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE";
                MyOraDB.Parameter_Name[2] = "V_P_PLANT";
                MyOraDB.Parameter_Name[3] = "V_P_LINE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR2";
                MyOraDB.Parameter_Name[6] = "OUT_CURSOR3";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_DATE;
                MyOraDB.Parameter_Values[2] = V_P_PLANT;
                MyOraDB.Parameter_Values[3] = ARG_LINE;
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";
                MyOraDB.Parameter_Values[6] = "";

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

        #endregion DB

        #region Events
        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region [Start Control Event]

        private void cboFactory_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            SetData();
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
                        if (_dtArea.Rows[iRow]["LABEL_CHART"].ToString() == _div_nm)
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

        private void gvwMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue == DBNull.Value) return;
            if (e.Column.ColumnHandle >= _start_column && gvwMain.GetRowCellValue(e.RowHandle, "DIV").ToString().Equals("3"))
            {
                e.DisplayText = e.CellValue.ToString() + "%";
            }
        }

        private void gvwMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            try
            {
                if (grdMain.DataSource == null || gvwMain.RowCount < 1) return;

                if (gvwMain.GetRowCellValue(e.RowHandle, "DIV").ToString().ToUpper().Contains("3"))
                {
                    e.Appearance.BackColor = Color.LightYellow;

                    if (e.Column.FieldName != "O_TYPE")
                    {
                        e.Appearance.ForeColor = Color.Blue;
                        e.Appearance.Font = new Font("Calibri", 16, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion [End Control Event]
    }
}
