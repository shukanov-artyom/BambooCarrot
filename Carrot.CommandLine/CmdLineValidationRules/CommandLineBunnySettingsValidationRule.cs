using System;

namespace Carrot.Configuration.CmdLineValidationRules
{
    internal class CommandLineBunnySettingsValidationRule
    {
        public bool Validate(CarrotCommandLine cmds)
        {
            bool hasUserName = cmds.ContainsKey(CommandLineArguments.BunnyUser);
            bool hasPasswd = cmds.ContainsKey(CommandLineArguments.BunnyPwd);
            if (cmds.ContainsKey(CommandLineArguments.Bunny))
            {
                if (hasUserName && !hasPasswd)
                {
                    throw new ArgumentException("Bamboo Build Bunny service password must be specified.");
                }
                if (!hasUserName && hasPasswd)
                {
                    throw new ArgumentException("Bamboo Build Bunny service username must be specified.");
                }
                if (!hasUserName && !hasPasswd)
                {
                    throw new ArgumentException("Bamboo Build Bunny service usename and password must be specified.");
                }
            }
            else
            {
                if (hasUserName || hasPasswd)
                {
                    throw new ArgumentException("Bamboo Build Bunny service URL is not specified.");
                }
            }
            return true;
        }
    }
}
