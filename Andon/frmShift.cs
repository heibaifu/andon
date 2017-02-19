using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Andon
{
    public partial class frmShift : Form
    {
        BindingSource bs = new BindingSource();
        public frmShift()
        {
            InitializeComponent();
        }

        private void frmShift_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
            Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2,
            Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);

            loadGrid();

            txtShift.DataBindings.Add(new Binding("Text", bs, "shift_desc", true));
            dtpFrom.DataBindings.Add(new Binding("Value", bs, "time_from", true));
            dtpTo.DataBindings.Add(new Binding("Value", bs, "time_to", true));
            txtId.DataBindings.Add(new Binding("Text", bs, "id", true));
        }

        private void loadGrid()
        {
            DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM shifts");
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
            dgvShift.DataSource = bs;


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {

                inputStatas(true);
                btnEdit.Text = "Update";
                btnEdit.Enabled = true;
                btnAdd.Enabled = false;
                txtShift.Focus();
            }
            else
            {
                if (txtShift.Text == "")
                {
                    MessageBox.Show("Shift name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtShift.Focus();
                }
                else if (dtpFrom.Text == "")
                {
                    MessageBox.Show("Time from is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dtpFrom.Focus();
                }

                else if (dtpTo.Text == "")
                {
                    MessageBox.Show("Time to is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dtpTo.Focus();
                }
                else
                {

                    DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM shifts where shift_desc ='" + txtShift.Text.Trim().ToUpper() + "' AND id != '" + txtId.Text.ToString() + "'");
                    DataTable dt = ds.Tables["data"];
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Shift already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        txtShift.Focus();
                        txtShift.Text = "";
                    }
                    else
                    {
                        if (updateShift() == 1)
                        {
                            loadGrid();
                            inputStatas(false);
                            btnAdd.Text = "Add";
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Unable to update shift", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); ;
                        }
                    }

                    loadGrid();
                    inputStatas(false);
                    btnEdit.Text = "Edit";
                }
            }
        }

        private int updateShift()
        {

            return SqlCEHelper.ExecuteNonQuery("UPDATE shifts SET shift_desc=@shift_desc,time_from=@time_from,time_to=@time_to WHERE id=@id",
                new SqlCeParameter("@shift_desc", txtShift.Text.ToUpper()),
                new SqlCeParameter("@time_from", dtpFrom.Text),
                new SqlCeParameter("@time_to", dtpTo.Text),
                new SqlCeParameter("@id", txtId.Text.ToString()));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            inputStatas(false);
            btnAdd.Text = "Add";
            btnEdit.Text = "Edit";
            btnAdd.Enabled = true;
            loadGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete User", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                int retVal = SqlCEHelper.ExecuteNonQuery("DELETE FROM shifts where id ='" + txtId.Text + "'");
                if (retVal == 1)
                {
                    loadGrid();
                }
                else
                {
                    MessageBox.Show("Erorr deleting record.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                inputStatas(true);
                txtShift.Text = "";
                dtpFrom.Text = "";
                dtpTo.Text = "";
                btnAdd.Text = "Save";

                txtShift.Focus();


            }
            else
            {
                if (txtShift.Text == "")
                {
                    MessageBox.Show("Shift name is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtShift.Focus();
                }
                else if (dtpFrom.Text == "")
                {
                    MessageBox.Show("Time from is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dtpFrom.Focus();
                }

                else if (dtpTo.Text == "")
                {
                    MessageBox.Show("Time to is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    dtpTo.Focus();
                }
                else
                {
                    addShift();
                    loadGrid();
                    inputStatas(false);
                    btnAdd.Text = "Add";
                }
            }
        }
        private void inputStatas(Boolean status)
        {
            txtShift.Enabled = status;
            dtpFrom.Enabled = status;
            dtpTo.Enabled = status;

            btnEdit.Enabled = !status;
            btnDelete.Enabled = !status;
            btnCancel.Enabled = status;

            dgvShift.Enabled = !status;
        }

        private int addShift()
        {
            return SqlCEHelper.ExecuteNonQuery("INSERT INTO shifts (shift_desc, time_from, time_to) VALUES(@shift_desc, @time_from, @time_to)",
                new SqlCeParameter("@shift_desc", txtShift.Text.ToUpper()),
                new SqlCeParameter("@time_from", dtpFrom.Text),
                new SqlCeParameter("@time_to", dtpTo.Text));
        }

        private void dgvShift_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvShift.Select(this.dgvShift.CurrentRowIndex);
        }
    }
}