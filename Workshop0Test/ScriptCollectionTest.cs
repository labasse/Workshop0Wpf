using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Workshop0.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Workshop0Test
{
    [TestClass]
    public sealed class ScriptCollectionTest
    {
        private Stream StringToStream(string s)
        {
            var res = new MemoryStream();

            using var writer = new StreamWriter(res, leaveOpen: true);

            writer.WriteLine(s);
            writer.Flush();
            return res;
        }

        [TestMethod]
        public void InitTestData()
        {
            // (Arrange) Act
            var test = ScriptCollection.InitTestData();

            // Assert
            Assert.HasCount(6, test.Scripts);
            Assert.HasCount(2, test.Scripts[0].Parameters);
            Assert.HasCount(2, test.Scripts[1].Parameters);
            for(var i = 2; i<6; i++)
                Assert.HasCount(0, test.Scripts[i].Parameters);
        }

        [TestMethod]
        public void SetPath_ValidWindowsPath()
        {
            // Arrange
            var test = new ScriptCollection();

            // Act
            test.Path = @"C:\test\scripts.xml";

            // Assert
            Assert.AreEqual(@"C:\test\scripts.xml", test.Path);
        }

        [TestMethod]
        public void SetPath_InvalidWindowsPath() 
        {
            var test = new ScriptCollection();

            var action = () => test.Path = @":\test\scripts.xml";

            Assert.Throws<ArgumentException>(action);
        }

        [TestMethod]
        public void SetPath_EmptyPath()
        {
            var test = new ScriptCollection();

            var action = () => test.Path = "";

            Assert.Throws<ArgumentException>(action);
        }

        [TestMethod]
        public void Load_ValidXmlWithScripts() // Usuel
        {
            using var s = StringToStream("""
                <?xml version="1.0" encoding="utf-8"?>
                <scripts>
                    <script name="Script1" description="Description1">
                        <parameters>
                            <parameter name="Param1" type="string" />
                            <parameter name="Param2" type="int" />
                        </parameters>
                    </script>
                    <script name="Script2" description="Description2">
                        <parameters>
                            <parameter name="ParamA" type="bool" />                            
                        </parameters>
                    </script>
                </scripts>
                """
            );
            var test = ScriptCollection.Load(s);
            Assert.HasCount(2, test.Scripts);
            Assert.HasCount(2, test.Scripts[0].Parameters);
            Assert.HasCount(1, test.Scripts[1].Parameters);
        }

        [TestMethod]
        public void Load_ValidXmlEmpty() // Extrême
        {
        }

        [TestMethod]
        public void Load_MalformedXml() // Erreur
        {
        }

        [TestMethod]
        public void Load_InvalidXml() // Erreur
        {
        }
    }
}
