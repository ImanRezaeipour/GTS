using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Model.Security;
using System.Configuration;

namespace GTS.Clock.Infrastructure.Repository
{
    public class GTSRoleProvider : RoleProvider
    {
        private RoleRepository roleRepository = new RoleRepository();
        private UserRepository userRepository = new UserRepository();

        public GTSRoleProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Override Properties

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion


        #region Override Methods

        #region Create Update Delete

        public override void CreateRole(string roleName)
        {
            Role role = new Role();
            role.Name = roleName;
            role.ParentId = 0;
            role.Active = true;
            roleRepository.Save(role);
        }
        public void CreateRole(string roleName, decimal parentId)
        {
            Role role = new Role();
            role.Name = roleName;
            role.ParentId = parentId;
            role.Active = true;
            roleRepository.Save(role);
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                Role role = GetRole(roleName);
                if (role.ID > 0)
                {
                    foreach (string userName in usernames)
                    {
                        User user = userRepository.GetByUserName(userName);
                        if (user.ID > 0)
                        {
                            user.Role = role;
                            role.UserList.Add(user);
                        }
                    }
                    roleRepository.Save(role);
                }
            }
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                Role role = GetRole(roleName);
                if (role.ID > 0)
                {
                    foreach (string userName in usernames)
                    {
                        User user = userRepository.GetByUserName(userName);
                        if (user.ID > 0)
                        {
                            user.Role = null;
                            role.UserList.Remove(user);
                        }
                    }
                    roleRepository.Save(role);
                }
            }
        }

        #endregion

        #region SearchRole
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {

            Role role = roleRepository.GetByRoleName(roleName);
            if (role.ID > 0)
            {
                return true;
            }
            return false;
        }
        public override string[] GetAllRoles()
        {
            return roleRepository.GetAll().Select(x => x.Name).ToArray();
        }
        /// <summary>
        /// نقش یک کاربر را برمیگرداند
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            User user = userRepository.GetByUserName(username);
            if (user != null && user.ID > 0 && user.Role != null)
            {
                return new string[] { user.Role.Name };
            }
            return null;
        }
        public override string[] GetUsersInRole(string roleName)
        {
            Role role = GetRole(roleName);
            if (role.ID > 0)
            {
                string[] names = new string[role.UserList.Count];
                for (int i = 0; i < role.UserList.Count; i++)
                {
                    names[i] = role.UserList[i].UserName;
                }
                return names;
            }
            return null;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            User user = userRepository.GetByUserName(username);
            if (user.ID > 0)
            {
                if (user.Role.Name.ToLower().Equals(roleName.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public Role GetRole(string roleName)
        {
            return roleRepository.GetByRoleName(roleName);
        }
        #endregion

        #endregion
    }
}
