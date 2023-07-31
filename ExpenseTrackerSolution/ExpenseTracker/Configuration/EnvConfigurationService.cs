using System.Text.Json;

namespace ExpenseTracker.Configuration
{
    public class EnvConfigurationService
    {
        public EnvConfiguration Config { get; }
        public EnvConfigurationService() { 

            Config = GetConfigurations();
        }
        private EnvConfiguration GetConfigurations()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string envJason = Path.Combine(strWorkPath, "Configuration", "env.json");

            using (var jsonFileReader = File.OpenText(envJason))
            {
                return JsonSerializer.Deserialize<EnvConfiguration>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
    }
}
