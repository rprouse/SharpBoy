using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class CbOpcodeHandler
{
    private readonly Registers _reg;
    private readonly MMU _mmu;
    private readonly VPU _vpu;
    private readonly Interupts _int;

    private Dictionary<byte, Opcode> _opcodes;

    private byte _result;

    public CbOpcodeHandler(Registers registers, MMU mmu, VPU vpu, Interupts interupts)
    {
        _reg = registers;
        _mmu = mmu;
        _vpu = vpu;
        _int = interupts;
        _opcodes = Initialize();
    }

    private void BIT(int bit, byte value)
    {
        _reg.FlagZ = (value & 1 << bit) == 0;
        _reg.FlagN = false;
        _reg.FlagH = true;
    }
    
    // Reset bit in value
    private byte RES(int bit, byte value) =>
        (byte)(value & ~(1 << bit));

    // Set bit in value
    private byte SET(int bit, byte value) =>
        (byte)(value | (1 << bit));
}
