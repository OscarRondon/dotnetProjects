#Authentication System [16 h]

	- Project Template: ASP.Net Core Web API
	- Source: https://www.youtube.com/watch?v=JG2TeGBs8MUs
	- Source code: https://github.com/medhatelmasry/StudentsApi 


##Nuget Packages

	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.EntityFrameworkCore.SQLite
	- Microsoft.EntityFrameworkCore.SQLite.Core
	- Microsoft.AspNetCore.JsonPatch
	- Microsoft.AspNetCore.Mvc.NewtonsoftJson
	- Swashbuckle.AspNetCore.Newtonsoft
	- CsvHelper (package for .csv file reading)

##Instructions

	- Run in Nuget Manager Console: dotnet tool install --global dotnet-ef (just once in the computer, you can ignore this step if you already did it)
	- Run in Nuget Manager Console: dotnet tool update --global dotnet-ef
	- Run in Nuget Manager Console: dotnet ef migrations add Initial -o Data/Migrations (you have to ensure that you are inside the application directory)
	- Run in Nuget Manager Console: dotnet ef database update
	- In project Properties > build > Documentation File > chek Generate a file conteining API Documentation
	- In Program.cs modify builder.Services.AddControllers() to builder.Services.AddControllers().AddNewtonsoftJson(); < for patch operations


##Configuration

	- Create a file "env.json" into root forlder, using "env.template.json"
	- Set the "env.json" properties as:
		- Build action: Content
		- Copy to Output directory: Copy always
	- Make sure to exclude "env.json" file form the source control
	- Add to Program.cs > builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

##Notes

	- On VisualStudio main menu > Edit > Paste Special > JSON as Classes
