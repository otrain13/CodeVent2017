using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day5
{
	static public void Puzzle2()
	{
		string filename = "input/d5p1_b.txt";

		var lines = File.ReadAllLines(filename);
		var instructions = new int[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			instructions[i] = int.Parse(lines[i]);
		}

		int steps = 0;
		int pc = 0;
		int programSize = instructions.Length;
		while (pc >= 0 && pc < programSize)
		{
			int jump = instructions[pc];
			if (jump >= 3)
			{
				instructions[pc]--;
			}
			else
			{
				instructions[pc]++;
			}
			pc += jump;
			steps++;
		}
		Console.WriteLine("Steps before exit = " + steps);
	}

	static public void Puzzle1()
	{
		string filename = "input/d5p1_b.txt";

		var lines = File.ReadAllLines(filename);
		var instructions = new int[lines.Length];
		for (int i = 0; i < lines.Length; i++)
		{
			instructions[i] = int.Parse(lines[i]);
		}

		int steps = 0;
		int pc = 0;
		int programSize = instructions.Length;
		while (pc >= 0 && pc < programSize)
		{
			int jump = instructions[pc];
			instructions[pc]++;
			pc += jump;
			steps++;
		}
		Console.WriteLine("Steps before exit = " + steps);
	}
}