namespace SharpBoy.Core.Processor;

public enum InteruptType
{
    None       = 0x00,
    VBlank     = 0x01,
    LCD        = 0x02,
    Timer      = 0x04,
    SerialLink = 0x08,
    Joypad     = 0x10
};

public class Interupts
{
    // Interupt Master Enable Register, it's a master switch for all interruptions
    public bool IME;

    // Interupt Registers
    // IE = When bits are set, the corresponding interrupt can be triggered
    // IF = When bits are set, an interrupt has happened
    // They use the same bit positions
    // 
    // Bit 
    // 0   Vblank 
    // 1   LCD
    // 2   Timer 
    // 3   Serial Link 
    // 4   Joypad 

    // 0xFFFF
    public byte IE { get; set; }

    // 0xFF0F
    public byte IF { get; set; }

    // ISR addresses
    public word[] ResetVectors { get; } = new word[] {
                0x0040,    // Vblank
                0x0048,    // LCD Status
                0x0050,    // TimerOverflow
                0x0058,    // SerialLink
                0x0060     // JoypadPress
         };

    private bool _pendingEnable = false;

    public void OnInstructionFinished()
    {
        if(_pendingEnable) Enable(false);
    }

    /// <summary>
    /// Disable Interrupts by clearing the IME flag.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Disable()
    {
        _pendingEnable = false;
        IME = false;
    }

    /// <summary>
    /// Enable Interrupts by setting the IME flag. 
    /// The flag is only set after the instruction 
    /// following EI.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Enable(bool withDelay)
    {
        if (withDelay)
        {
            _pendingEnable = true;
        }
        else
        {
            _pendingEnable = false;
            IME = true;
        }
    }
}
