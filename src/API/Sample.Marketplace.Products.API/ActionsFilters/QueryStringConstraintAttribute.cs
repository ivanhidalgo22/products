using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;

namespace Sample.Marketplace.Products.API.ActionsFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class QueryStringConstraintAttribute : ActionMethodSelectorAttribute
    {
        public string ValueName { get; private set; }
        public bool ValuePresent { get; private set; }

        public QueryStringConstraintAttribute(string valueName, bool valuePresent)
        {
            ValueName = valueName;
            ValuePresent = valuePresent;
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            var value = routeContext.HttpContext.Request.Query[ValueName];
            if(ValuePresent)
            {
                return !StringValues.IsNullOrEmpty(value);
            }
            return StringValues.IsNullOrEmpty(value);
        }
    }
}
