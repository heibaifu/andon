using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Andon
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            using (frmUser user = new frmUser())
            {
                user.ShowDialog();
            }
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            using (frmLine line = new frmLine())
            {
                line.ShowDialog();
            }
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            using (frmShift shift = new frmShift())
            {
                shift.ShowDialog();
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            using (frmSchedule schedule = new frmSchedule())
            {
                schedule.ShowDialog();
            }
        }

    }
}