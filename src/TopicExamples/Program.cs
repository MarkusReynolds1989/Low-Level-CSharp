using System.Runtime.InteropServices;

namespace TopicExamples;

internal static class Program
{
    private static void Main()
    {
        // Allocate a new array on the heap.
        var array = new int[10];

        // A span is a ref struct that is allocated on the stack.
        var arraySpan = new Span<int>(array);
        arraySpan[0] = 3;

        // Changes on the span are also made in the memory.
        Console.WriteLine(arraySpan[0]);
        Console.WriteLine(array[0]);

        // This allows you to do operations on the memory without extra allocations.
        // When you slice into span you aren't allocating extra memory, so you can capture the slice without extra allocations.
        
    }
}