using System.Text;

namespace WebScrape;

internal static class Program
{
    private ref struct CatFact
    {
        public Memory<byte> Fact;
    }

    private struct CatFactReg
    {
        public Memory<byte> Fact;
    }

    private static async Task LoadResultBuffer(Memory<byte> resultBuffer)
    {
        const string endPoint = "https://catfact.ninja/fact";
        using var client = new HttpClient();
        var response = new Memory<byte>(await client.GetByteArrayAsync(endPoint));
        response.CopyTo(resultBuffer);
    }

    private static async Task Main(string[] args)
    {
        // By using Memory we are saving ourselves from extra allocations. 
        // This is just a small example, imagine if we instead got a huge response.
        // We could slice into any indexes of the Memory block as spans without extra allocations, as we would if this was
        // just a byte array.
        var resultBuffer = new Memory<byte>(new byte[1024]);

        await LoadResultBuffer(resultBuffer);

        // Suppose we only want the first 10 characters.
        var length = Encoding.Default.GetString(resultBuffer.Span[^4..^1]);
        Console.WriteLine(length);
        Console.WriteLine(Encoding.Default.GetString(resultBuffer.Span[9..]));

        // Ref structs cannot be used in async contexts because they are guaranteed to stay on the stack.
        /*var cat = new CatFact
        {
            Fact = resultBuffer,
        }*/

        // A regular struct can go on the heap so this works correctly.
        var cat = new CatFactReg
        {
            Fact = resultBuffer
        };
    }
}