namespace SharpBoy.Core.Processor;

public partial class Registers
{
    public uint8 A { get; set; }
    public uint8 F { get; set; }
    public uint8 B { get; set; }
    public uint8 C { get; set; }
    public uint8 D { get; set; }
    public uint8 E { get; set; }
    public uint8 H { get; set; }
    public uint8 L { get; set; }

    public uint16 AF
    {
        get => (uint16)(A << 8 | F);
        set
        {
            A = (uint8)(value >> 8);
            F = (uint8)(value & 0x00FF);
        }
    }

    public uint16 BC
    {
        get => (uint16)(B << 8 | C);
        set
        {
            B = (uint8)(value >> 8);
            C = (uint8)(value & 0x00FF);
        }
    }

    public uint16 DE
    {
        get => (uint16)(D << 8 | E);
        set
        {
            D = (uint8)(value >> 8);
            E = (uint8)(value & 0x00FF);
        }
    }

    public uint16 HL
    {
        get => (uint16)(H << 8 | L);
        set
        {
            H = (uint8)(value >> 8);
            L = (uint8)(value & 0x00FF);
        }
    }

    public uint16 SP { get; set; }
    public uint16 PC { get; set; }

    // Zero
    public bool FlagZ
    {
        get => (F & 0b1000_0000) != 0;
        set => F = (uint8)(value ? F | 0b1000_0000 : F & 0b0111_1111);
    }

    // Subtract
    public bool FlagN
    {
        get => (F & 0b0100_0000) != 0;
        set => F = (uint8)(value ? F | 0b0100_0000 : F & 0b1011_1111);
    }

    // Half-Carry
    public bool FlagH
    {
        get => (F & 0b0010_0000) != 0;
        set => F = (uint8)(value ? F | 0b0010_0000 : F & 0b1101_1111);
    }

    // Carry
    public bool FlagC
    {
        get => (F & 0b0001_0000) != 0;
        set => F = (uint8)(value ? F | 0b0001_0000 : F & 0b1110_1111);
    }
}