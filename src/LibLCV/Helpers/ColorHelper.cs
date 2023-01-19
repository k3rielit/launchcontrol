using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace LibLCV {
    public static class ColorHelper {

        /// <summary>
        /// Makes a simple (255,255,255) RGB string from a <see cref='Color'/> object.
        /// </summary>
        /// <returns>
        /// "(R,G,B)" string.
        /// </returns>
        public static string RGBString(this Color color) => $"({color.R},{color.G},{color.B})";

        /// <summary>
        /// Converts a <see cref='Color'/> object to a "ffffff" hexadecimal string for use in Modest Menu config files.
        /// </summary>
        /// <returns>
        /// The resulting hexadecimal string.
        /// </returns>
        public static string MMHexString(this Color color) => color.R.ToString("x") + color.G.ToString("x") + color.B.ToString("x");

        /// <summary>
        /// Converts the "ffffff" or "#ffffff" hexadecimal strings used in Modest Menu config files to a <see cref='Color'/> object.
        /// </summary>
        /// <returns>
        /// The resulting <see cref='Color'/> object or <see cref='Color.White'/> by default.
        /// </returns>
        public static Color MMColor(this string hex) {
            try {
                return hex.StartsWith('#') ? ColorTranslator.FromHtml(hex) : ColorTranslator.FromHtml('#' + hex);
            }
            catch(Exception) {
                return Color.White;
            }
        }
    }
}
