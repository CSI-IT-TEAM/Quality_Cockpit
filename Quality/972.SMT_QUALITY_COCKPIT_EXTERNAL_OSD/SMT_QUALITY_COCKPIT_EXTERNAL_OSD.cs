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
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using DevExpress.XtraCharts;
//using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_EXTERNAL_OSD : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Long Thanh External OS&&D";
        string _strType = "Q";
        string _plant = ComVar.Var._strValue1;
        string _line = ComVar.Var._strValue2;
        int _time = 0;
        string _CurrentDay = "";
        int _start_column = 0;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_EXTERNAL_OSD()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }

        private void SMT_QUALITY_COCKPIT_EXTERNAL_OSD_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void SMT_QUALITY_COCKPIT_EXTERNAL_OSD_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 0;
                _strType = "Q";
                _plant = ComVar.Var._strValue1;
                _line = ComVar.Var._strValue2;
                timer1.Start();
                SetData(_strType, _plant, _line, false);

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
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd")) + "\n\r" + string.Format(DateTime.Now.ToString("HH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                SetData(_strType, _plant, _line, false);
            }

        }

        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================
        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "973";
        }
        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void cbo_Plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_Plant.SelectedValue.ToString() != null)
                    GET_COMBO_DATA("CLINE", cbo_Plant.SelectedValue.ToString());
                else
                    return;

            }
            catch { }
        }
        private void cbo_Plant_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_Plant.SelectedValue.ToString() != null)
                    GET_COMBO_DATA("CLINE", cbo_Plant.SelectedValue.ToString());

                else
                    return;

            }
            catch { }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetData(_strType, _plant, _line, false);

        }

        private void gvwBase_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.CellValue == DBNull.Value) return;
                if (e.Column.ColumnHandle >= _start_column && gvwBase.GetRowCellValue(e.RowHandle, "DIV").ToString().Equals("3"))
                {
                    e.DisplayText = e.CellValue.ToString() + "%";
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        private void gvwBase_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.ColumnHandle >= _start_column && gvwBase.GetRowCellValue(e.RowHandle, "DIV").ToString().Equals("3"))
            {
                e.Appearance.BackColor = Color.LightCyan;
                e.Appearance.ForeColor = Color.LightCoral;
            }
            if (e.Column.ColumnHandle == gvwBase.Columns.Count - 1)
            {
                e.Appearance.BackColor = Color.LightYellow;
                // e.Appearance.ForeColor = Color.LightCoral;
            }

        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================


        private void LoadForm()
        {
            //GET_COMBO_DATA("CPLANT", "");
            GET_COMBO_DATA("DATE", "");
        }
        private void GET_COMBO_DATA(string type, string plant)
        {
            //if (type == "CPLANT")
            //{
            //    DataTable dt = LOAD_COMBO_V2(type, "","");
            //    cbo_Plant.DataSource = dt;
            //    cbo_Plant.DisplayMember = "NAME";
            //    cbo_Plant.ValueMember = "CODE";
            //    cbo_Plant.SelectedIndex = 0;
            //}
            //if (type == "CLINE")
            //{
            //    plant = cbo_Plant.SelectedValue.ToString();
            //    DataTable dt = LOAD_COMBO_V2(type, plant,"");

            //    cbo_line.DataSource = dt;
            //    cbo_line.DisplayMember = "NAME";
            //    cbo_line.ValueMember = "CODE";
            //    cbo_line.SelectedIndex = 0;
            //}
            if (type == "DATE")
            {
                DataTable dt = Data_Select(type, "", "");
                _CurrentDay = dt.Rows[0]["CURRENTDAY"].ToString();
                dtpDateT.EditValue = dt.Rows[0]["TODAY"];
                dtpDateF.EditValue = dt.Rows[0]["PREV_DAY"];

            }

        }
        private void SetData(string arg_type, string plant, string line, bool arg_load = true)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                splashScreenManager1.ShowWaitForm();
                grdBase.DataSource = null;
                DataTable dtf = Data_Select(arg_type, plant, line);
                if (dtf == null || dtf.Rows.Count == 0) return;
                //DataTable dtSource = ds.Tables[0]; 
                if (dtf.Rows.Count > 0)
                {
                    DataTable dtSource = new DataTable();
                    if (CreateGrid_Day(dtf, grdBase, gvwBase))
                    {
                        dtSource = GetDataTable(gvwBase);
                        if (bindingData_Detail(dtSource, dtf, _start_column))
                        {
                            grdBase.DataSource = dtSource;
                            Set_Format_Grid_Day();
                        }
                    }
                    if (dtf.Select("DIV = 2 AND LOCATE <> 'TOTAL'", "RN1, RN2").Count() > 0)
                    {
                        DataTable dtChart = dtf.Select("DIV = 2 AND LOCATE <> 'TOTAL'", "RN1, RN2").CopyToDataTable();
                        chartControl1.Series.Clear();
                        chartControl1.DataSource = dtChart;
                        chartControl1.SeriesDataMember = "DIV";
                        chartControl1.SeriesTemplate.ArgumentDataMember = "LABEL_CHART";
                        chartControl1.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "QTY" });
                        //  chart.SeriesTemplate.View = new SplineSeriesView();
                        chartControl1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;

                        //((XYDiagram)chart.Diagram).AxisX.Title.Text = "Vendor";
                        //((XYDiagram)chartDay.Diagram).AxisX.Title.Visibility = DefaultBoolean.True;
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "OS&D Quantity (Pairs)";
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.Visibility = DefaultBoolean.True;
                        ((XYDiagram)chartControl1.Diagram).AxisY.Label.Font = new Font("Calibri", 12, FontStyle.Regular);
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.Font = new Font("Calibri", 16, FontStyle.Bold);
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.TextColor = Color.FromArgb(255, 128, 0);

                        ((XYDiagram)chartControl1.Diagram).AxisX.Title.Text = "Division";
                        ((XYDiagram)chartControl1.Diagram).AxisX.Title.Visibility = DefaultBoolean.True;
                        ((XYDiagram)chartControl1.Diagram).AxisX.Label.Font = new Font("Calibri", 12, FontStyle.Regular);
                        ((XYDiagram)chartControl1.Diagram).AxisX.Title.Font = new Font("Calibri", 16, FontStyle.Bold);
                        ((XYDiagram)chartControl1.Diagram).AxisX.Title.TextColor = Color.FromArgb(255, 128, 0);


                        chartControl1.SeriesTemplate.Label.TextPattern = "{V:#,0.#}";
                        chartControl1.SeriesTemplate.Label.Font = new Font("Calibri", 12, FontStyle.Regular);
                    }
                }
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                //this.Cursor = Cursors.Default;
                Debug.WriteLine(ex.ToString());
                //throw;
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
               // this.Cursor = Cursors.Default;
            }

        }
        private bool CreateGrid_Day(DataTable dt, DevExpress.XtraGrid.GridControl gridControl, BandedGridView gridView)
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
                _start_column = int.Parse(dt.Rows[0]["START_COLUMN"].ToString());

                var distinctValues = dt.AsEnumerable()
                                   .Select(row => new
                                   {
                                       RN1 = row.Field<string>("RN1"),
                                       RN2 = row.Field<string>("RN2"),
                                       PARENT_CAPTION = row.Field<string>("PARENT_CAPTION"),
                                       COL_NM = row.Field<string>("COL_NM"),
                                       COL_CAPTION = row.Field<string>("COL_CAPTION")
                                   })
                                   .Distinct().OrderBy(r => r.RN1 + r.RN2);
                DataTable dttmp = LINQResultToDataTable(distinctValues);

                for (int i = 0; i < _start_column; i++)
                {

                    if (i == 0)
                    {
                        band = new GridBand() { Caption = "DIV" };
                        gridView.Bands.Add(band);
                        band.Columns.Add(new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" });
                        band.Visible = false;
                    }
                    else if (i == 1)
                    {
                        band = new GridBand() { Caption = "" };
                        gridView.Bands.Add(band);
                        band.Columns.Add(new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" });
                    }
                    band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
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
            root.AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);
            root.RowCount = 2;
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

        private void Set_Format_Grid_Day()
        {
            gvwBase.BeginUpdate();
            for (int i = 0; i < gvwBase.Columns.Count; i++)
            {
                gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gvwBase.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvwBase.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvwBase.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                gvwBase.Columns[i].OptionsColumn.ReadOnly = false;
                gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
                gvwBase.Columns[i].OptionsFilter.AllowFilter = false;
                gvwBase.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gvwBase.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Regular);
                gvwBase.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);
                if (i < _start_column)
                {
                    gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvwBase.Columns[i].Width = 150;
                }
                else
                {
                    gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gvwBase.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                    gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
                    gvwBase.Columns[i].Width = 120;
                }



            }
            gvwBase.ColumnPanelRowHeight = 50;

            gvwBase.TopRowIndex = 0;
            gvwBase.EndUpdate();
        }


        DataTable GetDataTable(GridView view)
        {
            DataTable dt = new DataTable();
            foreach (DevExpress.XtraGrid.Columns.GridColumn c in view.Columns)
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

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
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
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        private DataTable Data_Select(string argType, string plant, string line)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_EXTERNAL_OSD_LT";//

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                MyOraDB.Parameter_Name[4] = "V_P_LINE";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = argType;
                MyOraDB.Parameter_Values[1] = dtpDateF.DateTime.ToString("yyyyMMdd");
                MyOraDB.Parameter_Values[2] = dtpDateT.DateTime.ToString("yyyyMMdd");
                MyOraDB.Parameter_Values[3] = plant;// 
                MyOraDB.Parameter_Values[4] = line;//cbo_line.SelectedValue == null ? "" : cbo_line.SelectedValue.ToString();
                MyOraDB.Parameter_Values[5] = "";

                MyOraDB.Add_Select_Parameter(true);
                DataSet retDS = MyOraDB.Exe_Select_Procedure();
                if (retDS == null) return null;

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[0];
            }
            catch
            {
                return null;
            }
        }


        #endregion ========= [Procedure Call] ===========================================

    }
}
