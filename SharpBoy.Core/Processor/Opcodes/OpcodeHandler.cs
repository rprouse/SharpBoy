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
    private bool _stop;

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
        _reg.FlagH = (_reg.A & 0x0F) + (value & 0x0F) + carry > 0x0F;
        _reg.FlagC = result > 0xFF;
        _reg.A = (byte)result;
    }

    private void ADD(byte value)
    {
        int result = _reg.A + value;
        _reg.FlagZ = result == 0;
        _reg.FlagN = false;
        _reg.FlagH = (_reg.A & 0x0F) + (value & 0x0F) > 0x0F;
        _reg.FlagC = result > 0xFF;
        _reg.A = (byte)result;
    }

    private void ADDHL(word value)
    {
        int result = _reg.HL + value;
        _reg.FlagN = false;
        _reg.FlagH = (_reg.HL & 0x0FFF) + (value & 0x0FFF) > 0x0FFF;
        _reg.FlagC = result > 0xFFFF;
        _reg.HL = (word)result;
    }

    private void ADDSP(byte value)
    {
        int result = _reg.SP + value;
        _reg.FlagZ = false;
        _reg.FlagN = false;
        _reg.FlagH = (_reg.SP & 0x000F) + (value & 0x0F) > 0x000F;
        _reg.FlagC = (_reg.SP & 0x00FF) + value > 0x00FF;
        _reg.SP = (word)result;
    }

    private void AND(byte value)
    {
        _reg.A &= value;
        _reg.FlagZ = _reg.A == 0;
        _reg.FlagN = false;
        _reg.FlagH = true;
        _reg.FlagC = false;
    }

    private void CP(byte value)
    {
        _reg.FlagZ = _reg.A == value;
        _reg.FlagN = true;
        _reg.FlagH = (value & 0x0F) > (_reg.A & 0x0F);
        _reg.FlagC = value > _reg.A;
    }

    private void DAA()
    {
        if (_reg.FlagN)
        {
            _reg.A -= (byte)(_reg.FlagC ? 0x60 : 0x06);
        }
        else
        {
            if (_reg.FlagC || (_reg.A > 0x99))
            {
                _reg.A += 0x60;
                _reg.FlagC = true;
            }
            if (_reg.FlagH || (_reg.A & 0x0F) > 0x09)
            {
                _reg.A += 0x6;
            }
        }
        _reg.FlagZ = _reg.A == 0;
        _reg.FlagH = false;
    }

    private byte DEC(byte value)
    {
        int result = value - 1;
        _reg.FlagZ = result == 0;
        _reg.FlagN = true;
        _reg.FlagH = (value & 0x0F) == 0x00;
        return (byte)result;
    }

    private byte INC(byte value)
    {
        int result = value + 1;
        _reg.FlagZ = result == 0;
        _reg.FlagN = false;
        _reg.FlagH = (value & 0x0F) == 0x0F;
        return (byte)result;
    }
}
