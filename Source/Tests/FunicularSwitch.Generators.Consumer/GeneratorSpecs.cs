using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunicularSwitch.Generators.Consumer.Extensions;

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class When_using_generated_result_type
{
	[TestMethod]
	public void Then_it_feels_good()
	{
		static OperationResult<decimal> Divide(decimal i, decimal divisor) => divisor == 0
			? OperationResult.Error<decimal>(Error.Generic("Division by zero"))
			: i / divisor;

		OperationResult<int> result = 42;

		var calc = result
			.Bind(i => Divide(i, 0))
			.Map(i => (i * 2).ToString(CultureInfo.InvariantCulture));

		calc.Should().BeEquivalentTo(OperationResult<string>.Error(Error.Generic("Division by zero")));

		var combinedError = calc.Aggregate(OperationResult.Error<int>(Error.NotFound));
		var combinedErrorStatic = OperationResult.Aggregate(calc, OperationResult.Error<int>(Error.NotFound), (_, i) => i);
		var combinedOk = OperationResult.Ok(42).Aggregate(OperationResult.Ok(" is the answer"));
		var combinedOkStatic = OperationResult.Aggregate(OperationResult.Ok(42), OperationResult.Ok(" is the answer"));

		var transformedToInt = combinedOkStatic.As<int>(() => Error.Generic("Unexpected type"));

		static IEnumerable<Error> IsGreaterThanFive(int i)
		{
			if (i <= 5)
				yield return Error.Generic("To small");
			if (i == 3)
				yield return Error.Generic("Uuh, it's three...");
		}

		(3.Validate(IsGreaterThanFive) is OperationResult<int>.Error_
		{
			Details: Error.Aggregated_
			{
				Errors:
				{
					Count: 2
				}
			}
		}).Should().BeTrue();
	}

	[TestMethod]
	public async Task Void_switches_are_generated()
	{
		var error = Error.Generic("This is wrong");

		static void DoNothing<T>(T item) { }
		error.Switch(
			generic: DoNothing,
			notFound: DoNothing,
			notAuthorized: DoNothing,
			aggregated: DoNothing
		);

		static Task DoNothingAsync<T>(T item) => Task.CompletedTask;
		await error.Switch(
			generic: DoNothingAsync,
			notFound: DoNothingAsync,
			notAuthorized: DoNothingAsync,
			aggregated: DoNothingAsync
		);
	}
}

[ResultType(ErrorType = typeof(string))]
public abstract partial class Result<T>
{
}

[ResultType(typeof(Error))]
abstract partial class OperationResult<T>
{
}

public static partial class ErrorExtension
{
	[MergeError]
	public static Error MergeErrors(this Error error, Error other) => error.Merge(other);

	[MergeError]
	public static string MergeErrors(this string error, string other) => $"{error}{Environment.NewLine}{other}";
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract class Error
{
	public static Error Generic(string message) => new Generic_(message);

	public static readonly Error NotFound = new NotFound_();
	public static readonly Error NotAuthorized = new NotAuthorized_();

	public static Error Aggregated(ImmutableList<Error> errors) => new Aggregated_(errors);

	public Error Merge(Error other) => this is Aggregated_ a
		? a.Add(other)
		: other is Aggregated_ oa
			? oa.Add(this)
			: Aggregated(ImmutableList.Create(this, other));

	public class Generic_ : Error
	{
		public string Message { get; }

		public Generic_(string message) : base(UnionCases.Generic)
		{
			Message = message;
		}
	}

	public class NotFound_ : Error
	{
		public NotFound_() : base(UnionCases.NotFound)
		{
		}
	}

	public class NotAuthorized_ : Error
	{
		public NotAuthorized_() : base(UnionCases.NotAuthorized)
		{
		}
	}

	public class Aggregated_ : Error
	{
		public ImmutableList<Error> Errors { get; }

		public Aggregated_(ImmutableList<Error> errors) : base(UnionCases.Aggregated) => Errors = errors;

		public Error Add(Error other) => Aggregated(Errors.Add(other));
	}

	internal enum UnionCases
	{
		Generic,
		NotFound,
		NotAuthorized,
		Aggregated
	}

	internal UnionCases UnionCase { get; }
	Error(UnionCases unionCase) => UnionCase = unionCase;

	public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
	bool Equals(Error other) => UnionCase == other.UnionCase;

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((Error)obj);
	}

	public override int GetHashCode() => (int)UnionCase;
}