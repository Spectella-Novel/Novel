using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class Result
    {
        public virtual bool IsSuccess { get; }
        public string Error { get; }

        protected Result(bool isSuccess, string error = null)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true);
        public static Result Failure(string error) => new(false, error);
    }
    public class Result<T> : Result
    {
        public T Value { get; }
        public override bool IsSuccess => Value != null && base.IsSuccess;
        
        public Result(bool isSeccess, T value, string error = null) : base(isSeccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Failure(string error) => new(false, default, error);
    }
}
