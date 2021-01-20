using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORM.UC
{
    public partial class UC_BOX : UserControl
    {
        [Category("LabelText"), Description("Test text displayed in the label.")]
        [Browsable(true)]
        public string Text
        {
            get => label1.Text;
            set => label1.Text = value;
        }
        public UC_BOX()
        {
            InitializeComponent();
        }
        
    }
}
