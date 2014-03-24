using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsAndDataStructs;
using DesignPatterns;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LinkedListAddFirst()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(7);
            list.AddFirst(5);
            list.AddFirst(3);

            Assert.AreEqual(3, list.Head.Value);
        }

        [TestMethod]
        public void HashTableTest()
        {
            HashTable<string, int> table = new HashTable<string, int>(100);
            table.Add("brad", 100);
            table.Add("rich", 200);
            table.Remove("rich");
            Assert.AreEqual(100, table.Find("brad"));
            Assert.AreEqual(0, table.Find("rich"));
        }

        [TestMethod]
        public void AdapterPatternStub()
        {
            var myRenderer = new DataRenderer(new StubAdapter());

            var writer = new StringWriter();
            myRenderer.Render(writer);

            string result = writer.ToString();
            Console.Write(result);
            int lineCount = result.Count(c => c.ToString() == "\n");
            Assert.AreEqual(3, lineCount);
        }
    }
}
