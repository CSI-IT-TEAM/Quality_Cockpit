using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_DASBOARD : Form
    {
        public SMT_QUALITY_COCKPIT_DASBOARD()
        {
            InitializeComponent();
            AddUc();
        }
        int _time = 0;

        Dictionary<Grp, UC.UCTitle> _dntTitle = new Dictionary<Grp, UC.UCTitle>();

        enum Grp
        {
            MrIssues,
            Overall,
            MI,
            NewColor,
            ExtOsd,
            Inbound,
            RampUp,
            MoldRepair,
            DrFcpp
        }

        #region Event

        private void Form_Load(object sender, EventArgs e)
        {
           
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _time = 30;
                tmrTime.Start();
            }
            else
            {
                tmrTime.Stop();
            }

        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _time = 0;
                LoadDataChart();
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        #endregion Event


        private void LoadDataChart()
        {
            SetChart_MRIssues();
            SetChart_RamUp();
            SetChart_Overall();
            SetChart_Inbound();
            SetChart_DrFcpp();
            SetChart_NewColor();
            SetChart_MoldRepair();
            SetChart_MI();
            SetChart_ExtOsd();
        }

        

        #region AddUc
        private void AddUc()
        {
            AddUcToPanelTitle(pnTitle_MrIssues, Grp.MrIssues, "MR Issues", "*Upper/Bottom" );
            AddUcToPanelTitle(pnTitle_Overall, Grp.Overall, "Overal Defective", "*PPM");
            AddUcToPanelTitle(pnTitle_MI, Grp.MI, "MI(Quality)", "*MI Gold(Inbound, FCPP, Bonding)");
            AddUcToPanelTitle(pnTitle_NewColor, Grp.NewColor, "New Colorway Readiness", "*P-BOM, SB, CFM Shoe\n && Lab Test");
            AddUcToPanelTitle(pnTitle_ExtOsd, Grp.ExtOsd, "External OS&&D", "*Upper & Bottoms");
            AddUcToPanelTitle(pnTitle_Inbound, Grp.Inbound, "Inbound", "*Inbound(%)");
            AddUcToPanelTitle(pnTitle_RampUp, Grp.RampUp, "Ramp up", "*Days");
            AddUcToPanelTitle(pnTitle_MoldRepair, Grp.MoldRepair, "Mold Repair", "*Bottoms");
            AddUcToPanelTitle(pnTitle_DrFcpp, Grp.DrFcpp, "DR/FCPP", "*DR(%)/FCPP($)");
        }

        private void AddUcToPanelTitle(Panel argPanel, Grp argName, string argTitle, string argText)
        {
            UC.UCTitle Uc = new UC.UCTitle();
            Uc.SetTitle(argTitle);
            Uc.SetText(argText);
            argPanel.Controls.Add(Uc);
            _dntTitle.Add(argName, Uc);
        }
        #endregion

        #region Chart MR issues
        private async void SetChart_MRIssues()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("MR");

                chtMrIssues.Series[0].Points.Clear();
                chtMrIssues.Series[0].ArgumentScaleType = ScaleType.Qualitative;

                if (dtData == null || dtData.Rows.Count == 0) return;
                for (int iRow = 0; iRow < dtData.Rows.Count; iRow++)
                {
                    for (int iCol = 2; iCol < dtData.Columns.Count; iCol++)
                    {
                        string colName = dtData.Columns[iCol].ColumnName;
                        if (iCol == 4)
                        {
                            string season = dtData.Rows[iRow]["SEASON"].ToString();
                            chtMrIssues.Series[0].Points.Add(new SeriesPoint(colName + "\n" + season, dtData.Rows[iRow][iCol]));
                        } 
                        else if (iRow == 1 && iCol == 5)
                        {
                            string season = dtData.Rows[iRow]["SEASON"].ToString();
                            chtMrIssues.Series[0].Points.Add(new SeriesPoint(colName + "\n" + season, dtData.Rows[iRow][iCol]));
                        }
                        else
                        {
                          //  chtMrIssues.Series[0].Points.Add(new SeriesPoint(colName + "\n" + addBlank(iRow + 1), dtData.Rows[iRow][iCol]));
                            chtMrIssues.Series[0].Points.Add(new SeriesPoint(colName + "_" + iRow, dtData.Rows[iRow][iCol]));
                        }
                        
                    }
                    chtMrIssues.Series[0].Points.Add(new SeriesPoint(addBlank(iRow +1), Double.NaN));
                }

                ((XYDiagram)chtMrIssues.Diagram).AxisX.NumericScaleOptions.AutoGrid = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private string addBlank(int arg_i)
        {
            string str = "";
            for (int i = 0; i < arg_i; i++)
            {
                str += " ";
            }
            return str;
        }

        #endregion

        #region Chart Ramp up issues
        private async void SetChart_RamUp()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("RAMP_UP");

                chtRampUp.DataSource = dtData;
                chtRampUp.Series[0].ArgumentDataMember = "YMD";
                chtRampUp.Series[0].ValueDataMembers.AddRange(new string[] { "AVG_PLANT_RES" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Chart Overall Deffective
        private async void SetChart_Overall()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("OVERALL");

                chtOverall.DataSource = dtData;
                chtOverall.Series[0].ArgumentDataMember = "MON";
                chtOverall.Series[0].ValueDataMembers.AddRange(new string[] { "RET_QTY" });

                chtOverall.Series[1].ArgumentDataMember = "MON";
                chtOverall.Series[1].ValueDataMembers.AddRange(new string[] { "QC_QTY" });

                chtOverall.Series[2].ArgumentDataMember = "MON";
                chtOverall.Series[2].ValueDataMembers.AddRange(new string[] { "SEF_QTY" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Chart Inbound
        private async void SetChart_Inbound()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("INBOUND");

                chtInbound.DataSource = dtData;
                chtInbound.Series[0].ArgumentDataMember = "MON";
                chtInbound.Series[0].ValueDataMembers.AddRange(new string[] { "DEF_RATE" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }
        #endregion

        #region Chart Inbound
        private async void SetChart_DrFcpp()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("DR_FCPP");

                chtDrFcpp.DataSource = dtData;
                chtDrFcpp.Series[0].ArgumentDataMember = "MON";
                chtDrFcpp.Series[0].ValueDataMembers.AddRange(new string[] { "FCPP_QTY" });
                chtDrFcpp.Series[1].ArgumentDataMember = "MON";
                chtDrFcpp.Series[1].ValueDataMembers.AddRange(new string[] { "DR_QTY" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Chart Chart New Colorway Readliness
        private async void SetChart_NewColor()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("NEW_COLOR");

                double green, yellow;
                double.TryParse(dtData.Rows[0]["GREEN"].ToString(), out green);
                double.TryParse(dtData.Rows[0]["YELLOW"].ToString(), out yellow);

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    string lable = dtData.Rows[i]["YMD"].ToString();
                    double value;
                    double.TryParse(dtData.Rows[i]["AVG_COL"].ToString(), out value);

                    chtNewColor.Series[0].Points.Add(new SeriesPoint(lable, value));
                    if (value >= green)
                        chtNewColor.Series[0].Points[i].Color = Color.Green;
                    else if (value >= yellow)
                        chtNewColor.Series[0].Points[i].Color = Color.Yellow;
                    else
                        chtNewColor.Series[0].Points[i].Color = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Chart Mold Repair
        private async void SetChart_MoldRepair()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("MOLD");

                chtMoldRepair.DataSource = dtData;
                chtMoldRepair.Series[0].ArgumentDataMember = "MON";
                chtMoldRepair.Series[0].ValueDataMembers.AddRange(new string[] { "CNT" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Chart MI
        private async void SetChart_MI()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("MI");
                chtMI.Series[0].Points.Clear();
                chtMI.Series[1].Points.Clear();
                chtMI.Series[2].Points.Clear();
                chtMI.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                chtMI.Series[1].ArgumentScaleType = ScaleType.Qualitative;
                chtMI.Series[2].ArgumentScaleType = ScaleType.Qualitative;

                ChtMI_AddDataPoint(dtData.Select("DIV = 'FCPP'","YM").CopyToDataTable(), 0);
                ChtMI_AddDataPoint(dtData.Select("DIV = 'INBOUND'", "YM").CopyToDataTable(), 1);
                ChtMI_AddDataPoint(dtData.Select("DIV = 'BONDING'", "YM").CopyToDataTable(), 2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void ChtMI_AddDataPoint(DataTable argData, int argSeries)
        {
            try
            {
                for (int i = 0; i < argData.Rows.Count; i++)
                {
                    string lable = argData.Rows[i]["MON"].ToString();
                    double value;
                    double.TryParse(argData.Rows[i]["SCORE"].ToString(), out value);

                    chtMI.Series[argSeries].Points.Add(new SeriesPoint(lable, value));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Chart External OS&D
        private async void SetChart_ExtOsd()
        {
            try
            {
                DataTable dtData = await Fn_SelectDataChart("EXT_OSD");
                chtExtOsd.Series[0].Points.Clear();
                chtExtOsd.Series[1].Points.Clear();
                chtExtOsd.Series[0].ArgumentScaleType = ScaleType.Qualitative;
                chtExtOsd.Series[1].ArgumentScaleType = ScaleType.Qualitative;

                ChtExOsd_AddDataPoint(dtData.Select("DIV = 'BOTTOM'", "YM").CopyToDataTable(), 0);
                ChtExOsd_AddDataPoint(dtData.Select("DIV = 'UPPER'", "YM").CopyToDataTable(), 1);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void ChtExOsd_AddDataPoint(DataTable argData, int argSeries)
        {
            try
            {
                for (int i = 0; i < argData.Rows.Count; i++)
                {
                    string lable = argData.Rows[i]["MON"].ToString();
                    double value;
                    double.TryParse(argData.Rows[i]["OSD_RATE"].ToString(), out value);

                    chtExtOsd.Series[argSeries].Points.Add(new SeriesPoint(lable, value));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region Database

        public async Task<DataTable> Fn_SelectDataChart(string argType)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                MyOraDB.ShowErr = true;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.DASBOARD_DATA_SELECT";

                    MyOraDB.ReDim_Parameter(3);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_FACTORY";
                    MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                    MyOraDB.Parameter_Values[0] = argType;
                    MyOraDB.Parameter_Values[1] = "2110";
                    MyOraDB.Parameter_Values[2] = "";

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
        private void chtMrIssues_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            if (e.Item.Axis is AxisX)
            {
                string[] str = e.Item.Text.Split('_');
                if (str.Length >=2)
                    e.Item.Text = str[0];
            }
        }
    }
}
