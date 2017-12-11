using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestData.JSON
{
	class Event
	{
		public string Date { get; set; }
		public string Description { get; set; }
		public string Lang { get; set; }
		public string Category1 { get; set; }
		public string Category2 { get; set; }
		public string Category3 { get; set; }
		public string Granularity { get; set; }
	}

	class City
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public float Lat { get; set; }
		public float Lng { get; set; }
	}

}
