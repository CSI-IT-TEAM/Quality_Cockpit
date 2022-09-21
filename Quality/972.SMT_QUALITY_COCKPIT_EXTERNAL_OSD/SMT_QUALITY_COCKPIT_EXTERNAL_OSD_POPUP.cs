using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POPUP : Form
    {


        string _date,  _plant_code, _line_code;

        public SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POPUP()
        {
            InitializeComponent();
        }
        public SMT_QUALITY_COCKPIT_EXTERNAL_OSD_POPUP(string date, string plant, string line )
        {
            InitializeComponent();
            _date = date; 
            _plant_code = plant;
            _line_code = line;
        }
        public DataTable _dtData = null;

        private void SetData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                grdBase.DataSource = null;
                DataSet ds = Data_Select("POP");
                if (ds == null || ds.Tables.Count == 0) return;
                _dtData = ds.Tables[0];
                grdBase.DataSource = _dtData;
                if (_dtData != null)
                {
                    gvwBase.RowHeight = 50;

                    for (int i = 0; i < gvwBase.Columns.Count; i++)
                    {
                        if (i <= 2)
                            gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                        else
                            gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                        gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                        if (i == 3 || i == 6 || i == 7 || i == 5)
                        {
                            gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                        }
                        if (i == 10 || i == 11)
                        {
                            gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                            gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Debug.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }         
        }

        #region DB
        private DataSet Data_Select(string argType)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(7);
            MyOraDB.Process_Name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_INTERNAL_OSD";//

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
            MyOraDB.Parameter_Values[1] = _date;
            MyOraDB.Parameter_Values[2] = "";
            MyOraDB.Parameter_Values[3] = _plant_code;// 
            MyOraDB.Parameter_Values[4] = _line_code;//cbo_line.SelectedValue == null ? "" : cbo_line.SelectedValue.ToString();
            MyOraDB.Parameter_Values[5] = "";
            MyOraDB.Parameter_Values[6] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS;
        }

        #endregion DB


        private void SMT_QUALITY_COCKPIT_INTERNAL_OSD_POPUP_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                SetData();
            }
            else
            {
               
            }
        }

        private void gvwBase_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!gvwBase.GetRowCellValue(e.RowHandle, "MLINE_CD").Equals("TOTAL") || !gvwBase.GetRowCellValue(e.RowHandle, "MLINE_CD").Equals("G-TOTAL"))
            {
                if (gvwBase.Columns[e.Column.ColumnHandle].FieldName.ToUpper().Contains("QTY"))
                {
                    if (!gvwBase.GetRowCellValue(e.RowHandle, "C_QTY").Equals(gvwBase.GetRowCellValue(e.RowHandle, "RE_QTY")))
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }

                }
            }
            if (gvwBase.GetRowCellValue(e.RowHandle, "MLINE_CD").Equals("TOTAL") && e.Column.ColumnHandle > 0)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.ForeColor = Color.Black;
            }
            //if (gvwBase.GetRowCellValue(e.RowHandle, "LINE_NAME").Equals("G-TOTAL"))
            //{
            //    e.Appearance.BackColor = Color.LightSalmon;
            //    e.Appearance.ForeColor = Color.Black;
            //}
        }

        private void gvwBase_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "LINE_NAME")
            {
                string line1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "LINE_NAME").Trim();
                string line2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "LINE_NAME").Trim();

                if (line1 == line2)
                {
                    e.Merge = true;
                }
                else
                {
                    e.Merge = false;
                }
            }

            if (e.Column.FieldName == "MLINE_CD")
            {
                string Mline1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "MLINE_CD").Trim();
                string Mline2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "MLINE_CD").Trim();

                string line1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "LINE_NAME").Trim();
                string line2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "LINE_NAME").Trim();
                if (Mline1 == Mline2 && line1 == line2)
                {
                    e.Merge = true;
                }
                else
                {
                    e.Merge = false;
                }
            }
            if (e.Column.FieldName == "MODEL_NAME")
            {
                string Mline1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "MLINE_CD").Trim();
                string Mline2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "MLINE_CD").Trim();

                string line1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "LINE_NAME").Trim();
                string line2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "LINE_NAME").Trim();

                string style_nm1 = gvwBase.GetRowCellDisplayText(e.RowHandle1, "MODEL_NAME").Trim();
                string style_nm2 = gvwBase.GetRowCellDisplayText(e.RowHandle2, "MODEL_NAME").Trim();

                if (Mline1 == Mline2 && line1 == line2 && style_nm1 == style_nm2)
                {
                    e.Merge = true;
                }
                else
                {
                    e.Merge = false;
                }
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SaveDlg = new SaveFileDialog())
            {
                SaveDlg.RestoreDirectory = true;
                SaveDlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (SaveDlg.ShowDialog() == DialogResult.OK)
                {
                    gvwBase.ExportToXlsx(SaveDlg.FileName);
                }


            }
        }
    }
}
