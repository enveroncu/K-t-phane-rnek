using Kütüphane.Controllers;
using Kütüphane.Models;
using Kütüphane.Models.Entities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Kütüphane
{
    public partial class Login : Form
    {
        LibraryModel db;
        public Login()
        {
            db = new LibraryModel();
            AuthorizationGroup administrator = new AuthorizationGroup()
            {
                Name = "Administrator",
            };

            Employee admin = new Employee()
            {
                Name = "",
                LastName = "",
                Authorization = administrator,
                UserName = "admin",
                Password = "admin"
            };

            if (!db.AuthorizationGroups.Any(x => x.Name == "Administrator"))
                db.AuthorizationGroups.Add(administrator);
            if (!db.Employees.Any(x => x.UserName == "admin"))
                db.Employees.Add(admin);

            db.SaveChanges();

            this.CenterToScreen();
            InitializeComponent();


        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Utility.onClose();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Utility.draggable(e, this);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel2_MouseDown(sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            Employee em = db.Employees.Where(x => x.UserName == txtUser.Text).FirstOrDefault();
            if (em != null)
            {
                if (txtPwd.Text.Equals(em.Password))
                {

                    Session session = new Session(em);
                    session.form = this;
                    Views.MainForm form = new Views.MainForm(session);
                    form.Show();
                    this.Hide();


                }
                else
                    MessageBox.Show("Yanlış kullanıcı adı yada parola");
            }
            else
                MessageBox.Show("Yanlış kullanıcı adı yada parola");
            Controllers.Utility.Clear(this);
        }
    }
}
