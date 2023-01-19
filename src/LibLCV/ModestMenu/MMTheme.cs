using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LibLCV {

    public class MMTheme {

        [JsonProperty("CustomThemes")]
        public List<MMThemeItem> CustomThemes { get; set; } = new();

    }

    public class MMThemeItem {

        [JsonProperty("Name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("FontName")]
        public string FontName { get; set; } = "Microsoft Sans Serif";

        [JsonProperty("ItemColor")]
        [JsonConverter(typeof(MMHexColor))]
        public Color ItemColor { get; set; } = Color.White;

        [JsonProperty("TextColor")]
        [JsonConverter(typeof(MMHexColor))]
        public Color TextColor { get; set; } = Color.Black;

        [JsonProperty("SelectedItemColor")]
        [JsonConverter(typeof(MMHexColor))]
        public Color SelectedItemColor { get; set; } = Color.White;

        [JsonProperty("SelectedTextColor")]
        [JsonConverter(typeof(MMHexColor))]
        public Color SelectedTextColor { get; set; } = Color.Black;

        [JsonProperty("BackgroundColor")]
        [JsonConverter(typeof(MMHexColor))]
        public Color BackgroundColor { get; set; } = Color.White;

        [JsonProperty("TransparencyColorKey")]
        [JsonConverter(typeof(MMHexColor))]
        public Color TransparencyColorKey { get; set; } = Color.Cyan;

        [JsonProperty("ItemHeight")]
        public int ItemHeight { get; set; } = 19;

        [JsonProperty("ItemWidth")]
        public int ItemWidth { get; set; } = 330;

        [JsonProperty("ItemSpacing")]
        public int ItemSpacing { get; set; } = 4;

    }

}
