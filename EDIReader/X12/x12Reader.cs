using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EDIReader.X12 {

    public enum SegmentType {
        Unknown = 0,
        Segment = 1,
        Loop = 2,
        HLoop = 3
    }

    public struct SegmentInfo {
        public SegmentInfo[] ChildInfo { get; private set; }
        public String Id { get; set; }
        public Guid InfoId { get; set; }
        public Boolean Initialized { get; set; }
        public String Level { get; set; }
        public String LoopId { get; set; }
        public String Name { get; set; }
        public Int16 Repeat { get; set; }
        public Boolean Required { get; set; }
        public Dictionary<Int32, String[]> SecondaryIds { get; private set; }
        public SegmentType SegmentType { get; set; }

        public SegmentInfo(XElement xmlElement)
            : this() {
            InfoId = Guid.NewGuid();
            Id = (xmlElement.Attribute("id") != null) ? xmlElement.Attribute("id").Value : String.Empty;
            LoopId = (xmlElement.Attribute("loopid") != null) ? xmlElement.Attribute("loopid").Value : String.Empty;
            Name = (xmlElement.Attribute("name") != null) ? xmlElement.Attribute("name").Value : String.Empty;
            Required = (xmlElement.Attribute("usage") != null) && (xmlElement.Attribute("usage").Value == "r");

            Int16 parsedValue = -1;
            Repeat = (xmlElement.Attribute("repeat") != null && Int16.TryParse(xmlElement.Attribute("repeat").Value, out parsedValue)) ? parsedValue : (Int16)(-1);

            switch (xmlElement.Name.ToString().ToLowerInvariant()) {
                case "segment":
                    SegmentType = X12.SegmentType.Segment;
                    break;

                case "loop":
                    SegmentType = X12.SegmentType.Loop;
                    break;

                case "hloop":
                    SegmentType = X12.SegmentType.HLoop;
                    break;
            }

            Level = (xmlElement.Attribute("level") != null) ? xmlElement.Attribute("level").Value : String.Empty;

            SecondaryIds = new Dictionary<int, string[]>();
            for (int j = 1; j < 20; j++) {
                if (xmlElement.Attribute("s" + j) != null) {
                    SecondaryIds.Add(j, xmlElement.Attribute("s" + j).Value.Split(':'));
                }
            }

            var TempList = new Collection<SegmentInfo>();
            foreach (var eChild in xmlElement.Elements()) {
                TempList.Add(new SegmentInfo(eChild));
            }
            ChildInfo = TempList.ToArray();

            Initialized = true;
        }

        public string[] SecondaryId(int index) {
            if (SecondaryIds == null) return new string[0];
            string[] returnList;
            if (SecondaryIds.TryGetValue(index, out returnList)) {
                return returnList;
            }
            return new string[0];
        }
    }

    public class Segment {
        public Dictionary<Guid, Int16> ChildCount { get; private set; }
        public Collection<Segment> ChildSegments { get; private set; }
        public String Error { get; set; }
        public bool HasChildren { get { return (ChildSegments != null) && ChildSegments.Count != 0; } }
        public SegmentInfo Info { get; set; }
        public Int32 LineNumber { get; set; }
        public String LineText { get; set; }
        public String Id { get; set; }
        public List<SegmentInfo> MissingRequiredChildren {
            get {
                var RequiredChildList = this.Info.ChildInfo.Where((x) => x.Required == true).ToList();
                if (this.ChildCount == null) return RequiredChildList;
                for (int i = RequiredChildList.Count() - 1; i >= 0; i--) {
                    if (this.ChildCount.ContainsKey(RequiredChildList[i].InfoId)) RequiredChildList.RemoveAt(i);
                }
                return RequiredChildList;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Segment"/> class.
        /// </summary>
        /// <param name="lineText">The line text.</param>
        /// <param name="LineNumber">The line number.</param>
        public Segment(String lineText, Int32 LineNumber = 0) {
            this.LineText = string.Intern(lineText);
            this.LineNumber = LineNumber;

            var index = lineText.IndexOf(x12File.elementDelimiter);
            Id = (index >= 0) ? string.Intern(lineText.Substring(0, index)) : String.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Segment"/> class.
        /// </summary>
        /// <param name="t">The TransactionSet.</param>
        /// <param name="i">Line to start from, within the file.</param>
        /// <param name = "segmentInfo"></param>
        /// <param name="Parent">Parent Segment</param>
        public Segment(Collection<Segment> t, ref int i, SegmentInfo segmentInfo) : this(t[i].LineText, t[i].LineNumber) {
            this.Info = segmentInfo;

            if (segmentInfo.SegmentType == SegmentType.Segment) {
                i++;
                return;
            }

            //Itterate Child Segments
            this.ChildSegments = new Collection<Segment>();
            for (i++; i < t.Count; i++) {
                var currentInfo = GetSegmentInfo(t[i], segmentInfo);

                if (currentInfo.Initialized) {
                    //Add This Child Segment to the ChildCount
                    if (ChildCount == null) ChildCount = new Dictionary<Guid, Int16>();
                    if (ChildCount.ContainsKey(currentInfo.InfoId)) {
                        ChildCount[currentInfo.InfoId]++;
                    } else {
                        ChildCount.Add(currentInfo.InfoId, 1);
                    }

                    //Check for Repeat Overflow
                    if (currentInfo.Repeat != -1 && ChildCount[currentInfo.InfoId] > currentInfo.Repeat) {
                        this.Error = string.Intern(t[i].Id + "*" + t[i].Element(1) + " has exceded the repeat maximum of (" + currentInfo.Repeat + ") specified in the format guide. " + ChildCount[currentInfo.InfoId]);
                    }

                    //Create the segment and add it.
                    ChildSegments.Add(new Segment(t, ref i, currentInfo));
                    --i;
                } else {
                    return;
                }
            }
        }

        ///<summary>
        ///Returns a string containing the element from the current segment.
        ///</summary>
        ///<param name="elementNumber">The element number that you want to return</param>
        public String Element(int elementNumber) {
            int count = 0;
            int startIndex = 0;
            int endIndex = 0;
            int currentIndex = 0;

            while (count < elementNumber && currentIndex != -1) {
                currentIndex = LineText.IndexOf(x12File.elementDelimiter, startIndex);
                startIndex = currentIndex + 1;
                count++;
            }

            if (currentIndex != -1) {
                endIndex = LineText.IndexOf(x12File.elementDelimiter, startIndex);
                if (endIndex == -1) endIndex = LineText.Length;
                return LineText.Substring(startIndex, endIndex - startIndex);
            } else {
                return String.Empty;
            }
        }

        ///<summary>
        ///Returns a decimal containing the element from the current segment.
        ///</summary>
        ///<param name="elementNumber">The element number that you want to return</param>
        public Decimal elementAsNumber(int elementNumber) {
            Decimal i = (Decimal.TryParse(Element(elementNumber), out i)) ? i : 0;
            return i;
        }

        public int Length() {
            return LineText.Count(x => x == x12File.elementDelimiter);
        }

        ///<summary>
        ///Replace an element within this segment.
        ///</summary>
        ///<param name="elementNumber">The element that you want to replace.</param>
        ///<param name="value">The string value with which you want to replace the element.</param>
        public bool replaceElement(int elementNumber, String value) {
            String[] elements = LineText.Split(x12File.elementDelimiter);
            if (elementNumber < elements.Length && elementNumber > 0) {
                elements[elementNumber] = value;
                this.LineText = String.Join(x12File.elementDelimiter.ToString(), elements);
                return true;
            }
            return false;
        }

        private static SegmentInfo GetSegmentInfo(Segment s, SegmentInfo ParentInfo) {
            if (String.Equals(s.Id, "HL", StringComparison.OrdinalIgnoreCase)) {

                //Check if we are on an HL segment
                for(int i = 0; i < ParentInfo.ChildInfo.Length; i++){
                    if (ParentInfo.ChildInfo[i].SegmentType == SegmentType.HLoop && String.Equals(ParentInfo.ChildInfo[i].Level, s.Element(3), StringComparison.OrdinalIgnoreCase)) return ParentInfo.ChildInfo[i];
                }
            } else {

                //Check if this segment is listed in the current segment's child segments with secondary value.
                for (int i = 0; i < ParentInfo.ChildInfo.Length; i++) {
                    if (String.Equals(ParentInfo.ChildInfo[i].Id, s.Id, StringComparison.OrdinalIgnoreCase) && MatchSecondaryId(ParentInfo.ChildInfo[i], s)) return ParentInfo.ChildInfo[i];
                }

                //Check for segment without secondary id
                for (int i = 0; i < ParentInfo.ChildInfo.Length; i++) {
                    if (String.Equals(ParentInfo.ChildInfo[i].Id, s.Id, StringComparison.OrdinalIgnoreCase) && ParentInfo.ChildInfo[i].SecondaryIds.Count == 0) return ParentInfo.ChildInfo[i];
                }
            }
            return default(SegmentInfo);
        }

        private static bool MatchSecondaryId(SegmentInfo SegInfo, Segment Segment) {
            foreach (var SecondaryIdArray in SegInfo.SecondaryIds) {
                var v = Segment.Element(SecondaryIdArray.Key);
                if (!String.IsNullOrEmpty(v) && SecondaryIdArray.Value.Contains(v, StringComparer.InvariantCultureIgnoreCase)) return true;
            }
            return false;
        }
    }

    public class x12File {
        public static Char elementDelimiter { get; set; }
        public static Segment GE { get; set; }
        public static Segment GS { get; set; }
        public static Segment IEA { get; set; }
        public static Segment ISA { get; set; }
        public static Char lineTerminator { get; set; }
        public static Char subDelimiter { get; set; }
        public String Format { get; set; }
        public String FormatGuide { get; set; }
        public Int32 LineCount { get; set; }
        public IEnumerable<Segment> Segments {
            get {
                if (FormatGuide != null) {
                    if (_Segments.Count > 0) {
                        foreach (var segment in _Segments) {
                            yield return segment;
                        }
                    } else {
                        foreach (var segment in readX12(FormatGuide)) {
                            if (HoldSegmentData) _Segments.Add(segment);
                            yield return segment;
                        }
                    }
                } else {
                    throw new Exception("FormatGuide must be provided for file segments to be read directly");
                }
            }
            private set {
                this.Segments = value;
            }
        }
        public String Version { get; set; }
        private List<Segment> _Segments { get; set; }
        private FileStream fileStream { get; set; }
        private bool HoldSegmentData { get; set; }
        private StreamReader streamReader { get; set; }
        private StringBuilder stringBuilder { get; set; }

        public x12File(String InputFile, String FormatGuide) {
            Initialize(InputFile, FormatGuide, false);
            ReadFormat();
        }

        public x12File(String InputFile, String FormatGuide, bool HoldSegmentData) {
            Initialize(InputFile, FormatGuide, HoldSegmentData);
            ReadFormat();
        }

        public event ProgressChangedEventHandler ProgressUpdated;

        public IEnumerable<Segment> FindSegments(String SegmentType, int ElementNumber, String SecondaryId = "Any", String regexExpression = "", IEnumerable<Segment> SegmentList = null) {
            if (SegmentList == null) SegmentList = Segments;
            foreach (var s in SegmentList) {
                if (s.Id == SegmentType) {
                    if (SecondaryId == "Any" || SecondaryId == "" || s.Element(1).ToLowerInvariant() == SecondaryId.ToLowerInvariant()) {
                        if (regexExpression != "") {
                            var r = new Regex(regexExpression);
                            if (r.IsMatch(s.Element(ElementNumber))) {
                                yield return s;
                            }
                        } else {
                            yield return s;
                        }
                    }
                }
                if (s.HasChildren) {
                    foreach (var ss in FindSegments(SegmentType, ElementNumber, SecondaryId, regexExpression, s.ChildSegments)) {
                        yield return ss;
                    }
                }
            }
        }

        public IEnumerable<Segment> FindSegments(IEnumerable<Segment> Segments, String SearchText) {
            foreach (var Segment in Segments) {
                if (Segment.LineText.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0) {
                    yield return Segment;
                }
                if (Segment.HasChildren) {
                    foreach (var returnedSegment in FindSegments(Segment.ChildSegments, SearchText)) {
                        yield return returnedSegment;
                    }
                }
            }
        }

        public IEnumerable<String> toX12() {
            var sb = new StringBuilder();
            foreach (var s in this.Segments) {
                yield return s.LineText + x12File.lineTerminator;
                if (s.ChildSegments != null) {
                    foreach (var segment in toX12(s.ChildSegments)) {
                        yield return segment;
                    }
                }
            }
        }

        public void toX12File(String outputPath) {
            var sr = new StreamWriter(outputPath);
            sr.Write(ISA.LineText + x12File.lineTerminator);
            sr.Write(GS.LineText + x12File.lineTerminator);
            foreach (var segment in toX12(this.Segments)) {
                sr.Write(segment);
            }
            sr.Write(GE.LineText + x12File.lineTerminator);
            sr.Write(IEA.LineText + x12File.lineTerminator);
            sr.Flush();
            sr.Close();
        }

        private int byteOrderMarkOffset() {
            if (readCharAtLocation(0) == 0xEF &&
                readCharAtLocation(1) == 0xBB &&
                readCharAtLocation(2) == 0xBF) {
                return 3;
            }
            return 0;
        }

        private void Initialize(String InputFile, String FormatGuide, bool HoldSegmentData) {
            this.HoldSegmentData = HoldSegmentData;
            this.FormatGuide = FormatGuide;
            this.stringBuilder = new StringBuilder();
            this._Segments = new List<Segment>();

            fileStream = File.Open(InputFile, FileMode.Open, FileAccess.Read);
            streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8, false, 4096);
            lineTerminator = readCharAtLocation(105 + byteOrderMarkOffset());
            subDelimiter = readCharAtLocation(104 + byteOrderMarkOffset());
            elementDelimiter = readCharAtLocation(103 + byteOrderMarkOffset());
        }

        private Char readCharAtLocation(long location) {
            long oldLocation = fileStream.Position;
            fileStream.Position = location;
            if (fileStream.Position < fileStream.Length) {
                Char c = (char)fileStream.ReadByte();
                fileStream.Position = oldLocation;
                return c;
            }
            fileStream.Position = oldLocation;
            return '\0';
        }

        private void ReadFormat() {
            while (!streamReader.EndOfStream && Format == null) {
                var s = new Segment(readLine(), 0);
                switch (s.Id) {
                    case "ST":
                        if (s.Length() >= 1) {
                            Format = s.Element(1);
                        }
                        if (s.Length() >= 3) {
                            Version = s.Element(3).Substring(0, 10);
                        }
                        break;
                }
            }

            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            LineCount = 0;
        }

        private String readLine() {
            stringBuilder.Clear();
            int n;
            while ((n = streamReader.Read()) != -1) {
                if (n == lineTerminator) {
                    LineCount++;
                    return stringBuilder.ToString();
                }
                stringBuilder.Append((char)n);
            }
            LineCount++;
            return stringBuilder.ToString();
        }

        private IEnumerable<Segment> readX12(String FormatGuidePath) {
            var X12Format = new X12Format(FormatGuidePath);
            int updateCount = 0;
            var t = new Collection<Segment>();

            if (ProgressUpdated != null) ProgressUpdated(this, new ProgressChangedEventArgs((int)(100 * ((float)streamReader.BaseStream.Position / (float)streamReader.BaseStream.Length)), "Begining"));
            using (streamReader) {
                while (!streamReader.EndOfStream) {
                    updateCount++;
                    if (updateCount >= 50000) {
                        if (ProgressUpdated != null) ProgressUpdated(this, new ProgressChangedEventArgs((int)(100 * ((float)streamReader.BaseStream.Position / (float)streamReader.BaseStream.Length)), "Working"));
                        updateCount = 0;
                    }

                    var s = new Segment(readLine(), LineCount);
                    switch (s.Id) {
                        case "ISA":
                            ISA = s;
                            ISA.LineText = ISA.LineText.Substring(byteOrderMarkOffset(), ISA.LineText.Length - byteOrderMarkOffset());
                            ISA.Info = new SegmentInfo() { Name = "INTERCHANGE CONTROL HEADER" };
                            break;

                        case "IEA":
                            IEA = s;
                            IEA.Info = new SegmentInfo() { Name = "INTERCHANGE CONTROL TRAILER" };
                            break;

                        case "GS":
                            GS = s;
                            GS.Info = new SegmentInfo() { Name = "FUNCTIONAL GROUP HEADER" };
                            break;

                        case "GE":
                            GE = s;
                            GS.Info = new SegmentInfo() { Name = "FUNCTIONAL GROUP TRAILER" };
                            break;

                        case "ST":
                            t.Clear();
                            t.Add(s);
                            break;

                        case "SE":
                            t.Add(s);
                            int i = 0;
                            yield return new Segment(t, ref i, X12Format.First());
                            break;

                        default:
                            t.Add(s);
                            break;
                    }
                }
                if (ProgressUpdated != null) ProgressUpdated(this, new ProgressChangedEventArgs((int)(100 * ((float)streamReader.BaseStream.Position / (float)streamReader.BaseStream.Length)), "Complete"));
            }
        }

        private IEnumerable<String> toX12(IEnumerable<Segment> SegmentList) {
            var sb = new StringBuilder();
            foreach (var s in SegmentList) {
                yield return s.LineText + x12File.lineTerminator;
                if (s.ChildSegments != null) {
                    foreach (var segment in toX12(s.ChildSegments)) {
                        yield return segment;
                    }
                }
            }
        }
    }

    public class X12Format : Collection<SegmentInfo> {

        public X12Format(String FormatGuide) {
            //Read in the FormatGuide
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(FormatGuide).ToLowerInvariant()))) {
                XDocument f = XDocument.Load(ms);
                //Create the SegmentInfos
                foreach (XElement e in f.Root.Elements()) {
                    this.Add(new SegmentInfo(e));
                }
            }
        }
    }
}