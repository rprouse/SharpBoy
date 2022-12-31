namespace SharpBoy.Core.Processor;

public class Clock
{
    public byte DIV { get; set; }
    public byte TIMA { get; set; }
    public byte TMA { get; set; }
    public byte TAC { get; set; }
    public void Tick()
    {
    }
}
