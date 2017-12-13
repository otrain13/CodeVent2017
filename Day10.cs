using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day10
{
	static public void Puzzle2()
	{
		string filename = "input/d10p1_b.txt";
		int listSize = 256;

		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var list = new int[listSize];
			for (int i = 0; i < listSize; i++)
			{
				list[i] = i;
			}

			var inputLengths = new int[line.Length + 5];
			for (int i = 0; i < line.Length; i++)
			{
				inputLengths[i] = (int)(line[i]);
			}
			inputLengths[line.Length + 0] = 17;
			inputLengths[line.Length + 1] = 31;
			inputLengths[line.Length + 2] = 73;
			inputLengths[line.Length + 3] = 47;
			inputLengths[line.Length + 4] = 23;

			int curPos = 0;
			int skipSize = 0;
			for (int r = 0; r < 64; r++)
			{
				foreach (var inputLength in inputLengths)
				{
					//Console.WriteLine("curPos = " + curPos + ", skipSize = " + skipSize);
					int startPos = curPos;
					int endPos = (curPos + inputLength - 1) % listSize;
					for (int s = 0; s < inputLength / 2; s++)
					{
						int temp = list[startPos];
						list[startPos] = list[endPos];
						list[endPos] = temp;
						startPos = (startPos + 1) % listSize;
						endPos = ((endPos - 1) + listSize) % listSize;
					}
					curPos = (curPos + inputLength + skipSize) % listSize;
					skipSize++;

					//Console.Write("Step with after input " + inputLength + " : ");
					//foreach (var listVal in list)
					//{
					//	Console.Write(listVal + " ");
					//}
					//Console.WriteLine();
				}
			}

			// Hash the chunks of 16
			var chunks = new int[16];
			for (int a = 0; a < 16; a++)
			{
				chunks[a] = list[a * 16];
				for (int b = 1; b < 16; b++)
				{
					int index = (a * 16) + b;
					chunks[a] = chunks[a] ^ list[index];
				}
			}

			// Convert chunks to hex values and compress into a string
			string hash = "";
			foreach (var chunk in chunks)
			{
				hash += chunk.ToString("X");
			}
				
			Console.WriteLine("Input: " + line);
			//Console.Write("Final State: ");
			//foreach (var listVal in list)
			//{
			//	Console.Write(listVal + " ");
			//}
			//Console.WriteLine();
			Console.WriteLine("Hash = " + hash);
		}
	}

	static public void Puzzle1()
	{
		string filename = "input/d10p1_b.txt";
		int listSize = 256;

		var lines = File.ReadAllLines(filename);
		foreach (var line in lines)
		{
			var list = new int[listSize];
			for (int i = 0; i < listSize; i++)
			{
				list[i] = i;
			}

			var intStrs = line.Split(',');
			var inputLengths = new int[intStrs.Length];
			for (int i = 0; i < inputLengths.Length; i++)
			{
				inputLengths[i] = int.Parse(intStrs[i]);
			}

			int curPos = 0;
			int skipSize = 0;
			foreach (var inputLength in inputLengths)
			{
				Console.WriteLine("curPos = " + curPos + ", skipSize = " + skipSize);
				int startPos = curPos;
				int endPos = (curPos + inputLength - 1) % listSize;
				for (int s = 0; s < inputLength / 2; s++)
				{
					int temp = list[startPos];
					list[startPos] = list[endPos];
					list[endPos] = temp;
					startPos = (startPos + 1) % listSize;
					endPos = ((endPos - 1) + listSize) % listSize;
				}
				curPos = (curPos + inputLength + skipSize) % listSize;
				skipSize++;

				Console.Write("Step with after input " + inputLength + " : ");
				foreach (var listVal in list)
				{
					Console.Write(listVal + " ");
				}
				Console.WriteLine();
			}

			var hash = list[0] * list[1];

			Console.WriteLine("Input: " + line);
			Console.Write("Final State: ");
			foreach (var listVal in list)
			{
				Console.Write(listVal + " ");
			}
			Console.WriteLine();
			Console.WriteLine("First two values multiplied = " + hash);
		}
	}
}