using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class CbOpcodeHandler : BaseOpcodeHandler
{
    // Some variables to carry values between ticks
    private byte _operand;
    private byte _result;

    public CbOpcodeHandler(Registers registers, MMU mmu, VPU vpu, Interupts interupts)
        : base(registers, mmu, vpu, interupts) { }

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
