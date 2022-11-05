using System.Runtime.InteropServices;

namespace TopicTests;

public class FunctionPointerTests
{
    [Fact]
    public void AddTest()
    {
        MathOp funcAdd = FunctionPointerExample.Add;
        var addPointer = Marshal.GetFunctionPointerForDelegate(funcAdd);
        var addFunction = Marshal.GetDelegateForFunctionPointer(addPointer, typeof(MathOp));

        // Dereferencing the pointer in C# has the overhead of allocating it all on the heap.
        // So be careful when using it, favor using it in cases where you want to pass it to unmanaged code.
        Assert.Equal(addFunction.DynamicInvoke(3, 4), 7);
    }

    [Fact]
    public void SubTest()
    {
        MathOp funcSub = FunctionPointerExample.Sub;

        var subPointer = Marshal.GetFunctionPointerForDelegate(funcSub);

        var subFunction = Marshal.GetDelegateForFunctionPointer(subPointer, typeof(MathOp));

        Assert.Equal(subFunction.DynamicInvoke(4, 3), 1);
    }

    [Fact]
    public void ForeignFunctionTest()
    {
        // We pretend here that we are passing our function off to some foreign code.
        // For instance, we could package our function down and pass it to run in 
        // unmanaged C or C++ code.
        MathOp funcAdd = FunctionPointerExample.Add;
        var addPointer = Marshal.GetFunctionPointerForDelegate(funcAdd);
        Assert.Equal(FunctionPointerExample.SomeMethod(addPointer, typeof(MathOp)), new[] {2, 3, 4, 5});
    }
}