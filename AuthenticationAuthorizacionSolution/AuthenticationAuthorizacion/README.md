#Authentication System [6 h]

	- Project Template: ASP.Net Core Web App
	- DataBase: 
	- Source: https://www.youtube.com/watch?v=B0_gM-wBlmE&list=PLjrh5YRNPzdQMQSFaxP8XrouoVq4QDw2H&index=14&t=8s
	- Source2 [Roles]: https://www.google.com/search?q=asp.net+identity+manage+roles&sca_esv=563180349&sxsrf=AB5stBib8md7Fn6Y24GF6md3-pgGI10VvQ%3A1694033426659&ei=Eub4ZKLqJ-y75OUPmfKoiA8&ved=0ahUKEwiiu4il7paBAxXsHbkGHRk5CvEQ4dUDCA8&uact=5&oq=asp.net+identity+manage+roles&gs_lp=Egxnd3Mtd2l6LXNlcnAiHWFzcC5uZXQgaWRlbnRpdHkgbWFuYWdlIHJvbGVzMgYQABgWGB4yBhAAGBYYHjIGEAAYFhgeMggQABiKBRiGAzIIEAAYigUYhgMyCBAAGIoFGIYDSJsiUNMKWPkbcAF4AZABAJgBzwKgAdIZqgEGMi0xMC4yuAEDyAEA-AEBwgIKEAAYRxjWBBiwA8ICCBAAGIoFGJECwgIKEAAYgAQYFBiHAsICBRAAGIAEwgIGEAAYHhgNwgIIEAAYCBgeGA3iAwQYACBBiAYBkAYI&sclient=gws-wiz-serp#fpstate=ive&vld=cid:ecbd6b0d,vid:Y6DCP-yH-9Q


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
