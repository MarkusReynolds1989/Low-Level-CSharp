using System.Collections;
using System.Runtime.InteropServices;

namespace TopicExamples;

// This technique is mostly to use to pass to low level code, i.e. passing a C# function to a Rust library (suppose we wanted
// to map a function over some sort of data in Rust or C but do it more quickly than we could in C#.)

// The delegate we want to use to pass as a function pointer.
public delegate int MathOp(int x, int y);

public static class FunctionPointerExample
{
    public static IEnumerable SomeMethod(IntPtr function, Type type)
    {
        int[] testArray = {1, 2, 3, 4};
        var map = Marshal.GetDelegateForFunctionPointer(function, type);

        foreach (var item in testArray)
        {
            // Yield a new value that has 1 added to it.
            yield return map.DynamicInvoke(item, 1);
        }
    }

    public static int Add(int x, int y) => x + y;
    public static int Sub(int x, int y) => x - y;
}