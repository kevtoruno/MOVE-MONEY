using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MoveMoney.API.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using MoveMoney.API.Models;
namespace MoveMoney.API.Helper
{
    public class LogUserActivity : ActionFilterAttribute
    {
        public string EventType { get; set; }  //Placed, Delivered, PushMoney, PushMoneyAgency, ClosingCashAgent, ClosingCashManager
        public async override void OnActionExecuted(ActionExecutedContext context)
        {
            int orderId = Convert.ToInt32(context.HttpContext.Items["orderId"]);
            string orderStatus = Convert.ToString(context.HttpContext.Items["orderStatus"]);
            string userFullName = Convert.ToString(context.HttpContext.Items["userFullName"]);
            int userId = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            decimal orderTotal = Convert.ToDecimal(context.HttpContext.Items["total"]);
            string eventType = Convert.ToString(context.HttpContext.Items["eventType"]);
            var userLog = new UserLogs();

            if (orderId != 0)
            {
                if (orderStatus == "Processed" || orderStatus == "Ready")
                {
                    if (eventType == "Transfer")
                    {
                        var repo = context.HttpContext.RequestServices.GetService<IMoveMoneyRepository>();
                        userLog.UserId = userId;
                        userLog.EventType = "Transfer";
                        userLog.Description = $"{userFullName} fully processed the order {orderId}";
                        userLog.Amount = orderTotal;
                        userLog.AgencyId = Convert.ToInt32(context.HttpContext.Items["agencyId"]);

                        await repo.CreateUserLog(userLog);
                        await repo.SaveAll();
                    }

                }
            }
        }
    }
}