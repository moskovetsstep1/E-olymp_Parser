﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using ElimpParse.DatabaseProvider.Models;
using ElimpParse.Model;
using Newtonsoft.Json;

namespace ElimpParse.DatabaseProvider.Repositories
{
    public class UserRepositoriy
    {
        private readonly string _connectionString;
        public UserRepositoriy(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Insert(userResult);
            }
        }

        public UserResult Read(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<UserResult>(username);
            }
        }

        public void Update(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Update(userResult);
            }
        }

        public void Delete(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Delete(userResult);
            }
        }
    }
}