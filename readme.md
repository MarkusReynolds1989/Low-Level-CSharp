# Low Level Programming in C#

## Installation Guide

To run this code you will need:

* .Net 7 SDK/Runtime
* Rustup, with the version of Rust being any that is "Nightly". This allows us to use some unsafe nightly features such
  as C strings.
* Visual Studio or VSCode
* You will see other DLLs included in the code, they are already compiled C and D code that is just there for demonstration purposes. You do not need a C or D compiler.

To run the code you will need to publish the projects and then make sure the DLL you want to use is inside.   
Most every project is set up to use native AOT so you can checkout how much smaller and faster the applications run.   
In conjuction with lower level languages, you can have really fast programs.

## Reasons to Care about DLL importing and Foreign Function Interfaces
* In cases where you need to test a legacy C program. You can import the DLLs from C and use C# as the testing harness.   
C# has amazing testing frameworks that work really well so calling those native C functions as unit tests will be much easier.
* You want to use a native library, but don't want to write your code in a lower level langauge.
* You've identified a function in your managed code that you can't seem to make any faster in C# and you know it will run faster in a language like Rust.   
LLVM generated code is highly optimized but make sure to profile your C# code vs the native code heavily. Remember there can be a performance hit for importing DLLs.


## Reasons to Use lower level C# Code
* The new Span and Memory classes are very performant and avoid unneeded allocations.
* Passing raw pointers and using unsafe code *could* increase performance and decreased memory allocations (always profile this, is it worth the safety trade offs? Is there another options?)
* Avoinding the GC is great for applications that much not have GC pauses. For instance, real time trading apps, games.
* Allocating on the stack instead of the heap can also increase performance in some cases.
* Manually managing streams of bytes can help to increase performance on applications that are heavily IO bound.



