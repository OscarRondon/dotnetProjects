# eCommercer Blazor solution

This is an eCommerce ASP.NET Blazor Web Assembly app

## Project
-----------------------------------------------------------------------------------------------------------------------------------------
```
	- Project Template: ASP.NET Blazor Web Assembly app -> ASP.Net core hosted
	- DataBase: MS SQL Server
```

### Nuget Packages Server
```
	- Swashbuckle.AspNetCore
	- Swashbuckle.AspNetCore.SwaggerUI
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.SqlServer

```
### Instructions
```
	- On Program.cs > builder.Services.AddDbContext
	- On Program.cs > builder.Services.AddIdentity<IdentityUser,  IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
	- On Program.cs > app.UseAuthentication();
	- Right click on project > Add > New Scaffolded Item > Identity (choose the options of the presented form)
	- Add config files
	- Install or update Entity framework command line tools
		- run in Nuget Manager Console: dotnet ef
		- run in Nuget Manager Console: dotnet tool uninstall --global dotnet-ef
		- run in Nuget Manager Console: dotnet tool install --global dotnet-ef
	- Run in Nuget Manager Console (create migration): 
		- Place on server directory
		- run > dotnet ef migrations add "Initial Create"
		- run > dotnet ef database update

```
### Configuration
```
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content 
		- Copy to Output directory: Copy always
	- Register the new file to de app configuration in Program.cs
	- Make sure to exclude "env.json" file form the source control
```


## Testing [14]

### Nuget Packages
```
	- Moq
	- MockQueryable.Moq
	- Microsoft.EntityFrameworkCore

	// - Microsoft.EntityFrameworkCore.InMemory
```

OARCeCommerceSolution
[
	* Add Initial create migration for DB
	* Get the products form de DB
	* Add Generic Service Response
	* Add ProducService to Server
	* Add ProducService to Client
]