using System.Runtime.InteropServices;
using System.Text;

namespace TopicTests;

public class NativeDataTests
{
    // An int[] would become an int*, an int[][] would become int** and so on and so forth.

    [Fact]
    public void CharLowerRef()
    {
        var test = new StringBuilder("TEST");
        // Will set all characters to lowercase (in place).
        _ = NativeImportDataTypes.CharLowerBuff(test, 4);
        Assert.Equal("test", test.ToString());
    }

    [Fact]
    public unsafe void CharLowerPointer()
    {
        var testUnmanaged = Marshal.AllocHGlobal(sizeof(byte) * 4);
        var testWindow = new Span<byte>(testUnmanaged.ToPointer(), 4)
        {
            [0] = (byte) 'T',
            [1] = (byte) 'E',
            [2] = (byte) 'S',
            [3] = (byte) 'T'
        };

        // We can also pass a unmanaged byte array to the native function because it understands the memory layout.
        _ = NativeImportDataTypes.CharLowerBuff((byte*) testUnmanaged, 4);
        Assert.Equal("test", Encoding.UTF8.GetString(testWindow));
        Marshal.FreeHGlobal(testUnmanaged);
    }
}