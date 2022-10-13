using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.IO;
//using JPlatform.Client.Controls;


namespace FORM
{
    public partial class SMT_QUALITY_COCKPIT_MAIN : Form
    {
        public SMT_QUALITY_COCKPIT_MAIN()
        {
            InitializeComponent();
            initForm();

        }

        private const int SW_MAXIMIZE = 3;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        int _iReload = 0;
        DataTable _dtMasterLine;
        DataTable _dtGMES;
        // Dictionary<string, UC.UC_Chart_Donut> _dicLocation = new Dictionary<string, UC.UC_Chart_Donut>();
        Dictionary<string, Button_Status> _dicLine = new Dictionary<string, Button_Status>();
        Dictionary<string, UC.UC_Factory> _dicFac = new Dictionary<string, UC.UC_Factory>();

        private void SMT_QUALITY_COCKPIT_MAIN_Load(object sender, EventArgs e)
        {
            SetButtonBack();
            _dtGMES = ComVar.Func.ReadXML(Application.StartupPath + @"\Config.xml", "GMES");

        }

        private void SMT_SCADA_COCKPIT_MENU_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //_dtTest = await GetDataAsync();
                // DataTable dt = Data_Select("");

                _iReload = 29;
                tmrTime.Start();

                tmrBlink.Start();
            }
            else
            {
                tmrTime.Stop();
                tmrBlink.Stop();
                Dispose();
            }

        }

        private void SetButtonBack()
        {
            try
            {
                DataTable dtXML = ComVar.Func.ReadXML(Application.StartupPath + "\\Config.XML", "MAIN");
                if (dtXML.Rows[0]["grpForm"].ToString() == "QUALITY_COCKPIT")
                {
                    cmdBack.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        #region Init Form

        private async void initForm()
        {
            try
            {


                // aPn3.Visible = false;

                // picSCADA.BackgroundImage = Image.FromFile(Application.StartupPath + @"\img\Start_up.png");



                _dtMasterLine = await Master_Select("");// createDatatableTest();// Master_Select("");

                DataTable dtFactory = _dtMasterLine.Select("FACTORY = 'F1'").CopyToDataTable();

                //create_Line_F1(dtFactory);

                int x = 45, y = 50;
                dtFactory = _dtMasterLine.Select("FACTORY = 'F1'").CopyToDataTable();
                create_Line(pnF1, dtFactory, x, y);
                // create_Line_F1(pnF1, dtFactory, x, ref y, "FGA");
                //  y += 10;
                //  create_Line_F1(pnF1, dtFactory, x, ref y, "FSS");

                // x = 74;
                // y = 35;
                dtFactory = _dtMasterLine.Select("FACTORY = 'F2'").CopyToDataTable();
                create_Line(pnF2, dtFactory, x, y);

                //  x = 74;
                // y = 35;
                dtFactory = _dtMasterLine.Select("FACTORY = 'F3'").CopyToDataTable();
                create_Line(pnF3, dtFactory, x, y);

                // x = 74;
                //  y = 35;
                dtFactory = _dtMasterLine.Select("FACTORY = 'F4'").CopyToDataTable();
                create_Line(pnF4, dtFactory, x, y);

                // x = 74;
                // y = 35;
                dtFactory = _dtMasterLine.Select("FACTORY = 'F5'").CopyToDataTable();
                create_Line(pnF5, dtFactory, x, y);

                dtFactory = _dtMasterLine.Select("FACTORY = 'LT'").CopyToDataTable();
                create_Line(pnLT, dtFactory, x, y);

                create_Factory(gpExF1, "F1");
                create_Factory(gpExF2, "F2");
                create_Factory(gpExF3, "F3");
                create_Factory(gpExF4, "F4");
                create_Factory(gpExF5, "F5");
                create_Factory(gpExLT, "LT");

                //setData();

                //initAPanel(gpF1, "F1", dt);
                //initAPanel(gpF2, "F2", dt);
                //initAPanel(gpF3, "F3", dt);
                //initAPanel(gpF4, "F4", dt);
                //initAPanel(gpF5, "F5", dt);

                // tmrBlink.Start();




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {

            }
        }

        private void create_Line(Panel pnControl, DataTable argDt, int locStartX, int locStartY)
        {
            int locX = locStartX, locY = locStartY;
            Font lineTextButtonFont = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold);
            Font buttonMlineFont = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            Font buttonLineFont = new System.Drawing.Font("Calibri", 40F, System.Drawing.FontStyle.Bold);
            Font buttonLineFont2 = new System.Drawing.Font("Calibri", 32F, System.Drawing.FontStyle.Bold);
            Point buttonLoc;
            Size buttonSize;
            Size lineTextButtonSize;
            Size lineButtonSize = new System.Drawing.Size(40, 27);
            Size buttonStatusSize = new System.Drawing.Size(55, 27);
            Dictionary<string, string> dicValue = new Dictionary<string, string>();
            string lineCd, factory = argDt.Rows[0]["FACTORY"].ToString();
            int iNumLine;
            const int sizeTextHead = 40;

            dicValue.Add("NAME", "");
            dicValue.Add("TEXT", "");
            dicValue.Add("BACK_COLOR", "");
            dicValue.Add("FORE_COLOR", "");
            dicValue.Add("TAG", "");
            Button cmdLine;

            //head line name
            buttonLoc = new Point(locStartX, 0);
            lineTextButtonSize = new System.Drawing.Size(lineButtonSize.Width, sizeTextHead);
            dicValue["NAME"] = "cmd_LineNm" + factory + "_TXT";
            dicValue["TEXT"] = "Line";
            dicValue["BACK_COLOR"] = "WHITE";
            dicValue["FORE_COLOR"] = "BLACK";
            dicValue["TAG"] = "";

            cmdLine = createButton(dicValue, buttonLoc, lineTextButtonSize, lineTextButtonFont);
            pnControl.Controls.Add(cmdLine);

            int LocStartXLineText = locStartX + lineButtonSize.Width;
            int SpaceButton = 1;
            //
            buttonLoc = new Point(LocStartXLineText + SpaceButton, 0);
            lineTextButtonSize = new System.Drawing.Size(buttonStatusSize.Width, sizeTextHead);
            dicValue["NAME"] = "cmd_NPI" + factory + "_TXT";
            dicValue["TEXT"] = "NPI";
            dicValue["TAG"] = "";
            cmdLine = createButton(dicValue, buttonLoc, lineTextButtonSize, lineTextButtonFont);
            pnControl.Controls.Add(cmdLine);

            buttonLoc = new Point(LocStartXLineText + buttonStatusSize.Width * 1 + SpaceButton * 2, 0);
            lineTextButtonSize = new System.Drawing.Size(buttonStatusSize.Width, sizeTextHead);
            dicValue["NAME"] = "cmd_REW" + factory + "_TXT";
            dicValue["TEXT"] = "Rework";
            dicValue["TAG"] = "";
            cmdLine = createButton(dicValue, buttonLoc, lineTextButtonSize, lineTextButtonFont);
            pnControl.Controls.Add(cmdLine);

            buttonLoc = new Point(LocStartXLineText + buttonStatusSize.Width * 2 + SpaceButton * 3, 0);
            lineTextButtonSize = new System.Drawing.Size(buttonStatusSize.Width, sizeTextHead);
            dicValue["NAME"] = "cmd_OSD" + factory + "_TXT";
            dicValue["TEXT"] = "Internal " + @"OS&&D";
            dicValue["TAG"] = "";
            cmdLine = createButton(dicValue, buttonLoc, lineTextButtonSize, lineTextButtonFont);
            pnControl.Controls.Add(cmdLine);

            buttonLoc = new Point(LocStartXLineText + buttonStatusSize.Width * 3 + SpaceButton * 4, 0);
            lineTextButtonSize = new System.Drawing.Size(buttonStatusSize.Width, sizeTextHead);
            dicValue["NAME"] = "cmd_BON" + factory + "_TXT";
            dicValue["TEXT"] = "Bonding GAP";
            dicValue["TAG"] = "";
            cmdLine = createButton(dicValue, buttonLoc, lineTextButtonSize, lineTextButtonFont);
            pnControl.Controls.Add(cmdLine);



            foreach (DataRow row in argDt.Rows)
            {
                int.TryParse(row["NUM_FGA"].ToString(), out iNumLine);
                lineCd = row["LINE_CD"].ToString();

                int iStart = 0;
                if (lineCd == "018" || lineCd == "019") iStart = 4;
                //lineCd = lineCd.Replace("_1", "");
                for (int iLine = iStart + 1; iLine <= iStart + iNumLine; iLine++)
                {
                    string lineAndMline = lineCd.Replace("_1", "") + "_" + iLine.ToString("000");
                    if (factory == "F1")
                    {

                        lineAndMline = iLine == 2 ? "003_000" : iLine == 3 ? "002_000" : iLine.ToString("000") + "_000";
                    }


                    buttonLoc = new Point(locX, locY);
                    dicValue["NAME"] = "cmd_" + lineAndMline + "_" + "MNM";
                    dicValue["TEXT"] = iLine.ToString();
                    dicValue["BACK_COLOR"] = "BLACK";
                    dicValue["FORE_COLOR"] = "WHITE";
                    dicValue["TAG"] = "";
                    cmdLine = createButton(dicValue, buttonLoc, lineButtonSize, buttonMlineFont);
                    pnControl.Controls.Add(cmdLine);

                    locX += lineButtonSize.Width + SpaceButton;

                    //NPI
                    buttonLoc = new Point(locX, locY);
                    dicValue["NAME"] = "cmd_" + lineAndMline + "_" + "NPI" + "_" + "STA";
                    dicValue["TAG"] = lineAndMline + "_" + "NPI";
                    dicValue["TEXT"] = "";
                    dicValue["BACK_COLOR"] = "GREEN";

                    dicValue["FORE_COLOR"] = "WHITE";
                    cmdLine = createButton(dicValue, buttonLoc, buttonStatusSize, buttonMlineFont);
                    pnControl.Controls.Add(cmdLine);

                    //Rework
                    dicValue["NAME"] = "cmd_" + lineAndMline + "_" + "REW" + "_" + "STA";
                    dicValue["TAG"] = lineAndMline + "_" + "REW";
                    dicValue["BACK_COLOR"] = "GREEN";
                    buttonLoc = new Point(locX += buttonStatusSize.Width + SpaceButton, locY);
                    cmdLine = createButton(dicValue, buttonLoc, buttonStatusSize, buttonMlineFont);
                    pnControl.Controls.Add(cmdLine);

                    //OS&D
                    dicValue["NAME"] = "cmd_" + lineAndMline + "_" + "OSD" + "_" + "STA";
                    dicValue["TAG"] = lineAndMline + "_" + "OSD";
                    buttonLoc = new Point(locX += buttonStatusSize.Width + SpaceButton, locY);
                    cmdLine = createButton(dicValue, buttonLoc, buttonStatusSize, buttonMlineFont);
                    pnControl.Controls.Add(cmdLine);

                    //Bonding
                    dicValue["NAME"] = "cmd_" + lineAndMline + "_" + "BON" + "_" + "STA";
                    dicValue["TAG"] = lineAndMline + "_" + "BON";
                    buttonLoc = new Point(locX += buttonStatusSize.Width + SpaceButton, locY);
                    cmdLine = createButton(dicValue, buttonLoc, buttonStatusSize, buttonMlineFont);
                    pnControl.Controls.Add(cmdLine);


                    locX = locStartX;
                    locY += buttonStatusSize.Height + 3;
                }

                //Lean Name
                buttonLoc = new Point(3, locStartY);
                buttonSize = new System.Drawing.Size(40, locY - locStartY - 3);
                dicValue["NAME"] = "cmd_" + lineCd + "_" + "LNM"; ;
                dicValue["TEXT"] = row["LINE_NM"].ToString();
                dicValue["BACK_COLOR"] = "BLACK";
                dicValue["FORE_COLOR"] = "WHITE";
                dicValue["TAG"] = "";

                if (dicValue["TEXT"].Count() > 1)
                {
                }

                cmdLine = createButton(dicValue, buttonLoc, buttonSize, dicValue["TEXT"].Count() > 1 ? buttonLineFont2 : buttonLineFont);
                pnControl.Controls.Add(cmdLine);

                locStartY = locY += 5;
            }

        }

        private Button createButton(Dictionary<string, string> value, Point location, Size size, Font font)
        {
            try
            {
                Button cmd = new Button();


                cmd.FlatAppearance.BorderSize = 0;
                cmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                cmd.Font = font;
                cmd.Location = location;
                cmd.BackColor = Color.FromName(value["BACK_COLOR"]);
                cmd.ForeColor = Color.FromName(value["FORE_COLOR"]);
                cmd.Name = value["NAME"];
                //cmd.Tag = value["NAME"].Remove(0, 4);
                cmd.Size = size;
                cmd.Text = value["TEXT"];
                cmd.UseVisualStyleBackColor = true;
                cmd.Click += new EventHandler(Button_Line_Click);



                if (value["TAG"] != "")
                {
                    Button_Status con = new Button_Status();
                    con.button = cmd;
                    con.status = "GREEN";
                    _dicLine.Add(value["TAG"].ToString(), con);
                }
                return cmd;
            }
            catch (Exception)
            {
                return null;

            }

        }

        private void create_Factory(GroupBoxEx gpEx, string factory)
        {
            int iStartX = 12, iStartY = 49;
            int iLocX = iStartX, iLocY = iStartY;
            int iSizeW = 95, iSizeH = 93;
            string[] area = { "GREEN", "YELLOW", "RED" };
            for (int iPart = 1; iPart <= 3; iPart++)
            {
                createUcFactory(gpEx, factory + "_" + area[iPart - 1], new Point(iLocX, iLocY), new Size(iSizeW, iSizeH));
                iLocX += iSizeW + 2;
            }
        }

        private void createUcFactory(GroupBoxEx gpEx, string tag, Point location, Size size)
        {
            UC.UC_Factory uC_Factory = new UC.UC_Factory();

            uC_Factory.BackColor = System.Drawing.Color.Transparent;
            uC_Factory.Location = location;
            uC_Factory.Size = size;
            uC_Factory.Tag = tag;

            gpEx.Controls.Add(uC_Factory);
            if (tag != "")
                _dicFac.Add(tag, uC_Factory);


        }



        #endregion Init Form

        #region set Data


        //private async Task<int> SetDataTest()
        //{
        //    // DataTable dt = null;
        //    await _dtTest = GetDataAsync();
        //   // await Task.Delay(10000);
        //    return dt;
        //}

        private async Task<DataTable> GetDataAsync()
        {
            return await Task.Run(() => {

                DataTable dt = Data_Select("");

                return dt;
            });
        }


        private void setData()
        {

            DataTable dt = Data_Select("");

            //reset color line
            foreach (var item in _dicLine)
            {
                if (item.Value.status != "GREEN")
                {
                    item.Value.status = "GREEN";
                    item.Value.button.BackColor = Color.Green;
                }
            }

            //reset color Factory
            foreach (var item in _dicFac)
            {
                if (item.Value._Color != "GREEN")
                {
                    item.Value._Color = "GREEN";
                    item.Value.setColor("GREEN");
                    item.Value.Visible = true;
                }
            }

            DataRow[] dr;
            Dictionary<string, string> dicStatus = new Dictionary<string, string>();
            string location;
            if (_dtMasterLine == null) return;
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    string lineCd = row["LINE_CD"].ToString();
                    if (lineCd == "001" || lineCd == "002" || lineCd == "003" || lineCd == "004" || lineCd == "005" || lineCd == "006")
                    {
                        location = "F1";
                    }
                    else
                    {
                        dr = _dtMasterLine.Select("LINE_CD = '" + lineCd + "'");
                        if (dr.Count() == 0) continue;
                        location = dr[0][0].ToString();
                    }

                    //Set color Line
                    if (_dicLine.ContainsKey(row["NAME_CONTROL"].ToString()))
                    {
                        _dicLine[row["NAME_CONTROL"].ToString()].status = row["STATUS"].ToString();
                        _dicLine[row["NAME_CONTROL"].ToString()].button.BackColor = Color.FromName(row["STATUS"].ToString());
                    }

                    if (row["STATUS"].ToString() == "RED")
                    {
                        _dicFac[location + "_RED"]._Color = row["STATUS"].ToString();
                        _dicFac[location + "_RED"].setColor(row["STATUS"].ToString());
                    }
                    else if (row["STATUS"].ToString() == "YELLOW")
                    {
                        _dicFac[location + "_YELLOW"]._Color = row["STATUS"].ToString();
                        _dicFac[location + "_YELLOW"].setColor(row["STATUS"].ToString());
                    }



                    //Set color Factory
                    //if (_dicFac[location]._Color != "RED")
                    //{
                    //    _dicFac[location].setColor(row["STATUS"].ToString()); 
                    //    _dicFac[location]._Color = row["STATUS"].ToString();                     
                    //}


                    //if (dicStatus.ContainsKey(location))
                    //{
                    //    if (dicStatus[location] == "YELLOW" && row["STATUS"].ToString() == "RED")
                    //    {
                    //        dicStatus[location] = row["STATUS"].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    dicStatus[location] = row["STATUS"].ToString();
                    //}
                }
                catch (Exception ex)
                {
                    ComVar.Var.writeToLog("MAIN --> setData: " + ex.ToString());
                }
            }





            //_dicFac["F4_UPN"].setColor("YELLOW");
            //_dicFac["F4_FSS"].setColor("YELLOW");
            //_dicFac["F4_FGA"].setColor("YELLOW");

            //   _dicFac["F5_FSS"].setColor("YELLOW");



        }
        #endregion Set Data

        #region Button Line Click
        private void Button_Line_Click(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            string[] strArr = ctr.Name.Split('_');

            if (strArr[strArr.Length - 1] == "LNM")
            {
                CallDigitalShopFloor(strArr);
            }
            else if (strArr[strArr.Length - 1] == "STA")
            {
                CallForm(strArr);
            }
        }

        private void CallForm(string[] argLine)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var._strValue1 = argLine[1];
            if (ComVar.Var._strValue1 == "001" ||
                ComVar.Var._strValue1 == "004" ||
                ComVar.Var._strValue1 == "005" ||
                ComVar.Var._strValue1 == "006")
            {
                ComVar.Var._strValue2 = ComVar.Var._strValue1;
            }
            else if (ComVar.Var._strValue1 == "002")
            {
                ComVar.Var._strValue2 = "003";
            }
            else if (ComVar.Var._strValue1 == "003")
            {
                ComVar.Var._strValue2 = "002";
            }
            else
            {
                ComVar.Var._strValue2 = argLine[2];
            }

            switch (argLine[argLine.Length - 2])
            {
                case "NPI":
                    ComVar.Var.callForm = "904";
                    break;
                case "REW":
                    ComVar.Var.callForm = "901";
                    break;
                case "OSD":
                    ComVar.Var.callForm = "902";
                    break;
                case "BON":
                    ComVar.Var.callForm = "903";
                    break;
            }
        }

        private void CallDigitalShopFloor(string[] argLineCd)
        {
            //current nos K using code 017
            string line_cd = argLineCd[1] == "017" ? "009" : argLineCd[1];

            //if NOSL line 5,6,7,8 
            if (argLineCd.Length == 4 && line_cd == "018")
            {
                ComVar.Var._Area = "NOS_L2.";
            }
            else
            {
                ComVar.Var._Area = "NOS.";
            }

            //setting call form
            ComVar.Var._IsBack = true;
            ComVar.Var._Frm_Back = "900";
            ComVar.Var._strValue1 = line_cd;

            //Using for NOS N
            if (line_cd == "099" || line_cd == "202")
            {
                ComVar.Var.callForm = "140";
            }

            //Using for F1
            else if (line_cd == "001" || line_cd == "002" || line_cd == "003" || line_cd == "004" || line_cd == "005" || line_cd == "006")
            {
                ComVar.Var.callForm = "245";
            }

            //Using for NOS have 4 line FGA
            else
            {
                ComVar.Var.callForm = "69";
            }
        }

        #endregion Button Line Click

        #region DB




        private async Task<DataTable> Master_Select(string argType)
        {
            return await Task.Run(() =>
            {
                COM.OraDB MyOraDB = new COM.OraDB();

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.MASTER_PLANT_SELECT";

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = argType;
                MyOraDB.Parameter_Values[1] = "";

                MyOraDB.Add_Select_Parameter(true);
                DataSet retDS = MyOraDB.Exe_Select_Procedure();
                if (retDS == null) return null;

                return retDS.Tables[MyOraDB.Process_Name];
            });
        }



        private DataTable Data_Select(string argType)
        {
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ShowErr = true;

            MyOraDB.ReDim_Parameter(2);
            MyOraDB.Process_Name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.MAIN_SELECT";

            MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
            MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS.Tables[MyOraDB.Process_Name];
        }

        private DataTable Data_Select_Machine(string argType, string argLine, string argMline, string argArea)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            //  MyOraDB.ShowErr = true;
            MyOraDB.ReDim_Parameter(5);
            MyOraDB.Process_Name = "MES.PKG_SMT_SCADA_COCKPIT.PM_SELECT_MACHINE_ALERT";

            MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
            MyOraDB.Parameter_Name[1] = "ARG_LINE";
            MyOraDB.Parameter_Name[2] = "ARG_MLINE";
            MyOraDB.Parameter_Name[3] = "ARG_AREA";
            MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

            MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
            MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

            MyOraDB.Parameter_Values[0] = argType;
            MyOraDB.Parameter_Values[1] = argLine;
            MyOraDB.Parameter_Values[2] = argMline;
            MyOraDB.Parameter_Values[3] = argArea;
            MyOraDB.Parameter_Values[4] = "";

            MyOraDB.Add_Select_Parameter(true);
            DataSet retDS = MyOraDB.Exe_Select_Procedure();
            if (retDS == null) return null;

            return retDS.Tables[0];
        }

        #endregion DB

        #region Event
        private void tmrTime_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            _iReload++;
            if (_iReload >= 20)
            {
                _iReload = 0;
                // setDataTest();
                setData();

                //uC_Chart_Donut1.setColor("Red");
                // uC_Chart_Pie1.setColor("Green");
            }
        }

        private void cmdF4_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var._strValue1 = "F4";
            ComVar.Var.callForm = "617";

        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // ComVar.Var._IsBack = true;
            // ComVar.Var.callForm = "617";
            //string Path = @"vnc\013.vnc";
            ////string path2 = @"C:\Program Files\RealVNC\VNC Viewer\vncviewer.exe";

            //Process startVNC = new Process();

            //try 
            //{
            //    startVNC.StartInfo.FileName = Path;
            //    if (startVNC.Start())
            //    {
            //        startVNC.WaitForInputIdle();
            //        System.Threading.Thread.Sleep(100);
            //        SendKeys.Send(@"Pop*2@19");
            //        SendKeys.Send("{ENTER}");

            //        //startVNC.WaitForInputIdle();
            //        //SendKeys.Send("172.30.105.108:5995");
            //        //SendKeys.Send("{ENTER}");
            //        //startVNC.WaitForInputIdle();
            //        //System.Threading.Thread.Sleep(100);
            //        //SendKeys.Send("Pop*2@19");
            //        //SendKeys.Send("{ENTER}");
            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


        }

        private void tmrBlink_Tick(object sender, EventArgs e)
        {
            foreach (var item in _dicLine)
            {
                if (item.Value.status == "RED" && item.Value.button.BackColor == Color.Red)
                    item.Value.button.BackColor = Color.White;
                else if (item.Value.status == "RED" && item.Value.button.BackColor == Color.White)
                    item.Value.button.BackColor = Color.Red;
            }

            foreach (var item in _dicFac)
            {
                if (item.Value._Color == "RED" && item.Value.Visible)
                    item.Value.Visible = false;
                else if (item.Value._Color == "RED" && !item.Value.Visible)
                    item.Value.Visible = true;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "minimized";
        }

        private void cmdDowntime_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "680";
        }

        private void btnEnergy_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "682";
        }

        #region QMS Click

        private void cmd_QMS_Click(object sender, EventArgs e)
        {
            try
            {
                string path = _dtGMES.Rows[0]["path"].ToString();
                string str_ProcessName = _dtGMES.Rows[0]["PROCESS_NAME"].ToString();

                if (!ProgramIsRunning(path))
                //Process.Start(patch);
                {
                    Process p = Process.Start(path);
                    p.WaitForInputIdle();
                    Thread.Sleep(2000);
                    SendKeys.SendWait("1{enter}"); //VJIT{tab}

                }
                else
                {
                    var ipex = Process.GetProcesses().Where(pr => pr.ProcessName == str_ProcessName);
                    foreach (var process in ipex)
                    {
                        //process.Kill();
                        var p = System.Diagnostics.Process.GetProcessesByName(str_ProcessName).FirstOrDefault();
                        ShowWindow(p.MainWindowHandle, SW_MAXIMIZE);
                    }
                }
            }
            catch (Exception ex)
            {
                ComVar.Var.writeToLog(this.GetType().Name + "-->FORM_MAIN-Load-->Err: " + ex.ToString());
            }
        }

        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    isRunning = true;
                    break;
                }

            return isRunning;
        }
        #endregion




        #endregion

        private void lblFTY_Click(object sender, EventArgs e)
        {
            using (POPUP_PROD_BY_PLANT pop = new POPUP_PROD_BY_PLANT())
            {
                Control ctr = (Control)sender;
                pop.Plant = ctr.Tag.ToString();
                pop.PlantName = ctr.Text;
                pop.ShowDialog();
            }
        }

        private void CmdDasboard_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "905";

        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        private void cmdRework_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "907";
        }
        private void cmdHFPA_Click_1(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "908";
        }

        private void cmdDefective_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "910";
        }

        private void cmdBCGrade_Click(object sender, EventArgs e)
        {
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "909";
        }

        private void cmd_FTY_OSD_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;

            Panel panel = (Panel)sender;

            ComVar.Var._strValue1 = panel.Tag.ToString();
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "973";

            this.Cursor = Cursors.Default;
        }

        private void cmd_FTY_OSD_Label_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;

            Label label = (Label)sender;

            ComVar.Var._strValue1 = label.Tag.ToString();
            ComVar.Var._IsBack = true;
            ComVar.Var.callForm = "973";

            this.Cursor = Cursors.Default;
        }
    }

    public class Button_Status
    {
        public Button button;
        public string status;
    }
}
