using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;
namespace SharpBoy.Core.Processor;

public class CPU
{
    private readonly Clock _clock;
    private readonly Interupts _int;
    private readonly VPU _vpu;
    private readonly MMU _mmu;

    public CPU(Clock clock, Interupts interupts, VPU vpu, MMU mmu)
    {
        _clock = clock;
        _int = interupts;
        _vpu = vpu;
        _mmu = mmu;
    }

    public void Tick()
    {
    }
}
