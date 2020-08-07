using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;
using GTS.Clock.Infrastructure.Repository;

namespace GTS.Clock.Business
{
    public class BActiveDirectory
    {
        DomainsRepository adRep = new DomainsRepository();
        
        public IList<Domains> GetAll() 
        {
            return adRep.GetAllActive();
        }
       
        public Domains GetById(decimal id)
        {
            return adRep.CheckById(id);
        }

        public void AddActiveDirectoryToPerson(decimal personId, string domainName, string username)
        {
            //PersonRepository pr = new PersonRepository(false);
            //ActiveDirectoryAccountRepository ar = new ActiveDirectoryAccountRepository();
            //ActiveDirectoryAccount account = new ActiveDirectoryAccount();
            //account.Active = true;
            //account.Domain = domainName;
            //account.UserName = username;

            //GTS.Clock.Model.Person person =pr.CheckById(personId);

            //if (person.ID > 0) 
            //{
            //    account.Person = person;
            //    ar.Save(account);               
            //}
        }
    }
}
