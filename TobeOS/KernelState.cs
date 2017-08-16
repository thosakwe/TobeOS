using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS
{
    public class KernelState
    {
        public Cosmos.System.FileSystem.CosmosVFS FileSystem { get; set; }

        public int LastExitCode { get; set; }

        public Programs.Program[] Programs { get; set; }

        public String WorkingDirectory { get; set; }
    }
}
