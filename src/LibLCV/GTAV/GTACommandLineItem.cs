using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibLCV {

    public interface ICLItem {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsSet { get; set; }
        public void Enable();
        public void Disable();
        public void Toggle();
    }

    public class CLFlagItem : ICLItem {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = "No description";
        public string Category { get; set; } = "Flag";
        public bool IsSet { get; set; } = false;
        public override string ToString() => '-' + Name;
        public void Enable() {
            IsSet = true;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Disable() {
            IsSet = false;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Toggle() {
            IsSet = !IsSet;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
    }

    public class CLIntItem : ICLItem {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = "No description";
        public string Category { get; set; } = "Int";
        public bool IsSet { get; set; } = false;
        public int Value { get; set; } = 0;
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = int.MaxValue;
        public override string ToString() => Value >= MinValue && Value <= MaxValue ? $"-{Name} {Value}" : $"-{Name} {MinValue}";
        public void Enable() {
            IsSet = true;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Disable() {
            IsSet = false;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Toggle() {
            IsSet = !IsSet;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void TrySetValue(string str) {
            if(str != null && int.TryParse(str, out int newNum)) Value = newNum >= MinValue && newNum <= MaxValue ? newNum : Value;
            if(IsSet) GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
    }

    public class CLDoubleItem : ICLItem {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = "No description";
        public string Category { get; set; } = "Double/Float";
        public bool IsSet { get; set; } = false;
        public double Value { get; set; } = 0.0;
        public double MinValue { get; set; } = 0.0;
        public double MaxValue { get; set; } = double.MaxValue;
        public override string ToString() => Value >= MinValue && Value <= MaxValue ? $"-{Name} {Value.ToString("0.0",CultureInfo.InvariantCulture)}" : $"-{Name} {MinValue.ToString("0.0", CultureInfo.InvariantCulture)}";
        public void Enable() {
            IsSet = true;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Disable() {
            IsSet = false;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Toggle() {
            IsSet = !IsSet;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void TrySetValue(string str) {
            if(str != null && double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double newNum)) Value = newNum >= MinValue && newNum <= MaxValue ? newNum : Value;
            if(IsSet) GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
    }

    public class CLSelectItem : ICLItem {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = "No description";
        public string Category { get; set; } = "Select";
        public bool IsSet { get; set; } = false;
        public List<string[]> Options { get; set; } = new(); // { ["name","description"], ["name","description"], ... }
        public int SelectedIndex { get; set; } = 0;
        public override string ToString() => Options.Count > SelectedIndex ? $"-{Name} {Options[SelectedIndex][0]}" : (Options.Count > 0 ? Options[0][0] : string.Empty);
        public void Enable() {
            IsSet = true;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Disable() {
            IsSet = false;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void Toggle() {
            IsSet = !IsSet;
            GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
        public void TrySetIndex(int index) {
            if(Options.IndexExists(index)) SelectedIndex = index;
            if(IsSet) GTACommandLine.UpdateFiles();
            LCV.SaveConfig();
        }
    }
}
