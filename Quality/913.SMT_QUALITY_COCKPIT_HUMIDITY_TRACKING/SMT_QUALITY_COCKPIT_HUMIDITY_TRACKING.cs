using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING : Form
    {
        public SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }
        private readonly string _strHeader = "       Humidity Tracking";
        int _time = 0;

        private void SetData(string arg_type, string plant = "ALL", string line = "ALL")
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int color = 0;
                chtHumi.DataSource = null;
                grdMain.DataSource = null;
                DataTable dt = Data_Select(arg_type, DateTime.Now.ToString(), plant, line);
                if (dt == null || dt.Rows.Count == 0) return;

                DataTable dtChart = dt.Copy();
                if (dtChart.Rows.Count > 0)
                {
                    if (chtHumi.Series[0].Points.Count > 0)
                    {
                        chtHumi.Series[0].Points.Clear();
                    }
                    if (dtChart != null && dtChart.Rows.Count > 0)
                    {
                        grdMain.DataSource = dtChart;
                        FormatGrid(gvwMain);
                        chtHumi.Series[0].ArgumentScaleType = ScaleType.Qualitative;

                        for (int i = 0; i < dtChart.Rows.Count; i++)
                        {
                            chtHumi.Series[0].Points.Add(new SeriesPoint(dtChart.Rows[i]["LINE_NM"].ToString(), dtChart.Rows[i]["HUMIDITY"]));
                            color = Convert.ToInt32(dtChart.Rows[i]["HUMIDITY"]);
                            if (color < 70)
                            {
                                chtHumi.Series[0].Points[i].Color = Color.Lime;
                            }
                            else
                            {
                                chtHumi.Series[0].Points[i].Color = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void SetDataChartMonth(string arg_type, string plant = "ALL", string line = "ALL")
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                chthumiMon.DataSource = null;
                grdBase.DataSource = null;
                string color = "";
                DataTable dt = Data_Select(arg_type, DateTime.Now.ToString(), plant, line);
                if (dt == null || dt.Rows.Count == 0) return;

                DataTable dtChart = dt.Copy();
                if (dtChart.Rows.Count > 0)
                {
                    grdBase.DataSource = dtChart;
                    FormatGrid(gvwBase);

                    chthumiMon.Series.Clear();
                    chthumiMon.DataSource = dtChart;
                    chthumiMon.SeriesDataMember = "FAC_NM";
                    chthumiMon.SeriesTemplate.ArgumentDataMember = "RST_DATE";
                    chthumiMon.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "HUMIDITY" });
                    chthumiMon.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void FormatGrid(BandedGridView gvwGrid)
        {
            try
            {
                for (int i = 0; i < gvwGrid.Columns.Count; i++)
                {
                    gvwGrid.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwGrid.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Regular);
                }
                gvwGrid.OptionsBehavior.Editable = false;
                gvwGrid.OptionsBehavior.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region DB
        private DataTable Data_Select(string argType, string date, string plant, string line)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.SEPHIROTH;
            string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_04.SP_GET_HUMIDITY_TRACKING";

            try
            {
                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE";
                MyOraDB.Parameter_Name[2] = "V_P_PLANT";
                MyOraDB.Parameter_Name[3] = "V_P_LINE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = argType;
                MyOraDB.Parameter_Values[1] = "";
                MyOraDB.Parameter_Values[2] = plant;
                MyOraDB.Parameter_Values[3] = line;
                MyOraDB.Parameter_Values[4] = "";

                MyOraDB.Add_Select_Parameter(true);
                DataSet retDS = MyOraDB.Exe_Select_Procedure();
                if (retDS == null) return null;

                return retDS.Tables[process_name];
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                SetData("Q_CHART");
                SetDataChartMonth("Q_CHART_MON");
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
        private void SMT_QUALITY_COCKPIT_EXTERNAL_OSD_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 0;
                SetData("Q_CHART");
                SetDataChartMonth("Q_CHART_MON");
                timer1.Start();

            }
            else
            {
                timer1.Stop();
                Dispose();
            }
        }
        private void gvwMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "HUMIDITY")
                {
                    e.DisplayText = Convert.ToInt32(e.CellValue) + "%";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void gvwBase_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "HUMIDITY")
                {
                    e.DisplayText = Convert.ToInt32(e.CellValue) + "%";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*CLICK LOCATION*/
        private void lbl_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                string[] strList = control.Name.Split('_');
                if (strList.Length < 2) return;
                switch (control.Name.ToString())
                {
                    case "lbl_L_1":
                        //MessageBox.Show(control.Tag.ToString() + "___" + strList[1]);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
