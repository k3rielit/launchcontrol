using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {
    public static class StringHelper {
        public static string EnabledStr(this bool b) => b ? "Enabled" : "Disabled";
        public static string OnOffStr(this bool b) => b ? "On" : "Off";
        public static string RunningStr(this bool b) => b ? "Running" : "Not Detected";
        public static string ExistsStr(this bool b) => b ? "Exists" : "Deleted";
    }
}
