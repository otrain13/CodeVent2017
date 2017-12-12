using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day6
{
	static public void Puzzle2()
	{
		string filename = "input/d6p1_b.txt";

		var dataStrs = File.ReadAllLines(filename);
		var dataStr = dataStrs[0];
		Console.WriteLine("Input: " + dataStr);
		var data = dataStr.Split('\t');
		int numData = data.Length;
		var intData = new int[numData];
		for (int d = 0; d < numData; d++)
		{
			intData[d] = int.Parse(data[d]);
		}

		var states = new List<string>();
		var newState = "";
		for (int i = 0; i < numData; i++)
		{
			newState += (char)(intData[i] + '0');
		}
		states.Add(newState);

		var steps = 0;
		//PrintStep(steps, newState);
		int phase = 0;
		string compareTo = "";
		while (true)
		{
			if (phase == 0)
			{
				bool dupeFound = StatesContainDuplicates(states);
				if (dupeFound)
				{
					compareTo = states[states.Count - 1];
					phase = 1;
					steps = 0;
				}
			}
			else if (phase == 1)
			{
				if (string.Equals(states[states.Count - 1], compareTo))
				{
					phase = 2;
					break;
				}
			}

			// Update the states
			int maxIndex = -1;
			int maxVal = -1;
			for (int i = 0; i < numData; i++)
			{
				if (intData[i] > maxVal)
				{
					maxIndex = i;
					maxVal = intData[i];
				}
			}

			intData[maxIndex] = 0;
			while (maxVal > 0)
			{
				maxIndex = (maxIndex + 1) % numData;
				intData[maxIndex]++;
				maxVal--;
			}

			newState = "";
			for (int i = 0; i < numData; i++)
			{
				newState += (char)(intData[i] + '0');
			}
			states.Add(newState);

			steps++;
			//PrintStep(steps, newState);
		}

		Console.WriteLine("Steps to duplicate: " + steps);
	}

	static bool StatesContainDuplicates(List<string> states)
	{
		bool isSame = false;
		if (states.Count == 0) return isSame;
		var newState = states[states.Count - 1];
		for (int s = 0; s < states.Count - 1; s++)
		{
			isSame = string.Equals(states[s], newState);
			if (isSame) break;
		}
		return isSame;
	}

	static void PrintStep(int stepIndex, string step)
	{
		Console.WriteLine("Step " + stepIndex + ": " + step);
	}

	static public void Puzzle1()
	{
		string filename = "input/d6p1_b.txt";

		var dataStrs = File.ReadAllLines(filename);
		var dataStr = dataStrs[0];
		Console.WriteLine("Input: " + dataStr);
		var data = dataStr.Split('\t');
		int numData = data.Length;
		var intData = new int[numData];
		for (int d = 0; d < numData; d++)
		{
			intData[d] = int.Parse(data[d]);
		}

		var states = new List<string>();
		var newState = "";
		for (int i = 0; i < numData; i++)
		{
			newState += (char)(intData[i] + '0');
		}
		states.Add(newState);

		var steps = 0;
		//PrintStep(steps, newState);
		while (!StatesContainDuplicates(states))
		{
			// Update the states
			int maxIndex = -1;
			int maxVal = -1;
			for (int i = 0; i < numData; i++)
			{
				if (intData[i] > maxVal)
				{
					maxIndex = i;
					maxVal = intData[i];
				}
			}

			intData[maxIndex] = 0;
			while (maxVal > 0)
			{
				maxIndex = (maxIndex + 1) % numData;
				//Console.WriteLine("maxIndex = " + maxIndex + ", data at index = " + intData[maxIndex]);
				intData[maxIndex]++;
				maxVal--;
			}

			newState = "";
			for (int i = 0; i < numData; i++)
			{
				newState += (char)(intData[i] + '0');
			}
			states.Add(newState);

			steps++;
			//PrintStep(steps, newState);
		}

		Console.WriteLine("Steps to duplicate: " + steps);
	}
}