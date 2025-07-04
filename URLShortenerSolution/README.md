# URL Shortener

## Solution

* Project Template [3 projects]: 
	* ASP.Net Core Web app (MVC) > Shortly.Client
	* ASP.Net Core Web API > Shortly.Redirect
	* .Net Core Class Library > Shortly.Data

* DataBases
	* MS SQL Server (loacal)


## Server side App (ASP.Net Core Web API)

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


## Client side App (ASP.Net Core Web app (MVC))

### Configuration
```
```

### Nuget Packages
```
	- AutoMapper
	- Microsoft.AspNetCore.Identity.EntityFrameworkCore

```

### Instuctions
```
	- Add the Shortly.Data project as a reference
	- On Program.cs > builder.Services.AddDbContext (server)
	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content 
		- Copy to Output directory: Copy always
	- Register the new file to de app configuration in Program.cs
	- Make sure to exclude "env.json" file form the source control
	- Add builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); to Program.cs
	- Add Authentication
		1. Add Identity service: builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();
		2. Configure the application cookie:
			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
				options.LoginPath = "Authentication/Login";
				/*
				options.LogoutPath = "Authentication/Logout";
				options.AccessDeniedPath = "Authentication/AccessDenied";
				*/
				options.SlidingExpiration = true;
			});
		3. Add Authentication middleware: app.UseAuthentication(); (this line goes after app.UseRouting())
		4. Create DB migration fot Identity tables:
			- run > dotnet ef migrations add "Identity" [Add-Migration Identity]
			- run > dotnet ef database update [Update-Database]

```

## Data side App (.Net Core Class Library)

### Configuration
```
	
```

### Nuget Packages
```
	- Microsoft.EntityFrameworkCore (base packege) with all EF core functionality)
	- Microsoft.EntityFrameworkCore.SqlServer (SQL Server provider)
	- Microsoft.EntityFrameworkCore.Tools (command line tools for migrations)
	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

### Instructions
```
	- Install or update Entity framework command line tools
		- run in Nuget Manager Console: dotnet ef
		- run in Nuget Manager Console: dotnet tool uninstall --global dotnet-ef
		- run in Nuget Manager Console: dotnet tool install --global dotnet-ef
	- Run in Nuget Manager Console (create migration): 
		- run > dotnet ef migrations add "Initial Create"
		- run > dotnet ef database update
```

## Testing [14]

### Nuget Packages
```
```

URLShortenerSolution
[
	*	Create Database roles and Users Seeders
]


## Todos

- 