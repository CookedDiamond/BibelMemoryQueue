using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelMemoryQueue
{
	public static class IOHandler
	{
		private static readonly char[] NEWLINE_SIGNS = { '?', '!', '.', ',', ';', ':' };

		public static void PrintNode(VerseNode node)
		{
			Console.WriteLine(node.Passage + "\n");
			Console.WriteLine(node.Content + "\n");
		}

		public static void PrintPassage(VerseNode node)
		{
			Console.WriteLine($"Passage: {node.Passage} (Priority: {node.GetPriority()})");
		}

		public static void PrintContent(VerseNode node)
		{
			string print = "";
			char[] oArray = node.Content.ToCharArray();
			for (int i = 0; i < oArray.Length; i++) {
				char c = oArray[i];
				print += c;
				if (c == ' ' && i >= 1)
				{
					foreach (char nl in NEWLINE_SIGNS)
					{
						if (oArray[i-1] == nl)
						{
							print += "\n";
						}
					}
				}
			}
			Console.WriteLine(print);
		}

		public static VerseNode CreateNewNode()
		{
			Console.WriteLine("Enter Passage:");
			string passage = Console.ReadLine();
			Console.WriteLine("Enter Content:");
			string content = Console.ReadLine();

			return new VerseNode(passage, content);
		}

		public static void HandleAfterVerse(VerseNode node, PriorityQueue queue)
		{
			Console.WriteLine("\n'y', 'n' or 'd'");
			string status = Console.ReadLine();
			if (status == "d")
			{
				queue.RemoveNode(node);
				return;
			}
			else if (status == "n")
			{
				node.Answer(false, queue);
				return;
			}
			else if (status == "y")
			{
				node.Answer(true, queue);
				return;
			}
			HandleAfterVerse(node, queue);
		}

		public static void HandleInit(PriorityQueue queue)
		{
			Console.WriteLine("Type 'n' for new verse, 'r' to reset list, nothing to memory");
			string input = Console.ReadLine();
			if (input == "n")
			{
                var newNode = CreateNewNode();
                queue.AddNode(newNode);
                SaveHandler.GenerateSave(queue);
                HandleInit(queue);
            }
			if (input == "r")
			{
				SaveHandler.DublicateSave();
				SaveHandler.ResetSave();
                HandleInit(new PriorityQueue());

            }
			
		}
	}
}
