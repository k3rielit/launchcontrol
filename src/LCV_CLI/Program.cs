using LibLCV;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;

namespace LCV_CLI {
    internal partial class Program {
        const string mmpath = @"D:\Cheats\modest-menu_v0.9.7\";
        const string gtapath = @"C:\Program Files\Epic Games\GTAV";

        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.Unicode;
            Console.Title = "LaunchControlV Dev";
            LCV.Init();
            MainMenu();
        }
    }
}