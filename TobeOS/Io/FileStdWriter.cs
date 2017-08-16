using System;
using System.IO;

namespace TobeOS.Io
{
    public class FileStdWriter : StringStdWriter
    {
        private string path;

        FileStdWriter(string path, int bufferSize = 255)
        {
            this.path = path;
        }

        public override void Dispose()
        {
            File.WriteAllText(path, ToString());
            base.Dispose();
        }
    }
}
