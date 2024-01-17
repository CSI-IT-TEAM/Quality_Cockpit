using DevExpress.Skins;
using DevExpress.XtraCharts;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using OS_DSF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM
{
    public partial class SMT_I_TMS_MFG_LT : Form
    {   
        public SMT_I_TMS_MFG_LT()
        {
            InitializeComponent();
            tmrLoad.Stop();  
            tmrAnimation.Stop();
        }
        UC.UC_BTN_NAV[] ucBtns = new UC.UC_BTN_NAV[13];
        List<ButtonModel> models = new List<ButtonModel>();
        int _Tag = 1, cCount = 0, cCountAni = 0, LT_LAST = 0, LT_CURR = 0;
        const int ReloadTime = 60;
        Random rand = new Random();
        string _season_cd = "", _season_nm = "";

        Random r = new Random();
        bool isLoad = false;
        // private string[] _arrMonthShortName = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //private string[] _arrMonthShortName = { "Model", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        #region CreateDatatable

        private DataTable GET_MODEL_SEASON(string V_P_TYPE, string V_P_YEAR, string V_P_SEASON)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            MyOraDB.ShowErr = true;

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "PKG_SMT_I_TMS.SP_GET_MFG_LT";

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_YEAR";
            MyOraDB.Parameter_Name[2] = "V_P_SEASON";
            MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = V_P_TYPE;
            MyOraDB.Parameter_Values[1] = V_P_YEAR;
            MyOraDB.Parameter_Values[2] = V_P_SEASON;
            MyOraDB.Parameter_Values[3] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS.Tables[MyOraDB.Process_Name];
        }

        private DataTable GET_MFG_LT(string V_P_TYPE, string V_P_YEAR, string V_P_SEASON)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            MyOraDB.ShowErr = true;

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "PKG_SMT_I_TMS.SP_GET_MFG_LT";

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_YEAR";
            MyOraDB.Parameter_Name[2] = "V_P_SEASON";
            MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = V_P_TYPE;
            MyOraDB.Parameter_Values[1] = V_P_YEAR;
            MyOraDB.Parameter_Values[2] = V_P_SEASON;
            MyOraDB.Parameter_Values[3] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS.Tables[MyOraDB.Process_Name];
        }

        private DataTable CreateDataTable()
        {
            try
            {
                // Create a DataTable and add two Columns to it
                DataTable dt = new DataTable();
                dt.Columns.Add("DIV", typeof(string));
                dt.Columns.Add("VALUES1", typeof(int));
                dt.Columns.Add("VALUES2", typeof(int));
                dt.Columns.Add("VALUES3", typeof(int));
                dt.Columns.Add("VALUES4", typeof(int));

                for (int i = 0; i < 10; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["DIV"] = "Production Days " + (i + 1);
                    dr["VALUES1"] = r.Next(100, 500);
                    dr["VALUES2"] = r.Next(100, 500);
                    dr["VALUES3"] = r.Next(100, 500);
                    dr["VALUES4"] = r.Next(100, 500);
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }

        private DataTable CreateDataTable_Tree()
        {
            try
            {
                // Create a DataTable and add two Columns to it
                DataTable dt = new DataTable();
                dt.Columns.Add("PARENTID", typeof(string));
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("MENU_NM", typeof(string));
                dt.Columns.Add("ID_NAME", typeof(string));

                DataTable dtModel = GET_MODEL_SEASON("TREE", DateTime.Now.ToString("yyyy"), _season_cd);
                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["PARENTID"] = "000";
                        dr["ID"] = _season_cd;
                        dr["MENU_NM"] = _season_nm;
                        dr["ID_NAME"] = _season_cd;
                        dt.Rows.Add(dr);
                    }
                    for (int ifac = 0; ifac < 1; ifac++)
                    {
                        for (int i = 0; i < dtModel.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["PARENTID"] = dt.Rows[ifac]["ID"].ToString();
                            dr["ID"] = dt.Rows[ifac]["ID"].ToString() + "_" + dtModel.Rows[i]["CODE"].ToString();
                            dr["MENU_NM"] = dtModel.Rows[i]["NAME"].ToString();
                            dr["ID_NAME"] = dt.Rows[ifac]["ID"].ToString() + "_" + dtModel.Rows[i]["CODE"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private DataTable CreateDataTable2()
        {
            try
            {
                // Create a DataTable and add two Columns to it
                DataTable dt = new DataTable();
                dt.Columns.Add("YM_LABEL", typeof(string));
                dt.Columns.Add("VALUES", typeof(int));
                for (int i = 0; i < 20; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["YM_LABEL"] = string.Concat("Model ", (i + 1));
                    dr["VALUES"] = r.Next(7, 35);
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }
        #endregion




        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }
        private void InitNavButton()
        {
            try
            {
                int iDx = 0;

                for (int irow = 0; irow < tblLeft.RowCount; irow++)
                {
                    for (int iCol = 0; iCol < tblLeft.ColumnCount; iCol++)
                    {

                        UC.UC_BTN_NAV ucBtn = new UC.UC_BTN_NAV();
                        ucBtn.Tag = iDx + 1;
                        ucBtn.OnUcClick += OnUcClick;
                        ucBtns[iDx] = ucBtn;
                        ucBtn.SetData(models[iDx]);
                        tblLeft.Controls.Add(ucBtn, iCol, irow);
                        iDx++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InitModels()
        {
            models.Add(new ButtonModel { HEADER_TEXT = "Season", DESCR_TEXT = "Season", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Model", DESCR_TEXT = "Model", BTN_CODE = "MODEL" });
            models.Add(new ButtonModel { HEADER_TEXT = "Style", DESCR_TEXT = "Style", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Order Qty", DESCR_TEXT = "Order Qty", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Schedule", DESCR_TEXT = "Schedule", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Material Set", DESCR_TEXT = "Material Set Balance", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Bottom", DESCR_TEXT = "Bottom", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Upper", DESCR_TEXT = "Upper", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Production Qty", DESCR_TEXT = "Production Qty", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Accumulate", DESCR_TEXT = "Accumulate", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Remaining", DESCR_TEXT = "Remaining", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Inventory", DESCR_TEXT = "Inventory", BTN_CODE = "SEASON" });
            models.Add(new ButtonModel { HEADER_TEXT = "Shipping Date", DESCR_TEXT = "Shipping Date", BTN_CODE = "SEASON" });
        }

        private void setControl (string sSEASON, A1Panel aLT_SEASON, A1Panel aQTY_SEASON, DevExpress.XtraEditors.SimpleButton bSEASON)
        {
            if(sSEASON == "SU")
            {
                aLT_SEASON.BorderColor = Color.FromArgb(255, 128, 0);
                aLT_SEASON.GradientEndColor = Color.Orange;
                aLT_SEASON.GradientStartColor = Color.FromArgb(255, 128, 0);

                aQTY_SEASON.BorderColor = Color.FromArgb(255, 128, 0);
                aQTY_SEASON.GradientEndColor = Color.Orange;
                aQTY_SEASON.GradientStartColor = Color.FromArgb(255, 128, 0);

                bSEASON.Appearance.ForeColor = Color.FromArgb(255, 128, 0);
            }
            else if (sSEASON == "FA")
            {
                aLT_SEASON.BorderColor = Color.Purple;
                aLT_SEASON.GradientEndColor = Color.FromArgb(255, 128, 255);
                aLT_SEASON.GradientStartColor = Color.Purple;

                aQTY_SEASON.BorderColor = Color.Purple;
                aQTY_SEASON.GradientEndColor = Color.FromArgb(255, 128, 255);
                aQTY_SEASON.GradientStartColor = Color.Purple;

                bSEASON.Appearance.ForeColor = Color.Purple;
            }
            else if (sSEASON == "HO")
            {
                aLT_SEASON.BorderColor = Color.DodgerBlue;
                aLT_SEASON.GradientEndColor = Color.DeepSkyBlue;
                aLT_SEASON.GradientStartColor = Color.DodgerBlue;

                aQTY_SEASON.BorderColor = Color.DodgerBlue;
                aQTY_SEASON.GradientEndColor = Color.DeepSkyBlue;
                aQTY_SEASON.GradientStartColor = Color.DodgerBlue;

                bSEASON.Appearance.ForeColor = Color.DodgerBlue;
            }
            else if (sSEASON == "SP")
            {
                aLT_SEASON.BorderColor = Color.Green;
                aLT_SEASON.GradientEndColor = Color.FromArgb(0, 192, 0);
                aLT_SEASON.GradientStartColor = Color.Green;

                aQTY_SEASON.BorderColor = Color.Green;
                aQTY_SEASON.GradientEndColor = Color.FromArgb(0, 192, 0);
                aQTY_SEASON.GradientStartColor = Color.Green;

                bSEASON.Appearance.ForeColor = Color.Green;
            }

        }

        private void load_data_season()
        {
            try
            {                
                DataTable dtSeason = GET_MFG_LT("SEASON", DateTime.Now.ToString("yyyy"), _season_cd);                
                if (dtSeason != null && dtSeason.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSeason.Rows.Count; i++)
                    {
                        //if(dtSeason.Rows[i]["SEASON"].ToString().Equals("SU"))
                        //{
                        //    lblQTY_SEASON1.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                        //    lblLT_SEASON1.Text = string.Concat(string.Format("{0:n1}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                        //}
                        //else if (dtSeason.Rows[i]["SEASON"].ToString().Equals("FA"))
                        //{
                        //    lblQTY_SEASON2.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                        //    lblLT_SEASON2.Text = string.Concat(string.Format("{0:n1}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                        //}
                        //else if (dtSeason.Rows[i]["SEASON"].ToString().Equals("HO"))
                        //{
                        //    lblQTY_SEASON3.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                        //    lblLT_SEASON3.Text = string.Concat(string.Format("{0:n1}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                        //}
                        //else if (dtSeason.Rows[i]["SEASON"].ToString().Equals("SP"))
                        //{
                        //    lblQTY_SEASON4.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                        //    lblLT_SEASON4.Text = string.Concat(string.Format("{0:n1}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                        //}
                        if (dtSeason.Rows[i]["RN"].ToString().Equals("1"))
                        {
                            lblQTY_SEASON1.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                            lblLT_SEASON1.Text = string.Concat(string.Format("{0:n2}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                            lblSEASON1.Text = dtSeason.Rows[i]["SEASON_NM"].ToString();
                            btnSEASON1.Tag = dtSeason.Rows[i]["YEAR_SEASON"].ToString();
                            setControl(dtSeason.Rows[i]["SEASON"].ToString(), pnLT_SEASON1, pnQTY_SEASON1, btnSEASON1);
                        }
                        else if (dtSeason.Rows[i]["RN"].ToString().Equals("2"))
                        {
                            lblQTY_SEASON2.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                            lblLT_SEASON2.Text = string.Concat(string.Format("{0:n2}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                            lblSEASON2.Text = dtSeason.Rows[i]["SEASON_NM"].ToString();
                            btnSEASON2.Tag = dtSeason.Rows[i]["YEAR_SEASON"].ToString();
                            setControl(dtSeason.Rows[i]["SEASON"].ToString(), pnLT_SEASON2, pnQTY_SEASON2, btnSEASON2);
                        }
                        else if (dtSeason.Rows[i]["RN"].ToString().Equals("3"))
                        {
                            lblQTY_SEASON3.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                            lblLT_SEASON3.Text = string.Concat(string.Format("{0:n2}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                            lblSEASON3.Text = dtSeason.Rows[i]["SEASON_NM"].ToString();
                            btnSEASON3.Tag = dtSeason.Rows[i]["YEAR_SEASON"].ToString();
                            setControl(dtSeason.Rows[i]["SEASON"].ToString(), pnLT_SEASON3, pnQTY_SEASON3, btnSEASON3);
                        }
                        else if (dtSeason.Rows[i]["RN"].ToString().Equals("4"))
                        {
                            lblQTY_SEASON4.Text = string.Concat(string.Format("{0:n0}", dtSeason.Rows[i]["ORDER_QTY"]), " Prs");
                            lblLT_SEASON4.Text = string.Concat(string.Format("{0:n2}", dtSeason.Rows[i]["LT_QTY"]), " Days");
                            lblSEASON4.Text = dtSeason.Rows[i]["SEASON_NM"].ToString();
                            btnSEASON4.Tag = dtSeason.Rows[i]["YEAR_SEASON"].ToString();
                            setControl(dtSeason.Rows[i]["SEASON"].ToString(), pnLT_SEASON4, pnQTY_SEASON4, btnSEASON4);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        DataTable dtLeadtime = null;
        private void load_data_leadtime()
        {
            try
            {
                dtLeadtime = GET_MFG_LT("LEADTIME", DateTime.Now.ToString("yyyy"), _season_cd);
                tmrAnimation.Start();
                //if (dtLeadtime != null && dtLeadtime.Rows.Count > 0)
                //{
                //    grdView.DataSource = dtLeadtime;
                //    for (int i = 0; i < gvwView.Columns.Count; i++)
                //    {
                //        if (i < 1)
                //        {
                //            gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //        }
                //        else
                //        {
                //            gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                //            gvwView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //            gvwView.Columns[i].DisplayFormat.FormatString = "#,0.##";
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                return;
            }
        }

        DataTable dtModel = null;
        private void load_data_model()
        {   
            try
            {
                dtModel = GET_MFG_LT("MODEL", DateTime.Now.ToString("yyyy"), _season_cd);
                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    lblLT_Last.Text = dtModel.Rows[0]["LT_LAST"].ToString();
                    lblLT_This.Text = dtModel.Rows[0]["LT_THIS"].ToString();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void BindingChart()
        {
            try
            {
                DataTable dt = null;
                chart1.DataSource = null;
                chart2.DataSource = null;
                chart3.DataSource = null;
                chart4.DataSource = null;
                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    dt = dtModel.Copy();
                    int cnt1 = 0, cnt2 = 0;
                    string _strname = "";
                    foreach (TreeListNode node in treeList.Nodes)
                    {
                        cnt1 = node.RootNode.Nodes.Count;
                        foreach (TreeListNode node1 in node.RootNode.Nodes)
                        {
                            if (!node1.Checked)
                            {
                                cnt2++;
                                if (dt.Select("MODEL_CD <> '" + node1.GetValue("ID").ToString().Split('_')[1] + "'", "").Count() > 0)
                                {
                                    dt = dt.Select("MODEL_CD <> '" + node1.GetValue("ID").ToString().Split('_')[1] + "'", "").CopyToDataTable();                                   
                                }
                                _strname = node1.GetValue("ID").ToString().Split('_')[1];
                            }
                        }
                    }
                    if (cnt1 == cnt2) dt.Rows.RemoveAt(0);

                    DataTable dtSeries = null;
                    if (dt.Select("", "MODEL_NAME").Count() > 0)
                    {
                        dtSeries = dt.Select("", "MODEL_NAME").CopyToDataTable();
                        load_chart(dtSeries, chart1, "MODEL_NAME", "ORDER_QTY");
                    }

                    if (dt.Select("FG_INV_QTY <> 0", "MODEL_NAME").Count() > 0)
                    {
                        dtSeries = dt.Select("FG_INV_QTY <> 0", "MODEL_NAME").CopyToDataTable();
                        load_chart(dtSeries, chart2, "MODEL_NAME", "FG_INV_QTY");
                    }

                    if (dt.Select("REMAIN_QTY <> 0", "MODEL_NAME").Count() > 0)
                    {
                        dtSeries = dt.Select("", "MODEL_NAME").CopyToDataTable();
                        load_chart2(dtSeries, chart5, "MODEL_NAME", "UCC_QTY", "REMAIN_QTY");
                    }

                    //if (dt.Select("REMAIN_QTY <> 0", "MODEL_NAME").Count() > 0)
                    //{
                    //    dtSeries = dt.Select("REMAIN_QTY <> 0", "MODEL_NAME").CopyToDataTable();
                    //    load_chart(dtSeries, chart3, "MODEL_NAME", "REMAIN_QTY");
                    //}

                    if (dt.Select("LT_QTY IS NOT NULL", "MODEL_NAME").Count() > 0)
                    {
                        dtSeries = dt.Select("LT_QTY IS NOT NULL", "MODEL_NAME").CopyToDataTable();
                        load_chart(dtSeries, chart4, "MODEL_NAME", "LT_QTY");
                    }
                }
            }
            catch(Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void load_chart(DataTable dt, ChartControl _chart, string _argument, string _value)
        {
            _chart.DataSource = dt;
            _chart.Series[0].ArgumentDataMember = _argument;
            _chart.Series[0].ValueDataMembers.AddRange(new string[] { _value });

            double max = Convert.ToDouble(dt.AsEnumerable()
                        .Max(row => row[_value]));
            double min = Convert.ToDouble(dt.AsEnumerable()
                        .Min(row => row[_value]));

            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.Auto = false;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.MaxValue = max;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.MinValue = min;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.SideMarginsValue = 1;
            XYDiagram diagram = (XYDiagram)_chart.Diagram;
            _chart.CrosshairOptions.CrosshairLabelMode = DevExpress.XtraCharts.CrosshairLabelMode.ShowCommonForAllSeries;
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            diagram.EnableAxisXScrolling = true;

            diagram.EnableAxisYZooming = true;
            //diagram.Panes[0].EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.True;

            if (dt.Rows.Count > 8)
            {
                diagram.AxisX.VisualRange.SetMinMaxValues(dt.Rows[0][_argument], dt.Rows[8][_argument]);
            }
            else
            {
                diagram.AxisX.VisualRange.SetMinMaxValues(dt.Rows[0][_argument], dt.Rows[dt.Rows.Count - 1][_argument]);
            }
        }

        private void load_chart2(DataTable dt, ChartControl _chart, string _argument, string _value1, string _value2)
        {
            _chart.DataSource = dt;
            _chart.Series[0].ArgumentDataMember = _argument;
            _chart.Series[0].ValueDataMembers.AddRange(new string[] { _value1 });
            _chart.Series[1].ArgumentDataMember = _argument;
            _chart.Series[1].ValueDataMembers.AddRange(new string[] { _value2 });

            double max = Convert.ToDouble(dt.AsEnumerable()
                        .Max(row => row[_value1]));
            double min = Convert.ToDouble(dt.AsEnumerable()
                        .Min(row => row[_value1]));

            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.Auto = false;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.MaxValue = max;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.MinValue = min;
            //((XYDiagram)_chart.Diagram).AxisY.WholeRange.SideMarginsValue = 1;
            XYDiagram diagram = (XYDiagram)_chart.Diagram;
            _chart.CrosshairOptions.CrosshairLabelMode = DevExpress.XtraCharts.CrosshairLabelMode.ShowCommonForAllSeries;
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
            diagram.EnableAxisXScrolling = true;
            diagram.EnableAxisYZooming = true;
            //diagram.Panes[0].EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.True;

            if (dt.Rows.Count > 8)
            {
                diagram.AxisX.VisualRange.SetMinMaxValues(dt.Rows[0][_argument], dt.Rows[8][_argument]);
            }
            else
            {
                diagram.AxisX.VisualRange.SetMinMaxValues(dt.Rows[0][_argument], dt.Rows[dt.Rows.Count - 1][_argument]);
            }
        }


        private void BindingChartMain()
        {
            chartControl1.DataSource = CreateDataTable();
            chartControl1.Series[0].ArgumentDataMember = "DIV";
            chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES1" });
            chartControl1.Series[1].ArgumentDataMember = "DIV";
            chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "VALUES2" });
            chartControl1.Series[2].ArgumentDataMember = "DIV";
            chartControl1.Series[2].ValueDataMembers.AddRange(new string[] { "VALUES3" });
            chartControl1.Series[3].ArgumentDataMember = "DIV";
            chartControl1.Series[3].ValueDataMembers.AddRange(new string[] { "VALUES4" });

            chartControl2.DataSource = CreateDataTable();
            chartControl2.Series[0].ArgumentDataMember = "DIV";
            chartControl2.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES2" });

            chartControl3.DataSource = CreateDataTable();
            chartControl3.Series[0].ArgumentDataMember = "DIV";
            chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES1" });
            chartControl3.Series[1].ArgumentDataMember = "DIV";
            chartControl3.Series[1].ValueDataMembers.AddRange(new string[] { "VALUES2" });

        }

        private void BindingChartMain2()
        {
            chart4.DataSource = CreateDataTable2();
            chart4.Series[0].ArgumentDataMember = "YM_LABEL";
            chart4.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

            chart3.DataSource = CreateDataTable2();
            chart3.Series[0].ArgumentDataMember = "YM_LABEL";
            chart3.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

            chart2.DataSource = CreateDataTable2();
            chart2.Series[0].ArgumentDataMember = "YM_LABEL";
            chart2.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

            chart1.DataSource = CreateDataTable2();
            chart1.Series[0].ArgumentDataMember = "YM_LABEL";
            chart1.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

        }

        private void setTreelist()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //while (treeList.Nodes.Count > 0)
                //{
                   
                //    if (treeList.Nodes.FirstNode.HasChildren != null)
                //    {
                //        while (treeList.Nodes.FirstNode.RootNode.Nodes.Count > 0)
                //        {
                //            treeList.Nodes.FirstNode.RootNode.Nodes.RemoveAt(0);
                //        }
                //    }
                //    treeList.Nodes.RemoveAt(0);
                //}

                DataTable dt = CreateDataTable_Tree();
                treeList.DataSource = null;             
                
                treeList.DataSource = dt;
                treeList.KeyFieldName = "ID";
                treeList.ParentFieldName = "PARENTID";
                treeList.Columns["ID_NAME"].Visible = false;
                //treeList.Columns["SHIFT"].Visible = false;
                //treeList.Columns["SELECT_YN"].Visible = false;
                //treeList.Columns["MENU_NM"].OptionsColumn.AllowSort = false;
                //treeList.Columns["MENU_NM"].Width = 200;
                //treeList.Columns["MENU_NM"].Caption = "Process";
                //treeList.Columns["QTY"].OptionsColumn.AllowSort = false;
                //treeList.Columns["QTY"].Width = 90;
                //treeList.Columns["QTY"].Caption = "Total";
                //treeList.Columns["QTY"].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                //treeList.Columns["QTY"].Format.FormatString = "#,#";

                Skin skin = GridSkins.GetSkin(treeList.LookAndFeel);
                skin.Properties[GridSkins.OptShowTreeLine] = true;
                chkAll.Checked = true;
                foreach (TreeListNode node in treeList.Nodes)
                {
                    var dataRow = treeList.GetDataRecordByNode(node);
                    node.Tag = dataRow;
                    node.Checked = true;
                    node.Expanded = true;
                    foreach (TreeListNode node1 in node.RootNode.Nodes)
                    {
                        if (node.Checked)
                            node1.Checked = true;
                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
        }

        private void OnUcClick(int tag)
        {
            for (int i = 0; i < ucBtns.Length; i++)
            {
                if (ucBtns[i] != null)
                    if (Convert.ToInt32(ucBtns[i].Tag) == tag)
                        ucBtns[i].SetColor();
                    else
                        ucBtns[i].SetDefaultColor();
            }
            cCount = ReloadTime - 3;
            _Tag = tag;
            navigationFrame1.SelectedPageIndex = tag - 1;
        }

        private void SMT_I_TMS_MFG_LT_V02_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            InitModels();
            InitNavButton();

        }

        private void SMT_I_TMS_MFG_LT_V02_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {

                    navigationFrame1.SelectedPageIndex = 0;
                    navigationFrame1.Visible = false;
                    //cCount = 59;
                    if (!isLoad)
                    {
                        _Tag = 0;
                        isLoad = true;
                        cCount = 5;
                        tmrLoad.Start();
                        load_data_season();
                        load_data_leadtime();
                    }
                    else
                    {
                        if (_Tag != 0)
                        {
                            //navigationFrame1.SelectedPageIndex = 0;
                            _Tag = 1;
                        }
                        cCount = 59;
                        tmrLoad.Start();
                    }

                }
                else
                {
                    //navigationFrame1.SelectedPageIndex = 0;
                    navigationFrame1.Visible = false;
                    //navigationFrame1.SelectedPageIndex = 0;
                    tmrLoad.Stop();
                    this.Dispose(true);
                }
            }
            catch { }
        }

        private void treeList_MouseUp(object sender, MouseEventArgs e)
        {
            //TreeList tree = sender as TreeList;
            //Point pt = new Point(e.X, e.Y);
            //TreeListHitInfo hit = tree.GetHitInfo(pt);
            //int cnt = 0;
            //if (hit.Column != null)
            //{
            //    foreach (TreeListNode node in treeList.Nodes)
            //    {
            //        foreach (TreeListNode node1 in node.RootNode.Nodes)
            //        {
            //            if (node1.Checked)
            //            {
            //                cnt++;
            //            }
            //        }
            //    }

            //    if (cnt == 0)
            //    {
            //        foreach (TreeListNode node in treeList.Nodes)
            //        {
            //            node.Checked = true;
            //            foreach (TreeListNode node1 in node.RootNode.Nodes)
            //            {
            //                node1.Checked = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (TreeListNode node in treeList.Nodes)
            //        {
            //            node.Checked = false;
            //            foreach (TreeListNode node1 in node.RootNode.Nodes)
            //            {
            //                node1.Checked = false;
            //            }
            //        }
            //    }
            //    tmrAnimation.Start();
            //    BindingChartMain2();
            //}
            
        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.GetValue("PARENTID").ToString() == "000")
            {
                e.Appearance.BackColor = Color.Black;
                e.Appearance.ForeColor = Color.Yellow;
                e.Appearance.Font = new Font("Times New Roman", 18, FontStyle.Bold ^ FontStyle.Italic);
            }
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            _Tag = 1;
            setTreelist();
            splMain.Panel1Collapsed = false;
            navigationFrame1.SelectedPageIndex = 1;
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            _Tag = 0;
            splMain.Panel1Collapsed = true;
            navigationFrame1.SelectedPageIndex = 0;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                foreach (TreeListNode node in treeList.Nodes)
                {
                    node.Checked = true;
                    foreach (TreeListNode node1 in node.RootNode.Nodes)
                    {
                        node1.Checked = true;
                    }
                }
            }
            else
            {
                foreach (TreeListNode node in treeList.Nodes)
                {
                    node.Checked = false;
                    foreach (TreeListNode node1 in node.RootNode.Nodes)
                    {
                        node1.Checked = false;
                    }
                }
            }
            tmrAnimation.Start();
            //BindingChartMain2();
        }

        private void treeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.ParentNode != null)
                e.Node.ParentNode.Checked = IsAllChecked(e.Node.ParentNode.Nodes);
            else
                SetCheckedChildNodes(e.Node.Nodes);
            tmrAnimation.Start();
            BindingChart();
        }

        private void SetCheckedChildNodes(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
                node.Checked = node.ParentNode.Checked;
        }

        private bool IsAllChecked(TreeListNodes nodes)
        {
            bool value = true;
            foreach (TreeListNode node in nodes)
            {
                if (!node.Checked)
                {
                    value = false;
                    break;
                }
            }
            return value; 
        }

        private void btn_Click(object sender, EventArgs e)
        {
            _Tag = 1;
            //splMain.Panel1Collapsed = false;
            _season_cd = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            switch (((DevExpress.XtraEditors.SimpleButton)sender).Name.Substring(3,7))
            {
                case "SEASON1":
                    _season_nm = lblSEASON1.Text;
                    break;
                case "SEASON2":
                    _season_nm = lblSEASON2.Text;
                    break;
                case "SEASON3":
                    _season_nm = lblSEASON3.Text;
                    break;
                case "SEASON4":
                    _season_nm = lblSEASON4.Text;
                    break;
            }
            //_season_nm = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();
            navigationFrame1.Visible = true;
            navigationFrame1.SelectedPageIndex = 1;
            
            load_data_model();
            setTreelist();
            BindingChart();
        }

        private void AnimationNumber(string QDiv,Label lbl,int RealNumber)
        {
           
            switch (QDiv)
            {
                case "LT_LAST":
                    if (LT_LAST < RealNumber)
                    {
                        LT_LAST++;
                        lbl.Text = string.Concat(LT_LAST, " ", "Days");
                    }
                    break;
                case "LT_CURR":
                    if (LT_CURR < RealNumber)
                    {
                        LT_CURR++;
                        lbl.Text = string.Concat(LT_CURR, " ", "Days");
                    }
                    break;
            }
            
            
        }
        private void separatorControl5_Click(object sender, EventArgs e)
        {

        }

        private void BindingLT(Label lbl, string sV)
        {
            lbl.Text = sV;
        }
        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            cCountAni++;
            BindingLT(lbl1, rand.Next(1, 20).ToString());
            BindingLT(lbl2, rand.Next(1, 20).ToString());
            BindingLT(lbl3, rand.Next(1, 20).ToString());
            BindingLT(lbl4, rand.Next(1, 20).ToString());
            BindingLT(lbl5, rand.Next(1, 20).ToString());
            BindingLT(lbl6, rand.Next(1, 20).ToString());
            BindingLT(lbl7, rand.Next(1, 20).ToString());
            BindingLT(lbl8, rand.Next(1, 20).ToString());
            BindingLT(lbl9, rand.Next(1, 20).ToString());
            BindingLT(lbl10, rand.Next(1, 20).ToString());
            BindingLT(lbl11, rand.Next(1, 20).ToString());
            BindingLT(lbl12, rand.Next(1, 20).ToString());

            if (cCountAni >= 15)
            {
                cCountAni = 0;
                tmrAnimation.Stop();
                if (dtLeadtime.Rows.Count > 0)
                {
                    BindingLT(lbl1, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE01"]));
                    BindingLT(lbl2, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE02"]));
                    BindingLT(lbl3, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE03"]));
                    BindingLT(lbl4, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE04"]));
                    BindingLT(lbl5, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE05"]));
                    BindingLT(lbl6, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE06"]));
                    BindingLT(lbl7, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE07"]));
                    BindingLT(lbl8, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE08"]));
                    BindingLT(lbl9, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE09"]));
                    BindingLT(lbl10, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE10"]));
                    BindingLT(lbl11, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE11"]));
                    BindingLT(lbl12, string.Format("{0:n2}", dtLeadtime.Rows[1]["VALUE12"]));
                }
            }
        }

        private void tmrLoad_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            cCount++;
            if (cCount >= ReloadTime)
            {
                cCount = 0;
                tmrAnimation.Stop();
                load_data_season();
                load_data_leadtime();
                switch (_Tag)
                {
                    case 0:
                       
                        BindingChart();
                        break;
                    case 1:
                        //Reset Some Param
                        LT_CURR = 0;
                        LT_LAST = 0;
                        tmrAnimation.Start();
                        BindingChart();                        
                        break;
                }
            }
        }
    }
}
