using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Graphics;

public class VPU
{
    private readonly MMU _mmu;

    public byte LCDC
    { 
        get => _mmu[0xFF40];
        set => _mmu[0xFF40] = value;
    }
    public byte STAT
    {
        get => _mmu[0xFF41];
        set => _mmu[0xFF41] = value;
    }
    public byte SCY
    { 
        get => _mmu[0xFF42];
        set => _mmu[0xFF42] = value;
    }
    public byte SCX
    { 
        get => _mmu[0xFF43];
        set => _mmu[0xFF43] = value;
    }
    public byte LY
    { 
        get => _mmu[0xFF44];
        set => _mmu[0xFF44] = value;
    }
    public byte LYC
    { 
        get => _mmu[0xFF45];
        set => _mmu[0xFF45] = value;
    }
    public byte BGP
    { 
        get => _mmu[0xFF47];
        set => _mmu[0xFF47] = value;
    }
    public byte OBP0
    { 
        get => _mmu[0xFF48];
        set => _mmu[0xFF48] = value;
    }
    public byte OBP1
    { 
        get => _mmu[0xFF49];
        set => _mmu[0xFF49] = value;
    }
    public byte WY
    { 
        get => _mmu[0xFF4A];
        set => _mmu[0xFF4A] = value;
    }
    public byte WX
    {
        get => _mmu[0xFF4B];
        set => _mmu[0xFF4B] = value;
    }

    public VPU(MMU mmu)
    {
        _mmu = mmu;
        LCDC = 0x91;
        STAT = 0x85;
        BGP = 0xFC;
        OBP0 = 0xFF;
        OBP1 = 0xFF;
    }

    public void Tick()
    {
    }
}
