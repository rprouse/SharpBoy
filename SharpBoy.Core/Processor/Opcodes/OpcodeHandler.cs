using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    private readonly Registers _reg;
    private readonly MMU _mmu;
    private readonly VPU _vpu;
    private readonly Interupts _int;

    // Some variables to carry values between ticks
    private byte _lsb;
    private byte _msb;
    private byte _operand;
    private word _address;
    private word _result;

    private Dictionary<byte, Opcode> _opcodes;

    public OpcodeHandler(Registers registers, MMU mmu, VPU vpu, Interupts interupts)
    {
        _reg = registers;
        _mmu = mmu;
        _vpu = vpu;
        _int = interupts;
        _opcodes = Initialize();
    }

    private void ADC(byte value)
    {
        int carry = _reg.FlagC ? 1 : 0;
        int result = _reg.A + value + carry;
        _reg.FlagZ = result == 0;
        _reg.FlagN = false;
        _reg.FlagH = (_reg.A & 0b_0000_1111) + (value & 0b_0000_1111) + carry > 0b_0000_1111;
        _reg.FlagC = result > 0xFF;
        _reg.A = (byte)result;
    }

    private void ADD(byte value)
    {
        int result = _reg.A + value;
        _reg.FlagZ = result == 0;
        _reg.FlagN = false;
        _reg.FlagH = (_reg.A & 0b_0000_1111) + (value & 0b_0000_1111) > 0b_0000_1111;
        _reg.FlagC = result > 0xFF;
        _reg.A = (byte)result;
    }
}
