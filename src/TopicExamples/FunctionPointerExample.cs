using System.Runtime.InteropServices;

namespace TopicExamples;

// This technique is mostly to use to pass to low level code, i.e. passing a C# function to a Rust library (suppose we wanted
// to map a function over some sort of data in Rust or C but do it more quickly than we could in C#.)

// The delegate we want to use to pass as a function pointer.
public delegate int MathOp(int x, int y);

public static class FunctionPointerExample
{
    public static void LocalMain()
    {
        MathOp funcAdd = Add;
        MathOp funcSub = Sub;
        var addPointer = Marshal.GetFunctionPointerForDelegate(funcAdd);
        var subPointer = Marshal.GetFunctionPointerForDelegate(funcSub);
        var addFunction = Marshal.GetDelegateForFunctionPointer(addPointer, typeof(MathOp));
        var subFunction = Marshal.GetDelegateForFunctionPointer(subPointer, typeof(MathOp));

        // Dereferencing the pointer in C# has the overhead of allocating it all on the heap.
        // So be careful when using it, favor using it in cases where you want to pass it to unmanaged code.
        Console.WriteLine(addFunction.DynamicInvoke(3, 4));
        Console.WriteLine(subFunction.DynamicInvoke(3, 4));

        SomeMethod(addPointer, typeof(MathOp));
    }

    public static void SomeMethod(IntPtr function, Type type)
    {
        int[] testArray = {1, 2, 3, 4};
        var map = Marshal.GetDelegateForFunctionPointer(function, type);
        foreach (int item in testArray)
        {
            // Will add one to each value in the array and write it out.
            Console.WriteLine(map.DynamicInvoke(item, 1));
        }
    }

    private static int Add(int x, int y) => x + y;
    private static int Sub(int x, int y) => x - y;
}