using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day8
{
	static public void Puzzle1()
	{
		string filename = "input/d8p1_b.txt";

		var lines = File.ReadAllLines(filename);
		var registers = new Dictionary<string, int>();
		int highestEverVal = int.MinValue;
		foreach (var line in lines)
		{
			var token = line.Split(' ');
			var reg = token[0];
			var op = token[1];
			var opVal = int.Parse(token[2]);
			// 'if' is token 3
			var compReg = token[4];
			var compare = token[5];
			var compareVal = int.Parse(token[6]);

			if (!registers.ContainsKey(reg)) registers[reg] = 0;
			if (!registers.ContainsKey(compReg)) registers[compReg] = 0;
			int compRegVal = registers[compReg];

			bool conditionResult = false;
			switch (compare)
			{
				case "<":
					conditionResult = compRegVal < compareVal;
					break;
				case ">":
					conditionResult = compRegVal > compareVal;
					break;
				case "<=":
					conditionResult = compRegVal <= compareVal;
					break;
				case ">=":
					conditionResult = compRegVal >= compareVal;
					break;
				case "==":
					conditionResult = compRegVal == compareVal;
					break;
				case "!=":
					conditionResult = compRegVal != compareVal;
					break;
			}
			if (conditionResult)
			{
				switch (op)
				{
					case "inc":
						registers[reg] += opVal;
						break;
					case "dec":
						registers[reg] -= opVal;
						break;
				}
			}

			// For puzzle 2
			highestEverVal = Math.Max(highestEverVal, registers[reg]);
		}

		int maxVal = int.MinValue;
		foreach (var keypair in registers)
		{
			maxVal = Math.Max(keypair.Value, maxVal);
			Console.WriteLine("Register: " + keypair.Key + " = " + keypair.Value);
		}

		Console.WriteLine("Max register value = " + maxVal); // puzzle 1
		Console.WriteLine("Highest every value = " + highestEverVal); // puzzle 2
	}
}