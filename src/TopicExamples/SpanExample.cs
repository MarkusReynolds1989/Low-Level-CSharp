namespace TopicExamples;

public static class SpanExample
{
    public static void LocalMain()
    {
        // Allocate a new array on the heap.
        var array = new int[10];

        // A span is a ref struct that is allocated on the stack.
        var arraySpan = new Span<int>(array);
        for (int i = 0; i < arraySpan.Length; i += 1)
        {
            arraySpan[i] = i;
        }

        // Changes on the span are also made in the memory.
        Console.WriteLine(arraySpan[0]);
        Console.WriteLine(array[0]);

        // This allows you to do operations on the memory without extra allocations.
        // When you slice into span you aren't allocating extra memory, so you can capture the slice without extra allocations.
        var slice = arraySpan[..2];
        Reverse(arraySpan);
        for (int i = 0; i < array.Length; i += 1)
        {
            Console.WriteLine(array[i]);
        }

        // Span has some limitations, because it's on the stack you can't use it across threads.
        // We will talk about other uses of span when we talk about stackalloc and pointers. 
    }

    private static void Reverse(Span<int> array)
    {
        var start = 0;
        var end = array.Length - 1;

        while (start < end)
        {
            // Use deconstruction to swap the start and end. 
            (array[end], array[start]) = (array[start], array[end]);
            end -= 1;
            start += 1;
        }
    }
}