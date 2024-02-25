using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelMemoryQueue
{

	[Serializable]
	public class PriorityQueue {
		private static readonly float RANDOM_VERSE_CHANCE = 0.2f;


		public List<VerseNode> Verses { get; set; }

		public PriorityQueue()
		{
			Verses = new List<VerseNode>();
		}

		public void AddNode(VerseNode node)
		{
			Verses.Add(node);
		}

		public VerseNode GetNext()
		{
			if (Random.Shared.NextDouble() < RANDOM_VERSE_CHANCE) {
				return GetRandom();
			}

			Verses = Verses.OrderBy(n => n.GetPriority()).ToList();

			var node = Verses.First();

			foreach (var updateNode in Verses)
			{
				updateNode.WasChosenLast = false;
			}

			return node;
		}

		public VerseNode GetRandom() {
			int random = Random.Shared.Next(0, Verses.Count);
			Console.Write("(selected by random) ");
			return Verses[random];
		}

		public void RemoveNode(VerseNode node) {
			Verses.Remove(node);
		}
	}
}
