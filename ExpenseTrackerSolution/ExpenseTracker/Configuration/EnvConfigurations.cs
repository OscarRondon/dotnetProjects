using System.Text.Json;

namespace ExpenseTracker.Configuration
{
    public class EnvConfigurations
    {
        public string DevConnection { get; }

        public EnvConfigurations(IConfiguration configuration) 
        {
            DevConnection = configuration.GetSection("ConnectionStrings:DevConnection").Value;
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
