using System;
using System.Collections.Generic;

namespace Carrot.Configuration
{
    public class CommandLineArguments
    {
        public static readonly string Seq = "-seq";
        public static readonly string Bunny = "-bunny";
        public static readonly string BunnyUser = "-bunnyUser";
        public static readonly string BunnyPwd = "-bunnyPasswd";
        public static readonly string UpdateFolder = "-updateFolder";

        public static IEnumerable<string> AllArguments { get; } = new HashSet<string>()
            {
                Seq,
                Bunny,
                BunnyUser,
                BunnyPwd,
                UpdateFolder
            };

        public static IEnumerable<string> ArgumentWithValue { get; } = new HashSet<string>()
            {
                Seq,
                Bunny,
                BunnyUser,
                BunnyPwd,
                UpdateFolder
            };
    }
}
