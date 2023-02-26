# CG.Green: 

[![Build Status](https://dev.azure.com/codegator/CG.Green/_apis/build/status/CodeGator.CG.Green?branchName=main)](https://dev.azure.com/codegator/CG.Green/_build/latest?definitionId=105&branchName=main)
[![Board Status](https://dev.azure.com/codegator/796f6869-3c91-495d-b4dd-16562d48d319/4b6e700e-dd5c-440a-9c8e-d63aebfc018e/_apis/work/boardbadge/f6d51148-5732-41b6-a699-2b0fdb281c56)](https://dev.azure.com/codegator/796f6869-3c91-495d-b4dd-16562d48d319/_boards/board/t/4b6e700e-dd5c-440a-9c8e-d63aebfc018e/Issues/)
![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/codegator/CG.Green/105)
[![Github discussion](https://img.shields.io/badge/Discussion-online-green)](https://github.com/CodeGator/CG.Green/discussions)
[![Github docs](https://img.shields.io/static/v1?label=Documentation&message=online&color=blue)](https://codegator.github.io/CG.Green/index.html)

### What does it do?

Green is an idea for an identity microservice. Essentially, we stood up the ASP.NET and Duende libraries as a Blazor microservice, then we added some additional administrative and nice-to-have debugging features.

Our main goal is to produce a tool that can be dropped into production, cheaply and easily, to provide secure, reliable identity services - without costing a fortune to operate or requiring a PHD to understand. 

We chose to build on Duende and ASP.NET because they are proven technologies and offer some of the most flexibility, along with the lowest overall cost of ownership.

### What works, at this point?

The ASP.NET and Duende portions are hosted and working perfectly. 

The admin UI is coming along, slowly. This piece alone is a hefty undertaking, so it will take time to finish.

The identity UI is functional, but generally needs some love and attention.

### What does the roadmap look like?

We're not at a point where we can commit to a feature plan. Honestly, we might not ever be. Still, keep checking back. You never know.

### What are the main dependencies?

One of the main goals of this project is to minimize dependencies on third party libraries. Where third party libraries are used, we try to ensure that they are well established and actively developed / supported. 

* [Duende](https://duendesoftware.com/) (The library formerly known as IdentityServer4)

* [Microsoft Identity UI](https://github.com/dotnet/aspnetcore)

* [MudBlazor](https://mudblazor.com/)

* [Entity Framework Core](https://github.com/dotnet/efcore)

* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

### What OS(s) does it support?

A goal of the project is to produce a tool that can be run on Windows and Linux. Thanks to .NET core, we operate on the following operating systems:

* [Windows](https://en.wikipedia.org/wiki/Microsoft_Windows) (tested)

* [Linux](https://en.wikipedia.org/wiki/Linux) (not fully tested)

* [macOS](https://en.wikipedia.org/wiki/MacOS) (theoretical, not tested)

### What .NET runtime(s) does it support?

We are building the project against [.NET 7.x](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

### What database(s) does it use?

A goal of the project is to integrate with a variety of back-end databases. We currently operate with:

* [SqlServer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

* [Sqlite](https://www.sqlite.org/index.html)

* [MySql](https://mysql.com)

* InMemory (for dev/demo/testing purposes)

### What languages does the UI support?

* English

* Spanish (in progress)

### Is there any documentation?

There is developer documentation [HERE](https://codegator.github.io/CG.Green/) generated from XML comments.

There is a WIKI (in progress) [HERE](https://github.com/CodeGator/CG.Green/wiki)

We also write about projects like this one on our website [HERE](https://www.codegator.com)

### How do I contact you?

If you've spotted a bug in the code please use the project Issues [HERE](https://github.com/CodeGator/CG.Green/issues)

We also have a discussion group [HERE](https://github.com/CodeGator/CG.Green/discussions)

## Disclaimer

This project and it's contents are experimental in nature. There is no official support. Use at your own risk.