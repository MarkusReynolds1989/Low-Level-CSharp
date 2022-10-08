using System.Runtime.InteropServices;

namespace RustInvoke;

internal static unsafe class Program
{
    [DllImport(
        @"C:\Users\marku\Code\C#\Low-Level-CSharp\src\RustInvoke\low_level_algo\target\debug\low_level_algo.dll")]
    // Using a raw pointer is the most unsafe way, but also the easiest way to pass information back and forth
    // via FFI.
    private static extern void reverse_array(out int* input, uint size);

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

        // Don't forget to free the heap you allocated.
        Marshal.FreeHGlobal((IntPtr) array);
    }
}