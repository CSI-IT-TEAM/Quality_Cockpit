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
        private UC.UC_COMPARE_WEEK uc_compare_week = new UC.UC_COMPARE_WEEK();
        string _strType = "D";
        int _time = 0;

        //private void SetData(string arg_type, bool arg_load = true)
        //{
        //    try
        //    {
        //        if(arg_load)
        //        {
                  
        //            pnBody2.Visible = false;
        //            pnBody1.Visible = true;
        //        }
                
                

        //        chartControl1.DataSource = null;
        //        DataSet ds = Data_Select(arg_type);
        //        if (ds == null || ds.Tables.Count == 0) return;
        //        DataTable dtData = ds.Tables[0];
        //        DataTable dtDate = ds.Tables[1];



        //        chartControl1.DataSource = dtData;
        //        chartControl1.Series[0].ArgumentDataMember = "MACHINE_CD";
        //        chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "OCR_TIME" });
        //        gridControl1.DataSource = dtData;


        //        for (int i = 0; i < gridView1.Columns.Count; i++)
        //        {

        //            gridView1.Columns[i].OptionsColumn.ReadOnly = true;
        //            gridView1.Columns[i].OptionsColumn.AllowEdit = false;
        //            if (i <= 4)
        //            {
        //                gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //                gridView1.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
        //            }
        //            //  gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            // gridView1.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

        //            //if (i <= 2)
        //            //    gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
        //            //else
        //            //    gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

        //            if (i == 4)
        //                gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;

        //            if (i == gridView1.Columns.Count - 1)
        //                gridView1.Columns[i].AppearanceCell.Font = new Font("Calibri", 16, FontStyle.Bold);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //        throw;
        //    }

        //}

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
      

        
        private void LoadForm()
        {
            // load_combo();

            //SetButtonClick(_strType);
            //SetHeader(_strType);
            
            //SetData(_strType);

        

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
        private DataSet Data_Select(string argType, string argDate = "")
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "MES.PKG_SMT_SCADA_COCKPIT.PM_SELECT";

            MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
            MyOraDB.Parameter_Name[1] = "ARG_DATE";
            MyOraDB.Parameter_Name[2] = "OUT_CURSOR";
            MyOraDB.Parameter_Name[3] = "OUT_CURSOR2";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = argDate;
            MyOraDB.Parameter_Values[2] = "";
            MyOraDB.Parameter_Values[3] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS;
        }


        private DataTable Data_Select_Machine(string argType, string argMachine)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(3);
            MyOraDB.Process_Name = "MES.PKG_SMT_SCADA_COCKPIT.PM_SELECT_MACHINE";

            MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
            MyOraDB.Parameter_Name[1] = "ARG_MACHINE";
            MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = argMachine;
            MyOraDB.Parameter_Values[2] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS.Tables[0];
        }

        private DataSet Data_Select_Compare(string argType, string argDate = "")
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "MES.PKG_SMT_SCADA_COCKPIT.PM_SELECT_COMPARE";

            MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
            MyOraDB.Parameter_Name[1] = "ARG_DATE";
            MyOraDB.Parameter_Name[2] = "OUT_CURSOR";
            MyOraDB.Parameter_Name[3] = "OUT_CURSOR2";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = argDate;
            MyOraDB.Parameter_Values[2] = "";
            MyOraDB.Parameter_Values[3] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS;
        }

        #endregion DB



        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            //_time++;
            //if (_time >= 30)
            //{
            //    _time = 0;
            //    //SetData(_strType, false);
            //}

            if (_time < 2)
                _time++;
            if (_time == 2)
            {
                _time++;
            //    if (!flag)
            //        load_combo();
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
            string YMDF,YMDT,PLANT_CD, LINE_CD;
            int total=0;
            double PER = 0;
            YMDF=dtpYMD.DateTime.ToString("yyyyMMdd") ;
            YMDT=dtpYMDT.DateTime.ToString("yyyyMMdd") ;
            PLANT_CD=cboPlant.SelectedValue.ToString().Trim();
            LINE_CD=cboLine.SelectedValue.ToString().Trim();


            while (gvwView.Columns.Count > 0)
            {
                gvwView.Columns.RemoveAt(0);
            }

           DataTable dt = sbGetRework("Q", YMDF, YMDT, PLANT_CD, LINE_CD);

           if (dt.Rows.Count > 2)
           {
               //TINH TOTAL
               for (int i = 0; i <= dt.Rows.Count - 2; i++)
               {
                   for (int j = 3; j <= dt.Columns.Count - 1; j++)
                   {
                       total = total + int.Parse(dt.Rows[i][j].ToString());
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


        private void Format_Grid()
        {
            gvwView.BeginUpdate();


           
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
                gvwView.Columns[i].Width = 100;
                   
                gvwView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvwView.Columns[i].DisplayFormat.FormatString = "#,###.##";
              


               // gvwView.Columns[1].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
               // gvwView.Columns[i].Caption = gvwView.Columns[i].FieldName.ToString().Replace("'", "");


            }
           
            gvwView.Appearance.Row.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Regular);
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

                            e.Appearance.BackColor = Color.LightGreen;
                            e.Appearance.ForeColor = Color.Black;
                        }
                    }
                    if (e.Column.FieldName.Contains("TOTAL"))
                    {
                        e.Appearance.ForeColor = Color.Blue;
                    }
                }

            
        }
      
    }
}
