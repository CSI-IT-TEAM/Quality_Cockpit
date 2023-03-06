using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FORM.UC
{
    public partial class UC_WEATHER : UserControl
    {
        public UC_WEATHER()
        {
            InitializeComponent();
        }
        public Action<string> clickdate = null;
        bool bClick = false;
        public string strDate = "";

        public void Image(string strImg)
        {
            try
            {
                if (strImg == "SUN")
                {
                    pctBox.Image = Properties.Resources.partly_cloudy;
                }
                else if (strImg == "RAIN")
                {
                    pctBox.Image = Properties.Resources.raining;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void BindingData(int iDx, DataTable dt)
        {
            try
            {
                lblDate.Text = "";
                lblTemp.Text = "";
                lblHumi.Text = "";
                bClick = false;
                if (dt != null && dt.Rows.Count > 0) 
                {
                    lblDate.Text = dt.Rows[iDx]["CAL_DATE_NM"].ToString();
                    lblDate.Tag  = iDx;
                    lblTemp.Text = dt.Rows[iDx]["TMP_VL"].ToString() + "°";
                    lblHumi.Text = dt.Rows[iDx]["HUMI_VL"].ToString() + "%";
                    CheckColor();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void CheckColor()
        {
            try
            {
                if (!bClick)
                {
                    if (lblDate.Text.Replace("-", "").Equals(strDate))
                    {
                        lblDate.BackColor = Color.Gray;
                        lblDate.ForeColor = Color.White;
                    }
                    else
                    {
                        lblDate.BackColor = Color.FromArgb(75, 88, 184);
                        lblDate.ForeColor = Color.White;
                    }
                }
                else
                {
                    lblDate.BackColor = Color.Gray;
                    lblDate.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void lblDate_Click(object sender, EventArgs e)
        {
            try
            {
                bClick = true;
                if (clickdate == null) return;
                clickdate((sender as Label).Text.Replace("-", ""));
                CheckColor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
