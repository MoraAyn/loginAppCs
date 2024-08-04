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
    public partial class LogFormApp : Form
    {
        public LogFormApp()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.ForeColor = Color.Violet;
        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.ForeColor = Color.White;
        }

        Point px;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left) 
            {
                this.Left += e.X - px.X;
                this.Top += e.Y - px.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            px = new Point(e.X, e.Y);
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            string loginUser = login.Text;
            string passwordUser = password.Text;

            db bd = new db();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", bd.getConnection()); // мы из Бд берем таки поля как логин и пароль, и если то что мы ввели совпадает,
             // то делаем что то, пока заглушки
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser; // в заглушку вставил нужную переменную  
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passwordUser; // аналогично

            adapter.SelectCommand = command; // это просто команда, которая спрашивает с БД данные 
            adapter.Fill(table); // все полученные данные мы помещаем в объект table(тупо таблица со свойствами)

            if(table.Rows.Count > 0) // если  есть ряды с полученными записями, то user есть в БД
            {
                this.Hide();
                MainForm mf = new MainForm();
                mf.Show();
            } else
            {
                MessageBox.Show("Не получилось!");
            }
                                                                                                                                 
        }

        private void resLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisnForm rf = new RegisnForm();
            rf.Show();
        }
    }
}
