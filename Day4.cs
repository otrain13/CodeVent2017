using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day4
{
	static public void Puzzle2()
	{
		string filename = "input/d4p2_b.txt";

		int numValid = 0;
		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var words = line.Split(' ');
			bool dupe = false;
			for (int i = 0; i < words.Length - 1; i++)
			{
				int[] charCounts1 = new int[26];
				foreach (var c in words[i])
				{
					charCounts1[c - 'a']++;
				}

				for (int j = i + 1; j < words.Length; j++)
				{
					int[] charCounts2 = new int[26];
					foreach (var c in words[j])
					{
						charCounts2[c - 'a']++;
					}

					dupe = true;
					for (int cc = 0; cc < 26; cc++)
					{
						if (charCounts1[cc] != charCounts2[cc])
						{
							dupe = false;
							break;
						}
					}
					if (dupe) break;
				}
				if (dupe) break;
			}

			if (!dupe)
			{
				numValid++;
			}
			Console.WriteLine(line + " - " + !dupe);
		}
		Console.WriteLine("Num valid pass phrases = " + numValid);
	}

	static public void Puzzle1()
	{
		string filename = "input/d4p1_b.txt";

		int numValid = 0;
		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var words = line.Split(' ');
			bool dupe = false;
			for (int i = 0; i < words.Length - 1; i++)
			{
				for (int j = i + 1; j < words.Length; j++)
				{
					if (string.Equals(words[i], words[j]))
					{
						dupe = true;
						break;
					}
				}
				if (dupe) break;
			}

			if (!dupe)
			{
				numValid++;
			}
			Console.WriteLine(line + " - " + !dupe);
		}
		Console.WriteLine("Num valid pass phrases = " + numValid);
	}
}
