using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day2
{
	static public void Puzzle2()
	{
		string filename = "input/d2p2_b.txt";

		int total = 0;
		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var values = line.Split('\t');
			var intValues = new int[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				intValues[i] = int.Parse(values[i]);
			}

			for (int a = 0; a < intValues.Length; a++)
			{
				bool found = false;
				for (int b = 0; b < intValues.Length; b++)
				{
					if (a == b) continue;
					if (intValues[a] % intValues[b] == 0)
					{
						// b divides a
						total += intValues[a] / intValues[b];
						found = true;
						break;
					}
				}
				if (found) break;
			}
		}
		Console.WriteLine("Checksum = " + total);
	}

	static public void Puzzle1()
	{
		string filename = "input/d2p1_b.txt";

		int total = 0;
		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var values = line.Split('\t');
			int min = int.MaxValue;
			int max = int.MinValue;
			for (int i = 0; i < values.Length; i++)
			{
				int val = int.Parse(values[i]);
				min = Math.Min(min, val);
				max = Math.Max(max, val);
			}
			total += (max - min);
		}
		Console.WriteLine("Checksum = " + total);
	}
}