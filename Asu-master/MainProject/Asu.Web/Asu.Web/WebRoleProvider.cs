using Asu.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;


namespace Asu.Web
{
    public class WebRoleProvider : RoleProvider
    {
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

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new ASU_AVIAEntities12())
            {
                //var result = (from user in context.User
                //                  //join role in context.Role
                //                  //on user.link_role equals role.Id
                //              where user.Login == username
                //              select user.Login).ToArray();
                //return result;

                var result = (from role_map in context.Role_Mapping
                              join user in context.User
                              on role_map.User_Id equals user.Id
                              join role in context.Role
                              on role_map.Role_Id equals role.Id
                              where user.Login == username
                              select role.RoleUser).ToArray();
                return result;

                //var task = (from user in context.User
                //            join tasks in context.Tasks
                //            on user.Id equals tasks.link_user
                //            join nm_task in context.Spr_nm_task
                //            on tasks.link_nm_task equals nm_task.Id
                //            where user.Login == username
                //            select nm_task.Task).ToArray();
                //return task;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}