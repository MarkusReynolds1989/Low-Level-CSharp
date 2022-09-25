namespace TopicExamples;

public static class SpanExample
{
    public static void LocalMain()
    {
        // Allocate a new array on the heap.
        var array = new int[10];

        // This array is allocated on the stack.
        // You should use span, otherwise you will get a pointer and have to manage the pointer.
        // This will pop off the stack at the end of the stack frame.
        Span<int> arrayStack = stackalloc int[10];
        // Use stack allocation in much the same way you'd use struct, when you need great memory performance
        // but for small memory footprints, otherwise you could run into a stack overflow.
        // Because C# is so good at heap allocations, stack alloc might not actually be more efficient, so make sure
        // to profile while deciding to use it.

        // A span is a ref struct that is allocated on the stack.
        var arraySpan = new Span<int>(array);
        // Marking this method as async will cause it not to compile due to the use of span.
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