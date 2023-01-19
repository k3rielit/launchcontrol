using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    public static class GTA {

        public const string GTAProc = "GTA5";

        public static bool IsRunning => Process.GetProcessesByName(GTAProc).Length > 0;

        public static void Start() => Start(LCV.Config.GTA.LaunchType);
        public static void Start(GTALaunchType launchType) {
            switch(launchType) {
                case GTALaunchType.Epic: EpicGames.Launch(EpicGames.AppId.GTAV); break;
                // todo: other types
            }
        }

        public static void Kill() {
            foreach(Process proc in Process.GetProcessesByName(GTAProc)) proc.Kill();
        }

        public static void Restart() {
            Kill();
            Start();
        }

        public static void Freeze() {
            Process[] procList = Process.GetProcessesByName(GTAProc);
            if(procList.Length>0) {
                Console.Write($"Freezing for {LCV.Config.GTA.EmptyLobbyFreezeTimeMS}ms on {procList.Length} processes and {procList.Sum(pr => pr.Threads.Count)} threads... ");
                foreach(Process proc in procList) proc.Suspend();
                Thread.Sleep(LCV.Config.GTA.EmptyLobbyFreezeTimeMS);
                foreach(Process proc in procList) proc.Resume();
                Console.WriteLine("Done.");
            }
        }

    }
}
