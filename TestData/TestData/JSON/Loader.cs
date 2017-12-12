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

		public Loader()
		{

		}
		

		public bool LoadFromJSON<T>(string path, out T result)
		{
			bool success = true;

			try
			{
				using (StreamReader file = File.OpenText(path))
				{
					string json = file.ReadToEnd();

					try
					{
						result = JsonConvert.DeserializeObject<T>(json);
					}
					catch (Newtonsoft.Json.JsonSerializationException jse)
					{
						Console.WriteLine(jse.Message);
						result = default(T);
						success = false;
					}
					catch (Newtonsoft.Json.JsonReaderException jre)
					{
						Console.WriteLine(jre.Message);
						result = default(T);
						success = false;
					}
				}
			}
			catch (FileNotFoundException fnfe)
			{
				Console.WriteLine(fnfe.Message);
				result = default(T);
				success = false;
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine(ae.Message);
				result = default(T);
				success = false;
			}

			return success;
		}
		
		
	}
}
