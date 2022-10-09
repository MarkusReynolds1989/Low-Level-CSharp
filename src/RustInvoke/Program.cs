using System.Runtime.InteropServices;

namespace RustInvoke;

internal static unsafe class Program
{
    [DllImport(
        @"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\low_level_algo\target\debug\low_level_algo.dll")]
    // Using a raw pointer is the most unsafe way, but also the easiest way to pass information back and forth
    // via FFI.
    // This is a Rust dll, but any C style ABI should work: C, D, C++, etc.
    private static extern void reverse_array(out int* input, uint size);

    [DllImport(
        @"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\low_level_algo\target\debug\low_level_algo.dll")]
    // Call sum_array from the Rust here.
    private static extern int sum_array(int* input, uint size);

    [DllImport(@"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\c_dlls\other_ffi.dll")]
    private static extern int max(int* input, uint size);

    [DllImport(@"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\d_dlls\more_ffi.dll")]
    private static extern int min(int* input, uint size);

    // When we have to compile this to native code, we will have to pull the loose DLLs and bundle them up with the 
    // executable.
    private static void Main()
    {
        // Because the int corresponds in memory to the i32 of rust, they can read the memory layout 
        // from each other and work together.
        var array = (int*) Marshal.AllocHGlobal(sizeof(int) * 10);
        // Safer way, we just pin the memory we need from the GC, this will make it to where
        var span = new Span<int>(array, 10);
        for (var i = 0; i < 10; i += 1)
        {
            span[i] = i;
        }

        reverse_array(out array, 10);

        foreach (var item in span)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();

        // From C.
        // Don't worry about this too much, this is just to show that any native DLL with a C ABI will work.
        var resultMax = max(array, 10);
        Console.WriteLine(resultMax);

        // From D.
        // As you can see this will also work correctly.
        var resultMin = min(array, 10);
        Console.WriteLine(resultMin);

        var resultSum = sum_array(array, 10);
        Console.WriteLine(resultSum);

        // Don't forget to free the heap you allocated.
        Marshal.FreeHGlobal((IntPtr) array);

        // Why do we care? Because we can use this for so many different things.
        // I'll include some example ideas in the readme.
    }
}