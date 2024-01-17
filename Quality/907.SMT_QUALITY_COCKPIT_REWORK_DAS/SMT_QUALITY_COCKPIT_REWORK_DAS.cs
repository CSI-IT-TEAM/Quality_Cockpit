using DevExpress.Skins;
using DevExpress.XtraCharts;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class SMT_QUALITY_COCKPIT_REWORK_DAS : Form
    {

        #region ========= [Global Variable] ==============================================

        private readonly string _strHeader = "  Rework By Day";
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");
        string sType = "DAY";
        string sLine = "ALL", sLine_nm = "ALL", sPlant = "ALL", sDateF = "", sDateT = "";
        DataTable _dtArea = null;
        DataTable dtModel = null;
        string _tabIndex = "0";
        bool isHasChild = false, isCheckState = true;
        string list = "";
        #endregion ========= [Global Variable] ==============================================

        #region ========= [Form Init] ==============================================

        public SMT_QUALITY_COCKPIT_REWORK_DAS()
        {
            InitializeComponent();
            lblHeader.Text = _strHeader;
        }

        private void SMT_QUALITY_COCKPIT_FORM1_Load(object sender, EventArgs e)
        {
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;

            dtpYMDT.EditValue = DateTime.Now;
            DateTime dt = DateTime.Now;
            DateTime fistdate = new DateTime(dt.Year, dt.Month, 1);
            dtpYMD.EditValue = DateTime.Now;
        }

        private void SMT_QUALITY_COCKPIT_REWORK_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                cboPlant.SelectedValue = ComVar.Var._strValue1;
                cboLine.SelectedValue = ComVar.Var._strValue2;
                tabControl.SelectedTabPage = tabControl.TabPages[0];
                clear_chart();
                _time = 28;


                timer1.Start();
            }
            else
            {
                timer1.Stop();
                Dispose();
            }

        }

        #endregion ========= [Form Init] ==============================================

        #region ========= [Timer Event] ==========================================

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd")) + "\n\r" +
                           string.Format(DateTime.Now.ToString("HH:mm:ss"));
            _time++;
            if (_time >= 30)
            {
                _tabIndex = tabControl.SelectedTabPageIndex.ToString();
                switch (_tabIndex)
                {
                    case "0":
                        SetData();
                        break;
                    case "1":


                        load_data_model();
                        setTreelist();
                        GetDataTable();
                        break;
                    default:
                        break;
                }

                _time = 0;

            }
        }

        #endregion ========= [Timer Event] ==========================================

        #region ========= [Control Event] ==========================================

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = false;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sType = "DAY";
            lblHeader.Text = "  Rework By Day";
            clear_chart();
            _time = 30;
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = false;
            btnMonth.Enabled = false;
            btnYear.Enabled = false;
            sType = "WEEK";
            lblHeader.Text = "  Rework By Week";
            clear_chart();
            _time = 30;
        }

        private void dtpYMD_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            LoadCombo("PLANT");
            _time = 30;
        }

        private void dtpYMDT_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            LoadCombo("PLANT");
            _time = 30;

        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = true;
            btnMonth.Enabled = false;
            btnYear.Enabled = true;
            sType = "MONTH";
            lblHeader.Text = "  Rework By Month";
            clear_chart();
            _time = 30;
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            btnDay.Enabled = true;
            btnWeek.Enabled = true;
            btnMonth.Enabled = true;
            btnYear.Enabled = false;
            sType = "YEAR";
            lblHeader.Text = "  Rework By Year";
            clear_chart();
            _time = 30;
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Hand;
                ChartHitInfo hit = chartControl1.CalcHitInfo(e.X, e.Y);
                SeriesPoint point = hit.SeriesPoint;
                // Check whether the series point was clicked or not.
                if (point != null)
                {
                    sLine_nm = point.Argument;

                    for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                    {
                        if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sLine_nm)
                        {
                            sLine = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                        }
                    }
                }
                else
                {
                    if (hit.AxisLabelItem == null)
                    {
                        sLine = "ALL";
                    }
                    else
                    {
                        sLine_nm = hit.AxisLabelItem.AxisValue.ToString();
                        for (int iRow = 0; iRow < _dtArea.Rows.Count; iRow++)
                        {
                            if (_dtArea.Rows[iRow]["LINE_NM"].ToString() == sLine_nm)
                            {
                                sLine = _dtArea.Rows[iRow]["LINE_CD"].ToString();
                            }
                        }
                    }

                }

                //else
                //    sLine = "ALL";
                _time = 10;
                SetDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion ========= [Control Event] ==========================================

        #region ========= [Method] ==========================================

        private void clear_chart()
        {
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl2.Series[0].Points.Clear();
            // chartControl3.Series[1].Points.Clear();
        }

        private void SetChart(DataTable argDtChart)
        {

            //DataTable dtchart = await sbGetRework_Chart("CHART", YMDF, YMDT, PLANT_CD, LINE_CD);
            chartControl1.DataSource = null;
            chartControl1.Series[0].Points.Clear();
            chartControl1.Series[1].Points.Clear();
            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            if (sType.ToString() == "MONTH")
            {
                diagram.AxisX.Title.Text = "Month";
            }
            else if (sType.ToString() == "YEAR")
            {
                diagram.AxisX.Title.Text = "Year";
            }
            else
                diagram.AxisX.Title.Text = "Plant";

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(),
                    argDtChart.Rows[i]["REW_QTY"]));
                chartControl1.Series[1].Points
                    .Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate >= 12)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(250, 55, 30);
                }
                else if (rate <= 9)
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(20, 200, 110);

                }
                else
                {
                    chartControl1.Series[0].Points[i].Color = Color.FromArgb(255, 180, 15);
                }
            }
        }

        private void SetChart1(DataTable argDtChart)
        {
            chartControl2.DataSource = null;
            chartControl2.Series[0].Points.Clear();
            chartControl2.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl2.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;
            XYDiagram diagram = (XYDiagram)chartControl2.Diagram;

            //if (sType.ToString() == "MONTH")
            //{
            //    diagram.AxisX.Title.Text = "Month";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else if (sType.ToString() == "YEAR")
            //{
            //    diagram.AxisX.Title.Text = "Year";
            //    diagram.AxisY.Label.Font = new System.Drawing.Font("Calibri", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            //}
            //else
            diagram.AxisX.Title.Text = "Plant";
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl2.Series[0].Points
                    .Add(new SeriesPoint(argDtChart.Rows[i]["LINE_NM"].ToString(), argDtChart.Rows[i]["RATE"]));

            }
        }

        private void SetChart2(DataTable argDtChart)
        {
            chartControl3.Series[0].Points.Clear();
            chartControl3.Series[0].ArgumentScaleType = ScaleType.Qualitative;

            if (argDtChart == null) return;
            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chartControl3.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["REWORK_NAME"].ToString(),
                    argDtChart.Rows[i]["REW_QTY"]));
                //chartControl3.DataSource = argDtChart;
                //chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                //chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

            }
        }

        private async void SetData()
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");

                DataSet dsData = await sbGetRework(sType, sDateF, sDateT, sPlant, sLine);
                if (dsData == null) return;
                DataTable dtChart = dsData.Tables[0];

                if (dtChart.Select("LINE_CD <> 'TOT'", "LINE_CD").Count() > 0)
                {
                    DataTable _dtChart = dtChart.Select("LINE_CD <> 'TOT'", "LINE_CD").CopyToDataTable();

                    _dtArea = _dtChart;
                    SetChart(_dtChart);
                }

                if (dtChart.Select("LINE_CD = 'TOT'", "LINE_CD").Count() > 0)
                {
                    DataTable _dtLabel = dtChart.Select("LINE_CD = 'TOT'", "LINE_CD").CopyToDataTable();
                    lblTotalRework.Text =
                        Convert.ToDouble(_dtLabel.Rows[0]["REW_QTY"].ToString()).ToString("###,##0.##") + " Pairs";
                    lblTotalProd.Text =
                        Convert.ToDouble(_dtLabel.Rows[0]["PROD_QY"].ToString()).ToString("###,##0.##") + " Pairs";
                    lblTotalRate.Text = _dtLabel.Rows[0]["RATE"].ToString() + " %";

                }

                SetDataDetail();
                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }

        private async void SetDataDetail()
        {
            try
            {

                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");
                DataSet _dtSet = await sbGetRework(sType, sDateF, sDateT, sPlant, sLine);

                DataTable dtChart1 = _dtSet.Tables[1];
                DataTable dtChart2 = _dtSet.Tables[2];

                SetChart1(dtChart1);
                if (dtChart2 != null && dtChart2.Rows.Count > 0)
                {
                    DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
                    chartControl3.Titles.Clear();
                    if (sLine == "ALL")
                    {
                        chartTitle2.Text = "Rework By Reason";
                    }
                    else
                    {
                        if (int.Parse(sLine) < 6)
                        {
                            chartTitle2.Text = "Plant " + sLine_nm + " By Reason";
                        }
                        else
                            chartTitle2.Text = "Plant " + sLine_nm + " By Reason";
                    }

                    // Define the alignment of the titles.
                    chartTitle2.Alignment = StringAlignment.Center;

                    // Place the titles where it's required.
                    chartTitle2.Dock = ChartTitleDockStyle.Top;

                    // Customize a title's appearance.
                    chartTitle2.Antialiasing = true;
                    chartTitle2.Font = new Font("Calibri", 22F, FontStyle.Bold);
                    chartTitle2.TextColor = Color.Blue;
                    chartTitle2.Indent = 10;
                    chartControl3.Titles.AddRange(new ChartTitle[] { chartTitle2 });

                    if (dtChart2 == null) return;
                    chartControl3.DataSource = dtChart2;
                    chartControl3.Series[0].ArgumentDataMember = "REWORK_NAME";
                    chartControl3.Series[0].ValueDataMembers.AddRange(new string[] { "REW_QTY" });

                }
                else
                {
                    chartControl3.DataSource = null;
                }

                dtChart1 = null;
                dtChart2 = null;


            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }


        #endregion ========= [Method] ==========================================

        #region ========= [Procedure Call] ===========================================

        public async Task<DataSet> sbGetRework(string ARG_QTYPE, string ARG_DATEF, string ARG_DATET, string ARG_PLANT,
            string ARG_LINE)
        {
            return await Task.Run(() =>
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                try
                {
                    string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_REWORK";

                    MyOraDB.ReDim_Parameter(8);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                    MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                    MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[4] = "V_P_LINE";
                    MyOraDB.Parameter_Name[5] = "OUT_CURSOR";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR2";
                    MyOraDB.Parameter_Name[7] = "OUT_CURSOR3";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
                    MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;


                    MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                    MyOraDB.Parameter_Values[1] = ARG_DATEF;
                    MyOraDB.Parameter_Values[2] = ARG_DATET;
                    MyOraDB.Parameter_Values[3] = ARG_PLANT;
                    MyOraDB.Parameter_Values[4] = ARG_LINE;
                    MyOraDB.Parameter_Values[5] = "";
                    MyOraDB.Parameter_Values[6] = "";
                    MyOraDB.Parameter_Values[7] = "";
                    ;

                    MyOraDB.Add_Select_Parameter(true);
                    ds_ret = MyOraDB.Exe_Select_Procedure();

                    if (ds_ret == null) return null;
                    return ds_ret;
                }
                catch
                {
                    return null;
                }
            });
        }

        private DataTable Fn_SelectDataGrid(string ARG_QTYPE, string ARG_DATEF, string ARG_DATET, string ARG_PLANT,
            string ARG_LINE, string ARG_MODEL)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_QUALITY_COCKPIT.SMT_QUA_REWORK_BY_MODEL";

                MyOraDB.ReDim_Parameter(7);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_DATE_FR";
                MyOraDB.Parameter_Name[2] = "V_P_DATE_TO";
                MyOraDB.Parameter_Name[3] = "V_P_PLANT";
                MyOraDB.Parameter_Name[4] = "V_P_LINE";
                MyOraDB.Parameter_Name[5] = "V_P_MODEL";
                MyOraDB.Parameter_Name[6] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_DATEF;
                MyOraDB.Parameter_Values[2] = ARG_DATET;
                MyOraDB.Parameter_Values[3] = ARG_PLANT;
                MyOraDB.Parameter_Values[4] = ARG_LINE;
                MyOraDB.Parameter_Values[5] = ARG_MODEL;
                MyOraDB.Parameter_Values[6] = "";

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

        #endregion ========= [Procedure Call] ===========================================

        #region ========= [add tab model] ===========================================

        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                _tabIndex = tabControl.SelectedTabPageIndex.ToString();
                switch (_tabIndex)
                {
                    case "0":
                        lblPlant.Visible = false;
                        cboPlant.Visible = false;
                        SetData();
                        //  strPlant = "2110";
                        //  BindingData();
                        break;
                    case "1":
                        lblPlant.Visible = true;
                        cboPlant.Visible = true;
                        LoadCombo("PLANT");
                        load_data_model();
                        setTreelist();
                        GetDataTable();
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

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.GetValue("PARENTID").ToString() == "000")
            {
                e.Appearance.BackColor = Color.Black;
                e.Appearance.ForeColor = Color.Yellow;
                e.Appearance.Font = new Font("Times New Roman", 18, FontStyle.Bold ^ FontStyle.Italic);
            }
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
        }

        private void treeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.ParentNode != null)
                e.Node.ParentNode.Checked = IsAllChecked(e.Node.ParentNode.Nodes);
            else
                SetCheckedChildNodes(e.Node.Nodes);
            GetDataTable();
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

        private void setTreelist()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = CreateDataTable_Tree();
                treeList.DataSource = null;
                treeList.DataSource = dt;
                treeList.KeyFieldName = "ID";
                treeList.ParentFieldName = "PARENTID";
                treeList.Columns["ID_NAME"].Visible = false;
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
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

                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");

                DataTable dtModel = Fn_SelectDataGrid("TREE", sDateF, sDateT, cboPlant.SelectedValue.ToString().Trim(),
                    "", "");

                if (dtModel != null && dtModel.Rows.Count > 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["PARENTID"] = "000";
                        dr["ID"] = "Model";
                        dr["MENU_NM"] = "Model";
                        dr["ID_NAME"] = "Model";
                        dt.Rows.Add(dr);
                    }

                    for (int ifac = 0; ifac < 1; ifac++)
                    {
                        for (int i = 0; i < dtModel.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["PARENTID"] = dt.Rows[ifac]["ID"].ToString();
                            dr["ID"] = dt.Rows[ifac]["ID"].ToString() + "_" + dtModel.Rows[i]["MODEL_CD"].ToString();
                            dr["MENU_NM"] = dtModel.Rows[i]["MODEL_NAME"].ToString();
                            dr["ID_NAME"] = dt.Rows[ifac]["ID"].ToString() + "_" +
                                            dtModel.Rows[i]["MODEL_CD"].ToString();
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

        private void load_data_model()
        {
            try
            {
                sDateF = dtpYMD.DateTime.ToString("yyyyMMdd");
                sDateT = dtpYMDT.DateTime.ToString("yyyyMMdd");
                dtModel = Fn_SelectDataGrid("TREE", sDateF, sDateT, cboPlant.SelectedValue.ToString().Trim(), "", "");
            }
            catch (Exception ex)
            {
                return;
            }
        }


        private void GetDataTable()
        {
            List<DataTable> lstData = new List<DataTable>();
            List<string> lstSeriesName = new List<string>();
            DataTable dt = new DataTable();
            isCheckState = false;
            dt = dtModel.Copy(); //Giu lai datatable goc, de su dung lai.
            var AllCheckedNode = treeList.GetAllCheckedNodes();
            var TreeMaxLevel = GetDeepestNodeLevel(treeList);
            foreach (var item in AllCheckedNode)
            {
                if (item.Level == TreeMaxLevel) //Lấy Node trong cùng.
                {
                    string NodeID = item.GetValue("ID").ToString().Split('_')[1];
                    string NodeName = item.GetValue("MENU_NM").ToString();
                    if (dt.Rows.Count > 1)
                    {
                        if (dt.Select("MODEL_CD = '" + NodeID + "'").Count() > 0)
                        {
                            DataTable dtTmp = dt.Select("MODEL_CD = '" + NodeID + "'").CopyToDataTable();
                            lstData.Add(dtTmp);
                            lstSeriesName.Add(NodeID);
                        }
                    }
                }
            }

            Loaddata(lstData, lstSeriesName);
        }

        public int GetDeepestNodeLevel(DevExpress.XtraTreeList.TreeList treeView)
        {
            int level = -1;
            foreach (TreeListNode node in treeView.Nodes)
            {
                int deep = DigInNodes(node);
                if (deep > level)
                    level = deep;
            }

            return level;
        }

        private void gvwBase_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            double temp = 0.0;

            if (e.Column.AbsoluteIndex >= 1 && e.CellValue != null)
            {
                if (e.Column.FieldName.Contains("RATE")) //&& e.RowHandle != gvwBase.RowCount - 1)
                {
                    double.TryParse(
                        gvwBase.GetRowCellDisplayText(gvwBase.RowCount - 1, gvwBase.Columns[e.Column.ColumnHandle])
                            .ToString(), out temp); //out
                    if (temp >= 12)
                    {
                        e.Appearance.BackColor = Color.FromArgb(250, 55, 30);
                    }
                    else if (temp <= 9)
                    {
                        e.Appearance.BackColor = Color.FromArgb(20, 200, 110);
                    }
                    else
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 180, 15);
                    }
                    e.Appearance.ForeColor = Color.White;
                }

                if (gvwBase.GetRowCellDisplayText(e.RowHandle, gvwBase.Columns["MODEL_NAME"]).ToString().ToUpper().Contains("TOTAL"))
                {
                    e.Appearance.ForeColor = Color.Blue;
                }

                if (gvwBase.GetRowCellDisplayText(e.RowHandle, gvwBase.Columns["LINE_NM"]).ToString().ToUpper().Contains("G.TOTAL"))
                {
                    e.Appearance.BackColor = Color.Bisque;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private int DigInNodes(TreeListNode node)
        {
            int level = node.Level;
            foreach (TreeListNode subnode in node.Nodes)
            {
                int deep = DigInNodes(subnode);
                if (deep > level)
                    level = deep;
            }

            return level;
        }
        private void Loaddata(List<DataTable> lstData, List<string> listSeriesName)
        {
            try
            {
                chart1.DataSource = null;
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
               // DataTable dtarg = new DataTable();
                for (int i = 0; i < lstData.Count; i++) //Khoi tao so luong series & add vao list Series
                {
                   // dtarg = lstData[i];
                    list += string.Concat(listSeriesName[i].ToString(), ", ");
                }

                DataTable argDtGrid = Fn_SelectDataGrid("Q", sDateF, sDateT, cboPlant.SelectedValue.ToString().Trim(),  "", list);
                fn_BindingData(argDtGrid);
                DataTable _dtLabel = null;
                _dtLabel = argDtGrid.Select("LINE_CD = 'G.Total'", "").CopyToDataTable();
                lblTotalRework.Text = Convert.ToDouble(_dtLabel.Rows[0]["REW_QTY"].ToString()).ToString("###,##0.##") + " Pairs";
                lblTotalProd.Text = Convert.ToDouble(_dtLabel.Rows[0]["PROD_QY"].ToString()).ToString("###,##0.##") + " Pairs";
                lblTotalRate.Text = _dtLabel.Rows[0]["RATE"].ToString() + " %";



                DataTable argDtChart = null;
                argDtChart = Fn_SelectDataGrid("C", sDateF, sDateT, cboPlant.SelectedValue.ToString().Trim(), "", list);
                SetChartModel(argDtChart);
                list = "";
            }
            catch (Exception ex)
            {
            }
        }

        private void fn_BindingData(DataTable dtgrid)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();
                if (dtgrid == null || dtgrid.Rows.Count == 0)
                {
                    grdBase.DataSource = null;
                    return;
                }
                grdBase.DataSource = dtgrid;
                for (int i = 0; i < gvwBase.Columns.Count; i++)
                {
                    gvwBase.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Regular);
                    gvwBase.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);
                    gvwBase.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                    if (i <= 4)
                    {
                        gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    }
                    else
                    {
                        gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }
                    gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    gvwBase.Columns[i].OptionsFilter.AllowFilter = false;
                    gvwBase.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
                    gvwBase.Columns[i].OptionsColumn.ReadOnly = true;
                    gvwBase.ColumnPanelRowHeight = 50;
                    gvwBase.RowHeight = 50;
                    gvwBase.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwBase.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvwBase.Columns[i].DisplayFormat.FormatString = "#,###0.##";
                }
                gvwBase.Columns["LINE_NM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gvwBase.Columns["MLINE_CD"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void SetChartModel(DataTable argDtChart)
        {
          
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chart1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            if (argDtChart == null) return;

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            if (sType.ToString() == "MONTH")
            {
                diagram.AxisX.Title.Text = "Month";
            }
            else if (sType.ToString() == "YEAR")
            {
                diagram.AxisX.Title.Text = "Year";
            }
            else
                diagram.AxisX.Title.Text = "Plant";

            for (int i = 0; i <= argDtChart.Rows.Count - 1; i++)
            {
                chart1.Series[0].Points.Add(new SeriesPoint(argDtChart.Rows[i]["REWORK_NAME"].ToString(),argDtChart.Rows[i]["REW_QTY"]));
                chart1.Series[1].Points.Add(new SeriesPoint(argDtChart.Rows[i]["REWORK_NAME"].ToString(),argDtChart.Rows[i]["RATE"]));

                double rate;
                double.TryParse(argDtChart.Rows[i]["RATE"].ToString(), out rate); //out

                if (rate >= 12)
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(250, 55, 30);
                }
                else if (rate <= 9)
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(20, 200, 110);
                }
                else
                {
                    chart1.Series[0].Points[i].Color = Color.FromArgb(255, 180, 15);
                }
            }
        }


        private void LoadCombo(string arg_type)
        {
            try
            {

                if (arg_type == "PLANT")
                {
                    DataTable dt = Fn_SelectDataGrid("PLANT", sDateF, sDateT, "", "", "");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cboPlant.DataSource = dt;
                        cboPlant.DisplayMember = "LINE_NM";
                        cboPlant.ValueMember = "LINE_CD";
                    }
                }
                //if (arg_type == "COMBO_LINE")
                //{
                //    DataTable dt = Fn_SelectDataGrid(arg_type, sDateF, sDateT, cboPlant.SelectedValue.ToString().Trim(), "","");
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        cboLine.DataSource = dt;
                //        cboLine.DisplayMember = "NAME";
                //        cboLine.ValueMember = "CODE";
                //    }
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
            }
        }

        private void cboPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            _time = 30;
        }

        #endregion ========= [add tab model] ===========================================
    }
}
