using System.Runtime.InteropServices;

namespace TopicTests;

public class PointersTests
{
    [Fact]
    public unsafe void ManagePointers()
    {
        // We are allocated 10 blocks that are the size of int, an int array allocated on the heap essentially.
        // We cast the IntPtr to a raw pointer of int*.
        var data = (int*) Marshal.AllocHGlobal(10 * sizeof(int));

        data[0] = 3;
        Assert.Equal(data[0], 3);
        Marshal.FreeHGlobal((IntPtr) data);
    }

    [Fact]
    public unsafe void ManagePointersWrong()
    {
        var data = (int*) Marshal.AllocHGlobal(10 * sizeof(int));
        Assert.NotEqual(data[22], 3);
        Marshal.FreeHGlobal((IntPtr) data);
        // Even though we are outside the bounds of the array, we still can get data from data 200, but it's 
        // random data and we actually don't own it.
        // That's what makes pointers unsafe.
        // There are cases where this test could fail because that memory spot might actually be 3.
    }

    [Fact]
    public unsafe void FunctionExample()
    {
        // We must use the unsafe keyword when we want to use raw pointers.
        var data = (int*) Marshal.AllocHGlobal(10 * sizeof(int));

        // This array allocation is just for ease of loading the int* data from above.
        var temp = new[] {-100, 20, 1000, 49, -5, 10_000, -222, 2, 100, -2222};
        for (var i = 0; i < temp.Length; i += 1)
        {
            data[i] = temp[i];
        }

        // Pass that pointer to data into the max method along with it's length to find it's max.
        var max = PointersExample.Max(data, 10);
        // Don't forget to free the allocated data.
        Marshal.FreeHGlobal((IntPtr) data);
        Assert.Equal(max, 10_000);
    }
}