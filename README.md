# Sample Project
## Introduction
The original example has been taken from [Pluralsight Course](https://app.pluralsight.com/library/courses/refactoring-anemic-domain-model/table-of-contents). And this has been modified to introduce 

 - [__Onion Architecture__](http://blog.thedigitalgroup.com/chetanv/2015/07/06/understanding-onion-architecture/)
 - [Swagger API](https://swagger.io/getting-started/)
 - [Serilog](https://github.com/serilog/serilog-aspnetcore) (for logging purpose)
 - [Microsoft.Extensions.Options](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.options?view=aspnetcore-2.0)
 - _.NetCore2.0_ and 
 -_Entity Framework core_ instead of NHibernate and Hilo

 ___

## Setup
### Database setup

 - Build the project
 - Open a command prompt
 - Go to the Template.DAL folder
 - Execute ` dotnet ef database update `

### Web
Set Template.Api as your startup project and run on kestrel. 

> __Note:__ 
At the moment, [an issue is raised for Edge](https://developer.microsoft.com/en-us/microsoft-edge/platform/issues/9370062/)
Please use _chrome_ or others to run the swagger

---

## Develpoment Tips

### "Migration commands" 

To create a migration script
` dotnet ef migrations add Initial `

To execute the script
` dotnet ef database update `

To remove the latest migration script
` dotnet ef migrations remove `

To drop the database
` dotnet ef database drop `

- here _Inital_ is the name of migration script 


## Helpful links


### When to use TryAddSingleton or AddSingleton?

> The difference between TryAddSingleton and AddSingleton is that AddSingleton always appends the registration to the collection, while TryAddSingleton only does this when there exists no registration for the given service type.

[When to use tryaddsingleton](https://stackoverflow.com/questions/48185894/when-to-use-tryaddsingleton-or-addsingleton)

