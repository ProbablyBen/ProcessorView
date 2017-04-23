using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorView
{
    public enum ProcessorType : ushort
    {
        Other = 1,
        Unknown,
        CentralProcessor,
        MathProcessor,
        DSPProcessor,
        VideoProcessor
    }
}
