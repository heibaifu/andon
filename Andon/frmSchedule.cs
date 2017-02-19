using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Andon
{
    public partial class frmSchedule : Form
    {
        BindingSource bs = new BindingSource();
        public frmSchedule()
        {
            InitializeComponent();
        }

        private void frmSchedule_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
            Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2,
            Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);

            loadGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel Files|*.xls";

            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .CUR file was selected, open it.  
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.  
                //this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }   
        }

        public void loadGrid()
        {
            DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT schedules.id, schedules.sku, schedules.target, " +
            "lines.line, shifts.shift_desc, schedules.run_date " +
            "FROM schedules " +
            "INNER JOIN lines ON schedules.id = lines.id " +
            "INNER JOIN shifts ON schedules.id = shifts.id");
            DataTable dtable = ds.Tables["data"];
           
            bs.DataSource = dtable;
            if (dtable.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            dgvSchedule.DataSource = bs;
        }
    }
}