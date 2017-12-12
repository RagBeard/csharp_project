using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestData.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestData.JSON.Tests
{
	[TestClass()]
	public class LoaderTests
	{
		private class TestClass
		{
			public string a { get; set; }
			string b { get; set; }
		}

		private void LoadFromJSONTestFileNotFound()
		{
			//arrange
			Loader loader = new Loader();
			string path = @"F:\Projects\GitRepos\csharp_project\datasets\nofile";
			TestClass tc = new TestClass();

			//act
			bool result = loader.LoadFromJSON<TestClass>(path, out tc);

			//assert
			Assert.IsNull(tc);
			Assert.IsFalse(result);

		}

		private void LoadFromJSONTestArgumentException()
		{
			//arrange
			Loader loader = new Loader();
			string path = "";
			TestClass tc = new TestClass();

			//act
			bool result = loader.LoadFromJSON<TestClass>(path, out tc);

			//assert
			Assert.IsNull(tc);
			Assert.IsFalse(result);
		}

		private void LoadFromJSONTestInvalidJSON()
		{
			//arrange
			Loader loader = new Loader();
			string path = @"F:\Projects\GitRepos\csharp_project\datasets\invalid_json.json";
			TestClass tc = new TestClass();

			//act
			bool result = loader.LoadFromJSON<TestClass>(path, out tc);

			//assert
			Assert.IsNull(tc);
			Assert.IsFalse(result);
		}

		[TestMethod()]
		public void LoadFromJSONTest()
		{
						
			LoadFromJSONTestArgumentException();
			LoadFromJSONTestFileNotFound();
			LoadFromJSONTestInvalidJSON();

		}
	}
}