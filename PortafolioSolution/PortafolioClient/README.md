# Portafolio Blazor solution

This is an Portafolio application in ASP.NET Blazor

Template Name: iPortfolio
Template URL: https://bootstrapmade.com/iportfolio-bootstrap-portfolio-websites-template/
Author: BootstrapMade.com
License: https://bootstrapmade.com/license/


## Project
-----------------------------------------------------------------------------------------------------------------------------------------
```
	- Project Template [1 projects]: 
			- Blazor Web Assembly Stand Alone [PWA] (PortafolioClient)
	- Databases: N/A
```

## Client side App (PortafolioClient)

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
	- Microsoft.AspNetCore.WebUtilities
	- MudBlazor
	- MudBlazor.ThemeManager
```

### 3rd party projects
```
	- BlazorTypewriter (https://github.com/ormesam/blazor-typewriter)
```

PortafolioSolution
[
	*	Working on Project list JSON file
	*	Working on read and load json file
]

## Todos
