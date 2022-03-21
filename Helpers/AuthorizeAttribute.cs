using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using SmartfaceSolution.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Method <c>OnAuthorization</c> perform the user authorization checks 
    /// </summary>
    /// <param name="context">the authenticated users</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // if the user authorization fails the response will be returned with a 401 Unauthorized status 
        if (((User) context.HttpContext.Items["User"]).Equals(null))
            context.Result = new JsonResult(new {message = "Unauthorized"})
                {StatusCode = StatusCodes.Status401Unauthorized};
    }
}