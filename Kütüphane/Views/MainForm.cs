using System;
using System.Linq;
using System.Windows.Forms;
using Kütüphane.Controllers;
using Kütüphane.Models;
using Kütüphane.Models.Entities;
using System.Collections.Generic;

namespace Kütüphane.Views
{
    public partial class MainForm : Form
    {
        Session session;
        LibraryModel db;
        public MainForm(Session s)
        {
            this.CenterToScreen();
            InitializeComponent();
            session = s;
            lblUser.Text = "@"+s.Employee.UserName;
            db = new LibraryModel();
            
        }
        private void lblClose_Click(object sender, EventArgs e)
        {
            Utility.onClose();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Utility.draggable(e, this);
        }

        private void lblHeader_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            tabMain.SelectTab(1);
            populateBookList();


        }

        List<Book> booksList;
        void populateBookList()
        {
            lstBooks.Items.Clear();
            booksList = db.Books.ToList();
            if(booksList.Count>0)
            {
                foreach (Book item in booksList)
                {

                    ListViewItem i = new ListViewItem(new string[] { item.Name, item.GetAuthors(), item.Category.Name, item.PageCount.ToString(), item.Publisher.Name, item.Stock.ToString() });
                    i.Tag = item;
                    lstBooks.Items.Add(i);
                }
            }
            

        }

        private void btnBookRent_Click(object sender, EventArgs e)
        {
            tabMain.SelectTab(0);
            setRentBook();
            populateRentedList();

        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            tabMain.SelectTab(2);
            populateEmployeeList();
            cmbEmployeePosition.DataSource = db.AuthorizationGroups.ToList();
        }

        private void populateEmployeeList()
        {
            lstEmployees.Items.Clear();
            List<Employee> EmployeeList = db.Employees.ToList();
            if(EmployeeList.Count>0)
            {
                foreach(Employee item in EmployeeList)
                {
                    ListViewItem i = new ListViewItem(new string[] { item.ToString(), item.UserName, item.Authorization.Name });
                    i.Tag = item;
                    lstEmployees.Items.Add(i);
                }
            }

        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            tabMain.SelectTab(3);
            populateClientList();
        }

        private void populateClientList()
        {
            lstClients.Items.Clear();
            List<Client> ClientList = db.Clients.ToList();
            if(ClientList.Count>0)
            {
                foreach(Client item in ClientList)
                {
                    ListViewItem i = new ListViewItem(new string[] { item.ToString() });
                    i.Tag = item;
                    lstClients.Items.Add(i);
                }
            }

        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm();
            form.ShowDialog();
            populateBookList();
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            AddAuthorForm form = new AddAuthorForm();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCategory form = new AddCategory();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPublisher form = new AddPublisher();
            form.ShowDialog();
        }

        private void btnEClear_Click(object sender, EventArgs e)
        {
            Utility.Clear(this);
        }

        private void btnEmployeeSave_Click(object sender, EventArgs em)
        {
            Employee e = new Employee();
            e.Name = txtEmployeeName.Text;
            e.LastName = txtEmployeeLastname.Text;
            e.UserName = (e.Name + "." + e.LastName).ToLower();
            e.Password = txtPass.Text;
            e.Authorization = (AuthorizationGroup)cmbEmployeePosition.SelectedItem;

            db.Employees.Add(e);
            if (db.SaveChanges() != 0)
                MessageBox.Show("Kayıt Başarılı");
            else
                MessageBox.Show("Hata oluştu");
            populateEmployeeList();
        }

        private void btnClientAdd_Click(object sender, EventArgs e)
        {
            Client c = new Client();
            c.Name = txtClientName.Text;
            c.LastName = txtClientLastname.Text;
            db.Clients.Add(c);
            if (db.SaveChanges() != 0)
                MessageBox.Show("Kayıt Başarılı");
            else
                MessageBox.Show("Hata oluştu");
            populateClientList();

        }

        private void btnClientClear_Click(object sender, EventArgs e)
        {
            Utility.Clear(this);
        }

        private void btnEmployeeDel_Click(object sender, EventArgs e)
        {

            if(lstEmployees.SelectedItems[0]!= null)
            {
                DialogResult opt = MessageBox.Show(((Employee)lstEmployees.SelectedItems[0].Tag).Name + " isimli çalışanı silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
                if (opt == DialogResult.Yes)
                {
                    Employee i = (Employee)lstEmployees.SelectedItems[0].Tag;
                    db.Employees.Remove(i);
                    db.SaveChanges();
                    populateEmployeeList();
                }
                else
                    MessageBox.Show("Listeden seçim yapınız");
            }
        }

        private void btnClientDel_Click(object sender, EventArgs e)
        {
            if (lstClients.SelectedItems[0] != null)
            {
                DialogResult opt = MessageBox.Show(((Client)lstClients.SelectedItems[0].Tag).Name + " isimli müşteriyi silmek istediğinize emin misiniz?", "Sil", MessageBoxButtons.YesNo);
                if (opt == DialogResult.Yes)
                {
                    Client i = (Client)lstClients.SelectedItems[0].Tag;
                    db.Clients.Remove(i);
                    db.SaveChanges();
                    populateClientList();
                }
                else
                    MessageBox.Show("Listeden seçim yapınız");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(session.Employee.Authorization.Name =="Çalışan")
            {
                btnEmployees.Enabled = false;
            }

            setRentBook();
            populateRentedList();
        }

        void setRentBook()
        {
            cmbBooks.DataSource = db.Books.ToList();
            cmbClients.DataSource = db.Clients.ToList();
            cmbEmployees.DataSource = db.Employees.ToList();
            cmbEmployees.SelectedIndex = cmbEmployees.FindStringExact(session.Employee.ToString());


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            session.form.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabMain.SelectTab(3);
            populateClientList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Utility.Clear(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RentedBook book = new RentedBook();
            if(((Book)cmbBooks.SelectedItem).Stock>0)
            {
                book.Book = (Book)cmbBooks.SelectedItem;
                book.Book.Stock -= 1;
                book.Client = (Client)cmbClients.SelectedItem;
                book.DueTo = dateTimePicker1.Value;
                book.RentDate = DateTime.Now;
                book.Employee = (Employee)cmbEmployees.SelectedItem;
                book.Fee = Convert.ToDecimal(txtFee.Text);

                db.RentedBooks.Add(book);
                if (db.SaveChanges() != 0)
                {

                }
                else
                    MessageBox.Show("Hatalı Giriş");

                populateRentedList();
            }

        }

        void populateRentedList()
        {
            
            lstRentedBooks.Items.Clear();
            List<RentedBook> RentedList = db.RentedBooks.ToList();
            if (RentedList.Count > 0)
            {
                foreach (RentedBook item in RentedList)
                {
                    decimal fee;
                    if (DateTime.Now > item.DueTo)
                        fee = (DateTime.Now - item.RentDate).Days * (decimal)1.5 * item.Fee;
                    else
                        fee = (item.DueTo-item.RentDate).Days * item.Fee;
                    ListViewItem i = new ListViewItem(new string[] { item.Book.Name,item.RentDate.ToString(),item.DueTo.ToString(), fee.ToString(),item.Client.ToString(),item.Employee.ToString()});
                    i.Tag = item;
                    lstRentedBooks.Items.Add(i);
                }
            }

        }

        private void cmbBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Book)cmbBooks.SelectedItem).Stock < 0)
                MessageBox.Show("Seçilen kitap stokta yok");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            lstBooks.Items.Clear();
            List<Book> books = booksList.Where(x => x.Name.Contains(textBox1.Text)|| x.GetAuthors().Contains(textBox1.Text) || x.Category.Name.Contains(textBox1.Text) || x.Publisher.Name.Contains(textBox1.Text)).ToList();
            foreach(Book b in books)
            {
                ListViewItem i = new ListViewItem(new string[] { b.Name, b.GetAuthors(), b.Category.Name, b.PageCount.ToString(), b.Publisher.Name, b.Stock.ToString() });
                
                lstBooks.Items.Add(i);
                
            }
            if (textBox1.Text == "")
                populateBookList();
            
        }
    }
}
