﻿using System;
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

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_INTERNAL_OSD : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Internal OS&&D";
        string _strType = "Q";
        string _plant = ComVar.Var._strValue1;  // "";// 
        string _line = ComVar.Var._strValue2;  //"";//
        int _time = 0;
        string _CurrentDay = "";

        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_INTERNAL_OSD()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }

        private void SMT_QUALITY_COCKPIT_INTERNAL_OSD_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void SMT_QUALITY_COCKPIT_INTERNAL_OSD_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _strType = "Q";

                cbo_Plant.SelectedValue = ComVar.Var._strValue1;
                cbo_line.SelectedValue = ComVar.Var._strValue2;

                //_plant = cbo_Plant.SelectedValue.ToString(); //
                //_line = cbo_line.SelectedValue.ToString(); //  

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
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd")) + "\n\r" + string.Format(DateTime.Now.ToString("HH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                SetData(_strType, cbo_Plant.SelectedValue.ToString(), cbo_line.SelectedValue.ToString(), false);
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


        private void rdByDay_CheckedChanged(object sender, EventArgs e)
        {
            pnBody2.Visible = true;
            pnBody1.Visible = false;
            cmdDay.Visible = false;
            cmdWeek.Visible = false;
            btn_Search.Visible = false;
        }
        private void cbo_Plant_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_Plant.SelectedValue.ToString() != null)
                    GET_COMBO_DATA("COMBO_LINE", cbo_Plant.SelectedValue.ToString());

                else
                    return;

            }
            catch { }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _time = 30;
        }

        private void gvwBase_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            try
            {
                if (e.Column.AbsoluteIndex > 0)
                {
                    if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("REPLENISHMENT RATE (%)") ||
                        gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("OS&D RATE (%)"))
                    {
                        if (e.CellValue == DBNull.Value) return;
                        e.DisplayText = double.Parse(e.CellValue.ToString()).ToString("0.0#");
                    }

                    if (gvwBase.Columns[e.Column.FieldName].OwnerBand.Caption == _CurrentDay)
                    {
                        //return;
                        Rectangle rect = e.Bounds;
                        rect.Inflate(new Size(1, 1));

                        Brush brush = new SolidBrush(e.Appearance.BackColor);
                        e.Graphics.FillRectangle(brush, rect);
                        Pen pen_horizental = new Pen(Color.Orange, 3F);
                        Pen pen_vertical = new Pen(Color.Orange, 4F);

                        ////draw bottom
                        //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                        //// draw top
                        //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

                        //if (e.RowHandle == 0)
                        //{
                        //    e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                        //}
                        //else 
                        if (e.RowHandle == gvwBase.RowCount - 1)
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

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        private void gvwBase_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.CellValue == DBNull.Value) return;
            if (e.Column.AbsoluteIndex > 0)
            {
                if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("OS&D RATE (%)"))
                {
                    double rate = double.Parse(e.CellValue.ToString());
                    e.Appearance.ForeColor = Color.White;
                    if (rate <= 0.3)
                    {
                        e.Appearance.BackColor = Color.FromArgb(20,200,110);   
                    }

                    else if (rate > 0.4)
                    {                       
                        e.Appearance.BackColor = Color.FromArgb(250, 55, 30);
                    }
                    else
                        e.Appearance.BackColor = Color.FromArgb(255, 180, 15);

                }
                if (e.Column.FieldName.ToUpper().Contains("TOTAL") && e.RowHandle != gvwBase.RowCount - 1)
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.Appearance.Font = new System.Drawing.Font("Calibri", 14F, FontStyle.Bold);
                }
            }

        }
        private void gvwBase_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

            if (e.CellValue == null) return;
            if (e.Column.AbsoluteIndex < 2) return;

            string strdate = gvwBase.Columns[e.Column.ColumnHandle - 1].FieldName.ToString();
            string strplant = cbo_Plant.SelectedValue.ToString();
            string strline = cbo_line.SelectedValue.ToString();

            try
            {
                if (e.Column.ColumnHandle > 2)
                {
                    if (gvwBase.GetRowCellValue(e.RowHandle, "ITEM").ToString().ToUpper().Equals("OS&D RATE (%)"))
                    {
                        double rate = double.Parse(e.CellValue.ToString());
                        string date = e.Column.FieldName.ToString();
                        string plant = cbo_Plant.SelectedValue.ToString();
                        string line = cbo_line.SelectedValue.ToString();
                        if (rate > 0)
                        {
                            using (SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP view1 = new SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP(date, plant, line))
                            {

                                view1.ShowDialog();
                            }
                        }
                    }

                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        using (SMT_QUALITY_COCKPIT_INTERNAL_OSD_POP view = new SMT_QUALITY_COCKPIT_INTERNAL_OSD_POP(strdate, strplant, strline))
                        {
                            view.ShowDialog();
                        }

                    }
                }
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

        private void gvwBase_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            try
            {
                if (e.Band == null) return;
                bool boBorder = false;


                if (e.Band.HasChildren)
                {
                    if (e.Band.Children[0].Columns.Count > 0)
                        if (e.Band.Children[0].Columns[0].Caption == _CurrentDay)
                        {
                            boBorder = true;

                        }
                }
                else
                {
                    if (e.Band.Columns.Count > 0)
                        if (e.Band.Columns[0].Caption == _CurrentDay)
                        {
                            boBorder = true;

                        }
                }

                if (boBorder)
                {
                    Rectangle rect = e.Bounds;
                    rect.Inflate(new Size(1, 1));

                    Brush brush = new SolidBrush(Color.Orange);
                    e.Graphics.FillRectangle(brush, rect);
                    Pen pen_horizental = new Pen(Color.Orange, 3F);
                    Pen pen_vertical = new Pen(Color.Orange, 4F);


                    // e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
                    // draw right
                    // e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width - 2, rect.Y, rect.X + rect.Width - 2, rect.Y + rect.Height);


                    // draw left
                    //  e.Graphics.DrawLine(pen_horizental, rect.X + 1, rect.Y, rect.X + 1, rect.Y + rect.Height);


                    e.Graphics.DrawString(_CurrentDay, e.Appearance.Font, new SolidBrush(Color.White), rect, e.Appearance.GetStringFormat());
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
        }
        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================
        private void LoadForm()
        {

            GET_COMBO_DATA("COMBO_PLANT", "");
            GET_COMBO_DATA("DATE", "");
        }
        private void GET_COMBO_DATA(string type, string plant)
        {
            if (type == "COMBO_PLANT")
            {
                DataSet dtSet = Data_Select(type, "", "");
                DataTable dt = dtSet.Tables[0];
                cbo_Plant.DataSource = dt;
                cbo_Plant.DisplayMember = "NAME";
                cbo_Plant.ValueMember = "CODE";
                cbo_Plant.SelectedIndex = 0;
            }
            if (type == "COMBO_LINE")
            {
                plant = cbo_Plant.SelectedValue.ToString();
                DataSet dtSet = Data_Select(type, plant, "");
                DataTable dt = dtSet.Tables[0];
                cbo_line.DataSource = dt;
                cbo_line.DisplayMember = "NAME";
                cbo_line.ValueMember = "CODE";
                cbo_line.SelectedIndex = 0;
            }
            if (type == "DATE")
            {
                DataSet dtSet = Data_Select(type, "", "");
                DataTable dt = dtSet.Tables[0];
                _CurrentDay = dt.Rows[0]["CURRENTDAY"].ToString();
                dtpDateT.EditValue = dt.Rows[0]["TODAY"];
                dtpDateF.EditValue = dt.Rows[0]["PREV_DAY"];

            }

        }
        private void SetData(string arg_type, string plant, string line, bool arg_load = true)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                grdBase.DataSource = null;
                DataSet ds = Data_Select(arg_type, plant, line);
                if (ds == null || ds.Tables.Count == 0) return;
                DataTable dtSource = ds.Tables[0];

                DataTable dtChart = ds.Tables[1];
                SetChart(dtChart);             

                if (dtSource != null)
                {
                    fnProcess(dtSource);
                    dtSource.Columns.Remove("MON");
                    dtSource.Columns.Remove("RN");
                  
                    DataTable dt = Pivot(dtSource, dtSource.Columns["YMD"], dtSource.Columns["QTY"]);
                    grdBase.DataSource = dt;

                    gvwBase.BandPanelRowHeight = 50;
                    gvwBase.RowHeight = 50;
                    for (int i = 0; i < gvwBase.Columns.Count; i++)
                    {
                        //Title             
                        gvwBase.Columns[i].OwnerBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gvwBase.Columns[i].OwnerBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        gvwBase.Columns[i].OwnerBand.Width = 80;
                        gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                        gvwBase.Columns[i].OwnerBand.AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);

                        //Data
                        gvwBase.Columns[i].AppearanceCell.Font = new Font("Calibri", 14, FontStyle.Regular);
                        gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                        gvwBase.Columns[i].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;

                        if (i == 0)
                        {
                            gvwBase.Columns[i].OwnerBand.Width = 300;
                            gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                        }

                        if (i > 0)
                        {
                            gvwBase.Columns[i].OwnerBand.Width = 100;
                            gvwBase.Columns[i].DisplayFormat.FormatType = FormatType.Numeric;
                            gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
                              
                        }
                     //   gvwBase.Columns[i].Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gvwBase.Columns[i].GetCaption().Replace("-", " ").Replace("'", " ").ToLower()).Split(',')[0];
                     
                    }
                }
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                Debug.WriteLine(ex.ToString());
                //throw;
            }
            finally
            {
                
            }

        }

        private void SetChart(DataTable argData)
        {
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();

            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;

            if (argData == null) return;
            for (int i = 0; i <= argData.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argData.Rows[i]["MON"].ToString(), argData.Rows[i]["OSD_QTY"]));
                chartControl1.Series[1].Points.Add(new SeriesPoint(argData.Rows[i]["MON"].ToString(), argData.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argData.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate > 0.4)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(250, 55, 30);
                }
                else if (rate <= 0.3)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(20,200,110);
                }
                else
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(255, 180, 15);
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

        private void fnProcess(DataTable dt)
        {
            try
            {
                gvwBase.Bands.Clear();
                gvwBase.Columns.Clear();

                GridBand gridBand1 = new GridBand();
                BandedGridColumn column_Band1 = new BandedGridColumn();
                GridBand gridBand2 = new GridBand();
                BandedGridColumn column_Band2 = new BandedGridColumn();

                gridBand1.Caption = "stt";
                gridBand1.Name = "STT";
                gridBand1.VisibleIndex = 0;
                gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                column_Band1.Caption = "STT";
                column_Band1.FieldName = "STT";
                column_Band1.Name = "STT";
                column_Band1.Visible = false;
                column_Band1.Width = 0;
                column_Band1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand2.Caption = "Date";
                gridBand2.Name = "ITEM";
                gridBand2.VisibleIndex = 0;
                gridBand2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                column_Band2.Caption = "Item";
                column_Band2.FieldName = "ITEM";
                column_Band2.Name = "ITEM";
                column_Band2.Visible = true;
                column_Band2.Width = 120;
                column_Band2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                //gridBand1.Columns.Add(column_Band1);
                gridBand2.Columns.Add(column_Band2);

                gvwBase.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band2 });
                gvwBase.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });

                //Create Header
                DataView view = new DataView(dt);
                //DataTable distinctValues = view.ToTable(true, "MON");

                var distinctValue = dt.AsEnumerable()
                                  .Select(row => new
                                  {
                                      RN = row.Field<decimal>("RN"),
                                      YMD = row.Field<string>("YMD"),
                                      MON = row.Field<string>("MON")                                      
                                  })
                                  .Distinct().OrderBy(r => r.RN);
                DataTable distinctValues = LINQResultToDataTable(distinctValue);

                for (int i = 0; i < distinctValues.Rows.Count; i++)
                {
                    GridBand gridBand = new GridBand();
                    BandedGridColumn column_Band = new BandedGridColumn();

                    // gvwBase.Columns[i].Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gvwBase.Columns[i].GetCaption().Replace("-", " ").Replace("'", " ").ToLower()).Split(',')[0];

                    gridBand.Caption = distinctValues.Rows[i]["MON"].ToString();
                    gridBand.Name = string.Concat("MON", i);
                    gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    gridBand.VisibleIndex = i;

                    column_Band.Caption = distinctValues.Rows[i]["MON"].ToString();
                    column_Band.FieldName = distinctValues.Rows[i]["YMD"].ToString();
                    column_Band.Name = distinctValues.Rows[i]["MON"].ToString();
                    column_Band.Visible = true;
                    column_Band.Width = 120;

                    gridBand.Columns.Add(column_Band);
                    gvwBase.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band });
                    gvwBase.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand });
                    gridBand.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                    if (gridBand.ToString().ToUpper() == "TOTAL")
                    {
                        gridBand.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }



        DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue) //(Bẻ cột)
        {
            try
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
            catch (Exception ex) { return null; }
        }

        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================
        private DataSet Data_Select(string argType, string plant, string line)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(7);
            MyOraDB.Process_Name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_INTERNAL_OSD";//

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

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = dtpDateF.DateTime.ToString("yyyyMMdd");
            MyOraDB.Parameter_Values[2] = dtpDateT.DateTime.ToString("yyyyMMdd");
            MyOraDB.Parameter_Values[3] = plant;// 
            MyOraDB.Parameter_Values[4] = line;//cbo_line.SelectedValue == null ? "" : cbo_line.SelectedValue.ToString();
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
