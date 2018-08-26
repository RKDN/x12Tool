using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace EDIReader.IO {
    public static class ResourceManagement {

        public static string[] ReadAllResourceLines(string resourceName) {
            var assembly = typeof(EDIReader.IO.ResourceManagement).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream)) {
                return EnumerateLines(reader).ToArray();
            }
        }

        private static IEnumerable<string> EnumerateLines(TextReader reader) {
            string line;

            while ((line = reader.ReadLine()) != null) {
                yield return line;
            }
        }

    }
}
