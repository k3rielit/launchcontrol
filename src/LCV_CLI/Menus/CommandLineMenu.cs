using LibLCV;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCV_CLI {
    internal partial class Program {
        static void ManageCommandLine() {
            ConsoleKey ck = ConsoleKey.Zoom;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine($"CommandLine[Config:{LCV.Config.CommandLine.Enabled.EnabledStr()} File:{GTACommandLine.FileEnabled.ExistsStr()} Enabled:{LCV.Config.CommandLine.EnabledCount()}]\n\n" +
                    $"[1] Enable\n" +
                    $"[2] Disable\n" +
                    $"[3] Toggle\n\n" +
                    $"[Enter] Edit Mode\n" +
                    $"[Insert] Create/Update Files\n" +
                    $"[Del] Delete Files\n\n" +
                    $"[ESC/Backspace] Back");
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.D1: GTACommandLine.Enable(); break;
                    case ConsoleKey.D2: GTACommandLine.Disable(); break;
                    case ConsoleKey.D3: GTACommandLine.Toggle(); break;
                    case ConsoleKey.Enter: EditCommandLine(); break;
                    case ConsoleKey.Insert: GTACommandLine.UpdateFiles(); break;
                    case ConsoleKey.Delete: GTACommandLine.DeleteFiles(); break;
                }
            }
        }

        static void EditCommandLine() {
            ConsoleKey ck = ConsoleKey.Zoom;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine($"CommandLine[Config:{LCV.Config.CommandLine.Enabled.EnabledStr()} File:{GTACommandLine.FileEnabled.ExistsStr()} Enabled:{LCV.Config.CommandLine.EnabledCount()}]\n" +
                    $"Select the type of item you want to toggle or edit:\n\n" +
                    $"[1] On/Off (x{LCV.Config.CommandLine.FlagItems.Count})\n" +
                    $"[2] Number (x{LCV.Config.CommandLine.IntItems.Count})\n" +
                    $"[3] Precision Number (x{LCV.Config.CommandLine.DoubleItems.Count})\n" +
                    $"[4] Select From List (x{LCV.Config.CommandLine.SelectItems.Count})\n\n" +
                    $"[ESC/Backspace] Back");
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.D1: EditFlags(); break;
                    case ConsoleKey.D2: EditInts(); break;
                    case ConsoleKey.D3: EditDoubles(); break;
                    case ConsoleKey.D4: EditSelects(); break;
                }
            }
        }

        static void EditFlags() {
            if(LCV.Config.CommandLine.FlagItems.Count == 0) return;
            ConsoleKey ck = ConsoleKey.Zoom;
            int selected = 0;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine("[Enter] Toggle   [ESC/Backspace] Back\n");
                foreach(var (item, index) in LCV.Config.CommandLine.FlagItems.WithIndex()) Console.WriteLine((index == selected ? " > " : "   ") + (item.IsSet ? TextColor.Green() : TextColor.Default()) + item);
                TextColor.Default();
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.W: selected = LCV.Config.CommandLine.FlagItems.PrevIndex(selected); break;
                    case ConsoleKey.UpArrow: selected = LCV.Config.CommandLine.FlagItems.PrevIndex(selected); break;
                    case ConsoleKey.S: selected = LCV.Config.CommandLine.FlagItems.NextIndex(selected); break;
                    case ConsoleKey.DownArrow: selected = LCV.Config.CommandLine.FlagItems.NextIndex(selected); break;
                    case ConsoleKey.Enter:
                        if(LCV.Config.CommandLine.FlagItems.IndexExists(selected)) LCV.Config.CommandLine.FlagItems[selected].Toggle();
                        break;
                }
            }
        }

        static void EditInts() {
            if(LCV.Config.CommandLine.IntItems.Count == 0) return;
            ConsoleKey ck = ConsoleKey.Zoom;
            int selected = 0;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine("[Enter] Toggle   [Space] Change Value   [ESC/Backspace] Back\n");
                foreach(var (item, index) in LCV.Config.CommandLine.IntItems.WithIndex()) Console.WriteLine((index == selected ? " > " : "   ") + (item.IsSet ? TextColor.Green() : TextColor.Default()) + item);
                TextColor.Default();
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.W: selected = LCV.Config.CommandLine.IntItems.PrevIndex(selected); break;
                    case ConsoleKey.UpArrow: selected = LCV.Config.CommandLine.IntItems.PrevIndex(selected); break;
                    case ConsoleKey.S: selected = LCV.Config.CommandLine.IntItems.NextIndex(selected); break;
                    case ConsoleKey.DownArrow: selected = LCV.Config.CommandLine.IntItems.NextIndex(selected); break;
                    case ConsoleKey.Enter:
                        if(LCV.Config.CommandLine.IntItems.IndexExists(selected)) LCV.Config.CommandLine.IntItems[selected].Toggle();
                        break;
                    case ConsoleKey.Spacebar:
                        Console.Clear();
                        Console.Write($"Current:{LCV.Config.CommandLine.IntItems[selected].Value} Min:{LCV.Config.CommandLine.IntItems[selected].MinValue} Max:{LCV.Config.CommandLine.IntItems[selected].MaxValue}\nNew Value: ");
                        LCV.Config.CommandLine.IntItems[selected].TrySetValue(Console.ReadLine() ?? string.Empty);
                        break;
                }
            }
        }

        static void EditDoubles() {
            if(LCV.Config.CommandLine.DoubleItems.Count == 0) return;
            ConsoleKey ck = ConsoleKey.Zoom;
            int selected = 0;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine("[Enter] Toggle   [Space] Change Value   [ESC/Backspace] Back\n");
                foreach(var (item, index) in LCV.Config.CommandLine.DoubleItems.WithIndex()) Console.WriteLine((index == selected ? " > " : "   ") + (item.IsSet ? TextColor.Green() : TextColor.Default()) + item);
                TextColor.Default();
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.W: selected = LCV.Config.CommandLine.DoubleItems.PrevIndex(selected); break;
                    case ConsoleKey.UpArrow: selected = LCV.Config.CommandLine.DoubleItems.PrevIndex(selected); break;
                    case ConsoleKey.S: selected = LCV.Config.CommandLine.DoubleItems.NextIndex(selected); break;
                    case ConsoleKey.DownArrow: selected = LCV.Config.CommandLine.DoubleItems.NextIndex(selected); break;
                    case ConsoleKey.Enter:
                        if(LCV.Config.CommandLine.DoubleItems.IndexExists(selected)) LCV.Config.CommandLine.DoubleItems[selected].Toggle();
                        break;
                    case ConsoleKey.Spacebar:
                        Console.Clear();
                        Console.Write($"Current:{LCV.Config.CommandLine.DoubleItems[selected].Value.ToString("0.0", CultureInfo.InvariantCulture)} Min:{LCV.Config.CommandLine.DoubleItems[selected].MinValue.ToString("0.0", CultureInfo.InvariantCulture)} Max:{LCV.Config.CommandLine.DoubleItems[selected].MaxValue.ToString("0.0", CultureInfo.InvariantCulture)}\nNew Value: ");
                        LCV.Config.CommandLine.DoubleItems[selected].TrySetValue(Console.ReadLine() ?? string.Empty);
                        break;
                }
            }
        }

        static void EditSelects() {
            if(LCV.Config.CommandLine.SelectItems.Count == 0) return;
            ConsoleKey ck = ConsoleKey.Zoom;
            int selected = 0;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine("[Enter] Toggle   [Space] Change Value   [ESC/Backspace] Back\n");
                foreach(var (item, index) in LCV.Config.CommandLine.SelectItems.WithIndex()) Console.WriteLine((index == selected ? " > " : "   ") + (item.IsSet ? TextColor.Green() : TextColor.Default()) + item);
                TextColor.Default();
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.W: selected = LCV.Config.CommandLine.SelectItems.PrevIndex(selected); break;
                    case ConsoleKey.UpArrow: selected = LCV.Config.CommandLine.SelectItems.PrevIndex(selected); break;
                    case ConsoleKey.S: selected = LCV.Config.CommandLine.SelectItems.NextIndex(selected); break;
                    case ConsoleKey.DownArrow: selected = LCV.Config.CommandLine.SelectItems.NextIndex(selected); break;
                    case ConsoleKey.Enter:
                        if(LCV.Config.CommandLine.SelectItems.IndexExists(selected)) LCV.Config.CommandLine.SelectItems[selected].Toggle();
                        break;
                    case ConsoleKey.Spacebar:
                        LCV.Config.CommandLine.SelectItems[selected].SelectedIndex = CLSelector(LCV.Config.CommandLine.SelectItems[selected]);
                        break;
                }
            }
        }

        static int CLSelector(CLSelectItem item) {
            if(item.Options.Count == 0) return item.SelectedIndex;
            ConsoleKey ck = ConsoleKey.Zoom;
            int selected = 0;
            while(ck != ConsoleKey.Escape && ck != ConsoleKey.Backspace) {
                Console.Clear();
                Console.WriteLine($"[WASD/Arrow Keys] Move Up/Down   [Enter] Select Item   [ESC/Backspace] Back\n\n   {item.Name}:\n");
                foreach(var (option, index) in item.Options.WithIndex()) Console.WriteLine((index == selected ? " > " : "   ") + (index == item.SelectedIndex ? TextColor.Green() : TextColor.Default()) + $"{option[0]} ({option[1]})");
                TextColor.Default();
                ck = Console.ReadKey().Key;
                switch(ck) {
                    case ConsoleKey.W: selected = item.Options.PrevIndex(selected); break;
                    case ConsoleKey.UpArrow: selected = item.Options.PrevIndex(selected); break;
                    case ConsoleKey.S: selected = item.Options.NextIndex(selected); break;
                    case ConsoleKey.DownArrow: selected = item.Options.NextIndex(selected); break;
                    case ConsoleKey.Enter:
                        if(item.Options.IndexExists(selected)) item.SelectedIndex = selected;
                        break;
                }
            }
            return item.SelectedIndex;
        }
    }
}
