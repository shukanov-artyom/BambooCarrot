using System;
using System.Collections.Generic;

namespace Carrot.Configuration
{
    public static class CmdLineToConfigKeyMapping
    {
        public static readonly IDictionary<string, string> Mapping = 
            new Dictionary<string, string>()
            {
                {CommandLineArguments.UpdateFolder, "UpdateFolder"},
                {CommandLineArguments.Seq, "SeqServiceUrl"},
                {CommandLineArguments.Bunny, "BambooBuildBunnyUrl" },
                {CommandLineArguments.BunnyUser, "BambooBuildBunnyUser"},
                {CommandLineArguments.BunnyPwd, "BambooBuildBunnyPassword"},
            };
    }
}
