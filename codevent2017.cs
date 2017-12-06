using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CodeVent2017
{
	class Program
	{
		static void Main(string[] args)
		{
			Day6Puzzle2();

			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		static void Day6Puzzle2()
		{
			string filename = "d6p1_b.txt";

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
			//D6P1_PrintStep(steps, newState);
			int phase = 0;
			string compareTo = "";
			while (true)
			{
				if (phase == 0)
				{
					bool dupeFound = D6P1_StatesContainDuplicates(states);
					if (dupeFound)
					{
						compareTo = states[states.Count - 1];
						phase = 1;
						steps = 0;
					}
				}
				else if (phase == 1)
				{
					if (string.Equals(states[states.Count -1], compareTo))
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
				//D6P1_PrintStep(steps, newState);
			}

			Console.WriteLine("Steps to duplicate: " + steps);
		}

		static bool D6P1_StatesContainDuplicates(List<string> states)
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

		static void D6P1_PrintStep(int stepIndex, string step)
		{
			Console.WriteLine("Step " + stepIndex + ": " + step);
		}

		static void Day6Puzzle1()
		{
			string filename = "d6p1_b.txt";

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
			//D6P1_PrintStep(steps, newState);
			while (!D6P1_StatesContainDuplicates(states))
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
				//D6P1_PrintStep(steps, newState);
			}

			Console.WriteLine("Steps to duplicate: " + steps);
		}

		static void Day5Puzzle2()
		{
			string filename = "d5p1_b.txt";

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

		static void Day5Puzzle1()
		{
			string filename = "d5p1_b.txt";

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

		static void Day4Puzzle2()
		{
			string filename = "d4p2_b.txt";

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

		static void Day4Puzzle1()
		{
			string filename = "d4p1_b.txt";

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

		static void Day3Puzzle2()
		{
			int[] inputs = new int[] { 1, 11, 123, 361527 };
			// Map row to column to int value
			var grid = new Dictionary<int, Dictionary<int, int>>();

			foreach (var input in inputs)
			{
				grid.Clear();
				int r = 0;
				int c = 0;
				int value = 1;
				grid[r] = new Dictionary<int, int>();
				grid[r][c] = value;

				int dir = 0; // 0 = right, 1 = up, 2 = left, 3 = down
				while (value <= input)
				{
					if (dir == 0)
					{
						// Move right
						c++;
						if (!grid.ContainsKey(r + 1) || !grid[r + 1].ContainsKey(c))
						{
							dir = 1;
						}
					}
					else if (dir == 1)
					{
						// Move up
						r++;
						if (!grid.ContainsKey(r))
						{
							grid[r] = new Dictionary<int, int>();
						}
						if (!grid[r].ContainsKey(c - 1))
						{
							dir = 2;
						}
					}
					else if (dir == 2)
					{
						// Move left
						c--;
						if (!grid.ContainsKey(r - 1) || !grid[r - 1].ContainsKey(c))
						{
							dir = 3;
						}
					}
					else
					{
						// Move down
						r--;
						if (!grid.ContainsKey(r))
						{
							grid[r] = new Dictionary<int, int>();
						}
						if (!grid[r].ContainsKey(c + 1))
						{
							dir = 0;
						}
					}

					value = 0;
					grid[r][c] = 0;
					for (int a = r - 1; a <= r + 1; a++)
					{
						for (int b = c - 1; b <= c + 1; b++)
						{
							if (grid.ContainsKey(a) && grid[a].ContainsKey(b))
							{
								value += grid[a][b];
							}
						}
					}
					grid[r][c] = value;
				}

				Console.WriteLine("Input: " + input + ", (r, c) = " + r + ", " + c + ", value = " + value);
			}
		}

		static void Day3Puzzle1b()
		{
			int[] inputs = new int[] { 1, 4, 9, 16, 25, 36, 11, 33, 19, 22, 12, 23, 1024, 361527 };
			// Map row to column to int value
			var grid = new Dictionary<int, Dictionary<int, int>>();

			foreach (var input in inputs)
			{
				grid.Clear();
				int r = 0;
				int c = 0;
				grid[r] = new Dictionary<int, int>();
				grid[r][c] = 1;

				int dir = 0; // 0 = right, 1 = up, 2 = left, 3 = down
				for (int i = 2; i <= input; i++)
				{
					if (dir == 0)
					{
						// Move right
						c++;
						if (!grid.ContainsKey(r+1) || !grid[r+1].ContainsKey(c))
						{
							dir = 1;
						}
					}
					else if (dir == 1)
					{
						// Move up
						r++;
						if (!grid.ContainsKey(r))
						{
							grid[r] = new Dictionary<int, int>();
						}
						if (!grid[r].ContainsKey(c-1))
						{
							dir = 2;
						}
					}
					else if (dir == 2)
					{
						// Move left
						c--;
						if (!grid.ContainsKey(r - 1) || !grid[r - 1].ContainsKey(c))
						{
							dir = 3;
						}
					}
					else
					{
						// Move down
						r--;
						if (!grid.ContainsKey(r))
						{
							grid[r] = new Dictionary<int, int>();
						}
						if (!grid[r].ContainsKey(c + 1))
						{
							dir = 0;
						}
					}
					grid[r][c] = i;
				}

				int dist = Math.Abs(r) + Math.Abs(c);
				Console.WriteLine("Input: " + input + ", (r, c) = " + r + ", " + c + ", dist = " + dist);
			}
		}

		// An O(1) solution that turns out to be useless at solving Puzzle 2...
		static void Day3Puzzle1()
		{
			int[] inputs = new int[] { 1, 4, 9, 16, 25, 36, 11, 33, 19, 22, 12, 23, 1024, 361527 };
			foreach (var input in inputs)
			{
				int c = 0;
				int r = 0;

				double s = Math.Sqrt((double)input);
				int f = (int)Math.Floor(s);
				if (f*f == input)
				{
					// input is a perfect square
					if (f % 2 == 0)
					{
						// Even square - top left corner
						c = -f / 2 + 1;
						r = f / 2;
					}
					else
					{
						// Odd square - bottom right corner
						c = (f - 1) / 2;
						r = -c;
					}
				}
				else
				{
					int f1 = f + 1;
					int bottom = f * f;
					int top = f1 * f1;
					int delta = top - bottom;
					int halfDelta = (delta - 1) / 2;
					int offset = top - input;

					if (top % 2 == 0)
					{
						// Even square - top left corner
						c = -f1 / 2 + 1;
						r = f1 / 2;
					}
					else
					{
						// Odd square - bottom right corner
						c = (f1 - 1) / 2;
						r = -c;
					}

					if (offset > halfDelta)
					{
						// Item is on the vertical
						int cOffset = halfDelta;
						int rOffset = -(offset - halfDelta);
						if (top % 2 == 1)
						{
							cOffset *= -1;
							rOffset *= -1;
						}
						c += cOffset;
						r += rOffset;
					}
					else
					{
						// Item is on the horizontal
						int cOffset = offset;
						if (top % 2 == 1)
						{
							cOffset *= -1;
						}
						c += cOffset;
					}
				}

				int dist = Math.Abs(c) + Math.Abs(r);
				Console.WriteLine("Input = " + input + ", Output: c = " + c + ", r = " + r + ", dist = " + dist);
			}
		}

		static void Day2Puzzle2()
		{
			string filename = "d2p2_b.txt";

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

		static void Day2Puzzle1()
		{
			string filename = "d2p1_b.txt";

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

		static void Day1Puzzle2()
		{
			// http://adventofcode.com/2017/day/1
			string[] inputs = {
				"1212", // 6
				"1221", // 0
				"123425", // 4
				"123123", // 12
				"12131415", // 4
				"181445682966897848665963472661939865313976877194312684993521259486517527961396717561854825453963181134379574918373213732184697746668399631642622373684425326112585283946462323363991753895647177797691214784149215198715986947573668987188746878678399624533792551651335979847131975965677957755571358934665327487287312467771187981424785514785421781781976477326712674311994735947987383516699897916595433228294198759715959469578766739518475118771755787196238772345762941477359483456641194685333528329581113788599843621326313592354167846466415943566183192946217689936174884493199368681514958669615226362538622898367728662941275658917124167353496334664239539753835439929664552886538885727235662548783529353611441231681613535447417941911479391558481443933134283852879511395429489152435996669232681215627723723565872291296878528334773391626672491878762288953597499218397146685679387438634857358552943964839321464529237533868734473777756775687759355878519113426969197211824325893376812556798483325994128743242544899625215765851923959798197562831313891371735973761384464685316273343541852758525318144681364492173465174512856618292785483181956548813344752352933634979165667651165776587656468598791994573513652324764687515345959621493346623821965554755615219855842969932269414839446887613738174567989512857785566352285988991946436148652839391593178736624957214917527759574235133666461988355855613377789115472297915429318142824465141688559333787512328799783539285826471818279818457674417354335454395644435889386297695625378256613558911695145397779576526397241795181294322797687168326696497256684943829666672341162656479563522892141714998477865114944671225898297338685958644728534192317628618817551492975251364233974374724968483637518876583946828819994321129556511537619253381981544394112184655586964655164192552352534626295996968762388827294873362719636616182786976922445125551927969267591395292198155775434997827738862786341543524544822321112131815475829945625787561369956264826651461575948462782869972654343749617939132353399334744265286151177931594514857563664329299713436914721119746932159456287267887878779218815883191236858656959258484139254446341"
			};

			for (int j = 0; j < inputs.Length; j++)
			{
				var input = inputs[j];
				Console.WriteLine("Input = " + input);

				int total = 0;
				for (int i = 0; i < input.Length; i++)
				{
					int val = (int)(input[i] - '0');
					int next = (int)(input[(i + (input.Length / 2)) % input.Length] - '0');

					if (val == next)
					{
						total += val;
					}
				}

				Console.WriteLine("Total value = " + total);
			}
		}

		static void Day1Puzzle1()
		{
			// http://adventofcode.com/2017/day/1
			//string input = "1122"; // 3
			//string input = "1111"; // 4
			//string input = "91212129"; // 9
			string input = "181445682966897848665963472661939865313976877194312684993521259486517527961396717561854825453963181134379574918373213732184697746668399631642622373684425326112585283946462323363991753895647177797691214784149215198715986947573668987188746878678399624533792551651335979847131975965677957755571358934665327487287312467771187981424785514785421781781976477326712674311994735947987383516699897916595433228294198759715959469578766739518475118771755787196238772345762941477359483456641194685333528329581113788599843621326313592354167846466415943566183192946217689936174884493199368681514958669615226362538622898367728662941275658917124167353496334664239539753835439929664552886538885727235662548783529353611441231681613535447417941911479391558481443933134283852879511395429489152435996669232681215627723723565872291296878528334773391626672491878762288953597499218397146685679387438634857358552943964839321464529237533868734473777756775687759355878519113426969197211824325893376812556798483325994128743242544899625215765851923959798197562831313891371735973761384464685316273343541852758525318144681364492173465174512856618292785483181956548813344752352933634979165667651165776587656468598791994573513652324764687515345959621493346623821965554755615219855842969932269414839446887613738174567989512857785566352285988991946436148652839391593178736624957214917527759574235133666461988355855613377789115472297915429318142824465141688559333787512328799783539285826471818279818457674417354335454395644435889386297695625378256613558911695145397779576526397241795181294322797687168326696497256684943829666672341162656479563522892141714998477865114944671225898297338685958644728534192317628618817551492975251364233974374724968483637518876583946828819994321129556511537619253381981544394112184655586964655164192552352534626295996968762388827294873362719636616182786976922445125551927969267591395292198155775434997827738862786341543524544822321112131815475829945625787561369956264826651461575948462782869972654343749617939132353399334744265286151177931594514857563664329299713436914721119746932159456287267887878779218815883191236858656959258484139254446341";

			Console.WriteLine("Input = " + input);

			int total = 0;
			for (int i = 0; i < input.Length; i++)
			{
				int val = (int)(input[i] - '0');
				int next = (int)(input[(i + 1) % input.Length] - '0');

				if (val == next)
				{
					total += val;
				}
			}

			Console.WriteLine("Total value = " + total);
		}
	}
}
