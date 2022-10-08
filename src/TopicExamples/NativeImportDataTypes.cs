using System.Runtime.InteropServices;
using System.Text;

namespace TopicExamples;

public static unsafe class NativeImportDataTypes
{
    // As an example of data types, here we are using string builder as a byte pointer like we would in an
    // unmanaged language.
    // We don't have to use it though, we can use any type that is similar.
    [DllImport("user32.dll")]
    private static extern int CharLowerBuff([In, Out] StringBuilder input, uint size);

    [DllImport("user32.dll")]
    private static extern int CharLowerBuff(byte* input, uint size);

    public static void LocalMain()
    {
        var test = new StringBuilder("TEST");
        // Will set all characters to lowercase (in place).
        _ = CharLowerBuff(test, 4);
        Console.WriteLine(test.ToString());

        var testUnmanaged = Marshal.AllocHGlobal(sizeof(byte) * 4);
        var testWindow = new Span<byte>(testUnmanaged.ToPointer(), 4)
        {
            [0] = (byte) 'T',
            [1] = (byte) 'E',
            [2] = (byte) 'S',
            [3] = (byte) 'T'
        };

        // We can also pass a unmanaged byte array to the native function because it understands the memory layout.
        _ = CharLowerBuff((byte*) testUnmanaged, 4);
        Console.WriteLine(Encoding.UTF8.GetString(testWindow));

        Marshal.FreeHGlobal(testUnmanaged);
    }
}