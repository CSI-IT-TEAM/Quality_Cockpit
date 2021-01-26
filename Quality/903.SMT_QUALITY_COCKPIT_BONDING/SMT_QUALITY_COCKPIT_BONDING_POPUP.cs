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
using DevExpress.XtraCharts;
using System.Threading.Tasks;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_BONDING_POPUP : Form
    {
        public SMT_QUALITY_COCKPIT_BONDING_POPUP()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Daily Bonding";

        string _date, _plant_code, _line_code;
        public SMT_QUALITY_COCKPIT_BONDING_POPUP(string date, string plant, string line)
        {
            InitializeComponent();
            _date = date;
            _plant_code = plant;
            _line_code = line;
        }
        //  private UC.UC_COMPARE_WEEK uc_compare_week = new UC.UC_COMPARE_WEEK();
        string _strType = "Q";
        string _plant = ComVar.Var._strValue1;  // "";// 
        string _line = ComVar.Var._strValue2;  //"";//
        int _time = 0;
        string _CurrentDay = "";// DateTime.Now.ToString("MMM - dd");
        private async void SetData()
        {
            try
            {

                string YMD, PLANT_CD, LINE_CD;
                int total = 0;
                double PER = 0;



                DataSet dsData = Data_Select("Q", _date, _plant_code, _line_code);

                if (dsData == null) return;
                DataTable dtGrid = dsData.Tables[0];
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

        private void SetChart(DataTable argDtChart)
        {
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["HH"].ToString(), argDtChart.Rows[i]["BOND_QTY"]));
            }
        }

        private void fnProcess(DataTable dt)
        {
            //try
            //{
            //    gvwBase.Bands.Clear();
            //    gvwBase.Columns.Clear();

            //    GridBand gridBand1 = new GridBand();
            //    BandedGridColumn column_Band1 = new BandedGridColumn();  

            //    gridBand1.Caption = "Date";
            //    gridBand1.Name = "ITEM";
            //    gridBand1.VisibleIndex = 0;
            //    gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            //    column_Band1.Caption = "Item";
            //    column_Band1.FieldName = "ITEM";
            //    column_Band1.Name = "ITEM";
            //    column_Band1.Visible = true;
            //    column_Band1.Width = 150;
            //    column_Band1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            //    gridBand1.Columns.Add(column_Band1);  

            //    gvwBase.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band1 });
            //    gvwBase.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
              
            //    //Create Header
            //    DataView view = new DataView(dt);
            //    DataTable distinctValues = view.ToTable(true, "MON");
            //    for (int i = 0; i < distinctValues.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < distinctValues.Columns.Count; j++)
            //        {
            //            GridBand gridBand = new GridBand();
            //            BandedGridColumn column_Band = new BandedGridColumn();

            //            gridBand.Caption = distinctValues.Rows[i]["MON"].ToString();
            //            gridBand.Name = string.Concat("MON", i);
            //            gridBand.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            //            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //            gridBand.VisibleIndex = i;

            //            column_Band.Caption = distinctValues.Rows[i]["MON"].ToString();
            //            column_Band.FieldName = distinctValues.Rows[i]["MON"].ToString();
            //            column_Band.Name = distinctValues.Rows[i]["MON"].ToString();
            //            column_Band.Visible = true;
            //            column_Band.Width = 100;

            //            gridBand.Columns.Add(column_Band);
            //            gvwBase.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band });
            //            gvwBase.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand });
            //            gridBand.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());               

            //}
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null; 
            }
        }

        //===================load com bo ===============

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
            //if (type == "DATE")
            //{
            //    DataTable dt = LOAD_COMBO_V2(type, "", "");
            //    _CurrentDay = dt.Rows[0]["CURRENTDAY"].ToString();
            //    dtpDateT.EditValue = dt.Rows[0]["TODAY"];
            //    dtpDateF.EditValue = dt.Rows[0]["PREV_DAY"];

            //}

        }       

        private void LoadForm()
        {
           // dtpDateF.EditValue = DateTime.Now.AddDays(-6).ToString("yyyy/MM/dd"); ;// DateTime.Now.ToString("yyyy/MM/dd");
          //  dtpDateT.EditValue = DateTime.Now.ToString("yyyy/MM/dd");

            GET_COMBO_DATA("CPLANT", "");
            GET_COMBO_DATA("DATE", "");
            //SetData(_strType, _plant, _line);

        }
       

        #region DB
        private DataSet Data_Select(string argType, string date, string plant, string line)
        {
                COM.OraDB MyOraDB = new COM.OraDB();

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_BONDING_POPUP";//

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_PLANT";
                MyOraDB.Parameter_Name[3] = "V_P_LINE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = argType;
                MyOraDB.Parameter_Values[1] = date;
                MyOraDB.Parameter_Values[2] = plant;// 
                MyOraDB.Parameter_Values[3] = line;//cbo_line.SelectedValue == null ? "" : cbo_line.SelectedValue.ToString();
                MyOraDB.Parameter_Values[4] = "";

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
            //lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if(_time >=30)
            {
                _time = 0;
                SetData();
            }
            
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        } 

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }        


        private void rdByDay_CheckedChanged(object sender, EventArgs e)
        {
                pnBody2.Visible = true;
                pnBody1.Visible = false;
                //btn_Search.Visible = false;

        }

        private void lblDate_DoubleClick_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

     
        private void cbo_Plant_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cbo_Plant.SelectedValue.ToString() != null)
                //    GET_COMBO_DATA("CLINE", cbo_Plant.SelectedValue.ToString());
                //else
                //    return;

            }
            catch { }
        }

        private void SMT_QUALITY_COCKPIT_BONDING_VisibleChanged(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {        
            //SetData(_strType, _plant, _line);
            

        }

        private void gvwBase_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.AbsoluteIndex > 0)
            //{
            //    if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("RATE"))
            //    {
            //        if (e.CellValue == DBNull.Value) return;
            //        e.DisplayText = double.Parse(e.CellValue.ToString()).ToString("0.##") ;

            //    }
            //}
            //try
            //{
            //    if (e.Column.FieldName == _CurrentDay)
            //    {
            //        //return;
            //        Rectangle rect = e.Bounds;
            //        rect.Inflate(new Size(1, 1));

            //        Brush brush = new SolidBrush(e.Appearance.BackColor);
            //        e.Graphics.FillRectangle(brush, rect);
            //        Pen pen_horizental = new Pen(Color.Blue, 3F);
            //        Pen pen_vertical = new Pen(Color.Blue, 4F);

            //        ////draw bottom
            //        //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            //        //// draw top
            //        //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

            //        //if (e.RowHandle == 0)
            //        //{
            //        //    e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);
            //        //}
            //        //else 
            //        if (e.RowHandle == gvwBase.RowCount - 1)
            //        {
            //            e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
            //        }
            //        // draw right
            //        e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);


            //        // draw left
            //        e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X, rect.Y + rect.Height);

            //        string[] ls = e.DisplayText.Split('\n');

            //        e.Graphics.DrawString(ls[0], e.Appearance.Font, new SolidBrush(e.Appearance.ForeColor), rect, e.Appearance.GetStringFormat());

            //        e.Handled = true;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //}
        }

        private void gvwBase_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e.CellValue == DBNull.Value) return;
            //if (e.Column.AbsoluteIndex > 0)
            //{
            //    if (gvwBase.GetRowCellValue(e.RowHandle, gvwBase.Columns["ITEM"]).ToString().ToUpper().Contains("RATE"))
            //    {
            //        double rate = double.Parse(e.CellValue.ToString());
            //        if (rate <= 1)
            //        {
            //            e.Appearance.BackColor = Color.Green;
            //            e.Appearance.ForeColor = Color.White;
            //        }    
            //        else if (rate > 2)
            //        {
            //            e.Appearance.BackColor = Color.Red;
            //            e.Appearance.ForeColor = Color.White;
            //        }
                        
            //        else
            //        {
            //            e.Appearance.BackColor = Color.Yellow;
            //            e.Appearance.ForeColor = Color.Black;
            //        }
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
                //if (cbo_Plant.SelectedValue.ToString() != null)
                //    GET_COMBO_DATA("CLINE", cbo_Plant.SelectedValue.ToString());

                //else
                //    return;

            }
            catch { }
        }

        private void SMT_QUALITY_COCKPIT_BONDING_Load(object sender, EventArgs e)
        {
            //LoadForm();
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {
           // double[] val = e.SeriesPoint.Values;
           // e.SeriesP
            /*
            BarDrawOptions drawOptions = e.SeriesDrawOptions as BarDrawOptions;
            if (drawOptions == null)
                return;

            if (val > 17)
            {
                drawOptions.Color = Color.Red;                
            }
            else if (val > 2)
            {
                drawOptions.Color = Color.Yellow;
            }
            else
            {
                drawOptions.Color = Color.Green;
            }
            drawOptions.FillStyle.FillMode = FillMode.Solid;
            */
        }

        private void gvwBase_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
        {
            try
            {
                if(e.Band == null) return;
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
    }
}
