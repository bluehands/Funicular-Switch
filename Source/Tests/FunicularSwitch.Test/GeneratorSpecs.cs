﻿using System;
using System.Globalization;
using FluentAssertions;
using FunicularSwitch.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test
{
    [TestClass]
    public class When_using_generated_result_type
    {
        [TestMethod]
        public void Then_it_feels_good()
        {
            static OperationResult<decimal> Divide(decimal i, decimal divisor) => divisor == 0
                ? OperationResult.Error<decimal>(MyError.Generic("Division by zero"))
                : i / divisor;

            OperationResult<int> result = 42;

            var calc = result
                .Bind(i => Divide(i, 0))
                .Map(i => (i * 2).ToString(CultureInfo.InvariantCulture));
            
            calc.Should().BeEquivalentTo(OperationResult<string>.Error(MyError.Generic("Division by zero")));

            var combinedError = calc.Aggregate(OperationResult.Error<int>(MyError.NotFound));
            var combinedErrorStatic = OperationResult.Aggregate(calc, OperationResult.Error<int>(MyError.NotFound), (_, i) => i);
            var combinedOk = OperationResult.Ok(42).Aggregate(OperationResult.Ok(" is the answer"));
            var combinedOkStatic = OperationResult.Aggregate(OperationResult.Ok(42), OperationResult.Ok(" is the answer"));
        }
    }

    [ResultType(typeof(MyError))]
    abstract partial class OperationResult<T>
    {
    }

    public static partial class MyErrorExtension
    {
        [MergeError]
        public static string Merge(this string error, string other) => $"{error}{Environment.NewLine}{other}";

        [MergeError]
        public static MyError Merge(this MyError error, MyError other) => error.Merge(other);
    }
}