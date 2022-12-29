namespace SharpBoy.Core.Memory;

/// <summary>
/// ROM and RAM in a Cartridge
/// </summary>
/// <remarks>
/// 0x0000 - 0x3FFF 16KB ROM Bank 00 (in cartridge, fixed at bank 00)
/// 0x4000 - 0x7FFF 16KB ROM Bank 01..NN(in cartridge, switchable bank number)
/// 0xA000 - 0xBFFF 8KB External RAM(in cartridge, switchable bank, if any)
/// </remarks>
public class Cartridge
{
    private MemoryBlock[] _romBanks;
    private MemoryBlock _ramBank;
    private int _romBank = 0;

    public enum BankType
    {
        None,
        MBC1,
        MBC2,
        MBC3
    }

    public BankType MBC { get; }

    public Cartridge()
    {
        // Start simple with one ROM bank
        MBC = BankType.MBC1;
        _romBanks = new[] { new MemoryBlock(0x0000, 0x3FFF) };
        _ramBank = new MemoryBlock(0xA000, 0xBFFF);
    }

    public byte this[int address]
    {
        get => Read(address);
        set => Write(address, value);
    }

    private byte Read(int address) =>
        address switch
        {
            <= 0x3FFF => _romBanks[0][address],
            <= 0x7FFF => _romBanks[_romBank][address],
            >= 0xA000 and <= 0xBFFF => _ramBank[address],
            _ => throw new NotImplementedException(),
        };

    private void Write(int address, byte data)
    {
        if (address >= 0xA000 && address <= 0xBFFF)
        {
            _ramBank[address] = data;
        }
        else
        {
            // TODO: Handle bank switching?
        }
    }

}
