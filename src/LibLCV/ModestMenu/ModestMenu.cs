using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LibLCV {
    public class ModestMenu {
        private const string MMProc = "modest-menu";

        public static string MMExec => Path.Combine(LCV.Config.ModestMenu.Path, "modest-menu.exe");
        public static string MMThemes => Path.Combine(LCV.Config.ModestMenu.Path, "themes.json");
        public static bool IsRunning => Process.GetProcessesByName(MMProc).Length > 0;
        public static MMTheme Theme { get; set; } = new();

        public static void Init() {
            if(Directory.Exists(LCV.Config.ModestMenu.Path)) {
                LoadThemes();
            }
        }

        public static void Start() {
            if(File.Exists(MMExec)) ProcessHelper.StartProcess(MMExec);
        }

        public static void Kill() {
            foreach(Process proc in Process.GetProcessesByName(MMProc)) proc.Kill();
        }

        public static void Restart() {
            Kill();
            Start();
        }

        public static void LoadThemes() {
            if(File.Exists(MMThemes)) {
                try {
                    Theme = JsonConvert.DeserializeObject<MMTheme>(File.ReadAllText(MMThemes),Converter.Settings) ?? Theme;
                }
                catch(Exception ex) {
                    Console.WriteLine($"[Error] ModestMenu.LoadThemes() :: {ex.GetType()} :: {ex.Message}");
                }
            }
        }
    }
}
