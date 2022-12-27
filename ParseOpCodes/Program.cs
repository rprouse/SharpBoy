using System.Text;
using System.Text.Json;

string fileName = @"C:\Src\Retro\GameBoy\dmgops.json";
string jsonString = File.ReadAllText(fileName);
var opcodes = JsonSerializer.Deserialize<Opcodes>(jsonString)!;

SetValues(opcodes.Unprefixed);
SetValues(opcodes.CBPrefixed);

GenerateHandler(opcodes.Unprefixed, "OpcodeHandler");
GenerateHandler(opcodes.CBPrefixed, "CbOpcodeHandler");

static void SetValues(IEnumerable<Opcode> opcodes)
{
    byte value = 0x00;
    foreach (var oc in opcodes)
    {
        oc.Value = value++;
        if (oc.TCyclesNoBranch != oc.TCyclesBranch)
            Console.WriteLine($"Opcode {oc.Value} has different cycles {oc.TCyclesNoBranch} != {oc.TCyclesBranch}");
    }
}

static void GenerateHandler(IEnumerable<Opcode> opcodes, string classname)
{
    const string dir = @"..\..\..\..\SharpBoy.Core\Processor\Opcodes";

    var sb = new StringBuilder();
    sb.AppendLine("namespace SharpBoy.Core.Processor.Opcodes;\r\n");
    sb.AppendLine($"public partial class {classname}\r\n{{");
    sb.AppendLine("    private Dictionary<uint8, Opcode> Initialize() => new Dictionary<uint8, Opcode>\r\n    {");

    var groups = opcodes.Select(o => o.Group).Distinct().OrderBy(g => g);
    foreach (var group in groups)
    {
        sb.AppendLine($"        // {group}");
        foreach (var oc in opcodes.Where(o => o.Group == group))
        {
            sb.AppendLine($"        {{ 0x{oc.Value:X2}, new Opcode(0x{oc.Value:X2}, \"{oc.Name}\", {oc.Length}, {oc.TCyclesNoBranch / 4}, new Tick[] {{");
            for (int i = 1; i == 1 || i < oc.TCyclesNoBranch / 4; i++)
                sb.AppendLine("            () => { throw new NotImplementedException(); },");
            sb.AppendLine("        } ) },");
        }
        sb.AppendLine();
    }
    sb.AppendLine("    };\r\n}\r\n");

    File.WriteAllText(Path.Combine(dir, $"{classname}.Initialize.Gen.cs"), sb.ToString());
}