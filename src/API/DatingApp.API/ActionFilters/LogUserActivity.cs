﻿using DatingApp.Framework.Data.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
//using System.Data.Entity;
using DatingApp.Common.Extensions;

namespace DatingApp.API.ActionFilters
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public LogUserActivity(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    // This means the Task is completed and it will return ActionExecutedContext.
        //    // That means you can do your stuff after the execution of API task.
        //    var resultContext = await next();

        //    if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        //    int userId = Convert.ToInt32(resultContext.HttpContext.User.GetUserId());

        //    if (userId != 0)
        //    {
        //        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

        //        if (user == null)
        //        {
        //            context.Result = new NotFoundResult();
        //            return;
        //        }

        //        user.LastActive = DateTime.UtcNow;

        //        await _userManager.UpdateAsync(user);
        //    }
        //    else
        //    {
        //        context.Result = new BadRequestObjectResult("Error occured while fetching user from Httpcontext");
        //        return;
        //    }
        //}
    }
}
