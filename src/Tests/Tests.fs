module SpanTests

open System
open Xunit

[<Fact>]
let ``A span is stack allocated and is a handle to a block of memory.`` () =
    let items: Array
    items[0] <- 1
    let spanOverArray = Span<int>(items)
    Assert.Equal(spanOverArray, items)
