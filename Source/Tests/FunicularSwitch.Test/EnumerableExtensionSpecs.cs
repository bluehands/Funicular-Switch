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

        0.Yield().Concat([1, 2]).Should().Equal([0, 1, 2]);
    }

    private static IEnumerable<int> PureEnumerable(IEnumerable<int> source)
    {
        foreach (var element in source)
        {
            yield return element;
        }
    }

    private static IEnumerable<int?> PureEnumerable(IEnumerable<int?> source)
    {
        foreach (var element in source)
        {
            yield return element;
        }
    }

    private static IEnumerable<int> ListEnumerable(IEnumerable<int> source) => source.ToList();
    private static IEnumerable<int?> ListEnumerable(IEnumerable<int?> source) => source.ToList();

    [TestMethod]
    public void FirstOrNone_Empty_ReturnsNone()
    {
        // Given
        List<int> subject = [];
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.FirstOrNone();

            // Then
            result.Should().BeNone();
        }
    }
    
    [TestMethod]
    public void FirstOrNone_OneElement_ReturnsSome()
    {
        // Given 
        List<int> subject = [1];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.FirstOrNone();

            // Then
            result.Should().BeSome().Which.Should().Be(1);
        }
    }
    
    [TestMethod]
    public void FirstOrNone_Multiple_ReturnsFirst()
    {
        // Given 
        List<int> subject = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.FirstOrNone();

            // Then
            result.Should().BeSome().Which.Should().Be(1);
        }
    }
    
    [TestMethod]
    public void FirstOrNone_Predicate_ReturnsFirst()
    {
        // Given 
        List<int> subject = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.FirstOrNone(i => i >= 4);

            // Then
            result.Should().BeSome().Which.Should().Be(4);
        }
    }
    
    [TestMethod]
    public void LastOrNone_Empty_ReturnsNone()
    {
        // Given 
        List<int> subject = [];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeNone();
        }
    }
    
    [TestMethod]
    public void LastOrNone_SingleElement_ReturnsElement()
    {
        // Given 
        List<int> subject = [0];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeSome().Which.Should().Be(0);
        }
    }
    
    [TestMethod]
    public void LastOrNone_MultipleElements_ReturnsLastElement()
    {
        // Given 
        List<int> subject = [0, 1, 2, 3, 4];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeSome().Which.Should().Be(4);
        }
    }
    
    [TestMethod]
    public void LastOrNone_NullStruct_ReturnsSome()
    {
        // Given 
        List<int?> subject = [null];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int?> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeSome().Which.Should().BeNull();
        }
    }    
        
    [TestMethod]
    public void LastOrNone_NullStructValue_ReturnsValue()
    {
        // Given 
        List<int?> subject = [1, null, 2];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int?> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeSome().Which.Should().Be(2);
        }
    }    
    
    [TestMethod]
    public void LastOrNone_NullStructEmpty_ReturnsNone()
    {
        // Given 
        List<int?> subject = [];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int?> subject)
        {
            // When
            var result = subject.LastOrNone();

            // Then
            result.Should().BeNone();
        }
    }
    
    [TestMethod]
    public void LastOrNone_Predicate_ReturnsCorrectValue()
    {
        // Given 
        List<int> subject = [1, 2, 3, 4, 5, 6];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.LastOrNone(x => x < 5);

            // Then
            result.Should().BeSome().Which.Should().Be(4);
        }
    }
    
    [TestMethod]
    public void ElementAtOrNone_EmptyEnumerable_ReturnsNone()
    {
        // Given 
        List<int> subject = [];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.ElementAtOrNone(0);

            // Then
            result.Should().BeNone();
        }
    }    
    
    [TestMethod]
    public void ElementAtOrNone_MultipleValues_ReturnsSome()
    {
        // Given 
        List<int> subject = [1, 2, 3, 4, 5, 6, 7, 8];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.ElementAtOrNone(3);

            // Then
            result.Should().BeSome().Which.Should().Be(4);
        }
    }
    
    [TestMethod]
    public void ElementAtOrNone_IndexOutOfRange_ReturnsNone()
    {
        // Given 
        List<int> subject = [1, 2, 3, 4, 5, 6, 7, 8];
        
        Assert(PureEnumerable(subject));
        Assert(ListEnumerable(subject));
        return;

        static void Assert(IEnumerable<int> subject)
        {
            // When
            var result = subject.ElementAtOrNone(10);

            // Then
            result.Should().BeNone();
        }
    }
}