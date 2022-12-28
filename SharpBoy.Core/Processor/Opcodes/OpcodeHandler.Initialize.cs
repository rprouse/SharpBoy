using SharpBoy.Core.Utilities;

namespace SharpBoy.Core.Processor.Opcodes;

public partial class OpcodeHandler
{
    private Dictionary<byte, Opcode> Initialize() => new Dictionary<byte, Opcode>
    {
        // control/br
        { 0x18, new Opcode(0x18, "JR i8", 2, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x20, new Opcode(0x20, "JR NZ,i8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x28, new Opcode(0x28, "JR Z,i8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x30, new Opcode(0x30, "JR NC,i8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x38, new Opcode(0x38, "JR C,i8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC0, new Opcode(0xC0, "RET NZ", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC2, new Opcode(0xC2, "JP NZ,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC3, new Opcode(0xC3, "JP u16", 3, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC4, new Opcode(0xC4, "CALL NZ,u16", 3, 3, new Tick[] {
            () => { _lsb = _mmu[_reg.PC++]; },
            () => { _msb = _mmu[_reg.PC++]; _stop = _reg.FlagZ; },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Msb(); },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = BitUtils.ToWord(_msb, _lsb); },
        } ) },
        { 0xC7, new Opcode(0xC7, "RST 00h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC8, new Opcode(0xC8, "RET Z", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC9, new Opcode(0xC9, "RET", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xCA, new Opcode(0xCA, "JP Z,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xCC, new Opcode(0xCC, "CALL Z,u16", 3, 3, new Tick[] {
            () => { _lsb = _mmu[_reg.PC++]; },
            () => { _msb = _mmu[_reg.PC++]; _stop = !_reg.FlagZ; },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Msb(); },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = BitUtils.ToWord(_msb, _lsb); },
        } ) },
        { 0xCD, new Opcode(0xCD, "CALL u16", 3, 6, new Tick[] {
            () => { _lsb = _mmu[_reg.PC++]; },
            () => { _msb = _mmu[_reg.PC++]; },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Msb(); },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = BitUtils.ToWord(_msb, _lsb); },
        } ) },
        { 0xCF, new Opcode(0xCF, "RST 08h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD0, new Opcode(0xD0, "RET NC", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD2, new Opcode(0xD2, "JP NC,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD4, new Opcode(0xD4, "CALL NC,u16", 3, 6, new Tick[] {
            () => { _lsb = _mmu[_reg.PC++]; },
            () => { _msb = _mmu[_reg.PC++]; _stop = _reg.FlagC; },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Msb(); },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = BitUtils.ToWord(_msb, _lsb); },
        } ) },
        { 0xD7, new Opcode(0xD7, "RST 10h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD8, new Opcode(0xD8, "RET C", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD9, new Opcode(0xD9, "RETI", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xDA, new Opcode(0xDA, "JP C,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xDC, new Opcode(0xDC, "CALL C,u16", 3, 3, new Tick[] {
            () => { _lsb = _mmu[_reg.PC++]; },
            () => { _msb = _mmu[_reg.PC++]; _stop = !_reg.FlagC; },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Msb(); },
            () => { _reg.SP--; _mmu[_reg.SP] = _reg.PC.Lsb(); },
            () => { _reg.PC = BitUtils.ToWord(_msb, _lsb); },
        } ) },
        { 0xDF, new Opcode(0xDF, "RST 18h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE7, new Opcode(0xE7, "RST 20h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE9, new Opcode(0xE9, "JP HL", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xEF, new Opcode(0xEF, "RST 28h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF7, new Opcode(0xF7, "RST 30h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFF, new Opcode(0xFF, "RST 38h", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },

        // control/misc
        { 0x00, new Opcode(0x00, "NOP", 1, 1, new Tick[] {
            () => { },
        } ) },
        { 0x10, new Opcode(0x10, "STOP", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x76, new Opcode(0x76, "HALT", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xCB, new Opcode(0xCB, "PREFIX CB", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF3, new Opcode(0xF3, "DI", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFB, new Opcode(0xFB, "EI", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },

        // unused
        { 0xD3, new Opcode(0xD3, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xDB, new Opcode(0xDB, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xDD, new Opcode(0xDD, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE3, new Opcode(0xE3, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE4, new Opcode(0xE4, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xEB, new Opcode(0xEB, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xEC, new Opcode(0xEC, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xED, new Opcode(0xED, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF4, new Opcode(0xF4, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFC, new Opcode(0xFC, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFD, new Opcode(0xFD, "UNUSED", 1, 0, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },

        // x16/alu
        { 0x03, new Opcode(0x03, "INC BC", 1, 2, new Tick[] {
            // Todo: OAM Bug
            () => { _reg.BC++; },
        } ) },
        { 0x09, new Opcode(0x09, "ADD HL,BC", 1, 2, new Tick[] {
            () => { ADDHL(_reg.BC); },
        } ) },
        { 0x0B, new Opcode(0x0B, "DEC BC", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x13, new Opcode(0x13, "INC DE", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x19, new Opcode(0x19, "ADD HL,DE", 1, 2, new Tick[] {
            () => { ADDHL(_reg.DE); },
        } ) },
        { 0x1B, new Opcode(0x1B, "DEC DE", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x23, new Opcode(0x23, "INC HL", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x29, new Opcode(0x29, "ADD HL,HL", 1, 2, new Tick[] {
            () => { ADDHL(_reg.HL); },
        } ) },
        { 0x2B, new Opcode(0x2B, "DEC HL", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x33, new Opcode(0x33, "INC SP", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x39, new Opcode(0x39, "ADD HL,SP", 1, 2, new Tick[] {
            () => { ADDHL(_reg.SP); },
        } ) },
        { 0x3B, new Opcode(0x3B, "DEC SP", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE8, new Opcode(0xE8, "ADD SP,i8", 2, 4, new Tick[] {
            () => { ADDSP(_mmu[_reg.PC++]); },
            () => { },
            () => { },
        } ) },
        { 0xF8, new Opcode(0xF8, "LD HL,SP+i8", 2, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },

        // x16/lsm
        { 0x01, new Opcode(0x01, "LD BC,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x08, new Opcode(0x08, "LD (u16),SP", 3, 5, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x11, new Opcode(0x11, "LD DE,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x21, new Opcode(0x21, "LD HL,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x31, new Opcode(0x31, "LD SP,u16", 3, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC1, new Opcode(0xC1, "POP BC", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xC5, new Opcode(0xC5, "PUSH BC", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD1, new Opcode(0xD1, "POP DE", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xD5, new Opcode(0xD5, "PUSH DE", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE1, new Opcode(0xE1, "POP HL", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE5, new Opcode(0xE5, "PUSH HL", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF1, new Opcode(0xF1, "POP AF", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF5, new Opcode(0xF5, "PUSH AF", 1, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF9, new Opcode(0xF9, "LD SP,HL", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },

        // x8/alu
        { 0x04, new Opcode(0x04, "INC B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x05, new Opcode(0x05, "DEC B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x0C, new Opcode(0x0C, "INC C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x0D, new Opcode(0x0D, "DEC C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x14, new Opcode(0x14, "INC D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x15, new Opcode(0x15, "DEC D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x1C, new Opcode(0x1C, "INC E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x1D, new Opcode(0x1D, "DEC E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x24, new Opcode(0x24, "INC H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x25, new Opcode(0x25, "DEC H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x27, new Opcode(0x27, "DAA", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x2C, new Opcode(0x2C, "INC L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x2D, new Opcode(0x2D, "DEC L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x2F, new Opcode(0x2F, "CPL", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x34, new Opcode(0x34, "INC (HL)", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x35, new Opcode(0x35, "DEC (HL)", 1, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x37, new Opcode(0x37, "SCF", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x3C, new Opcode(0x3C, "INC A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x3D, new Opcode(0x3D, "DEC A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x3F, new Opcode(0x3F, "CCF", 1, 1, new Tick[] {
            () => { _reg.FlagC = !_reg.FlagC; _reg.FlagN = false; _reg.FlagH = false; },
        } ) },
        { 0x80, new Opcode(0x80, "ADD A,B", 1, 1, new Tick[] {
            () => { ADD(_reg.B); },
        } ) },
        { 0x81, new Opcode(0x81, "ADD A,C", 1, 1, new Tick[] {
            () => { ADD(_reg.C); },
        } ) },
        { 0x82, new Opcode(0x82, "ADD A,D", 1, 1, new Tick[] {
            () => { ADD(_reg.D); },
        } ) },
        { 0x83, new Opcode(0x83, "ADD A,E", 1, 1, new Tick[] {
            () => { ADD(_reg.E); },
        } ) },
        { 0x84, new Opcode(0x84, "ADD A,H", 1, 1, new Tick[] {
            () => { ADD(_reg.H); },
        } ) },
        { 0x85, new Opcode(0x85, "ADD A,L", 1, 1, new Tick[] {
            () => { ADD(_reg.L); },
        } ) },
        { 0x86, new Opcode(0x86, "ADD A,(HL)", 1, 2, new Tick[] {
            () => { ADD(_mmu[_reg.HL]); },
        } ) },
        { 0x87, new Opcode(0x87, "ADD A,A", 1, 1, new Tick[] {
            () => { ADD(_reg.A); },
        } ) },
        { 0x88, new Opcode(0x88, "ADC A,B", 1, 1, new Tick[] {
            () => { ADC(_reg.B); },
        } ) },
        { 0x89, new Opcode(0x89, "ADC A,C", 1, 1, new Tick[] {
            () => { ADC(_reg.C); },
        } ) },
        { 0x8A, new Opcode(0x8A, "ADC A,D", 1, 1, new Tick[] {
            () => { ADC(_reg.D); },
        } ) },
        { 0x8B, new Opcode(0x8B, "ADC A,E", 1, 1, new Tick[] {
            () => { ADC(_reg.E); },
        } ) },
        { 0x8C, new Opcode(0x8C, "ADC A,H", 1, 1, new Tick[] {
            () => { ADC(_reg.H); },
        } ) },
        { 0x8D, new Opcode(0x8D, "ADC A,L", 1, 1, new Tick[] {
            () => { ADC(_reg.L); },
        } ) },
        { 0x8E, new Opcode(0x8E, "ADC A,(HL)", 1, 2, new Tick[] {
            () => { ADC(_mmu[_reg.HL]); },
        } ) },
        { 0x8F, new Opcode(0x8F, "ADC A,A", 1, 1, new Tick[] {
            () => { ADC(_reg.A); },
        } ) },
        { 0x90, new Opcode(0x90, "SUB A,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x91, new Opcode(0x91, "SUB A,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x92, new Opcode(0x92, "SUB A,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x93, new Opcode(0x93, "SUB A,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x94, new Opcode(0x94, "SUB A,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x95, new Opcode(0x95, "SUB A,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x96, new Opcode(0x96, "SUB A,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x97, new Opcode(0x97, "SUB A,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x98, new Opcode(0x98, "SBC A,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x99, new Opcode(0x99, "SBC A,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9A, new Opcode(0x9A, "SBC A,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9B, new Opcode(0x9B, "SBC A,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9C, new Opcode(0x9C, "SBC A,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9D, new Opcode(0x9D, "SBC A,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9E, new Opcode(0x9E, "SBC A,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x9F, new Opcode(0x9F, "SBC A,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xA0, new Opcode(0xA0, "AND A,B", 1, 1, new Tick[] {
            () => { AND(_reg.B); },
        } ) },
        { 0xA1, new Opcode(0xA1, "AND A,C", 1, 1, new Tick[] {
            () => { AND(_reg.C); },
        } ) },
        { 0xA2, new Opcode(0xA2, "AND A,D", 1, 1, new Tick[] {
            () => { AND(_reg.D); },
        } ) },
        { 0xA3, new Opcode(0xA3, "AND A,E", 1, 1, new Tick[] {
            () => { AND(_reg.E); },
        } ) },
        { 0xA4, new Opcode(0xA4, "AND A,H", 1, 1, new Tick[] {
            () => { AND(_reg.H); },
        } ) },
        { 0xA5, new Opcode(0xA5, "AND A,L", 1, 1, new Tick[] {
            () => { AND(_reg.L); },
        } ) },
        { 0xA6, new Opcode(0xA6, "AND A,(HL)", 1, 2, new Tick[] {
            () => { AND(_mmu[_reg.HL]); },
        } ) },
        { 0xA7, new Opcode(0xA7, "AND A,A", 1, 1, new Tick[] {
            () => { AND(_reg.A); },
        } ) },
        { 0xA8, new Opcode(0xA8, "XOR A,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xA9, new Opcode(0xA9, "XOR A,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAA, new Opcode(0xAA, "XOR A,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAB, new Opcode(0xAB, "XOR A,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAC, new Opcode(0xAC, "XOR A,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAD, new Opcode(0xAD, "XOR A,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAE, new Opcode(0xAE, "XOR A,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xAF, new Opcode(0xAF, "XOR A,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB0, new Opcode(0xB0, "OR A,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB1, new Opcode(0xB1, "OR A,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB2, new Opcode(0xB2, "OR A,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB3, new Opcode(0xB3, "OR A,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB4, new Opcode(0xB4, "OR A,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB5, new Opcode(0xB5, "OR A,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB6, new Opcode(0xB6, "OR A,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB7, new Opcode(0xB7, "OR A,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xB8, new Opcode(0xB8, "CP A,B", 1, 1, new Tick[] {
            () => { CP(_reg.B); },
        } ) },
        { 0xB9, new Opcode(0xB9, "CP A,C", 1, 1, new Tick[] {
            () => { CP(_reg.C); },
        } ) },
        { 0xBA, new Opcode(0xBA, "CP A,D", 1, 1, new Tick[] {
            () => { CP(_reg.D); },
        } ) },
        { 0xBB, new Opcode(0xBB, "CP A,E", 1, 1, new Tick[] {
            () => { CP(_reg.E); },
        } ) },
        { 0xBC, new Opcode(0xBC, "CP A,H", 1, 1, new Tick[] {
            () => { CP(_reg.H); },
        } ) },
        { 0xBD, new Opcode(0xBD, "CP A,L", 1, 1, new Tick[] {
            () => { CP(_reg.L); },
        } ) },
        { 0xBE, new Opcode(0xBE, "CP A,(HL)", 1, 2, new Tick[] {
            () => { CP(_mmu[_reg.HL]); },
        } ) },
        { 0xBF, new Opcode(0xBF, "CP A,A", 1, 1, new Tick[] {
            () => { CP(_reg.A); },
        } ) },
        { 0xC6, new Opcode(0xC6, "ADD A,u8", 2, 2, new Tick[] {
            () => { ADD(_mmu[_reg.PC++]); },
        } ) },
        { 0xCE, new Opcode(0xCE, "ADC A,u8", 2, 2, new Tick[] {
            () => { ADC(_mmu[_reg.PC++]); },
        } ) },
        { 0xD6, new Opcode(0xD6, "SUB A,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xDE, new Opcode(0xDE, "SBC A,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE6, new Opcode(0xE6, "AND A,u8", 2, 2, new Tick[] {
            () => { AND(_mmu[_reg.PC]); },
        } ) },
        { 0xEE, new Opcode(0xEE, "XOR A,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF6, new Opcode(0xF6, "OR A,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFE, new Opcode(0xFE, "CP A,u8", 2, 2, new Tick[] {
            () => { CP(_mmu[_reg.PC++]); },
        } ) },

        // x8/lsm
        { 0x02, new Opcode(0x02, "LD (BC),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x06, new Opcode(0x06, "LD B,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x0A, new Opcode(0x0A, "LD A,(BC)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x0E, new Opcode(0x0E, "LD C,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x12, new Opcode(0x12, "LD (DE),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x16, new Opcode(0x16, "LD D,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x1A, new Opcode(0x1A, "LD A,(DE)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x1E, new Opcode(0x1E, "LD E,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x22, new Opcode(0x22, "LD (HL+),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x26, new Opcode(0x26, "LD H,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x2A, new Opcode(0x2A, "LD A,(HL+)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x2E, new Opcode(0x2E, "LD L,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x32, new Opcode(0x32, "LD (HL-),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x36, new Opcode(0x36, "LD (HL),u8", 2, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x3A, new Opcode(0x3A, "LD A,(HL-)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x3E, new Opcode(0x3E, "LD A,u8", 2, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x40, new Opcode(0x40, "LD B,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x41, new Opcode(0x41, "LD B,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x42, new Opcode(0x42, "LD B,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x43, new Opcode(0x43, "LD B,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x44, new Opcode(0x44, "LD B,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x45, new Opcode(0x45, "LD B,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x46, new Opcode(0x46, "LD B,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x47, new Opcode(0x47, "LD B,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x48, new Opcode(0x48, "LD C,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x49, new Opcode(0x49, "LD C,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4A, new Opcode(0x4A, "LD C,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4B, new Opcode(0x4B, "LD C,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4C, new Opcode(0x4C, "LD C,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4D, new Opcode(0x4D, "LD C,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4E, new Opcode(0x4E, "LD C,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x4F, new Opcode(0x4F, "LD C,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x50, new Opcode(0x50, "LD D,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x51, new Opcode(0x51, "LD D,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x52, new Opcode(0x52, "LD D,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x53, new Opcode(0x53, "LD D,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x54, new Opcode(0x54, "LD D,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x55, new Opcode(0x55, "LD D,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x56, new Opcode(0x56, "LD D,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x57, new Opcode(0x57, "LD D,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x58, new Opcode(0x58, "LD E,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x59, new Opcode(0x59, "LD E,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5A, new Opcode(0x5A, "LD E,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5B, new Opcode(0x5B, "LD E,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5C, new Opcode(0x5C, "LD E,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5D, new Opcode(0x5D, "LD E,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5E, new Opcode(0x5E, "LD E,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x5F, new Opcode(0x5F, "LD E,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x60, new Opcode(0x60, "LD H,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x61, new Opcode(0x61, "LD H,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x62, new Opcode(0x62, "LD H,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x63, new Opcode(0x63, "LD H,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x64, new Opcode(0x64, "LD H,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x65, new Opcode(0x65, "LD H,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x66, new Opcode(0x66, "LD H,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x67, new Opcode(0x67, "LD H,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x68, new Opcode(0x68, "LD L,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x69, new Opcode(0x69, "LD L,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6A, new Opcode(0x6A, "LD L,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6B, new Opcode(0x6B, "LD L,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6C, new Opcode(0x6C, "LD L,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6D, new Opcode(0x6D, "LD L,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6E, new Opcode(0x6E, "LD L,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x6F, new Opcode(0x6F, "LD L,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x70, new Opcode(0x70, "LD (HL),B", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x71, new Opcode(0x71, "LD (HL),C", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x72, new Opcode(0x72, "LD (HL),D", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x73, new Opcode(0x73, "LD (HL),E", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x74, new Opcode(0x74, "LD (HL),H", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x75, new Opcode(0x75, "LD (HL),L", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x77, new Opcode(0x77, "LD (HL),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x78, new Opcode(0x78, "LD A,B", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x79, new Opcode(0x79, "LD A,C", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7A, new Opcode(0x7A, "LD A,D", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7B, new Opcode(0x7B, "LD A,E", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7C, new Opcode(0x7C, "LD A,H", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7D, new Opcode(0x7D, "LD A,L", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7E, new Opcode(0x7E, "LD A,(HL)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x7F, new Opcode(0x7F, "LD A,A", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE0, new Opcode(0xE0, "LD (FF00+u8),A", 2, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xE2, new Opcode(0xE2, "LD (FF00+C),A", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xEA, new Opcode(0xEA, "LD (u16),A", 3, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF0, new Opcode(0xF0, "LD A,(FF00+u8)", 2, 3, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xF2, new Opcode(0xF2, "LD A,(FF00+C)", 1, 2, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0xFA, new Opcode(0xFA, "LD A,(u16)", 3, 4, new Tick[] {
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
            () => { throw new NotImplementedException(); },
        } ) },

        // x8/rsb
        { 0x07, new Opcode(0x07, "RLCA", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x0F, new Opcode(0x0F, "RRCA", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x17, new Opcode(0x17, "RLA", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },
        { 0x1F, new Opcode(0x1F, "RRA", 1, 1, new Tick[] {
            () => { throw new NotImplementedException(); },
        } ) },

    };
}

