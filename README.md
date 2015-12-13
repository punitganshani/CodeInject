CodeInject
==========

[![Build status](https://ci.appveyor.com/api/projects/status/vupse9cynn2e8q5s?svg=true)](https://ci.appveyor.com/project/punitganshani/codeinject)

CodeInject - Code Inject and Runtime Intelligence

CInject (or CodeInject) allows code injection into any managed assembly without disassembling and recompiling it. It eases the inevitable task of injecting any code in single or multiple methods in one or many assemblies to intercept code for almost any purpose.

When using CInject, you do not require any knowledge of the target application.  You can create your own injectors very easily and inject them in any target assembly/executable.  An example is provided with this application.  If you have an existing code which has no logging or no diagnostics, inject it with CInject and execute it as usual to generate log files. **Helps you analyze any code and ease your reverse engineering exercise.**

Provides **runtime intelligence** such as 

- Values of arguments to the called function
- Object life time of a method / variables called within the method
- Allows customization of logging or diagnostics
- Allows extension of injectors to tailor your own solution as the need be
- Measure the method execution time to check performance

Build your own **plugins** using CInject information

* CInject supports building your own plugins
* The plugin receives information from the application such as
   * Target assembly & method
   * Injector assembly & method
   * Processing details, results with timestamp
   * Exceptions and errors
* Customized Plugin menu in CInject application

## FAQ

**Can I use it in my organization?**
Yes, you can use it.

**I have some doubts, how can I ask?**
Please use the 'Issues' tab

**Do I get to know the value of arguments at runtime?**
Yes, that's the actual beauty of CInject. It lets you know the value of arguments to a function at runtime.

**Can I inject static methods?**
Yes. In the current release, you can call create a class that implements ICInject interface and call static methods.  In later release, provision for calling static methods directly will be added as well.

**What version of .NET is this built on? Can I use it with other versions?**
CInject is built on .NET 4.0 runtime.  To use it with other versions, rebuild the Injectors, or create your own using .NET 2.0, 3.0, 3.5.

**How do I distribute my plugin?**
You have many alternatives to distribute the plugin

- Host the source code on CodePlex and link it with this project 
- Provide the source code, and get it hosted on this webpage & www.ganshani.com website
- Host the source code on GitHub, or other repository websites and provide us the link for promotion purposes

**How about code injection at compile time?**
If you are looking at dynamic injection based on the code you have written in an assembly, it would be worth while trying [dI.Hook](https://github.com/punitganshani/dI.Hook)  You can define the injections (or called hooks) in configuration file and invoke them wherever required. 
