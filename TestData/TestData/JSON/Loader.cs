using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;


namespace TestData.JSON
{
	//reads from JSON-files and populates SQL db
	public class Loader
	{

	

		public void LoadFromJSON<T>(string path, out T result)
		{
			using (StreamReader file = File.OpenText(path))
			{
				string json = file.ReadToEnd();

				result = JsonConvert.DeserializeObject<T>(json);

			}
		}
		
		
	}
}
