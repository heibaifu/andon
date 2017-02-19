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
    public partial class frmUser : Form
    {
        BindingSource bs = new BindingSource();
        public frmUser()
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

            DataTable dtLevel = new DataTable();
            dtLevel.Columns.Add("id");
            dtLevel.Columns.Add("desc");

            DataRow dr = dtLevel.NewRow();
            //dr["id"] = "1"; // or dr[0]="Mohammad";
            //dr["desc"] = "ADMIN"; // or dr[1]=24;
            //dtLevel.Rows.Add(dr);

            // Create another DataRow, add Name and Age data, and add to the DataTable
            dr = dtLevel.NewRow();
            dr["id"] = "2";
            dr["desc"] = "MANAGER";
            dtLevel.Rows.Add(dr);

            dr = dtLevel.NewRow();
            dr["id"] = "3";
            dr["desc"] = "OPERATOR";
            dtLevel.Rows.Add(dr);

            cmbLevel.DisplayMember = "desc";
            cmbLevel.ValueMember = "id";
            cmbLevel.DataSource = dtLevel;

            cmbLevel.SelectedIndex = 0;

            txtName.DataBindings.Add(new Binding("Text", bs, "name", true));
            txtPassword.DataBindings.Add(new Binding("Text", bs, "password", true));
            cmbLevel.DataBindings.Add(new Binding("SelectedValue", bs, "level", true));
            chkActive.DataBindings.Add(new Binding("Checked", bs, "active", true));
            txtId.DataBindings.Add(new Binding("Text", bs, "id", true));
        }

        private void loadGrid()
        {
            DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM users where level > 1");
            DataTable dtable = ds.Tables["data"];
            dtable.Columns.Add("status", typeof(String));
            dtable.Columns.Add("level_desc", typeof(String));
            foreach (DataRow dr in dtable.Rows)
            {
                if (dr["active"].ToString() == "1")
                {
                    dr["status"] = "ACTIVE";
                }
                else
                {
                    dr["status"] = "IN-ACTIVE";
                }

                if (dr["level"].ToString() == "2")
                {
                    dr["level_desc"] = "MANAGER";
                }
                if (dr["level"].ToString() == "3")
                {
                    dr["level_desc"] = "OPERATOR";
                }
            }
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
            dgvUser.DataSource = bs;


        }

        private void dgvUser_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvUser.Select(this.dgvUser.CurrentRowIndex);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                inputStatas(true);
                txtName.Text = "";
                txtPassword.Text = "";
                btnAdd.Text = "Save";

                txtName.Focus();


            }
            else
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Username is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtName.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Password is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtPassword.Focus();
                }
                else
                {
                    addUser();
                    loadGrid();
                    inputStatas(false);
                    btnAdd.Text = "Add";
                }
            }
        }


        private void inputStatas(Boolean status)
        {
            txtName.Enabled = status;
            txtPassword.Enabled = status;
            cmbLevel.Enabled = status;
            chkActive.Enabled = status;

            btnEdit.Enabled = !status;
            btnDelete.Enabled = !status;
            btnCancel.Enabled = status;

            dgvUser.Enabled = !status;
        }
        private int addUser()
        {
            int active;
            if (chkActive.Checked)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            string level = cmbLevel.SelectedValue.ToString();
            return SqlCEHelper.ExecuteNonQuery("INSERT INTO users (name, password, active, level) VALUES(@name, @password, @active, @level)",
                new SqlCeParameter("@name", txtName.Text.ToUpper()),
                new SqlCeParameter("@password", txtPassword.Text),
                new SqlCeParameter("@active", active),
                new SqlCeParameter("@level", level));
        }

        private int updateUser()
        {
            int active;
            if (chkActive.Checked)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            string level = cmbLevel.SelectedValue.ToString();
            return SqlCEHelper.ExecuteNonQuery("UPDATE users SET name=@name,password=@password,active=@active,level=@level WHERE id=@id",
                new SqlCeParameter("@name", txtName.Text.ToUpper()),
                new SqlCeParameter("@password", txtPassword.Text),
                new SqlCeParameter("@active", active),
                new SqlCeParameter("@level", level),
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
                    MessageBox.Show("Username is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtName.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Password is required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    txtPassword.Focus();
                }
                else
                {

                    DataSet ds = SqlCEHelper.ExecuteDataTable("SELECT * FROM users where password ='" + txtPassword.Text.Trim().ToUpper() + "' AND id != '" + txtId.Text.ToString() + "'");
                    DataTable dt = ds.Tables["data"];
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Password already exist.", "Error", MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
                        txtPassword.Focus();
                        txtPassword.Text = "";
                    }
                    else
                    {
                        if (updateUser() == 1)
                        {
                            loadGrid();
                            inputStatas(false);
                            btnAdd.Text = "Add";
                            btnAdd.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Unable to update user", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); ;
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete User", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                int retVal = SqlCEHelper.ExecuteNonQuery("DELETE FROM users where id ='" + txtId.Text + "'");
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