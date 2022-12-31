namespace SharpBoy.Core.Processor;

public class Registers
{
    public byte A { get; set; }
    public byte F { get; set; }
    public byte B { get; set; }
    public byte C { get; set; }
    public byte D { get; set; }
    public byte E { get; set; }
    public byte H { get; set; }
    public byte L { get; set; }

    public word AF
    {
        get => (word)(A << 8 | F);
        set
        {
            A = (byte)(value >> 8);
            F = (byte)(value & 0x00FF);
        }
    }

    public word BC
    {
        get => (word)(B << 8 | C);
        set
        {
            B = (byte)(value >> 8);
            C = (byte)(value & 0x00FF);
        }
    }

    public word DE
    {
        get => (word)(D << 8 | E);
        set
        {
            D = (byte)(value >> 8);
            E = (byte)(value & 0x00FF);
        }
    }

    public word HL
    {
        get => (word)(H << 8 | L);
        set
        {
            H = (byte)(value >> 8);
            L = (byte)(value & 0x00FF);
        }
    }

    public word SP { get; set; }
    public word PC { get; set; }

    // Zero
    public bool FlagZ
    {
        get => (F & 0b1000_0000) != 0;
        set => F = (byte)(value ? F | 0b1000_0000 : F & 0b0111_1111);
    }

    // Subtract
    public bool FlagN
    {
        get => (F & 0b0100_0000) != 0;
        set => F = (byte)(value ? F | 0b0100_0000 : F & 0b1011_1111);
    }

    // Half-Carry
    public bool FlagH
    {
        get => (F & 0b0010_0000) != 0;
        set => F = (byte)(value ? F | 0b0010_0000 : F & 0b1101_1111);
    }

    // Carry
    public bool FlagC
    {
        get => (F & 0b0001_0000) != 0;
        set => F = (byte)(value ? F | 0b0001_0000 : F & 0b1110_1111);
    }
}