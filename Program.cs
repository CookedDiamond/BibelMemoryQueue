// See https://aka.ms/new-console-template for more information
using BibelMemoryQueue;

var queue = SaveHandler.LoadSave();
IOHandler.HandleInit(queue);

while (true)
{
	Console.Clear();

	var nextNode = queue.GetNext();
	IOHandler.PrintPassage(nextNode);
	Console.ReadLine();
	IOHandler.PrintContent(nextNode);

	IOHandler.HandleAfterVerse(nextNode, queue);
}
