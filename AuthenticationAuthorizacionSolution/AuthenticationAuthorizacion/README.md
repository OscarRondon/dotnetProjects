#Authentication System [0.5 h]

	- Project Template: ASP.Net Core Web App
	- DataBase: 
	- Source: https://www.youtube.com/watch?v=B0_gM-wBlmE&list=PLjrh5YRNPzdQMQSFaxP8XrouoVq4QDw2H&index=14&t=8s


##Nuget Packages

	- Microsoft.AspNetCore.Identity.EntityFrameworkCore
	- Microsoft.AspNetCore.Identity.UI
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.VisualStudio.Web.CodeGeneration.Design
	- Microsoft.VisualStudio.Web.CodeGeneration.Utils	

##Instructions

	- On Program.cs > builder.Services.AddDbContext
	- On Program.cs > builder.Services.AddIdentity<IdentityUser,  IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
	- On Program.cs > app.UseAuthentication();
	- Right click on project > Add > New Scaffolded Item > Identity (choose the options of the presented form)
	- Add config files
	- Program.cs > Add Service for Razor Pages [builder.Services.AddRazorPages();]
	- Program.cs > Map Razor Pages [app.MapRazorPages();]
	- Modify Auth model
	- Run in Nuget Manager Console: Add-Migration "Initial Create"
	- Run in Nuget Manager Console: Update-Database (for data base creation)

##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
