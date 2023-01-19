using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    public static class LCV {
        private const string defaultConfigPath = @"data\config.json";
        public static LCVConfig Config { get; private set; } = new();

        public static void LoadConfig(string path = defaultConfigPath) {
            LCVConfig lcvConfig = new();
            try {
                lcvConfig = JsonConvert.DeserializeObject<LCVConfig>(File.ReadAllText(path), Converter.Settings) ?? lcvConfig;
                Config = lcvConfig;
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] LCV.LoadConfig() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void SaveConfig(string path = defaultConfigPath) {
            try {
                File.WriteAllText(path, JsonConvert.SerializeObject(Config,Converter.Settings));
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] LCV.SaveConfig() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void DeleteConfig(string path = defaultConfigPath) {
            try {
                File.Delete(path);
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] LCV.DeleteConfig() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void Init(string path = defaultConfigPath) {
            if(File.Exists(path)) LoadConfig();
            else {
                try {
                    Directory.CreateDirectory(Path.GetDirectoryName(path) ?? @"data\");
                    Config.CommandLine.FlagItems = CommandLineConfigDefaults.FlagItems;
                    Config.CommandLine.IntItems = CommandLineConfigDefaults.IntItems;
                    Config.CommandLine.DoubleItems = CommandLineConfigDefaults.DoubleItems;
                    Config.CommandLine.SelectItems = CommandLineConfigDefaults.SelectItems;
                    SaveConfig();
                }
                catch(Exception ex) {
                    Console.WriteLine($"[Error] LCV.Init() :: {ex.GetType()} :: {ex.Message}");
                }
            }
            GTAPrivateLobby.Init();
            GTACommandLine.Init();
            ModestMenu.Init();
        }
    }
}
