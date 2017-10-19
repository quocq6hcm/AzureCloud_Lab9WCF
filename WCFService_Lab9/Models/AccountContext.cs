using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace WCFService_Lab9.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("EAPLab9WCFConn") { } 

        public DbSet<Models.Account> Accounts { get; set; }
    }
}