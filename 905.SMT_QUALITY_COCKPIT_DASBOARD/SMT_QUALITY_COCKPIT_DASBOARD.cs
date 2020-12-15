using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.BandedGrid;
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
    public partial class SMT_QUALITY_COCKPIT_DASBOARD : Form
    {
        public SMT_QUALITY_COCKPIT_DASBOARD()
        {
            InitializeComponent();
        }
        int _time = 0;
        string _CurrentDay = DateTime.Now.ToString("MMM - dd");

        #region Event

        private void Form_Load(object sender, EventArgs e)
        {
           
        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
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
            }
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }

        #endregion Event

        #region Database
        private DataTable Fn_SelectDataCombo(string argType, string argPlant, string argLine )
        {           
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.NPI_COMBO_SELECT";

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

        public async Task<DataTable> Fn_SelectDataGrid(string argType, string argPlan, string argLine, string argFromDate, string argToDate)
        {
            return await Task.Run(() => {
                COM.OraDB MyOraDB = new COM.OraDB();
                DataSet ds_ret;
                MyOraDB.ShowErr = true;
                try
                {
                    string process_name = "SEPHIROTH.PKG_SMT_QUALITY_COCKPIT_02.NPI_DATA_SELECT";

                    MyOraDB.ReDim_Parameter(7);
                    MyOraDB.Process_Name = process_name;

                    MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                    MyOraDB.Parameter_Name[1] = "V_P_FACTORY";
                    MyOraDB.Parameter_Name[2] = "V_P_PLANT";
                    MyOraDB.Parameter_Name[3] = "V_P_MLINE";
                    MyOraDB.Parameter_Name[4] = "V_P_FROM";
                    MyOraDB.Parameter_Name[5] = "V_P_TO";
                    MyOraDB.Parameter_Name[6] = "OUT_CURSOR";

                    MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[4] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[5] = (int)OracleType.VarChar;
                    MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;

                    MyOraDB.Parameter_Values[0] = argType;
                    MyOraDB.Parameter_Values[1] = "2110";
                    MyOraDB.Parameter_Values[2] = argPlan;
                    MyOraDB.Parameter_Values[3] = argLine;
                    MyOraDB.Parameter_Values[4] = argFromDate;
                    MyOraDB.Parameter_Values[5] = argToDate;
                    MyOraDB.Parameter_Values[6] = "";

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
