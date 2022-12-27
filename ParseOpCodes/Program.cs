using System.Text;
using System.Text.Json;

string fileName = @"C:\Src\Retro\GameBoy\dmgops.json";
string jsonString = File.ReadAllText(fileName);
var opcodes = JsonSerializer.Deserialize<Opcodes>(jsonString)!;

byte value = 0x00;
foreach (var oc in opcodes.Unprefixed)
{
    oc.Value = value++;
    if (value == 0xCB) value++;
    if (oc.TCyclesNoBranch != oc.TCyclesBranch)
        Console.WriteLine($"Opcode {oc.Value} has different cycles {oc.TCyclesNoBranch} != {oc.TCyclesBranch}");
}
value = 0x00;
foreach (var oc in opcodes.CBPrefixed)
{
    oc.Value = value++;
    if (oc.TCyclesNoBranch != oc.TCyclesBranch)
        Console.WriteLine($"CB Opcode {oc.Value} has different cycles {oc.TCyclesNoBranch} != {oc.TCyclesBranch}");
}

const string dir = @"..\..\..\..\SharpBoy.Core\Processor\Opcodes";

var sb = new StringBuilder();
sb.AppendLine("namespace SharpBoy.Core.Processor.Opcodes;\r\n");
sb.AppendLine("public partial class OpcodeHandler\r\n{");
sb.AppendLine("    private Dictionary<uint8, Opcode> Initialize() => new Dictionary<uint8, Opcode>\r\n    {");

var groups = opcodes.Unprefixed.Select(o => o.Group).Distinct().OrderBy(g => g);
foreach (var group in groups)
{
    sb.AppendLine($"        // {group}");
    foreach (var oc in opcodes.Unprefixed.Where(o => o.Group == group))
    {
        sb.AppendLine($"        {{ 0x{oc.Value:X2}, new Opcode(0x{oc.Value:X2}, \"{oc.Name}\", {oc.Length}, {oc.TCyclesNoBranch}, new Tick[] {{");
        for (int i = 1; i < oc.TCyclesNoBranch; i++)
            sb.AppendLine( "            () => { },");
        sb.AppendLine( "        } ) },");
    }
    sb.AppendLine();
}
sb.AppendLine("    };\r\n}\r\n");

File.WriteAllText(Path.Combine(dir, "OpcodeHandler.Initialize.Gen.cs"), sb.ToString());

sb.Clear();
sb.AppendLine("namespace SharpBoy.Core.Processor.Opcodes;\r\n");
sb.AppendLine("public partial class CbOpcodeHandler\r\n{");
sb.AppendLine("    private Dictionary<uint8, Opcode> Initialize() => new Dictionary<uint8, Opcode>\r\n    {");

groups = opcodes.CBPrefixed.Select(o => o.Group).Distinct().OrderBy(g => g);
foreach (var group in groups)
{
    sb.AppendLine($"        // {group}");
    foreach (var oc in opcodes.CBPrefixed.Where(o => o.Group == group))
    {
        sb.AppendLine($"        {{ 0x{oc.Value:X2}, new Opcode(0x{oc.Value:X2}, \"{oc.Name}\", {oc.Length}, {oc.TCyclesNoBranch}, new Tick[] {{");
        for (int i = 1; i < oc.TCyclesNoBranch; i++)
            sb.AppendLine("            () => { },");
        sb.AppendLine("        } ) },");
    }
    sb.AppendLine();
}
sb.AppendLine("    };\r\n}\r\n");

File.WriteAllText(Path.Combine(dir, "CbOpcodeHandler.Initialize.Gen.cs"), sb.ToString());