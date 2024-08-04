using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFormApp
{
    internal class db
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=useracc"); // useracc - БД, которая созданна в MAMP -> работа с MySQL 

        public void openCon()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        } // подключение к БД
        public void closeCon()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        } // отключение от БД
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
