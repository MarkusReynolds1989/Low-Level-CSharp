namespace TopicExamples;

public static class MemoryExample
{
    public class Person
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

    public static class Examples
    {
        public static IEnumerable<Memory<char>> GenerateEmptyStrings()
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