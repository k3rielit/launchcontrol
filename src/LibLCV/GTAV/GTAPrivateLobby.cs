using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    // Original idea: https://www.unknowncheats.me/forum/grand-theft-auto-v/488229-gta-public-solo-session.html
    public static class GTAPrivateLobby {

        public static string EnabledFilePath => Path.Combine(LCV.Config.GTA.Path, @"x64\data\startup.meta");
        public static string DisabledFilePath => Path.Combine(LCV.Config.GTA.Path, @"x64\data\startup.meta.disabled");
        private static string GTAPath => LCV.Config.GTA.Path;
        public static bool FileEnabled => File.Exists(EnabledFilePath);

        public static void Init() {
            if(LCV.Config.PrivateLobby.Key == string.Empty && LCV.Config.PrivateLobby.AutoFindKey) ChangeKey(FindKey(),false);
            if(LCV.Config.PrivateLobby.AutoDetectEnabled) LCV.Config.PrivateLobby.Enabled = FileEnabled;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Disable() {
            LCV.Config.PrivateLobby.Enabled = false;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Enable() {
            LCV.Config.PrivateLobby.Enabled = true;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void Toggle() {
            LCV.Config.PrivateLobby.Enabled = !LCV.Config.PrivateLobby.Enabled;
            UpdateFiles();
            LCV.SaveConfig();
        }

        public static void ChangeKey(string key, bool updateFiles = true) {
            LCV.Config.PrivateLobby.Key = key;
            if(updateFiles) UpdateFiles();
            LCV.SaveConfig();
        }

        public static void UpdateFiles() {
            if(GTAPath == string.Empty || !Directory.Exists(GTAPath)) return;
            // Create the new startup.meta content
            string newcontent = string.Empty;
            try {
                newcontent = File.ReadAllText(LCV.Config.PrivateLobby.BaseFilePath) + $"<!--{LCV.Config.PrivateLobby.Key}-->";
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTAPrivateLobby.UpdateFiles() :: {ex.GetType()} :: {ex.Message}");
                return;
            }
            // Delete outdated files
            DeleteFiles();
            // Create new file
            try {
                File.WriteAllText((LCV.Config.PrivateLobby.Enabled ? EnabledFilePath : DisabledFilePath), newcontent);
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTAPrivateLobby.UpdateFiles() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static void DeleteFiles() {
            try {
                File.Delete(EnabledFilePath);
                File.Delete(DisabledFilePath);
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTAPrivateLobby.DeleteFiles() :: {ex.GetType()} :: {ex.Message}");
            }
        }

        public static string FindKey() {
            string newstr = LCV.Config.PrivateLobby.Key;
            string basestr = string.Empty;
            // Read the startup.meta.base to subtract from the key files
            try {
                basestr = File.ReadAllText(LCV.Config.PrivateLobby.BaseFilePath);
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTAPrivateLobby.FindKey() :: {ex.GetType()} :: {ex.Message}");
                return LCV.Config.PrivateLobby.Key;
            }
            // Find files and extract keys
            try {
                if(File.Exists(EnabledFilePath)) newstr = File.ReadAllText(EnabledFilePath)[(basestr.Length + 4)..^3];
                else if(File.Exists(DisabledFilePath)) newstr = File.ReadAllText(DisabledFilePath)[(basestr.Length + 4)..^3];
            }
            catch(Exception ex) {
                Console.WriteLine($"[Error] GTAPrivateLobby.FindKey() :: {ex.GetType()} :: {ex.Message}");
            }
            return newstr;
        }
    }
}
