using System;
using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;
using SharpBoy.Core.Utilities;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class OpcodeHandler : BaseOpcodeHandler
{
    // Some variables to carry values between ticks
    private byte _lsb;
    private byte _msb;
    private byte _operand;
    private word _address;
    private bool _stop;

    public OpcodeHandler(Registers registers, MMU mmu, VPU vpu, Interupts interupts)
        : base(registers, mmu, vpu, interupts) { }

    /// <summary>
    /// Reads the next byte from memory and increments PC
    /// </summary>
    /// <returns></returns>
    private byte NextByte() => _mmu[_reg.PC++];

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
                _reg.A += 0x06;
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

    private void OR(byte value)
    {
        _reg.A = (byte)(_reg.A | value);
        _reg.FlagZ = _reg.A == 0;
        _reg.FlagN = false;
        _reg.FlagH = false;
        _reg.FlagC = false;
    }

    private void XOR(byte value)
    {
        _reg.A = (byte)(_reg.A | value);
        _reg.FlagZ = _reg.A == 0;
        _reg.FlagN = false;
        _reg.FlagH = false;
        _reg.FlagC = false;
    }

    private Tick[] RST(word address) =>
        new Tick[] {
            () => { _mmu[--_reg.SP] = _reg.PC.Msb(); },
            () => { _mmu[--_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = address; }
        };

    private void SBC(byte value)
    {
        int carry = _reg.FlagC ? 1 : 0;
        int result = _reg.A - value - carry;
        _reg.FlagZ = result == 0;
        _reg.FlagN = true;
        _reg.FlagH = ((value + carry) & 0x0F) > (_reg.A & 0x0F);
        _reg.FlagC = value + carry > _reg.A;
        _reg.A = (byte)result;
    }

    private void SUB(byte value)
    {
        int result = _reg.A - value;
        _reg.FlagZ = result == 0;
        _reg.FlagN = true;
        _reg.FlagH = (value & 0x0F) > (_reg.A & 0x0F);
        _reg.FlagC = value > _reg.A;
        _reg.A = (byte)result;
    }
}
