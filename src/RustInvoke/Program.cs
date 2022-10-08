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

    [DllImport(@"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\other_ffi.dll")]
    private static extern int max(int* input, uint size);

    private static void Main()
    {
        // Because the int corresponds in memory to the i32 of rust, they can read the memory layout 
        // from each other and work together.
        var array = (int*) Marshal.AllocHGlobal(sizeof(int) * 10);
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
        var result = max(array, 10);

        Console.WriteLine(result);
        // Don't forget to free the heap you allocated.
        Marshal.FreeHGlobal((IntPtr) array);
    }
}