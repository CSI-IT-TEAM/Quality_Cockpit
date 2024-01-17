using DevExpress.Skins;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
    public partial class SMT_I_TMS_MFG_LT_BKV01 : Form
    {
        public SMT_I_TMS_MFG_LT_BKV01()
        {
            InitializeComponent();
            tmrLoad.Stop();
            tmrAnimation.Stop();
        }
        UC.UC_BTN_NAV[] ucBtns = new UC.UC_BTN_NAV[13];
        List<ButtonModel> models = new List<ButtonModel>();
        int _Tag = 1, cCount = 0, cCountAni = 0, LT_LAST = 0, LT_CURR = 0;
        const int ReloadTime = 5;
        string _season_cd = "", _season_nm = "";

        Random r = new Random();
        bool isLoad = false;
        // private string[] _arrMonthShortName = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //private string[] _arrMonthShortName = { "Model", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        #region CreateDatatable

        private DataTable GET_PLANT_DATA(string V_P_TYPE, string V_P_PLANT, string V_P_LINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            MyOraDB.ShowErr = true;

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "PKG_SMT_I_TMS.SP_SET_COMBO";

            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_PLANT";
            MyOraDB.Parameter_Name[2] = "V_P_LINE";
            MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = V_P_TYPE;
            MyOraDB.Parameter_Values[1] = V_P_PLANT;
            MyOraDB.Parameter_Values[2] = V_P_LINE;
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

                DataTable dtModel = GET_PLANT_DATA("MODEL", "", "");
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
            chartControl4.DataSource = CreateDataTable2();
            chartControl4.Series[0].ArgumentDataMember = "YM_LABEL";
            chartControl4.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

            chartControl5.DataSource = CreateDataTable2();
            chartControl5.Series[0].ArgumentDataMember = "YM_LABEL";
            chartControl5.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

            chartControl6.DataSource = CreateDataTable2();
            chartControl6.Series[0].ArgumentDataMember = "YM_LABEL";
            chartControl6.Series[0].ValueDataMembers.AddRange(new string[] { "VALUES" });

        }

        private void setTreelist()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = CreateDataTable_Tree();
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
            catch { this.Cursor = Cursors.Default; }
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

        private void SMT_I_TMS_MFG_LT_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            InitModels();
            InitNavButton();
        }

        private void SMT_I_TMS_MFG_LT_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                
                if (!isLoad)
                {                   
                    isLoad = true;
                    cCount = 5;
                    tmrLoad.Start();
                }
                else
                {
                    if (_Tag != 0)
                    {
                        navigationFrame1.SelectedPageIndex = 0;
                        _Tag = 0;
                    }
                    cCount = 3;
                    tmrLoad.Start();
                }

            }
            else
                tmrLoad.Stop();
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
            BindingChartMain2();
        }

        private void treeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.ParentNode != null)
                e.Node.ParentNode.Checked = IsAllChecked(e.Node.ParentNode.Nodes);
            else
                SetCheckedChildNodes(e.Node.Nodes);
            tmrAnimation.Start();
            BindingChartMain2();
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
            _season_nm = ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString();                    
            navigationFrame1.SelectedPageIndex = 1;
            setTreelist();

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

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            cCountAni++;
            switch (_Tag)
            {
                case 1:
                    
                    break;
                case 2:
                    AnimationNumber("LT_LAST", label13, 8);
                    AnimationNumber("LT_CURR", label9, 7);
                    break;
            }
            if (cCountAni >= 12)
            {
                cCountAni = 0;
                tmrAnimation.Stop();
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
                switch (_Tag)
                {
                    case 0:
                       
                        BindingChartMain();
                        break;
                    case 1:
                        //Reset Some Param
                        LT_CURR = 0;
                        LT_LAST = 0;
                        tmrAnimation.Start();
                        BindingChartMain2();                        
                        break;
                }
            }
        }
    }
}
