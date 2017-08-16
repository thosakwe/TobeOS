using System;

namespace TobeOS.Programs
{
    public class VolumesProgram : Program
    {
        public override string GetName()
        {
            return "volumes";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            foreach (var volume in state.FileSystem.GetVolumes())
                Console.WriteLine(volume.mName);

            return 0;
        }
    }
}
