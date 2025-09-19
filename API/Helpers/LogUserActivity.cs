using System;
using API.Data;
using API.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();
        // will not track the unuthenticated user
        if (context.HttpContext.User.Identity?.IsAuthenticated != true) return;
        var memberId = resultContext.HttpContext.User.GetMemberId();

        // to update the database
        var dbContext = resultContext.HttpContext.RequestServices
                        .GetRequiredService<AppDbContext>();
        // updating the database
        await dbContext.Members.Where(x => x.Id == memberId)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.LastActive, DateTime.UtcNow));

        // After this update the Program class service
    }
}
