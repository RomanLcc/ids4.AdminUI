using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using QuickstartIdentityServer.Apis.ApiDTO;
using QuickstartIdentityServer.DBManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuickstartIdentityServer.IdsAuthorization
{
    public class IdsAuthPermissionRequirement:IAuthorizationRequirement
    {
        public string SystemTag { get; set; }
        public IdsAuthPermissionRequirement(string sysTag)
        {
            this.SystemTag = sysTag;
        }
    }
    public class IdsAuthPermissionHandler : AuthorizationHandler<IdsAuthPermissionRequirement> 
    {
        private const string userinfo = "userinfo";
        private readonly PermissionConext db;
        public IdsAuthPermissionHandler(PermissionConext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Handles the requirement async.
        /// </summary>
        /// <returns>The requirement async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="requirement">Requirement.</param>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IdsAuthPermissionRequirement requirement)
        {
            var subid = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            var ep = context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint;//Microsoft.AspNetCore.Http.Endpoint
            //int.TryParse(subid, out int userid);
            ValidatePermissionDTO param = new ValidatePermissionDTO {
                    Account = subid,
                    Code = requirement.SystemTag,
                    Controller = ep.RoutePattern.Defaults["controller"].ToString(),
                    Action = ep.RoutePattern.Defaults["action"].ToString()
            };
            //if (context.User.Claims.Any(c => c.Type == "name" && c.Value == "superadmin")) {
            //    context.Succeed(requirement);
            //    return;
            //} 

            //var isvalidate = await Validate(db,param);
            //if (isvalidate) {
                context.Succeed(requirement);return;
            //}

            HandleBlockedAsync(context, requirement);
        }

        private async Task<bool> Validate(PermissionConext pcontext, ValidatePermissionDTO input)
        {
            bool haspermission = await (from u in pcontext.User.Where(u => u.Account == input.Account)
                                        join urm in pcontext.UserRoleMap on u.Id equals urm.UserId
                                        join rmp in pcontext.RolePermissionMap.Where(m => m.Code == input.Code) on urm.RoleId equals rmp.RoleId
                                        join p in pcontext.Permission.Where(per => per.ControllerName == input.Controller && per.ActionName == input.Action) on rmp.PermissionId equals p.Id
                                        select 1).AnyAsync();
            return haspermission;
        }

        private void HandleBlockedAsync(AuthorizationHandlerContext context, IdsAuthPermissionRequirement requirement)
        {
            var authorizationFilterContext = context.Resource as AuthorizationFilterContext;
            authorizationFilterContext.Result = new StatusCodeResult(403);
            //设置为403会显示不了自定义信息,改为Accepted202,由客户端处理
            context.Succeed(requirement);
        }

    }
}
