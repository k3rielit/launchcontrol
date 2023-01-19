using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {

    public static class GTACommandLine {

        public static string EnabledFilePath => Path.Combine(LCV.Config.GTA.Path, @"commandline.txt");
        public static string DisabledFilePath => Path.Combine(LCV.Config.GTA.Path, @"commandline.txt.disabled");
        public static bool FileEnabled => File.Exists(EnabledFilePath);
        private static string GTAPath => LCV.Config.GTA.Path;

        public static void Init() {
            if(LCV.Config.CommandLine.AutoDetectEnabled) LCV.Config.CommandLine.Enabled = FileEnabled;
            if(LCV.Config.CommandLine.AutoFindSettings) LoadFromFile();
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Disable() {
            LCV.Config.CommandLine.Enabled = false;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Enable() {
            LCV.Config.CommandLine.Enabled = true;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Toggle() {
            LCV.Config.CommandLine.Enabled = !LCV.Config.CommandLine.Enabled;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void UpdateFiles() {
            if(GTAPath == string.Empty || !Directory.Exists(GTAPath)) return;
            // Delete outdated files
            DeleteFiles();
            // Create the new file and its content
            try {
                StreamWriter wr = new(LCV.Config.CommandLine.Enabled ? EnabledFilePath : DisabledFilePath);
                foreach(ICLItem clItem in LCV.Config.CommandLine.EnabledItems()) wr.WriteLine(clItem.ToString());
                wr.Close();
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTACommandLine.UpdateFiles() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void DeleteFiles() {
            try {
                File.Delete(EnabledFilePath);
                File.Delete(DisabledFilePath);
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTACommandLine.DeleteFiles() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void LoadFromFile() {
            // ...
        }
    }
}
