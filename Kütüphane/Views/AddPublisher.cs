using Kütüphane.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphane.Views
{
    public partial class AddPublisher : Form
    {
        Models.LibraryModel db;
        public AddPublisher()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool contains = false;
            foreach (Publisher item in lstPublishers.Items)
            {
                if (item.Name == txtPublisher.Text)
                {
                    contains = true;
                    break;
                }


            }
            if (!contains)
            {
                Publisher c = new Publisher();
                c.Name = txtPublisher.Text;
                db.Publishers.Add(c);
                if (db.SaveChanges() != 0)
                    lblStatus.Text = "Kayıt başarılı";
                else
                    lblStatus.Text = "Hata oluştu";
                populateList();
                txtPublisher.Clear();
            }
            else
            {
                lblStatus.Text = "Aynı isimli kayıt mevcut";
            }
        }

        private void AddPublisher_Load(object sender, EventArgs e)
        {
            db = new Models.LibraryModel();
            populateList();

        }

        void populateList()
        {
            lstPublishers.Items.Clear();
            lstPublishers.Items.AddRange(db.Publishers.ToArray());
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Controllers.Utility.draggable(e, this);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult opt = MessageBox.Show(((Category)lstPublishers.SelectedItem).Name + " isimli yayınevini silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
            if (opt == DialogResult.Yes)
            {
                db.Publishers.Remove((Publisher)lstPublishers.SelectedItem);
                db.SaveChanges();
                populateList();
            }
        }
    }
}
