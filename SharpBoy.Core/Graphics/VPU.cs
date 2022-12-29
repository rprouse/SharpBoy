namespace SharpBoy.Core.Graphics;

public class VPU
{
    public byte LCDC { get; set; }
    public byte STAT { get; set; }
    public byte SCY { get; set; }
    public byte SCX { get; set; }
    public byte LY { get; set; }
    public byte LYC { get; set; }
    public byte BGP { get; set; }
    public byte OBP0 { get; set; }
    public byte OBP1 { get; set; }
    public byte WY { get; set; }
    public byte WX { get; set; }
}
