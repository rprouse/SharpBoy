public class Opcodes
{
    public Opcode[] Unprefixed { get; set; }
    public Opcode[] CBPrefixed { get; set; }
}

public class Opcode
{
    public byte Value { get; set; }
    public string Name { get; set; }
    public string Group { get; set; }
    public int TCyclesBranch { get; set; }
    public int TCyclesNoBranch { get; set; }
    public int Length { get; set; }
    public Flags Flags { get; set; }
    public Timing[] TimingNoBranch { get; set; }
    public Timing[] TimingBranch { get; set; }
}

public class Flags
{
    public string Z { get; set; }
    public string N { get; set; }
    public string H { get; set; }
    public string C { get; set; }
}

public class Timing
{
    public string Type { get; set; }
    public string Comment { get; set; }
}
