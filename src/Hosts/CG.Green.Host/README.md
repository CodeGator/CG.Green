
### CG.Green.Host - README

This project contains a server-side Blazor host the **CG.Green** microservice.

#### Notes

* Administrative UI is located in the CG.Green.Admin project. See the README file in that project for details.

* Identity UI is located in the CG.Green.Identity project. See the README file in that project for details.

* The REST API is located in the CG.Green.Controllers project. See the README file in that project for details.

* The core business logic is located in the CG.Green prject. See the README file in that project for details.

* The core abstractions are located in the CG.Green.Abstractions project. See the README file in that project for details.

* The core primitives are located in the CG.Green.Primitives project. See the README file in that project for details.

* The data access logic is located in the CG.Green.Data project. See the README file in that project for details.

* The migrations are located in the provider projects (CG.Green.Data.Sqlite, CG.Green.Data.SqlServer, etc). See the README file in those projects for details.

* Because all the EFCORE logic has been refactored out of the main application, this project no longer supports Visual Studio scaffolding. If scaffolding is thing you simply MUST do, then stand up a temporary solution, in Visual Studio, perform the scaffolding, then copy bits of code you're interested in, into this solution.



