using System.Text;

namespace TopicExamples;

// If you see the IL code for this, it will show you that it's a value type 
// as opposed to an object(reference) type.
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
    public ref struct Person
    {
        private ReadOnlyMemory<char> Name { get; }
        public  int                  Age  { set; get; }

        public Person(ReadOnlyMemory<char> name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public class PersonC
    {
        public int Age { get; set; }

        public PersonC(int age)
        {
            Age = age;
        }
    }

    public static void AddYearAge(PersonC person)
    {
        // Will mutate because the person class is a reference type.
        // Which means it will be passed by reference.
        person.Age += 1;
    }

    public static int AddYearAge(Person person)
    {
        // Won't mutate the person because it was passed by value (value type). 
        // This is a copy, think of it as a brand new allocation of person localized in this stack frame.
        person.Age += 1;
        return person.Age;
    }

    // How can we mutate the struct? Pass by reference.
    public static void AddYearAge(ref Person person)
    {
        // Although this struct is pass by value, we are passing it by reference so it will be used
        // as if it were a reference type.
        person.Age += 1;
    }
}