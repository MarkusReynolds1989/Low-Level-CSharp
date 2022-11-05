using System.Runtime.InteropServices;
using System.Text;

namespace TopicExamples;

public static unsafe class NativeImportDataTypes
{
    // As an example of data types, here we are using string builder as a byte pointer like we would in an
    // unmanaged language.
    // We don't have to use it though, we can use any type that is similar.
    [DllImport("user32.dll")]
    public static extern int CharLowerBuff([In, Out] StringBuilder input, uint size);

    [DllImport("user32.dll")]
    public static extern int CharLowerBuff(byte* input, uint size);
}