using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    public static class EpicGames {
        public struct AppId {
            public const string GTAV = "9d2d0eb64d5c44529cece33fe2a46482";
        }

        public static void Launch(string appId) {
            ProcessHelper.CMDRun($"start com.epicgames.launcher://apps/{appId}?action=launch&silent=true");
        }
    }
}
