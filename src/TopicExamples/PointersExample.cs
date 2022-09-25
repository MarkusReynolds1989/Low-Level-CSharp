using System.Runtime.InteropServices;

namespace TopicExamples;

public static class PointersExample
{
    // Also unsafe because it handles pointers.
    private static unsafe int Max(int* array, uint length)
    {
        var max = int.MinValue;

        for (var i = 0; i < length; i += 1)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }

        return max;
    }

    // Putting the unsafe keyword onto a block does several things.
    // First, it allows 
    public static unsafe void LocalMain()
    {
        // Like malloc, allocates a certain amount of bytes.
        // We are allocated 10 blocks that are the size of int, an int array allocated on the heap essentially.
        int* data = (int*) Marshal.AllocHGlobal(10 * sizeof(int));
        // We have to make sure that the allocation was successful first, then we can use the memory.
        if (data != null)
        {
            data[0] = 3;
            // This will work, we allocated data to 0.
            Console.WriteLine(data[0]);

            // This will also work, but it will give us random data as this data cell is allocated for us but isn't cleared up or assigned.
            Console.WriteLine(data[5]);

            // Same situation here, even though we are outside the bounds of the array, we still can get data from data 200, but it's 
            // random data and we actually don't own it.
            // That's what makes pointers unsafe.
            Console.WriteLine(data[200]);
            // This array allocation is just for ease of loading the int* data from above.
            var temp = new[] {-100, 20, 1000, 49, -5, 10_000, -222, 2, 100, -2222};
            for (var i = 0; i < temp.Length; i += 1)
            {
                data[i] = temp[i];
            }

            // Pass that pointer to data into the max method along with it's length to find it's max.
            var max = Max(data, 10);
            Console.WriteLine(max);
        }

        // Don't forget to free the memory when you are done or you could run into a memory leak.
        Marshal.FreeHGlobal((IntPtr) data);
        // You don't have to worry about an accidental double free, if the IntPtr is already 0 then it will do nothing.
    }
}