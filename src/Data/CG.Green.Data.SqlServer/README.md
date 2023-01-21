
## CG.Green.Data.SqlServer - README

This project contains an EFCORE SQLServer provider for the **CG.Green** microservice.

### Notes

To add/remove/change EFCORE migrations follow these steps:
    
1. Set the CG.Green.Data.SqlServer project as the startup project, in Visual Studio.
2. Open the Package Manager Console window, in Visual Studio.
3. Use the normal add-migration commands, in the Package Manager Console window. Remember to use the -p CG.Green.Data.SqlServer argument, so the migrations will end up in the right project. Remember to use the -o Migrations/Whatever argument, so the migrations won't overwrite each other.

So here's an example command line for adding a migration in Visual Studio: 

```
add-migration MyMigration -p CG.Green.Data.SqlServer -o Migrations/Whatever
```
Note: this project actually has three data-contexts. Because of that, this procedure must be completed, three times. Use the -o flag to ensure that the resulting migrations are kept tidy and don't conflict with each other.

Remember to set the start project back to the CG.Green.Host project (or whatever project you normally start with), when you're done.

To deploy SQL for migrations in a production environment, use the following from the command line:

```
dotnet ef migrations script --idempotent
```

Or using the following command, in Visual Studio:

```
Script-Migration -Idempotent
```

<br />

Here are the commands used to create the initial migrations:

For the `GreenDbContext`
```
add-migration InitialCreate -p CG.Green.Data.SqlServer -o Migrations\Green -c GreenDbContext
```

For the `ConfigurationDbContext`
```
add-migration InitialCreate -p CG.Green.Data.SqlServer -o Migrations\Configuration -c ConfigurationDbContext
```

For the `PersistedGrantDbContext`
```
add-migration InitialCreate -p CG.Green.Data.SqlServer -o Migrations\Grants -c PersistedGrantDbContext
```

<br />

[Here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=vs) is a link with more suggestions for generating migrations.
