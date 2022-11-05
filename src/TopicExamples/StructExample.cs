namespace TopicExamples;

struct Example
{
    private int   x;
    private float y;
}

public static class StructExample
{
    // Defining it as a "ref struct" has guarantees that it won't move to the heap.
    // This is our way of avoiding the implementation details problem where 
    // we can't be sure if an item is on the heap or stack.
    private ref struct Person
    {
        private ReadOnlyMemory<char> Name { get; }
        public  int                  Age  { set; get; }

        public Person(ReadOnlyMemory<char> name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    private class PersonC
    {
        public int Age { get; set; }

        public PersonC(int age)
        {
            Age = age;
        }
    }

    private static void AddYearAge(PersonC person)
    {
        // Will mutate because the person class is a reference type.
        // Which means it will be passed by reference.
        person.Age += 1;
    }

    private static void AddYearAge(Person person)
    {
        // Won't mutate the person because it was passed by value (value type). 
        // This is a copy, think of it as a brand new allocation of person localized in this stack frame.
        person.Age = 3;
    }

    // How can we mutate the struct? Pass by reference.
    private static void AddYearAge(ref Person person)
    {
        // Although this struct is pass by value, we are passing it by reference so it will be used
        // as if it were a reference type.
        person.Age += 1;
    }

    public static void LocalMain()
    {
        // This person object is a value type and is stack allocated. 
        var tom = new Person("tom".AsMemory(), 32);
        Console.WriteLine(tom.Age);
        AddYearAge(tom);
        Console.WriteLine(tom.Age);

        var tim = new PersonC(32);
        Console.WriteLine(tim.Age);
        AddYearAge(tim);
        Console.WriteLine(tim.Age);

        // When we pass the struct by reference we an mutate it.
        // It is still on the stack, but we are not passing a copy of it, but a reference instead.
        // This allows mutation.
        AddYearAge(ref tom);

        Console.WriteLine(tom.Age);
    }
}