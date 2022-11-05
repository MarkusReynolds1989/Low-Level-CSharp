using System.Runtime.InteropServices;

namespace TopicExamples;

public static class PointersExample
{
    // Also unsafe because it handles pointers.
    public static unsafe int Max(int* array, uint length)
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
}