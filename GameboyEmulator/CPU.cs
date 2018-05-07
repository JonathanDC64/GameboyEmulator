

using Newtonsoft.Json;
using System;
using System.IO;

namespace GameboyEmulator
{
	//http://marc.rawer.de/Gameboy/Docs/GBCPUman.pdf
	class CPU
	{
		// Eight 8-bit registers (or 4 16-bit registers representing 2 registers each)
		private ushort AF;
		private ushort BC;
		private ushort DE;
		private ushort HL;

		// 'A' register
		private byte A
		{
			get
			{
				return GetUpperByte(AF);
			}
			set
			{
				SetUpperByte(ref AF, value);
			}
		}

		// 'F' register (Flag)
		private byte F
		{
			get
			{
				return GetLowerByte(AF);
			}
			set
			{
				SetLowerByte(ref AF, value);
			}
		}

		// 'B' register
		private byte B
		{
			get
			{
				return GetUpperByte(BC);
			}
			set
			{
				SetUpperByte(ref BC, value);
			}
		}

		// 'C' register
		private byte C
		{
			get
			{
				return GetLowerByte(BC);
			}
			set
			{
				SetLowerByte(ref BC, value);
			}
		}

		// 'D' register
		private byte D
		{
			get
			{
				return GetUpperByte(DE);
			}
			set
			{
				SetUpperByte(ref DE, value);
			}
		}

		// 'E' register
		private byte E
		{
			get
			{
				return GetLowerByte(DE);
			}
			set
			{
				SetLowerByte(ref DE, value);
			}
		}

		// 'H' register
		private byte H
		{
			get
			{
				return GetUpperByte(HL);
			}
			set
			{
				SetUpperByte(ref HL, value);
			}
		}

		// 'L' register
		private byte L
		{
			get
			{
				return GetLowerByte(HL);
			}
			set
			{
				SetLowerByte(ref HL, value);
			}
		}

		//Useful bit #'s of the Flag register 'F'
		private enum Flags
		{
			C = 4,
			H = 5,
			N = 6,
			Z = 7
		}

		// Two 16-bit registers
		ushort SP;
		ushort PC;

		private Memory memory;

		private Instructions instructions;


		// Initialize RAM and Registers
		public CPU()
		{
			// Initalize memory unit
			memory = new Memory();

			// Nintendo graphic storred in fixed location 0x0104-0x0133
			//for (int i = 0; i < NGRAPH.Length; i++)
			//{
			//	RAM[0x104 + i] = NGRAPH[i];
			//}

			// On power up, the GameBoy Program Counter is
			// initialized to $100(100 hex) and the instruction found
			// at this location in ROM is executed.
			PC = 0x100;

			// Load a list of instructions from file
			string instruction_string = File.ReadAllText("opcodes.json");
			instructions = new Instructions(JsonConvert.DeserializeObject<InstructionsData>(instruction_string));

			// Power Up Sequence

			AF = 0x01;	// $01- GB / SGB, $FF - GBP, $11 - GBC
			F = 0xB0;
			BC = 0x0013;
			DE = 0x00D8;
			HL = 0x014D;
			SP = 0xFFFE;

			memory.WriteByte(0xFF05, 0x00);
			memory.WriteByte(0xFF06, 0x00);
			memory.WriteByte(0xFF07, 0x00);
			memory.WriteByte(0xFF10, 0x80);
			memory.WriteByte(0xFF11, 0xBF);
			memory.WriteByte(0xFF12, 0xF3);
			memory.WriteByte(0xFF14, 0xBF);
			memory.WriteByte(0xFF16, 0x3F);
			memory.WriteByte(0xFF17, 0x00);
			memory.WriteByte(0xFF19, 0xBF);
			memory.WriteByte(0xFF1A, 0x7F);
			memory.WriteByte(0xFF1B, 0xFF);
			memory.WriteByte(0xFF1C, 0x9F);
			memory.WriteByte(0xFF1E, 0xBF);
			memory.WriteByte(0xFF20, 0xFF);
			memory.WriteByte(0xFF21, 0x00);
			memory.WriteByte(0xFF22, 0x00);
			memory.WriteByte(0xFF23, 0xBF);
			memory.WriteByte(0xFF24, 0x77);
			memory.WriteByte(0xFF25, 0xF3);
			memory.WriteByte(0xFF26, 0xF1); //  $F1-GB, $F0-SGB ;
			memory.WriteByte(0xFF40, 0x91);
			memory.WriteByte(0xFF42, 0x00);
			memory.WriteByte(0xFF43, 0x00);
			memory.WriteByte(0xFF45, 0x00);
			memory.WriteByte(0xFF47, 0xFC);
			memory.WriteByte(0xFF48, 0xFF);
			memory.WriteByte(0xFF49, 0xFF);
			memory.WriteByte(0xFF4A, 0x00);
			memory.WriteByte(0xFF4B, 0x00);
			memory.WriteByte(0xFFFF, 0x00);

			instructions.unprefixed[0x0].MapFunction(NOP);
		}

		private void NOP(ushort operand)
		{
			
		}

		

		// Get the upper byte of a 16 bit number
		private byte GetUpperByte(ushort value)
		{
			return (byte)(value & 0xFF00);
		}

		// Set the upper byte of a 16 bit number
		private void SetUpperByte(ref ushort reference, byte value)
		{
			reference = (ushort)((reference & 0x00FF) + (value << 8));
		}

		// Get the lower byte of a 16 bit number
		private byte GetLowerByte(ushort value)
		{
			return (byte)(value & 0x00FF);
		}

		// Set the lower byte of a 16 bit number
		private void SetLowerByte(ref ushort reference, byte value)
		{
			reference = (ushort)((reference & 0xFF00) + (value));
		}

		//
		public static int HexStringToInt(string hex)
		{
			return Convert.ToInt32(hex, 16);
		}
	}
}
