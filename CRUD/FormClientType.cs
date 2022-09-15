using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class FormClientType : Form
    {
        INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();
        int ClientTypeId = 0;
        public FormClientType()
        {
            InitializeComponent();
        }

        private void FormClientType_Load(object sender, EventArgs e)
        {
            var clientTypes = db.ClientTypes.ToList();
            dgvClientType.DataSource = clientTypes;
        }

        private void dgvClientType_Click(object sender, EventArgs e)
        {
            ClientTypeId = 0;
            string Id = string.Empty;

            if (!string.IsNullOrEmpty(dgvClientType.SelectedRows[0].Cells["Id"].Value.ToString()))
            {
                Id = dgvClientType.SelectedRows[0].Cells["Id"].Value.ToString();
                ClientTypeId = int.Parse(Id);
                GetClientTypeById(ClientTypeId);
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void GetClientTypeById(int clientTypeId)
        {
            var clientType = db.ClientTypes.FirstOrDefault(x => x.Id == clientTypeId);
            if (clientType != null)
            {
                txtName.Text = clientType.Name;
                txtDescription.Text = clientType.Description;
                chkEnable.Checked = clientType.Enabled;
                pnlFormClientType.Enabled = true;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            Update(ClientTypeId);
            pnlFormClientType.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void Update(int clientTypeId)
        {
            var clientType = db.ClientTypes.FirstOrDefault(x => x.Id == clientTypeId);
            if (clientType != null)
            {
                clientType.Name = txtName.Text.ToString();
                clientType.Description = txtDescription.Text.ToString();
                clientType.Enabled = chkEnable.Checked;
                db.ClientTypes.AddOrUpdate(clientType);
                db.SaveChanges();
                pnlFormClientType.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea borrar?", "Borrar ahora", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string Id = string.Empty;
                Id = dgvClientType.SelectedRows[0].Cells["Id"].Value.ToString();
                ClientTypeId = int.Parse(Id);
                var clientType = db.ClientTypes.FirstOrDefault(x => x.Id == ClientTypeId);
                if (clientType != null)
                {
                    db.ClientTypes.Remove(clientType);
                    db.SaveChanges();
                    MessageBox.Show("Lo borrate durisimo");
                }
            }
            else
            {
                MessageBox.Show("Esta bien cambianr de opinion :)", "Borrar ahora", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //string Id = string.Empty;
            //Id = dgvClientType.SelectedRows[0].Cells["Id"].Value.ToString();
            //ClientTypeId = int.Parse(Id);
            //var clientType = db.ClientTypes.FirstOrDefault(x => x.Id == ClientTypeId);
            //if (clientType != null)
            //{
            //    db.ClientTypes.Remove(clientType);
            //    db.SaveChanges();
            //    MessageBox.Show("Lo borrate durisimo");
            //}
        }
    }
}
