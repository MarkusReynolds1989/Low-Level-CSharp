namespace TopicTests;

public class StructExamples
{
    [Fact]
    public void ImmutabilityTest()
    {
        // This person object is a value type and is stack allocated. 
        var tom = new StructExample.Person("tom".AsMemory(), 32);
        Assert.Equal(32, tom.Age);
        // Cannot be mutated because it's passed by value.
        StructExample.AddYearAge(tom);
        Assert.Equal(32, tom.Age);
        // If we return the value it will increment, however, because the local value was altered
        // and returned.
        Assert.Equal(33,StructExample.AddYearAge(tom));
    }

    [Fact]
    public void RefTest()
    {
        var tom = new StructExample.Person("tom".AsMemory(), 32);
        Assert.Equal(32, tom.Age);
        // Passing this value type by reference makes it to where we can mutate it.
        // It also doesn't get copied in this case.
        // Which saves us memory.
        StructExample.AddYearAge(ref tom);
        Assert.Equal(33, tom.Age);
    }

    [Fact]
    public void ClassTest()
    {
        // Classes are pass by reference by default, so we can mutate them
        // when we pass them in.
        var tim = new StructExample.PersonC(32);
        StructExample.AddYearAge(tim);
        Assert.Equal(33, tim.Age);
    }
}