using FluentAssertions;
using FunicularSwitch.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class FuncToActionSpecs
{
    [TestMethod]
    public void IgnoreReturn_T1_T2_FuncIsExecuted()
    {
        // Given
        bool funcCalled = false;
        int numberPassed = -1;
        var func = string (int x) =>
        {
            funcCalled = true;
            numberPassed = x;
            return "Hi";
        };
        
        // When
        var action = func.IgnoreReturn();
        action(1);
        
        // Then
        funcCalled.Should().BeTrue();
        numberPassed.Should().Be(1);
    }

    [TestMethod]
    public void IgnoreReturn_T_FuncIsExecuted()
    {
        // Given
        bool funcCalled = false;
        var func = string () =>
        {
            funcCalled = true;
            return "Hello";
        };
        
        // When
        var action = func.IgnoreReturn();
        action();
        
        // Then
        funcCalled.Should().BeTrue();
    }

    [TestMethod]
    public void ToFunc_TNullable_ActionIsCalled()
    {
        // Given
        bool actionCalled = false;
        var action = () =>
        {
            actionCalled = true;
        };
        
        // When
        var func = action.ToFunc<string>();
        var result = func();
        
        // Then
        actionCalled.Should().BeTrue();
    }

    [TestMethod]
    public void ToFunc_Tint_ActionIsCalled()
    {
        // Given
        bool actionCalled = false;
        int numberPassed = -1;
        var action = (int x) =>
        {
            actionCalled = true;
            numberPassed = x;
        };
        
        // When
        var func = action.ToFunc();
        var result = func(23);
        
        // Then
        actionCalled.Should().BeTrue();
        numberPassed.Should().Be(23);
    }

    [TestMethod]
    public async Task ToFunc_FuncTask_ActionIsCalled()
    {
        // Given
        bool actionCalled = false;
        var asyncAction = () =>
        {
            actionCalled = true;
            return Task.CompletedTask;
        };
        
        // When
        var func = asyncAction.ToFunc<string>();
        var result = await func();
        
        // Then
        actionCalled.Should().BeTrue();
    }

    [TestMethod]
    public async Task ToFunc_Func_T_Task_ActionIsCalled()
    {
        // Given
        bool actionCalled = false;
        int numberPassed = -1;
        var asyncAction = (int x) =>
        {
            actionCalled = true;
            numberPassed = x;
            return Task.CompletedTask;
        };
        
        // When
        var func = asyncAction.ToFunc();
        var result = await func(12);
        
        // Then
        actionCalled.Should().BeTrue();
        numberPassed.Should().Be(12);
    }
}