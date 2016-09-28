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
    public partial class AddCategory : Form
    {
        Models.LibraryModel db;
        public AddCategory()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool contains = false;
            foreach(Category item in lstCategories.Items)
            {
                if (item.Name==txtCategoryName.Text)
                {
                    contains = true;
                    break;
                }
                    
                    
            }
            if (!contains)
            {
                Category c = new Category();
                c.Name = txtCategoryName.Text;
                db.Categories.Add(c);
                if (db.SaveChanges() != 0)
                    lblStatus.Text = "Kayıt başarılı";
                else
                    lblStatus.Text = "Hata oluştu"; ;
                populateList();
                txtCategoryName.Clear();
            }
            else
            {
                lblStatus.Text = "Aynı isimli kayıt mevcut";
            }

        }
        
        void populateList()
        {
            lstCategories.Items.Clear();
            lstCategories.Items.AddRange(db.Categories.ToArray());
        }

        private void AddCategory_Load(object sender, EventArgs e)
        {
            db = new Models.LibraryModel();
            populateList();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Controllers.Utility.draggable(e, this);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult opt = MessageBox.Show(((Category)lstCategories.SelectedItem).Name + " isimli türü silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
            if(opt==DialogResult.Yes)
            {
                db.Categories.Remove((Category)lstCategories.SelectedItem);
                db.SaveChanges();
                populateList();
            }

        }
    }
}
