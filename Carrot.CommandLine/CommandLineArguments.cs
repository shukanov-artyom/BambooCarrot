using System;
using System.Collections.Generic;

namespace Carrot.Configuration
{
    public static class CommandLineArguments
    {
        public static readonly string Seq = "-seq";
        public static readonly string Bunny = "-bunny";
        public static readonly string BunnyUser = "-bunnyuser";
        public static readonly string BunnyPwd = "-bunnypasswd";
        public static readonly string UpdateFolder = "-updatefolder";

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
