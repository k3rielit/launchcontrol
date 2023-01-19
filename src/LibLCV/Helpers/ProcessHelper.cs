using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace LibLCV {
    public static class ProcessHelper {
        [Flags]
        public enum ThreadAccess : int {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        public static void SuspendProcess(int processId) => Process.GetProcessById(processId).Suspend();
        public static void ResumeProcess(int processId) => Process.GetProcessById(processId).Resume();

        public static void Suspend(this Process process) {
            foreach(ProcessThread thread in process.Threads) {
                try {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                    if(pOpenThread != IntPtr.Zero) {
                        SuspendThread(pOpenThread);
                    }
                    CloseHandle(pOpenThread);
                }
                catch(Exception) { }
            }
        }

        public static void Resume(this Process process) {
            if(process.ProcessName != string.Empty) {
                foreach(ProcessThread pT in process.Threads) {
                    try {
                        IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
                        if(pOpenThread != IntPtr.Zero) {
                            int suspendCount = 0;
                            do {
                                suspendCount = ResumeThread(pOpenThread);
                            }
                            while(suspendCount > 0);
                        }
                        CloseHandle(pOpenThread);
                    }
                    catch(Exception) { }
                }
            }
        }

        public static bool StartProcess(string path, bool attemptWithCmd = true) {
            try {
                Process.Start(path);
                return true;
            }
            catch(Exception) {
                if(attemptWithCmd) {
                    return CMDRun($"/C start {path}");
                }
            }
            return false;
        }

        public static bool CMDRun(string arguments) {
            ProcessStartInfo proc = new() {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = arguments.StartsWith("/C") ? arguments : $"/C {arguments}"
            };
            try {
                Process.Start(proc);
                return true;
            }
            catch(Exception) { }
            return false;
        }

        /*public static bool CMDRunAdmin(string arguments) {
            Console.WriteLine($"CMDRunAdmin({arguments})");
            // runas /profile /user:administrator “C:\D\e”
            ProcessStartInfo proc = new() {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = arguments.StartsWith("/C") ? $"/C runas /profile /user:administrator \"{arguments[3..]}\"" : $"/C runas /profile /user:administrator \"{arguments}\""
            };
            try {
                Process.Start(proc);
                return true;
            }
            catch(Exception) { }
            return false;
        }*/
    }
}