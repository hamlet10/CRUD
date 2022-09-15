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
    public partial class FormContactType : Form
    {
        INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();
        int ContactTypeId = 0;
        public FormContactType()
        {
            InitializeComponent();
        }

        private void FormContactType_Load(object sender, EventArgs e)
        {
            var contactTypes = db.ContactTypes.ToList();
            dgvContactType.DataSource = contactTypes;
        }

        private void dgvContactType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                ContactTypeId = 0;
                string Id = string.Empty;

                if (!string.IsNullOrEmpty(dgvContactType.SelectedRows[0].Cells["Id"].Value.ToString()))
                {
                    Id = dgvContactType.SelectedRows[0].Cells["Id"].Value.ToString();
                    ContactTypeId = int.Parse(Id);
                    GetContactTypeById(ContactTypeId);
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        private void GetContactTypeById(int contactTypeId)
        {
            var contactType = db.ContactTypes.FirstOrDefault(x => x.Id == contactTypeId);
            if (contactType != null)
            {
                txtName.Text = contactType.Name;
                txtDescription.Text = contactType.Description;
                chkEnable.Checked = contactType.Enabled;
                pnlFormContactType.Enabled = true;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void pnlFormContactType_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvClientType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                ContactTypeId = 0;
                string Id = string.Empty;

                if (!string.IsNullOrEmpty(dgvContactType.SelectedRows[0].Cells["Id"].Value.ToString()))
                {
                    Id = dgvContactType.SelectedRows[0].Cells["Id"].Value.ToString();
                    ContactTypeId = int.Parse(Id);
                    GetContactTypeById(ContactTypeId);
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            Update(ContactTypeId);
            pnlFormContactType.Enabled = true;
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
        private void Update(int contactTypeId)
        {
            var contactType = db.ContactTypes.FirstOrDefault(x => x.Id == contactTypeId);
            if (contactType != null)
            {
                contactType.Name = txtName.Text.ToString();
                contactType.Description = txtDescription.Text.ToString();
                contactType.Enabled = chkEnable.Checked;
                db.ContactTypes.AddOrUpdate(contactType);
                db.SaveChanges();
                pnlFormContactType.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta seguro que desea borrar?","Borrar ahora", MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                string Id = string.Empty;
                Id = dgvContactType.SelectedRows[0].Cells["Id"].Value.ToString();
                ContactTypeId = int.Parse(Id);
                var ContactType = db.ContactTypes.FirstOrDefault(x => x.Id == ContactTypeId);
                if (ContactType != null)
                {
                    db.ContactTypes.Remove(ContactType);
                    db.SaveChanges();
                    MessageBox.Show("Lo borrate durisimo");
                }
            }
            else
            {
                MessageBox.Show(",", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

        }
    }
}
