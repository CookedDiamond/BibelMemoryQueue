using System.Diagnostics;
using System.Text.Json;
using System.Xml;

namespace BibelMemoryQueue
{
	public class SaveHandler
	{
		private static readonly string FILE_NAME = "save.json";

		public static void GenerateSave(PriorityQueue queue)
		{
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string save = JsonSerializer.Serialize(queue, options);
			File.WriteAllText(FILE_NAME, save);
		}

		public static PriorityQueue LoadSave()
		{
			string jsonString = File.ReadAllText(FILE_NAME);
			if (jsonString == null)
			{
				Console.WriteLine("Error loading json");
				Environment.Exit(0);
			}
			return JsonSerializer.Deserialize<PriorityQueue>(jsonString);
		}

		public static void ResetSave()
		{
			string save = JsonSerializer.Serialize(new PriorityQueue());
			File.WriteAllText(FILE_NAME, save);
		}

		public static void DublicateSave()
		{
            string jsonString = File.ReadAllText(FILE_NAME);
            File.WriteAllText("dublicate_"+FILE_NAME, jsonString);
        }
	}
}
