using FluentAssertions;
using FunicularSwitch.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class TypeExtensionSpecs
{
    [TestMethod]
    public void BeautifulName_int()
    {
        // Given
        var type = typeof(int);
        
        // When
        var result = type.BeautifulName();
        
        // Then
        result.Should().Be("Int32");
    }

    [TestMethod]
    public void BeautifulName_List_int()
    {
        // Given
        var type = typeof(List<int>);
        
        // When
        var result = type.BeautifulName();
        
        // Then
        result.Should().Be("List<Int32>");
    }

    [TestMethod]
    public void BeautifulName_List_WithoutArgument()
    {
        // Given
        var type = typeof(List<>);
        
        // When
        var result = type.BeautifulName();
        
        // Then
        result.Should().Contain("List");
    }

    [TestMethod]
    public void BeautifulName_FuncWithNestedTypes()
    {
        // Given
        var type = typeof(Func<List<int>, Action<string>>);
        
        // When
        var result = type.BeautifulName();
        
        // Then
        result.Should().Be("Func<List<Int32>,Action<String>>");
    }
}