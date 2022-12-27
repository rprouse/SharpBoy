using System.Text.Json;

string fileName = @"C:\Src\Retro\GameBoy\dmgops.json";
string jsonString = File.ReadAllText(fileName);
var opcodes = JsonSerializer.Deserialize<Opcodes>(jsonString)!;

foreach( var opcode in opcodes.Unprefixed)
{
    Console.WriteLine($"{opcode.Group}: {opcode.Name}");
}

public class Opcodes
{
    public Opcode[] Unprefixed { get; set; }
    public Opcode[] CBPrefixed { get; set; }
}

public class Opcode
{
    public string Name { get; set; }
    public string Group { get; set; }
    public int TCyclesBranch { get; set; }
    public int TCyclesNoBranch { get; set; }
    public int Length { get; set; }
    public Flags Flags { get; set; }
    public Timingnobranch[] TimingNoBranch { get; set; }
    public Timingbranch[] TimingBranch { get; set; }
}

public class Flags
{
    public string Z { get; set; }
    public string N { get; set; }
    public string H { get; set; }
    public string C { get; set; }
}

public class Timingnobranch
{
    public string Type { get; set; }
    public string Comment { get; set; }
}

public class Timingbranch
{
    public string Type { get; set; }
    public string Comment { get; set; }
}