using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CSharp.Filter
{
    public class ValidateInputAttribute: IAuthorizationFilter
    {
        public ValidateInputAttribute(bool enableValidation)
        {

                this.EnableValidation = enableValidation;
        }
          public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }

        public bool EnableValidation { get; private set; }
    }
}