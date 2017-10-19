using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;


namespace WCFService_Lab9.Models
{
    public class DVDContext : DbContext
    {
        public DVDContext() : base("EAPLab9WCFConn") { }

        public DbSet<Models.DVD> DVDs{ get; set; }
    }
}