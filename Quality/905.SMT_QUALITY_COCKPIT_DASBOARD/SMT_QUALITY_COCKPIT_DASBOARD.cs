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
            MR,
            OVERALL,
            MI,
            NEW_COLOR,
            EXT_OSD,
            INBOUND,
            RAMP_UP,
            MOLD_REPAIR,
            DR_FCPP
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
                Dispose();
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
            AddUcToPanelTitle(pnTitle_MrIssues, Grp.MR, "MR Issues", "*Upper/Bottom" );
            AddUcToPanelTitle(pnTitle_Overall, Grp.OVERALL, "Overall Defective", "*PPM");
            AddUcToPanelTitle(pnTitle_MI, Grp.MI, "MI(Quality)", "*MI Gold(Inbound, FCPP, Bonding)");
            AddUcToPanelTitle(pnTitle_NewColor, Grp.NEW_COLOR, "New Colorway Readiness", "*P-BOM, SB, CFM Shoe\n && Lab Test");
            AddUcToPanelTitle(pnTitle_ExtOsd, Grp.EXT_OSD, "External OS&&D", "*Upper & Bottoms");
            AddUcToPanelTitle(pnTitle_Inbound, Grp.INBOUND, "Inbound", "*Inbound(%)");
            AddUcToPanelTitle(pnTitle_RampUp, Grp.RAMP_UP, "Ramp up", "*Days");
            AddUcToPanelTitle(pnTitle_MoldRepair, Grp.MOLD_REPAIR, "Mold Repair", "*Bottoms");
            AddUcToPanelTitle(pnTitle_DrFcpp, Grp.DR_FCPP, "DR/FCPP", "*DR(%)/FCPP($)");
        }

        private void AddUcToPanelTitle(Panel argPanel, Grp argName, string argTitle, string argText)
        {
            UC.UCTitle Uc = new UC.UCTitle();

            if(argPanel.Tag.ToString() == "MONTH")
            {
                Uc._typeDisplay = UC.UCTitle.TypeDisplay.MONTH;
            }
            else if (argPanel.Tag.ToString() == "SEASON")
            {
                Uc._typeDisplay = UC.UCTitle.TypeDisplay.SEASON;
            }
            Uc.setDisplay();
            Uc.SetTitle(argTitle);
            Uc.SetText(argText);
            Uc.Tag = argName.ToString();
            Uc.ValueChangeEvent += Uc_ValueChangeEvent;
            argPanel.Controls.Add(Uc);
            _dntTitle.Add(argName, Uc);
        }

        private void Uc_ValueChangeEvent(object sender, EventArgs e)
        {
            UC.UCTitle uc = (UC.UCTitle)sender;
            //MessageBox.Show(uc.Tag +": " + uc.GetValue());

            if (uc.Tag.ToString() == Grp.MR.ToString())
            {
                SetChart_MRIssues();
            }
            else if (uc.Tag.ToString() == Grp.RAMP_UP.ToString())
            {
                SetChart_RamUp();
            }
            else if (uc.Tag.ToString() == Grp.OVERALL.ToString())
            {
                SetChart_Overall();
            }
            else if (uc.Tag.ToString() == Grp.INBOUND.ToString())
            {
                SetChart_Inbound();
            }
            else if (uc.Tag.ToString() == Grp.DR_FCPP.ToString())
            {
                SetChart_DrFcpp();
            }
            else if (uc.Tag.ToString() == Grp.MOLD_REPAIR.ToString())
            {
                SetChart_MoldRepair();
            }
            else if (uc.Tag.ToString() == Grp.MI.ToString())
            {
                SetChart_MI();
            }
            else if (uc.Tag.ToString() == Grp.EXT_OSD.ToString())
            {
                SetChart_ExtOsd();
            }

        }
        #endregion

        #region Chart MR issues
        private async void SetChart_MRIssues()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable dtData = await Fn_SelectDataChart(Grp.MR.ToString(), _dntTitle[Grp.MR].GetValue());

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
            finally
            {
                this.Cursor = Cursors.Default;
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

        private void chtMrIssues_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            if (e.Item.Axis is AxisX)
            {
                string[] str = e.Item.Text.Split('_');
                if (str.Length >= 2)
                    e.Item.Text = str[0];
            }
        }
        #endregion

        #region Chart Ramp up issues
        private async void SetChart_RamUp()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.RAMP_UP.ToString(), _dntTitle[Grp.RAMP_UP].GetValue());

                chtRampUp.DataSource = dtData;
                chtRampUp.Series[0].ArgumentDataMember = "YMD";
                chtRampUp.Series[0].ValueDataMembers.AddRange(new string[] { "AVG_PLANT_RES" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Chart Overall Deffective
        private async void SetChart_Overall()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.OVERALL.ToString(), _dntTitle[Grp.OVERALL].GetValue());

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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Chart Inbound
        private async void SetChart_Inbound()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.INBOUND.ToString(), _dntTitle[Grp.INBOUND].GetValue());

                chtInbound.DataSource = dtData;
                chtInbound.Series[0].ArgumentDataMember = "MON";
                chtInbound.Series[0].ValueDataMembers.AddRange(new string[] { "DEF_RATE" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Chart Dr FCPP
        private async void SetChart_DrFcpp()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.DR_FCPP.ToString(), _dntTitle[Grp.DR_FCPP].GetValue());

                chtDrFcpp.DataSource = dtData;
                chtDrFcpp.Series[0].ArgumentDataMember = "MON";
                chtDrFcpp.Series[0].ValueDataMembers.AddRange(new string[] { "DR_QTY" });
                chtDrFcpp.Series[1].ArgumentDataMember = "MON";
                chtDrFcpp.Series[1].ValueDataMembers.AddRange(new string[] { "FCPP_QTY" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Chart Chart New Colorway Readliness
        private async void SetChart_NewColor()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.NEW_COLOR.ToString(), _dntTitle[Grp.NEW_COLOR].GetValue());

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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Chart Mold Repair
        private async void SetChart_MoldRepair()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                DataTable dtData = await Fn_SelectDataChart(Grp.MOLD_REPAIR.ToString(), _dntTitle[Grp.MOLD_REPAIR].GetValue());

                chtMoldRepair.DataSource = dtData;

                string[] column = { "OUTSOLE", "PHYLON", "PU", "IP", "DMP" };

                for(int i=0; i <column.Length;i++)
                {
                    chtMoldRepair.Series[i].ArgumentDataMember = "MON";
                    chtMoldRepair.Series[i].ValueDataMembers.AddRange(new string[] { column[i] });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Chart MI
        private async void SetChart_MI()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.MI.ToString(), _dntTitle[Grp.MI].GetValue());
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ChtMI_AddDataPoint(DataTable argData, int argSeries)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Chart External OS&D
        private async void SetChart_ExtOsd()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dtData = await Fn_SelectDataChart(Grp.EXT_OSD.ToString(), _dntTitle[Grp.EXT_OSD].GetValue());
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
            finally
            {
                this.Cursor = Cursors.Default;
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

        public async Task<DataTable> Fn_SelectDataChart(string argType, string argYm)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                MyOraDB.ShowErr = true;
                try
                {
                    string process_name = "PKG_SMT_QUALITY_COCKPIT_02.DASBOARD_DATA_SELECT";

                    MyOraDB.ReDim_Parameter(4);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "ARG_TYPE";
                    MyOraDB.Parameter_Name[1] = "ARG_FACTORY";
                    MyOraDB.Parameter_Name[2] = "ARG_YM";
                    MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                    MyOraDB.Parameter_Values[0] = argType;
                    MyOraDB.Parameter_Values[1] = "2110";
                    MyOraDB.Parameter_Values[2] = argYm;
                    MyOraDB.Parameter_Values[3] = "";

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
