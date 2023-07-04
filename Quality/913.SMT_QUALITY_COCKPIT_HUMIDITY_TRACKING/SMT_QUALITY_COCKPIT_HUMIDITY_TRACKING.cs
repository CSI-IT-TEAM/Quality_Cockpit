using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING : Form
    {
        public SMT_QUALITY_COCKPIT_HUMIDITY_TRACKING()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
            tmrAniNumber.Stop();
            timer1.Stop();
        }
        #region Variable
        bool isFirstLoad = true;
        List<AdvancedPanel> advLst = new List<AdvancedPanel>();
        #endregion
        private readonly string _strHeader = "       Humidity Tracking";
        int _time = 0;
        DataTable dtWarn = null;
        DataTable dtBlink = null;
        private int iHumidity = 0;
        int cAnimationNo = 0;
        Dictionary<Color, RepositoryItemProgressBar> progressBarColor = new Dictionary<Color, RepositoryItemProgressBar>();

        private void CheckLineInTable(string strLine = "")
        {
            try
            {
                dtWarn = SMT_MAT_WEATHER_MGNT("CHK_WARNING", "ALL", "ALL", DateTime.Now.ToString("yyyyMMdd"));
                CheckBlink();
                if (dtWarn != null)
                {
                    foreach (var btn in advLst)
                    {
                        if (btn.Tag != null)
                        {
                            foreach (DataRow dr in dtWarn.Rows)
                            {
                                if (dr["LINE_CD"].ToString().Equals(btn.Tag.ToString()))
                                {
                                    tmrWarning.Start();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void CheckBlink()
        {
            try
            {
                dtBlink = SMT_MAT_WEATHER_MGNT("CHECK");
                if (dtBlink != null)
                {
                    foreach (var btn in advLst)
                    {
                        if (btn.Tag != null)
                        {
                            foreach (DataRow dr in dtBlink.Rows)
                            {
                                if (dr["LINE_CD"].ToString().Equals(btn.Tag.ToString()))
                                {
                                    btn.EndColor = Color.Lime;
                                    btn.FlatBorderColor = Color.LimeGreen;
                                    btn.StartColor = Color.LimeGreen;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*Binding Data*/
        private void BindingData()
        {
            try
            {
                CheckLineInTable();
                SetData("Q_CHART");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private RepositoryItemProgressBar CreateProgressBar(Color color)
        {
            try
            {
                var pgrsbar = new RepositoryItemProgressBar();
                pgrsbar.Minimum = 0;
                pgrsbar.Maximum = 100;
                pgrsbar.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                pgrsbar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                pgrsbar.StartColor = color;
                pgrsbar.EndColor = color;
                pgrsbar.PercentView = true;
                pgrsbar.ShowTitle = false;
                pgrsbar.Step = 1;
                pgrsbar.LookAndFeel.UseDefaultLookAndFeel = false;
                return pgrsbar;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        private void SetData(string arg_type, string plant = "ALL", string line = "ALL")
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                chtHumi.DataSource = null;
                grdMain.DataSource = null;
                lblLine.Text = "";
                lblHumi.Text = "";
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
                        for (int i = 0; i < dtChart.Rows.Count; i++)
                        {
                            chtHumi.Series[0].Points.Add(new SeriesPoint(dtChart.Rows[i]["LINE_NM"].ToString(), dtChart.Rows[i]["HUMIDITY"]));
                            double rate;
                            double.TryParse(dtChart.Rows[i]["HUMIDITY"].ToString(), out rate);

                            chtHumi.Series[0].Points[i].Color = Color.FromArgb(255, 192, 0);
                            if (rate <= 70)
                            {
                                chtHumi.Series[0].View.Color = Color.Lime;
                                chtHumi.Series[0].Points[i].Color = Color.Lime;
                            }
                            else
                            {
                                chtHumi.Series[0].View.Color = Color.Red;
                                chtHumi.Series[0].Points[i].Color = Color.Red;
                            }
                        }

                        DataTable dtCharttmp = dtChart.Copy();
                        if (dtCharttmp != null && dtCharttmp.Rows.Count >= 0)
                        {
                            dtCharttmp = dtCharttmp.Select("HUMIDITY = MAX(HUMIDITY)", "LINE_CD").CopyToDataTable();
                            lblLine.Text = dtCharttmp.Rows[0]["LINE_NM"].ToString();
                            iHumidity = Convert.ToInt32(dtCharttmp.Rows[0]["HUMIDITY"]);
                            tmrAniNumber.Start();
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
            catch (Exception ex)
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
            if (_time >= 60)
            {
                _time = 0;
                BindingData();
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
                if (isFirstLoad)
                {
                    isFirstLoad = false;
                    //master production line
                    AdvancedPanel[] ButtonLst = new AdvancedPanel[] {
                    btnLocation_FTY1,
                    btnLocation_B,
                    btnLocation_C,
                    btnLocation_D,
                    btnLocation_E,
                    btnLocation_F,
                    btnLocation_N,
                    btnLocation_G,
                    btnLocation_H,
                    btnLocation_I,
                    btnLocation_J,
                    btnLocation_K,
                    btnLocation_L,
                    btnLocation_M
                };
                    foreach (var item in ButtonLst)
                    {
                        advLst.Add(item);
                    }
                }
                BindingData();
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
        private void gvwMain_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            try
            {
                var view = sender as BandedGridView;
                if (e.Column.AbsoluteIndex < 3) return;
                else
                {
                    int value = int.Parse(view.GetRowCellValue(e.RowHandle, "HUMIDITY").ToString());
                    var c = Color.Transparent;
                    if (value <= 70)
                    {
                        c = Color.Lime;
                    }
                    else
                    {
                        c = Color.Red;
                    }
                    if (!progressBarColor.ContainsKey(c))
                        progressBarColor.Add(c, CreateProgressBar(c));
                    e.RepositoryItem = progressBarColor[c];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*CLICK LOCATION*/
        private void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                string[] strList = control.Name.Split('_');
                if (strList.Length < 2) return;
                ComVar.Var._IsBack = true;
                switch (strList[1].ToString())
                {
                    case "L":
                        ComVar.Var._Area = "018";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "FTY1":
                        ComVar.Var._Area = "000";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "M":
                        ComVar.Var._Area = "019";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "C":
                        ComVar.Var._Area = "008";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "D":
                        ComVar.Var._Area = "010";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "E":
                        ComVar.Var._Area = "011";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "F":
                        ComVar.Var._Area = "012";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "N":
                        ComVar.Var._Area = "099";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "G":
                        ComVar.Var._Area = "013";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "H":
                        ComVar.Var._Area = "014";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "I":
                        ComVar.Var._Area = "015";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    case "J":
                        ComVar.Var._Area = "016";
                        CheckSeq(ComVar.Var._Area);
                        break;
                    default:
                        break;
                }
                _time = 60;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void CheckSeq(string strLine)
        {
            try
            {
                ComVar.Var._strValue1 = "";
                ComVar.Var._strValue5 = "";
                DataTable dtCheck = dtWarn.Copy();
                if (dtCheck != null)
                {
                    for (int i = 0; i < dtCheck.Rows.Count; i++)
                    {
                        if (dtCheck.Rows[i]["LINE_CD"].ToString().Equals(strLine))
                        {
                            ComVar.Var._strValue1 = dtCheck.Rows[i]["RST_DATE"].ToString();
                            ComVar.Var._strValue5 = dtCheck.Rows[i]["SEQ_ID"].ToString();
                        }
                    }
                    if (ComVar.Var._strValue1 != "" && ComVar.Var._strValue5 != "")
                    {
                        ComVar.Var.callForm = "914";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /*DATABASE*/
        private DataTable SMT_MAT_WEATHER_MGNT(string ARG_TYPE, string ARG_PLANT = "", string ARG_LINE = "", string ARG_DATE = "", string ARG_SEQ = "")
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            DataSet ds_ret;
            try
            {
                string process_name = "LMES.PKG_MAT_INV_TREND_MONITOR.SMT_MAT_WEATHER_MGNT_V2";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_PLANT";
                MyOraDB.Parameter_Name[2] = "V_P_LINE";
                MyOraDB.Parameter_Name[3] = "V_P_DATE";
                MyOraDB.Parameter_Name[4] = "V_P_SEQ";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR1";


                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;


                MyOraDB.Parameter_Values[0] = ARG_TYPE;
                MyOraDB.Parameter_Values[1] = ARG_PLANT;
                MyOraDB.Parameter_Values[2] = ARG_LINE;
                MyOraDB.Parameter_Values[3] = ARG_DATE;
                MyOraDB.Parameter_Values[4] = ARG_SEQ;
                MyOraDB.Parameter_Values[5] = "";


                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[process_name];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
        private void tmrAniNumber_Tick(object sender, EventArgs e)
        {
            try
            {
                Random r = new Random();
                cAnimationNo++;
                if (cAnimationNo >= 100)
                {
                    cAnimationNo = 0;
                    tmrAniNumber.Stop();
                    lblHumi.Text = string.Concat(string.Format("{0:n0}", iHumidity), "%");
                    if (iHumidity > 70)
                        lblHumi.ForeColor = Color.Red;
                    else
                        lblHumi.ForeColor = Color.Lime;
                }
                else
                {
                    lblHumi.Text = string.Concat(string.Format("{0:n0}", r.Next(1, 100)), "%");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void tmrWarning_Tick(object sender, EventArgs e)
        {
            try
            {
                if (dtWarn == null) return;
                foreach (var btn in advLst)
                {
                    if (btn.Tag != null)
                    {
                        foreach (DataRow dr in dtWarn.Rows)
                        {
                            if (dr["LINE_CD"].ToString().Equals(btn.Tag.ToString()) && dr["COLOR"].ToString().ToUpper().Equals("RED"))
                            {
                                if (btn.EndColor == Color.Lime && btn.FlatBorderColor == Color.LimeGreen && btn.StartColor == Color.LimeGreen)
                                {
                                    btn.EndColor = Color.FromArgb(255, 102, 0);
                                    btn.FlatBorderColor = Color.Red;
                                    btn.StartColor = Color.Red;
                                }
                                else
                                {
                                    btn.EndColor = Color.Lime;
                                    btn.FlatBorderColor = Color.LimeGreen;
                                    btn.StartColor = Color.LimeGreen;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
