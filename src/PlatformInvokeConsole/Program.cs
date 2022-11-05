using System.Text;

namespace PlatformInvokeConsole;

using System.Runtime.InteropServices;

internal static partial class Program
{
    [LibraryImport("kernel32.dll",
                   EntryPoint = "lstrcpyn",
                   StringMarshalling = StringMarshalling.Utf8)]
    private static partial void CopyString(IntPtr destination, IntPtr src, int length);

    [LibraryImport("msvcrt.dll", EntryPoint = "memcpy")]
    private static partial void CopyMemory(IntPtr destination, IntPtr src, UIntPtr count);

    [LibraryImport("msvcrt.dll", EntryPoint = "fopen_s", StringMarshalling = StringMarshalling.Utf8)]
    private static partial int OpenFile(out IntPtr file,
                                        string     fileName,
                                        string     mode);

    [LibraryImport("msvcrt.dll", EntryPoint = "fread", StringMarshalling = StringMarshalling.Utf8)]
    private static partial void ReadFile(Span<byte> buffer, int size, int number, IntPtr file);

    [LibraryImport("msvcrt.dll", EntryPoint = "fclose")]
    private static partial void CloseFile(IntPtr stream);

    private static unsafe void Main()
    {
        // Read a file.
        var result = OpenFile(out var file,
                              @"C:\Users\marku\Code\C#\Low-Level-CSharp\src\PlatformInvokeConsole\test.txt", "r");

        // Allocate some space on the stack to store the contents of the buffer.
        // Don't have to worry about freeing this memory, it will pop off the stack at the end of the frame.
        Span<byte> buffer = stackalloc byte[200];
        // Read the contents of the file into the buffer.
        ReadFile(buffer, sizeof(byte), 27, file);
        // Close the file.
        CloseFile(file);

        // Write the contents of the buffer out.
        Console.WriteLine(Encoding.Default.GetString(buffer));

        // Allocate 5 bytes on the heap.
        var name = Marshal.AllocHGlobal(sizeof(byte) * 5);
        // Allocate a name on the heap.
        var temp = Marshal.StringToHGlobalAnsi("Tom");
        // Copy the string from the temp pointer to the name pointer.
        CopyString(name, temp, 4);

        // Allocated 100 ints on the heap.
        var numbersPtr = Marshal.AllocHGlobal(sizeof(int)     * 100);
        var destinationPtr = Marshal.AllocHGlobal(sizeof(int) * 100);
        // Get a span to cover that memory.
        var numbers = new Span<int>(numbersPtr.ToPointer(), 100)
        {
            [0] = 1,
            [1] = 2,
            [2] = 3,
        };

        CopyMemory(destinationPtr, numbersPtr, new UIntPtr(100));
        var numbersTwo = new Span<int>(destinationPtr.ToPointer(), 100);
        // Because we copied the memory of numbersPtr over (which was altered by the span) we can now natively copy the memory over.
        // When we write line it will be 2.
        Console.WriteLine(numbersTwo[1]);

        // Dereference and get the string from the name pointer.
        Console.WriteLine(Marshal.PtrToStringAnsi(name));

        // Free both pointers now that we don't need them.
        Marshal.FreeHGlobal(temp);
        Marshal.FreeHGlobal(name);
        Marshal.FreeHGlobal(numbersPtr);
        Marshal.FreeHGlobal(destinationPtr);
    }
}