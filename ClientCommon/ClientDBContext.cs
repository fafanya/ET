using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ClientCommon
{
    public class ClientDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string DatabasePath { get; set; }

        public ClientDBContext()
        {

        }

        public ClientDBContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}