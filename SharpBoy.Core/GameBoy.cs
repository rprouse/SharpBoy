using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpBoy.Core.Graphics;
using SharpBoy.Core.Input;
using SharpBoy.Core.Memory;
using SharpBoy.Core.Serial;
using System.Xml.Linq;
using SharpBoy.Core.Processor;

namespace SharpBoy.Core;

public class GameBoy
{
    const string BootRom = @"C:\Src\Retro\GameBoy\SharpBoy\roms\dmg_boot.bin";
    const string Cartridge = @"C:\Src\Retro\GameBoy\SharpBoy\roms\Tetris.gb";

    public const int ClockSpeed = 4_194_304;
    public CPU _cpu;
    public VPU _vpu;
    public MMU _mmu;
    public Dma _dma;
    public Interupts _int;
    public Clock _clock;
    public SerialLink _serial;
    public Joypad _joypad;

    // constructor
    public GameBoy()
    {
        _int = new Interupts();
        _clock = new Clock();
        _vpu = new VPU();
        _joypad = new Joypad();
        _dma = new Dma();
        _serial = new SerialLink();
        _mmu = new MMU(_clock, _int, _dma, _vpu, _joypad, _serial);

        // Load before the CPU so the CPU can set the PC
        // Based on whether or not the boot rom is loaded
        _mmu.LoadBootRom(BootRom);
        _mmu.LoadCartridge(Cartridge);

        _cpu = new CPU(_clock, _int, _vpu, _mmu);

        //dma.SetMMU(mmu);
    }

    public int Tick()
    {
        _clock.Tick();
        _cpu.Tick();
        _dma.Tick();
        _serial.Tick();
        _vpu.Tick();
        return 1;
    }
}
