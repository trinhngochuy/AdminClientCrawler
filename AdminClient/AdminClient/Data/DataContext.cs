using AdminClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdminClient.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DemoApp")
        {
                
        }


        public System.Data.Entity.DbSet<AdminClient.Models.Source> Sources { get; set; }

        public System.Data.Entity.DbSet<AdminClient.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<AdminClient.Models.Article> Articles { get; set; }
    }
}