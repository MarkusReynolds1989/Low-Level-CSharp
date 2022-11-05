using System.Runtime.InteropServices;

namespace TopicTests;

public class UnsafeMarshalTests
{
    [Fact]
    public void Basics()
    {
        // Marshaling is when we want to control the memory of our data
        // between unmanaged and managed contexts.
        // Managed being GC code and unmanaged being non-GC code.

        // This fits in nicely with how unsafe works, it allows us to 
        // work with our memory in an environment that not only may not be using the GC,
        // but also has many safety checks turned off, and allows us to directly
        // touch memory addresses.

        // Unsafe will NOT make magically make slow code fast, it is more
        // for use in circumstances where we don't have any other choice
        // but to use raw pointers and we need to avoid using the GC and 
        // bounds checking for whatever reason.
    }

    [Fact]
    public void PalindromeCheck()
    {
        // A managed string.
        const string palindrome = "racecar";
        var length = palindrome.Length - 1;

        // Now we have an unmanaged pointer.
        var pointer = Marshal.StringToHGlobalAuto(palindrome);

        // Pass a string that hasn't been assigned yet.
        var otherPointer = Marshal.StringToHGlobalAuto("Other");

        // Move the pointer to some unmanaged context.
        unsafe
        {
            // We are passing a byte array to the FFI, this might look like char* in other languages like C and C++.
            MarshalExample.ForeignUnmanagedFunction((byte*) pointer, (uint) length);
            // Get a string back from the pointer.
            var managedName = Marshal.PtrToStringAuto(pointer);

            Assert.Equal(managedName, palindrome);
        }
    }
}