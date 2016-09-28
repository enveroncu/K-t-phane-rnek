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
    public partial class AddBookForm : Form
    {
        Models.LibraryModel db;
        public AddBookForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            Book book = new Book();
            book.Name = txtName.Text;
            book.Authors.Add((Author)cmbAuthor1.SelectedItem);

            book.Category = ((Category)cmbCategory.SelectedItem);
            book.Publisher = (Publisher)cmbPublisher.SelectedItem;
            book.PublishYear = int.Parse(txtYear.Text);
            book.PageCount = (int)numPage.Value;
            book.Stock = (int)numStock.Value;
            book.Description = txtDescr.Text;

            db.Books.Add(book);
            if (db.SaveChanges() != 0)
            {
                MessageBox.Show("Kayıt başarılı");
                Controllers.Utility.Clear(this);
            }
            else
                MessageBox.Show("Hata oluştu");

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Controllers.Utility.Clear(this);
        }

        private void AddBookForm_Load(object sender, EventArgs e)
        {
            db = new Models.LibraryModel();
            cmbAuthor1.DataSource = db.Authors.ToList();
            cmbCategory.DataSource = db.Categories.ToList();
            cmbPublisher.DataSource = db.Publishers.ToList();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            AddAuthorForm authorform = new AddAuthorForm();
            authorform.ShowDialog();
            cmbAuthor1.DataSource = db.Authors.ToList();
        }

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            AddCategory form = new AddCategory();
            form.ShowDialog();
            cmbCategory.DataSource = db.Categories.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPublisher form = new AddPublisher();
            form.ShowDialog();
            cmbPublisher.DataSource = db.Publishers.ToList();

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Controllers.Utility.draggable(e, this);
        }
    }
}
