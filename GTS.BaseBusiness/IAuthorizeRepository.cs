using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Infrastructure;
using GTS.Clock.Model.Security;

namespace GTS.Business
{
    public interface IAuthorizeServices
    {
       Role GetRoleByCode(RoleCustomCodeType roleCode);
       IList<Resource> GetAccessDeniedList(decimal roleId);
       IList<Resource> GetAlowedResourceList(decimal roleId);
       bool IsManager(decimal personId);
       bool IsSubstituteManager(decimal personId);
       bool IsOperator(decimal personId);
    }
}
