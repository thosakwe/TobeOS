using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class ProcessIO : IDisposable
    {
        public Stdin In { get; set; }

        public StdWriter Out { get; set; }

        public StdWriter Err { get; set; }

        public void Dispose()
        {
            In.Dispose();
            Out.Dispose();
            Err.Dispose();
        }
    }
}
