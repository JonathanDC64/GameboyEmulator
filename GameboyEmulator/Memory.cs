using System;

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
		// Random Access Memory
		// Main RAM: 8K Byte
		byte[] RAM = new byte[0x2000];

		// Video RAM
		// Video RAM: 8K Byte
		byte[] VRAM = new byte[0x2000];

		// Scrolling Nintendo Graphic Encoding
		byte[] NGRAPH = new byte[]{
				0xCE, 0xED, 0x66, 0x66, 0xCC, 0x0D, 0x00, 0x0B, 0x03, 0x73, 0x00, 0x83, 0x00, 0x0C, 0x00, 0x0D,
				0x00, 0x08, 0x11, 0x1F, 0x88, 0x89, 0x00, 0x0E, 0xDC, 0xCC, 0x6E, 0xE6, 0xDD, 0xDD, 0xD9, 0x99,
				0xBB, 0xBB, 0x67, 0x63, 0x6E, 0x0E, 0xEC, 0xCC, 0xDD, 0xDC, 0x99, 0x9F, 0xBB, 0xB9, 0x33, 0x3E
			};


		public byte ReadByte(ushort addr)
		{
			if (addr >= 0xC000 && addr < 0xE000)
			{
				return RAM[addr - 0xC000];
			}
			else if (addr >= 0xE000 && addr < 0xFE00)
			{
				return RAM[addr - 0xE000];
			}
			else if (addr >= 0x8000 && addr <= 0xA000)
			{
				return VRAM[addr - 0x8000];
			}
			else
			{
				throw new Exception($"Memory address 0x{Convert.ToString(addr, 16)} not implemented");
			}
		}

		public void WriteByte(ushort addr, byte data)
		{
			if (addr >= 0xC000 && addr < 0xE000)
			{
				RAM[addr - 0xC000] = data;
			}
			else if (addr >= 0xE000 && addr < 0xFE00)
			{
				RAM[addr - 0xE000] = data;
			}
			else if (addr >= 0x8000 && addr <= 0xA000)
			{
				VRAM[addr - 0x8000] = data;
			}
			else
			{
				throw new Exception($"Memory address 0x{Convert.ToString(addr, 16)} not implemented");
			}
		}
	}
}
