namespace SharpBoy.Core.Memory;

/// <summary>
/// A block of memory in the memory map at a specified address range
/// </summary>
public class MemoryBlock
{
    private byte[] _data;
    private int _start;
    private int _end;

    public byte this[int address]
    { 
        get => _data[address - _start];
        set => _data[address - _start] = value;
    }

    /// <summary>
    /// Constructs a block of memory for a given memory range
    /// </summary>
    /// <param name="start">The start address of this block of memory</param>
    /// <param name="end">The end address of this block of memory inclusive</param>
    public MemoryBlock(int start, int end)
    {
        _start = start;
        _end = end;
        _data = new byte[_end - _start + 1];
    }

    /// <summary>
    /// Is this memory block in the address range for the given address?
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public bool HandlesAddress(int address) =>
        address >= _start && address <= _end;
}
