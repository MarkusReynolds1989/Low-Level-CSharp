using System.Runtime.InteropServices;

namespace TopicExamples;

public static class MarshalExample
{
    // This has to be unsafe because we are using a pointer to unmanaged memory.
    private static unsafe void ForeignUnmanagedFunction(byte* charList, uint length)
    {
        // Notice here that when you want to dereference the field of a pointer you have to use the -> arrow operator.
        // If you know C++ or C this will be familiar for you.
        // In this case, because the pointer only points to the first element of memory it will only return that one.
        // Console.WriteLine(charList->ToString());

        var first = 0;
        var last = length;

        while (first < last)
        {
            Console.WriteLine(first);
            Console.WriteLine(last);

            // Traditional swap.
            var temp = charList[first];
            charList[first] = charList[last];
            charList[last] = temp;

            (charList[first], charList[last]) = (charList[last], charList[first]);

            first += 1;
            last -= 1;
        }
    }

    public static void LocalMain()
    {
        // In some circumstances we may need to pass unmanaged data to an unmanaged context.
        // In that case we would use Marshalling to handle for that.

        // A managed string.
        var palindrome = "racecar";
        var length = palindrome.Length - 1;

        // Now we have an unmanaged pointer.
        var pointer = Marshal.StringToHGlobalAuto(palindrome);

        // Pass a string that hasn't been assigned yet.
        var otherPointer = Marshal.StringToHGlobalAuto("Other");

        // Move the pointer to some unmanaged context.
        unsafe
        {
            // We are passing a byte array to the FFI, this might look like char* in other languages like C and C++.
            ForeignUnmanagedFunction((byte*) pointer, (uint) length);
            // Get a string back from the pointer.
            var managedName = Marshal.PtrToStringAuto(pointer);
            Console.WriteLine(managedName);
        }

        // Remember to free the pointer.
        Marshal.FreeHGlobal(pointer);
    }
}