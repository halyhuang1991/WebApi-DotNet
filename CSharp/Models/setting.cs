using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace CSharp.Models
{
    public class SettingOptions
    {
        public string ConnectionStrings { get; set; }
    }
    public class Setting
    {
        private readonly SettingOptions _Options;

        public Setting(IOptions<SettingOptions> Options)
        {
            _Options = Options.Value;
        }
        public static void GetSetting(){
            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            path=@"D:\C\github\WebApi-DotNet\CSharp\appsettings.json";
            var config = new ConfigurationBuilder().AddJsonFile(path).Build();
            string test=config["ConnectionStrings:SqlServerConnection"].ToString();
            string jsonString = File.ReadAllText(path, Encoding.Default);
            JObject jobject = JObject.Parse(jsonString);//解析成json
            test=jobject["ConnectionStrings"]["SqlServerConnection"].ToString();
        }
    }
}