using System.Runtime.InteropServices;

namespace PlatformInvoke;

// Use this website for finding out all the different libraries that can be used to platform invoke.
// https://www.pinvoke.net/

internal static partial class Program
{
    [DllImport("user32.dll")]
    private static extern int MessageBox(IntPtr handle, string text, string caption, uint type);

    /*[LibraryImport("shell32.dll")]
    private static partial IntPtr ShellExecute(IntPtr handle,
                                               [MarshalAs(UnmanagedType.LPTStr)] string operation,
                                               [MarshalAs(UnmanagedType.LPTStr)] string file,
                                               [MarshalAs(UnmanagedType.LPTStr)] string parameters,
                                               [MarshalAs(UnmanagedType.LPTStr)] string directory,
                                               int showCommand);*/

    [DllImport("shell32.dll")]
    private static extern IntPtr ShellExecute(IntPtr handle,
                                              string operation,
                                              string file,
                                              string parameters,
                                              string directory,
                                              int showCommand);

    private static int Main()
    {
        var result = ShellExecute(IntPtr.Zero, "echo", null, "Help", null, 1);
        var message = MessageBox(IntPtr.Zero, "The button corresponds to a certain code.", "This is a native window.",
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