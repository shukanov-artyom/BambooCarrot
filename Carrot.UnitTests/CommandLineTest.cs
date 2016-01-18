using System;
using System.Linq;
using NUnit.Framework;

namespace Carrot.Configuration.Tests
{
    [TestFixture]
    public class CommandLineTest
    {
        [Test]
        public void ParseValidCommandLineTest()
        {
            CarrotCommandLine cmd = new CarrotCommandLine(GetValidArgs());
            Assert.IsTrue(cmd.ContainsKey(CommandLineArguments.Seq));
            Assert.IsTrue(cmd[CommandLineArguments.Seq].Equals("http:\\sequrl"));
            Assert.IsTrue(cmd.ContainsKey(CommandLineArguments.Bunny));
            Assert.IsTrue(cmd[CommandLineArguments.Bunny].Equals("http:\\bunyyurl.json"));
            Assert.IsTrue(cmd.ContainsKey(CommandLineArguments.BunnyUser));
            Assert.IsTrue(cmd[CommandLineArguments.BunnyUser].Equals("username"));
            Assert.IsTrue(cmd.ContainsKey(CommandLineArguments.BunnyPwd));
            Assert.IsTrue(cmd[CommandLineArguments.BunnyPwd].Equals("password"));
            Assert.IsTrue(cmd.ContainsKey(CommandLineArguments.UpdateFolder));
            Assert.IsTrue(cmd[CommandLineArguments.UpdateFolder].Equals("\\\\updateserver\\updatefolder"));
        }

        private static string[] GetValidArgs()
        {
            string commandLine = "-seq http:\\sequrl -bunny http:\\bunyyurl.json -bunnyuser username -bunnypasswd password -updatefolder \\\\updateserver\\updatefolder";
            return commandLine.Split().ToArray();
        }

        private static string[] GetInvalidArgs()
        {
            string commandLine = "";
            return commandLine.Split().ToArray();
        }
    }
}
