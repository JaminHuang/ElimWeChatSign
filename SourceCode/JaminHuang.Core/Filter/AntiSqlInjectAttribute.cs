using JaminHuang.Util;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JaminHuang.Core
{
    public class AntiSqlInjectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionParameters = actionContext.ActionDescriptor.GetParameters();
            foreach (var a in actionParameters)
            {
                if (a.ParameterType == typeof(string))
                {
                    if (actionContext.ActionArguments[a.ParameterName] != null)
                    {
                        actionContext.ActionArguments[a.ParameterName] = ExtendUtil.RemoveSpecString(actionContext.ActionArguments[a.ParameterName].ToString());
                    }
                }
            }
        }
    }
}
