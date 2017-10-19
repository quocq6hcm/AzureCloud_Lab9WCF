using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFService_Lab9.Models;

namespace WCFService_Lab9
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        Models.DVDContext _dbContext = new DVDContext();
        Models.AccountContext _dbAccount = new AccountContext();


        public List<Account> GetAccounts() => _dbAccount.Accounts.ToList();
        

        public List<DVD> GetByRange(string from, string to)
            => _dbContext.DVDs
            .Where(d => d.YearOfRelease.ToString().CompareTo(from) >= 0 && d.YearOfRelease.ToString().CompareTo(to) <= 0).ToList();
        

        public List<DVD> GetDVDs() => _dbContext.DVDs.ToList();

        public void PostAccount(Account newAccount)
        {
            _dbAccount.Accounts.Add(newAccount);
            _dbAccount.SaveChanges();
            return;
        }

        public void PostDVD(DVD newDVD)
        {
            _dbContext.DVDs.Add(newDVD);
            _dbContext.SaveChanges();
            return;
        }
    }
}
