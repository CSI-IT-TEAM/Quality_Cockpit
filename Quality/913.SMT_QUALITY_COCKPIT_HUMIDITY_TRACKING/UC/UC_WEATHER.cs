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
        public Action<UserControl, Label> ucClickDate = null;
        string sImg = string.Empty;
        public Action<string> clickdate = null;
        bool bClick = false;
        public string strDate = "";

        public void Image(string strImg)
        {
            try
            {
                lblTemp.ForeColor = Color.Magenta;
                if (strImg == "SUN")
                {
                    pctBox.Image = Properties.Resources.partly_cloudy;
                    lblDate.BackColor = Color.FromArgb(75, 88, 184);
                    lblDate.ForeColor = Color.White;
                }
                else if (strImg == "RAIN")
                {
                    pctBox.Image = Properties.Resources.raining;
                    lblDate.BackColor = Color.Red;
                    lblDate.ForeColor = Color.White;
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
                    sImg = dt.Rows[iDx]["SHOW"].ToString();
                    Image(sImg);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //private void CheckColor()
        //{
        //    try
        //    {
        //        if (!bClick)
        //        {
        //            if (lblDate.Text.Replace("-", "").Equals(strDate))
        //            {
        //                lblDate.BackColor = Color.Gray;
        //                lblDate.ForeColor = Color.White;
        //            }
        //            else
        //            {
        //                lblDate.BackColor = Color.FromArgb(75, 88, 184);
        //                lblDate.ForeColor = Color.White;
        //            }
        //        }
        //        else
        //        {
        //            lblDate.BackColor = Color.Gray;
        //            lblDate.ForeColor = Color.White;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
        private void lblDate_Click(object sender, EventArgs e)
        {
            try
            {
                bClick = true;
                //if (clickdate == null) return;
                //clickdate((sender as Label).Text.Replace("-", ""));
                Label lbl = ((Label)sender);
                if (ucClickDate != null)
                {
                    ucClickDate(this, lbl);
                }
                //   CheckColor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void lblDate_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblDate.DisplayRectangle,
                                        Color.Blue, 3, ButtonBorderStyle.Solid,
                                        Color.Blue, 3, ButtonBorderStyle.Solid,
                                        Color.Blue, 3, ButtonBorderStyle.Solid,
                                        Color.Blue, 3, ButtonBorderStyle.Solid);
        }

        public void lblDate_Paint2(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblDate.DisplayRectangle,
                                        Color.Transparent, 0, ButtonBorderStyle.None,
                                        Color.Transparent, 0, ButtonBorderStyle.None,
                                        Color.Transparent, 0, ButtonBorderStyle.None,
                                        Color.Transparent, 0, ButtonBorderStyle.None);
        }

        internal void PaintBorder(bool isPaint)
        {
            lblDate.Paint -= lblDate_Paint;
            lblDate.Paint -= lblDate_Paint2;
            if (isPaint)
            {

                lblDate.Paint += lblDate_Paint;
            }
            else
            {
                lblDate.Paint += lblDate_Paint2;

            }
        }
    }
}
