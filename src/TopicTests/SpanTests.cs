namespace TopicTests;

public class SpanExamples
{
    [Fact]
    public void SpanBasics()
    {
        // A span is a ref struct that is allocated on the stack.
        // It is a handle to an arbitrary block of memory.
        var array = new int[10];
        // Here we create a span to be a handle to the array allocated on line 10.
        var arraySpan = new Span<int>(array);
        Assert.Equal(array, arraySpan.ToArray());
        // Because spans stay on the stack, they cannot be used in several situations,
        // including with Async.
    }

    [Fact]
    public void SpanSlice()
    {
        // Changes to the span are also made to the underlying structure.
        var array = new int[10];
        var arraySpan = new Span<int>(array)
        {
            [0] = 3
        };

        Assert.Equal(array[0], arraySpan[0]);
    }

    [Fact]
    public void SpanReverse()
    {
        // This span is allocated on the stack, reversed on the stack, and then
        // returned as a heap allocated array, but still with a span pointing to it.
        var values = SpanExample.AssignValues();
        var array = new int[10];
        var counter = 0;
        for (var i = 9; i > 0; i -= 1)
        {
            array[counter] = i;
            counter += 1;
        }

        // When we convert it to an array, it goes back to being a heap allocated data
        // structure. But we can save on allocations everywhere before by using Span.
        Assert.Equal(values, array);
    }

    [Fact]
    public void ScopedTest()
    {
        Span<int> values = stackalloc int[10];
        // This is stack allocated, however, we can still pass it to the other function because of the 
        // 'scoped' keyword. This allows us to assert that the item we are using's lifetime won't exceed the lifetime
        // of where it is called from.
        SpanExample.AssignValues(values);
        Assert.Equal(0, values[0]);
    }
}