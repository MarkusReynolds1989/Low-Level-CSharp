using System.Runtime.InteropServices;

namespace PlatformInvokeConsole;

internal static partial class Program
{
    [LibraryImport("kernel32.dll",
                   EntryPoint = "lstrcpyn",
                   StringMarshalling = StringMarshalling.Utf8)]
    private static partial IntPtr CopyString(IntPtr destination, IntPtr src, int length);

    private static void Main()
    {
        // Allocate 200 bytes on the heap.
        var name = Marshal.AllocHGlobal(sizeof(byte) * 5);
        // Allocate a name on the heap.
        var temp = Marshal.StringToHGlobalAnsi("Tom");
        // Copy the string from the temp pointer to the name pointer.
        CopyString(name, temp, 4);

        // Dereference and get the string from the name pointer.
        Console.WriteLine(Marshal.PtrToStringAnsi(name));

        // Free both pointers now that we don't need them.
        Marshal.FreeHGlobal(temp);
        Marshal.FreeHGlobal(name);
    }
}