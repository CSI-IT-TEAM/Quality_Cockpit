using DevExpress.XtraCharts;
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
    public partial class SMT_QUALITY_COCKPIT_REWORK : Form
    {
        public SMT_QUALITY_COCKPIT_REWORK()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Daily Rework";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");

        #region Load-Visible Change-Timer
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
                chartControl1.Series[0].Points.Clear();
                chartControl1.Series[1].Points.Clear();
                grdView.DataSource = null;
                _time = 0;
                btnSearch_Click(null, null);

                timer1.Start();
            }
            else
            {
                timer1.Stop();
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

            }
        }

        #endregion

        #region Combo
        private void LoadCombo(string arg_type)
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombo("COMBO_LINE");
        }
        #endregion


        #region

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion


        #region Database
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

        public async Task<DataTable> sbGetRework(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {
            return await Task.Run(() => {
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
            });
        }

        public async Task<DataTable> sbGetRework_Chart(string ARG_QTYPE, string ARG_YMDF, string ARG_YMDT, string ARG_PLANT, string ARG_LINE)
        {
            return await Task.Run(() => {
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
            });
        }
   

        #endregion DB


        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SetGrid();
                
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

        private async void SetChart()
        {
            string YMDF, YMDT, PLANT_CD, LINE_CD;
            YMDF = dtpYMD.DateTime.ToString("yyyyMMdd");
            YMDT = dtpYMDT.DateTime.ToString("yyyyMMdd");
            PLANT_CD = cboPlant.SelectedValue.ToString().Trim();
            LINE_CD = cboLine.SelectedValue.ToString().Trim();

            DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);

            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (dtchart == null) return;
            for (int i = 0; i <= dtchart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(dtchart.Rows[i]["YMD"].ToString(), dtchart.Rows[i]["REWORK"]));
                chartControl1.Series[1].Points.Add(new SeriesPoint(dtchart.Rows[i]["YMD"].ToString(), dtchart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(dtchart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate > 6)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Red;
                }
                else if (rate > 3)
                {
                    chartControl1.Series[0].Points[i].Color = Color.Yellow;
                }
                else 
                {
                    chartControl1.Series[0].Points[i].Color = Color.Green;
                }
            }
        }


        private async void SetGrid()
        {
            try
            {
                gvwView.BeginUpdate();
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

                DataTable dt = await sbGetRework("Q", YMDF, YMDT, PLANT_CD, LINE_CD);

                if (dt != null && dt.Rows.Count > 2)
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
                        dt.Rows[i]["ITEM"] = dt.Rows[i]["ITEM"].ToString() + "(Prs)";
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
                    dt.Rows[2]["ITEM"] = dt.Rows[2]["ITEM"].ToString() + "(%)";
                }

                grdView.DataSource = dt;
                gvwView.Appearance.Row.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);

                gvwView.Columns[0].Caption = " ";
                gvwView.Columns[1].Caption = "Total";

                for (int i = 0; i < gvwView.Columns.Count; i++)
                {

                    gvwView.Columns[i].AppearanceHeader.Font = new Font("Calibri", 18, FontStyle.Bold);


                    gvwView.Columns[i].AppearanceCell.Options.UseTextOptions = true;

                    if (i==0)
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
                    //gvwView.Columns[i].AppearanceHeader.Fonts

                    gvwView.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    //  

                    gvwView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvwView.Columns[i].DisplayFormat.FormatString = "#,###.##";
                }


               // gvwView.BestFitColumns();
                

                SetChart();
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

        private void gvwView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            double temp = 0.0;
            
            if (e.Column.AbsoluteIndex >= 1 && e.CellValue != null)
            {
                if (gvwView.GetRowCellDisplayText(e.RowHandle, gvwView.Columns["ITEM"]).ToString().ToUpper().Contains("RATE"))
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

        

        private void gvwView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == _CurrentDay)
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

        private void gvwView_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column == null) return;
            if (e.Column.FieldName == _CurrentDay)
            {
                Rectangle rect = e.Bounds;
                rect.Inflate(new Size(1, 1));

                Brush brush = new SolidBrush(Color.DodgerBlue);
                e.Graphics.FillRectangle(brush, rect);

                ////draw bottom
                //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y + rect.Height - 1, rect.X + rect.Width, rect.Y + rect.Height - 1);
                //// draw top
               // e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X + rect.Width, rect.Y);

                // draw right
               // e.Graphics.DrawLine(pen_vertical, rect.X + rect.Width, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);


                // draw left
                //e.Graphics.DrawLine(pen_horizental, rect.X, rect.Y, rect.X, rect.Y + rect.Height);

                string text = e.Column.Caption == "" ? e.Column.FieldName : e.Column.Caption;
                e.Graphics.DrawString(text, e.Appearance.Font, new SolidBrush(Color.White), rect, e.Appearance.GetStringFormat());

                e.Handled = true;
            }
        }
    }
}
