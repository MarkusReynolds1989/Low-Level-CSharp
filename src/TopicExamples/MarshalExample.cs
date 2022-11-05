namespace TopicExamples;

public static class MarshalExample
{
    // This has to be unsafe because we are using a pointer to unmanaged memory.
    public static unsafe void ForeignUnmanagedFunction(byte* charList, uint length)
    {
        // Notice here that when you want to dereference the field of a pointer you have to use the -> arrow operator.
        // If you know C++ or C this will be familiar for you.
        // In this case, because the pointer only points to the first element of memory it will only return that one.
        // Console.WriteLine(charList->ToString());

        var first = 0;
        var last = length;

        while (first < last)
        {
            Console.WriteLine(first);
            Console.WriteLine(last);

            // Traditional swap.
            var temp = charList[first];
            charList[first] = charList[last];
            charList[last] = temp;

            (charList[first], charList[last]) = (charList[last], charList[first]);

            first += 1;
            last -= 1;
        }
    }
}