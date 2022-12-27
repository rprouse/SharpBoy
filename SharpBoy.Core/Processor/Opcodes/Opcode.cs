namespace SharpBoy.Core.Processor.Opcodes;

public delegate void Tick();

public class Opcode
{
    public uint8 Value { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    public int Cycles { get; set; }

    public Tick[] Ticks { get; set; }

    public Opcode(uint8 value, string name, int length, int cycles, Tick[] ticks)
    {
        Value = value;
        Name = name;
        Length = length;
        Cycles = cycles;
        Ticks = ticks;
    }
}
