using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunicularSwitch.Extensions;
using FunicularSwitch.Generators;

namespace FunicularSwitch
{
	public abstract partial class Result
	{
        public static Result<Unit> Try(Action action, Func<Exception, string> formatError)
        {
            try
            {
                action();
                return No.Thing;
            }
            catch (Exception e)
            {
                return Error<Unit>(formatError(e));
            }
        }

		public static async Task<Result<Unit>> Try(Func<Task> action, Func<Exception, string> formatError)
		{
			try
			{
				await action();
				return No.Thing;
			}
			catch (Exception e)
			{
				return Error<Unit>(formatError(e));
			}
		}
	}

	[ResultType(ErrorType = typeof(string))]
	public abstract partial class Result<T>
	{
	}
	public static partial class ResultExtension
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public static string ErrorSeparator = Environment.NewLine;

        [MergeError]
        public static string JoinErrors(this string error1, string error2) =>
	        string.Join(ErrorSeparator, error1, error2);

        public static Result<T1> As<T, T1>(this Result<T> result) =>
	        result.Bind(r =>
	        {
		        if (r is T1 converted)
			        return converted;
		        return Result.Error<T1>($"Could not convert '{r?.GetType().Name}' to type {typeof(T1)}");
	        });

        public static Result<T1> As<T1>(this Result<object> result) => result.As<object, T1>();

        public static Result<T> First<T>(this IEnumerable<T> candidates, Func<T, bool> predicate, Func<string> noMatch) =>
	        candidates
		        .FirstOrDefault(predicate)
		        .NotNull(noMatch);
        
    }
}