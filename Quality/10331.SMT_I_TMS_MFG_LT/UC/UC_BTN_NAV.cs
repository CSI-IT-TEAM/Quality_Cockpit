using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM.UC
{
    public partial class UC_BTN_NAV : UserControl
    {
        public UC_BTN_NAV()
        {
            InitializeComponent();
            lblTitle.Click += new EventHandler(UC_BTN_NAV_Click);
            pictureEdit1.Click += new EventHandler(UC_BTN_NAV_Click);
            lblDecr.Click += new EventHandler(UC_BTN_NAV_Click);
        }
        public delegate void UcClick(int tag);
        public UcClick OnUcClick = null;
        private void UC_BTN_NAV_Click(object sender, EventArgs e)
        {
            if (OnUcClick != null)
            {
                OnUcClick(Convert.ToInt32(this.Tag));

            }
        }

        public void SetData(ButtonModel model)
        {
            lblTitle.Text = model.HEADER_TEXT;
            lblDecr.Text = model.DESCR_TEXT;
        }
        public void SetColor()
        {
            this.BackColor = Color.FromArgb(70, 104, 165);
        }

        public void SetDefaultColor()
        {
            this.BackColor = Color.FromArgb(70, 158, 165);
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
