﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameboyEmulator
{
	class Program
	{
		static void Main(string[] args)
		{
			//EmulatorDisplay e = new EmulatorDisplay();

			//e.Run();

			//Console.WriteLine((0xAABB & 0x00FF) + (0xFF << 2));
			/*ushort b = 0xFFFF;
			Console.WriteLine(b);
			T(ref b, 0xAA);
			Console.WriteLine(b);*/

			//Console.WriteLine(Directory.GetCurrentDirectory());
			//string instruction_string = File.ReadAllText("opcodes.json");
			//Console.WriteLine(instruction_string);
			//InstructionsData instructions = JsonConvert.DeserializeObject<InstructionsData>(instruction_string);

			//Console.WriteLine(instructions);

			//foreach (var item in instructions["unprefixed"])
			//{

			//	//foreach(var instruction in item)
			//	//{
			//	//	Console.WriteLine(instruction);
			//	//}
			//}

			CPU c = new CPU("C:\\Users\\Eugenio\\Downloads\\Tetris (World)\\Tetris (World).gb");
			c.Run();
			c.Run();
		}

		private static void T(ref ushort a, byte value)
		{
			a = (ushort)((a & 0x00FF) + (value << 8));
		}
	}
}
