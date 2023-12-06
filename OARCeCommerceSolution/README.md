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
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database (for data base creation)
```
### Configuration
```
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
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
	* Add Product Controller (server)
	* Call the web api for Products (client)
	* Add SwaggerUI to (server)
	* Add .en file (server)
	* Add EF (server)
	* Add DataContext (server)
	* Register the DBContext & configure SQLServer
]