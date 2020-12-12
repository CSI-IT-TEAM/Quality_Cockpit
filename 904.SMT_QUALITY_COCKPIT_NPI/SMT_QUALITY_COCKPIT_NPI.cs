using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class SMT_QUALITY_COCKPIT_NPI : Form
    {
        public SMT_QUALITY_COCKPIT_NPI()
        {
            InitializeComponent();
        }
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");

        #region Event

        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
            load_combo("DATE");
            load_combo("COMBO_PLANT");
            load_combo("COMBO_LINE");
            Sp_BindingHeader();
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                cboPlant.SelectedValue = ComVar.Var._strValue1;
                cboLine.SelectedValue = ComVar.Var._strValue2;
                grdBase.DataSource = null;

                fn_BindingData();
                _time = 0;
                btnSearch_Click(null, null);

                tmrTime.Start();
            }
            else
            {
                tmrTime.Stop();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                btnSearch_Click(null, null);
                //SetData(_strType, false);
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_combo("COMBO_LINE");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                

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

        #endregion Event

        #region Combobox
        private void load_combo(string arg_type)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (arg_type == "DATE")
                {
                    DataTable dtDATE = Fn_SelectDataCombo(arg_type, "", "");

                    dtpYMD.EditValue = dtDATE.Rows[0]["PREV_DAY"];
                    dtpYMDT.EditValue = dtDATE.Rows[0]["TODAY"];
                }
                if (arg_type == "COMBO_PLANT")
                {
                    DataTable dt = Fn_SelectDataCombo(arg_type, "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cboPlant.DataSource = dt;
                        cboPlant.DisplayMember = "NAME";
                        cboPlant.ValueMember = "CODE";
                    }
                }
                if (arg_type == "COMBO_LINE")
                {
                    DataTable dt1 = Fn_SelectDataCombo(arg_type, cboPlant.SelectedValue.ToString().Trim(), "");
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        cboLine.DataSource = dt1;
                        cboLine.DisplayMember = "NAME";
                        cboLine.ValueMember = "CODE";

                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { 
                this.Cursor = Cursors.Default; 
            }
        }

        #endregion


        #region Set Grid
        private async void Sp_BindingHeader()
        {
            try
            {
                string plant = cboPlant.SelectedValue.ToString();
                string line = cboLine.SelectedValue.ToString();
                string frDate = dtpYMD.DateTime.ToString("yyyyMMdd");
                string toDate = dtpYMDT.DateTime.ToString("yyyyMMdd");

                DataTable dtData = await Fn_SelectDataGrid("H", plant, line , frDate, toDate) ;
                if (dtData == null || dtData.Rows.Count == 0) return;

                grdView.Bands.Clear();
                grdView.Columns.Clear();

                string[] fieldName = { "PLANT_NM", "LINE_CD", "CATEGORY_NAME", "TD_CODE", "STYLE_CODE", "MODEL_NAME", "PROD_DATE" };
                string[] caption = { "Plant", "Line", "Category", "TD Code", "Style Code", "Model Name","Prod Date" };

                for (int i =0;i <fieldName.Length;i++)
                {
                    Sp_AddBandFixed(fieldName[i], caption[i]);
                }
                

                /*
                GridBand gridBand1 = new GridBand();
                GridBand gridBand2 = new GridBand();
                GridBand gridBand3 = new GridBand();
                GridBand gridBand4 = new GridBand();
                GridBand gridBand5 = new GridBand();
                GridBand gridBand6 = new GridBand();
                GridBand gridBand7 = new GridBand();

                BandedGridColumn column_Band1 = new BandedGridColumn();
                BandedGridColumn column_Band2 = new BandedGridColumn();
                BandedGridColumn column_Band3 = new BandedGridColumn();
                BandedGridColumn column_Band4 = new BandedGridColumn();
                BandedGridColumn column_Band5 = new BandedGridColumn();
                BandedGridColumn column_Band6 = new BandedGridColumn();
                BandedGridColumn column_Band7 = new BandedGridColumn();
               // column_Band1.Caption = "PLANT";
                column_Band1.FieldName = "PLANT_NM";
                column_Band1.Name = "PLANT_NM";
                column_Band1.Visible = true;

                column_Band2.Caption = "LINE";
                column_Band2.FieldName = "LINE_CD";
                column_Band2.Name = "LINE_CD";
                column_Band2.Visible = true;

                column_Band3.Caption = "CATEGORY";
                column_Band3.FieldName = "CATEGORY_NAME";
                column_Band3.Name = "CATEGORY_NAME";
                column_Band3.Visible = true;
                column_Band3.Width = 130;

                column_Band4.Caption = "TD_CODE";
                column_Band4.FieldName = "TD_CODE";
                column_Band4.Name = "TD_CODE";
                column_Band4.Visible = true;

                column_Band5.Caption = "STYLE_CODE";
                column_Band5.FieldName = "STYLE_CODE";
                column_Band5.Name = "STYLE_CODE";
                column_Band5.Visible = true;

                column_Band6.Caption = "MODEL_NAME";
                column_Band6.FieldName = "MODEL_NAME";
                column_Band6.Name = "MODEL_NAME";
                column_Band6.Visible = true;
                column_Band6.Width = 150;

                column_Band7.Caption = "PROD_DATE";
                column_Band7.FieldName = "PROD_DATE";
                column_Band7.Name = "PROD_DATE";
                column_Band7.Visible = true;

                //6 Fixed band
                gridBand1.Caption = "Plant";
             //   gridBand1.Name = "gridBand1";
                gridBand1.VisibleIndex = 0;
                gridBand1.Columns.Add(column_Band1);
                gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridBand1.AppearanceHeader.Options.UseTextOptions = true;
                gridBand1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                gridBand2.Caption = "Line";
                gridBand2.Name = "gridBand2";
                gridBand2.VisibleIndex = 1;
                gridBand2.Columns.Add(column_Band2);
                gridBand2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand3.Caption = "Category";
                gridBand3.Name = "gridBand3";
                gridBand3.VisibleIndex = 2;
                gridBand3.Columns.Add(column_Band3);
                gridBand3.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand4.Caption = "TD Code";
                gridBand4.Name = "gridBand4";
                gridBand4.VisibleIndex = 3;
                gridBand4.Columns.Add(column_Band4);
                gridBand4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand5.Caption = "Style Code";
                gridBand5.Name = "gridBand5";
                gridBand5.VisibleIndex = 4;
                gridBand5.Columns.Add(column_Band5);
                gridBand5.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand6.Caption = "Model Name";
                gridBand6.Name = "gridBand6";
                gridBand6.VisibleIndex = 5;
                gridBand6.Columns.Add(column_Band6);
                gridBand6.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                gridBand7.Caption = "Prod Date";
                gridBand7.Name = "gridBand7";
                gridBand7.VisibleIndex = 6;
                gridBand7.Columns.Add(column_Band7);
                gridBand7.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                grdView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band1, column_Band2, column_Band3, column_Band4, column_Band5, column_Band6, column_Band7 });
                grdView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1, gridBand2, gridBand3, gridBand4, gridBand5, gridBand6, gridBand7 });
               */

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //2 band chung cặp
                    //  GridBand first_Band = new GridBand();
                    GridBand second_Band = new GridBand();
                    GridBand third_Band = new GridBand();
                    BandedGridColumn column_Band = new BandedGridColumn();

                    //first_Band.Caption = dt.Rows[i]["NPI_CODE"].ToString();
                    //first_Band.Name = string.Concat("first_Band", i);
                    //first_Band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    //first_Band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //first_Band.VisibleIndex = i;

                    second_Band.Caption = dtData.Rows[i]["NPI_DATE"].ToString();
                    second_Band.Name = string.Concat("second_Band", i);
                    second_Band.AppearanceHeader.Options.UseTextOptions = true;
                    second_Band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    second_Band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    second_Band.VisibleIndex = i;
                    third_Band.Caption = dtData.Rows[i]["NPI_NAME"].ToString();
                    third_Band.Name = string.Concat("third_Band", i);
                    third_Band.VisibleIndex = i;
                    third_Band.AppearanceHeader.Options.UseTextOptions = true;
                    third_Band.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    third_Band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    third_Band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    third_Band.RowCount = 6;
                    third_Band.VisibleIndex = i;

                    column_Band.Caption = dtData.Rows[i]["NPI_CODE"].ToString();
                    column_Band.FieldName = dtData.Rows[i]["NPI_CODE"].ToString();
                    column_Band.Name = dtData.Rows[i]["NPI_CODE"].ToString();
                    
                    column_Band.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    column_Band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    column_Band.Visible = true;
                    column_Band.Width = 90;
                    third_Band.Columns.Add(column_Band);
                    grdView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] { column_Band });
                    //first_Band.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { second_Band });
                    second_Band.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { third_Band });
                    grdView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { second_Band });
                    grdView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                }

                //for (int i = 0; i < grdView.Columns.Count; i++)
                //{
                //    grdView.Columns[i].AppearanceHeader.Font = new Font("Calibri", 16, FontStyle.Bold);                   
                //    grdView.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                //    grdView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sp_AddBandFixed(string argFieldName, string argCaption)
        {
            GridBand gridBand1 = new GridBand();
            BandedGridColumn column_Band1 = new BandedGridColumn();

            column_Band1.FieldName = argFieldName;
            column_Band1.Name = argFieldName;
            column_Band1.Visible = true;
            gridBand1.Caption = argCaption;
            gridBand1.VisibleIndex = 0;
            gridBand1.Columns.Add(column_Band1);
            gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            gridBand1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            grdView.Columns.AddRange(new BandedGridColumn[] { column_Band1});
            grdView.Bands.AddRange(new GridBand[] { gridBand1 });

        }

        private async void fn_BindingData()
        {
            try
            {
                grdView.BeginUpdate();
                string plant = cboPlant.SelectedValue.ToString();
                string line = cboLine.SelectedValue.ToString();
                string frDate = dtpYMD.DateTime.ToString("yyyyMMdd");
                string toDate = dtpYMDT.DateTime.ToString("yyyyMMdd");
                DataTable dt = await Fn_SelectDataGrid("Q", plant, line, frDate, toDate);
                DataTable dtPivot = Pivot(dt, dt.Columns["NPI_CODE"], dt.Columns["VALUE1"]);
                grdBase.DataSource = dtPivot.Copy();

                for (int i = 0; i < grdView.Columns.Count; i++)
                {
                    grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    if (i >= 7)
                    {
                        grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    }
                }

                grdView.Columns["CATEGORY_NAME"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                grdView.Columns["MODEL_NAME"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;

                grdView.OptionsBehavior.Editable = false;
                grdView.OptionsBehavior.ReadOnly = true;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                grdView.EndUpdate();
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

        private void grdView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.AbsoluteIndex >= 7)
                {
                    string ValueCell = grdView.GetRowCellValue(e.RowHandle, grdView.Columns[e.Column.FieldName]).ToString();
                    if (ValueCell.Length > 1)
                    {
                        ValueCell = ValueCell.Substring(0, 1);
                    }
                    switch (ValueCell)
                    {
                        case "Y":
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.ForeColor = Color.Yellow;
                            break;
                        case "G":
                            e.Appearance.BackColor = Color.Green;
                            e.Appearance.ForeColor = Color.Green;
                            break;
                        case "R":
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.Red;
                            break;
                        case "S":
                            e.Appearance.BackColor = Color.Silver;
                            e.Appearance.ForeColor = Color.Silver;
                            break;
                        case "B":
                            e.Appearance.BackColor = Color.Black;
                            e.Appearance.ForeColor = Color.Black;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region Database
        private DataTable Fn_SelectDataCombo(string argType, string argPlant, string argLine )
        {           
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.NPI_COMBO_SELECT";

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

                MyOraDB.Parameter_Values[0] = argType;
                MyOraDB.Parameter_Values[1] = argPlant;
                MyOraDB.Parameter_Values[2] = argLine;
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

        public async Task<DataTable> Fn_SelectDataGrid(string argType, string argPlan, string argLine, string argFromDate, string argToDate)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                MyOraDB.ShowErr = true;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.NPI_DATA_SELECT";

                    MyOraDB.ReDim_Parameter(7);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_FACTORY";
                    MyOraDB.Parameter_Name[2] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[3] = "V_P_MLINE";
                    MyOraDB.Parameter_Name[4] = "V_P_FROM";
                    MyOraDB.Parameter_Name[5] = "V_P_TO";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;

                    MyOraDB.Parameter_Values[0] = argType;
                    MyOraDB.Parameter_Values[1] = "2110";
                    MyOraDB.Parameter_Values[2] = argPlan;
                    MyOraDB.Parameter_Values[3] = argLine;
                    MyOraDB.Parameter_Values[4] = argFromDate;
                    MyOraDB.Parameter_Values[5] = argToDate;
                    MyOraDB.Parameter_Values[6] = "";

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
            });
        }

        #endregion DB


       


        

        
    }
}
