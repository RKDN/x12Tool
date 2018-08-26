using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x12Tool {
    public class Config {
        private StreamWriter _ConfigWriter { get; set; }
        private StreamReader _ConfigReader { get; set; }
        private String _ConfigPath { get; set; }
        public ConfigValues Values { get; set; }

        public Config() {
            _ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "x12_tool_config.cfg");
            Values = new ConfigValues();

            ReadConfig();
            WriteConfig();
        }

        public void Refresh(){
            ReadConfig();
            WriteConfig();
        }

        public void WriteConfig() {
            using (_ConfigWriter = new StreamWriter(File.Open(_ConfigPath, FileMode.Create))) {
                foreach (var Property in typeof(ConfigValues).GetProperties()) {
                    if (Property.PropertyType == typeof(Color)) {
                        _ConfigWriter.WriteLine(Property.Name + " : " + ColorToString((Color)Property.GetValue(Values)));
                        continue;
                    }
                    if (Property.PropertyType == typeof(String)) {
                        _ConfigWriter.WriteLine(Property.Name + " : " + Property.GetValue(Values));
                        continue;
                    }
                    if (Property.PropertyType == typeof(Boolean)) {
                        _ConfigWriter.WriteLine(Property.Name + " : " + Property.GetValue(Values));
                        continue;
                    }
                    if (Property.PropertyType == typeof(Font)) {
                        _ConfigWriter.WriteLine(Property.Name + " : " + FontSerializationHelper.ToString((Font)Property.GetValue(Values)));
                        continue;
                    }
                }
            }
        }

        public static string ColorToString(Color color) {
            return color.A + "," + color.R + "," + color.G + "," + color.B;
        }

        public void ReadConfig() {
            if (File.Exists(_ConfigPath)) {
                using (_ConfigReader = new StreamReader(_ConfigPath)) {
                    while (!_ConfigReader.EndOfStream) {
                        var Line = _ConfigReader.ReadLine();
                        var ParsedLine = Line.Split(':').Select((x) => x.Trim()).ToArray();

                        if (ParsedLine.Length < 2) continue;

                        foreach (var Property in typeof(ConfigValues).GetProperties()) {
                            if (Property.Name.ToUpperInvariant() == ParsedLine[0].ToUpperInvariant()) {
                                if (Property.PropertyType == typeof(Color)) {
                                    Property.SetValue(Values, ParseColor(ParsedLine, (Color)Property.GetValue(Values)));
                                    break;
                                }
                                if (Property.PropertyType == typeof(String)) {
                                    Property.SetValue(Values, ParsedLine[1]);
                                    break;
                                }
                                if (Property.PropertyType == typeof(Boolean)) {
                                    bool value = false;
                                    if (bool.TryParse(ParsedLine[1], out value)) {
                                        Property.SetValue(Values, value);
                                    }
                                    break;
                                }
                                if (Property.PropertyType == typeof(Font)) {
                                    Property.SetValue(Values, FontSerializationHelper.FromString(ParsedLine[1]));
                                    break;
                                }
                            }
                        }

                    }
                }
            }
        }

        private static Color ParseColor(IList<string> ParsedLine, Color DefaultColor) {
            try {
                string[] colors = ParsedLine[1].Split(',');
                return Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]), int.Parse(colors[3]));
            } catch {
                return DefaultColor;
            }
        }

        public class ConfigValues {
            public Color EditorForegroundColor { get; set; }
            public Color EditorBackgroundColor { get; set; }
            public Color EditorSeparatorColor { get; set; }
            public Color EditorSelectionTextColor { get; set; }
            public Color EditorSelectionHighlightColor { get; set; }
            public Color EditorSelectionShadowColor { get; set; }
            public Font EditorFont { get; set; }

            public Color MenuBackgroundColor { get; set; }
            public Color MenuForegroundColor { get; set; }
            public Font MenuFont { get; set; }

            public Color InfoPanelBackgroundColor { get; set; }
            public Color InfoPanelForgroundColor { get; set; }
            public Font InfoPanelFont { get; set; }

            public Color TextBoxBackgroundColor { get; set; }
            public Color TextBoxForegroundColor { get; set; }
            public Font TextBoxFont { get; set; }

            public Boolean ShowLineNumbers { get; set; }
            public Boolean ShowLoopID { get; set; }
            public Boolean BoxedLineNumbersAndLoopIds { get; set; }

            public ConfigValues() {
                //Set defaults;
                EditorBackgroundColor = Color.FromArgb(255, 34, 34, 45);
                EditorForegroundColor = Color.White;
                EditorSeparatorColor = Color.Red;
                EditorSelectionTextColor = Color.FromArgb(255, 233, 250, 197);
                EditorSelectionHighlightColor = Color.FromArgb(255, 108, 108, 108);
                EditorSelectionShadowColor = Color.Black;
                EditorFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                MenuFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                InfoPanelFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                TextBoxFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);

                MenuBackgroundColor = Color.FromArgb(255, 64, 64, 70);
                MenuForegroundColor = Color.White;

                InfoPanelBackgroundColor = Color.FromArgb(255, 64, 64, 70);
                InfoPanelForgroundColor = Color.White;

                TextBoxBackgroundColor = Color.FromArgb(255, 62, 70, 83);
                TextBoxForegroundColor = Color.White;

                ShowLineNumbers = true;
                ShowLoopID = true;
                BoxedLineNumbersAndLoopIds = true;
            }

        }

        [TypeConverter(typeof(FontConverter))]
        static public class FontSerializationHelper {

            static public Font FromString(string value) {
                var parts = value.Split(',');
                return new Font(
                    parts[0],                                                   // FontFamily.Name
                    float.Parse(parts[1]),                                      // Size
                    EnumSerializationHelper.FromString<FontStyle>(parts[2]),    // Style
                    EnumSerializationHelper.FromString<GraphicsUnit>(parts[3]), // Unit
                    byte.Parse(parts[4]),                                       // GdiCharSet
                    bool.Parse(parts[5])                                        // GdiVerticalFont
                );
            }

            static public string ToString(Font font) {
                return font.FontFamily.Name
                        + "," + font.Size
                        + "," + font.Style
                        + "," + font.Unit
                        + "," + font.GdiCharSet
                        + "," + font.GdiVerticalFont
                        ;
            }
        }

        [TypeConverter(typeof(EnumConverter))]
        static public class EnumSerializationHelper {

            static public T FromString<T>(string value) {
                return (T)Enum.Parse(typeof(T), value, true);
            }
        }

    }
}
