using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_REWORK : Form
    {
        public SMT_QUALITY_COCKPIT_REWORK()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Daily Rework";
        int _time = 0;

        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
            load_combo("DATE");
            load_combo("COMBO_PLANT");
            load_combo("COMBO_LINE");
            while (gvwView.Columns.Count > 0)
            {
                gvwView.Columns.RemoveAt(0);
            }
        }
       
        #region DATABASE
        private DataTable Data_Select_Combo(string argType, string argPlant, string argLine )
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_SET_COMBO";

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

        public DataTable sbGetRework(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_GET_REWORK";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATEF";
                MyOraDB.Parameter_Name[2] = "V_P_DATET";
                MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                MyOraDB.Parameter_Name[4] = "V_P_LINE";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMDF;
                MyOraDB.Parameter_Values[2] = ARG_YMDT;
                MyOraDB.Parameter_Values[3] = ARG_PLANT;
                MyOraDB.Parameter_Values[4] = ARG_LINE;
                MyOraDB.Parameter_Values[5] = "";

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

        public DataTable sbGetRework_Chart(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_03.SP_GET_REWORK_CHART";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATEF";
                MyOraDB.Parameter_Name[2] = "V_P_DATET";
                MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                MyOraDB.Parameter_Name[4] = "V_P_LINE";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMDF;
                MyOraDB.Parameter_Values[2] = ARG_YMDT;
                MyOraDB.Parameter_Values[3] = ARG_PLANT;
                MyOraDB.Parameter_Values[4] = ARG_LINE;
                MyOraDB.Parameter_Values[5] = "";

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

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
        private void load_combo(string arg_type)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (arg_type == "DATE")
                {
                    DataTable dtDATE = Data_Select_Combo(arg_type, "", "");

                    dtpYMD.EditValue = dtDATE.Rows[0]["PREV_DAY"];
                    dtpYMDT.EditValue = dtDATE.Rows[0]["TODAY"];
                }
                if (arg_type == "COMBO_PLANT")
                {
                    DataTable dt = Data_Select_Combo(arg_type, "", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cboPlant.DataSource = dt;
                        cboPlant.DisplayMember = "NAME";
                        cboPlant.ValueMember = "CODE";
                    }
                }
                if (arg_type == "COMBO_LINE") 
                {
                    DataTable dt1 = Data_Select_Combo(arg_type, cboPlant.SelectedValue.ToString().Trim(), "");
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        cboLine.DataSource = dt1;
                        cboLine.DisplayMember = "NAME";
                        cboLine.ValueMember = "CODE";

                    }
                }

            }
            catch
            {

            }
            finally { this.Cursor = Cursors.Default; }
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
                string YMDF, YMDT, PLANT_CD, LINE_CD;
                int total = 0;
                double PER = 0;
                YMDF = dtpYMD.DateTime.ToString("yyyyMMdd");
                YMDT = dtpYMDT.DateTime.ToString("yyyyMMdd");
                PLANT_CD = cboPlant.SelectedValue.ToString().Trim();
                LINE_CD = cboLine.SelectedValue.ToString().Trim();


                while (gvwView.Columns.Count > 0)
                {
                    gvwView.Columns.RemoveAt(0);
                }

                DataTable dt = sbGetRework("Q", YMDF, YMDT, PLANT_CD, LINE_CD);

                if (dt.Rows.Count > 2)
                {
                    //TINH TOTAL
                    int iQty;
                    for (int i = 0; i <= dt.Rows.Count - 2; i++)
                    {
                        for (int j = 3; j <= dt.Columns.Count - 1; j++)
                        {
                            int.TryParse(dt.Rows[i][j].ToString(), out iQty);
                            total = total + iQty;
                        }
                        dt.Rows[i]["TOTAL"] = total;
                        total = 0;
                    }

                    if (int.Parse(dt.Rows[0]["TOTAL"].ToString()) > 0)
                    {
                        PER = (double.Parse(dt.Rows[1]["TOTAL"].ToString()) / double.Parse(dt.Rows[0]["TOTAL"].ToString())) * 100;

                        dt.Rows[2]["TOTAL"] = Math.Round(PER, 2);
                    }
                    else
                    {
                        dt.Rows[2]["TOTAL"] = 0;
                    }

                }
                grdView.DataSource = dt;

                Format_Grid();


                //---chart---//
                DataTable dtchart = sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);


                chartControl1.DataSource = dtchart;
                chartControl1.Series[0].ArgumentDataMember = "YMD";
                chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "REWORK" });
                chartControl1.Series[1].ArgumentDataMember = "YMD";
                chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "RATE" });
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


        private void Format_Grid()
        {
            gvwView.BeginUpdate();

            gvwView.Appearance.Row.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            for (int i = 0; i < gvwView.Columns.Count; i++)
            {
                if (gvwView.GetRowCellValue(0, gvwView.Columns[i].FieldName).ToString() == "ITEM")
                {
                    gvwView.SetRowCellValue(0, gvwView.Columns[i].FieldName, "Item");
                }

                if (gvwView.GetRowCellValue(0, gvwView.Columns[i].FieldName).ToString() == "TOTAL")
                {
                    gvwView.SetRowCellValue(0, gvwView.Columns[i].FieldName, "Total");
                }
                gvwView.Columns[i].AppearanceHeader.Font = new Font("Calibri", 18, FontStyle.Bold);


                gvwView.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvwView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                gvwView.Columns[i].OptionsFilter.AllowFilter = false;
                gvwView.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                gvwView.Columns[i].OptionsColumn.AllowEdit = false;
                gvwView.Columns[i].OptionsColumn.ReadOnly = true;
                gvwView.ColumnPanelRowHeight = 50;
                gvwView.RowHeight = 50;
                gvwView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //gvwView.Columns[i].AppearanceHeader.Fonts

                gvwView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
              //  gvwView.Columns[i].Width = 100;
                   
                gvwView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvwView.Columns[i].DisplayFormat.FormatString = "#,###.##";
            }
           
            
            gvwView.BestFitColumns();

            // gvwView.OptionsView.ColumnAutoWidth = false;
            gvwView.EndUpdate();
        }

        private void gvwView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            double temp = 0.0;
            
            if (e.Column.AbsoluteIndex >= 1 && e.CellValue != null)
            {
                if (gvwView.GetRowCellDisplayText(e.RowHandle, gvwView.Columns["ITEM"]).ToString() == "Rate")
                {
                    double.TryParse(gvwView.GetRowCellDisplayText(gvwView.RowCount - 1, gvwView.Columns[e.Column.ColumnHandle]).ToString(), out temp); //out
                    if (temp > 6)
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (temp > 3 && temp <= 6)
                    {

                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (temp <= 3)
                    {

                        e.Appearance.BackColor = Color.Green;
                        e.Appearance.ForeColor = Color.White;
                    }
                }
                if (e.Column.FieldName.Contains("TOTAL") && e.RowHandle != gvwView.RowCount -1)
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
            }

        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            cboPlant.SelectedValue = ComVar.Var._strValue1;
            cboLine.SelectedValue = ComVar.Var._strValue2;
            _time = 29;
        }
    }
}
