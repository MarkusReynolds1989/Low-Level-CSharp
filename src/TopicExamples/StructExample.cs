namespace TopicExamples;

public static class StructExample
{
    private struct Person
    {
        public ReadOnlyMemory<char> Name { get; }
        public int                  Age  { get; set; }

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
        // Will mutate because the person class is a heap allocated reference type.
        // This means it is passed by reference.
        person.Age += 1;
    }

    private static void AddYearAge(Person person)
    {
        // Won't mutate the person because it was passed by value (value type). 
        // This is a copy, think of it as a brand new stack allocation of person localized in this stack frame.
        person.Age += 1;
    }

    // How can we mutate the struct? Pass by reference.
    private static void AddYearAge(ref Person person)
    {
        // This is the struct that was stack allocated and we are actually
        // passing a reference to that place in the stack for us to modify instead of a copy.
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