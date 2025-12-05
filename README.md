# Transactions (ConsoleApp1)

Requirements
- .NET 8 SDK installed (download from https://dotnet.microsoft.com).
- (Optional) __Visual Studio 2022__ with the .NET desktop workload for IDE usage.

How to run (CLI)
1. Open a terminal at the repository root.
2. Run the console app: 
   - cd ConsoleApp1
   - dotnet run
   The program prints `42` and waits for input (Console.ReadLine) to keep the window open.

How to run (Visual Studio)
1. Open the solution via __File > Open > Project/Solution__.
2. In __Solution Explorer__, right-click  __Set as Startup Project__.
3. Use __Debug > Start Debugging__ or __Debug > Start Without Debugging__ to run.

How to run tests
- From the repo root run: dotnet test
- Or open __Test Explorer__ in __Visual Studio 2022__ and click __Run All Tests__.

This assignment could be improved by adding a black box testing, by giving these specifications, the graders should have some test cases
that should pass even though they aren't explicitly put into the assignment. Alternatively, there can just be a requirement to
include tests into the assignment to show proof that it is working especially since we are allowed to use whatever language we want.
To make it even easier we could just show a small video recording on the program working so the graders don't need to install anything
