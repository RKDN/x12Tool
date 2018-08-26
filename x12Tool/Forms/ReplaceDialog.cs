using EDIReader.X12;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace x12Tool {

    public partial class ReplaceDialog : Form {
        List<ListViewItem> VirtualList = new List<ListViewItem>();
        x12File x12File;

        public ReplaceDialog(x12File x12File) {
            InitializeComponent();
            this.x12File = x12File;
            ApplyConfig();
        }

        private void ApplyConfig() {
            resultsList.BackColor = GLOBALS.Config.Values.EditorBackgroundColor;
            resultsList.ForeColor = GLOBALS.Config.Values.EditorForegroundColor;

            _gen_func.SetFormatByControlType(this);

        }

        private void elementNumberDropDown_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedSegment = ((SegmentType)segmentTypesDropDown.SelectedItem);
            lengthVal.Value = selectedSegment.Value.Element(elementNumberDropDown.SelectedIndex + 1).Length;
        }

        private void fakerFields_Enter(object sender, EventArgs e) {
            generatedRadio.Checked = true;
        }

        private void findBtn_Click(object sender, EventArgs e) {
            findOrReplace();
        }

        private void findOrReplace(bool replace = false) {
            findBtn.Enabled = false;
            ReplaceBtn.Enabled = false;
            pBar.Visible = true;
            pBar.Maximum = (int)x12File.LineCount;
            resultsList.VirtualListSize = 0;

            var ElementNumber = 0;
            if (int.TryParse(elementNumberDropDown.Text, out ElementNumber) || !replace) {
                var SegmentType = segmentTypesDropDown.Text;
                var SecondaryId = secondaryIdDropdown.Text;
                var RegexFilter = regexFilterTxt.Text;
                var FakerSelected = (fakerFields.SelectedItems.Count > 0) ? fakerFields.SelectedItems[0].Text : null;
                var SimpleReplace = replaceRadio.Checked;

                resultsList.Items.Clear();
                resultsList.HeaderStyle = ColumnHeaderStyle.Nonclickable;

                Thread t = new Thread(() => {
                    VirtualList.Clear();
                    foreach (var s in x12File.FindSegments(SegmentType, ElementNumber, SecondaryId, RegexFilter)) {
                        if (SimpleReplace) {
                            //Replace using the text that was provided.
                            Invoke(new Action(() => pBar.Value = (int)s.LineNumber));
                            if (replace) s.replaceElement(ElementNumber, replaceTxt.Text);
                            VirtualList.Add(SegmentToLVI(s));
                        } else if (FakerSelected != null) {
                            //Replace each element with new generated faker data.
                            Invoke(new Action(() => pBar.Value = (int)s.LineNumber));
                            var fakerValue = runFaker(FakerSelected);
                            if (fakerValue.Length > lengthVal.Value) fakerValue = fakerValue.Substring(0, (int)lengthVal.Value);
                            if (replace) s.replaceElement(ElementNumber, fakerValue);
                            VirtualList.Add(SegmentToLVI(s));
                        }
                    }

                    Invoke(new Action(() => {
                        resultsList.VirtualListSize = VirtualList.Count;
                        _gen_func.colorize_list_view(resultsList, GLOBALS.Config);
                        _gen_func.smart_column_resize(resultsList);

                        StatusTxt.Text = "Count: " + resultsList.Items.Count;
                        findBtn.Enabled = true;
                        ReplaceBtn.Enabled = true;
                        this.pBar.Visible = false;
                    }));
                });
                t.Start();
            } else {
                MessageBox.Show("You must select an element to replace.");
                findBtn.Enabled = true;
                ReplaceBtn.Enabled = true;
                this.pBar.Visible = false;
            }
        }

        private IEnumerable<SegmentType> GatherSegmentTypes(IEnumerable<Segment> Segments) {
            foreach (var s in Segments) {
                yield return new SegmentType(s.Id, s);
                if (s.HasChildren) {
                    foreach (var ss in GatherSegmentTypes(s.ChildSegments)) {
                        yield return ss;
                    }
                }
            }
        }

        private void lengthVal_ValueChanged(object sender, EventArgs e) {
        }

        private void regexFilterTxt_TextChanged(object sender, EventArgs e) {
            findBtn.Enabled = true;
            ReplaceBtn.Enabled = true;
            errorTxt.Clear();
            regexFilterTxt.BackColor = GLOBALS.Config.Values.TextBoxBackgroundColor;
            regexFilterTxt.ForeColor = GLOBALS.Config.Values.EditorForegroundColor;
            try {
                var r = new Regex(regexFilterTxt.Text);
            } catch (Exception ex) {
                regexFilterTxt.BackColor = Color.Red;
                regexFilterTxt.ForeColor = Color.White;
                findBtn.Enabled = false;
                ReplaceBtn.Enabled = false;

                errorTxt.Text = ex.Message;
            }
        }

        private void ReplaceBtn_Click(object sender, EventArgs e) {
            findOrReplace(true);
        }

        private void ReplaceDialog_Load(object sender, EventArgs e) {
            this.StatusTxt.Text = "Loading Segment Types...";
            this.findBtn.Enabled = false;
            this.ReplaceBtn.Enabled = false;
            this.pBar.Visible = true;
            this.pBar.Maximum = (int)x12File.LineCount;
            this.segmentTypesDropDown.Enabled = false;

            Thread t = new Thread(() => {
                var updateFrequency = 0;
                var TempList = new List<String>();
                var segmentList = new List<SegmentType>();

                foreach (var segmentType in GatherSegmentTypes(x12File.Segments)) {
                    if (x12File.LineCount < 100000 || updateFrequency > 2000) {
                        Invoke(new Action(() => this.pBar.Value = (int)segmentType.Value.LineNumber));
                        updateFrequency = 0;
                    } else {
                        updateFrequency++;
                    }
                    if (!TempList.Contains(segmentType.Text)) {
                        if (segmentType.Value.Info.SecondaryId(1) != null) segmentType.SecondaryIds = segmentType.Value.Info.SecondaryId(1).ToList();
                        TempList.Add(segmentType.Text);
                        segmentList.Add(segmentType);
                    } else {
                        foreach (var segment in segmentList) {
                            if (((SegmentType)segment).Text == segmentType.Text && segmentType.Value.Info.SecondaryId(1) != null) {
                                foreach (var secondaryId in segmentType.Value.Info.SecondaryId(1).ToList()) {
                                    if (!((SegmentType)segment).SecondaryIds.Contains(secondaryId)) {
                                        ((SegmentType)segment).SecondaryIds.Add(secondaryId);
                                    }
                                }
                            }
                        }
                    }
                }

                Invoke(new Action(() => {
                    segmentTypesDropDown.Items.AddRange(segmentList.ToArray());
                    this.StatusTxt.Text = "";
                    this.findBtn.Enabled = true;
                    this.ReplaceBtn.Enabled = true;
                    this.pBar.Visible = false;
                    this.segmentTypesDropDown.Enabled = true;
                }));
            });
            t.Start();

            foreach (var Field in typeof(Person).GetFields()) {
                fakerFields.Items.Add(Field.Name);
            }
            _gen_func.colorize_list_view(fakerFields, GLOBALS.Config);
        }

        private void resultsList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if (VirtualList.Count >= e.ItemIndex) {
                e.Item = VirtualList[e.ItemIndex];
            }
        }

        private static String runFaker(String FakerName) {
            var Person = new Person();
            return typeof(Person).GetField(FakerName).GetValue(Person).ToString();
        }

        private void secondaryIdDropdown_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private static ListViewItem SegmentToLVI(Segment s) {
            ListViewItem lvi = new ListViewItem(s.LineNumber.ToString());
            lvi.SubItems.Add((s.Info.LoopId != String.Empty) ? s.Info.LoopId : "--");
            lvi.SubItems.Add(s.LineText);
            lvi.SubItems.Add((!string.IsNullOrEmpty(s.Info.Name)) ? s.Info.Name : "--");
            return lvi;
        }

        private void segmentTypesDropDown_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedSegment = ((SegmentType)segmentTypesDropDown.SelectedItem);

            elementNumberDropDown.Enabled = true;
            elementNumberDropDown.Items.Clear();
            for (int i = 1; i < selectedSegment.Value.Length(); i++) {
                elementNumberDropDown.Items.Add(i);
            }
            elementNumberDropDown.SelectedIndex = 0;

            secondaryIdDropdown.Enabled = false;
            secondaryIdDropdown.Items.Clear();
            secondaryIdDropdown.Items.Add("Any");
            if (selectedSegment.SecondaryIds != null) {
                selectedSegment.SecondaryIds.Sort();
                if (selectedSegment.SecondaryIds != null) {
                    secondaryIdDropdown.Enabled = true;
                    foreach (var secondaryId in selectedSegment.SecondaryIds) {
                        secondaryIdDropdown.Items.Add(secondaryId.ToUpperInvariant());
                    }
                }
                secondaryIdDropdown.SelectedIndex = 0;
            }
        }

        [Serializable]
        public class Person {
            public string Address;
            public string AlphaNumeric;
            public string City;
            public string DOB;
            public string FirstName;
            public string LastName;
            public string MI;
            public string Numeric;
            public string PatientId;
            public string RId;
            public string SSN;
            public string State;
            public string Zip;

            public Person() {
                Address = Faker.LocationFaker.Street();
                City = Faker.LocationFaker.City();
                DOB = Faker.DateTimeFaker.BirthDay(10, 90).ToString("MMddyyyy");
                FirstName = Faker.NameFaker.FirstName().ToLowerInvariant();
                LastName = Faker.NameFaker.LastName().ToLowerInvariant();
                MI = Faker.NameFaker.Name().Substring(0, 1).ToLowerInvariant();
                PatientId = Faker.StringFaker.Numeric(17);
                RId = Faker.StringFaker.Numeric(12);
                AlphaNumeric = Faker.StringFaker.AlphaNumeric(100);
                Numeric = Faker.StringFaker.Numeric(100);
                SSN = Faker.StringFaker.Numeric(9);
                State = "TN";
                Zip = Faker.StringFaker.Numeric(9);
            }
        }

        public class SegmentDef {

            public SegmentDef(String Text, string[] SecondaryIds) {
                this.Text = Text;
                this.SecondaryIds = SecondaryIds.ToList();
            }

            public int Length { get; set; }
            public List<string> SecondaryIds { get; set; }
            public string Text { get; set; }

            public override string ToString() {
                return Text;
            }
        }

        public class SegmentType {

            public SegmentType(String Text, Segment Value) {
                this.Text = Text;
                this.Value = Value;
            }

            public List<string> SecondaryIds { get; set; }
            public string Text { get; set; }
            public Segment Value { get; set; }

            public override string ToString() {
                return Text;
            }
        }
    }
}