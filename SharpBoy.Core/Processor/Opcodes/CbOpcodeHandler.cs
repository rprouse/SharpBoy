using SharpBoy.Core.Graphics;
using SharpBoy.Core.Memory;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class CbOpcodeHandler
{
    private readonly Registers _reg;
    private readonly MMU _mmu;
    private readonly VPU _vpu;
    private readonly Interupts _int;

    private Dictionary<byte, Opcode> _opcodes;

    public CbOpcodeHandler(Registers registers, MMU mmu, VPU vpu, Interupts interupts)
    {
        _reg = registers;
        _mmu = mmu;
        _vpu = vpu;
        _int = interupts;
        _opcodes = Initialize();
    }
}
