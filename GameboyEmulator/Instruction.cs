using System;

namespace GameboyEmulator
{
	class Instruction
	{
		public string mnemonic;
		public int length;
		public int[] cycles;
		public string[] flags;
		public string addr;
		public string operand1;
		public string operand2;

		public delegate void Exec(ushort operand);
		private Exec function;

		public void MapFunction(Exec function)
		{
			this.function = function;
		}


		public void Execute(ushort operand)
		{
			if (function == null)
				throw new Exception($"Instruction {addr} {mnemonic} has not been implemented");
			function(operand);
		}

		public override string ToString()
		{
			return $"{{ mnemonic : {mnemonic}, length : {length}, cycles : [ {String.Join(", ", cycles)} ], flags : [{String.Join(", ", flags)}], addr : {addr}, operand1 : {operand1}, operand2 : {operand2}}}";
		}


	}
}
