using LibLCV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCV_CLI {
    internal partial class Program {
        static void MainMenu() {
            ConsoleKey ck = ConsoleKey.Zoom;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine($"GTA5[{GTA.IsRunning.RunningStr()}] ModestMenu[{ModestMenu.IsRunning.RunningStr()}]\n" +
                    $"PrivateLobby[Config:{LCV.Config.PrivateLobby.Enabled.EnabledStr()} File:{GTAPrivateLobby.FileEnabled.ExistsStr()} Key:{LCV.Config.PrivateLobby.Key}]\n" +
                    $"CommandLine[Config:{LCV.Config.CommandLine.Enabled.EnabledStr()} File:{GTACommandLine.FileEnabled.ExistsStr()} Enabled:{LCV.Config.CommandLine.EnabledCount()}]\n\n" +
                    $"[1] Start GTA\n" +
                    $"[2] Start Modest Menu\n" +
                    $"[3] Freeze/SoloLobby GTA\n" +
                    $"[4] Kill GTA\n" +
                    $"[5] Kill Modest Menu\n\n" +
                    $"[G] Manage GTA\n" +
                    $"[M] Manage Modest Menu\n" +
                    $"[C] Manage CommandLine\n" +
                    $"[P] Manage PrivateLobby\n\n" +
                    $"[ESC/Backspace] Exit");
                ck = Console.ReadKey().Key;
                Console.Clear();
                switch(ck) {
                    case ConsoleKey.D1: GTA.Start(); break;
                    case ConsoleKey.D2: ModestMenu.Start(); break;
                    case ConsoleKey.D3: GTA.Freeze(); break;
                    case ConsoleKey.D4: GTA.Kill(); break;
                    case ConsoleKey.D5: ModestMenu.Kill(); break;
                    case ConsoleKey.G: /*todo*/ break;
                    case ConsoleKey.M: /*todo*/ break;
                    case ConsoleKey.C: ManageCommandLine(); break;
                    case ConsoleKey.P: ManagePrivateLobby(); break;
                }
            }
        }
    }
}
