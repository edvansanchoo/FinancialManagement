using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Models
{
    public class Constants
    {
        public string? KEYTOKEN { get; set; }
        public string? DATABASE_HOST { get; set; }
        public string? DATABASE_USER { get; set; }
        public string? DATABASE_PASSWORD { get; set; }
        public string? DATABASE_NAME { get; set; }
        public string? DATABASE_PORT { get; set; }


        public Constants()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            #if DEBUG
                KEYTOKEN = configuration.GetValue<string>("KEYTOKEN");
                DATABASE_HOST = configuration.GetValue<string>("DATABASE_HOST");
                DATABASE_USER = configuration.GetValue<string>("DATABASE_USER");
                DATABASE_PASSWORD = configuration.GetValue<string>("DATABASE_PASSWORD");
                DATABASE_NAME = configuration.GetValue<string>("DATABASE_NAME");
                DATABASE_PORT = configuration.GetValue<string>("DATABASE_PORT");
            #else
                KEYTOKEN = Environment.GetEnvironmentVariable("KEYTOKEN");
                DATABASE_HOST = Environment.GetEnvironmentVariable("DATABASE_HOST");
                DATABASE_USER = Environment.GetEnvironmentVariable("DATABASE_USER");
                DATABASE_PASSWORD = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
                DATABASE_NAME = Environment.GetEnvironmentVariable("DATABASE_NAME");
                DATABASE_PORT = Environment.GetEnvironmentVariable("DATABASE_PORT");
            #endif
        }

        public string GetConnectionString(string DatabaseName)
        {
            return $"Data Source={DATABASE_HOST},{DATABASE_PORT};Initial Catalog={DatabaseName}; User ID={DATABASE_USER}; Password={DATABASE_PASSWORD}; MultipleActiveResultSets=True;TrustServerCertificate=true";
        }
    }
}