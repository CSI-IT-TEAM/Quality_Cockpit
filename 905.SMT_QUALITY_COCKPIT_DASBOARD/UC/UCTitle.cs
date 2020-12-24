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
    public partial class UCTitle : UserControl
    {
        public UCTitle()
        {
            InitializeComponent();
        }

        public void SetStatus(string argStatus)
        {
            cmdStatus.BackColor = Color.FromName(argStatus);
        }

        public void SetTitle(string argTitle)
        {
            cmdTitle.Text = argTitle;
        }

        public void SetText(string argText)
        {
            cmdText.Text = argText;
        }
    }
}
