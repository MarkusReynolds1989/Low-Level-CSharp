using System.Runtime.InteropServices;

namespace PlatformInvokeWindow;

// Use this website for finding out all the different libraries that can be used to platform invoke.
// https://www.pinvoke.net/

internal static class Program
{
    [DllImport("user32.dll")]
    private static extern int MessageBox(IntPtr handle, string text, string caption, uint type);


    private static int Main()
    {
        var message = MessageBox(IntPtr.Zero,
                                 "The button corresponds to a certain code which we can use to do another action.",
                                 "This is a native window.",
                                 0x00000000);
        switch (message)
        {
            case 1:
                var secondMessage = MessageBox(IntPtr.Zero, "Another pop up.", "Another native window.", 0x00000000);
                break;
        }

        return message;
    }
}