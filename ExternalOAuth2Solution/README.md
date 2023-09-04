#Authentication System [3 h]

	- Project Template: Blazor WebAssembly
		- Authentication type: Individual Accouny
		- ASP.Net Core Hosted
	- DataBase: MS Sql Server
	- Source: https://www.youtube.com/watch?v=r3tytnzCuNw
	- Source code: https://github.com/patrickgod/BlazorGoogleAuthTutorial


##Nuget Packages

	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
	- Microsoft.AspNetCore.Identity.UI
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.VisualStudio.Web.CodeGeneration.Design
	- Microsoft.VisualStudio.Web.CodeGeneration.Utils
	- Microsoft.AspNetCore.Authentication.Google

##Instructions

	- Create Google API Key (As indicates on documentation https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-7.0)
	- On Program.cs Modyfy: builder.Services.AddAuthentication().AddIdentityServerJwt(); to builder.Services.AddAuthentication().AddIdentityServerJwt().AddGoogle(<you have to put the options>);
	- Modify Auth model
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database (for data base creation)

##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json" (For Server)
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
