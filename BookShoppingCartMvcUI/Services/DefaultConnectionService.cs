﻿using Npgsql;
using BookShoppingCartMvcUI.Services;

namespace BookShoppingCartMvcUI.Services
{
    public class DefaultConnectionService : IConnectionService
    {
        public string GetConnectionString(IConfiguration configuration)
        {
            //The default connection string will come from appSettings
            //var connectionString = configuration.GetSection("pgSettings")["pgConnectionString"];


            //var connectionString = configuration.GetConnectionString("BlogDb");

            var connectionString = configuration.GetConnectionString("DATABASE_URL");

            //It will be automatically overwritten if running on Railway
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // I COMMENTED IT OUT
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);


        }

        private string BuildConnectionString(string databaseUrl)
        {
            //Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI.
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            //Provides a simple way to create and manage the contents of connection strings used by the NpgsqlConnection class.
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

    }
}
