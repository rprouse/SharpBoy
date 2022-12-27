using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoy.Core.Memory;

public class MMU
{
    public byte this[int offset]
    {
        get => Read(offset);
        set => Write(offset, value);
    }

    private byte Read(int offset)
    {
        return 0;
    }

    private void Write(int offset, byte value)
    {
    }
}
