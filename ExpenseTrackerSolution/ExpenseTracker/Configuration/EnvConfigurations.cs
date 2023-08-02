using System.Text.Json;

namespace ExpenseTracker.Configuration
{
    public class EnvConfigurations
    {
        public string dbServer { get; }
        public string dbName { get; }
        public string dbUser { get; }
        public string dbPwd { get; }


        public EnvConfigurations(IConfiguration configuration) 
        {
            dbServer = configuration.GetSection("DataBase:DBServer").Value ?? "";
            dbName = configuration.GetSection("DataBase:DBName").Value ?? "";
            dbUser = configuration.GetSection("DataBase:DBUser").Value ?? "";
            dbPwd = configuration.GetSection("DataBase:DBPwd").Value ?? "";
        }

        // Read File from a directory

        //private EnvConfiguration GetConfigurations()
        //{
        //    string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //    string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
        //    string envJason = Path.Combine(strWorkPath, "env.json");

        //    using (var jsonFileReader = File.OpenText(envJason))
        //    {
        //        return JsonSerializer.Deserialize<EnvConfiguration>(jsonFileReader.ReadToEnd(),
        //            new JsonSerializerOptions
        //            {
        //                PropertyNameCaseInsensitive = true
        //            });
        //    }
        //}
    }
}
