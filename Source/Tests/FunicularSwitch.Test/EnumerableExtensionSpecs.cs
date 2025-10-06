using FluentAssertions;
using FunicularSwitch.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test;

[TestClass]
public class EnumerableExtensionSpecs
{
    [TestMethod]
    public void EnumerableConcatStillWorks()
    {
        Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 2)).Should().BeEquivalentTo(Enumerable.Range(0, 4));
        Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 2).ToArray()).Should().BeEquivalentTo(Enumerable.Range(0, 4));

        0.Yield().Concat(new[] { 1, 2 }).Should().BeEquivalentTo(new[]{0, 1, 2});
    }

    [TestMethod]
    public void SingleOrNone_EmptyCollection_ReturnsNone()
    {
        // Given
        IEnumerable<string> subject = [];
        
        // When
        var result = subject.SingleOrNone();
        
        // Then
        result.Should().BeNone();
    }

    [TestMethod]
    public void SingleOrNone_Null_ThrowsException()
    {
        // Given 
        IEnumerable<string> subject = null!;
        
        // When
        var getResult = () => subject.SingleOrNone();
        
        // Then
        getResult.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void SingleOrNone_Single_ReturnsSome()
    {
        // Given 
        IEnumerable<string> subject = ["A"];
        
        // When
        var result = subject.SingleOrNone();

        // Then
        result.Should().BeSome().Which.Should().Be("A");
    }

    [TestMethod]
    public void SingleOrNone_Multiple_ReturnsNone()
    {
        // Given 
        IEnumerable<string> subject = ["A", "B", "C"];
        
        // When
        var result = subject.SingleOrNone();

        // Then
        result.Should().BeNone();
    }

    [TestMethod]
    public void SingleOrNone_Infinite_ReturnsNone()
    {
        // Given 
        var subject = InfiniteEnumerable();
        
        // When
        var result = subject.SingleOrNone();

        // Then
        result.Should().BeNone();
        return;

        IEnumerable<int> InfiniteEnumerable()
        {
            var i = 0;
            while (true)
            {
                yield return i++;
            }
        } 
    }

    [TestMethod]
    public void SingleOrNone_PredicateLeavesOnlyOneElement_ReturnsSome()
    {
        // Given 
        IEnumerable<int> subject = [1, 2, 3];
        
        // When
        var result = subject.SingleOrNone(i => i % 2 == 0);

        // Then
        result.Should().BeSome().Which.Should().Be(2);
    }
    
    [TestMethod]
    public void SingleOrNone_PredicateLeavesMultipleElements_ReturnsNone()
    {
        // Given 
        IEnumerable<int> subject = [1, 2, 3, 4, 5];
        
        // When
        var result = subject.SingleOrNone(i => i >= 3);

        // Then
        result.Should().BeNone();
    }

    [TestMethod]
    public void FirstOrNone_Empty_ReturnsNone()
    {
        // Given 
        IEnumerable<int> subject = [];
        
        // When
        var result = subject.FirstOrNone();

        // Then
        result.Should().BeNone();
    }
    
    [TestMethod]
    public void FirstOrNone_OneElement_ReturnsSome()
    {
        // Given 
        IEnumerable<int> subject = [1];
        
        // When
        var result = subject.FirstOrNone();

        // Then
        result.Should().BeSome().Which.Should().Be(1);
    }
    
    [TestMethod]
    public void FirstOrNone_Multiple_ReturnsFirst()
    {
        // Given 
        IEnumerable<int> subject = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        
        // When
        var result = subject.FirstOrNone();

        // Then
        result.Should().BeSome().Which.Should().Be(1);
    }
    
    [TestMethod]
    public void FirstOrNone_Predicate_ReturnsFirst()
    {
        // Given 
        IEnumerable<int> subject = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        
        // When
        var result = subject.FirstOrNone(i => i >= 4);

        // Then
        result.Should().BeSome().Which.Should().Be(4);
    }