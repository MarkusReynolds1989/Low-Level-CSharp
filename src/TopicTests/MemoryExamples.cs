namespace TopicTests;

public class MemoryExamples
{
    [Fact]
    public void MemoryBasics()
    {
        var array = new[] {1, 2, 3, 4};
        // The memory object is stored on the heap, it's a window over and arbitrary memory type. 
        // In this case, we are windowing over the array that's on the heap.
        // Can be used in spots Span can't, like as the field of a class or with await and yield.
        var memory = new Memory<int>(array);
        // We can capture a span of the memory we are looking at.
        var span = memory.Span;
        Assert.Equal(span[0], 1);

        // Using memory instead of string is faster, it will reduce allocations
        // and can be sliced into much more efficiently.
        var tom = new MemoryExample.Person("tom".AsMemory(), 23);
        Assert.Equal(tom.Name, "tom".AsMemory());
    }

    [Fact]
    public void YieldTest()
    {
        // We can yield up any memory item we want, just like a regular item.
        var emptyStrings = MemoryExample.Examples.GenerateEmptyStrings();
        Assert.NotEmpty(emptyStrings);
    }

    [Fact]
    public void SliceTest()
    {
        // Because we can convert a memory to a span, you can do the changes you need to
        // to a span and then let that span expire and still have the memory. 
        var array = new char[4];
        var test = new Memory<char>(array);
        var testPointer = test.Span;
        testPointer[0] = 'i';
        testPointer[1] = 't';
        testPointer[2] = 'e';
        testPointer[3] = 'm';

        // We changed the items in the Memory via a Span, but the changes
        // still went back to the Memory.
        Assert.Equal(test.ToString(), "item");
    }

    [Fact]
    public void AsyncTest()
    {
        // We can use Async on Memory, because it is heap allocated.
        MemoryExample.Examples.DoWork("test".ToArray());
        // Also read about Memory ownership: https://learn.microsoft.com/en-us/dotnet/standard/memory-and-spans/memory-t-usage-guidelines
    }
}