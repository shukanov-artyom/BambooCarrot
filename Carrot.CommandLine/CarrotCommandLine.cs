using System;
using System.Collections.Generic;
using System.Linq;
using Carrot.Configuration.CmdLineValidationRules;

namespace Carrot.Configuration
{
    public class CarrotCommandLine
    {
        private readonly IDictionary<string, string> values = new Dictionary<string, string>(
            StringComparer.InvariantCultureIgnoreCase);

        private static readonly List<dynamic> Validators = new List<dynamic>()
        {
            new CommandLineBunnySettingsValidationRule()
        };

        public CarrotCommandLine(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string argName = args[i].ToLower();
                if (!CommandLineArguments.AllArguments.Contains(argName))
                {
                    throw new NotSupportedException(
                        String.Format("Parameter {0} not supported.", argName));
                }
                bool requiresValue = CommandLineArguments.ArgumentWithValue.Contains(argName);
                if (requiresValue && i >= args.Length - 1)
                {
                    throw new ArgumentException(
                        String.Format("The value is not specified for argument {0}", argName));
                }
                string argValue;
                if (requiresValue)
                {
                    ++i;
                    if (CommandLineArguments.AllArguments.Contains(args[i]))
                    {
                        throw new ArgumentException(
                            String.Format("Proper value is not specified for argument {0}", argName));
                    }
                    argValue = args[i];
                }
                else
                {
                    argValue = null;
                }
                this[argName] = argValue;
            }
        }

        /// <summary>
        /// Provides access to values of parameters with values.
        /// </summary>
        public string this[string argument]
        {
            get
            {
                return values[argument];
            }
            set
            {
                values[argument] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            return values.ContainsKey(key);
        }

        /// <summary>
        /// Exposes provided configuration keys.
        /// </summary>
        public string[] ProvidedKeys
        {
            get
            {
                return values.Keys.ToArray();
            }
        }

        public bool Validate()
        {
            bool result = true;
            foreach (dynamic validator in Validators)
            {
                result = result && validator.Validate(this);
            }
            return result;
        }
    }
}
