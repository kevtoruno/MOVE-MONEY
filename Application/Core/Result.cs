using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsRedirected { get; private set; }
        public bool IsSucess { get; private set; }
        public T Value { get; private set; }
        public bool NotFound { get; private set; }
        public string ErrorMessage { get; private set; } = "";
        public string UrlToRedirectTo { get; private set; } = "";
        public bool IsNoContent { get; private set; }
        public string RouteName { get; set; } = "";
        public bool ResourceCreated { get; set; }

        public static Result<T> Success(T value) => new Result<T> { IsSucess = true, Value = value };

        public static Result<T> NoContent() => new Result<T> { IsNoContent = true };

        public static Result<T> Failure(string errorMessage) => new Result<T> { IsSucess = false, ErrorMessage = errorMessage };

        public static Result<T> ResultNotFound(string errorMessage) => new Result<T> { IsSucess = false, ErrorMessage = errorMessage, NotFound = true };
        public static Result<T> RedirectTo(string urlToRedirectTo) => new Result<T> { IsRedirected = true, UrlToRedirectTo = urlToRedirectTo };
        public static Result<T> CreatedAtRoute(T value, string routeName = "") => new Result<T> { ResourceCreated = true, Value = value, RouteName = routeName };
    }
}
