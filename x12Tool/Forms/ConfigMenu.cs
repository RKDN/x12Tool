using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace x12Tool {
    public partial class ConfigMenu : Form {

        Action RefreshForm;

        public ConfigMenu(Action RefreshForm) {
            InitializeComponent();
            this.RefreshForm = RefreshForm;
        }

        private void ConfigMenu_Load(object sender, EventArgs e) {

            foreach (var Property in typeof(Config.ConfigValues).GetProperties()) {
                if (Property.PropertyType == typeof(Color)) {

                    var FlowPanel = new FlowLayoutPanel();
                    FlowPanel.Width = ColorPanel.Width - 30;
                    FlowPanel.Height = 25;

                    var Button = new Button();
                    Button.BackColor = SystemColors.Control;
                    Button.TextAlign = ContentAlignment.TopCenter;
                    Button.Text = "Edit";
                    Button.Width = 40;
                    Button.Height = 20;
                    FlowPanel.Controls.Add(Button);

                    var Label = new Label();
                    Label.Parent = FlowPanel;
                    Label.Text = Property.Name;
                    Label.AutoSize = false;
                    Label.Width = 160;
                    Label.Height = 25;
                    Label.TextAlign = ContentAlignment.MiddleLeft;
                    FlowPanel.Controls.Add(Label);

                    Button.Click += new EventHandler((o, ee) => {
                        colorDialog1.FullOpen = true;
                        colorDialog1.AnyColor = true;
                        colorDialog1.Color = (Color)Property.GetValue(GLOBALS.Config.Values);

                        if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            Property.SetValue(GLOBALS.Config.Values, colorDialog1.Color);
                            RefreshForm();
                            ApplyConfig();
                        }
                    });

                    ColorPanel.Controls.Add(FlowPanel);
                } else if (Property.PropertyType == typeof(Font)) {
                    var FlowPanel = new FlowLayoutPanel();
                    FlowPanel.Width = ColorPanel.Width - 30;
                    FlowPanel.Height = 25;

                    var Button = new Button();
                    Button.BackColor = SystemColors.Control;
                    Button.TextAlign = ContentAlignment.TopCenter;
                    Button.Text = "Edit";
                    Button.Width = 40;
                    Button.Height = 20;
                    FlowPanel.Controls.Add(Button);

                    var Label = new Label();
                    Label.Parent = FlowPanel;
                    Label.Text = Property.Name;
                    Label.AutoSize = false;
                    Label.Width = 160;
                    Label.Height = 25;
                    Label.TextAlign = ContentAlignment.MiddleLeft;
                    FlowPanel.Controls.Add(Label);

                    Button.Click += new EventHandler((o, ee) => {

                        fontDialog1.Font = (Font)Property.GetValue(GLOBALS.Config.Values);

                        if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            Property.SetValue(GLOBALS.Config.Values, fontDialog1.Font);
                            RefreshForm();
                            ApplyConfig();
                        }
                    });
                    FontPanel.Controls.Add(FlowPanel);
                } else if (Property.PropertyType == typeof(Boolean)) {
                    var FlowPanel = new FlowLayoutPanel();
                    FlowPanel.Width = ColorPanel.Width - 30;
                    FlowPanel.Height = 25;

                    var CheckBox = new CheckBox();
                    CheckBox.Text = Property.Name;
                    CheckBox.Width = 180;
                    CheckBox.Checked = (Boolean)Property.GetValue(GLOBALS.Config.Values);
                    FlowPanel.Controls.Add(CheckBox);

                    CheckBox.Click += new EventHandler((o, ee) => {
                        Property.SetValue(GLOBALS.Config.Values, CheckBox.Checked);
                        RefreshForm();
                        ApplyConfig();
                    });
                    FlagPanel.Controls.Add(FlowPanel);
                }
            }

            ApplyConfig();
        }

        private void ApplyConfig() {
            this.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
            this.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;

            _gen_func.SetFormatByControlType(this);
        }

        private void okBtn_Click(object sender, EventArgs e) {
            GLOBALS.Config.WriteConfig();
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e) {
            GLOBALS.Config.Refresh();
            this.Close();
        }
    }
}
