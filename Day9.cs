using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day9
{
	enum State
	{
		None = 0,
		Group,
		Garbage,
		Escape
	};

	static public void Puzzle1()
	{
		string filename = "input/d9p1_b.txt";

		var lines = File.ReadAllLines(filename);
		// Each line is a separate input
		foreach (var stream in lines)
		{
			int score = 0;
			int depth = 0;
			var states = new List<State>();
			states.Add(State.None);
			State curState = State.None;
			int garbageCharCount = 0;

			foreach (char c in stream)
			{
				//Console.WriteLine("c = " + c + ", state = " + curState);
				if (curState == State.None)
				{
					if (c == '{')
					{
						states.Add(State.Group);
						curState = State.Group;
						depth = 1;
						score += depth;
					}
					else if (c == '<')
					{
						states.Add(State.Garbage);
						curState = State.Garbage;
					}
				}
				else if (curState == State.Group)
				{
					if (c == '{')
					{
						states.Add(State.Group);
						curState = State.Group;
						depth++;
						score += depth;
					}
					else if (c == '}')
					{
						states.RemoveAt(states.Count - 1);
						curState = states.Last();
						depth--;
					}
					else if (c == '<')
					{
						states.Add(State.Garbage);
						curState = State.Garbage;
					}
				}
				else if (curState == State.Garbage)
				{
					if (c == '>')
					{
						states.RemoveAt(states.Count - 1);
						curState = states.Last();
					}
					else if (c == '!')
					{
						// A ! inside garbage escapes the next character
						states.Add(State.Escape);
						curState = State.Escape;
					}
					else
					{
						garbageCharCount++;
					}
				}
				else if (curState == State.Escape)
				{
					// Skip this character and exit the escape state
					states.RemoveAt(states.Count - 1);
					curState = states.Last();
				}
			}

			Console.WriteLine("Input: " + stream + " = " + score); // puzzle 1
			Console.WriteLine("Garbage char count = " + garbageCharCount); // puzzle 2
		}
	}
}