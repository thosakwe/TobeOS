using System;
using System.Collections.Generic;
using System.Text;

namespace TobeOS.Programs
{
    public class EditProgram : Program
    {
        public override string GetName()
        {
            return "edit";
        }

        public override int Run(KernelState state, string[] arguments)
        {
            using (var buf = new StringBuf())
            {
                int i = 0;

                while (state.Io.In.CanRead)
                {
                    var line = state.Io.In.ReadLine();

                    if (line.Trim().ToLower() == ":x")
                        break;

                    if (i++ > 0) buf.Add('\n');
                    buf.AddString(line);
                }

                state.Io.Out.Write(buf.ToString());
                return (int)ExitCodes.SUCCESS;
            }
        }
    }
}
