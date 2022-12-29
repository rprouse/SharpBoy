using System.Net;
using SharpBoy.Core.Graphics;
using SharpBoy.Core.Input;
using SharpBoy.Core.Processor;
using SharpBoy.Core.Serial;

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
    private readonly MemoryBlock _bootRom;
    private readonly MemoryBlock _vram;
    private readonly MemoryBlock _wram;
    private readonly MemoryBlock _oam;
    private readonly MemoryBlock _io;
    private readonly MemoryBlock _hram;

    private readonly Clock _clock;
    private readonly Interupts _int;
    private readonly Dma _dma;
    private readonly VPU _vpu;
    private readonly Joypad _joypad;
    private readonly SerialLink _serial;

    private bool _bootRomBankedIn = false;

    public MMU(Clock clock, Interupts interupts, Dma dma, VPU vpu, Joypad joypad, SerialLink serial)
    {
        _clock = clock;
        _int = interupts;
        _dma = dma;
        _vpu = vpu;
        _joypad = joypad;
        _serial = serial;

        _bootRom = new MemoryBlock(0x0000, 0x00FF);
        _vram    = new MemoryBlock(0x8000, 0x9FFF);
        _wram    = new MemoryBlock(0xC000, 0xCFFF);
        _oam     = new MemoryBlock(0xFE00, 0xFE9F);
        _io      = new MemoryBlock(0xFF00, 0xFF7F);
        _hram    = new MemoryBlock(0xFF80, 0xFFFE);

        Write(0xFF00, 0xCF);  // JOYP
        Write(0xFF02, 0x7E);  // SC
        Write(0xFF04, 0xAB);  // DIV
        Write(0xFF08, 0xF8);  // TAC 
        Write(0xFF0F, 0xE1);  // IF 

        // Sound 1
        Write(0xFF10, 0x80);  // ENT1
        Write(0xFF11, 0xBF);  // LEN1
        Write(0xFF12, 0xF3);  // ENV1
        Write(0xFF13, 0xC1);  // FRQ1
        Write(0xFF14, 0xBF);  // KIK1

        Write(0xFF15, 0xFF);  // N/A
        Write(0xFF16, 0x3F);  // LEN2
        Write(0xFF19, 0xB8);  // KIK2
        Write(0xFF1A, 0x7F);
        Write(0xFF1B, 0xFF);
        Write(0xFF1C, 0x9F);
        Write(0xFF1E, 0xBF);
        Write(0xFF20, 0xFF);
        Write(0xFF23, 0xBF);
        Write(0xFF24, 0x77);
        Write(0xFF25, 0xF3);
        Write(0xFF26, 0xF1);

        // graphics
        Write(0xFF40, 0x91);  // LCDC
        Write(0xFF41, 0x85);  // STAT
        Write(0xFF46, 0xFF);  // DMA
        Write(0xFF47, 0xFC);  // BGP
        Write(0xFF48, 0xFF);  // OBJ0
        Write(0xFF49, 0xFF);  // OBJ1

        Write(0xFF70, 0xFF);  // SVBK
        Write(0xFF4F, 0xFF);  // VBK
        Write(0xFF4D, 0xFF);  // KEY1
    }

    public bool LoadBootRom(string filename)
    {
        if (!File.Exists(filename)) return false;
        byte[] data = File.ReadAllBytes(filename);
        _bootRom.Copy(data);
        _bootRomBankedIn = true;
        return true;
    }

    public bool LoadCartridge(string filename)
    {
        if (!File.Exists(filename)) return false;
        byte[] data = File.ReadAllBytes(filename);
        _cartridge = new Cartridge(data);
        return true;
    }

    public byte this[int address]
    {
        get => Read(address);
        set => Write(address, value);
    }

    private byte Read(int address)
    {
        if (_bootRomBankedIn && address <= 0x00FF) return _bootRom[address];
        // Cartridge memory unless the boot rom is still loaded
        else if (address < 0x8000) return _cartridge[address];
        // ECHO ram
        else if ((address >= 0xE000) && (address <= 0xFDFF)) return _wram[address - 0x2000];
        // no control needed over this area so write to memory
        else if (_vram.HandlesAddress(address)) return _vram[address];
        else if (_wram.HandlesAddress(address)) return _wram[address];
        else if (_oam.HandlesAddress(address)) return _oam[address];
        else if (_hram.HandlesAddress(address)) return _hram[address];

        // IO Registers
        return address switch
        {
            // _clock
            0xFF04 => _clock.DIV,
            0xFF05 => _clock.TIMA,
            0xFF06 => _clock.TMA,
            0xFF07 => _clock.TAC,
            // interruptions
            0xFFFF => _int.IE,
            0xFF0F => _int.IF,
            // graphics
            0xFF40 => _vpu.LCDC,
            0xFF41 => _vpu.STAT,
            0xFF42 => _vpu.SCY,
            0xFF43 => _vpu.SCX,
            0xFF44 => _vpu.LY,
            0xFF45 => _vpu.LYC,
            0xFF46 => _dma.Register,
            0xFF47 => _vpu.BGP,
            0xFF48 => _vpu.OBP0,
            0xFF49 => _vpu.OBP1,
            0xFF4A => _vpu.WY,
            0xFF4B => _vpu.WX,
            // joypad
            0xFF00 => _joypad.JOYP,
            // serial
            0xFF01 => _serial.SB,
            0xFF02 => _serial.SC,
            _ => _io.HandlesAddress(address) ? _io[address] : (byte)0xFF,
        };
    }

    private void Write(int address, byte data)
    {
        if (_bootRomBankedIn && address <= 0xFF) _bootRom[address] = data;
        // Cartridge memory unless the boot rom is still loaded
        else if (address < 0x8000) _cartridge[address] = data;
        // ECHO ram
        else if ((address >= 0xE000) && (address <= 0xFDFF)) _wram[address - 0x2000] = data;
        // this area is restricted
        else if ((address >= 0xFEA0) && (address <= 0xFEFF)) { }
        // no control needed over this area so write to memory
        else if (_vram.HandlesAddress(address)) _vram[address] = data;
        else if (_wram.HandlesAddress(address)) _wram[address] = data;
        else if (_oam.HandlesAddress(address)) _oam[address] = data;
        else if (_hram.HandlesAddress(address)) _hram[address] = data;

        // IO
        switch (address)
        {
            case 0xFF04: _clock.DIV = data; break;
            case 0xFF05: _clock.TIMA = data; break;
            case 0xFF06: _clock.TMA = data; break;
            case 0xFF07: _clock.TAC = data; break;
            // interrupts
            case 0xFFFF: _int.IE = data; break;
            case 0xFF0F: _int.IF = data; break;
            // graphics
            case 0xFF40: _vpu.LCDC = data; break;
            case 0xFF41: _vpu.STAT = data; break;
            case 0xFF42: _vpu.SCY = data; break;
            case 0xFF43: _vpu.SCX = data; break;
            case 0xFF44: _vpu.LY = data; break;
            case 0xFF45: _vpu.LYC = data; break;
            case 0xFF46: _dma.Register = data; break;
            case 0xFF47: _vpu.BGP = data; break;
            case 0xFF48: _vpu.OBP0 = data; break;
            case 0xFF49: _vpu.OBP1 = data; break;
            case 0xFF4A: _vpu.WY = data; break;
            case 0xFF4B: _vpu.WX = data; break;
            // joypad
            case 0xFF00: _joypad.JOYP = data; break;
            // serial
            case 0xFF01: _serial.SB = data; break;
            case 0xFF02: _serial.SC = data; break;
            default: if (_io.HandlesAddress(address)) _io[address] = data; break;
        }
    }
}
