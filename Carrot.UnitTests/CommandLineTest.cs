using System;
using System.Linq;
using Carrot.Configuration.CmdLineValidationRules;
using NUnit.Framework;

namespace Carrot.Configuration.Tests
{
    [TestFixture]
    public class CommandLineTest
    {
        private const string ValidCommandLine =
            "-seq http:\\sequrl -bunny http:\\bunyyurl.json -bunnyuser username -bunnypasswd password -updatefolder \\\\updateserver\\updatefolder";

        [Test]
        public void ParseValidCommandLineTest()
        {
            CarrotCommandLine cmd = new CarrotCommandLine(Args(
                "-seq http:\\sequrl -bunny http:\\bunyyurl.json -bunnyuser username -bunnypasswd password -updatefolder \\\\updateserver\\updatefolder"));
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

        [Test]
        public void ParseNonexistentCommandLineParameter()
        {
            try
            {
                // changed -seq to -sew
                CarrotCommandLine cmd = new CarrotCommandLine(Args(
                    "-sew http:\\sequrl -bunny http:\\bunyyurl.json -bunnyuser username -bunnypasswd password -updatefolder \\\\updateserver\\updatefolder"));
            }
            catch (NotSupportedException)
            {
                // nothing
            }
        }

        [Test]
        public void TestBunnyValidationRule()
        {
            var cmdline = new CarrotCommandLine(Args(
                "-seq http:\\sequrl -bunnyuser username -bunnypasswd password"));
            var rule = new CommandLineBunnySettingsValidationRule();
            try
            {
                rule.Validate(cmdline);
            }
            catch (ArgumentException)
            {
                // nothing
            }
        }

        [Test]
        public void CommandLineValidator()
        {
            var cmdline = new CarrotCommandLine(Args(
                "-seq http:\\sequrl -bunnyuser username -bunnypasswd password"));
            var rule = new CommandLineBunnySettingsValidationRule();
            try
            {
                cmdline.Validate();
            }
            catch (ArgumentException)
            {
                // nothing
            }
            Assert.IsTrue(new CarrotCommandLine(Args(ValidCommandLine)).Validate());
        }

        private static string[] Args(string line)
        {
            return line.Split().ToArray();
        }
    }
}
