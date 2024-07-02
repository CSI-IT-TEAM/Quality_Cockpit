using DevExpress.Skins;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Columns;
using System;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FORM
{
    public partial class POPUP_PROD_BY_PLANT : Form
    {
        public POPUP_PROD_BY_PLANT()
        {
            InitializeComponent();
            chartControl1.LegendItemChecked += OnLegendItemChecked;
            
        }

        public POPUP_PROD_BY_PLANT(string argPlant, string argName)
        {
            InitializeComponent();
            string Plant = argPlant;
            
        }

        public string Plant ;
        public string PlantName;
        string Line = "";
        UC.UC_MONTH_SELECTION UcMonth = new UC.UC_MONTH_SELECTION();
        bool ShowTree = false;
        private void BindingData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DataTable data = SelectData();
                DataTable dataGrid = Pivot(data, "YMD");
                SetGrid(dataGrid);
                BindingDataChart(ConvertDatatable(data));
                string avg = (Avg(data)).ToString("#,0.#");
                lblPlanAvg.Text = $"Daily Average Rework Rate: {avg}%";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BindingData: " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            
        }

        private double Avg(DataTable data)
        {
            double count = 0;
            double sum = 0;
            for (int i = 1; i < data.Rows.Count; i++)
            {
                if (data.Rows[i]["RATE_QTY"].ToString() != "")
                {
                    double planQty = 0;
                    double.TryParse(data.Rows[i]["RATE_QTY"].ToString().Split('\n')[0].Replace(",",""), out planQty);
                    sum += planQty;
                    count++;
                }

            }

            return sum/count;
        } 

        #region Chart
        bool initializationFlag = false;
        public void BindingDataChart(DataTable argData)
        {
            try
            {
                if (ShowTree) chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                else chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                chartControl1.DataSource = null;
                foreach (Series series in chartControl1.Series)
                {
                    series.Points.Clear();
                    series.ArgumentDataMember = "";
                    series.ValueDataMembers.Clear();
                }
                chartControl1.DataSource = argData;
                chartControl1.Series[SerialIndex].ArgumentDataMember = "TXT";
                chartControl1.Series[SerialIndex].ValueDataMembers.AddRange(new string[] { "VAL" });
                

                int minValue = Convert.ToInt32(argData.Compute("min([VAL])", string.Empty));
                int maxValue = Convert.ToInt32(argData.Compute("max([VAL])", string.Empty));

                NumericScaleOptions yAxisOptions = ((XYDiagram)chartControl1.Diagram).AxisY.NumericScaleOptions;
                yAxisOptions.GridSpacing = 5;

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                diagram.AxisY.WholeRange.Auto = false;
                diagram.AxisY.WholeRange.SetMinMaxValues(minValue - 1, maxValue + 1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("BindingDataChart: " + ex.Message);
            }
        }

        private void AddSeriesChart()
        {
            DataTable dt = GetDataLine();
            chartControl1.Series[0].LegendText = dt.Rows[0]["MENU_NM"].ToString();
            chartControl1.Series[0].Name = dt.Rows[0]["ID"].ToString();
            chartControl1.Series[0].Tag = 0;
            int maxRow = dt.Rows.Count;
            for (int i = 1; i < chartControl1.Series.Count; i++)
            {
                if (i >= maxRow)
                {
                    chartControl1.Series[i].Visible = false;
                    continue;
                }
                Series series = chartControl1.Series[i];
                series.LegendText = dt.Rows[i]["MENU_NM"].ToString();
                series.Name = dt.Rows[i]["ID"].ToString();
                series.Tag = i;
            }         
        }

        int SerialIndex = 0;
        void OnLegendItemChecked(object sender, LegendItemCheckedEventArgs e)
        {
            try
            {
                if (initializationFlag == true)
                    return;
                initializationFlag = true;
                {
                    Series checkedSeries = e.CheckedElement as Series;
                    
                    if (checkedSeries == null) return;

                    //foreach (Series series in chartControl1.Series)
                    //    series.CheckedInLegend = false;
                    //checkedSeries.CheckedInLegend = true;
                    //SerialIndex = (int)checkedSeries.Tag;

                    if (checkedSeries.Tag.Equals(0))
                    {
                        foreach (Series series in chartControl1.Series)
                            series.CheckedInLegend = true;
                        SerialIndex = 0;
                    }
                    else
                    {
                        foreach (Series series in chartControl1.Series)
                            series.CheckedInLegend = false;
                        checkedSeries.CheckedInLegend = true;
                        SerialIndex = (int)checkedSeries.Tag;
                    }
                    Line = checkedSeries.Name;
                    BindingData();

                    //checkedSeries.CheckableInLegend = false;


                }
                initializationFlag = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            
        }


        #endregion Chart

        public void SetGrid(DataTable argData)
        {
            try
            {
                while (gvwMain.Columns.Count > 0)
                {
                    gvwMain.Columns.RemoveAt(0);
                }
                if (argData == null || argData.Rows.Count == 0) return;
                gvwMain.BeginUpdate();
                gvwMain.OptionsView.ColumnAutoWidth = false;
                gvwMain.RowHeight = 30;
                grdMain.DataSource = argData;

                int gridColum = gvwMain.Columns.Count;
                for (int i = 0; i < gridColum; i++)
                {
                    GridColumn gridColumn = gvwMain.Columns[i];
                    //using (GridColumn gridColumn = gvwMain.Columns[i])
                    //{
                    
                    gridColumn.AppearanceHeader.Font = new Font("Calibri", 14, FontStyle.Bold);
                    gridColumn.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    gridColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    gridColumn.AppearanceCell.Font = new Font("Calibri", (float)12, FontStyle.Regular);
                    gridColumn.OptionsColumn.ReadOnly = true;
                    gridColumn.OptionsColumn.AllowEdit = false;
                    if (i >0)
                    {
                        gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    }
                    else
                    {
                        gridColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        gridColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    }

                    gridColumn.MinWidth = 58;
                    if (i <= 2)
                    {
                        gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    }
                    else
                        gridColumn.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                }


                //foreach (DataRow row in argColumn.Rows)
                //{
                //    GridColumn gridColumn = gvwMain.Columns[row["COL_NAME"].ToString()];
                //    gridColumn.Caption = row["COL_TEXT"].ToString();
                //    gridColumn.Visible = row["SHOW_YN"].ToString() == "Y";
                //    gridColumn.AppearanceCell.TextOptions.HAlignment = row["ALIGN"].ToString() == "L" ?
                //                                                        DevExpress.Utils.HorzAlignment.Near : row["ALIGN"].ToString() == "R" ?
                //                                                        DevExpress.Utils.HorzAlignment.Far : DevExpress.Utils.HorzAlignment.Center;
                //}
                gvwMain.BestFitColumns();
                int columWidth = (grdMain.Width - gvwMain.Columns[0].Width)/ (gridColum - 1);
                for (int i = 1; i < gridColum; i++)
                {
                    gvwMain.Columns[i].Width = columWidth;
                }


                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SetGrid " + ex.Message);
            }
            finally
            {
                gvwMain.EndUpdate();
            }
            
        }

        


        private void gvwMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.Column.AbsoluteIndex <= 0) return;
                if (!gvwMain.GetRowCellValue(e.RowHandle, gvwMain.Columns[0].FieldName).ToString().ToUpper().Contains("RATE")) return;

                string[] dataArray = gvwMain.GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString().Split('\n');
                if (dataArray.Length < 2) return;
                string color = dataArray[1];
                e.Appearance.BackColor = Color.FromName(color);
                switch (color.ToUpper())
                {
                    case "RED":
                    case "GREEN":
                        e.Appearance.ForeColor = Color.White;
                        break;
                    default:
                        e.Appearance.ForeColor = Color.Black;
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("gvwMain_RowCellStyle: " + ex.Message);
            }
            



            //if (e.Column.FieldName.ToString().ToUpper() == "TOTAL")
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    e.Appearance.ForeColor = Color.Blue;
            //    e.Appearance.Font = new Font("Calibri", 13, FontStyle.Bold);

            //}
            //if (gvwMain.GetRowCellValue(e.RowHandle, "MODEL_NM").ToString().ToUpper().Equals("TOTAL"))
            //{
            //    e.Appearance.BackColor = Color.Orange;
            //    e.Appearance.ForeColor = Color.White;
            //    e.Appearance.Font = new Font("Calibri", 14, FontStyle.Bold);
            //}

            //if (gvwMain.GetRowCellValue(e.RowHandle, "MOLD_CD").ToString().ToUpper().Equals("TOTAL"))
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //    e.Appearance.ForeColor = Color.Blue;
            //    e.Appearance.Font = new Font("Calibri", 13, FontStyle.Bold);
            //}

            //if (e.Column.FieldName.ToString().ToUpper() == "MOLD_CD" || e.Column.FieldName.ToString().ToUpper() == "MODEL_NM")
            //{
            //    e.Appearance.BackColor = Color.White;
            //    e.Appearance.ForeColor = Color.Black;
            //}
        }

        private DataTable Pivot(DataTable dt, string pivotColumn)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                foreach (DataRow row in dt.Rows)
                {
                    dtTemp.Columns.Add(row[pivotColumn].ToString());
                }
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    DataRow rowData = dtTemp.NewRow();
                    if (dt.Columns[col].ColumnName == pivotColumn) continue;
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        rowData[dt.Rows[row][pivotColumn].ToString()] = dt.Rows[row][col].ToString();
                    }
                    dtTemp.Rows.Add(rowData);
                }

                return dtTemp;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Pivot: {ex.Message}");
                return null;
            }
        }

        private DataTable ConvertDatatable(DataTable data)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("TXT");
            dtReturn.Columns.Add("VAL", typeof(double));
            for (int i = 1; i < data.Rows.Count; i++)
            {
                string txt = data.Rows[i]["YMD"].ToString();
                string val = data.Rows[i]["RATE_QTY"].ToString();
                if (txt == "00" || txt == " ") continue;
                DataRow rowData = dtReturn.NewRow();
                rowData["TXT"] = data.Rows[i]["YMD"];
                if (val != "")
                {
                    val = val.Split('\n')[0].Replace(",", "").Replace("%", "");
                    double rate = 0;
                    double.TryParse(val, out rate);
                    rowData["VAL"] = rate;
                }
                dtReturn.Rows.Add(rowData);
            }

            return dtReturn;
        }

        private DataTable SelectData()
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            MyOraDB.ConnectName = COM.OraDB.ConnectDB.LMES;
            DataSet ds_ret;
            try
            {
                string process_name = "PKG_SMT_I_TMS.POPUP_MAIN_SELECT";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_PLANT";
                MyOraDB.Parameter_Name[1] = "ARG_MONTH";
                MyOraDB.Parameter_Name[2] = "ARG_LINE";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Values[0] = Plant;
                MyOraDB.Parameter_Values[1] = UcMonth.GetYearValue() + UcMonth.GetMonthValue();
                MyOraDB.Parameter_Values[2] = Line == "" ? Plant : Line;
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

        private void LoadScreen()
        {
            int iScreen = 1;
            try
            {
                DataTable dt = ComVar.Func.ReadXML(Application.StartupPath + "\\config.XML", "MAIN");
                int.TryParse(dt.Rows[0]["SCREEN"].ToString(), out iScreen);
            }
            catch
            { }

            Screen[] s = Screen.AllScreens;
            if (s.Count() == 1)
            {
              //  this.Bounds = s[s.Count() - 1].Bounds;
                Rectangle bounds = s[0].Bounds;
                this.SetBounds(bounds.X, bounds.Y, this.Width, this.Height);
                this.CenterToScreen();

            }
            else
            {
                Rectangle bounds = s[iScreen - 1].Bounds;
                this.SetBounds(bounds.X, bounds.Y, this.Width, this.Height);
                this.CenterToScreen();
                // this.Bounds = s[iScreen].Bounds;
            }
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void POPUP_PROD_BY_PLANT_Load(object sender, EventArgs e)
        {
            LoadScreen();
            UcMonth.ValueChangeEvent += UcMonth_ValueChangeEvent;
            pnMonth.Controls.Add(UcMonth);
            Text = PlantName;// + " Production";
           // tlsLoction.Visible = false;
            switch (Plant)
            {
                case "F1":
                case "F2":
                case "F3":
                case "F4":
                case "F5":
                case "VJ2":
                    ShowTree = true;
                    //tlsLoction.Visible = true;
                    //SetTreelist();
                    AddSeriesChart();
                    break;
                default:
                    ShowTree = false;
                    //tlsLoction.Visible = false;
                    break;
            }
            BindingData();
        }

        private void UcMonth_ValueChangeEvent(object sender, EventArgs e)
        {
            BindingData();
        }

        #region
        //private void SetTreelist()
        //{
        //    try
        //    {
        //        DataTable dt = GetDataLine();
        //        tlsLoction.DataSource = dt;
        //        tlsLoction.KeyFieldName = "ID";
        //        tlsLoction.ParentFieldName = "PARENTID";

        //        Skin skin = GridSkins.GetSkin(tlsLoction.LookAndFeel);
        //        skin.Properties[GridSkins.OptShowTreeLine] = true;

        //        foreach (TreeListNode node in tlsLoction.Nodes)
        //        {
        //            var dataRow = tlsLoction.GetDataRecordByNode(node);
        //            node.Tag = dataRow;
        //            string nodeId = node.GetValue("ID").ToString();
        //            node.Checked = true;
        //            node.Expanded = true;
        //            foreach (TreeListNode node1 in node.RootNode.Nodes)
        //            {

        //                node1.Checked = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //}


        private DataTable GetDataLine()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            string[] parentId = { "000", "000", "000", "000", "000", "000",
                                 "F1", "F1", "F1", "F1", "F1", "F1",
                                 "F2", "F2", "F2",
                                 "F3", "F3", "F3",
                                 "F4", "F4", "F4", "F4", "F4",
                                 "F5", "F5", "F5", "F5",
                                 "VJ2", "VJ2"
                               };

            string[] id = { "F1", "F2", "F3", "F4", "F5", "VJ2",
                                 "001", "002", "003", "004", "005", "006",
                                 "007", "008", "010",
                                 "011", "012", "099",
                                 "013", "014", "015", "016", "017",
                                 "018_1", "018_2", "019_1", "019_2",
                                 "201","202"
                               };
            string[] menuNm = { "Factory 1", "Factory 2", "Factory 3", "Factory 4", "Factory 5", "VJ2",
                                 "1", "2", "3", "4", "5", "6",
                                 "B", "C", "D",
                                 "E", "F", "N",
                                 "G", "H", "I", "J", "K",
                                 "L2", "L1", "M2", "M1",
                                 "LD","LE"
                               };

            dt.Columns.Add("PARENTID");
            dt.Columns.Add("ID");
            dt.Columns.Add("MENU_NM");
            for (int i = 0; i < id.Length; i++)
            {
                DataRow row = dt.NewRow();
                row["PARENTID"] = parentId[i];
                row["ID"] = id[i];
                row["MENU_NM"] = menuNm[i];
                dt.Rows.Add(row);
            }

            dt = ShowTree? dt.Select($"PARENTID = '{Plant}' or id = '{Plant}'").CopyToDataTable() :dt;
            return dt;
        }

        #endregion

        private void CheckNode(string argCode)
        {
            //if (tlsLoction.GetNodeList()[0].GetValue("ID").ToString() == argCode)
            //{
            //    foreach (TreeListNode node in tlsLoction.GetNodeList())
            //    {
            //        node.Checked = true;
            //    }
            //}
            //else
            //{
            //    foreach (TreeListNode node in tlsLoction.GetNodeList())
            //    {
            //        string nodeId = node.GetValue("ID").ToString();
            //        if (nodeId == argCode)
            //        {
            //            node.Checked = true;
            //        }
            //        else
            //        {
            //            node.Checked = false;
            //        }
            //    }
            //}
            
        }

        

        private void tlsLoction_MouseClick(object sender, MouseEventArgs e)
        {
            //string nodeId = tlsLoction.GetFocusedDataRow().ItemArray[1].ToString();
            //CheckNode(nodeId);
            //Line = nodeId;
            //BindingData();
            //MessageBox.Show(tlsLoction.GetFocusedDataRow().ItemArray[1].ToString());
        }

        
    }
}