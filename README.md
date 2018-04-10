"Migration commands" 

To create a migration script
` dotnet ef migrations add Initial `

To execute the script
` dotnet ef database update Initial `

To remove the latest migration script
` dotnet ef migrations remove `

- here _Inital_ is the name of migration script 