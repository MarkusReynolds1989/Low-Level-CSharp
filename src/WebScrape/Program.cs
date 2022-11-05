using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace WebScrape;

internal static class Program
{
    private const int FactOffset = 9;

    // Can't use a ref struct here, because the main method is async.
    private struct CatFact
    {
        public Memory<byte> Fact;
        public int          Length;

        public CatFact(Memory<byte> fact, int length)
        {
            Fact = fact;
            Length = length;
        }
    }

    private static async Task LoadResultBuffer(Memory<byte> resultBuffer)
    {
        const string endPoint = "https://catfact.ninja/fact";
        using var client = new HttpClient();
        var response = new Memory<byte>(await client.GetByteArrayAsync(endPoint));
        response.CopyTo(resultBuffer);
    }

    private static Memory<byte> ExtractFact(Memory<byte> input, int length) =>
        // We will slice into the memory and return the rest, which shouldn't allocate.
        input[FactOffset..(length + FactOffset)];

    private static async Task Main(string[] args)
    {
        // By using Memory we are saving ourselves from extra allocations. 
        // This is just a small example, imagine if we instead got a huge response.
        // We could slice into any indexes of the Memory block as spans without extra allocations, as we would if this was
        // just a byte array.
        var resultBuffer = new Memory<byte>(new byte[2048]);
        var lengthRegex = new Regex(@"length"":(?'length'\d+)");
        await LoadResultBuffer(resultBuffer);

        // There's already a lot of extra allocations here, but this is kind of a contrived example anyway.
        // Really, we wouldn't be polling a public api that is returning a JSON string, we'd be polling
        // some place for bytes (i.e. we set up our api to send bytes for efficiency/space.
        var lengthMatch = lengthRegex.Match(Encoding.UTF8.GetString(resultBuffer.Span));
        var length = int.Parse(lengthMatch.Groups["length"].Value);

        var catFact = new CatFact(ExtractFact(resultBuffer, length), length);
        Console.WriteLine(catFact.Length);
        Console.WriteLine(Encoding.UTF8.GetString(catFact.Fact.Span));
    }
}