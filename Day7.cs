using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Day7
{
	public class TowerNode
	{
		public string name;
		public int weight;
		public int totalChildWeight = 0;
		public string[] subTowerNames = null;
		public TowerNode parentNode = null;
		public List<TowerNode> subTowers = null;

		public TowerNode(string name, int weight)
		{
			this.name = name;
			this.weight = weight;
			subTowerNames = new string[0];
			subTowers = new List<TowerNode>();
		}
	}

	static TowerNode FindAttachPoint(TowerNode rootNode, string nameToFind)
	{
		if (rootNode == null) return null;

		foreach (var childName in rootNode.subTowerNames)
		{
			if (string.Equals(childName, nameToFind))
			{
				return rootNode;
			}
		}

		foreach (var tower in rootNode.subTowers)
		{
			var foundNode = FindAttachPoint(tower, nameToFind);
			if (foundNode != null)
			{
				return foundNode;
			}
		}
		return null;
	}

	static void CalcTowerWeights(TowerNode rootNode)
	{
		rootNode.totalChildWeight = rootNode.weight;

		foreach (var subtower in rootNode.subTowers)
		{
			CalcTowerWeights(subtower);
			rootNode.totalChildWeight += subtower.totalChildWeight;
		}
	}

	static TowerNode FindLastUnbalancedNode(TowerNode rootNode)
	{
		var valCount = new Dictionary<int, int>();
		var valIndices = new Dictionary<int, int>();
		for (int i = 0; i < rootNode.subTowers.Count; i++)
		{
			var st = rootNode.subTowers[i];
			if (!valCount.ContainsKey(st.totalChildWeight))
			{
				valCount[st.totalChildWeight] = 0;
			}
			valCount[st.totalChildWeight]++;
			valIndices[st.totalChildWeight] = i;
		}
		int unbalancedIndex = -1;
		foreach (var keypair in valCount)
		{
			if (keypair.Value == 1)
			{
				unbalancedIndex = valIndices[keypair.Key];
			}
		}
		if (unbalancedIndex == -1)
		{
			// This is a balanced subtower
			return rootNode;
		}
		else
		{
			return FindLastUnbalancedNode(rootNode.subTowers[unbalancedIndex]);
		}
	}

	static public void Puzzle1()
	{
		string filename = "input/d7p1_b.txt";

		var lines = File.ReadAllLines(filename);
		var rootNode = new TowerNode("", -1); // Just used to keep everything organized
		var allNodes = new List<TowerNode>();

		// First create all the nodes and then organize them into a tree after
		foreach (var line in lines)
		{
			// Parse the line - aaaa (32) -> bbbb, cccc, dddd, ...
			var chunks = line.Split(' ');
			var name = chunks[0];
			var weight = int.Parse(chunks[1].Split('(')[1].Split(')')[0]);
			string[] children = new string[0];
			if (chunks.Length > 3)
			{
				children = new string[chunks.Length - 3];
				for (int c = 3; c < chunks.Length; c++)
				{
					children[c - 3] = chunks[c].Split(',')[0];
				}
			}
			var tower = new TowerNode(name, weight);
			tower.subTowerNames = children;
			allNodes.Add(tower);
		}

		// Now we can build the tree
		foreach (var node in allNodes)
		{
			// Find any nodes in the tree that need this node as a child and connect them
			TowerNode attachPoint = null;
			foreach (var tower in rootNode.subTowers)
			{
				attachPoint = FindAttachPoint(tower, node.name);
				if (attachPoint != null)
				{
					attachPoint.subTowers.Add(node);
					node.parentNode = attachPoint;
					break;
				}
			}
			if (attachPoint == null)
			{
				rootNode.subTowers.Add(node);
			}

			// Find any child nodes that are already in the tree and connect them
			for (int t = rootNode.subTowers.Count - 1; t >= 0; t--)
			{
				var tower = rootNode.subTowers[t];
				foreach (var childName in node.subTowerNames)
				{
					if (string.Equals(childName, tower.name))
					{
						// Remove the found root node and add it as a child to the new node
						node.subTowers.Add(tower);
						tower.parentNode = node;
						rootNode.subTowers.RemoveAt(t);
					}
				}
			}
		}

		Console.WriteLine("Number of root nodes: " + rootNode.subTowers.Count);
		Console.WriteLine("Root tower name: " + rootNode.subTowers[0].name);

		// Part 2 - Uses all the existing code plus more stuff
		CalcTowerWeights(rootNode.subTowers[0]);
		var unbalancedNode = FindLastUnbalancedNode(rootNode.subTowers[0]);
		var parent = unbalancedNode.parentNode;

		Console.Write("Tower weights: ");
		var valCount = new Dictionary<int, int>();
		var valIndices = new Dictionary<int, int>();
		for (int i = 0; i < parent.subTowers.Count; i++)
		{
			var st = parent.subTowers[i];
			Console.Write(st.totalChildWeight + " ");
			if (!valCount.ContainsKey(st.totalChildWeight))
			{
				valCount[st.totalChildWeight] = 0;
			}
			valCount[st.totalChildWeight]++;
			valIndices[st.totalChildWeight] = i;
		}
		Console.WriteLine();
		int unbalancedIndex = -1;
		int balancedValue = -1;
		foreach (var keypair in valCount)
		{
			if (keypair.Value == 1)
			{
				unbalancedIndex = valIndices[keypair.Key];
			}
			else
			{
				balancedValue = keypair.Key;
			}
		}
		Console.WriteLine("Index of unbalanced tower: " + unbalancedIndex);
		int weightDelta = parent.subTowers[unbalancedIndex].totalChildWeight - balancedValue;
		int newWeight = parent.subTowers[unbalancedIndex].weight - weightDelta;
		Console.WriteLine("Old weight: " + parent.subTowers[unbalancedIndex].weight + ", new weight: " + newWeight);
	}
}
