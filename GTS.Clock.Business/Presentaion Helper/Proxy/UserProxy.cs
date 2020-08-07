using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Security;
using GTS.Clock.Model;

namespace GTS.Clock.Business.Proxy
{
    /// <summary>
    /// UserName + Person Property ==> for presenter
    /// </summary>
    public class UserProxy 
    {

        public UserProxy()
        {
        }
        
        public UserProxy(User user)
        {
            Active = user.Active;
            ActiveDirectoryAuthenticate = user.ActiveDirectoryAuthenticate;
            ID = user.ID;
            UserName = user.UserName;
            if (user.Role != null)
            {
                RoleName = user.Role.Name;
                RoleID = user.Role.ID;
            }
            if (user.Person != null)
            {
                PersonCode = user.Person.PersonCode;
                PersonName = user.Person.Name;
                PersonID = user.Person.ID;
            }
            Password = "D8B+=obX32GPir0Q_Y432RJqmBdL_32QhA==";
            ConfirmPassword = Password;
            IsPasswodChanged = false;
            if (user.ActiveDirectoryAuthenticate && user.Domain != null) 
            {
                TheDoaminId = user.Domain.ID;
                TheDoaminName = user.Domain.Domain;
            }

        }
       
        public decimal ID { get; set; }

        public string UserName
        {
            get;
            set;
        }

        public string PersonCode
        {
            get;
            set;
        }

        public decimal PersonID { get; set; }

        public decimal RoleID { get; set; }

        public string PersonName
        {
            get;
            set;
        }

        public string RoleName
        {
            get;
            set;
        }

        public decimal DomainId { get; set; }

        public bool ActiveDirectoryAuthenticate { get; set; }

        public bool Active{ get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsPasswodChanged { get; set; }

        public decimal TheDoaminId { get; set; }

        public string TheDoaminName { get; set; }

        public static User Export(UserProxy proxy) 
        {
            User user = new User();
            user.ID = proxy.ID;
            if (proxy.PersonID > 0)
                user.Person = new Person() { ID = proxy.PersonID };
            if (proxy.RoleID > 0)
                user.Role = new Role() { ID = proxy.RoleID };
            if (proxy.DomainId > 0)
                user.Domain = new Domains() { ID = proxy.DomainId };
            user.UserName = proxy.UserName;
            user.Active = proxy.Active;
            user.ActiveDirectoryAuthenticate = proxy.ActiveDirectoryAuthenticate;
            user.IsPasswodChanged = proxy.IsPasswodChanged;
            user.Password = proxy.Password;
            user.ConfirmPassword = proxy.ConfirmPassword;
            return user;
        }
    }
}
