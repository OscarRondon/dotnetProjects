# eCommercer Blazor solution

This is an eCommerce ASP.NET Blazor Web Assembly app

## Project
-----------------------------------------------------------------------------------------------------------------------------------------
```
	- Project Template [3 projects]: 
			- ClassLibrary (eCommerceShared)
			- ASP.Net Core Web API (eCommerceServer)
			- Blazor Web Assembly Stand Alone (eCommerceClient)
			
	- DataBase: MS SQL Server
```

## Server side App (eCommerceServer)

### Configuration
```
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content 
		- Copy to Output directory: Copy always
	- Register the new file to de app configuration in Program.cs
	- Make sure to exclude "env.json" file form the source control
```
### Nuget Packages
```
	- Swashbuckle.AspNetCore
	- Swashbuckle.AspNetCore.SwaggerUI
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.AspNetCore.Authentication.JwtBearer
	- Stripe.net
```
### Instructions
```
	- On Program.cs > builder.Services.AddDbContext (server)
	- Install or update Entity framework command line tools
		- run in Nuget Manager Console: dotnet ef
		- run in Nuget Manager Console: dotnet tool uninstall --global dotnet-ef
		- run in Nuget Manager Console: dotnet tool install --global dotnet-ef
	- Run in Nuget Manager Console (create migration): 
		- Place on server directory
		- run > dotnet ef migrations add "Initial Create"
		- run > dotnet ef database update
```

## Client side App (eCommerceClient)

### Configuration
```
	- Create a file "appsettings.json" into wwwroot folder
	- Create a Folder into de Project "Settings"
	- Create class for settings "ClientAppSettings"
	- Add the following into Program.cs
		var settings = new ClientAppSettings();
		builder.Configuration.Bind(settings);
		builder.Services.AddSingleton(settings);
```
### Nuget Packages
```
	- Blazored.LocalStorage (Package to work with local storage)
	- Microsoft.AspNetCore.Components.Authorization
	- Microsoft.AspNetCore.WebUtilities
	- MudBlazor
	- MudBlazor.ThemeManager
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
	*	Show Image Carousel on the product details page (client)
]


## Todos

- session expiration control and security
- Adjust auto logout when app close