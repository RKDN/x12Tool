using EDIReader.X12;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x12Tool {

    public partial class Main : Form {
        private int currentSearchIndex = 0;
        private String SearchPhrase = "";
        private x12File x12File;

        private const int WM_SETREDRAW = 11;

        public Main(string[] args) {
            InitializeComponent();
           
            ApplyConfig();

            SegmentDef = new Dictionary<string, string>();
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Definitions\Segments.def"))) {
                var tempList = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Definitions\Segments.def")).ToList();

                foreach (var s in tempList) {
                    var def = s.Split(':');
                    if (def.Length > 1) {
                        SegmentDef.Add(def[0], def[1]);
                    }
                }
            }

            if (args.Length > 0) {
                if (File.Exists(args[0])) {
                    openFile(args[0]);
                }
            }
        }

        public void ApplyConfig() {
            logoBox.BackColor = GLOBALS.Config.Values.EditorBackgroundColor;
            x12View.BackColor = GLOBALS.Config.Values.EditorBackgroundColor;
            x12View.ForeColor = GLOBALS.Config.Values.EditorForegroundColor;
            x12View.Font = GLOBALS.Config.Values.EditorFont;
            x12View.ItemHeight = GLOBALS.Config.Values.EditorFont.Height + 4;

            errorList.BackColor = GLOBALS.Config.Values.EditorBackgroundColor;
            errorList.ForeColor = GLOBALS.Config.Values.EditorForegroundColor;
            errorList.Font = GLOBALS.Config.Values.EditorFont;

            _gen_func.SetFormatByControlType(this);
        }

        public IEnumerable<Segment> SearchResults { get; set; }
        public Dictionary<String, String> SegmentDef { get; private set; }

        public String ElementDef(String SegmentId, int ElementNum) {
            if (SegmentDef == null) return "";
            ElementNum--;
            String defLine;
            if (SegmentDef.TryGetValue(SegmentId, out defLine)) {
                var splitDefLine = defLine.Split(',');
                if (defLine.Length > ElementNum) {
                    return splitDefLine[ElementNum];
                }
            }
            return "";
        }

        public void ShowNodeMatchingSegment(TreeNodeCollection t, Segment s) {
            for (int i = t.Count - 1; i >= 0; i--) {
                var nodeSegment = (Segment)t[i].Tag;

                if (nodeSegment.LineNumber < s.LineNumber) {
                    if (nodeSegment.HasChildren) {
                        ExpandNode(t[i]);
                        ShowNodeMatchingSegment(t[i].Nodes, s);
                        return;
                    } else {
                        return;
                    }
                } else if (nodeSegment.LineNumber == s.LineNumber) {
                    x12View.SelectedNode = t[i];
                    ShowSegmentInEditor(nodeSegment);
                    x12View.SelectedNode.EnsureVisible();
                    return;
                }
            }
        }

        private void errorList_SelectedIndexChanged(object sender, EventArgs e) {
            if (errorList.SelectedItems.Count > 0) {
                var s = (Segment)errorList.SelectedItems[0].Tag;
                ShowNodeMatchingSegment(x12View.Nodes, s);
            }
        }

        private void errorsToolStripMenuItem_Click(object sender, EventArgs e) {
            EditorErrorSplitter.Panel2Collapsed = !EditorErrorSplitter.Panel2Collapsed;
        }

        private void ExpandNode(TreeNode NodeToExpand) {
            var s = (Segment)NodeToExpand.Tag;
            var Nodes = new List<TreeNode>();

            if (NodeToExpand.Nodes.Count == 0) {
                if (s.HasChildren) {
                    foreach (var segment in s.ChildSegments) {
                        var node = new TreeNode(segment.LineText);
                        node.Tag = segment;
                        node.ImageIndex = segment.HasChildren ? 0 : 1;
                        Nodes.Add(node);
                    }

                    Invoke(new Action(() => NodeToExpand.Nodes.AddRange(Nodes.ToArray())));

                    NodeToExpand.Expand();
                    NodeToExpand.ImageIndex = 2;
                }
            } else {
                if (NodeToExpand.IsExpanded) {
                    NodeToExpand.Collapse();
                    NodeToExpand.ImageIndex = 0;
                } else {
                    NodeToExpand.Expand();
                    NodeToExpand.ImageIndex = 2;
                }
            }
            NodeToExpand.SelectedImageIndex = NodeToExpand.ImageIndex;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                openFile(openFileDialog1.FileName);
                searchToolStripMenuItem.Enabled = true;
            }
        }

        private void openFile(string fileName) {
            x12View.Nodes.Clear();
            errorList.Items.Clear();

            //Create our x12File object
            x12File = new x12File(fileName, null, true);

            //Read the format file for the x12 file selected
            //If we can't find the correct format file return
            if (ReadFormatFile() == false) return;

            //Show the file path in the textbox.
            this.Text = "X12Tool " + fileName;

            //Set up our progress handler
            x12File.ProgressUpdated += new System.ComponentModel.ProgressChangedEventHandler((object o, System.ComponentModel.ProgressChangedEventArgs args) => Invoke(new Action(() => {
                progressBar.Value = args.ProgressPercentage;
            })));

            //Setup x12View
            x12View.DrawMode = TreeViewDrawMode.OwnerDrawText;
            x12View.ImageList = new ImageList();
            x12View.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            x12View.ImageList.Images.Add(Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\loop.png")));
            x12View.ImageList.Images.Add(Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\segment.png")));
            x12View.ImageList.Images.Add(Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\selected.png")));

            DisplayX12();
        }

        private bool ReadFormatFile() {
            if (x12File.Format != null) {
                x12File.FormatGuide = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"FormatFiles", x12File.Format);
                if (!string.IsNullOrEmpty(x12File.Version)) {
                    x12File.FormatGuide += "-" + x12File.Version;
                }
                x12File.FormatGuide += ".xml";

                if (!File.Exists(x12File.FormatGuide)) {
                    MessageBox.Show("No valid config file found for (" + x12File.Format + "-" + x12File.Version + "). Please create a format file for (" + x12File.Format + "-" + x12File.Version + ") and place it in the FormatFiles directory.");
                    return false;
                }
            } else {
                MessageBox.Show("Could not determine the format of this file.");
                return false;
            }
            return true;
        }

        private void DisplayX12() {
            x12View.Nodes.Clear();
            logoBox.Visible = false;
            var thread = new Thread(() => {
                var t = new System.Diagnostics.Stopwatch();
                t.Start();
                var TreeNodes = new List<TreeNode>();
                var SegmentsWithErrors = new List<ListViewItem>();

                try {
                    foreach (var segment in x12File.Segments) {
                        var node = new TreeNode(segment.LineText);
                        node.Tag = segment;
                        node.ImageIndex = segment.HasChildren ? 0 : 1;
                        TreeNodes.Add(node);
                    }

                    Validation(x12File.Segments, SegmentsWithErrors);
                } catch (Exception ex) {
                    Invoke(new Action(() => {
                        var lvi = new ListViewItem("Format Guide Error");
                        lvi.SubItems.Add(ex.Message);
                        errorList.Items.Insert(0, lvi);
                        EditorErrorSplitter.Panel2Collapsed = false;
                    }));
                }

                if (x12File.ISA != null) {
                    var ISA = new TreeNode(x12File.ISA.LineText);
                    ISA.Tag = x12File.ISA;
                    ISA.ImageIndex = 1;
                    TreeNodes.Insert(0, ISA);
                }

                if (x12File.GS != null) {
                    var GS = new TreeNode(x12File.GS.LineText);
                    GS.Tag = x12File.GS;
                    GS.ImageIndex = 1;
                    TreeNodes.Insert(1, GS);
                }

                if (x12File.GE != null) {
                    var GE = new TreeNode(x12File.GE.LineText);
                    GE.Tag = x12File.GE;
                    GE.ImageIndex = 1;
                    TreeNodes.Add(GE);
                }

                if (x12File.IEA != null) {
                    var IEA = new TreeNode(x12File.IEA.LineText);
                    IEA.Tag = x12File.IEA;
                    IEA.ImageIndex = 1;
                    TreeNodes.Add(IEA);
                }

                if (!this.IsDisposed) {
                    Invoke(new Action(() => {
                        x12View.BeginUpdate();
                        x12View.Nodes.AddRange(TreeNodes.ToArray());
                        if (SegmentsWithErrors.Count > 0) {
                            errorList.Items.AddRange(SegmentsWithErrors.ToArray());
                            EditorErrorSplitter.Panel2Collapsed = false;
                        }
                        x12View.EndUpdate();
                    }));
                }

                t.Stop();
                Console.WriteLine(t.ElapsedMilliseconds);
            });
            thread.Start();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            var SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.AddExtension = true;
            SaveFileDialog.DefaultExt = ".txt";
            SaveFileDialog.Filter = "Text File (*.txt)|*.txt|All Files (*.*)|*.*";
            if (SaveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                x12File.toX12File(SaveFileDialog.FileName);
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void ShowSegmentInEditor(Segment s) {
            Invoke(new Action(InfoContainer.Panel2.Controls.Clear));

            Invoke(new Action(() => {
                if (!string.IsNullOrEmpty(s.Info.Name)) {
                    if (!string.IsNullOrEmpty(s.Info.LoopId)) {
                        NameInfoText.Text = s.Info.LoopId.ToUpperInvariant() + " - " + s.Info.Name.ToUpperInvariant();
                    } else {
                        NameInfoText.Text = s.Info.Name.ToUpperInvariant();
                    }
                }

                UsageText.Text = "Usage: " + ((s.Info.Required) ? "Required" : "Situational");
                RepeatText.Text = "Repeat: " + s.Info.Repeat;
            }));

            const int Spacing = 22;
            var StartPoint = -12;
            for (int i = 1; i <= s.Length(); i++) {
                Invoke(new Action(() => {
                    StartPoint += Spacing;

                    var Label = new Label();
                    Label.Text = "E" + i;
                    Label.Parent = InfoContainer.Panel2;
                    Label.Location = new Point(0, StartPoint);
                    Label.Width = Label.Text.Length * 10;
                    Label.ForeColor = Color.White;

                    if (i == 1 && s.Info.SecondaryId(1).Length > 0) {
                        var ComboBox = new ComboBox();
                        ComboBox.Text = s.Element(i);
                        ComboBox.DropDownStyle = ComboBoxStyle.DropDown;
                        ComboBox.Parent = InfoContainer.Panel2;
                        ComboBox.Location = new Point(30, StartPoint - 3);
                        ComboBox.Width = (InfoContainer.Panel2.Width - ComboBox.Location.X) - 5;
                        ComboBox.BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
                        ComboBox.ForeColor = GLOBALS.Config.Values.TextBoxForegroundColor;
                        ComboBox.Tag = i;
                        ComboBox.Items.AddRange(s.Info.SecondaryId(1).Select((x) => x.ToUpperInvariant()).ToArray());

                        ComboBox.TextChanged += new EventHandler((o, ee) => {
                            var t = (ComboBox)o;
                            s.replaceElement((int)t.Tag, t.Text);
                            x12View.SelectedNode.Text = s.LineText;
                        });

                        Label.MouseHover += new EventHandler((o, ee) => new ToolTip().Show(ElementDef(s.Id, (int)ComboBox.Tag), ComboBox, 0, 0, 1000));
                    } else {
                        var TextBox = new TextBox();
                        TextBox.Text = s.Element(i);
                        TextBox.Parent = InfoContainer.Panel2;
                        TextBox.Location = new Point(30, StartPoint - 3);
                        TextBox.Width = (InfoContainer.Panel2.Width - TextBox.Location.X) - 5;
                        TextBox.BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
                        TextBox.ForeColor = GLOBALS.Config.Values.TextBoxForegroundColor;
                        TextBox.Tag = i;

                        TextBox.TextChanged += new EventHandler((o, ee) => {
                            var t = (TextBox)o;
                            s.replaceElement((int)t.Tag, t.Text);
                            x12View.SelectedNode.Text = s.LineText;
                        });

                        Label.MouseHover += new EventHandler((o, ee) => new ToolTip().Show(ElementDef(s.Id, (int)TextBox.Tag), TextBox, 0, 0, 1000));
                    }
                }));
            }

            StartPoint += Spacing;

            var AddElement = new Button();
            AddElement.BackColor = GLOBALS.Config.Values.InfoPanelBackgroundColor;
            AddElement.ForeColor = GLOBALS.Config.Values.InfoPanelForgroundColor;
            AddElement.Height = 15;
            AddElement.Width = 20;
            AddElement.Text = "+";
            AddElement.Parent = InfoContainer.Panel2;
            AddElement.FlatStyle = FlatStyle.System;
            AddElement.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
            AddElement.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
            AddElement.Location = new Point(30, StartPoint);

            AddElement.Click += new EventHandler((o, ee) => {
                s.LineText += x12File.elementDelimiter;
                x12View.SelectedNode.Text = s.LineText;
                ShowSegmentInEditor(s);
            });

            var RemoveElement = new Button();
            RemoveElement.BackColor = GLOBALS.Config.Values.InfoPanelBackgroundColor;
            RemoveElement.ForeColor = GLOBALS.Config.Values.InfoPanelForgroundColor;
            RemoveElement.Height = 15;
            RemoveElement.Width = 20;
            RemoveElement.Text = "-";
            RemoveElement.Parent = InfoContainer.Panel2;
            RemoveElement.FlatStyle = FlatStyle.System;
            RemoveElement.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
            RemoveElement.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
            RemoveElement.Location = new Point(55, StartPoint);

            RemoveElement.Click += new EventHandler((o, ee) => {
                var tempList = s.LineText.Split(x12File.elementDelimiter).ToList();
                tempList.Remove(tempList.Last());
                s.LineText = String.Join(x12File.elementDelimiter.ToString(), tempList);

                x12View.SelectedNode.Text = s.LineText;
                ShowSegmentInEditor(s);
            });
        }

        private void Validation(IEnumerable<Segment> Segments, List<ListViewItem> SegmentsWithErrors) {
            foreach (var segment in Segments) {
                //Validation Rules can go here.

                //Show missing child errors
                foreach (var MissingChild in segment.MissingRequiredChildren) {
                    var lvi = new ListViewItem(segment.LineText);
                    lvi.SubItems.Add("Missing required child element " + MissingChild.Id + " " + String.Join(":", MissingChild.SecondaryId(1)).ToUpperInvariant());
                    lvi.Tag = segment;
                    SegmentsWithErrors.Add(lvi);
                }

                //Catch errors that were thrown in the reader.
                if (segment.Error != null) {
                    var lvi = new ListViewItem(segment.LineText);
                    lvi.SubItems.Add(segment.Error);
                    lvi.Tag = segment;
                    SegmentsWithErrors.Add(lvi);
                }

                if (segment.HasChildren) {
                    Validation(segment.ChildSegments, SegmentsWithErrors);
                }
            }
        }

        private void x12View_DrawNode(object sender, DrawTreeNodeEventArgs e) {
            if (!e.Node.IsVisible) return;

            var sb = new StringBuilder();

            //Get the node segment
            var NodeSegment = ((Segment)e.Node.Tag);

            //Fonts
            var NodeFont = GLOBALS.Config.Values.EditorFont;
            var InfoFont = GLOBALS.Config.Values.EditorFont;
            var SeperatorFont = new Font("Arial", NodeFont.Size + 3);

            //Bounds
            var BoundsAdjustment = (NodeSegment.HasChildren) ? 0 : 18;

            var LineNumberText = (GLOBALS.Config.Values.ShowLineNumbers == true) ? NodeSegment.LineNumber.ToString() : String.Empty;
            var LoopText = (GLOBALS.Config.Values.ShowLoopID && !string.IsNullOrEmpty(NodeSegment.Info.LoopId)) ? "*(" + NodeSegment.Info.LoopId.ToUpperInvariant() + ")" : String.Empty;

            var InfoText = String.Empty;
            if (GLOBALS.Config.Values.ShowLineNumbers) {
                InfoText += LineNumberText;
            }
            if (GLOBALS.Config.Values.ShowLoopID) {
                InfoText += LoopText;
            }

            var InfoTextWidth = -(int)e.Graphics.MeasureString(InfoText, InfoFont).Width - 4;

            var LineNumberBounds = new Rectangle(e.Node.Bounds.Left - BoundsAdjustment, e.Node.Bounds.Top, (int)e.Graphics.MeasureString(InfoText, InfoFont).Width, e.Node.Bounds.Height - 3);
            var LineNumberShadowBounds = new Rectangle(e.Node.Bounds.Left - BoundsAdjustment + 2, e.Node.Bounds.Top, (int)e.Graphics.MeasureString(InfoText, InfoFont).Width + 1, e.Node.Bounds.Height + 1);

            var TextBounds = new Rectangle(e.Node.Bounds.Left - BoundsAdjustment - InfoTextWidth, e.Node.Bounds.Top, x12View.Width - (e.Node.Bounds.Left - BoundsAdjustment - InfoTextWidth) - 5, e.Node.Bounds.Height - 3);
            var HighlightBounds = new Rectangle(e.Node.Bounds.Left - BoundsAdjustment, e.Node.Bounds.Top, x12View.Width - (e.Node.Bounds.Left - BoundsAdjustment) - 5, e.Node.Bounds.Height);
            var ShadowBounds = new Rectangle((e.Node.Bounds.Left + 2) - BoundsAdjustment - InfoTextWidth, e.Node.Bounds.Top, x12View.Width - ((e.Node.Bounds.Left + 2) - BoundsAdjustment - InfoTextWidth) - 3, e.Node.Bounds.Height);

            //Points
            var DrawPoint = new PointF(e.Node.Bounds.Left - BoundsAdjustment, e.Node.Bounds.Top);

            //Colors
            Brush TextColor = new SolidBrush(GLOBALS.Config.Values.EditorForegroundColor);
            Brush SeperatorColor = new SolidBrush(GLOBALS.Config.Values.EditorSeparatorColor);

            //Selection Highlighting
            if ((e.State & TreeNodeStates.Selected) != 0) {
                e.Graphics.FillRectangle(new SolidBrush(GLOBALS.Config.Values.EditorBackgroundColor), HighlightBounds);
                e.Graphics.FillRectangle(new SolidBrush(GLOBALS.Config.Values.EditorSelectionShadowColor), ShadowBounds);
                e.Graphics.FillRectangle(new SolidBrush(GLOBALS.Config.Values.EditorSelectionHighlightColor), TextBounds);
                e.Graphics.DrawRectangle(new Pen(GLOBALS.Config.Values.EditorBackgroundColor, 1f), TextBounds);

                TextColor = new SolidBrush(GLOBALS.Config.Values.EditorSelectionTextColor);
                SeperatorColor = new SolidBrush(GLOBALS.Config.Values.EditorSeparatorColor);
                NodeFont = new Font(NodeFont, FontStyle.Bold);
            }

            if (GLOBALS.Config.Values.BoxedLineNumbersAndLoopIds && (GLOBALS.Config.Values.ShowLineNumbers == true  || (GLOBALS.Config.Values.ShowLoopID == true && !string.IsNullOrEmpty(NodeSegment.Info.LoopId)))) {
                e.Graphics.FillRectangle(new SolidBrush(GLOBALS.Config.Values.EditorSelectionShadowColor), LineNumberShadowBounds);
                e.Graphics.FillRectangle(new SolidBrush(GLOBALS.Config.Values.EditorBackgroundColor), LineNumberBounds);
                e.Graphics.DrawRectangle(new Pen(GLOBALS.Config.Values.EditorSelectionHighlightColor), LineNumberBounds);
            }

            if (GLOBALS.Config.Values.ShowLineNumbers == true) {
                //Line Number
                e.Graphics.DrawString(NodeSegment.LineNumber + " ", InfoFont, TextColor, DrawPoint);
                DrawPoint = new PointF(DrawPoint.X + e.Graphics.MeasureString(NodeSegment.LineNumber + " ", InfoFont).Width, DrawPoint.Y);
            }

            //LoopId
            if (GLOBALS.Config.Values.ShowLoopID && !string.IsNullOrEmpty(NodeSegment.Info.LoopId)) {
                e.Graphics.DrawString("(" + NodeSegment.Info.LoopId.ToUpperInvariant() + ")", InfoFont, TextColor, DrawPoint);
                DrawPoint = new PointF(DrawPoint.X + e.Graphics.MeasureString("(" + NodeSegment.Info.LoopId.ToUpperInvariant() + ")", InfoFont).Width, DrawPoint.Y);
            }

            DrawPoint = new PointF(DrawPoint.X + 5, DrawPoint.Y);

            //Segment
            for (int i = 0; i < e.Node.Text.Length; i++) {
                if (e.Node.Text[i] == '*') {
                    e.Graphics.DrawString(sb.ToString(), NodeFont, TextColor, DrawPoint);
                    DrawPoint = new PointF(DrawPoint.X + e.Graphics.MeasureString(sb.ToString(), NodeFont).Width, DrawPoint.Y);
                    sb.Clear();

                    e.Graphics.DrawString(e.Node.Text[i].ToString(), SeperatorFont, SeperatorColor, DrawPoint);
                    DrawPoint = new PointF(DrawPoint.X + e.Graphics.MeasureString("*", SeperatorFont).Width, DrawPoint.Y);
                } else {
                    sb.Append(e.Node.Text[i]);
                }
            }
            e.Graphics.DrawString(sb.ToString(), NodeFont, TextColor, DrawPoint);
            DrawPoint = new PointF(DrawPoint.X + e.Graphics.MeasureString(sb.ToString(), NodeFont).Width, DrawPoint.Y);
            sb.Clear();
        }

        private void x12View_MouseClick(object sender, MouseEventArgs e) {
            x12View.Controls.Clear();
        }

        private void x12View_MouseUp(object sender, MouseEventArgs e) {
            x12View.SelectedNode = x12View.GetNodeAt(e.Location);

            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                if (x12View.SelectedNode != null) {
                    ExpandNode(x12View.SelectedNode);
                    Segment s = (Segment)x12View.SelectedNode.Tag;
                    ShowSegmentInEditor(s);
                } else {
                    Invoke(new Action(InfoContainer.Panel2.Controls.Clear));
                }
            }
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (x12File.Segments != null) {
                x12View.Controls.Clear();

                var ButtonFind = new Button();
                ButtonFind.Parent = x12View;
                ButtonFind.Text = "Find";
                ButtonFind.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8f, FontStyle.Regular);
                ButtonFind.TextAlign = ContentAlignment.MiddleCenter;
                ButtonFind.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
                ButtonFind.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
                ButtonFind.Width = 50;
                ButtonFind.Height = 20;
                ButtonFind.BringToFront();
                ButtonFind.Location = new Point(x12View.Width - ButtonFind.Width - 25, 0);

                var ButtonNext = new Button();
                ButtonNext.Parent = x12View;
                ButtonNext.Text = Char.ConvertFromUtf32(0x25BC);
                ButtonNext.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8f, FontStyle.Regular);
                ButtonNext.TextAlign = ContentAlignment.MiddleCenter;
                ButtonNext.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
                ButtonNext.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
                ButtonNext.Width = 25;
                ButtonNext.Height = 20;
                ButtonNext.BringToFront();
                ButtonNext.Location = new Point(x12View.Width - ButtonNext.Width - ButtonFind.Width - 25, 0);

                var ButtonLast = new Button();
                ButtonLast.Parent = x12View;
                ButtonLast.Text = Char.ConvertFromUtf32(0x25B2);
                ButtonLast.Font = new System.Drawing.Font(FontFamily.GenericMonospace, 8f, FontStyle.Regular);
                ButtonLast.TextAlign = ContentAlignment.MiddleCenter;
                ButtonLast.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
                ButtonLast.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
                ButtonLast.Width = 25;
                ButtonLast.Height = 20;
                ButtonLast.BringToFront();
                ButtonLast.Location = new Point(x12View.Width - ButtonLast.Width - ButtonLast.Width - ButtonFind.Width - 25, 0);

                var SearchBox = new TextBox();
                SearchBox.BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
                SearchBox.ForeColor = GLOBALS.Config.Values.TextBoxForegroundColor;
                SearchBox.Text = SearchPhrase;
                SearchBox.Parent = x12View;
                SearchBox.Width = 200;
                SearchBox.BringToFront();
                SearchBox.Focus();
                SearchBox.Location = new Point(x12View.Width - SearchBox.Width - ButtonNext.Width - ButtonLast.Width - ButtonFind.Width - 25, 0);

                var IndexLabel = new Label();
                IndexLabel.Parent = x12View;
                if (SearchResults != null) {
                    IndexLabel.Text = (currentSearchIndex + 1) + "/" + SearchResults.ToList().Count.ToString();
                } else {
                    IndexLabel.Visible = false;
                }
                IndexLabel.BringToFront();
                IndexLabel.BackColor = GLOBALS.Config.Values.MenuBackgroundColor;
                IndexLabel.ForeColor = GLOBALS.Config.Values.MenuForegroundColor;
                IndexLabel.Width = 50;
                IndexLabel.Height = 15;
                IndexLabel.TextAlign = ContentAlignment.MiddleCenter;
                IndexLabel.Location = new Point(x12View.Width - IndexLabel.Width - ButtonNext.Width - ButtonLast.Width - ButtonFind.Width - 25, SearchBox.Height);

                SearchBox.KeyUp += new KeyEventHandler((o, ee) => {
                    x12View.CollapseAll();
                    SearchBox.BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
                    SearchBox.ForeColor = GLOBALS.Config.Values.TextBoxForegroundColor;
                    currentSearchIndex = 1;
                    SearchResults = x12File.FindSegments(x12File.Segments, SearchBox.Text);

                    if (SearchResults.FirstOrDefault() != null) {
                        ShowNodeMatchingSegment(x12View.Nodes, SearchResults.First());
                    } else {
                        SearchBox.BackColor = Color.Red;
                        SearchBox.ForeColor = Color.White;
                    }
                    SearchPhrase = SearchBox.Text;
                    IndexLabel.Visible = false;
                });

                ButtonFind.Click += new EventHandler((o, ee) => {
                    x12View.CollapseAll();
                    SearchResults = x12File.FindSegments(x12File.Segments, SearchBox.Text);
                    currentSearchIndex = 1;
                    if (SearchResults.FirstOrDefault() != null) {
                        ShowNodeMatchingSegment(x12View.Nodes, SearchResults.First());
                    } else {
                        SearchBox.BackColor = Color.Red;
                        SearchBox.ForeColor = Color.White;
                    }
                    SearchPhrase = SearchBox.Text;
                    IndexLabel.Text = (currentSearchIndex + 1) + "/" + SearchResults.ToList().Count.ToString();
                    IndexLabel.Visible = true;
                });

                ButtonNext.Click += new EventHandler((o, ee) => {
                    if (SearchResults != null) {
                        if (SearchResults.ElementAtOrDefault(currentSearchIndex + 1) != null) {
                            x12View.CollapseAll();
                            currentSearchIndex++;
                            ShowNodeMatchingSegment(x12View.Nodes, SearchResults.ElementAt(currentSearchIndex));
                        }
                    }
                    IndexLabel.Text = (currentSearchIndex + 1) + "/" + SearchResults.ToList().Count.ToString();
                    IndexLabel.Visible = true;
                });

                ButtonLast.Click += new EventHandler((o, ee) => {
                    if (SearchResults != null) {
                        if (SearchResults.ElementAtOrDefault(currentSearchIndex - 1) != null) {
                            x12View.CollapseAll();
                            currentSearchIndex--;
                            ShowNodeMatchingSegment(x12View.Nodes, SearchResults.ElementAt(currentSearchIndex));
                        }
                    }
                    IndexLabel.Text = (currentSearchIndex + 1) + "/" + SearchResults.ToList().Count.ToString();
                    IndexLabel.Visible = true;
                });
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e) {
            var replaceDialog = new ReplaceDialog(x12File);
            replaceDialog.ShowDialog();
            DisplayX12();
        }

        private void Main_Load(object sender, EventArgs e) {
        }

        private void configToolStripMenuItem1_Click(object sender, EventArgs e) {
            var ConfigMenu = new ConfigMenu(ApplyConfig);
            ConfigMenu.ShowDialog();

            ApplyConfig();
        }
    }
}