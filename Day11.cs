using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day11
{
	static int CalcDist(int x, int y, int z)
	{
		return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
	}

	static public void Puzzle1()
	{
		string filename = "input/d11p1_a.txt";

		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var steps = line.Split(',');
			int x = 0;
			int y = 0;
			int z = 0;
			int maxDist = int.MinValue;
			foreach (var step in steps)
			{
				if (step == "n")
				{
					y++;
					z--;
				}
				else if (step == "s")
				{
					y--;
					z++;
				}
				else if (step == "ne")
				{
					x++;
					z--;
				}
				else if (step == "se")
				{
					x++;
					y--;
				}
				else if (step == "nw")
				{
					x--;
					y++;
				}
				else if (step == "sw")
				{
					x--;
					z++;
				}
				maxDist = Math.Max(maxDist, CalcDist(x, y, z));
			}

			int dist = CalcDist(x, y, z);
			Console.WriteLine("Distance = " + dist);
			Console.WriteLine("Max distance = " + maxDist);
		}
	}
}