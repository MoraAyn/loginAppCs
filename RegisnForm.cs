using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginFormApp
{
    public partial class RegisnForm : Form
    {
        public RegisnForm()
        {
            InitializeComponent();
            nick.Text = "ник...";
            nick.ForeColor = Color.Gray;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        Point lastPoint;
        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void nick_Enter(object sender, EventArgs e)
        {
            if(nick.Text == "ник...")
                nick.Text = "";
                nick.ForeColor = Color.Black;
        }

        private void nick_Leave(object sender, EventArgs e)
        {
            if(nick.Text == "")
            {
                nick.Text = "ник...";
                nick.ForeColor = Color.Gray;
            }
        }

        private void btnRegistr_Click(object sender, EventArgs e)
        {
            if (login.Text == "" || password.Text == "" || nick.Text == "ник...")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (checkUser()) return;

            db bd = new db();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`id`, `login`, `password`, `nickname`) VALUES (NULL, @login, @pass, @nickname)", bd.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = login.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password.Text;
            command.Parameters.Add("@nickname", MySqlDbType.VarChar).Value = nick.Text;
            // мы сделали Sql запрос, который в БД добавляет user'a с полями login, password, nickname
            bd.openCon();

            if(command.ExecuteNonQuery() == 1) // если все ок, то ->
            {
                MessageBox.Show("Аккаунт создан");
            } else
            {
                MessageBox.Show("Аккаунт не создан");
            }

            bd.closeCon();
        }

        public bool checkUser()
        {
            db bd = new db();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", bd.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login.Text; 
            adapter.SelectCommand = command; 
            adapter.Fill(table); // проверяем есть ли такой логин уже или нет

            if (table.Rows.Count > 0) 
            {
                MessageBox.Show("Такой логин уже есть");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void autoLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogFormApp lfa = new LogFormApp();
            lfa.Show();
        }
    }
}
