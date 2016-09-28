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
    public partial class AddAuthorForm : Form
    {
        Models.LibraryModel db;
        public AddAuthorForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Author a = new Author();
            a.Name = txtName.Text;
            a.LastName = txtLastname.Text;
            db.Authors.Add(a);
            if (db.SaveChanges() != 0)
                lblStatus.Text = "Kayıt başarılı";
            else
                lblStatus.Text = "Hata oluştu";
            populateList();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Controllers.Utility.Clear(this);
        }

        private void AddAuthorForm_Load(object sender, EventArgs e)
        {
            db = new Models.LibraryModel();
            populateList();
        }

        void populateList()
        {
            lstAuthors.Items.Clear();
            List<Author> authors = db.Authors.ToList();
            foreach (Author author in authors)
            {
                ListViewItem lstItem = new ListViewItem(new string[] { author.ToString(), author.Books.Count.ToString() });
                lstAuthors.Items.Add(lstItem);
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Controllers.Utility.draggable(e, this);
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult opt = MessageBox.Show(((Author)lstAuthors.SelectedItems[0].Tag).Name + " isimli türü silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
            if (opt == DialogResult.Yes)
            {
                db.Authors.Remove((Author)lstAuthors.SelectedItems[0].Tag);
                db.SaveChanges();
                populateList();
            }
        }
    }
}
