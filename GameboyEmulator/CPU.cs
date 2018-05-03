

using Newtonsoft.Json;
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

		// Two 16-bit registers
		ushort SP;
		ushort PC;

		// Random Access Memory
		byte[] RAM;

		// Video RAM
		byte[] VRAM;

		// Scrolling Nintendo Graphic Encoding
		byte[] NGRAPH;

		/**
		 * MEMORY MAP
		 * 
		 * Interrupt Enable Register
		 * --------------------------- FFFF
		 * Internal RAM
		 * --------------------------- FF80
		 * Empty but unusable for I/O
		 * --------------------------- FF4C
		 * I/O ports
		 * --------------------------- FF00
		 * Empty but unusable for I/O
		 * --------------------------- FEA0
		 * Sprite Attrib Memory (OAM)
		 * --------------------------- FE00
		 * Echo of 8kB Internal RAM
		 * --------------------------- E000
		 * 8kB Internal RAM
		 * --------------------------- C000
		 * 8kB switchable RAM bank
		 * --------------------------- A000
		 * 8kB Video RAM
		 * --------------------------- 8000 --
		 * 16kB switchable ROM bank |
		 * --------------------------- 4000 |= 32kB Cartrigbe 0000-7FFF
		 * 16kB ROM bank #0 |
		 * --------------------------- 0000 --		*/

		// Initialize RAM and Registers
		public CPU()
		{
			// Main RAM: 8K Byte
			RAM = new byte[8192];

			// Video RAM: 8K Byte
			VRAM = new byte[8192];

			// Scrolling Nintendo graphic
			NGRAPH = new byte[]{
				0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
				0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
				0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E			};

			// Nintendo graphic storred in fixed location 0x0104-0x0133
			for (int i = 0; i < NGRAPH.Length; i++)
			{
				RAM[0x104 + i] = NGRAPH[i];
			}

			// On power up, the GameBoy Program Counter is
			// initialized to $100(100 hex) and the instruction found
			// at this location in ROM is executed.
			PC = 0x100;

			// The GameBoy stack pointer is initialized to $FFFE on power up
			SP = 0xFFFE;

			// Load a list of instructions from file
			string instruction_string = File.ReadAllText("opcodes.json");
			Instructions instructions = JsonConvert.DeserializeObject<Instructions>(instruction_string);
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
	}
}
