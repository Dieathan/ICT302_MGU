﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;

namespace LoginServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string m_userid, m_password;
            bool m_passCheck = false;
            bool m_userCheck = false;
            int m_count = 0;
            List<String> m_userIDList = new List<String>();

            SQLiteConnection con = new SQLiteConnection("data source=..\\..\\..\\..\\KinesisArcade.sqlite");
            con.Open();

            SQLiteCommand m_createLoginInstance = con.CreateCommand();
            SQLiteCommand m_checkPassword = con.CreateCommand();
            SQLiteCommand m_checkUser = con.CreateCommand();
            SQLiteCommand m_getCount = con.CreateCommand();

            m_getCount.CommandText = "SELECT COUNT(*) FROM USER;";
            m_getCount.Connection = con;
            SQLiteDataReader readerC = m_getCount.ExecuteReader();
            readerC.Read();
            m_count = readerC.GetInt32(0);

            m_checkUser.CommandText = "SELECT UserID FROM [USER]";
            m_checkUser.Connection = con;
            SQLiteDataReader readerU = m_checkUser.ExecuteReader();

            for (int i = 0; i < m_count; i++)
            {
                readerU.Read();
                string user = readerU.GetString(0);
                m_userIDList.Add(user);
            }

            Console.WriteLine("Please enter your login details\n");

            Console.WriteLine("User ID: ");
            m_userid = Console.ReadLine();

            while (!m_userCheck)
            {
                for (int i = 0; i < m_count; i++)
                {
                    if (String.Compare(m_userIDList[i], m_userid) == 0)
                    {
                        m_userCheck = true;
                    }
                }

                if (!m_userCheck)
                {
                    Console.WriteLine("UserID does not exist in the database\n");
                    Console.WriteLine("User ID: ");
                    m_userid = Console.ReadLine();
                }
            };


            Console.WriteLine("Password: ");
            m_password = Console.ReadLine();
            m_checkPassword.CommandText = "SELECT UserID, Password, IsSupervisor FROM [USER] WHERE UserID = \"" + m_userid + "\"";
            m_checkPassword.Connection = con;
            SQLiteDataReader readerP = m_checkPassword.ExecuteReader();
            readerP.Read();
            string password = readerP.GetString(1);

            while (!m_passCheck)
            {
                if (String.Compare(password, m_password) == 0)
                {
                    m_passCheck = true;
                }
                else
                {
                    Console.WriteLine("Incorrect Passsword, Please try again\n");
                    Console.WriteLine("Password: ");
                    m_password = Console.ReadLine();
                }

                string time = DateTime.Now.ToString("d/MM/yyyy HH:mm");
                m_createLoginInstance.CommandText = "Insert into LOGININSTANCE(UserIDEntered,PasswordCorrect,LoginTime)Values('" + m_userid + "','" + Convert.ToInt32(m_passCheck) + "','" + time + "')";
                m_createLoginInstance.Connection = con;
                m_createLoginInstance.ExecuteNonQuery();
            };

            if (readerP.GetBoolean(2))
            {
                ProcessStartInfo wInfo = new ProcessStartInfo("..\\..\\index.html");
                Process.Start(wInfo);
            }
            else
            {
                ProcessStartInfo gInfo = new ProcessStartInfo("..\\..\\iamtext.txt");
                Process.Start(gInfo);
            }

        }

    }
}
