# Sample Project
## Introduction
The original example has been taken from [Pluralsight Course](https://app.pluralsight.com/library/courses/refactoring-anemic-domain-model/table-of-contents). And this has been modified to introduce 

 - [__Onion Architecture__](http://blog.thedigitalgroup.com/chetanv/2015/07/06/understanding-onion-architecture/)
 - [Swagger API](https://swagger.io/getting-started/)
 - [Serilog](1) (for logging purpose)
 - [Microsoft.Extensions.Options](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options?view=aspnetcore-2.0)
 - _.NetCore2.0_ and 
 -_Entity Framework core_ instead of NHibernate and Hilo

[1]: https://github.com/serilog/serilog-aspnetcore
 ___
## Setup
### Database setup

 - Build the project
 - Open a command prompt
 - Go to the Template.DAL folder
 - Execute ` dotnet ef database update `

## Develpoment Tips

### "Migration commands" 

To create a migration script
` dotnet ef migrations add Initial `

To execute the script
` dotnet ef database update Initial `

To remove the latest migration script
` dotnet ef migrations remove `

- here _Inital_ is the name of migration script 


## Helpful links


### When to use TryAddSingleton or AddSingleton?

> The difference between TryAddSingleton and AddSingleton is that AddSingleton always appends the registration to the collection, while TryAddSingleton only does this when there exists no registration for the given service type.

[When to use tryaddsingleton](https://stackoverflow.com/questions/48185894/when-to-use-tryaddsingleton-or-addsingleton)

