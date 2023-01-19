using LibLCV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCV_CLI {
    internal partial class Program {
        static void ManagePrivateLobby() {
            ConsoleKey ck = ConsoleKey.Zoom;
            Console.Clear();
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.WriteLine($"PrivateLobby[Config:{LCV.Config.PrivateLobby.Enabled.EnabledStr()} File:{GTAPrivateLobby.FileEnabled.ExistsStr()} Key:{LCV.Config.PrivateLobby.Key}]\n\n" +
                    $"[1] Enable\n" +
                    $"[2] Disable\n" +
                    $"[3] Toggle\n\n" +
                    $"[Enter] Change Lobby Key\n" +
                    $"[Insert] Create/Update Files\n" +
                    $"[Del] Delete Files\n\n" +
                    $"[ESC/Backspace] Back");
                ck = Console.ReadKey().Key;
                Console.Clear();
                switch(ck) {
                    case ConsoleKey.D1: GTAPrivateLobby.Enable(); break;
                    case ConsoleKey.D2: GTAPrivateLobby.Disable(); break;
                    case ConsoleKey.D3: GTAPrivateLobby.Toggle(); break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.Write("New Lobby Key: ");
                        GTAPrivateLobby.ChangeKey(Console.ReadLine() ?? string.Empty);
                        break;
                    case ConsoleKey.Insert: GTAPrivateLobby.UpdateFiles(); break;
                    case ConsoleKey.Delete: GTAPrivateLobby.DeleteFiles(); break;
                }
            }
        }
    }
}
