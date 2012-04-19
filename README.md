# Grasp

A runtime in which developers can define, compile, and execute calculations against arbitrary data schemas

# When Would You Use It?

You have a project whose problem domain centers around analysis: generating data by examining existing data. A survey system is a common example, as it collects values from users and performs different kinds of calculations on the answers:

* How many people filled out the entire survey?
* What is the average value of question 7?
* What percentage of the survey did Bob complete?
* Did Mary answer all required questions?
* What is the total of Joe's answers on page 3?

The results of asking each of these questions becomes a new data point available to future rounds of analysis. This allows a very fine-grained and iterative approach to describing relationships within a set of data.

Thanks to its declarative and compositional nature, Grasp works very well at the core of dynamic systems. Let's say your project involves letting users create _their own_ surveys, complete with calculations and validation; your application, then, doesn't define a survey itself but instead serves as a middleman between its users and Grasp. This neatly sidesteps the problem of inventing a data definition and execution engine (as usually happens in this scenario) and instead allows you to focus on building features for your users.

Surveys are just one example of a form structure; Grasp targets any system which asks questions about data. See the Articles section below for more examples.

## NuGet

### Get it in the [package manager console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console):

    PM> Install-Package Grasp

### Or from the online gallery:
https://nuget.org/packages/Grasp

## Articles

* [Part 1: Overview](http://www.executableintent.com/grasp-a-net-analysis-engine-part-1-overview/)
* [Part 2: Variables](http://www.executableintent.com/grasp-a-net-analysis-engine-part-2-variables/)
* [Part 3: Calculations](http://www.executableintent.com/grasp-a-net-analysis-engine-part-3-calculations/)
* [Part 4: Runtime](http://www.executableintent.com/grasp-a-net-analysis-engine-part-4-runtime/)
* [Part 5: Executable](http://www.executableintent.com/grasp-a-net-analysis-engine-part-5-executable/)
* [Part 6: Validating Calculations](http://www.executableintent.com/grasp-a-net-analysis-engine-part-6-validating-calculations/)
* [Part 7: Compiling Calculations](http://www.executableintent.com/grasp-a-net-analysis-engine-part-7-compiling-calculations/)
* [Part 8: Calculation Dependencies](http://www.executableintent.com/grasp-a-net-analysis-engine-part-8-calculation-dependencies/)
* [Part 9: Dependency Sorting](http://www.executableintent.com/grasp-a-net-analysis-engine-part-9-dependency-sorting/)

# Working with the Code

### Code Contracts

You will need the Code Contracts tooling to build the solution correctly. The method calls in the `System.Diagnostics.Contracts` namespace are included in .NET 4; this simply allows Visual Studio to rewrite the assembly after it compiles:

http://msdn.microsoft.com/en-us/devlabs/dd491992

### Tests

All tests are written using [NUnit 2.5.10.11092](http://nuget.org/packages/NUnit/2.5.10.11092). In order to run them, you will need a test runner that supports NUnit 2.5. I recommend the excellent [TestDriven.NET](http://www.testdriven.net/). 

## License
[MIT License](https://github.com/bwatts/Grasp/blob/master/Grasp-License.txt)

# Copyright

Copyright &copy; 2012 Bryan Watts