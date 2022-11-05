namespace TopicExamples;

public static class SpanExample
{
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

    public static IEnumerable<int> Values()
    {
        Span<int> arrayStack = stackalloc int[10];
        // Can't return this because it's on the stack, it will
        // pop off at the end of the method.
        // return arrayStack;

        // It may still be worthwhile to operate on the item on the stack, however.
        // The stack memory is closer together and faster to iterate.
        // Using a span, even over a regular array will still be faster than a regular array.
        for (var i = 0; i < arrayStack.Length; i += 1)
        {
            arrayStack[i] = i;
        }

        // Notice we can pass the stack allocated items because we are passing them
        // as a span.
        Reverse(arrayStack);

        // Once we put it on the heap as an array, it can be returned without issues.
        return arrayStack.ToArray();
    }
}