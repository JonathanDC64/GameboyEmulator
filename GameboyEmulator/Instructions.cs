using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameboyEmulator
{
	class Instructions
	{
		public string source;
		public Instruction[] unprefixed;
		public Instruction[] cbprefixed;

		public Instructions(InstructionsData instructionsData)
		{
			source = instructionsData.source;
			unprefixed = new Instruction[0x100]; // 256 possible opcodes
			cbprefixed = new Instruction[0x100]; // 256 possible opcodes

			foreach (KeyValuePair<string, Instruction> instruction in instructionsData.unprefixed)
			{
				int index = CPU.HexStringToInt(instruction.Key);
				unprefixed[index] = instruction.Value;
			}

			foreach (KeyValuePair<string, Instruction> instruction in instructionsData.cbprefixed)
			{
				int index = CPU.HexStringToInt(instruction.Key);
				cbprefixed[index] = instruction.Value;
			}
		}
	}
}
