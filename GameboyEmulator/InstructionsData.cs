using System.Collections.Generic;
using System.Linq;

namespace GameboyEmulator
{
	class InstructionsData
	{
		public string source;
		public Dictionary<string, Instruction> unprefixed;
		public Dictionary<string, Instruction> cbprefixed;

		public override string ToString()
		{
			return string.Join(";\n", unprefixed.Select(x => x.Key + "=" + x.Value).ToArray());
		}
	}
}
