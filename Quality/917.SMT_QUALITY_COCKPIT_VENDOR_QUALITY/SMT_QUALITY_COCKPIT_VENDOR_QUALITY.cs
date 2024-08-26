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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_VENDOR_QUALITY : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Vendor Quality";
        int _time = 0, _start_column = 4;
        string _vendor = "TOT";
        DataTable _dtVendor = null;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_VENDOR_QUALITY()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        

        
        private void SMT_QUALITY_COCKPIT_VENDOR_QUALITY_Load(object sender, EventArgs e)
        {        
            DateTime dt = DateTime.Now;
            DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            cboDateFr.EditValue = DateTime.Now.AddDays(-7); //fistdate;
           
            cboDateTo.EditValue = DateTime.Now;
            LoadComboBox("C_FACTORY");
        }

        private void SMT_QUALITY_COCKPIT_VENDOR_QUALITY_VisibleChanged(object sender, EventArgs e)
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
            try
            {
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd")) + "\n\r" + string.Format(DateTime.Now.ToString("HH:mm:ss"));
                _time++;
                if (_time >= 30)
                {
                    timer1.Stop();
                    splash.ShowWaitForm();
                    BindingChart();
                    BindingGrid();
                    splash.CloseWaitForm();
                    _time = 0;
                    timer1.Start();
                }
            }
            catch { }
        }

        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }
        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void gvwMain_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band == null) return;
                if (e.Band.ParentBand == null) return;
                if (e.Band.AppearanceHeader.BackColor != Color.Empty)
                    e.Info.AllowColoring = true;
        }
        private void gvwMain_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {

            try
            {
                if (e.RowHandle1 < 0 || gvwMain.RowCount == 0)
                    return;
                e.Merge = false;
                e.Handled = true;

                if (e.Column.FieldName == "VENDER_NAME")
                {
                    string line1 = gvwMain.GetRowCellDisplayText(e.RowHandle1, "VENDER_NAME").Trim();
                    string line2 = gvwMain.GetRowCellDisplayText(e.RowHandle2, "VENDER_NAME").Trim();
                    e.Merge = line1 == line2;
                }
                if (e.Column.FieldName == "COMP_CD")
                {
                    string line1 = gvwMain.GetRowCellDisplayText(e.RowHandle1, "VENDER_NAME").Trim();
                    string line2 = gvwMain.GetRowCellDisplayText(e.RowHandle2, "VENDER_NAME").Trim();
                    string line3 = gvwMain.GetRowCellDisplayText(e.RowHandle1, "COMP_CD").Trim();
                    string line4 = gvwMain.GetRowCellDisplayText(e.RowHandle2, "COMP_CD").Trim();
                    e.Merge = line1 == line2 && line3 == line4;
                }


            }
            catch { }
        }
        private void gvwMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.AbsoluteIndex >= _start_column)
            {
                if (gvwMain.GetRowCellValue(e.RowHandle, gvwMain.Columns["DIV"]).ToString().ToUpper().Equals("REJECT QUANTITY"))
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                //if (e.Column.FieldName.ToString().ToUpper().Contains("SUM"))
                //{
                //    e.Appearance.BackColor = Color.LightYellow;
                //    e.Appearance.ForeColor = Color.Blue;
                //}
                //if (e.Column.FieldName.ToString().ToUpper().Contains("AVG"))
                //{
                //    e.Appearance.BackColor = Color.LightSkyBlue;
                //    e.Appearance.ForeColor = Color.Yellow;
                //}
                if (gvwMain.GetRowCellValue(e.RowHandle, gvwMain.Columns["DIV"]).ToString().ToUpper().Contains("RATE"))
                {
                    e.Appearance.BackColor = Color.Teal;
                    e.Appearance.ForeColor = Color.White;
                }
            }

        }

        private void gvwMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            string cellValue = gvwMain.GetRowCellValue(e.RowHandle, gvwMain.Columns[e.Column.AbsoluteIndex]).ToString();
            if (string.IsNullOrEmpty(cellValue)) return;

            if (gvwMain.GetRowCellValue(e.RowHandle, gvwMain.Columns["DIV"]).ToString().ToUpper().Contains("RATE"))
            {
                if (e.Column.AbsoluteIndex >= _start_column)
                {
                    e.DisplayText = e.CellValue.ToString() + "%";
                }

            }
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (gvwMain.DataRowCount > 0)
            {
                gvwMain.TopRowIndex = (int)(
                            (gvwMain.RowCount)
                            *
                            (1 + (1.0 * vScrollBar.LargeChange / vScrollBar.Maximum)) * vScrollBar.Value / vScrollBar.Maximum
                            );
            }
        }

        private void cboDateFr_EditValueChanged(object sender, EventArgs e)
        {
            _time = 30;
        }

        private void cboFactory_SelectedValueChanged(object sender, EventArgs e)
        {
            _time = 30;
        }
        private async void chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chart.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                string strdate = cboDateFr.DateTime.ToString("yyyyMMdd");
                string strdateto = cboDateTo.DateTime.ToString("yyyyMMdd");
                string sYM = "";
                if (point != null)
                {
                    sYM = point.Argument;

                    for (int iRow = 0; iRow < _dtVendor.Rows.Count; iRow++)
                    {
                        if (_dtVendor.Rows[iRow]["VENDER_NAME"].ToString() == sYM)
                        {
                            _vendor = _dtVendor.Rows[iRow]["VENDER_CD"].ToString();
                        }
                    }
                }
                else
                {
                    if (hit.AxisLabelItem == null)
                    {
                        _vendor = "TOT";
                    }
                    else
                    {
                        sYM = hit.AxisLabelItem.AxisValue.ToString();
                        for (int iRow = 0; iRow < _dtVendor.Rows.Count; iRow++)
                        {
                            if (_dtVendor.Rows[iRow]["VENDER_NAME"].ToString() == sYM)
                            {
                                _vendor = _dtVendor.Rows[iRow]["VENDER_CD"].ToString();
                            }
                        }
                    }

                }

                SMT_QUALITY_COCKPIT_VENDOR_QUALITY_POP view = new SMT_QUALITY_COCKPIT_VENDOR_QUALITY_POP(strdate, strdateto,cboFactory.SelectedValue.ToString(), _vendor);
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================
        public void LoadComboBox(string _type, string _default_val = "")
        {
            try
            {
                switch (_type)
                {
                    case "C_FACTORY":
                        DataTable dtFactory = DataSelect(_type, "", "", "");
                        if (dtFactory != null && dtFactory.Rows.Count > 0)
                        {

                            cboFactory.DataSource = dtFactory;
                            cboFactory.DisplayMember = "NAME";
                            cboFactory.ValueMember = "CODE";
                            //////Set current location
                            if (!string.IsNullOrEmpty(_default_val))
                            {
                                cboFactory.SelectedValue = _default_val;
                            }

                        }
                        break;
                }
            }
            catch { }
        }
        private void BindingChart()
        {
            DataTable dtSource = null;
            try
            {
                chart.DataSource = null;
                DataTable dtf = DataSelect("Q_CHART", cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd"), cboFactory.SelectedValue.ToString());

                if (dtf != null && dtf.Rows.Count > 0)
                {
                    chart.DataSource = dtf;
                    chart.Series[0].ArgumentDataMember = "VENDER_NAME";
                    chart.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                }
                chart.RuntimeHitTesting = true;
            }
            catch
            {
            }
            finally
            {
            }
        }
        private void BindingGrid()
        {
            gvwMain.Bands.Clear();
            gvwMain.Columns.Clear();
            grdMain.DataSource = null;
            DataTable dtSource = null;
            try
            {
                DataTable dtf = DataSelect("Q_GRID", cboDateFr.DateTime.ToString("yyyyMMdd"), cboDateTo.DateTime.ToString("yyyyMMdd"), cboFactory.SelectedValue.ToString());

                if (dtf != null && dtf.Rows.Count > 0)
                {
                    if (createGrid(dtf, grdMain, gvwMain))
                    {
                        dtSource = GetDataTable(gvwMain);
                        dtSource.Rows.Clear();
                        if (bindingData(dtSource, dtf, _start_column))
                        {
                            DataTable dttmp = dtSource.Copy();
                            dtSource.Rows.RemoveAt(0);
                            dtSource.Rows.RemoveAt(0);
                            dtSource.Rows.RemoveAt(0);
                            dtSource.Rows.RemoveAt(0);
                            grdMain.DataSource = dtSource;
                            Set_Format_Grid();
                            for (int i = 0; i < gvwMain.Columns.Count; i++)
                            {
                                BandedGridColumn col = gvwMain.Columns[i];
                                if (col.FieldName.Contains("VENDER_NAME"))
                                {
                                    col.OwnerBand.Caption = "G-Total";
                                }
                                if (i >= _start_column)
                                {
                                    col.OwnerBand.ParentBand.ParentBand.ParentBand.Caption = dttmp.Rows[0][i].ToString() == "" ? "" : double.Parse(dttmp.Rows[0][i].ToString()).ToString("#,0.#");
                                    col.OwnerBand.ParentBand.ParentBand.Caption = dttmp.Rows[1][i].ToString() == "" ? "" : double.Parse(dttmp.Rows[1][i].ToString()).ToString("#,0.#");
                                    col.OwnerBand.ParentBand.Caption = dttmp.Rows[2][i].ToString() == "" ? "" : double.Parse(dttmp.Rows[2][i].ToString()).ToString("#,0.#");
                                    col.OwnerBand.Caption = dttmp.Rows[3][i].ToString() == "" ? "" : string.Concat(double.Parse(dttmp.Rows[3][i].ToString()).ToString("#,0.#"), "%");

                                    col.OwnerBand.ParentBand.ParentBand.ParentBand.AppearanceHeader.TextOptions.HAlignment = i < _start_column ? HorzAlignment.Far : HorzAlignment.Far;
                                    col.OwnerBand.ParentBand.ParentBand.AppearanceHeader.TextOptions.HAlignment = i < _start_column ? HorzAlignment.Far : HorzAlignment.Far;
                                    col.OwnerBand.ParentBand.AppearanceHeader.TextOptions.HAlignment = i < _start_column ? HorzAlignment.Far : HorzAlignment.Far;
                                    col.OwnerBand.AppearanceHeader.TextOptions.HAlignment = i < _start_column ? HorzAlignment.Far : HorzAlignment.Far;
                                }

                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        private void Set_Format_Grid()
        {
            gvwMain.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < gvwMain.Columns.Count; i++)
            {
                gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                gvwMain.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvwMain.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gvwMain.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                gvwMain.Columns[i].OptionsColumn.ReadOnly = true;
                gvwMain.Columns[i].OptionsColumn.AllowEdit = false;
                gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                gvwMain.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                gvwMain.Columns[i].DisplayFormat.FormatString = "#,0.#";
                gvwMain.Columns[i].Width = 90;
                if (i <= 3)
                {
                    gvwMain.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvwMain.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                }

                gvwMain.Columns[i].OptionsFilter.AllowFilter = false;
                gvwMain.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gvwMain.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Regular);
            }
            gvwMain.TopRowIndex = 0;
            gvwMain.RowHeight = 35;
            gvwMain.Columns["DIV"].Width = 180;
            gvwMain.Columns["COMP_CD"].Width = 180;
            gvwMain.Columns["VENDER_NAME"].Width = 250;

        }
        DataTable GetDataTable(DevExpress.XtraGrid.Views.Grid.GridView view)
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
        private bool createGrid(DataTable dt, GridControl gridControl, BandedGridView gridView)
        {
            try
            {
                gridControl.BeginUpdate();
                gridView.OptionsView.ShowGroupPanel = false;
                gridView.Bands.Clear();
                gridView.Columns.Clear();
                gridControl.DataSource = null;
                gridView.OptionsView.ShowColumnHeaders = false;

                GridBand band = null;
                GridBand bandchlid1 = null;
                GridBand bandchlid2 = null;
                GridBand bandchlid3 = null;
                GridBand bandchlid4 = null;

                BandedGridColumn col = null;
                string distinct_row = "", sCol = "";
                _start_column = int.Parse(dt.Rows[0]["START_COLUMN"].ToString());

                DataView view = new DataView(dt);
                _dtVendor = view.ToTable(true, "VENDER_CD", "VENDER_NAME");
                var distinctValues = dt.AsEnumerable()
                                   .Select(row => new
                                   {
                                       ORD = row.Field<string>("ORD"),
                                       PARENT_CAPTION = row.Field<string>("PARENT_CAPTION"),
                                       COL_NAME = row.Field<string>("COL_NAME")
                                   })
                                   .Distinct().OrderBy(r => r.ORD);
                DataTable dttmp = LINQResultToDataTable(distinctValues);
                for (int i = 0; i < _start_column; i++)
                {
                    if (i == 0)
                    {
                        band = new GridBand() { Caption = dt.Columns[i].ColumnName };
                        gvwMain.Bands.Add(band);
                        //Total Incoming Quantity
                        bandchlid1 = new GridBand() { Caption = "G-Total" };
                        band.Children.Add(bandchlid1);
                        col = new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" };
                        bandchlid1.Columns.Add(col);
                        band.Visible = false;
                        bandchlid1.RowCount = 4;
                        formatBandChild(bandchlid1, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    }
                    if (i == 1)
                    {
                        band = new GridBand() { Caption = "Vendor" };
                        gvwMain.Bands.Add(band);
                        //Total Incoming Quantity
                        bandchlid1 = new GridBand() { Caption = "G-Total" };
                        band.Children.Add(bandchlid1);
                        col = new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" };
                        bandchlid1.Columns.Add(col);
                        band.Visible = true;
                        bandchlid1.RowCount = 4;
                        formatBandChild(bandchlid1, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    }
                    if (i == 2)
                    {
                        band = new GridBand() { Caption = "Component" };
                        gvwMain.Bands.Add(band);
                        //Total Incoming Quantity
                        bandchlid1 = new GridBand() { Caption = " " };
                        band.Children.Add(bandchlid1);
                        col = new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" };
                        bandchlid1.Columns.Add(col);
                        band.Visible = true;
                        formatBandChild(bandchlid1, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    }
                    if (i == 3)
                    {
                        band = new GridBand() { Caption = "Division" };
                        gvwMain.Bands.Add(band);
                        //Total Incoming Quantity
                        bandchlid1 = new GridBand() { Caption = "Incoming Quantity" };
                        band.Children.Add(bandchlid1);
                        //Total Inspection Quantity
                        bandchlid2 = new GridBand() { Caption = "Inspection Quantity" };
                        bandchlid1.Children.Add(bandchlid2);
                        //Total Reject Quantity
                        bandchlid3 = new GridBand() { Caption = "Reject Quantity" };
                        bandchlid2.Children.Add(bandchlid3);
                        //Total Reject Rate
                        bandchlid4 = new GridBand() { Caption = "Reject Rate" };
                        bandchlid3.Children.Add(bandchlid4);
                        col = new BandedGridColumn() { FieldName = dt.Columns[i].ColumnName, Visible = true, Caption = "" };
                        bandchlid4.Columns.Add(col);
                        band.Visible = true;
                        formatBandChild(bandchlid1, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Near);
                        formatBandChild(bandchlid2, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Near);
                        formatBandChild(bandchlid3, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Near);
                        formatBandChild(bandchlid4, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Near);
                    }
                    formatBandChild(band, Color.White, Color.FromArgb(0, 200, 245), 18, FontStyle.Bold, HorzAlignment.Center);
                    band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                }
                int cnt = -1;
                string strParent = "";

                for (int i = 0; i < dttmp.Rows.Count; i++)
                {
                    if (!strParent.Equals(dttmp.Rows[i]["PARENT_CAPTION"].ToString()))
                    {
                        strParent = dttmp.Rows[i]["PARENT_CAPTION"].ToString();
                        band = new GridBand() { Caption = dttmp.Rows[i]["PARENT_CAPTION"].ToString() };
                        gridView.Bands.Add(band);
                        formatBandChild(band, Color.White, Color.FromArgb(0, 200, 245), 16, FontStyle.Bold, HorzAlignment.Center);
                        band.Visible = true;

                    }
                    //Total Incoming Quantity
                    bandchlid1 = new GridBand() { Caption = "" };
                    band.Children.Add(bandchlid1);
                    //Total Inspection Quantity
                    bandchlid2 = new GridBand() { Caption = "" };
                    bandchlid1.Children.Add(bandchlid2);
                    //Total Reject Quantity
                    bandchlid3 = new GridBand() { Caption = "" };
                    bandchlid2.Children.Add(bandchlid3);
                    //Total Reject Rate
                    bandchlid4 = new GridBand() { Caption = "" };
                    bandchlid3.Children.Add(bandchlid4);
                    col = new BandedGridColumn() { FieldName = dttmp.Rows[i]["COL_NAME"].ToString(), Visible = true, Caption = "" };
                    bandchlid4.Columns.Add(col);
                    if (dttmp.Rows[i]["COL_NAME"].ToString().Contains("COL"))
                    {
                        band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    }
                    formatBandChild(bandchlid1, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    formatBandChild(bandchlid2, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    formatBandChild(bandchlid3, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                    formatBandChild(bandchlid4, Color.Black, Color.LightYellow, 14, FontStyle.Bold, HorzAlignment.Center);
                }


                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand gb in gridView.Bands)
                {
                    FormatBand(gb);
                }
                gridControl.EndUpdate();
                return true;
            }
            catch (Exception EX) { return false; }
        }
        private void formatBandChild(GridBand band, Color cFore, Color cBack, int iSizeF, FontStyle fStyle, HorzAlignment hAlign)
        {
            band.AppearanceHeader.BackColor = cBack;
            band.AppearanceHeader.ForeColor = cFore;
            band.AppearanceHeader.Font = new Font("Calibri", iSizeF, fStyle);
            band.AppearanceHeader.TextOptions.HAlignment = hAlign;
        }
        private bool bindingData(DataTable dtSource, DataTable dt, int startcolumn)
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
                    dtSource.Rows[dtSource.Rows.Count - 1][dt.Rows[i]["COL_NAME"].ToString()] = dt.Rows[i]["QTY"];
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            System.Reflection.PropertyInfo[] columns = null;
            if (Linqlist == null) return dt;
            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (System.Reflection.PropertyInfo GetProperty in columns)
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
                foreach (System.Reflection.PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void FormatBand(DevExpress.XtraGrid.Views.BandedGrid.GridBand root)
        {
            root.AppearanceHeader.Options.UseTextOptions = true;
            root.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            //root.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            root.OptionsBand.FixedWidth = true;
            if (root.Children.Count > 0)
            {
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand child in root.Children)
                {
                    FormatBand(child);
                    child.Width = 65;
                }
            }
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (gvwMain.Columns.Count > 0)
            {
                gvwMain.LeftCoord = (int)(
                          (gvwMain.Columns.Count)
                          *
                          (100.0 + (1.0 * hScrollBar.LargeChange / hScrollBar.Maximum)) * hScrollBar.Value / hScrollBar.Maximum
                         );
            }
        }
        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        public DataTable DataSelect(string arg_type, string arg_ymdF, string arg_ymdT, string arg_plant)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            MyOraDB.ShowErr = true;

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

                MyOraDB.Parameter_Values[0] = arg_type;
                MyOraDB.Parameter_Values[1] = arg_ymdF;
                MyOraDB.Parameter_Values[2] = arg_ymdT;
                MyOraDB.Parameter_Values[3] = arg_plant;
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";

                MyOraDB.Add_Select_Parameter(true);
                DataSet retDS = MyOraDB.Exe_Select_Procedure();
                if (retDS == null) return null;

                return retDS.Tables[0];
            }
            catch
            {
                return null;
            }
        }


        #endregion ========= [Procedure Call] ===========================================
        
    }
}
