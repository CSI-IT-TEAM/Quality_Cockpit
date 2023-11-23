using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class SMT_QUALITY_COCKPIT_REWORK : Form
    {
        
        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Daily Rework";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        bool _isLoad = true;
        int _start_column = 0;

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================
        public SMT_QUALITY_COCKPIT_REWORK()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
            LoadCombo("DATE");
            LoadCombo("COMBO_PLANT");
            LoadCombo("COMBO_LINE");
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                cboPlant.SelectedValue = ComVar.Var._strValue1;
                cboLine.SelectedValue = ComVar.Var._strValue2;
                //chartControl1.Series[0].Points.Clear();
                //chartControl1.Series[1].Points.Clear();
                //grdView.DataSource = null;
                _time = 30;

                timer1.Start();
                _isLoad = false;
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
                splashScreenManager1.ShowWaitForm();
                SetData();
                SetData_Detail(dtpYMD.DateTime.ToString("yyyyMMdd"), dtpYMDT.DateTime.ToString("yyyyMMdd"), cboPlant.SelectedValue.ToString(), cboLine.SelectedValue.ToString());
                splashScreenManager1.CloseWaitForm();
            }
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _time = 30;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
            }
        }
        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombo("COMBO_LINE");
        }
        private void cboLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLine.Text != "" && _isLoad == false)
            {
                _time = 30;
            }
        }
        private void gvwView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName.ToString() == _CurrentDay)
                {
                    //return;
                    Rectangle rect = e.Bounds;
                    // rect.Inflate(new Size(1, 1));
                    // rect.p

                    Brush brush = new SolidBrush(e.Appearance.BackColor);
                    e.Graphics.FillRectangle(brush, rect);
                    Pen pen_horizental = new Pen(Color.Blue, 3F);
                    Pen pen_vertical = new Pen(Color.Blue, 4F);

                    ////draw bottom
                    //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                    //// draw top
                    //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

                    //if (e.RowHandle == 0)
                    //{
                    //    e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                    //}
                    //else 
                    if (e.RowHandle == gvwView.RowCount - 1)
                    {
                        e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                    }
                    // draw right
                    e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);


                    // draw left
                    e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X, rect.Y + rect.Height);

                    string[] ls = e.DisplayText.Split('\n');

                    e.Graphics.DrawString(ls[0], e.Appearance.Font, new SolidBrush(e.Appearance.ForeColor), rect, e.Appearance.GetStringFormat());

                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        private void gvwView_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            if (e.Band == null) return;
            if (e.Band.AppearanceHeader.BackColor != Color.Empty)
                e.Info.AllowColoring = true;
        }
        private void gvwView_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            if (e.Column.FieldName.ToString().Split(new char[] { '\n' })[0] == _CurrentDay)
            {
                Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));

                Brush brush = new SolidBrush(Color.DodgerBlue);
                e.Graphics.FillRectangle(brush, rect);


                string text = e.Column.Caption == "" ? e.Column.FieldName.ToString().Split(new char[] { '\n' })[0] : e.Column.Caption.Split(new char[] { '\n' })[0];

                //  if (e.Column.Caption == "") return;
                //  string text = e.Column.FieldName.ToString().Split(new char[] { '\n' })[0];
                e.Graphics.DrawString(text, e.Appearance.Font, new SolidBrush(Color.White), rect, e.Appearance.GetStringFormat());

                e.Handled = true;
            }
        }

        private void gvwView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.CellValue == null) return;

            if (e.Column.AbsoluteIndex < 2) return;

            string strdate = gvwView.Columns[e.Column.ColumnHandle].FieldName.ToString();
            string strplant = cboPlant.SelectedValue.ToString();
            string strline = cboLine.SelectedValue.ToString();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SetData_Detail(strdate, strdate, strplant, strline);
                SMT_QUALITY_COCKPIT_REWORK_POP view = new SMT_QUALITY_COCKPIT_REWORK_POP(strdate, strplant, strline);
                view.ShowDialog();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void gvwView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            double temp = 0.0;

            if (e.Column.AbsoluteIndex >= 1 && e.CellValue != null)
            {
                if (gvwView.GetRowCellDisplayText(e.RowHandle, gvwView.Columns["DIV"]).ToString().ToUpper().Contains("RATE"))
                {
                    double.TryParse(gvwView.GetRowCellDisplayText(gvwView.RowCount - 1, gvwView.Columns[e.Column.ColumnHandle]).ToString(), out temp); //out
                    if (temp >= 15)
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 55, 30);
                    }
                    else if (temp <= 9)
                    {
                        e.Appearance.BackColor = Color.FromArgb(20, 200, 110);
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 180, 15);
                    }
                    e.Appearance.ForeColor = Color.White;
                }
                if (e.Column.FieldName.Contains("00000000") && e.RowHandle != gvwView.RowCount - 1)
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }

        }
        private void gvwDetail_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //try
            //{

            //    if (e.Column.Caption== "Total")
            //    {

            //        Rectangle rect = e.Bounds;


            //        Brush brush = new SolidBrush(e.Appearance.BackColor);
            //        e.Graphics.FillRectangle(brush, rect);
            //        Pen pen_horizental = new Pen(Color.Blue, 3F);
            //        Pen pen_vertical = new Pen(Color.Blue, 4F);


            //        if (e.RowHandle == gvwDetail.RowCount - 1)
            //        {
            //            e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            //        }
            //        // draw right
            //        e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);


            //        // draw left
            //        e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X, rect.Y + rect.Height);

            //        string ls = e.DisplayText;
            //       // string ls = e.Column.FieldName.ToString();

            //          e.Graphics.DrawString(ls, e.Appearance.Font, new SolidBrush(e.Appearance.ForeColor), rect, e.Appearance.GetStringFormat());

            //        e.Handled = true;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //}
        }

        private void gvwDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.Caption == "Total")
            //{
            //    e.Appearance.ForeColor = Color.Blue;

            //}
        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================

        private void LoadCombo(string arg_type)
        {
            try
            {
                if (arg_type == "DATE")
                {
                    DataSet dtset = sbGetRework(arg_type, "", "","","");
                    DataTable dt = dtset.Tables[0];

                    dtpYMD.EditValue = dt.Rows[0]["PREV_DAY"];
                    dtpYMDT.EditValue = dt.Rows[0]["TODAY"];
                }
                if (arg_type == "COMBO_PLANT")
                {
                    DataSet dtset = sbGetRework(arg_type, "", "", "", "");
                    DataTable dt = dtset.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cboPlant.DataSource = dt;
                        cboPlant.DisplayMember = "NAME";
                        cboPlant.ValueMember = "CODE";
                    }
                }
                if (arg_type == "COMBO_LINE")
                {
                    DataSet dtset = sbGetRework(arg_type,"","", cboPlant.SelectedValue.ToString().Trim(), "");
                    DataTable dt = dtset.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cboLine.DataSource = dt;
                        cboLine.DisplayMember = "NAME";
                        cboLine.ValueMember = "CODE";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { }
        }

        private void SetChart(DataTable argDtChart)
        {
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["REW_QTY"]));
                chartControl1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["YMD"].ToString(), argDtChart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate >= 15)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(250,55,30);
                }
                else if (rate >= 9.1 && rate < 15)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(255,180,15);
                }
                else
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(20,200,110);
                }
            }
        }


        private void SetData()
        {
            try
            {
                gvwView.BeginUpdate();

                DataTable dtSource = new DataTable();
                grdView.Refresh();
                gvwView.Columns.Clear();

                string YMDF = dtpYMD.DateTime.ToString("yyyyMMdd");
                string YMDT = dtpYMDT.DateTime.ToString("yyyyMMdd");
                string PLANT_CD = cboPlant.SelectedValue.ToString().Trim();
                string LINE_CD = cboLine.SelectedValue.ToString().Trim();

               
                DataSet dsData = sbGetRework("Q", YMDF, YMDT, PLANT_CD, LINE_CD);

                if (dsData == null) return;
                DataTable dt = dsData.Tables[0];
                DataTable dtChart = dsData.Tables[1];
                DataTable dtBand = dsData.Tables[2];
                if (Create_Grid(dtBand, grdView, gvwView))
                {
                    dtSource = GetDataTable(gvwView);
                    dtSource.Rows.Clear();
                    if (bindingDataSource_detail(dtSource, dt))
                    {
                        grdView.DataSource = dtSource;

                        gvwView.OptionsView.ColumnAutoWidth = false;
                        

                        for (int i = 0; i < gvwView.Columns.Count; i++)
                        {
                            gvwView.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Regular);
                            gvwView.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);


                            gvwView.Columns[i].AppearanceCell.Options.UseTextOptions = true;

                            if (i == 0)
                            {
                                gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                                gvwView.Columns[i].Width = 150;
                            }
                            else
                            {
                                gvwView.Columns[i].Width = 100;
                                gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            }


                            gvwView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                            gvwView.Columns[i].OptionsFilter.AllowFilter = false;
                            gvwView.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                            gvwView.Columns[i].OptionsColumn.AllowEdit = false;
                            gvwView.Columns[i].OptionsColumn.ReadOnly = true;
                            gvwView.ColumnPanelRowHeight = 50;
                            gvwView.RowHeight = 50;
                            gvwView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                            gvwView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                            gvwView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvwView.Columns[i].DisplayFormat.FormatString = "#,###0.##";
                        }
                    }
                }
                //if (dtGrid != null && dtGrid.Rows.Count > 2)
                //{
                //    //TINH TOTAL
                //    int iQty;
                //    for (int i = 0; i <= dtGrid.Rows.Count - 2; i++)
                //    {
                //        for (int j = 2; j <= dtGrid.Columns.Count - 1; j++)
                //        {
                //            int.TryParse(dtGrid.Rows[i][j].ToString(), out iQty);
                //            total = total + iQty;
                //        }
                //        dtGrid.Rows[i]["TOTAL"] = total;
                //        total = 0;
                //        dtGrid.Rows[i]["ITEM"] = dtGrid.Rows[i]["ITEM"].ToString() + "(Prs)";
                //    }

                //    if (int.Parse(dtGrid.Rows[0]["TOTAL"].ToString()) > 0)
                //    {
                //        PER = (double.Parse(dtGrid.Rows[1]["TOTAL"].ToString()) / double.Parse(dtGrid.Rows[0]["TOTAL"].ToString())) * 100;
                //        dtGrid.Rows[2]["TOTAL"] = Math.Round(PER, 2);
                //    }
                //    else
                //    {
                //        dtGrid.Rows[2]["TOTAL"] = 0;
                //    }
                //    dtGrid.Rows[2]["ITEM"] = dtGrid.Rows[2]["ITEM"].ToString() + "(%)";
                //}

                //grdView.DataSource = dtGrid;
                //gvwView.Appearance.Row.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Regular);

                //gvwView.Columns[0].Caption = " ";
                //gvwView.Columns[1].Caption = "Total";

               


                // gvwView.BestFitColumns();


                SetChart(dtChart);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                gvwView.EndUpdate();
            }
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
        private void FormatBand(DevExpress.XtraGrid.Views.BandedGrid.GridBand root)
        {
            root.AppearanceHeader.Options.UseTextOptions = true;
            root.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            root.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //root.AppearanceHeader.Font = new Font("Calibri", 14, FontStyle.Bold);
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
        private bool bindingDataSource_detail(DataTable dtSource, DataTable dt)
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

                    for (int col = 0; col < _start_column; col++)
                    {
                        dtSource.Rows[dtSource.Rows.Count - 1][dt.Columns[col].ColumnName] = dt.Rows[i][dt.Columns[col].ColumnName].ToString();
                    }
                    dtSource.Rows[dtSource.Rows.Count - 1][dt.Rows[i]["YMD"].ToString()] = dt.Rows[i]["QTY"];
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool Create_Grid(DataTable dt, GridControl grd, BandedGridView gvw)
        {
            try
            {
                grd.Hide();
                gvw.BeginDataUpdate();
                try
                {
                    gvw.OptionsView.ShowGroupPanel = false;
                    gvw.OptionsView.AllowCellMerge = true;
                    gvw.OptionsCustomization.AllowBandMoving = false;
                    gvw.Bands.Clear();
                    gvw.Columns.Clear();

                    gvw.OptionsView.ShowColumnHeaders = false;
                    GridBand band = null;
                    BandedGridColumn col = null;

                    if (dt.Rows.Count > 0)
                    {
                        _start_column = int.Parse(dt.Rows[0]["START_COLUMN"].ToString());

                    }
                    var distinctValues = dt.AsEnumerable()
                                  .Select(row => new
                                  {
                                      COL_NM = row.Field<string>("COL_NM"),
                                      BAND_NM = row.Field<string>("BAND_NM"),
                                      CUR_DATE = row.Field<string>("CUR_DATE"),
                                  })
                                  .Distinct().OrderBy(r => r.COL_NM);
                    DataTable dttmp = LINQResultToDataTable(distinctValues);
                    for (int i = 0; i <= _start_column; i++)
                    {

                        // gvw.Bands.Add(band);

                        if (i == 0)
                        {
                            band = new GridBand() { Caption = "Date", Visible = true };
                            gvw.Bands.Add(band);
                            band.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                            col = new BandedGridColumn() { FieldName = "DIV", Visible = true, Caption = "" };
                            band.Columns.Add(col);
                            band.AppearanceHeader.ForeColor = Color.White;
                            band.AppearanceHeader.BackColor = Color.DodgerBlue;
                            band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                            col.Width = 200;
                        }
                        else
                        {
                            band = new GridBand() { Caption = "Total" , Visible = true };
                            gvw.Bands.Add(band);
                            band.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                            col = new BandedGridColumn() { FieldName = "00000000", Visible = true, Caption = "" };
                            band.Columns.Add(col);
                            band.AppearanceHeader.ForeColor = Color.White;
                            band.AppearanceHeader.BackColor = Color.DodgerBlue;
                            band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                        }
                    }
                    string strParent = "";
                    for (int i = 0; i < dttmp.Rows.Count; i++)
                    {
                        if (string.Compare(dttmp.Rows[i]["COL_NM"].ToString(), dttmp.Rows[i]["CUR_DATE"].ToString()) == 0)
                        {
                            if (!strParent.Equals(dttmp.Rows[i]["BAND_NM"].ToString()))
                            {
                                strParent = dttmp.Rows[i]["BAND_NM"].ToString();
                                band = new GridBand() { Caption = dttmp.Rows[i]["BAND_NM"].ToString() };
                                gvw.Bands.Add(band);
                                band.Visible = true;
                                band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                                band.AppearanceHeader.ForeColor = Color.White;
                                band.AppearanceHeader.BackColor = Color.Orange;
                            }
                            band.Columns.Add(new BandedGridColumn() { FieldName = dttmp.Rows[i]["COL_NM"].ToString(), Visible = true, Caption = "" });

                        }
                        else
                        {
                            if (!strParent.Equals(dttmp.Rows[i]["BAND_NM"].ToString()))
                            {
                                strParent = dttmp.Rows[i]["BAND_NM"].ToString();
                                band = new GridBand() { Caption = dttmp.Rows[i]["BAND_NM"].ToString() };
                                gvw.Bands.Add(band);
                                band.Visible = true;
                                band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                                band.AppearanceHeader.ForeColor = Color.White;
                                band.AppearanceHeader.BackColor = Color.DodgerBlue;
                            }
                            band.Columns.Add(new BandedGridColumn() { FieldName = dttmp.Rows[i]["COL_NM"].ToString(), Visible = true, Caption = "" });
                        }

                    }
                    foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand gb in gvw.Bands)
                    {
                        FormatBand(gb);
                    }

                }
                catch
                {

                }
                grd.Show();
                gvw.EndDataUpdate();
                gvw.ExpandAllGroups();
            }
            catch
            {
                return false;
            }
            return true;
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


        private void SetData_Detail(string DATEF, string DATET, string plant, string line)
        {
            try
            {
                gvwDetail.BeginUpdate();

                DataSet dsData = sbGetRework("Q_DETAIL", DATEF, DATET, plant, line);

                DataTable dtGrid = dsData.Tables[0];

                grdDetail.DataSource = dtGrid;

                for (int i = 0; i < gvwDetail.Columns.Count; i++)
                {
                    gvwDetail.Columns[i].AppearanceCell.Options.UseTextOptions = true;

                    gvwDetail.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    gvwDetail.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gvwDetail.Columns[i].OptionsFilter.AllowFilter = false;
                    gvwDetail.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    gvwDetail.Columns[i].OptionsColumn.AllowEdit = false;
                    gvwDetail.Columns[i].OptionsColumn.ReadOnly = true;

                    gvwDetail.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    gvwDetail.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gvwDetail.Columns[i].AppearanceCell.Font = new Font("Calibri", 14, FontStyle.Regular);
                    gvwDetail.Columns[i].AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);
                    gvwDetail.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvwDetail.Columns[i].DisplayFormat.FormatString = "#,###.##";
                }
                gvwDetail.ColumnPanelRowHeight = 80;
                gvwDetail.BandPanelRowHeight = 50;
                gvwDetail.RowHeight = 50;


                // gvwView.BestFitColumns();


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                gvwDetail.EndUpdate();
            }
        }

        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        //private DataTable Data_Select_Combo(string argType, string argPlant, string argLine )
        //{           
        //    COM.OraDB MyOraDB = new COM.OraDB();
        //    DataSet ds_ret;
        //    try
        //    {
        //        string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_SET_COMBO";

        //        MyOraDB.ReDim_Parameter(4);
        //        MyOraDB.Process_Name = process_name;

        //        MyOraDB.Parameter_Name[0] = "V_P_TYPE";
        //        MyOraDB.Parameter_Name[1] = "V_P_PLANT";
        //        MyOraDB.Parameter_Name[2] = "V_P_LINE";
        //        MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

        //        MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
        //        MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
        //        MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
        //        MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

        //        MyOraDB.Parameter_Values[0] = argType;
        //        MyOraDB.Parameter_Values[1] = argPlant;
        //        MyOraDB.Parameter_Values[2] = argLine;
        //        MyOraDB.Parameter_Values[3] = "";

        //        MyOraDB.Add_Select_Parameter(true);
        //        ds_ret = MyOraDB.Exe_Select_Procedure();

        //        if (ds_ret == null) return null;
        //        return ds_ret.Tables[process_name];
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

        public DataSet sbGetRework(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
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




        //public async Task<DataSet> sbGetRework_Detail(string ARG_QTYPE, string ARG_YMD,  string ARG_PLANT, string ARG_LINE)
        //{
        //    return await Task.Run(() =>
        //    {
        //        COM.OraDB MyOraDB = new COM.OraDB();
        //        DataSet ds_ret;
        //        try
        //        {
        //            string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_GET_REWORK_DETAIL";

        //            MyOraDB.ReDim_Parameter(5);
        //            MyOraDB.Process_Name = process_name;

        //            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
        //            MyOraDB.Parameter_Name[1] = "V_P_DATE";                    
        //            MyOraDB.Parameter_Name[2] = "V_P_PLANT";
        //            MyOraDB.Parameter_Name[3] = "V_P_LINE";
        //            MyOraDB.Parameter_Name[4] = "OUT_CURSOR";


        //            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;                    
        //            MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;


        //            MyOraDB.Parameter_Values[0] = ARG_QTYPE;
        //            MyOraDB.Parameter_Values[1] = ARG_YMD;                    
        //            MyOraDB.Parameter_Values[2] = ARG_PLANT;
        //            MyOraDB.Parameter_Values[3] = ARG_LINE;
        //            MyOraDB.Parameter_Values[4] = "";


        //            MyOraDB.Add_Select_Parameter(true);
        //            ds_ret = MyOraDB.Exe_Select_Procedure();

        //            if (ds_ret == null) return null;
        //            return ds_ret;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    });
        //}
        //public async Task<DataTable> sbGetRework_Chart(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        //{
        //    return await Task.Run(() => {
        //        COM.OraDB MyOraDB = new COM.OraDB();
        //        DataSet ds_ret;
        //        try
        //        {
        //            string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_GET_REWORK_CHART";

        //            MyOraDB.ReDim_Parameter(6);
        //            MyOraDB.Process_Name = process_name;

        //            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
        //            MyOraDB.Parameter_Name[1] = "V_P_DATEF";
        //            MyOraDB.Parameter_Name[2] = "V_P_DATET";
        //            MyOraDB.Parameter_Name[3] = "V_P_PLANT";
        //            MyOraDB.Parameter_Name[4] = "V_P_LINE";
        //            MyOraDB.Parameter_Name[5] = "OUT_CURSOR";

        //            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
        //            MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

        //            MyOraDB.Parameter_Values[0] = ARG_QTYPE;
        //            MyOraDB.Parameter_Values[1] = ARG_YMDF;
        //            MyOraDB.Parameter_Values[2] = ARG_YMDT;
        //            MyOraDB.Parameter_Values[3] = ARG_PLANT;
        //            MyOraDB.Parameter_Values[4] = ARG_LINE;
        //            MyOraDB.Parameter_Values[5] = "";

        //            MyOraDB.Add_Select_Parameter(true);
        //            ds_ret = MyOraDB.Exe_Select_Procedure();

        //            if (ds_ret == null) return null;
        //            return ds_ret.Tables[process_name];
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    });
        //}

        #endregion ========= [Procedure Call] ===========================================

    }
}
