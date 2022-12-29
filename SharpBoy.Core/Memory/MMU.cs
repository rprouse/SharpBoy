using System.Net;

namespace SharpBoy.Core.Memory;

/// <summary>
/// Controls access to all memory on the device
/// </summary>
/// <remarks>
/// 0x0000 - 0x00FF Boot ROM
/// 0x0000 - 0x3FFF 16KB ROM Bank 00 (in cartridge, fixed at bank 00)
/// 0x4000 - 0x7FFF 16KB ROM Bank 01..NN(in cartridge, switchable bank number)
/// 0x8000 - 0x9FFF 8KB Video RAM(VRAM)(switchable bank 0 - 1 in CGB Mode)
/// 0xA000 - 0xBFFF 8KB External RAM(in cartridge, switchable bank, if any)
/// 0xC000 - 0xCFFF 4KB Work RAM Bank 0(WRAM)
/// 0xD000 - 0xDFFF 4KB Work RAM Bank 1(WRAM)(switchable bank 1 - 7 in CGB Mode)
/// 0xE000 - 0xFDFF Same as C000 - DDFF(ECHO)(typically not used)
/// 0xFE00 - 0xFE9F Sprite Attribute Table(OAM)
/// 0xFEA0 - 0xFEFF Not Usable
/// 0xFF00 - 0xFF7F I/O Ports
/// 0xFF80 - 0xFFFE High RAM(HRAM)
/// 0xFFFF          Interrupt Enable Register
/// </remarks>
public class MMU
{
    private Cartridge _cartridge;
    private MemoryBlock _bootRom;
    private MemoryBlock _vram;
    private MemoryBlock _wram;
    private MemoryBlock _oam;
    private MemoryBlock _hram;

    public MMU()
    {
        _bootRom = new MemoryBlock(0x0000, 0x00FF);
        _vram    = new MemoryBlock(0x8000, 0x9FFF);
        _wram    = new MemoryBlock(0xC000, 0xCFFF);
        _oam     = new MemoryBlock(0xFE00, 0xFE9F);
        _hram    = new MemoryBlock(0xFF80, 0xFFFE);
    }

    public byte this[int address]
    {
        get => Read(address);
        set => Write(address, value);
    }

    private byte Read(int address)
    {
        return 0;
    }

    private void Write(int address, byte data)
    {
        // Cartridge memory unless the boot rom is still loaded
        if (address < 0x8000)
        {
        }
        // ECHO ram
        else if ((address >= 0xE000) && (address <= 0xFDFF))
        {
            _wram[address - 0x2000] = data;
        }
        // this area is restricted
        else if ((address >= 0xFEA0) && (address <= 0xFEFF))
        {
        }
        // no control needed over this area so write to memory
        else
        {
            //m_Rom[address] = data;
        }
    }
}
