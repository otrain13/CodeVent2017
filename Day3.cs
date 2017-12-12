using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day3
{
	static public void Puzzle2()
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

	static public void Puzzle1b()
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
				grid[r][c] = i;
			}

			int dist = Math.Abs(r) + Math.Abs(c);
			Console.WriteLine("Input: " + input + ", (r, c) = " + r + ", " + c + ", dist = " + dist);
		}
	}

	// An O(1) solution that turns out to be useless at solving Puzzle 2...
	static public void Puzzle1()
	{
		int[] inputs = new int[] { 1, 4, 9, 16, 25, 36, 11, 33, 19, 22, 12, 23, 1024, 361527 };
		foreach (var input in inputs)
		{
			int c = 0;
			int r = 0;

			double s = Math.Sqrt((double)input);
			int f = (int)Math.Floor(s);
			if (f * f == input)
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
}