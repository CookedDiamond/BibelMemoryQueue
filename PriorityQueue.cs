using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelMemoryQueue
{

	[Serializable]
	public class PriorityQueue
	{
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
			Verses = Verses.OrderBy(n => n.GetPriority()).ToList();

			var node = Verses.First();

			foreach (var updateNode in Verses)
			{
				updateNode.WasChosenLast = false;
			}

			return node;
		}

		public void RemoveNode(VerseNode node) {
			Verses.Remove(node);
		}
	}
}
