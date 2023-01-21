
### CG.Green.Identity - README

This project contains ASP.NET / Duende specific identity UI and support logic, for the **CG.Green** microservice.

#### Why is this code here, and not in the Blazor host?

One of the goals of this solution is to create a microservice with a non-monolithic architecture. What I mean by that is, to create a project where everything isn't crammed into a single server-side Blazor project. Towards that end, I have factored out the identity stuff into this specific assembly. 

The end result is, hopefully, a less cluttered, more easily understandable, and more maintainable solution.

#### Notes

* All the scaffolding that one might normally do, to override one or more identity UI pages, has already been done. The results are razor pages in the `Areas` folder. Further scaffolding isn't required and wouldn't work anyway because Microsoft Visual Studio expects everything to physically live in a single exe project, and I chose not to take that approach. Feel free to modify the code but understand that scaffolding in Visual Studio is no longer an option.
