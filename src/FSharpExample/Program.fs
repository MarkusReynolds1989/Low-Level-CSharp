open System

// Without the overhead of JIT compilation, the execution speed will be very fast.
// This is only because it doesn't need to start up the VM at the beginning and the code is native.

let rec count (list: List<'t>) =
    match list with
    | [] -> 0
    | _ -> 1 + count list.Tail

[<EntryPoint>]
let main _args =
    // We can use span in F# as well.
    let x = Span<int>([| 1; 2; 3; 4 |])

    // Or memory
    let _y = Memory<int>([| 1; 2; 3; 4 |])

    for i in x do
        Console.WriteLine i

    let countThis = [ 1; 2; 3; 4; 5 ]
    let countThat = [ 1.0; 2.0; 3.0; 4.0; 5.0 ]

    let result = count countThis
    let resultTwo = count countThat

    let sum = List.sum countThat

    Console.WriteLine result
    Console.WriteLine resultTwo
    0
