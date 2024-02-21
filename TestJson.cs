using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibelMemoryQueue
{
	internal class TestJson
	{
		public List<string> Test { set; get; }

		public TestJson() {
			Test = new List<string>();
			Test.Add("Entry1");
			Test.Add("Entry2");
		}
	}
}
