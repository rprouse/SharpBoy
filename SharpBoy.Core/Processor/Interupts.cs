namespace SharpBoy.Core.Processor;

public class Interupts
{
    /// <summary>
    /// Disable Interrupts by clearing the IME flag.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Disable()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Enable Interrupts by setting the IME flag. 
    /// The flag is only set after the instruction 
    /// following EI.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Enable()
    {
        throw new NotImplementedException();
    }
}
