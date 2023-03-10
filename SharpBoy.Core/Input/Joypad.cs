using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Input;

public class Joypad
{
    private readonly MMU _mmu;

    public byte JOYP
    {
        get => _mmu[0xFF00];
        set => _mmu[0xFF00] = value;
    }

    public Joypad(MMU mmu)
    {
        _mmu = mmu;
        JOYP = 0xCF;
    }
}
