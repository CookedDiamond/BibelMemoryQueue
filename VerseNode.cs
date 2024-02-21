using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelMemoryQueue
{
	[Serializable]
	public class VerseNode
	{
		private static readonly float POINTS_ALL_TIME = 10f;
		private static readonly float POINTS_LAST_FIVE = 30f;
		private static readonly float POINTS_PER_TIME_INTERVALL = 10f;
		private static readonly int[] TIME_INTERVALLS = {3,7,15,30,60,90,180};
		public string Passage { private set; get; }
		public string Content { private set; get; }
		public bool WasChosenLast { set; get; }
		public List<bool> AnswerHistory { set; get; }
		public DateTime LastRight { get; set; }
		public VerseNode(string passage, string content)
		{
            AnswerHistory = new List<bool>();
			LastRight = DateTime.Now;
            Passage = passage;
			Content = content;
		}

		public float GetPriority()
		{
			float result = 0;
			// Last Chosen points
			if (WasChosenLast) return float.MaxValue;

			// All answer points
			int trueAnsweres = 0;
			foreach (var answer in AnswerHistory)
			{
				if (answer) trueAnsweres++;
			}

			float allAnsweresScore = (float) trueAnsweres/ AnswerHistory.Count() * POINTS_ALL_TIME;
			result += allAnsweresScore;

            // Last few answeres points
            int lastFiveTrueAnsweres = 0;
			for (int i = Math.Max(5, AnswerHistory.Count - 6); i < AnswerHistory.Count; i++)
			{
				if (AnswerHistory[i]) lastFiveTrueAnsweres++;
            }
			float lastFiveAnsweresScore = (lastFiveTrueAnsweres / 5f) * POINTS_LAST_FIVE;
			result += lastFiveAnsweresScore;

            // Time intervall points
            var timeDiff = DateTime.Now - LastRight;
			double dayDiff = timeDiff.TotalDays;
			float timeScore = 0;
			foreach (var ti in TIME_INTERVALLS)
			{
				if (dayDiff > ti)
				{
                    timeScore -= POINTS_PER_TIME_INTERVALL;
                }
			}
			result += timeScore;

			// Console.WriteLine($"passage: {Passage} Score: {result}, allTime: {allAnsweresScore}, lastFive: {lastFiveAnsweresScore}, time: {timeScore}");
            return result;
		}

		public void Answer(bool remembered, PriorityQueue queue)
		{
			AnswerHistory.Add(remembered);
			WasChosenLast = true;
			SaveHandler.GenerateSave(queue);
			if (remembered)
            {
                LastRight = DateTime.Now;
            }
		}

        public override string ToString()
        {
            return $"{Passage} {GetPriority()}";
        }
    }
}
