using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace StudyProject.UI.WebCore.Extensions
{
    public class NotificationFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext filterContext, ResultExecutionDelegate next)
        {
            // do something after the action executes
            //ViewResult result = filterContext.Result as ViewResult;
            //if (result != null)
            //{
            //    result.ViewData["ViewData_NOTIFICATION"] =
            //    "Comes from MyActionAttributeFilter at " + DateTime.Now.ToLongTimeString();
            //}

            //var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);

            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                // var modelState = ModelStateHelpers.SerialiseModelState(filterContext.ModelState);
                controller.TempData["KEY_NOTIFICATION"] = "Teste message tempdata";
                controller.ViewData["ViewData_NOTIFICATION"] = "Teste message viewdata";
            }

            await next();
        }

    }
}
