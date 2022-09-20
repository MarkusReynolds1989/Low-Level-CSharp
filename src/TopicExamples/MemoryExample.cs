namespace TopicExamples;

public static class MemoryExample
{
    public static void LocalMain()
    {
        var array = new[] {1, 2, 3, 4};
        // The memory object is stored on the heap, it's a window over and arbitrary memory type. 
        // In this case, we are windowing over the array that's on the heap.
        // Can be used in spots Span can't, like as the field of a class or with await and yield.
        var memory = new Memory<int>(array);
        // We can capture a span of the memory we are looking at.
        var span = memory.Span;
        Console.WriteLine(span[0]);

        var tom = new Person("tom".AsMemory(), 23);
        Console.WriteLine(tom.Name.ToString());
        Console.WriteLine(tom.Age);
        Examples.DoWork("test".ToArray());
        
        // Also read about Memory ownership: https://learn.microsoft.com/en-us/dotnet/standard/memory-and-spans/memory-t-usage-guidelines
    }

    private class Person
    {
        // Can use Memory in the field of a class.
        // ReadOnlyMemory is the immutable version of Memory.
        public ReadOnlyMemory<char> Name { get; }
        public int                  Age  { get; }

        public Person(ReadOnlyMemory<char> name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    private static class Examples
    {
        public static IEnumerable<Memory<char>> GenerateStrings()
        {
            for (var i = 0; i < 10; i += 1)
            {
                // Can yield memory to IEnumerable.
                yield return Memory<char>.Empty;
            }
        }

        public static async void DoWork(Memory<char> info)
        {
            // Memory can be used in async await situations.
            await Task.Run(() => Console.WriteLine(info.ToString()));
        }
    }
}