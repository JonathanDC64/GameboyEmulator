using System;


// View page 7 of http://marc.rawer.de/Gameboy/Docs/GBCPUman.pdf
namespace GameboyEmulator
{
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
		 * --------------------------- 4000 |= 32kB Cartridge 0000-7FFF
		 * 16kB ROM bank #0 |
		 * --------------------------- 0000 --
		*/

	class Memory
	{
		// The interrup enabled register located at 0xFFFF
		private byte InterruptEnable = 0;

		// Internal RAM [FF80, FFFF)
		private byte[] IRAM = new byte[0x80];

		// Input / Output ports
		private byte[] IO = new byte[0x100];

		// Sprite Attrib Memory [FE00, FEA0)
		private byte[] OAM = new byte[0x100];

		// Random Access Memory
		// Main RAM: 8K Byte
		private byte[] RAM = new byte[0x2000];

		// 8 KB switchable RAM bank
		private byte[] SRAM = new byte[0x2000];

		// Video RAM
		// Video RAM: 8K Byte
		private byte[] VRAM = new byte[0x2000];

		// 32 KB game cart (16 KB ROM bank 0 [0000, 4000) ) (16 KB switchable rombank [4000, 8000) )
		private byte[] Cart = new byte[0x8000];
		

		// Scrolling Nintendo Graphic Encoding
		byte[] NGRAPH = new byte[]{
				0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
				0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
				0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E
			};


		public byte ReadByte(ushort addr)
		{
			// Cart
			if (addr < 0x8000)
				return Cart[addr];

			// VRAM
			else if (addr >= 0x8000 && addr < 0xA000)
				return VRAM[addr - 0x8000];

			// 8 KB switchable rambank
			else if (addr >= 0xA000 && addr < 0xC000)
				return SRAM[addr - 0xA000];

			// 8KB Internal RAM
			else if (addr >= 0xC000 && addr < 0xE000)
				return RAM[addr - 0xC000];


			// Echo of 8KB Interal RAM
			else if (addr >= 0xE000 && addr < 0xFE00)
				return RAM[addr - 0xE000];


			// Sprite Attrib Memory OAM
			else if (addr >= 0xFE00 && addr < 0xFF00)
				return OAM[addr - 0xFE00];


			// I/O Ports
			else if (addr >= 0xFF00 && addr < 0xFF80)
				return IO[addr - 0xFF00];


			// Internal RAM
			else if (addr >= 0xFF80)
				return IRAM[addr - 0xFF80];


			// Interrupt Enable Register
			else if (addr == 0xFFFF)
				return InterruptEnable;


			// If exception triggers, there is a problem with registers, opcodes or memory address has not been implemented
			else
				throw new Exception($"Memory address 0x{Convert.ToString(addr, 16)} not implemented");
			
		}

		public void WriteByte(ushort addr, byte data)
		{
			// Cart
			if (addr < 0x8000)
				Cart[addr] = data;

			// VRAM
			else if (addr >= 0x8000 && addr < 0xA000)
				VRAM[addr - 0x8000] = data;

			// 8 KB switchable rambank
			else if (addr >= 0xA000 && addr < 0xC000)
				SRAM[addr - 0xA000] = data;

			// 8KB Internal RAM
			else if (addr >= 0xC000 && addr < 0xE000)
				RAM[addr - 0xC000] = data;


			// Echo of 8KB Interal RAM
			else if (addr >= 0xE000 && addr < 0xFE00)
				RAM[addr - 0xE000] = data;


			// Sprite Attrib Memory OAM
			else if (addr >= 0xFE00 && addr < 0xFF00)
				OAM[addr - 0xFE00] = data;


			// I/O Ports
			else if (addr >= 0xFF00 && addr < 0xFF80)
				IO[addr - 0xFF00] = data;


			// Internal RAM
			else if (addr >= 0xFF80)
				IRAM[addr - 0xFF80] = data;


			// Interrupt Enable Register
			else if (addr == 0xFFFF)
				InterruptEnable = data;


			// If exception triggers, there is a problem with registers, opcodes or memory address has not been implemented
			else
				throw new Exception($"Memory address 0x{Convert.ToString(addr, 16)} not implemented");
		}
	}
}
