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

using System.Threading;
using System.IO;
using System.Drawing.Drawing2D;
//using JPlatform.Client.Controls;


namespace FORM
{
    public partial class SMT_I_TMS_INV : Form
    {
        public SMT_I_TMS_INV()
        {            
            InitializeComponent();
           
            
        }
        int _iReload = 0;
        private void SMT_QUALITY_COCKPIT_MAIN_Load(object sender, EventArgs e)
        {
            cmdBack.Visible = ComVar.Var._IsBack;
        }

        private void SMT_SCADA_COCKPIT_MENU_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                _iReload = 29;
               
                
                
            }
            else
            {
               
            }

        }

        private void SMT_I_TMS_INV_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = "back";
        }
    }

}
