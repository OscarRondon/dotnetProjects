﻿# eCommerceTickets [WebSite](https://ecommercetickets.azurewebsites.net)

This is the demo of an eCommerce ASP.NET MVC 5 application that you are going to build step by step starting with an empty project in Visual Studio.
This application is an eCommerce application used to buy movies online from different cinemas. 
You will be able to add items to your card, pay using PayPal and also log in as an administrator to add new cinemas, actors, producers, and movies. 

## Project [62]
-----------------------------------------------------------------------------------------------------------------------------------------
```
	- Project Template: ASP.Net Core Web App (Model - View - Controller)
	- DataBase: MS SQL Server Express
```
### Nuget Packages
```
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
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