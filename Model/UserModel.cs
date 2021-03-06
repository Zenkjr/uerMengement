﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    class UserModel
    {
         private static ObservableCollection<Entity.User> listUser;
        public static ObservableCollection<Entity.User> GetUsers(List<string> f)
        {
            DataAccess.InitializeDatabase();

                if (listUser == null)
                {
                    listUser = new ObservableCollection<Entity.User>();

                }
                using (SqliteConnection db = new SqliteConnection("Filename=users_manager.db"))
                {
                    db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                {
                    Connection = db
                };
                if (f.Count == 0)
                {
                    selectCommand.CommandText = "SELECT * FROM users";
                }
                else
                {
                    selectCommand.CommandText = "SELECT * FROM users WHERE " + f[1].ToLower() + " LIKE '" + f[0] + "'";
                }
                SqliteDataReader sqliteData = selectCommand.ExecuteReader();
                    Entity.User user;
                    while (sqliteData.Read())
                    {
                        user = new Entity.User
                        {
                            Id = Convert.ToInt16(sqliteData["id"]),
                            Name = Convert.ToString(sqliteData["name"]),
                            Email = Convert.ToString(sqliteData["email"]),
                            Phone = Convert.ToString(sqliteData["phone"]),
                            Address = Convert.ToString(sqliteData["address"]),
                            Avatar = Convert.ToString(sqliteData["avatar"]),
                        };
                        listUser.Add(user);
                    }
                    db.Close();
                }
                if (listUser == null)
                {
                    listUser = new ObservableCollection<Entity.User>();
                }
                return listUser;
            }

            public static void SetUsers(ObservableCollection<Entity.User> users)
            {
                listUser = users;
            }

            public static void AddUser(Entity.User user)
            {
                DataAccess.InitializeDatabase();
                using (SqliteConnection db = new SqliteConnection("Filename=users_manager.db"))
                {
                    db.Open();

                SqliteCommand insertCommand = new SqliteCommand
                {
                    Connection = db,

                    // Use parameterized query to prevent SQL injection attacks
                    CommandText = "INSERT INTO users (name, email, phone, address, avatar) VALUES (@name, @email, @phone, @address, @avatar);"
                };
                insertCommand.Parameters.AddWithValue("@name", user.Name);
                    insertCommand.Parameters.AddWithValue("@email", user.Email);
                    insertCommand.Parameters.AddWithValue("@phone", user.Phone);
                    insertCommand.Parameters.AddWithValue("@address", user.Address);
                    insertCommand.Parameters.AddWithValue("@avatar", user.Avatar);

                    insertCommand.ExecuteReader();

                    db.Close();
                }
                if (listUser == null)
                {
                    listUser = new ObservableCollection<Entity.User>();
            }
            else
            {
                Console.WriteLine(listUser);
            }
                Console.WriteLine(listUser);

            listUser.Add(user);
            }
    }
}
