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
    public partial class frmLine : Form
    {
        BindingSource bs = new BindingSource();
        public frmLine()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            inputStatas(false);
            btnAdd.Text = "Add";
            btnEdit.Text = "Edit";
            btnAdd.Enabled = true;
            loadGrid();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            this.Location = new Point(
            Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2,
            Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);

            loadGrid();

            

            txtName.DataBindings.Add(new Binding("Text", bs, "line", true));
            txtTag.DataBindings.Add(new Binding("Text", bs, "tag", true));
            txtId.DataBindings.Add(new Binding("Text", bs, "id", true));
        }

        private void loadGrid()
        {
            DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM lines");
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
            dgvLine.DataSource = bs;


        }

        private void dgvUser_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvLine.Select(this.dgvLine.CurrentRowIndex);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                inputStatas(true);
                txtName.Text = "";
                txtTag.Text = "";
                btnAdd.Text = "Save";

                txtName.Focus();


            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Line description is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtName.Focus();
                }
                else if (txtTag.Text == "")
                {
                    MessageBox.Show("Line Tag is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtTag.Focus();
                }
                else
                {
                    addLine();
                    loadGrid();
                    inputStatas(false);
                    btnAdd.Text = "Add";
                }
            }
        }


        private void inputStatas(Boolean status)
        {
            txtName.Enabled = status;
            txtTag.Enabled = status;

            btnEdit.Enabled = !status;
            btnDelete.Enabled = !status;
            btnCancel.Enabled = status;

            dgvLine.Enabled = !status;
        }
        private int addLine()
        {
            return SqlCEHelper.ExecuteNonQuery("INSERT INTO lines (line, tag) VALUES(@line, @tag)",
                new SqlCeParameter("@line", txtName.Text.ToUpper()),
                new SqlCeParameter("@tag", txtTag.Text));
        }

        private int updateLine()
        {
            return SqlCEHelper.ExecuteNonQuery("UPDATE lines SET line=@line,tag=@tag WHERE id=@id",
                new SqlCeParameter("@line", txtName.Text.ToUpper()),
                new SqlCeParameter("@tag", txtTag.Text),
                new SqlCeParameter("@id", txtId.Text.ToString()));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {

                inputStatas(true);
                btnEdit.Text = "Update";
                btnEdit.Enabled = true;
                btnAdd.Enabled = false;
                txtName.Focus();
            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Line description is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtName.Focus();
                }
                else if (txtTag.Text == "")
                {
                    MessageBox.Show("Line Tag is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtTag.Focus();
                }
                else
                {

                    DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM lines where tag ='" + txtTag.Text.Trim().ToUpper() + "' AND id != '" + txtId.Text.ToString() + "'");
                    DataTable dt = ds.Tables["data"];
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Tag description already exist.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
                        txtTag.Focus();
                        txtTag.Text = "";
                    }
                    else
                    {
                        if (updateLine() == 1)
                        {
                            loadGrid();
                            inputStatas(false);
                            btnAdd.Text = "Add";
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Unable to update line", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); ;
                        }
                    }

                    loadGrid();
                    inputStatas(false);
                    btnEdit.Text = "Edit";
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete Line", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                int retVal = SqlCEHelper.ExecuteNonQuery("DELETE FROM lines where id ='" + txtId.Text + "'");
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


    }
}