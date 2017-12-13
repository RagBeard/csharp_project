using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestData.JSON;

namespace TestData
{
	//class for processing the data from JSON before it's put into SQL db

	public class Processing
	{
		// When City.Country is actually a Country Code, look up which country that code represents
		// this modifies the List<City> input
		public void CountryCodesToCountryNames(List<City> cities, List<Country> countries)
		{
			foreach (var city in cities)
			{
				var country = countries.Find((v) => v.Code == city.Country);

				if (country != null)
				{
					city.Country = country.Name;
				}
				else
				{
					Console.Write(" miss " + city.Country);
				}
			}			
		}




	}
}
