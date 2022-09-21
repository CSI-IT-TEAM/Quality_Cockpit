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
        public SMT_QUALITY_COCKPIT_EXTERNAL_OSD()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Long Thanh External OS&&D";
        //  private UC.UC_COMPARE_WEEK uc_compare_week = new UC.UC_COMPARE_WEEK();
        string _strType = "Q";
        string _plant = ComVar.Var._strValue1;  // "";// 
        string _line = ComVar.Var._strValue2;  //"";//
        int _time = 0;
        string _CurrentDay = "";
        int _start_column = 0;

        private void SetData(string arg_type, string plant, string line, bool arg_load = true)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                grdBase.DataSource = null;
                DataSet ds = Data_Select(arg_type, plant, line);
                if (ds == null || ds.Tables.Count == 0) return;
                //DataTable dtSource = ds.Tables[0]; 
                DataTable dtf = ds.Tables[0];
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
                        chartControl1.PaletteName = "doit";
                        //((XYDiagram)chart.Diagram).AxisX.Title.Text = "Vendor";
                        //((XYDiagram)chartDay.Diagram).AxisX.Title.Visibility = DefaultBoolean.True;
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "OS&D (Prs)";
                        ((XYDiagram)chartControl1.Diagram).AxisY.Title.Visibility = DefaultBoolean.True;
                        chartControl1.SeriesTemplate.Label.TextPattern = "{V:#,0.#}";
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Debug.WriteLine(ex.ToString());
                //throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

                if (rate > 2)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate > 1)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Yellow;
                }
                else
                {
                    chartControl1.Series[0].Points[i].Color = Color.Green;
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

                gridBand2.Caption = "";
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

        //===================load com bo ===============

        private void GET_COMBO_DATA(string type, string plant)
        {
            if (type == "CPLANT")
            {
                DataTable dt = LOAD_COMBO_V2(type, "","");
                cbo_Plant.DataSource = dt;
                cbo_Plant.DisplayMember = "NAME";
                cbo_Plant.ValueMember = "CODE";
                cbo_Plant.SelectedIndex = 0;
            }
            if (type == "CLINE")
            {
                plant = cbo_Plant.SelectedValue.ToString();
                DataTable dt = LOAD_COMBO_V2(type, plant,"");

                cbo_line.DataSource = dt;
                cbo_line.DisplayMember = "NAME";
                cbo_line.ValueMember = "CODE";
                cbo_line.SelectedIndex = 0;
            }
            if (type == "DATE")
            {
                DataTable dt = LOAD_COMBO_V2(type, "", "");
                _CurrentDay = dt.Rows[0]["CURRENTDAY"].ToString();
                dtpDateT.EditValue = dt.Rows[0]["TODAY"];
                dtpDateF.EditValue = dt.Rows[0]["PREV_DAY"];

            }

        }       

        private void LoadForm()
        {

            //GET_COMBO_DATA("CPLANT", "");
            GET_COMBO_DATA("DATE", "");
        }

        #region DB
        private DataSet Data_Select(string argType, string plant, string line)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(7);
            MyOraDB.Process_Name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_EXTERNAL_OSD";//

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_DATEF";
            MyOraDB.Parameter_Name[2] = "V_P_DATET";
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


        public DataTable LOAD_COMBO_V2(string type, string plant, string line)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_SET_COMBO";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_PLANT";
                MyOraDB.Parameter_Name[2] = "V_P_LINE";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = type;
                MyOraDB.Parameter_Values[1] = plant;
                MyOraDB.Parameter_Values[2] = line;
                MyOraDB.Parameter_Values[3] = "";

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[process_name];
            }
            catch
            {
                return null;
            }
        }

        #endregion DB

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if(_time >=30)
            {
                _time = 0;             
                SetData(_strType,_plant, _line, false);
            }
            
        }

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

        private void SMT_QUALITY_COCKPIT_EXTERNAL_OSD_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 0;
                _strType = "Q";
              
               // cbo_Plant.SelectedValue = ComVar.Var._strValue1;
               // cbo_line.SelectedValue = ComVar.Var._strValue2;

                _plant = ComVar.Var._strValue1; // cbo_Plant.SelectedValue.ToString(); //
                _line = ComVar.Var._strValue2; // cbo_line.SelectedValue.ToString(); //  

                timer1.Start();
                SetData(_strType, _plant, _line, false);

            }
            else
            {
                timer1.Stop();
                Dispose();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {        
            SetData(_strType, _plant, _line,false);
            

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
            
            //    if (e.Column.AbsoluteIndex > 0)
            //    {
            //        if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("REPLENISHMENT RATE (%)") ||
            //            gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("OS&D RATE (%)"))
            //        {
            //            if (e.CellValue == DBNull.Value) return;
            //            e.DisplayText = double.Parse(e.CellValue.ToString()).ToString("0.0#");
            //        }

            //        if (gvwBase.Columns[e.Column.FieldName].OwnerBand.Caption == _CurrentDay)
            //        {
            //            //return;
            //            Rectangle rect = e.Bounds;
            //            rect.Inflate(new Size(1, 1));

            //            Brush brush = new SolidBrush(e.Appearance.BackColor);
            //            e.Graphics.FillRectangle(brush, rect);
            //            Pen pen_horizental = new Pen(Color.Blue, 3F);
            //            Pen pen_vertical = new Pen(Color.Blue, 4F);

            //            ////draw bottom
            //            //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            //            //// draw top
            //            //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

            //            //if (e.RowHandle == 0)
            //            //{
            //            //    e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
            //            //}
            //            //else 
            //            if (e.RowHandle == gvwBase.RowCount - 1)
            //            {
            //                e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            //            }
            //            // draw right
            //            e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);


            //            // draw left
            //            e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X, rect.Y + rect.Height);

            //            string[] ls = e.DisplayText.Split('\n');

            //            e.Graphics.DrawString(ls[0], e.Appearance.Font, new SolidBrush(e.Appearance.ForeColor), rect, e.Appearance.GetStringFormat());

            //            e.Handled = true;
            //        }
            //    }

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
            //if (e.CellValue == DBNull.Value) return;
            //if (e.Column.AbsoluteIndex > 0)
            //{
            //    if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("OS&D RATE (%)"))
            //    {
            //        double rate = double.Parse(e.CellValue.ToString());
            //        if (rate <= 1)
            //        {
            //            e.Appearance.BackColor = Color.Green;
            //            e.Appearance.ForeColor = Color.White;

            //        }

            //        else if (rate > 2)
            //        {
            //            e.Appearance.ForeColor = Color.White;
            //            e.Appearance.BackColor = Color.Red;
            //        }
            //        else                  

            //            e.Appearance.BackColor = Color.Yellow;

            //    }
            //    if (e.Column.FieldName.ToUpper().Contains("TOTAL") && e.RowHandle != gvwBase.RowCount - 1)
            //    {
            //        e.Appearance.ForeColor = Color.Blue;
            //    }
            //}

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

        private void SMT_QUALITY_COCKPIT_EXTERNAL_OSD_Load(object sender, EventArgs e)
        {
            LoadForm();
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
                            //using (SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POPUP view1 = new SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POPUP(date, plant, line))
                            //{
                                
                            //    view1.ShowDialog();
                            //}
                        }
                    }

                    else
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        //using (SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POP view = new SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POP(strdate, strplant, strline))
                        //{
                        //    view.ShowDialog();
                        //}
                            
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

                    Brush brush = new SolidBrush(Color.DodgerBlue);
                    e.Graphics.FillRectangle(brush, rect);
                    Pen pen_horizental = new Pen(Color.Blue, 3F);
                    Pen pen_vertical = new Pen(Color.Blue, 4F);


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
            root.AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Regular);
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
                gvwBase.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gvwBase.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Regular);
                gvwBase.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 18, FontStyle.Regular);
                
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

        private void lblDate_DoubleClick_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "973";
        }
    }
}
